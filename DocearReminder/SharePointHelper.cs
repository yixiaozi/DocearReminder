using Microsoft.SharePoint.Client;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocearReminder
{
    public class SharePointHelper
    {
        public static ClientContext CreateAuthenticatedContext(string siteUrl, string userName, string password, string clientId = "", string tenantId = "")
        {
            if (string.IsNullOrWhiteSpace(siteUrl))
            {
                throw new ArgumentException("SharePoint site URL is empty.", nameof(siteUrl));
            }

            // SharePoint Online now commonly requires TLS 1.2.
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            if (!string.IsNullOrWhiteSpace(clientId))
            {
                return CreateModernAuthenticatedContext(siteUrl, clientId, tenantId);
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("SharePoint user name is empty.", nameof(userName));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("SharePoint password is empty.", nameof(password));
            }

            ClientContext context = new ClientContext(siteUrl);
            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            context.Credentials = new SharePointOnlineCredentials(userName, securePassword);
            return context;
        }

        private static ClientContext CreateModernAuthenticatedContext(string siteUrl, string clientId, string tenantId)
        {
            Uri siteUri = new Uri(siteUrl);
            string tenantSegment = string.IsNullOrWhiteSpace(tenantId) ? "organizations" : tenantId;
            string authority = "https://login.microsoftonline.com/" + tenantSegment;
            string[] scopes = new string[] { siteUri.Scheme + "://" + siteUri.Host + "/.default" };

            IPublicClientApplication app = PublicClientApplicationBuilder
                .Create(clientId)
                .WithAuthority(authority)
                .WithRedirectUri("http://localhost")
                .Build();

            AuthenticationResult result = null;
            IEnumerable<IAccount> accounts = app.GetAccountsAsync().GetAwaiter().GetResult();
            IAccount firstAccount = accounts.FirstOrDefault();

            try
            {
                if (firstAccount != null)
                {
                    result = app.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync().GetAwaiter().GetResult();
                }
            }
            catch (MsalUiRequiredException)
            {
                result = null;
            }

            if (result == null)
            {
                result = app.AcquireTokenInteractive(scopes).ExecuteAsync().GetAwaiter().GetResult();
            }

            ClientContext context = new ClientContext(siteUrl);
            context.ExecutingWebRequest += (sender, args) =>
            {
                args.WebRequestExecutor.WebRequest.Headers["Authorization"] = "Bearer " + result.AccessToken;
            };
            return context;
        }

        public static ListItem GetListItem(ClientContext content,List list,string Title)
        {
            //查找Title等于reminder.json的条目
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = @"<View>
                                    <Query>
                                        <Where>
                                            <Eq>
                                                <FieldRef Name='Title' />
                                                <Value Type='Text'>"+ Title + @"</Value>
                                            </Eq>
                                        </Where>
                                    </Query>
                                </View>";
            ListItemCollection listItems = list.GetItems(camlQuery);
            content.Load(listItems);
            content.ExecuteQuery();
            //如果没有找到，就创建一个
            if (listItems.Count == 0)
            {
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                ListItem newItem = list.AddItem(itemCreateInfo);
                newItem["Title"] = Title;
                newItem.Update();
                content.ExecuteQuery();
                return newItem;
            }
            //获取reminder.json的内容
            ListItem item = listItems[0];
            content.Load(item);
            content.ExecuteQuery();
            return item;
        }
    }
}

using Microsoft.SharePoint.Client;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
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
            string sharePointResource = siteUri.Scheme + "://" + siteUri.Host;
            string[] scopes = new string[] { sharePointResource + "/AllSites.Write" };

            IPublicClientApplication app = PublicClientApplicationBuilder
                .Create(clientId)
                .WithAuthority(authority)
                .WithRedirectUri("http://localhost")
                .Build();

            AuthenticationResult result = null;
            IEnumerable<IAccount> accounts = Task.Run(() => app.GetAccountsAsync()).GetAwaiter().GetResult();
            IAccount firstAccount = accounts.FirstOrDefault();

            try
            {
                if (firstAccount != null)
                {
                    result = Task.Run(() => app.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync()).GetAwaiter().GetResult();
                }
            }
            catch (MsalUiRequiredException)
            {
                result = null;
            }

            if (result == null)
            {
                using (CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromMinutes(5)))
                {
                    try
                    {
                        result = Task.Run(() => app
                            .AcquireTokenInteractive(scopes)
                            .WithUseEmbeddedWebView(false)
                            .WithPrompt(Prompt.SelectAccount)
                            .ExecuteAsync(cts.Token)).GetAwaiter().GetResult();
                    }
                    catch (MsalServiceException ex) when (ex.ErrorCode == "invalid_resource" || ex.Message.Contains("AADSTS650057"))
                    {
                        throw new InvalidOperationException(
                            "Azure app registration is missing SharePoint delegated permissions. " +
                            "Please add SharePoint delegated permission AllSites.Write (or AllSites.Read/AllSites.Manage), then grant admin consent. " +
                            "Current requested resource: " + sharePointResource, ex);
                    }
                }
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

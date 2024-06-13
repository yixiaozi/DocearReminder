using Microsoft.SharePoint.Client;
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
        public static ClientContext CreateAuthenticatedContext(string siteUrl, string userName, string password)
        {
            // 创建 ClientContext 实例
            ClientContext context = new ClientContext(siteUrl);

            // 将密码转换为 SecureString
            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            // 设置 SharePointOnlineCredentials
            context.Credentials = new SharePointOnlineCredentials(userName, securePassword);
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
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
                                                <Value Type='Text'>reminder.json</Value>
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
                newItem["Title"] = "reminder.json";
                newItem["Content"] = "[]";
                newItem.Update();
                content.ExecuteQuery();
            }
            //获取reminder.json的内容
            ListItem item = listItems[0];
            content.Load(item);
            content.ExecuteQuery();
            return item;
        }
    }
}

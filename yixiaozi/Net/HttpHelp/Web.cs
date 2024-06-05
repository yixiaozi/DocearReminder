using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace yixiaozi.Net.HttpHelp
{
    public class Web
    {
        public static String getTitle(String url)
        {
            //请求资源  
            System.Net.WebRequest wb = System.Net.WebRequest.Create(url.Trim());

            //响应请求  
            WebResponse webRes = null;

            //将返回的数据放入流中  
            System.IO.Stream webStream = null;
            try
            {
                webRes = wb.GetResponse();
                webStream = webRes.GetResponseStream();
            }
            catch (Exception e)
            {
                return "输入的网址不存在或非法...";
            }


            //从流中读出数据  (这里如果乱码改变编码即可)
            StreamReader sr = new StreamReader(webStream, System.Text.Encoding.UTF8);

            //创建可变字符对象，用于保存网页数据   
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //读出数据存入可变字符中  
            String str = "";
            while ((str = sr.ReadLine()) != null)
            {
                sb.Append(str);
            }

            //建立获取网页标题正则表达式  
            String regex = @"<title>.+</title>";

            //返回网页标题  
            String title = Regex.Match(sb.ToString(), regex).ToString();
            title = Regex.Replace(title, @"[\""]+", "");
            title = title.Replace("<title>", "").Replace("</title>", "");
            title = Regex.Replace(title, @"((?=[\x21-\x7e]+)[^A-Za-z0-9])", "");
            if (title.Length>50)
            {
                if (title.Contains('<'))
                {
                    title = title.Split('<')[0];
                    return title;
                }
                return title.Substring(0, 49);
            }
            return title;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Net.HttpHelp
{
    public class HttpRequest
    {
        //此处自定义函数HttpGet
        public static string HttpGet(string url)
        {
            Encoding encoding = Encoding.UTF8;//编码方式，此处为UTF-8
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//创建Http请求
            request.Method = "GET";//请求方法，此处为GET
            request.Accept = "text/html, application/xhtml+xml, */*";//媒体类型
            request.ContentType = "application/json";//JSON数据格式
            request.Timeout = 20000;//超时时间,以毫秒为单位
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();//发送请求
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();//返回数据
            }
        }
    }
}

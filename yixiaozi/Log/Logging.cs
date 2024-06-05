using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace yixiaozi
{
    public class Logging
    {
        public static string logFilePath = "";
        public Logging()
        {
            logFilePath = "log"+ DateTime.Now.ToFileTime();;
        }
        //记录日志   
        public void RecordLogTxt(string content)
        {
            if (logFilePath==""|| logFilePath == null)
            {
                logFilePath = "log" + DateTime.Now.TimeOfDay ;
            }
            string logSite = AppDomain.CurrentDomain.BaseDirectory + logFilePath + ".txt";//本地文件
            if (!System.IO.File.Exists(logSite))
            {
                System.IO.File.Create(logSite).Dispose(); ;
            }

            //历史记录读取
            List<string> historyRecord = new List<string>();
            StreamReader sr = new StreamReader(logSite, false);
            string readLine;
            while (!string.IsNullOrEmpty(readLine = sr.ReadLine()))
            {
                historyRecord.Add(readLine);
            }
            sr.Close();
            sr.Dispose();
            historyRecord.Add(content);

            //新纪录存储
            StreamWriter sw = new StreamWriter(logSite, false);
            for (int i = 0; i < historyRecord.Count; i++)
            {
                sw.WriteLine(historyRecord[i]);
            }
            sw.Close();
            sw.Dispose();
        }
    }
}

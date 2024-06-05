using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Log.LogToText
{
    class LogToText
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="logtype">日志类型</param>
        public static void WriteLog(string message, LogType logtype)
        {
            try
            {
                DateTime now = DateTime.Now;

                string LogFolderPath = "";
                //如果不存在该文件夹则创建该文件夹
                if (!Directory.Exists(LogFolderPath))
                    Directory.CreateDirectory(LogFolderPath);
                string fileName = now.ToString("yyyy-MM-dd-HH");
                switch (logtype)
                {
                    case LogType.Info:
                        {
                            fileName = fileName + "_INFO";
                            break;
                        }
                    case LogType.Error:
                        {
                            fileName = fileName + "_ERROR";
                            break;
                        }
                    default:
                        {
                            fileName = fileName + "_INFO";
                            break;
                        }
                }
                //日志文件保存的路径
                string strFilePath = string.Format("{0}\\{1}.txt", LogFolderPath, fileName);
                //追加文件
                using (FileStream fs = new FileStream(strFilePath, FileMode.Append, FileAccess.Write, FileShare.Write))
                {
                    if (fs != null)
                    {
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                        {
                            if (sw != null)
                            {
                                //写信息
                                sw.WriteLine(string.Format("{0}:{1}", "[" + now.ToString("yyyy-MM-dd HH:mm:ss") + "]", message));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">日志类容</param>
        /// <param name="logtype">日志类型</param>
        public static void WriteLog(string title, string message, LogType logtype)
        {
            WriteLog("(" + title + ")" + message, logtype);
        }
    }

    /// <summary>
    /// 日志枚举
    /// </summary>
    public enum LogType
    {
        Info,
        Error
    }
}

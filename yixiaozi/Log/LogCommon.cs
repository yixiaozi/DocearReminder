using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.SharePoint;
using System.Security.AccessControl;
using System.Configuration;
using System.Web;

namespace yixiaozi.Log
{
    public class LogCommon : System.Web.UI.Page
    {

        private static void writeLog(string path, string error1, string PageName)
        {
            //string path = @"c:\\UserCenter.txt";
            StringBuilder ErrMess = new StringBuilder();
            ErrMess.Append(@"WebPart：'" + PageName + "'  时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ErrMess.Append("错误信息：" + error1);
            if (File.Exists(path))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ErrMess);
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    sw.WriteLine(sb.ToString());
                    sw.Close();
                }
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312")))//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                    {
                        sw.WriteLine(ErrMess);
                        sw.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="error1">错误详细信息</param>
        /// <param name="PartName">错误发生的位置</param>
        public static void writeLogMessage(string error1, string PartName, HttpContext context)
        {
            try
            {
                string folderPath = context.Server.MapPath("~/SystemLog/");
                //星期天为第一天  
                DateTime datetime = DateTime.Now;
                int weeknow = Convert.ToInt32(datetime.DayOfWeek);
                int daydiff = (-1) * weeknow;
                //本周第一天  
                string firstDay = datetime.AddDays(daydiff).ToString("yyyyMMdd") + ".txt";
                if (string.IsNullOrEmpty(folderPath))
                {
                    folderPath = @"c:\AppLog";
                }
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string allPath = folderPath + "\\" + firstDay;
                if (!File.Exists(allPath))
                {
                    using (FileStream fs = new FileStream(allPath, FileMode.Create))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            //获取文件信息
                            FileInfo fileInfo = new FileInfo(allPath);
                            //获得该文件的访问权限
                            System.Security.AccessControl.FileSecurity fileSecurity = fileInfo.GetAccessControl();
                            //添加ereryone用户组的访问权限规则 完全控制权限
                            fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                            //添加Users用户组的访问权限规则 完全控制权限
                            fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                            //设置访问权限
                            fileInfo.SetAccessControl(fileSecurity);
                        }
                    }
                }
                writeLog(allPath, error1, PartName);
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="error1">错误详细信息</param>
        /// <param name="PartName">错误发生的位置</param>
        /// <param name="folderPath">记录错误的文件夹路径，适用于TimeJob中调用</param>
        public static void writeLogMessage(string error1, string PartName, string folderPath)
        {
            //string folderPath = ConfigurationManager.AppSettings["AppLogFolderPath"];//OAHelper.GetSystemArgs("AppLogFolderPath", SPContext.Current.Site.RootWeb, true);
            //星期天为第一天  
            DateTime datetime = DateTime.Now;
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (-1) * weeknow;
            //本周第一天  
            string firstDay = datetime.AddDays(daydiff).ToString("yyyyMMdd") + ".txt";
            if (string.IsNullOrEmpty(folderPath))
            {
                folderPath = @"c:\AppLog";
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string allPath = folderPath + "\\" + firstDay;
            if (!File.Exists(allPath))
            {
                using (FileStream fs = new FileStream(allPath, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        //获取文件信息
                        FileInfo fileInfo = new FileInfo(allPath);
                        //获得该文件的访问权限
                        System.Security.AccessControl.FileSecurity fileSecurity = fileInfo.GetAccessControl();
                        //添加ereryone用户组的访问权限规则 完全控制权限
                        fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                        //添加Users用户组的访问权限规则 完全控制权限
                        fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                        //设置访问权限
                        fileInfo.SetAccessControl(fileSecurity);
                    }
                }
            }
            writeLog(allPath, error1, PartName);
        }


        public static void writeAdExportLog(string log)
        {
            writeAdLog(@"c:\\AdExport.txt", log);
        }

        public static void writeExcelLog(string log)
        {
            string path = @"c:\\ExcelImportLog.txt";
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    sw.WriteLine(log);
                    sw.Close();
                }
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312")))//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                    {
                        sw.WriteLine(log);
                        sw.Close();
                    }
                }

            }
        }

        public static void ClearExcelLog()
        {
            string path = @"c:\\ExcelImportLog.txt";
            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }

        }

        private static void writeAdLog(string path, string log)
        {
            //string path = @"c:\\UserCenter.txt";
            StringBuilder ErrMess = new StringBuilder();
            ErrMess.Append(@"记录：" + log + "'——时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (File.Exists(path))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ErrMess);
                StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.GetEncoding("GB2312"));
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("GB2312")))//通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
                    {
                        sw.WriteLine(ErrMess);
                        sw.Close();
                    }
                }
            }
        }
    }
}

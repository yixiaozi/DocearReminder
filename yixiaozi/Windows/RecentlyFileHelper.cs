using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using yixiaozi.Model.DocearReminder;

namespace yixiaozi.Windows
{
    public class RecentlyFileHelper
    {
        public static MyListBoxItemRemind GetShortcutTargetFile(string shortcutFilename, string searchwork)
        {
            try
            {
                var type = Type.GetTypeFromProgID("WScript.Shell");  //获取WScript.Shell类型
                object instance = Activator.CreateInstance(type);    //创建该类型实例
                var result = type.InvokeMember("CreateShortCut", BindingFlags.InvokeMethod, null, instance, new object[] { shortcutFilename });
                var targetFile = result.GetType().InvokeMember("TargetPath", BindingFlags.GetProperty, null, result, null) as string;
                FileInfo file = new FileInfo(targetFile);
                FileInfo file1 = new FileInfo(shortcutFilename);
                if (file.FullName.ToLower().Contains("onedrive") || file.Name.StartsWith(".") || !file.FullName.Contains("yixiaozi"))
                {
                    return null;
                }
                if (!file.FullName.Contains(searchwork))
                {
                    return null;
                }
                return new MyListBoxItemRemind() { Text = "       " + file.Name, Name = file.Name, Value = file.FullName, Time = (new DateTime() + (DateTime.Now.AddYears(80) - file1.LastWriteTime)) };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<MyListBoxItemRemind> GetRecentlyFiles(string searchwork)
        {
            var recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);  //获取Recent路径
            return from file in Directory.EnumerateFiles(recentFolder)
                   where Path.GetExtension(file) == ".lnk"
                   select GetShortcutTargetFile(file, searchwork);
        }
        public static void DeleteRecentlyFiles(string searchwork)
        {
            var recentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            foreach (string file in Directory.EnumerateFiles(recentFolder))
            {
                if (file.Contains(searchwork))
                {
                    System.IO.File.Delete(file);
                }

            }
        }
        public static IEnumerable<StationInfo> GetStartFiles()
        {
            try
            {
                return from file in Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms), "*.lnk", SearchOption.AllDirectories)
                       where GetShortcutTargetFile(file) != null
                       select GetShortcutTargetFile(file);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static StationInfo GetShortcutTargetFile(string shortcutFilename)
        {
            try
            {
                var type = Type.GetTypeFromProgID("WScript.Shell");  //获取WScript.Shell类型
                object instance = Activator.CreateInstance(type);    //创建该类型实例
                var result = type.InvokeMember("CreateShortCut", BindingFlags.InvokeMethod, null, instance, new object[] { shortcutFilename });
                var targetFile = result.GetType().InvokeMember("TargetPath", BindingFlags.GetProperty, null, result, null) as string;
                FileInfo file = new FileInfo(targetFile);
                FileInfo file1 = new FileInfo(shortcutFilename);
                return new StationInfo { StationName_CN = "start:" + file1.Name.Substring(0, file1.Name.LastIndexOf(".")), mindmapurl = file.FullName };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

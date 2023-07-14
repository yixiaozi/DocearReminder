using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;
using System.Text;

namespace DocearReminder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Mutex run = new System.Threading.Mutex(true, "DocearReminder", out bool runone);
            if (runone)
            {
                run.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //检查自动更新？
                //判断一下是否设置了账号密码，如果为空则打开设置账号密码的窗口
                IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
                bool IsSetPassword = ini.ReadString("password", "i", "") != "";
                if (IsSetPassword)
                {
                    LoginForm form1 = new LoginForm();
                    if (form1.autologin||form1.ShowDialog() == DialogResult.OK)
                    {
                        //Application.Run(new DocearReminderForm());
                        Application.Run(new DrawIO());
                    }
                }
                else
                {
                    Application.Run(new Password());
                }
            }
            else
            {
                //Application.Run(new CalendarForm(System.IO.Path.GetFullPath(((new IniFile(System.IO.Path.GetFullPath(@".\config.ini"))).ReadString("path", "rootpath", "")))));
            }
        }
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("DocearReminder.Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }

        public class IniFile
        {
            private string m_FileName;

            public string FileName
            {
                get { return m_FileName; }
                set { m_FileName = value; }
            }

            [DllImport("kernel32.dll")]
            private static extern int GetPrivateProfileInt(
                string lpAppName,
                string lpKeyName,
                int nDefault,
                string lpFileName
                );

            [DllImport("kernel32.dll")]
            private static extern int GetPrivateProfileString(
                string lpAppName,
                string lpKeyName,
                string lpDefault,
                StringBuilder lpReturnedString,
                int nSize,
                string lpFileName
                );

            [DllImport("kernel32.dll")]
            private static extern int WritePrivateProfileString(
                string lpAppName,
                string lpKeyName,
                string lpString,
                string lpFileName
                );
            [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
            private static extern uint GetPrivateProfileStringA(string section, string key,
                string def, Byte[] retVal, int size, string filePath);
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="aFileName">Ini文件路径</param>
            public IniFile(string aFileName)
            {
                this.m_FileName = aFileName;
            }

            /// <summary>
            /// 构造函数
            /// </summary>
            public IniFile()
            { }

            /// <summary>
            /// [扩展]读Int数值
            /// </summary>
            /// <param name="section">节</param>
            /// <param name="name">键</param>
            /// <param name="def">默认值</param>
            /// <returns></returns>
            public int ReadInt(string section, string name, int def)
            {
                return GetPrivateProfileInt(section, name, def, this.m_FileName);
            }

            /// <summary>
            /// [扩展]读取string字符串
            /// </summary>
            /// <param name="section">节</param>
            /// <param name="name">键</param>
            /// <param name="def">默认值</param>
            /// <returns></returns>
            public string ReadString(string section, string name, string def)
            {
                return ReadStringDefault(section, name, def);
                //do not need any more;
                //StringBuilder vRetSb = new StringBuilder(2048);
                //GetPrivateProfileString(section, name, def, vRetSb, 2048, this.m_FileName);
                //string result = vRetSb.ToString();
                //if (result.Contains(@":\"))
                //{
                //    if (result[0] != System.AppDomain.CurrentDomain.BaseDirectory[0])
                //    {
                //        result = System.AppDomain.CurrentDomain.BaseDirectory[0] + result.Substring(1);
                //    }
                //}
                //return result;
            }
            /// <summary>
            /// [扩展]读取string字符串
            /// </summary>
            /// <param name="section">节</param>
            /// <param name="name">键</param>
            /// <param name="def">默认值</param>
            /// <returns></returns>
            public string ReadStringDefault(string section, string name, string def)
            {
                StringBuilder vRetSb = new StringBuilder(2048);
                GetPrivateProfileString(section, name, def, vRetSb, 2048, this.m_FileName);
                string result = vRetSb.ToString();
                return result;
            }

            /// <summary>
            /// [扩展]写入Int数值，如果不存在 节-键，则会自动创建
            /// </summary>
            /// <param name="section">节</param>
            /// <param name="name">键</param>
            /// <param name="Ival">写入值</param>
            public void WriteInt(string section, string name, int Ival)
            {

                WritePrivateProfileString(section, name, Ival.ToString(), this.m_FileName);
            }

            /// <summary>
            /// [扩展]写入String字符串，如果不存在 节-键，则会自动创建
            /// </summary>
            /// <param name="section">节</param>
            /// <param name="name">键</param>
            /// <param name="strVal">写入值</param>
            public void WriteString(string section, string name, string strVal)
            {
                WritePrivateProfileString(section, name, strVal, this.m_FileName);
            }

            /// <summary>
            /// 删除指定的 节
            /// </summary>
            /// <param name="section"></param>
            public void DeleteSection(string section)
            {
                WritePrivateProfileString(section, null, null, this.m_FileName);
            }

            /// <summary>
            /// 删除全部 节
            /// </summary>
            public void DeleteAllSection()
            {
                WritePrivateProfileString(null, null, null, this.m_FileName);
            }

            /// <summary>
            /// 读取指定 节-键 的值
            /// </summary>
            /// <param name="section"></param>
            /// <param name="name"></param>
            /// <returns></returns>
            public string IniReadValue(string section, string name)
            {
                StringBuilder strSb = new StringBuilder(256);
                GetPrivateProfileString(section, name, "", strSb, 256, this.m_FileName);
                return strSb.ToString();
            }

            /// <summary>
            /// 写入指定值，如果不存在 节-键，则会自动创建
            /// </summary>
            /// <param name="section"></param>
            /// <param name="name"></param>
            /// <param name="value"></param>
            public void IniWriteValue(string section, string name, string value)
            {
                WritePrivateProfileString(section, name, value, this.m_FileName);
            }

            public List<string> ReadSections()
            {
                return ReadSections(this.m_FileName);
            }

            public List<string> ReadSections(string iniFilename)
            {
                List<string> result = new List<string>();
                Byte[] buf = new Byte[65536];
                uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, iniFilename);
                int j = 0;
                for (int i = 0; i < len; i++)
                    if (buf[i] == 0)
                    {
                        result.Add(Encoding.Default.GetString(buf, j, i - j));
                        j = i + 1;
                    }
                return result;
            }

            public List<string> ReadKeys(String SectionName)
            {
                return ReadKeys(SectionName, this.m_FileName);
            }

            public List<string> ReadKeys(string SectionName, string iniFilename)
            {
                List<string> result = new List<string>();
                Byte[] buf = new Byte[65536];
                uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
                int j = 0;
                for (int i = 0; i < len; i++)
                    if (buf[i] == 0)
                    {
                        result.Add(Encoding.Default.GetString(buf, j, i - j));
                        j = i + 1;
                    }
                return result;
            }

        }

    }
}

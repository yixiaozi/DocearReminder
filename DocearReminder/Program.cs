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
using yixiaozi.Windows;
using yixiaozi.Config;

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
            Mutex run = new System.Threading.Mutex(true, "DocearReminder", out bool runone);
            if (runone)
            {
                run.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //检查自动更新？
                //判断一下是否设置了账号密码，如果为空则打开设置账号密码的窗口
                bool IsSetPassword = true;
                if (true)
                {
                    LoginForm form1 = new LoginForm();
                    if (form1.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new DocearReminderForm());
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
        
    }
}

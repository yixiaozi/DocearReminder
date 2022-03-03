﻿using System;
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
                Form1 form1 = new Form1();
                if (form1.ShowDialog() == DialogResult.OK)
                {
                    
                    Application.Run(new DocearReminderForm());
                }
            }
            else
            {
                Application.Run(new CalendarForm((new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini")).ReadString("path", "rootpath", "")));
            }

            //new MyProcess().OnlyOneForm("DocearReminder.exe");
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DocearReminderForm());
        }
        
    }
}

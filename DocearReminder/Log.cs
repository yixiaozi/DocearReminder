using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using yixiaozi.Config;
using yixiaozi.Model.DocearReminder;
using yixiaozi.WinForm.Common;
using static DocearReminder.DocearReminderForm;

namespace DocearReminder
{
    public partial class Log : Form
    {
        List<result> logs = new List<result>();
        static string logpass = "niqishihenhao";
        public Log()
        {
            InitializeComponent();
            logpass = ini.ReadString("password", "i", "");
            resultlistBox.Items.Add(new result { Words = "", path = "" });
            resultlistBox.Items.Clear();
            GetLog();
            logs = logs.OrderByDescending(m=>m.Time).ToList();
            resultlistBox.DisplayMember = "words";
            resultlistBox.ValueMember = "path";
            SearchLog();
            Center();
            keyword.Focus();
        }
        public void GetLog()
        {
            logs.Clear();
            resultlistBox.Items.Clear();
            string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            GetFileLog(fileName);
            try
            {
                if (Showbackup.Checked)
                {
                    foreach (string item in System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + @"\log"))
                    {
                        if (item.EndsWith("txt"))
                        {
                            GetFileLog(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void GetFileLog(string fileName)
        {
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(fileName))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    DateTime dt=DateTime.Now.AddYears(-5);
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Length > 10 && !line.Contains("程序开启"))
                        {
                            if (line.StartsWith("***"))
                            {
                                line = DecryptStringlog(line);
                            }
                            if (line.Length>21)
                            {
                                string timeString = line.Substring(0,21);
                                try
                                {
                                    dt = Convert.ToDateTime(timeString.Trim());
                                }
                                catch (Exception)
                                {
                                }
                                result r = new result { Words = line, path = fileName,Time=dt};
                                logs.Add(r);
                            }
                        }
                    }
                }
            }
            
        }
        
        #region 数据加密解密

        private const string initVector = "yixiaoziyixiaozi";
        private const int keysize = 256;
        public static string DecryptStringlog(string cipherText)
        {
            try
            {
                if (logpass == "")
                {
                    return "";
                }
                cipherText = cipherText.Substring(3, cipherText.Length - 6);
                byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(logpass, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged
                {
                    Mode = CipherMode.CBC
                };
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
        yixiaozi.WinForm.Common.AutoSizeForm asc = new AutoSizeForm();
        private void MainPage_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }
        public void SearchLog()
        {
            resultlistBox.Items.Clear();
            foreach (result item in logs.Where(m => m.Words.ToLower().Contains(keyword.Text.ToLower())))
            {
                resultlistBox.Items.Add(item);
            }
            //resultlistBox.DataSource = logs.Where(m => m.words.ToLower().Contains(keyword.Text.ToLower())).ToList();
            //resultlistBox.Sorted = false;
            //resultlistBox.Sorted = true;
            button1.Text = resultlistBox.Items.Count.ToString();
        }
        public class SortByTimeListBox : ListBox
        {
            public SortByTimeListBox() : base()
            {
            }

            // Overrides the parent class Sort to perform a simple
            // bubble sort on the length of the string contained in each item.
            protected override void Sort()
            {
                if (Items.Count > 1)
                {
                    bool swapped;
                    do
                    {
                        int counter = Items.Count - 1;
                        swapped = false;

                        while (counter > 0)
                        {
                            // Compare the items' length. 
                            if (((result)Items[counter]).Time
                                > ((result)Items[counter - 1]).Time)
                            {
                                // Swap the items.
                                object temp = Items[counter];
                                Items[counter] = Items[counter - 1];
                                Items[counter - 1] = temp;
                                swapped = true;
                            }
                            // Decrement the counter.
                            counter -= 1;
                        }
                    }
                    while ((swapped == true));
                }
            }
        }
        public class result
        {
            public string Words { get; set; }
            public string path { get; set; }
            public DateTime Time { get; set; }
        }
        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
        }

        private void keyword_TextChanged(object sender, EventArgs e)
        {
            //if (keyword.Text.Length>4)
            //{
            //    getlog();
            //}
            if (keyword.Text.ToLower().Contains("exit"))
            {
                this.Close();
            }

            if(keyword.Text.ToLower()==("showall"))
            {
                keyword.Text = "";
                DirectoryInfo dictionary = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "logFolder", "")));
                foreach (FileInfo item in dictionary.GetFiles())
                {
                    if (item.Name.EndsWith("txt"))
                    {
                        GetFileLog(item.FullName);
                    }
                }
            }
        }

        private void keyword_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    SearchLog();
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchLog();
        }

        private void resultlistBox_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(((result)resultlistBox.SelectedItem).path);
            }
            catch (Exception)
            {
            }

        }

        private void Showbackup_CheckedChanged(object sender, EventArgs e)
        {
            GetLog();
            logs = logs.OrderByDescending(m => m.Time).ToList();
            SearchLog();
            Center();
        }

        private void Log_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.KeyCode:
                    break;
                case Keys.Modifiers:
                    break;
                case Keys.None:
                    break;
                case Keys.RButton:
                    break;
                case Keys.Cancel:
                    break;
                case Keys.MButton:
                    break;
                case Keys.XButton1:
                    break;
                case Keys.XButton2:
                    break;
                case Keys.Back:
                    break;
                case Keys.Tab:
                    break;
                case Keys.LineFeed:
                    break;
                case Keys.Return:
                    break;
                case Keys.ShiftKey:
                    break;
                case Keys.ControlKey:
                    break;
                case Keys.Menu:
                    break;
                case Keys.Pause:
                    break;
                case Keys.Capital:
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.IMEConvert:
                    break;
                case Keys.IMENonconvert:
                    break;
                case Keys.IMEAccept:
                    break;
                case Keys.IMEModeChange:
                    break;
                case Keys.Space:
                    break;
                case Keys.Prior:
                    break;
                case Keys.End:
                    break;
                case Keys.Home:
                    break;
                case Keys.Left:
                    break;
                case Keys.Up:
                    break;
                case Keys.Right:
                    break;
                case Keys.Down:
                    break;
                case Keys.Select:
                    break;
                case Keys.Print:
                    break;
                case Keys.Execute:
                    break;
                case Keys.Snapshot:
                    break;
                case Keys.Insert:
                    break;
                case Keys.Delete:
                    break;
                case Keys.Help:
                    break;
                case Keys.D0:
                    break;
                case Keys.D1:
                    break;
                case Keys.D2:
                    break;
                case Keys.D3:
                    break;
                case Keys.D4:
                    break;
                case Keys.D5:
                    break;
                case Keys.D6:
                    break;
                case Keys.D7:
                    break;
                case Keys.D8:
                    break;
                case Keys.D9:
                    break;
                case Keys.A:
                    break;
                case Keys.B:
                    break;
                case Keys.C:
                    break;
                case Keys.D:
                    break;
                case Keys.E:
                    break;
                case Keys.F:
                    break;
                case Keys.G:
                    break;
                case Keys.H:
                    break;
                case Keys.I:
                    keyword.Focus();
                    break;
                case Keys.J:
                    break;
                case Keys.K:
                    break;
                case Keys.L:
                    break;
                case Keys.M:
                    break;
                case Keys.N:
                    break;
                case Keys.O:
                    break;
                case Keys.P:
                    break;
                case Keys.Q:
                    this.Close();
                    break;
                case Keys.R:
                    break;
                case Keys.S:
                    break;
                case Keys.T:
                    break;
                case Keys.U:
                    break;
                case Keys.V:
                    break;
                case Keys.W:
                    break;
                case Keys.X:
                    break;
                case Keys.Y:
                    break;
                case Keys.Z:
                    break;
                case Keys.LWin:
                    break;
                case Keys.RWin:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.NumPad0:
                    break;
                case Keys.NumPad1:
                    break;
                case Keys.NumPad2:
                    break;
                case Keys.NumPad3:
                    break;
                case Keys.NumPad4:
                    break;
                case Keys.NumPad5:
                    break;
                case Keys.NumPad6:
                    break;
                case Keys.NumPad7:
                    break;
                case Keys.NumPad8:
                    break;
                case Keys.NumPad9:
                    break;
                case Keys.Multiply:
                    break;
                case Keys.Add:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Divide:
                    break;
                case Keys.F1:
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.NumLock:
                    break;
                case Keys.Scroll:
                    break;
                case Keys.LShiftKey:
                    break;
                case Keys.RShiftKey:
                    break;
                case Keys.LControlKey:
                    break;
                case Keys.RControlKey:
                    break;
                case Keys.LMenu:
                    break;
                case Keys.RMenu:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.Oemplus:
                    break;
                case Keys.Oemcomma:
                    break;
                case Keys.OemMinus:
                    break;
                case Keys.OemPeriod:
                    break;
                case Keys.OemQuestion:
                    break;
                case Keys.Oem8:
                    break;
                case Keys.OemBackslash:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Packet:
                    break;
                case Keys.Attn:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Play:
                    break;
                case Keys.Zoom:
                    break;
                case Keys.NoName:
                    break;
                case Keys.Pa1:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.Shift:
                    break;
                case Keys.Control:
                    break;
                case Keys.Alt:
                    break;
                default:
                    break;
            }
        }

        private void resultlistBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Clipboard.SetText(((result)resultlistBox.SelectedItem).Words);
            }
            catch (Exception)
            {
            }
        }
        #region 添加使用记录

        Guid currentUsedTimerId;
        string usedTimeLog = "";
        private void UseTime_Activated(object sender, EventArgs e)
        {
            UsedLogRenew();
        }

        private void UseTime_Deactivate(object sender, EventArgs e)
        {
            UsedLogRenew(false);
        }
        public void UsedLogRenew(bool newlog = true, bool newid = true)
        {
            //结束之前的ID记录，并生成新的ID
            if (newid || currentUsedTimerId == null)
            {
                DocearReminderForm.usedTimer.SetEndDate(currentUsedTimerId);
                Thread th = new Thread(() => DocearReminderForm.SaveUsedTimerFile(DocearReminderForm.usedTimer)){IsBackground = true};
                currentUsedTimerId = Guid.NewGuid();
            }
            //添加一个新的记录
            if (newlog)
            {
                //主窗口
                //时间块
                //历史记录
                //剪切板
                //工具窗口
                //报表 - 时间块
                //报表 - 键盘
                //报表 - 使用记录
                //所有
                DocearReminderForm.usedTimer.NewOneTime(currentUsedTimerId, "", "", keyword.Text, "历史记录");
            }
        }
        private void SaveUsedTimerFile(UsedTimer data)
        {
            string json = new JavaScriptSerializer()
            {
                MaxJsonLength = Int32.MaxValue
            }.Serialize(data);
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json", "");
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json");
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
        }
        #endregion

        private void resultlistBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Clipboard.SetText(((result)resultlistBox.SelectedItem).Words);
                    break;
                case Keys.ShiftKey:
                    break;
                case Keys.ControlKey:
                    break;
                case Keys.Menu:
                    break;
                case Keys.Pause:
                    break;
                case Keys.Capital:
                    break;
                case Keys.Up:
                    break;
                case Keys.Right:
                    break;
                case Keys.Down:
                    break;
                case Keys.Select:
                    break;
                case Keys.Print:
                    break;
                case Keys.Execute:
                    break;
                case Keys.Snapshot:
                    break;
                case Keys.Help:
                    break;
                case Keys.D0:
                    break;
                case Keys.D1:
                    break;
                case Keys.D2:
                    break;
                case Keys.D3:
                    break;
                case Keys.D4:
                    break;
                case Keys.D5:
                    break;
                case Keys.D6:
                    break;
                case Keys.D7:
                    break;
                case Keys.D8:
                    break;
                case Keys.D9:
                    break;
                case Keys.A:
                    break;
                case Keys.B:
                    break;
                case Keys.C:
                    break;
                case Keys.D:
                    break;
                case Keys.E:
                    break;
                case Keys.F:
                    break;
                case Keys.G:
                    break;
                case Keys.H:
                    break;
                case Keys.I:
                    break;
                case Keys.J:
                    break;
                case Keys.K:
                    break;
                case Keys.L:
                    break;
                case Keys.M:
                    break;
                case Keys.N:
                    break;
                case Keys.O:
                    break;
                case Keys.P:
                    break;
                case Keys.Q:
                    break;
                case Keys.R:
                    break;
                case Keys.S:
                    break;
                case Keys.T:
                    break;
                case Keys.U:
                    break;
                case Keys.V:
                    break;
                case Keys.W:
                    break;
                case Keys.X:
                    break;
                case Keys.Y:
                    break;
                case Keys.Z:
                    break;
                case Keys.LWin:
                    break;
                case Keys.RWin:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.NumPad0:
                    break;
                case Keys.NumPad1:
                    break;
                case Keys.NumPad2:
                    break;
                case Keys.NumPad3:
                    break;
                case Keys.NumPad4:
                    break;
                case Keys.NumPad5:
                    break;
                case Keys.NumPad6:
                    break;
                case Keys.NumPad7:
                    break;
                case Keys.NumPad8:
                    break;
                case Keys.NumPad9:
                    break;
                case Keys.Multiply:
                    break;
                case Keys.Add:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Divide:
                    break;
                case Keys.F1:
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.NumLock:
                    break;
                case Keys.Scroll:
                    break;
                case Keys.LShiftKey:
                    break;
                case Keys.RShiftKey:
                    break;
                case Keys.LControlKey:
                    break;
                case Keys.RControlKey:
                    break;
                case Keys.LMenu:
                    break;
                case Keys.RMenu:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Packet:
                    break;
                case Keys.Attn:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Play:
                    break;
                case Keys.Zoom:
                    break;
                case Keys.NoName:
                    break;
                case Keys.Pa1:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.Shift:
                    break;
                case Keys.Control:
                    break;
                case Keys.Alt:
                    break;
                default:
                    break;
            }
        }
    }
}

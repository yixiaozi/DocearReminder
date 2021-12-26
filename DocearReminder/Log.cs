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
using System.Threading.Tasks;
using System.Windows.Forms;
using yixiaozi.Config;
using yixiaozi.WinForm.Common;

namespace DocearReminder
{
    public partial class Log : Form
    {
        List<result> logs = new List<result>();
        static string logpass = "niqishihenhao";
        private IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"config.ini");
        public Log()
        {
            InitializeComponent();
            logpass = ini.ReadString("password", "abc", "");
            resultlistBox.Items.Add(new result { words = "", path = "" });
            string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            resultlistBox.Items.Clear();
            getalllog(fileName);
            try
            {
                foreach (string item in System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory+@"\log"))
                {
                    if (item.EndsWith("txt"))
                    {
                        getalllog(item);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            logs = logs.OrderByDescending(m=>m.Time).ToList();
            resultlistBox.DisplayMember = "words";
            resultlistBox.ValueMember = "path";
            getlog();
            Center();
            keyword.Focus();
            getlog();
        }
        public void getalllog(string fileName)
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
                                result r = new result { words = line, path = fileName,Time=dt};
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
        public void getlog()
        {
            resultlistBox.Items.Clear();
            foreach (result item in logs.Where(m => m.words.ToLower().Contains(keyword.Text.ToLower())))
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
            public string words { get; set; }
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
                DirectoryInfo dictionary = new DirectoryInfo(ini.ReadString("path", "logFolder", ""));
                foreach (FileInfo item in dictionary.GetFiles())
                {
                    if (item.Name.EndsWith("txt"))
                    {
                        getalllog(item.FullName);
                    }
                }
            }
        }

        private void keyword_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    getlog();
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            getlog();
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
    }
}

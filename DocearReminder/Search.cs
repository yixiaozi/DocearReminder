using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;
using yixiaozi.WinForm.Common;

namespace DocearReminder
{
    public partial class Search : Form
    {
        public static string path = System.AppDomain.CurrentDomain.BaseDirectory;
        List<result> allresult = new List<result>();
        public Search()
        {
            InitializeComponent();
            resultlistBox.DisplayMember = "words";
            resultlistBox.ValueMember = "path";
            Center();
            DirSearch(path + @"\" + DateTime.Now.Year.ToString());
            for (int i = 1; i < 50; i++)
            {
                DirSearch(path + @"\" + (DateTime.Now.Year - i).ToString());
            }
            allresult = allresult.OrderByDescending(m => m.Time).ToList();
            getlog();
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            resultlistBox.Items.Clear();
            getlog();
        }
        void DirSearch(string sDir)
        {
            try
            {
                if (!Directory.Exists(sDir))
                {
                    return;
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string fileName in Directory.GetFiles(d, "*.txt"))
                    {
                        if (fileName.Contains("File")|| fileName.Contains("key"))
                        {
                            continue;
                        }
                        const Int32 BufferSize = 128;
                        using (var fileStream = File.OpenRead(fileName))
                        {
                            string time = "";
                            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                            {
                                String line;
                                while ((line = streamReader.ReadLine()) != null)
                                {
                                    if (line.Trim()=="")
                                    {
                                        continue;
                                    }
                                    try
                                    {
                                        if (line.StartsWith("20"))
                                        {
                                            time = line.Substring(0, 20);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    try
                                    {
                                        DateTime dt = Convert.ToDateTime(time.Trim());
                                        result r = new result { words = (!line.StartsWith("20") ? time : "") + line, path = fileName, Time = dt };
                                        allresult.Add(r);
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            DateTime dt = Convert.ToDateTime(time.Substring(0,18).Trim());
                                            result r = new result { words = (!line.StartsWith("20") ? time : "") + line, path = fileName, Time = dt };
                                            allresult.Add(r);
                                        }
                                        catch (Exception)
                                        {
                                            result r = new result { words = (!line.StartsWith("20") ? time : "") + line, path = fileName, Time = new DateTime() };
                                            allresult.Add(r);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception ex)
            {
                resultlistBox.Items.Add(ex.ToString());
            }
        }
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
            string word = keyword.Text;
            resultlistBox.Items.AddRange(allresult.Where(m => m.words.Contains(word)).Take(50000).ToArray());
            //foreach (result item in allresult.Where(m => m.words.Contains(word)).Take(50000))
            //{
            //    resultlistBox.Items.Add(item);
            //}
        }
        public class result{
            public string words{get;set;}
            public string path{get;set;}
            public DateTime Time { get; set; }
        }

        private void resultlistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start(((result)resultlistBox.SelectedItem).path);
        }

        private void keyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                this.searchbutton_Click(sender, e);//触发button事件  
            }  
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

        }

        private void resultlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                DocearReminderForm.usedTimer.NewOneTime(currentUsedTimerId, "", "", keyword.Text, "剪切板");
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
    }
}

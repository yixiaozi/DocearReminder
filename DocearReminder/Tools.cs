using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocearReminder;
using System.Web.Script.Serialization;
using NPinyin;
using System.Xml;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using yixiaozi.Config;
using yixiaozi.Model.DocearReminder;
using yixiaozi.Security;
using yixiaozi.Net.HttpHelp;
using yixiaozi.MyConvert;

namespace DocearReminder
{
    public partial class Tools : Form
    {
        public Tools()
        {
            InitializeComponent();
            checkBox1.Checked=DocearReminderForm.isZhuangbi ;
            Center();
        }
        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
        }

        private void reducejson_Click(object sender, EventArgs e)
        {
            Reminder reminderObject = new Reminder();
            Reminder reminderObjectBACKUP = new Reminder();
            FileInfo fi = new FileInfo(@"reminder.json");
            IniFile ini = new IniFile(@"./config.ini");
            DirectoryInfo path = new DirectoryInfo(ini.ReadString("path", "rootpath", "")); //System.AppDomain.CurrentDomain.BaseDirectory);
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                reminderObject = serializer.Deserialize<Reminder>(s);
                reminderObjectBACKUP.reminders.AddRange(reminderObject.reminders.Where(m => m.isCompleted));
                for (int i = reminderObject.reminders.Count()-1; i >=0 ; i--)
                {
                    if (reminderObject.reminders[i].isCompleted)
                    {
                        reminderObject.reminders.RemoveAt(i);
                    }
                }
            }
            SaveJson(reminderObject, @"reminder.json");
            SaveJson(reminderObjectBACKUP, path + @".\reminderjson\" + DateTime.Now.ToString("yyyyMMddMMss") + ".json");
            this.Close();
        }
        public void SaveJson(Reminder jsonObject, string path) {
            if (!System.IO.File.Exists(path))
            {
                File.Create(path).Dispose() ;
            }
            else
            {
                File.WriteAllText(path, "");
            }
            FileInfo fi = new FileInfo(path);
            string json = new JavaScriptSerializer().Serialize(jsonObject);
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
        }

        private void deletetemp_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(@"./config.ini");
            DirectoryInfo path = new DirectoryInfo(ini.ReadString("path", "rootpath", "")); //System.AppDomain.CurrentDomain.BaseDirectory);
            int deleteCount = 0;
            foreach (FileInfo file in path.GetFiles("~*.mm", SearchOption.AllDirectories))
            {
                file.Delete();
                deleteCount++;
            }
            if (deleteCount>0)
            {
                MessageBox.Show("delete temp file count:"+deleteCount.ToString());
            }
            foreach (FileInfo file in path.GetFiles("*.MM", SearchOption.AllDirectories))
            {
                System.IO.File.Move(file.FullName, file.FullName.Substring(0, file.FullName.Length - 2) + "mm");
            }
            this.Close();
        }

        private void setPWD_Click(object sender, EventArgs e)
        {
            DocearReminderForm.PassWord=pwd.Text;
            pwd.Text = "";
            this.Close();
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            Encrypt encrypt = new Encrypt(DocearReminderForm.PassWord);
            pwd.Text= encrypt.DecryptString(pwd.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                OpenFileDialog fileDialog = openFileDialog;
                fileDialog.Multiselect = false;
                fileDialog.Title = "请选择需要格式化的思维导图";
                fileDialog.Filter = "思维导图(*mm*)|*.mm*"; //设置要选择的文件的类型
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = fileDialog.FileName;//返回文件的完整路径         
                    yixiaozi.Model.DocearReminder.Helper.ConvertFile(file);
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void createsuggest_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(@"./config.ini");
            DirectoryInfo path = new DirectoryInfo(ini.ReadString("path", "rootpath", ""));
            string content = "";
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(file.FullName);
                content += filename;
                content += "|";
                content += GetFirstSpell(filename);
                content += "|";
                content += ConvertToAllSpell(filename);
                content += "|";
                content += GetFirstSpell(filename);
                content += "@";
            }
            try//获取外部文件夹
            {
                string calanderpath = ini.ReadString("path", "calanderpath", "");
                foreach (string item in calanderpath.Split(';'))
                {
                    if (item != "")
                    {
                        if (ini.ReadString("path", item, "") != "")
                        {
                            if (!ini.ReadString("path", item, "").Contains(ini.ReadString("path", "rootpath", "")))
                            {
                                DirectoryInfo pathout = new DirectoryInfo(ini.ReadString("path", item, ""));
                                foreach (FileInfo file in pathout.GetFiles("*.mm", SearchOption.AllDirectories))
                                {
                                    string filename = Path.GetFileNameWithoutExtension(file.FullName);
                                    content += filename;
                                    content += "|";
                                    content += GetFirstSpell(filename);
                                    content += "|";
                                    content += ConvertToAllSpell(filename);
                                    content += "|";
                                    content += GetFirstSpell(filename);
                                    content += "@";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            RecordLog(content);
            StationInfo.StationData = null;
            this.Close();
        }
        public static void createsuggest_fun()
        {
            IniFile ini = new IniFile(@"./config.ini");
            DirectoryInfo path = new DirectoryInfo(ini.ReadString("path", "rootpath", "")); 
            string content = "";
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(file.FullName);
                content += filename;
                content += "|";
                content += GetFirstSpell(filename);
                content += "|";
                content += ConvertToAllSpell(filename);
                content += "|";
                content += GetFirstSpell(filename);
                content += "@";
            }
            try//获取外部文件夹
            {
                string calanderpath = ini.ReadString("path", "calanderpath", "");
                foreach (string item in calanderpath.Split(';'))
                {
                    if (item != "")
                    {
                        if (ini.ReadString("path", item, "")!="")
                        {
                            if (!ini.ReadString("path", item, "").Contains(ini.ReadString("path", "rootpath", "")))
                            {
                                DirectoryInfo pathout = new DirectoryInfo(ini.ReadString("path", item, "")); 
                                foreach (FileInfo file in pathout.GetFiles("*.mm", SearchOption.AllDirectories))
                                {
                                    string filename = Path.GetFileNameWithoutExtension(file.FullName);
                                    content += filename;
                                    content += "|";
                                    content += GetFirstSpell(filename);
                                    content += "|";
                                    content += ConvertToAllSpell(filename);
                                    content += "|";
                                    content += GetFirstSpell(filename);
                                    content += "@";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            RecordLog(content);
            StationInfo.StationData = null;
        }
        
        public static void RecordLog(string Content)
        {
            string logSite = AppDomain.CurrentDomain.BaseDirectory + "mindmaps.txt";//本地文件
            StreamWriter sw = new StreamWriter(logSite, false, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(Content);
            sw.Close();
            sw.Dispose();
        }
        private static Encoding gb2312 = Encoding.GetEncoding("GB2312");

        /// <summary>
        /// 汉字转全拼
        /// </summary>
        public static string ConvertToAllSpell(string strChinese, IDictionary<char, string> pinyinDic = null)
        {
            try
            {
                if (strChinese.Length != 0)
                {
                    var fullSpell = new StringBuilder();
                    for (var i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        var pinyin = GetSpell(chr);
                        fullSpell.Append(pinyin);
                    }

                    return fullSpell.ToString().ToLower();
                }
            }
            catch (Exception e)
            {
            }
            return string.Empty;
        }
        /// <summary>
        /// 汉字转首字母
        /// </summary>
        /// <param name="strChinese"></param>
        /// <returns></returns>
        public static string GetFirstSpell(string strChinese)
        {
            //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别
            //return NPinyin.Pinyin.GetInitials(strChinese);

            try
            {
                if (strChinese.Length != 0)
                {
                    var fullSpell = new StringBuilder();
                    for (var i = 0; i < strChinese.Length; i++)
                    {
                        var chr = strChinese[i];
                        fullSpell.Append(GetSpell(chr)[0]);
                    }

                    return fullSpell.ToString().ToLower();
                }
            }
            catch (Exception e)
            {
            }

            return string.Empty;
        }
        private static string GetSpell(char chr)
        {
            var coverchr = NPinyin.Pinyin.GetPinyin(chr);
            return coverchr;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DocearReminderForm.isZhuangbi = checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(@"./config.ini");
            DirectoryInfo path = new DirectoryInfo(ini.ReadString("path", "rootpath", "")); //System.AppDomain.CurrentDomain.BaseDirectory);
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                try
                {
                    System.Xml.XmlDocument x = new XmlDocument();
                    x.Load(file.FullName);
                    bool isNeedUpdate = false;
                    foreach (XmlNode node in x.GetElementsByTagName("node"))
                    {
                        if (node.Attributes != null && node.Attributes["ID"] == null)
                        {
                            isNeedUpdate = true;
                            XmlAttribute TASKID = x.CreateAttribute("ID");
                            node.Attributes.Append(TASKID);
                            node.Attributes["ID"].Value = Guid.NewGuid().ToString();
                        }
                    }
                    if (isNeedUpdate)
                    {
                        x.Save(file.FullName);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(file.FullName));
                        th.Start();
                    }
                }
                catch (Exception)
                {
                }
            }
            this.Close();
        }

        private void encry_txt_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Title = "请选择需要加密的日志",
                    Filter = "日志文件(*txt*)|*.txt*" //设置要选择的文件的类型
                };
                Encrypt encrypt = new Encrypt(DocearReminderForm.PassWord);
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = fileDialog.FileName;//返回文件的完整路径         
                    const Int32 BufferSize = 128;
                    using (System.IO.StreamWriter file1 = new System.IO.StreamWriter(file.Replace(".txt", "1.txt"), true))
                    {
                        using (var fileStream = File.OpenRead(file))
                        {
                            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                            {
                                String line;
                                while ((line = streamReader.ReadLine()) != null)
                                {
                                    if (line.Length > 10 && !line.Contains("程序开启"))
                                    {
                                        line = encrypt.EncryptString(line);

                                        if (line != "")
                                        {
                                            file1.WriteLine(line);
                                        }
                                    }
                                }
                            }
                        }
                    };
                }
            }
            catch (Exception)
            {
            }
        }

        private void Tools_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Title = "请选择需要格式化的思维导图",
                    Filter = "思维导图(*mm*)|*.mm*" //设置要选择的文件的类型
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string file = fileDialog.FileName;
                    System.Xml.XmlDocument x = new XmlDocument();
                    x.Load(file);
                    foreach (XmlNode node in x.GetElementsByTagName("node"))
                    {
                        try
                        {
                            if (node.Attributes != null && node.Attributes["TEXT"] != null && isURL(node.Attributes["TEXT"].Value) && node.Attributes["LINK"] == null)
                            {
                                XmlAttribute LINK = x.CreateAttribute("LINK");
                                LINK.Value = node.Attributes["TEXT"].Value;
                                node.Attributes["TEXT"].Value = yixiaozi.Net.HttpHelp.Web.getTitle(node.Attributes["TEXT"].Value);
                                node.Attributes.Append(LINK);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    x.Save(file);
                    yixiaozi.Model.DocearReminder.Helper.ConvertFile(file);
                }
            }
            catch (Exception)
            {
            }
            
        }
        public bool isURL(string url)
        {
            string matchStr = @"http(s)?://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]";
            return Regex.IsMatch(url, matchStr);
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            return;
            //IniFile ini = new IniFile(@"./config.ini");
            //DirectoryInfo path = new DirectoryInfo(ini.ReadString("path", "rootpath", "")); //System.AppDomain.CurrentDomain.BaseDirectory);
            //foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            //{
            //    try
            //    {
            //        System.Xml.XmlDocument x = new XmlDocument();
            //        x.Load(file.FullName);
            //        bool isNeedUpdate = false;
            //        foreach (XmlNode node in x.GetElementsByTagName("node"))
            //        {
            //            try
            //            {
            //                if (node.Attributes != null && node.Attributes["TEXT"] != null && isURL(node.Attributes["TEXT"].Value) && node.Attributes["LINK"] == null)
            //                {
            //                    XmlAttribute LINK = x.CreateAttribute("LINK");
            //                    LINK.Value = node.Attributes["TEXT"].Value;
            //                    node.Attributes["TEXT"].Value = yixiaozi.Net.HttpHelp.Web.getTitle(node.Attributes["TEXT"].Value);
            //                    node.Attributes.Append(LINK);
            //                    isNeedUpdate=true;
            //                }
            //            }
            //            catch (Exception)
            //            {
            //            }
            //        }
            //        if (isNeedUpdate)
            //        {
            //            x.Save(file.FullName);
            //            yixiaozi.Model.DocearReminder.Helper.ConvertFile(file.FullName);
            //            //Thread th = new Thread(() => DocearReminderForm.ConvertFile(file.FullName));
            //            //th.Start();
            //        }
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            //this.Close();
        }
        //所有links建议文件
        private void button5_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(@"./config.ini");
            DirectoryInfo path = new DirectoryInfo(ini.ReadString("path", "rootpath", "")); //System.AppDomain.CurrentDomain.BaseDirectory);

            List<string> usedSuggest = new List<string>();
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                if (file.FullName.Contains("Work"))
                {
                    continue;
                }
                try
                {
                    System.Xml.XmlDocument x = new XmlDocument();
                    x.Load(file.FullName);
                    foreach (XmlNode node in x.GetElementsByTagName("node"))
                    {
                        try
                        {
                            if (node.Attributes != null && node.Attributes["TEXT"] != null && node.Attributes["LINK"] != null && isURL(node.Attributes["LINK"].Value))
                            {
                                usedSuggest.Add(new HtmlToString().StripHTML(node.Attributes["TEXT"].Value).Replace("\r","").Replace("\n", "").Replace("\t", "").Replace("|", "") + "|"+ new HtmlToString().StripHTML(node.Attributes["LINK"].Value));
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            new TextListConverter().WriteListToTextFile(usedSuggest, System.AppDomain.CurrentDomain.BaseDirectory + @"\alllinks.txt");
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //显示颜色对话框
            DialogResult dr = colorDialog1.ShowDialog();
            //如果选中颜色，单击“确定”按钮则改变文本框的文本颜色
            if (dr == DialogResult.OK)
            {
                DocearReminderForm.BackGroundColor= colorDialog1.Color;
            }
        }
    }
}

using DocearReminder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using yixiaozi.Config;
using yixiaozi.Model.DocearReminder;
using yixiaozi.WinForm.Control.Calendar;
using static DocearReminder.DocearReminderForm;
using yixiaozi.WinForm.Common;
using yixiaozi.Windows;
using yixiaozi.Security;

namespace DocearReminder
{
    public partial class CalendarForm : Form
    {
        List<Appointment> m_Appointments;
        string mindmappath = "";
        string[] noFolder = new string[] { };
        string CalendarImagePath = "";
        string logfile = "reminder.json";
        List<string> workfolders = new List<string>();
        private IniFile ini = new IniFile(@"./config.ini");
        bool ismovetask = false;
        [DllImport("User32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        Encrypt encryptlog;
        string lastId = "";
        string lastName = "";
        List<Color> timeblockColors = new List<Color>();
        //private Sunisoft.IrisSkin.SkinEngine skinEngine1;

        public CalendarForm(string path,bool OpenNew=false)// 后期希望只显示当期文件夹的日历
        {
            //if (!OpenNew)
            //{
            //    Center();
            //    this.Show();
            //    this.Activate();
            //}

            mindmappath = path;
            string logpass = ini.ReadString("password", "i", "");
            encryptlog = new Encrypt(logpass);
            InitializeComponent();
            //if (ini.ReadString("Skin", "src", "") != "")
            //{
            //    skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            //    skinEngine1.SkinFile = ini.ReadString("Skin", "src", "");
            //}
            //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            new MyProcess().OnlyOneForm("Calendar.exe");
            string calanderpath = ini.ReadString("path", "calanderpath", "");
            workfolders.Add(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
            foreach (string item in calanderpath.Split(';'))
            {
                workfolder_combox.Items.Add(item);
                workfolders.Add(System.IO.Path.GetFullPath(ini.ReadString("path", item, "")));
            }
            workfolder_combox.Items.Add("All");
            workfolder_combox.SelectedIndex = hasinworkfolderIndex(mindmappath);
            string no = ini.ReadString("path", "no", "");
            noFolder = no.Split(';');
            CalendarImagePath = System.IO.Path.GetFullPath((ini.ReadStringDefault("path", "CalendarImagePath", "")));
            dayView1.Renderer = new Office11Renderer();
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
            RefreshCalender();
            dayView1.AllowScroll = true;
            DateTime startWeek = DateTime.Today;
            if (startWeek.DayOfWeek.ToString("d") != "0")
            {
                startWeek = startWeek.AddDays(1 - Convert.ToInt32(startWeek.DayOfWeek.ToString("d")));
            }
            else
            {
                startWeek = startWeek.AddDays(-6);
            }
            try
            {
                this.Opacity = Convert.ToDouble(ini.ReadString("appearance", "CalanderOpacity", "1"));
                numericOpacity.Value = Convert.ToInt32(this.Opacity * 100);

            }
            catch (Exception)
            {
            }
            dateTimePicker1.Value = startWeek;
            dayView1.StartDate = startWeek;
            dayView1.NewAppointment += new NewAppointmentEventHandler(dayView1_NewAppointment);
            dayView1.SelectionChanged += new EventHandler(dayView1_SelectionChanged);
            //dayView1.ResolveAppointments += new ResolveAppointmentsEventHandler(this.dayView1_ResolveAppointments);
            dayView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dayView1_MouseMove);
            timer1.Interval = 60000;
            timer1.Start();
            try
            {
                if (Environment.OSVersion.Version.Major < 6)
                {
                    base.SendToBack();

                    IntPtr hWndNewParent = User32.FindWindow("Progman", null);
                    User32.SetParent(base.Handle, hWndNewParent);
                }
                else
                {
                    User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
                }
            }
            catch (ApplicationException exx)
            {
                MessageBox.Show(this, exx.Message, "Pin to Desktop");
            }
            Center();
            dayView1.MouseWheel += CalendarWhell;
            IntPtr hWndMyWindow = FindWindow(null, this.Name);//通过窗口的标题获得句柄
            IntPtr hWndDesktop = FindWindow("Progman", "Program Manager");//获得桌面句柄
            SetParent(hWndMyWindow, hWndDesktop); //将窗口设置为桌面的子窗体
            dayView1.AllowInplaceEditing = false;
            dayView1.AllowNew = false;
            #region 添加菜单
            try
            {
                System.Xml.XmlDocument timeblockmm = new XmlDocument();
                timeblockmm.Load(ini.ReadString("TimeBlock", "mindmap", ""));
                foreach (XmlNode node in timeblockmm.GetElementsByTagName("node"))
                {
                    if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "事件类别")
                    {
                        SearchNode(node, null);
                    }else if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "进步")
                    {
                        System.Windows.Forms.ToolStripItem newMenu = this.Menu.Items.Add("进步", global::DocearReminder.Properties.Resources.square_ok, SetProgress);
                        newMenu.BackColor = Color.Red;
                        SearchNode_progress(node, newMenu);
                    }
                    else if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "错误")
                    {
                        System.Windows.Forms.ToolStripItem newMenu = this.Menu.Items.Add("错误", global::DocearReminder.Properties.Resources.square_ok, SetMistake);
                        newMenu.BackColor = Color.Gray;
                        SearchNode_mistake(node, newMenu);
                    }
                }
                if (timeblockColors.Count>0)
                {
                    System.Windows.Forms.ToolStripItem newMenu = this.Menu.Items.Add("改变颜色", global::DocearReminder.Properties.Resources.square_ok, SetTimeBlockColor);
                    newMenu.BackColor = Color.White;
                    System.Windows.Forms.ToolStripItem newMenunew;
                    foreach (Color item in timeblockColors)
                    {
                        newMenunew = ((ToolStripMenuItem)newMenu).DropDownItems.Add("", global::DocearReminder.Properties.Resources.square_ok, SetTimeBlockColor);
                        newMenunew.BackColor = item;
                    }
                }
            }
            catch (Exception)
            {
            }
            #endregion
        }

        public void SearchNode(XmlNode node, ToolStripItem menuitem)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode Subnode in node.ChildNodes)
                {
                    if (Subnode.Attributes["TEXT"] != null)
                    {
                        System.Windows.Forms.ToolStripItem newMenu;
                        if (menuitem == null)
                        {
                            newMenu = this.Menu.Items.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetTimeBlock);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        else
                        {
                            newMenu = ((ToolStripMenuItem)menuitem).DropDownItems.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetTimeBlock);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        if (!timeblockColors.Contains(newMenu.BackColor))
                        {
                            timeblockColors.Add(newMenu.BackColor);
                        }
                        SearchNode(Subnode, newMenu);
                    }
                }
            }
        }
        public void SearchNode_progress(XmlNode node, ToolStripItem menuitem)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode Subnode in node.ChildNodes)
                {
                    if (Subnode.Attributes["TEXT"] != null)
                    {
                        System.Windows.Forms.ToolStripItem newMenu;
                        if (menuitem == null)
                        {
                            newMenu = this.Menu.Items.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetProgress);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        else
                        {
                            newMenu = ((ToolStripMenuItem)menuitem).DropDownItems.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetProgress);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        SearchNode_progress(Subnode, newMenu);
                    }
                }
            }
        }
        public void SearchNode_mistake(XmlNode node, ToolStripItem menuitem)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode Subnode in node.ChildNodes)
                {
                    if (Subnode.Attributes["TEXT"] != null)
                    {
                        System.Windows.Forms.ToolStripItem newMenu;
                        if (menuitem == null)
                        {
                            newMenu = this.Menu.Items.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetMistake);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        else
                        {
                            newMenu = ((ToolStripMenuItem)menuitem).DropDownItems.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetMistake);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        SearchNode_mistake(Subnode, newMenu);
                    }
                }
            }
        }
        public string GetFatherNodeName(XmlNode node)
        {
            try
            {
                string s = "";
                while (node.ParentNode != null)
                {
                    try
                    {
                        //去掉根节点
                        if (node.ParentNode.ParentNode.Name == "map"|| node.ParentNode.Attributes["TEXT"].Value== "事件类别")
                        {
                            break;
                        }
                        s = node.ParentNode.Attributes["TEXT"].Value + (s != "" ? "|" : "") + s;
                        node = node.ParentNode;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                return s;
            }
            catch (Exception)
            {
                return "";
            }
        }

        yixiaozi.WinForm.Common.AutoSizeForm asc = new AutoSizeForm();
        private void MainPage_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            //this.Height = this.MaximumSize.Height;
            //this.Width = this.MaximumSize.Width;
            this.WindowState = FormWindowState.Maximized;
            asc.controlAutoSize(this);
            Center();
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
        }
        public void reminderObjectJsonAdd(string TaskName, string ID, string mindmap, double tasktime, DateTime taskTime, string mindmapName,object tag=null,string comment="")
        {
            try
            {
                Reminder reminderObject = new Reminder();
                FileInfo fi = new FileInfo(logfile);
                using (StreamReader sw = fi.OpenText())
                {
                    string s = sw.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    reminderObject = serializer.Deserialize<Reminder>(s);
                    reminderObject.reminders.Add(new ReminderItem
                    {
                        name = TaskName,
                        time = taskTime,
                        mindmapPath = mindmap,
                        mindmap = mindmapName,
                        ID = ID,
                        tasktime = tasktime,
                        nameFull=tag!=null?tag.ToString():"",
                        comment=comment
                    });
                }
                string json = new JavaScriptSerializer().Serialize(reminderObject);
                File.WriteAllText(logfile, "");
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.Write(json);
                }
            }
            catch (Exception)
            {
            }

        }
        public void reminderObjectJsonTimeBlockColor(string ID,Color color)
        {
            try
            {
                Reminder reminderObject = new Reminder();
                FileInfo fi = new FileInfo(logfile);
                using (StreamReader sw = fi.OpenText())
                {
                    string s = sw.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    reminderObject = serializer.Deserialize<Reminder>(s);
                    ReminderItem item= reminderObject.reminders.FirstOrDefault(m=>m.ID==ID);
                    if (item!=null)
                    {
                        item.mindmapPath = color.ToArgb().ToString();
                    }
                }
                string json = new JavaScriptSerializer().Serialize(reminderObject);
                File.WriteAllText(logfile, "");
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.Write(json);
                }
            }
            catch (Exception)
            {
            }

        }
        void dayView1_NewAppointment(object sender, NewAppointmentEventArgs args)
        {
            Appointment m_Appointment = new Appointment
            {
                StartDate = args.StartDate,
                EndDate = args.EndDate,
                Title = args.Title,
                value = ini.ReadStringDefault("path", "CalendarmapPath", ""),//mindmappath + @"\calander.mm",
                ID = Guid.NewGuid().ToString()
            };
            m_Appointments.Add(m_Appointment);
            try
            {
                reminderObjectJsonAdd(args.Title, m_Appointment.ID, m_Appointment.value, (args.EndDate - args.StartDate).TotalMinutes, args.StartDate, "calander");
                string path = ini.ReadStringDefault("path", "CalendarmapPath", "");//mindmappath + @"\calander.mm";
                if (!System.IO.File.Exists(path))
                {
                    return;
                    //System.IO.File.Copy(System.AppDomain.CurrentDomain.BaseDirectory + @"\calander.mm", path);
                }
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(path);
                XmlNode root = x.GetElementsByTagName("node")[0];
                if (!HaschildNode(root, args.StartDate.Year.ToString()))
                {
                    XmlNode yearNode = x.CreateElement("node");
                    XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                    yearNodeValue.Value = args.StartDate.Year.ToString();
                    yearNode.Attributes.Append(yearNodeValue);
                    root.AppendChild(yearNode);
                }
                XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == args.StartDate.Year.ToString());
                if (!HaschildNode(year, args.StartDate.Month.ToString()))
                {
                    XmlNode monthNode = x.CreateElement("node");
                    XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                    monthNodeValue.Value = args.StartDate.Month.ToString();
                    monthNode.Attributes.Append(monthNodeValue);
                    year.AppendChild(monthNode);
                }
                XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == args.StartDate.Month.ToString());
                if (!HaschildNode(month, args.StartDate.Day.ToString()))
                {
                    XmlNode dayNode = x.CreateElement("node");
                    XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                    dayNodeValue.Value = args.StartDate.Day.ToString();
                    dayNode.Attributes.Append(dayNodeValue);
                    month.AppendChild(dayNode);
                }
                XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == args.StartDate.Day.ToString());
                XmlNode newNote = x.CreateElement("node");
                string changedtaskname = args.Title;
                XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                SaveLog("日历添加任务：" + changedtaskname + "    导图：" + ini.ReadStringDefault("path", "CalendarmapPath", ""));// mindmappath + @"\calander.mm");
                newNotetext.Value = changedtaskname;
                XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                newNoteCREATED.Value = (Convert.ToInt64((args.StartDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                newNoteMODIFIED.Value = (Convert.ToInt64((args.StartDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                newNote.Attributes.Append(newNotetext);
                newNote.Attributes.Append(newNoteCREATED);
                newNote.Attributes.Append(newNoteMODIFIED);
                XmlAttribute TASKID = x.CreateAttribute("ID");
                newNote.Attributes.Append(TASKID);
                newNote.Attributes["ID"].Value = m_Appointment.ID;
                XmlAttribute TASKLEVEL = x.CreateAttribute("TASKLEVEL");
                newNote.Attributes.Append(TASKLEVEL);
                newNote.Attributes["TASKLEVEL"].Value = "1";
                XmlAttribute TASKTIME = x.CreateAttribute("TASKTIME");
                newNote.Attributes.Append(TASKTIME);
                newNote.Attributes["TASKTIME"].Value = (args.EndDate - args.StartDate).TotalMinutes.ToString("N0");
                XmlNode remindernode = x.CreateElement("hook");
                XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                remindernode.Attributes.Append(remindernodeName);
                XmlNode remindernodeParameters = x.CreateElement("Parameters");
                XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                remindernodeTime.Value = (Convert.ToInt64((args.StartDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                remindernodeParameters.Attributes.Append(remindernodeTime);
                remindernode.AppendChild(remindernodeParameters);
                newNote.AppendChild(remindernode);
                day.AppendChild(newNote);
                x.Save(path);
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void SaveLog(string log)
        {
            log = log.Replace("\r", " ").Replace("\n", " ");
            log = (DateTime.Now + "    " + log);
            log = encryptlog.EncryptString(log);
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\log.txt", true))
            {
                if (log != "")
                {
                    //file.Write(DateTime.Now + "        ");
                    file.WriteLine(log);
                    //file.Write("\r");
                }
            }
        }

        private void dayView1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void dayView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                lastId = dayView1.SelectedAppointment.ID;
                lastName = dayView1.SelectedAppointment.Title;
            }
            //测试是否变化
            foreach (Appointment m_App in m_Appointments)
            {
                if (m_App.ID == lastId && m_App.Title != lastName&& !m_App.Title.Contains("("))
                {
                    Edit(true, m_App);
                    lastId = m_App.ID;
                    lastName = m_App.Title;
                }

            }
        }
        public bool HaschildNode(XmlNode node, string child)
        {
            foreach (XmlNode item in node.ChildNodes.Cast<XmlNode>().Where(m => m.Name == "node"))
            {
                if (item.Attributes.Cast<XmlAttribute>().Any(m => m.Name == "TEXT"))
                {
                    if (item.Attributes["TEXT"].Value == child)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void dayView1_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            List<Appointment> m_Apps = new List<Appointment>();
            foreach (Appointment m_App in m_Appointments)
            {
                if ((m_App.StartDate >= args.StartDate) &&
                    (m_App.StartDate <= args.EndDate))
                {
                    m_Apps.Add(m_App);
                }
            }

            args.Appointments = m_Apps;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public class comboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
        public bool hasinworkfolder(string path)
        {
            int i = 0;//跳过第一个，因为第一个是根目录
            foreach (var item in workfolders)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }
                else
                {
                    if (path.Contains(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public int hasinworkfolderIndex(string path)
        {
            int i = 0;
            foreach (var item in workfolders)
            {
                if (path == item)
                {
                    return i;
                }
                else
                {
                    i++;
                }
            }
            return i;
        }
        #region MyRegion
        private void button6_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 3;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 5;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 17;
        }
        #endregion



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            dayView1.StartDate = dateTimePicker1.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dayView1.DaysToShow = (int)numericUpDown1.Value;
        }



        private void CalendarForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dayView1.StartDate = dateTimePicker1.Value;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefreshCalender();
        }
        public void RefreshCalender()
        {
            m_Appointments = new List<Appointment>();
            DateTime m_Date = DateTime.Now;

            Appointment m_Appointment = new Appointment();
            m_Date = m_Date.AddHours(10 - m_Date.Hour);
            m_Date = m_Date.AddMinutes(-m_Date.Minute);

            Reminder reminderObject = new Reminder();
            FileInfo fi = new FileInfo(logfile);
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                reminderObject = serializer.Deserialize<Reminder>(s);
                if (reminderObject.reminders == null || reminderObject.reminders.Count == 0)
                {
                    return;
                }
                IEnumerable<ReminderItem> items = reminderObject.reminders.Where(m => ((!m.isCompleted && !m.isview && !m.isEBType && m.mindmapPath.Contains(mindmappath)) &&m.mindmap != "TimeBlock" && m.mindmap != "FanQie" && m.mindmap != "Progress" && m.mindmap != "Mistake" && !c_timeBlock.Checked && !c_fanqie.Checked && !c_done.Checked && !c_progress.Checked && !c_mistake.Checked) || (c_timeBlock.Checked && m.mindmap == "TimeBlock") || (c_done.Checked && m.isCompleted) || (c_fanqie.Checked && m.mindmap == "FanQie"&&!m.isCompleted)|| (c_progress.Checked && m.mindmap == "Progress")|| (c_mistake.Checked && m.mindmap == "Mistake"));
                if (workfolder_combox.SelectedItem != null && workfolder_combox.SelectedItem.ToString() == "RootPath")
                {
                    items = items.Where(m => !hasinworkfolder(m.mindmapPath));
                }
                foreach (ReminderItem item in items)//这里还有问题,先不折腾逻辑了
                {
                    try//过滤字符串
                    {
                        if (!(item.name.Contains(textBox_searchwork.Text))&&!(item.mindmapPath.Contains(textBox_searchwork.Text))&&!(item.comment != null && item.comment != "" && item.comment.Contains(textBox_searchwork.Text)))
                        {
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    

                    m_Appointment = new Appointment
                    {
                        StartDate = item.time.AddHours(8)
                    };
                    string taskname = item.name;
                    if (!logfile.Contains("fanqie"))
                    {
                        //taskname += ("("+item.tasktime.ToString("N0")+")");
                    }
                    try//解决有点只保存结束时间的问题
                    {
                        if (item.mindmap == "FanQie"&&item.tasktime==0&& item.comleteTime!=null)
                        {
                            item.tasktime = ((TimeSpan)(item.comleteTime - item.time)).TotalMinutes;
                        }
                    }
                    catch (Exception e)
                    { }
                    if (ini.ReadString("appearance", "Calander15", "1")=="true"&&!c_15.Checked||c_fanqie.Checked || c_timeBlock.Checked)
                    {
                        if (item.tasktime < 15)
                        {
                            item.tasktime = 15;
                        }
                    }
                    else
                    {
                        if (item.tasktime < 30)
                        {
                            item.tasktime = 30;
                        }
                    }
                    
                    m_Appointment.EndDate = item.time.AddHours(8).AddMinutes(item.tasktime);
                    if (logfile.Contains("fanqie"))
                    {
                        DateTime dt = DateTime.Now;
                        if (item.comleteTime != null)
                        {
                            dt = Convert.ToDateTime(item.comleteTime);
                            if ((dt - item.time).TotalMinutes >= 20)
                            {
                                m_Appointment.EndDate = dt;
                            }
                            //taskname += ("(" + (dt - item.time).TotalMinutes.ToString("N0") + ")");
                        }
                    }
                    if (isZhuangbi)
                    {
                        string patten = @"(\S)";
                        Regex reg = new Regex(patten);
                        taskname = reg.Replace(taskname, "*");
                        if (item.comment!=null&&item.comment!="")
                        {
                            item.comment = reg.Replace(item.comment, "*");
                        }
                    }
                    if (showfatchertimeblock&& item.mindmap == "TimeBlock")
                    {
                        m_Appointment.Title =item.nameFull+ (item.nameFull!=null&& item.nameFull != ""? "|":"") + taskname;
                    }
                    else
                    {
                        m_Appointment.Title = taskname;
                    }
                    m_Appointment.Comment = item.comment;
                    if (showcomment && m_Appointment.Comment!=null&& m_Appointment.Comment!="")
                    {
                        m_Appointment.Title += ("("+m_Appointment.Comment+")");
                    }
                    m_Appointment.value = item.mindmapPath;
                    m_Appointment.ID = item.ID != null ? item.ID.ToString() : "";
                    int zhongyao = item.tasklevel;
                    if (numericUpDown2.Value >= 0)
                    {
                        if (zhongyao < numericUpDown2.Value)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (zhongyao > numericUpDown2.Value)
                        {
                            continue;
                        }
                    }

                    if (zhongyao == 0)
                    {
                        m_Appointment.Color = System.Drawing.Color.White;
                        m_Appointment.BorderColor = System.Drawing.Color.White;
                        if (item.mindmap== "TimeBlock"|| item.mindmap == "Mistake"||item.mindmap == "Progress")
                        {
                            try
                            {

                                m_Appointment.Color = Color.FromArgb(Int32.Parse(item.mindmapPath));
                                m_Appointment.BorderColor = Color.FromArgb(Int32.Parse(item.mindmapPath));
                                //if (item.time.AddHours(8) < DateTime.Today)//时间块禁止编辑？
                                //{
                                //    m_Appointment.Locked = true;
                                //}
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    else if (zhongyao == 1)
                    {
                        m_Appointment.Color = System.Drawing.Color.PowderBlue;
                        m_Appointment.BorderColor = System.Drawing.Color.PowderBlue;
                    }
                    else if (zhongyao == 2)
                    {
                        m_Appointment.Color = System.Drawing.Color.PowderBlue;
                        m_Appointment.BorderColor = System.Drawing.Color.PowderBlue;
                    }
                    else if (zhongyao == 3)
                    {
                        m_Appointment.Color = System.Drawing.Color.LightSkyBlue;
                        m_Appointment.BorderColor = System.Drawing.Color.LightSkyBlue;
                    }
                    else if (zhongyao == 4)
                    {
                        m_Appointment.Color = System.Drawing.Color.DeepSkyBlue;
                        m_Appointment.BorderColor = System.Drawing.Color.DeepSkyBlue;
                    }
                    else if (zhongyao == 5)
                    {
                        m_Appointment.Color = System.Drawing.Color.CadetBlue;
                        m_Appointment.BorderColor = System.Drawing.Color.CadetBlue;
                    }
                    else if (zhongyao == 6)
                    {
                        m_Appointment.Color = System.Drawing.Color.Gold;
                        m_Appointment.BorderColor = System.Drawing.Color.Gold;
                    }
                    else if (zhongyao == 7)
                    {
                        m_Appointment.Color = System.Drawing.Color.Orange;
                        m_Appointment.BorderColor = System.Drawing.Color.Orange;
                    }
                    else if (zhongyao == 8)
                    {
                        m_Appointment.Color = System.Drawing.Color.OrangeRed;
                        m_Appointment.BorderColor = System.Drawing.Color.OrangeRed;
                    }
                    else if (zhongyao == 9)
                    {
                        m_Appointment.Color = System.Drawing.Color.Crimson;
                        m_Appointment.BorderColor = System.Drawing.Color.Crimson;
                    }
                    else if (zhongyao >= 10)
                    {
                        m_Appointment.Color = System.Drawing.Color.Red;
                        m_Appointment.BorderColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                         m_Appointment.Color = System.Drawing.Color.PowderBlue;
                         m_Appointment.BorderColor = System.Drawing.Color.PowderBlue;
                    }
                    switch (item.mindmap)
                    {
                        case "FanQie":
                            m_Appointment.Type = "番茄钟";
                            break;
                        case "TimeBlock":
                            m_Appointment.Type = "时间块";
                            break;
                        default:
                            m_Appointment.Type = "任务";
                            break;
                    }
                    m_Appointment.Tag = item;
                    m_Appointments.Add(m_Appointment);
                }
            }
            dayView1.Refresh();
        }
        public void jietu()
        {
            try
            {
                //截图
                Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
                Graphics g = Graphics.FromImage(bit);
                g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
                g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
                                                                                               //g.CopyFromScreen(panel游戏区 .PointToScreen(Point.Empty), Point.Empty, panel游戏区.Size);//只保存某个控件（这里是panel游戏区）
                bit.Save(CalendarImagePath + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好
            }
            catch (Exception ex)
            {
                MessageBox.Show("截图错误" + ex.ToString());
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            RefreshCalender();
        }
        public string GetFolderName(string path)
        {
            if (path == "")
            {
                return "";
            }
            string filePathOnly = Path.GetDirectoryName(path);  //D:\Temp
            string folderName = Path.GetFileName(filePathOnly);  //Temp
            return folderName;
        }
        public bool HasInNoFolder(string path)
        {
            if (path == "" || path == null)
            {
                return false;
            }
            foreach (string item in noFolder)
            {
                if (path.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        private void 截图_Click(object sender, EventArgs e)
        {
            jietu();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            //jietu();
        }

        private void CalendarForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Console.CapsLock && !islock)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Edit(true);
                        break;
                    default:
                        break;
                }
                dayView1.AllowInplaceEditing = true;
                dayView1.AllowNew = true;
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            this.Opacity += 0.05;
                        }
                        else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (workfolder_combox.SelectedIndex > 0)
                            {
                                workfolder_combox.SelectedIndex--;
                            }
                        }
                        else
                        {
                            if (numericUpDown1.Value < 14&&!c_lock.Checked)
                            {
                                numericUpDown1.Value++;
                            }
                        }
                        break;
                    case Keys.Down:
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            this.Opacity -= 0.05;
                        }
                        else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (workfolder_combox.SelectedIndex < workfolder_combox.Items.Count - 1)
                            {
                                workfolder_combox.SelectedIndex++;
                            }
                        }
                        else
                        {
                            if (numericUpDown1.Value >= 2 && !c_lock.Checked)
                            {
                                numericUpDown1.Value--;
                            }
                        }
                        break;

                    case Keys.Left:
                        if (c_lock.Checked)
                        {
                            return;
                        }
                        if (numericUpDown1.Value == 7&& dateTimePicker1.Value.DayOfWeek==DayOfWeek.Monday)
                        {
                            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-7);
                        }
                        else if (numericUpDown1.Value == 14)
                        {
                            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-14);
                        }
                        else
                        {
                            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
                        }
                        break;
                    case Keys.Right:
                        if (c_lock.Checked)
                        {
                            return;
                        }
                        if (numericUpDown1.Value == 7)
                        {
                            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(7);
                        }
                        else if (numericUpDown1.Value == 14)
                        {
                            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(14);
                        }
                        else
                        {
                            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
                        }
                        break;
                    case Keys.S:
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            showfatchertimeblock = !showfatchertimeblock;
                            RefreshCalender();
                        }
                        else
                        {
                            c_fanqie.Checked = !c_fanqie.Checked;
                        }
                        break;
                    case Keys.A:
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            showcomment = !showcomment;
                            RefreshCalender();
                        }
                        else
                        {
                            c_timeBlock.Checked = !c_timeBlock.Checked;
                        }
                        break;
                    case Keys.Q:
                        this.Close();
                        break;
                    case Keys.PageUp:
                        if (numericUpDown2.Value <= 99)
                        {
                            numericUpDown2.Value++;
                        }
                        break;
                    case Keys.PageDown:
                        if (numericUpDown2.Value >= 0)
                        {
                            numericUpDown2.Value--;
                        }
                        break;
                    case Keys.Home:
                        dateTimePicker1.Value = DateTime.Today;
                        break;
                    case Keys.I:
                        textBox_searchwork.Focus();
                        break;
                    case Keys.R:
                        RefreshCalender();
                        break;
                    case Keys.Escape:
                        dayView1.Focus();
                        break;
                    case Keys.Delete:
                        toolStripMenuItem2_Click(null, null);
                        break;
                    default:
                        break;
                }
                dayView1.AllowInplaceEditing = false;
                dayView1.AllowNew = false;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            dayView1.StartDate = dayView1.StartDate;//用于刷新
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (workfolder_combox.SelectedItem.ToString() == "All")
            {
                mindmappath = "";
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//用于刷新
            }
            else if (workfolder_combox.SelectedItem.ToString() == "RootPath")
            {
                mindmappath = ini.ReadString("path", "rootpath", ""); ;
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//用于刷新
            }
            else
            {
                mindmappath = ini.ReadString("path", workfolder_combox.SelectedItem.ToString(), "");
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//用于刷新
            }
        }

        private void dayView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                try
                {
                    this.Text = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes.ToString("N") + "分钟" + "|" + dayView1.SelectedAppointment.Title;
                }
                catch (Exception)
                {
                }
            }
        }

        private void dayView1_AppoinmentMove(object sender, AppointmentEventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                ismovetask = true;
            }

        }
        public void EditTask(string path, string id, DateTime startdate, double tasktime, string TaskName)
        {
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(path);
            foreach (XmlNode node in x.GetElementsByTagName("node"))
            {
                if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == id)
                {
                    try
                    {
                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                        bool isHashook = false;
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            if (item.Name == "hook" && !isHashook)
                            {
                                isHashook = true;
                                item.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((startdate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            }
                        }
                        if (!isHashook)
                        {
                            XmlNode remindernode = x.CreateElement("hook");
                            XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                            remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                            remindernode.Attributes.Append(remindernodeName);
                            XmlNode remindernodeParameters = x.CreateElement("Parameters");
                            XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                            remindernodeTime.Value = (Convert.ToInt64((startdate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            remindernodeParameters.Attributes.Append(remindernodeTime);
                            remindernode.AppendChild(remindernodeParameters);
                            node.AppendChild(remindernode);
                        }
                        //else
                        //{
                        //    node.FirstChild.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        //}
                        XmlAttribute TASKTIME = x.CreateAttribute("TASKTIME");
                        node.Attributes.Append(TASKTIME);
                        node.Attributes["TASKTIME"].Value = ((int)tasktime).ToString();
                        node.Attributes["TEXT"].Value = TaskName;
                        x.Save(path);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                        th.Start();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        public void EditTaskName(string path, string id, string NewName)
        {
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(path);
            foreach (XmlNode node in x.GetElementsByTagName("node"))
            {
                if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == id)
                {
                    try
                    {
                        node.Attributes["TEXT"].Value = NewName;
                        x.Save(path);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                        th.Start();
                        return;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        //修改时间
        private void dayView1_MouseUp(object sender, MouseEventArgs e)
        {
            toolTip2.Hide(this);
            if (logfile.Contains("fanqie"))
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                Edit();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Menu.Show(dayView1, new System.Drawing.Point(e.X, e.Y));
            }
        }
        public void Edit(bool isEdit = false, Appointment app = null)
        {
            if ((ismovetask || isEdit) && dayView1.SelectedAppointment != null)
            {
                ismovetask = false;
                try
                {
                    if (!c_timeBlock.Checked)
                    {
                        EditTask(dayView1.SelectedAppointment.value, dayView1.SelectedAppointment.ID, dayView1.SelectedAppointment.StartDate, (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes, dayView1.SelectedAppointment.Title);
                    }
                    Reminder reminderObject = new Reminder();
                    FileInfo fi = new FileInfo(logfile);
                    using (StreamReader sw = fi.OpenText())
                    {
                        string s = sw.ReadToEnd();
                        var serializer = new JavaScriptSerializer();
                        reminderObject = serializer.Deserialize<Reminder>(s);
                        ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(dayView1.SelectedAppointment.value) && m.ID == dayView1.SelectedAppointment.ID);
                        if (current != null)
                        {
                            current.time = dayView1.SelectedAppointment.StartDate;
                            current.tasktime = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes;
                            current.name = dayView1.SelectedAppointment.Title.Split('(')[0];
                        }
                    }
                    string json = new JavaScriptSerializer().Serialize(reminderObject);
                    File.WriteAllText(@"reminder.json", "");
                    using (StreamWriter sw = fi.AppendText())
                    {
                        sw.Write(json);
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (app != null)
            {
                ismovetask = false;
                try
                {
                    if (!c_timeBlock.Checked)
                    {
                        EditTask(app.value, app.ID, app.StartDate, (app.EndDate - app.StartDate).TotalMinutes, app.Title);
                    }
                    Reminder reminderObject = new Reminder();
                    FileInfo fi = new FileInfo(logfile);
                    using (StreamReader sw = fi.OpenText())
                    {
                        string s = sw.ReadToEnd();
                        var serializer = new JavaScriptSerializer();
                        reminderObject = serializer.Deserialize<Reminder>(s);
                        ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(app.value) && m.ID == app.ID);
                        if (current != null)
                        {
                            current.time = app.StartDate;
                            current.tasktime = (app.EndDate - app.StartDate).TotalMinutes;
                            current.name = app.Title.Split('(')[0]; ;
                        }
                    }
                    string json = new JavaScriptSerializer().Serialize(reminderObject);
                    File.WriteAllText(@"reminder.json", "");
                    using (StreamWriter sw = fi.AppendText())
                    {
                        sw.Write(json);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void TextBox_searchwork_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    RefreshCalender();
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
        private void CalendarWhell(object sender, MouseEventArgs e)
        {
            try
            {
                if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                {
                    if (dayView1.HalfHourHeight == 20)
                    {
                        return;
                    }
                    if (e.Delta > 0)
                    {
                        if (dayView1.StartHour < 12)
                        {
                            dayView1.StartHour += 1;
                        }
                    }
                    else
                    {
                        if (dayView1.StartHour > 0)
                        {
                            dayView1.StartHour -= 1;
                        }
                    }
                }
                else
                {
                    if (dayView1.HalfHourHeight == 20)
                    {
                        dayView1.StartHour = 0;
                    }
                    if (e.Delta > 0)
                    {
                        dayView1.HalfHourHeight += 2;
                    }
                    else
                    {
                        if (dayView1.HalfHourHeight > 20)
                        {
                            dayView1.HalfHourHeight -= 2;
                        }
                    }
                }
                dayView1.Refresh();
            }
            catch (Exception)
            {
                dayView1.Refresh();
            }
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void dayView1_DoubleClick(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                ismovetask = false;
                try
                {
                    System.Diagnostics.Process.Start(dayView1.SelectedAppointment.value);
                }
                catch (Exception)
                {
                }
            }
        }

        private void numericOpacity_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = Convert.ToDouble(numericOpacity.Value / 100);
        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void dayView1_NewAppointment_1(object sender, NewAppointmentEventArgs args)
        {

        }
        bool islock = true;
        private bool showfatchertimeblock;
        private bool showcomment=true;
        private int lastX;
        private int lastY;
        private bool nomoretooltip;

        private void lockButton_Click(object sender, EventArgs e)
        {
            if (lockButton.Text == "锁定")
            {
                lockButton.Text = "解锁";
                islock = true;
            }
            else
            {
                lockButton.Text = "锁定";
                islock = false;
            }
        }

        private void SetTimeBlock(object sender, EventArgs e)
        {

            Appointment m_Appointment = new Appointment();
            m_Appointment = new Appointment
            {
                StartDate = dayView1.SelectionStart
            };
            m_Appointment.EndDate = dayView1.SelectionEnd;
            m_Appointment.Title = ((System.Windows.Forms.ToolStripItem)sender).Text;
            m_Appointment.value = ((System.Windows.Forms.ToolStripItem)sender).BackColor.ToArgb().ToString();
            m_Appointment.ID = Guid.NewGuid().ToString();
            m_Appointment.Type = "TimeBlock";
            string comment = "";
            string fanqieid = "";
            if (dayView1.SelectedAppointment != null&&dayView1.SelectedAppointment.Type== "番茄钟")//切换颜色吧？
            {
                m_Appointment.StartDate = dayView1.SelectedAppointment.StartDate;
                m_Appointment.EndDate = dayView1.SelectedAppointment.EndDate;
                m_Appointment.Comment = dayView1.SelectedAppointment.Title;
                comment = dayView1.SelectedAppointment.Title;
                fanqieid = dayView1.SelectedAppointment.ID;
                m_Appointments.Remove(dayView1.SelectedAppointment);
            }
            unchecked
            {
                m_Appointment.Color = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
                m_Appointment.BorderColor = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
            }

            //m_Appointment.Locked = true;
            m_Appointments.Add(m_Appointment);
            dayView1.Refresh();
            reminderObjectJsonAdd(m_Appointment.Title, m_Appointment.ID, m_Appointment.value, (m_Appointment.EndDate - m_Appointment.StartDate).TotalMinutes, m_Appointment.StartDate, "TimeBlock", ((System.Windows.Forms.ToolStripItem)sender).Tag, comment);
            SetFanQieComplete(fanqieid);
        }
        private void SetProgress(object sender, EventArgs e)
        {

            Appointment m_Appointment = new Appointment();
            m_Appointment = new Appointment
            {
                StartDate = dayView1.SelectionStart
            };
            m_Appointment.EndDate = dayView1.SelectionEnd;
            m_Appointment.Title = ((System.Windows.Forms.ToolStripItem)sender).Text;
            m_Appointment.value = ((System.Windows.Forms.ToolStripItem)sender).BackColor.ToArgb().ToString();
            m_Appointment.ID = Guid.NewGuid().ToString();
            m_Appointment.Type = "Progress";
            string comment = "";
            unchecked
            {
                m_Appointment.Color = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
                m_Appointment.BorderColor = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
            }

            //m_Appointment.Locked = true;
            m_Appointments.Add(m_Appointment);
            dayView1.Refresh();
            reminderObjectJsonAdd(m_Appointment.Title, m_Appointment.ID, m_Appointment.value, (m_Appointment.EndDate - m_Appointment.StartDate).TotalMinutes, m_Appointment.StartDate, "Progress", ((System.Windows.Forms.ToolStripItem)sender).Tag, comment);
        }
        private void SetMistake(object sender, EventArgs e)
        {

            Appointment m_Appointment = new Appointment();
            m_Appointment = new Appointment
            {
                StartDate = dayView1.SelectionStart
            };
            m_Appointment.EndDate = dayView1.SelectionEnd;
            m_Appointment.Title = ((System.Windows.Forms.ToolStripItem)sender).Text;
            m_Appointment.value = ((System.Windows.Forms.ToolStripItem)sender).BackColor.ToArgb().ToString();
            m_Appointment.ID = Guid.NewGuid().ToString();
            m_Appointment.Type = "Mistake";
            string comment = "";
            unchecked
            {
                m_Appointment.Color = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
                m_Appointment.BorderColor = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
            }

            //m_Appointment.Locked = true;
            m_Appointments.Add(m_Appointment);
            dayView1.Refresh();
            reminderObjectJsonAdd(m_Appointment.Title, m_Appointment.ID, m_Appointment.value, (m_Appointment.EndDate - m_Appointment.StartDate).TotalMinutes, m_Appointment.StartDate, "Mistake", ((System.Windows.Forms.ToolStripItem)sender).Tag, comment);
        }
        private void SetTimeBlockColor(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)//切换颜色吧？
            {
                dayView1.SelectedAppointment.Color = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
                dayView1.SelectedAppointment.BorderColor = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
            }
            reminderObjectJsonTimeBlockColor(dayView1.SelectedAppointment.ID, dayView1.SelectedAppointment.Color);
            dayView1.Refresh();
        }
        public string GetColor(XmlNode node)
        {
            try
            {
                foreach (XmlNode item in node.ChildNodes)
                {
                    if (item.Name == "edge")
                    {
                        return item.Attributes["COLOR"].Value;
                    }
                }
                foreach (XmlNode item in node.ParentNode.ChildNodes)
                {
                    if (item.Name == "edge")
                    {
                        return item.Attributes["COLOR"].Value;
                    }
                }
                foreach (XmlNode item in node.ParentNode.ParentNode.ChildNodes)
                {
                    if (item.Name == "edge")
                    {
                        return item.Attributes["COLOR"].Value;
                    }
                }
                return "#ffffff";
            }
            catch (Exception)
            {
                return "#ffffff";
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                Reminder reminderObject = new Reminder();
                FileInfo fi = new FileInfo(logfile);
                using (StreamReader sw = fi.OpenText())
                {
                    string s = sw.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    reminderObject = serializer.Deserialize<Reminder>(s);
                    reminderObject.reminders.RemoveAll(m => m.ID == dayView1.SelectedAppointment.ID);
                }
                string json = new JavaScriptSerializer().Serialize(reminderObject);
                File.WriteAllText(@"reminder.json", "");
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.Write(json);
                }
                m_Appointments.Remove(dayView1.SelectedAppointment);
                dayView1.Refresh();
            }
            catch (Exception)
            {
            }
        }
        private void SetFanQieComplete(string id)
        {
            try
            {
                Reminder reminderObject = new Reminder();
                FileInfo fi = new FileInfo(logfile);
                using (StreamReader sw = fi.OpenText())
                {
                    string s = sw.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    reminderObject = serializer.Deserialize<Reminder>(s);
                    ReminderItem item= reminderObject.reminders.FirstOrDefault(m => m.ID == id);
                    if (item!=null)
                    {
                        item.isCompleted = true;
                    }
                }
                string json = new JavaScriptSerializer().Serialize(reminderObject);
                File.WriteAllText(@"reminder.json", "");
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.Write(json);
                }
                m_Appointments.Remove(dayView1.SelectedAppointment);
                dayView1.Refresh();
            }
            catch (Exception)
            {
            }
        }

        private void 打开导图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(dayView1.SelectedAppointment.value);

            }
            catch (Exception)
            {
            }
        }

        private void c_timeBlock_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }

        private void c_fanqie_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            dayView1.StartDate = dayView1.StartDate;//用于刷新
        }

        private void dayView1_AppointmentMouseHover(object sender, yixiaozi.WinForm.Control.DayView.AppointmentMouseHoverEventArgs args)
        {
            //基本不会运行
            //toolTip1.ToolTipTitle = "任务";
            //toolTip1.Show(args.Title + Environment.NewLine + args.EndDate.ToShortDateString(), this, new System.Drawing.Point(Control.MousePosition.X+ 1, Control.MousePosition.Y + 1), int.MaxValue);
        }

        private void dayView1_AppointmentMouseLeave(object sender, yixiaozi.WinForm.Control.DayView.AppointmentMouseLeaveEventArgs args)
        {
            //toolTip1.Hide(this);
        }

        private void dayView1_AppointmentMouseMove(object sender, Appointment args, MouseEventArgs e)
        {
            if (args == null || nomoretooltip)
            {
                nomoretooltip = false;
                toolTip2.Hide(this);
            }
            else
            {
                toolTip2.Hide(dayView1);
                toolTip2.ToolTipTitle = args.Type;
                string editinfo = "";
                try
                {
                    if (args != null&& args.Tag!=null&& ((ReminderItem)args.Tag).editCount > 0)
                    {
                        editinfo += Environment.NewLine;
                        editinfo += ("编辑次数:"+ ((ReminderItem)args.Tag).editCount);
                        editinfo += Environment.NewLine;
                        int count = 0;
                        foreach (DateTime item in ((ReminderItem)args.Tag).editTime)
                        {
                            editinfo += (item.ToString("MM月dd-HH:mm")+";");
                            count++;
                            if (count%5==0)
                            {
                                editinfo += Environment.NewLine;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                string timeblocktop = "";
                try
                {
                    if (args.Type== "时间块")
                    {
                        timeblocktop = ((ReminderItem)args.Tag).nameFull;
                        if (timeblocktop!="")
                        {
                            timeblocktop += "|";
                        }
                    }
                }
                catch (Exception)
                {
                }
                
                toolTip2.Show(timeblocktop+args.Title.Split('(')[0] + Environment.NewLine + args.StartDate.ToShortTimeString() + Environment.NewLine + args.EndDate.ToShortTimeString() + (args.Comment != null ? (Environment.NewLine + args.Comment.Substring(0,args.Comment.Length>=20?20: args.Comment.Length)) : "")+ editinfo, dayView1, new System.Drawing.Point(Control.MousePosition.X + 1, Control.MousePosition.Y + 1), int.MaxValue);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolTip2_Popup(object sender, PopupEventArgs e)
        {

        }
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 420,
                Height = 210,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            RichTextBox textBox = new RichTextBox() { Left = 10, Top = 10, Width = 380,Height=100};
            textBox.Text = text;
            Button confirmation = new Button() { Text = "Ok", Left = 50, Width = 100, Top = 130, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void commentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptValue = "";
            if (dayView1.SelectedAppointment!=null)
            {
                promptValue = ShowDialog(dayView1.SelectedAppointment.Comment ?? "", "编辑详细");
                dayView1.SelectedAppointment.Comment = promptValue;
                dayView1.Refresh();
            }
            Reminder reminderObject = new Reminder();
            FileInfo fi = new FileInfo(logfile);
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                reminderObject = serializer.Deserialize<Reminder>(s);
                ReminderItem current = reminderObject.reminders.FirstOrDefault(m => m.ID == dayView1.SelectedAppointment.ID);
                if (current != null)
                {
                    current.comment = promptValue;
                }
            }
            string json = new JavaScriptSerializer().Serialize(reminderObject);
            File.WriteAllText(@"reminder.json", "");
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
            RefreshCalender();
        }

        private void dayView1_KeyDown(object sender, KeyEventArgs e)
        {
            toolTip2.Hide(this);
            nomoretooltip = true;
        }

        private void textBox_searchwork_TextChanged(object sender, EventArgs e)
        {
            RefreshCalender();
        }

        private void c_done_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
        }

        private void c_progress_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();

        }

        private void c_mistake_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();

        }

        private void 番茄钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread th = new Thread(() => OpenFanQie(Convert.ToInt32((dayView1.SelectedAppointment.EndDate-dayView1.SelectedAppointment.StartDate).TotalMinutes), dayView1.SelectedAppointment.Title, dayView1.SelectedAppointment.value, GetPosition()));
                tomatoCount += 1;
                th.Start();
            }
            catch (Exception)
            {
            }
        }
        public void OpenFanQie(int time, string name, string mindmap, int fanqieCount, bool isnotDefault = false, int tasklevelNum = 0)
        {
            Tomato fanqie = new Tomato(new DateTime().AddMinutes(time), name, mindmap, GetPosition(), isnotDefault, tasklevelNum);
            fanqie.ShowDialog();
        }
    }
    internal class User32
    {
        public const int SE_SHUTDOWN_PRIVILEGE = 0x13;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx,
            int cy, uint uFlags);
    }

    
}
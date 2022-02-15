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

namespace Calendar
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

        public CalendarForm(string path)// ����ϣ��ֻ��ʾ�����ļ��е�����
        {
            mindmappath = path;
            string logpass = ini.ReadString("password", "abc", "");
            encryptlog = new Encrypt(logpass);
            InitializeComponent();
            //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            new MyProcess().OnlyOneForm("Calendar.exe");
            string calanderpath = ini.ReadString("path", "calanderpath", "");
            workfolders.Add(ini.ReadString("path", "rootpath", ""));
            foreach (string item in calanderpath.Split(';'))
            {
                workfolder_combox.Items.Add(item);
                workfolders.Add(ini.ReadString("path", item, ""));
            }
            tasktime.Text = "";
            workfolder_combox.Items.Add("All");
            workfolder_combox.SelectedIndex= hasinworkfolderIndex(mindmappath);
            taskname.Text = "";
            tasktime.Text = "";
            string no = ini.ReadString("path", "no", "");
            noFolder = no.Split(';');
            CalendarImagePath = ini.ReadStringDefault("path", "CalendarImagePath", "");
            dayView1.Renderer = new Office11Renderer();
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //�����λ����Location���Ծ���
            this.Location = (System.Drawing.Point)new Size(x, y);         //�������ʼλ��Ϊ(x,y)
            RefreshCalender();
            DateTime startWeek = DateTime.Today;
            if (startWeek.DayOfWeek.ToString("d")!="0")
            {
                startWeek = startWeek.AddDays(1 - Convert.ToInt32(startWeek.DayOfWeek.ToString("d")));
            }
            else
            {
                startWeek = startWeek.AddDays(-6);
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
            IntPtr hWndMyWindow = FindWindow(null, this.Name);//ͨ�����ڵı����þ��
            IntPtr hWndDesktop = FindWindow("Progman", "Program Manager");//���������
            SetParent(hWndMyWindow, hWndDesktop); //����������Ϊ������Ӵ���
            dayView1.AllowInplaceEditing = false;
            dayView1.AllowNew = false;
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
            this.StartPosition = FormStartPosition.Manual; //�����λ����Location���Ծ���
            this.Location = (System.Drawing.Point)new Size(x, y);         //�������ʼλ��Ϊ(x,y)
        }
        public void reminderObjectJsonAdd(string TaskName, string ID, string mindmap,double tasktime,DateTime taskTime)
        {
            try
            {
                Reminder reminderObject = new Reminder();
                FileInfo fi = new FileInfo(logfile);
                if (!System.IO.File.Exists(logfile))
                {
                    File.WriteAllText(logfile, "");
                }
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
                        mindmap="calander",
                        ID = ID,
                        tasktime= tasktime
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
        void dayView1_NewAppointment(object sender, NewAppointmentEventArgs args)
        {
            Appointment m_Appointment = new Appointment
            {
                StartDate = args.StartDate,
                EndDate = args.EndDate,
                Title = args.Title,
                value = mindmappath + @"\calander.mm",
                ID = Guid.NewGuid().ToString()
            };
            m_Appointments.Add(m_Appointment);
            try
            {
                reminderObjectJsonAdd(args.Title, m_Appointment.ID, m_Appointment.value, (args.EndDate - args.StartDate).TotalMinutes,args.StartDate);
                string path = mindmappath + @"\calander.mm";
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Copy(System.AppDomain.CurrentDomain.BaseDirectory + @"\calander.mm", path);
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
                SaveLog("�����������" + changedtaskname + "    ��ͼ��" + mindmappath + @"\calander.mm");
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
            if (dayView1.SelectedAppointment!=null)
            {
                lastId = dayView1.SelectedAppointment.ID;
                lastName = dayView1.SelectedAppointment.Title;
            }
            //�����Ƿ�仯
            foreach (Appointment m_App in m_Appointments)
            {
                if (m_App.ID==lastId&&m_App.Title!=lastName)
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
            int i = 0;//������һ������Ϊ��һ���Ǹ�Ŀ¼
            foreach (var item in workfolders)
            {
                if (i==0)
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
            int i=0;
            foreach (var item in workfolders)
            {
                if (path==item)
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
            if (!System.IO.File.Exists(logfile))
            {
                File.WriteAllText(logfile, "");
            }
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                reminderObject = serializer.Deserialize<Reminder>(s);
                //IEnumerable<ReminderItem> items = reminderObject.reminders.Where(m => !m.isCompleted && m.mindmapPath.Contains(mindmappath)&& ((!HasInNoFolder(GetFolderName(m.mindmapPath)))|| ((workfolder_combox.SelectedItem == null && !hasinworkfolder(m.mindmapPath)) || (workfolder_combox.SelectedItem != null ? workfolder_combox.SelectedItem.ToString() == "All" : false))));
                if (reminderObject.reminders==null||reminderObject.reminders.Count==0)
                {
                    return;
                }
                IEnumerable<ReminderItem> items = reminderObject.reminders.Where(m => !m.isCompleted &&!m.isview&&!m.isEBType&& m.mindmapPath.Contains(mindmappath)&&(m.mindmapPath.Contains(textBox_searchwork.Text)||m.name.Contains(textBox_searchwork.Text)));
                if (workfolder_combox.SelectedItem!=null&&workfolder_combox.SelectedItem.ToString()== "RootPath")
                {
                    items = items.Where(m=>!hasinworkfolder(m.mindmapPath));
                }
                foreach (ReminderItem item in items)//���ﻹ������,�Ȳ������߼���
                {

                    m_Appointment = new Appointment
                    {
                        StartDate = item.time.AddHours(8)
                    };
                    string taskname = item.name;
                    if (!logfile.Contains("fanqie"))
                    {
                        //taskname += ("("+item.tasktime.ToString("N0")+")");
                    }
                    if (item.tasktime < 15)
                    {
                        item.tasktime = 30;
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
                    }
                    m_Appointment.Title = taskname;
                    m_Appointment.value = item.mindmapPath;
                    m_Appointment.ID = item.ID!=null? item.ID.ToString():"";
                    int zhongyao = item.tasklevel;
                    if (numericUpDown2.Value>=0)
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
                    m_Appointments.Add(m_Appointment);
                }
            }
            dayView1.Refresh();
        }
        public void jietu()
        {
            try
            {
                //��ͼ
                Bitmap bit = new Bitmap(this.Width, this.Height);//ʵ����һ���ʹ���һ�����bitmap
                Graphics g = Graphics.FromImage(bit);
                g.CompositingQuality = CompositingQuality.HighQuality;//������Ϊ���
                g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//������������ΪͼƬ
                                                                                               //g.CopyFromScreen(panel��Ϸ�� .PointToScreen(Point.Empty), Point.Empty, panel��Ϸ��.Size);//ֻ����ĳ���ؼ���������panel��Ϸ����
                bit.Save(CalendarImagePath + DateTime.Now.ToString("yyyy��MM��dd��HHʱmm��ss��") + ".png");//Ĭ�ϱ����ʽΪPNG�������jpg��ʽ�������Ǻܺ�
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ͼ����" + ex.ToString());
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

        private void ��ͼ_Click(object sender, EventArgs e)
        {
            jietu();
        }
        
        private void Timer2_Tick(object sender, EventArgs e)
        {
            //jietu();
        }

        private void CalendarForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (!Console.CapsLock&&!islock)
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
                            this.Opacity+=5;
                        }
                        else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (workfolder_combox.SelectedIndex>0)
                            {
                                workfolder_combox.SelectedIndex--;
                            }
                        }
                        else
                        {
                            if (numericUpDown1.Value < 14)
                            {
                                numericUpDown1.Value++;
                            }
                        }
                        break;
                    case Keys.Down:
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            this.Opacity -= 5;
                        }
                        else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (workfolder_combox.SelectedIndex < workfolder_combox.Items.Count-1)
                            {
                                workfolder_combox.SelectedIndex++;
                            }
                        }
                        else
                        {
                            if (numericUpDown1.Value >= 2)
                            {
                                numericUpDown1.Value--;
                            }
                        }
                        break;

                    case Keys.Left:
                        if (numericUpDown1.Value == 7)
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
                        if (logfile == "reminder.json")
                        {
                            logfile = "fanqie.json";
                        }
                        else
                        {
                            logfile = "reminder.json";
                        }
                        RefreshCalender();
                        dayView1.StartDate = dayView1.StartDate;//����ˢ��
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
                    case Keys.Escape:
                        dayView1.Focus();
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
            dayView1.StartDate = dayView1.StartDate;//����ˢ��
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (workfolder_combox.SelectedItem.ToString() == "All")
            {
                mindmappath = "";
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//����ˢ��
            }
            else if (workfolder_combox.SelectedItem.ToString() == "RootPath")
            {
                mindmappath = ini.ReadString("path", "rootpath", ""); ;
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//����ˢ��
            }
            else
            {
                mindmappath = ini.ReadString("path", workfolder_combox.SelectedItem.ToString(), "");
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//����ˢ��
            }
        }

        private void dayView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                try
                {
                    //taskname.Text = dayView1.SelectedAppointment.Title;
                    //tasktime.Text = ((dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes.ToString("N") + "����");
                    this.Text = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes.ToString("N") + "����"+"|"+ dayView1.SelectedAppointment.Title;
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
                ismovetask=true;
            }
            
        }
        public void EditTask(string path,string id,DateTime startdate,double tasktime,string TaskName)
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
                                item.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((startdate- TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
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
                            remindernodeTime.Value = (Convert.ToInt64((startdate- TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
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
        public void EditTaskName(string path, string id,string NewName)
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
        //�޸�ʱ��
        private void dayView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (logfile.Contains("fanqie"))
            {
                return;
            }
            if (e.Button== MouseButtons.Left)
            {
                Edit();
            }
            else if (e.Button==MouseButtons.Right)
            {
                contextMenuStrip1.Show(dayView1, new System.Drawing.Point(e.X, e.Y));
            }
        }
        public void Edit(bool isEdit=false,Appointment app=null)
        {
            if ((ismovetask||isEdit)&& dayView1.SelectedAppointment != null)
            {
                ismovetask = false;
                try
                {
                    EditTask(dayView1.SelectedAppointment.value, dayView1.SelectedAppointment.ID, dayView1.SelectedAppointment.StartDate, (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes, dayView1.SelectedAppointment.Title);
                    Reminder reminderObject = new Reminder();
                    FileInfo fi = new FileInfo(logfile);
                    if (!System.IO.File.Exists(logfile))
                    {
                        File.WriteAllText(logfile, "");
                    }
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
                            current.name = dayView1.SelectedAppointment.Title;
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
                    EditTask(app.value, app.ID, app.StartDate, (app.EndDate - app.StartDate).TotalMinutes, app.Title);
                    Reminder reminderObject = new Reminder();
                    FileInfo fi = new FileInfo(logfile);
                    if (!System.IO.File.Exists(logfile))
                    {
                        File.WriteAllText(logfile, "");
                    }
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
                            current.name = app.Title;
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
            this.Opacity =Convert.ToDouble(numericOpacity.Value/100);
        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void dayView1_NewAppointment_1(object sender, NewAppointmentEventArgs args)
        {

        }
        bool islock = true;
        private void lockButton_Click(object sender, EventArgs e)
        {
            if (lockButton.Text=="����")
            {
                lockButton.Text = "����";
                islock = true;
            }
            else
            {
                lockButton.Text = "����";
                islock = false;
            }
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
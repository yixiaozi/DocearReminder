using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using yixiaozi.Model.DocearReminder;
using yixiaozi.Security;
using yixiaozi.Windows;
using yixiaozi.WinForm.Control.Calendar;
using static DocearReminder.DocearReminderForm;
using Size = System.Drawing.Size;

namespace DocearReminder
{
    public partial class CalendarForm : Form
    {
        public List<Appointment> m_Appointments;
        public string mindmappath = "";
        public string[] noFolder = new string[] { };
        public string CalendarImagePath = "";
        public List<string> workfolders = new List<string>();
        public bool ismovetask = false;

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public Encrypt encryptlog;
        public string lastId = "";
        public string lastName = "";
        public List<Color> timeblockColors = new List<Color>();
        //public Sunisoft.IrisSkin.SkinEngine skinEngine1;
        CalendarFormSetting setting;
        Thread thCalendarForm;
        

        public CalendarForm(string path)// 后期希望只显示当期文件夹的日历
        {
            setting = new CalendarFormSetting();
            //thCalendarForm = new Thread(() => Application.Run(setting)); //重点
            //thCalendarForm.SetApartmentState(ApartmentState.STA); //重点
            //thCalendarForm.Priority = ThreadPriority.Highest;
            //thCalendarForm.Start();
            if (path == "show")
            {
                this.Show();
                this.Activate();
            }
            InitializeComponent();
            //读取CalendarStartDate,设置开始时间
            try
            {
                setting.btn_SwitchdTPicker.Text = ini.ReadString("Calendar", "CalendarStartDate", "今天");
                SetBtnSwitchdTPickerText();
            }
            catch (Exception ex)
            {
                SetMonday();
            }
            mindmappath = path;
            string logpass = ini.ReadString("password", "i", "");
            encryptlog = new Encrypt(logpass);
            //if (ini.ReadString("Skin", "src", "") != "")
            //{
            //    skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            //    skinEngine1.SkinFile = ini.ReadString("Skin", "src", "");
            //}
            //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            new MyProcess().OnlyOneForm("Calendar.exe");
            string calanderpath = ini.ReadString("path", "calanderpath", "");
            workfolders.Add(System.IO.Path.GetFullPath(ini.ReadString("path", "rootPath", "")));
            foreach (string item in calanderpath.Split(';'))
            {
                setting.workfolder_combox.Items.Add(item);
                workfolders.Add(System.IO.Path.GetFullPath(ini.ReadString("path", item, "")));
            }
            setting.workfolder_combox.Items.Add("All");
            setting.workfolder_combox.SelectedIndex = hasinworkfolderIndex(mindmappath);
            string no = ini.ReadString("path", "no", "");
            noFolder = no.Split(';');
            CalendarImagePath = System.IO.Path.GetFullPath((ini.ReadStringDefault("path", "CalendarImagePath", "")));
            dayView1.Renderer = new Office11Renderer();
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
            dayView1.AllowScroll = true;
            try
            {
                this.Opacity = Convert.ToDouble(ini.ReadString("appearance", "CalanderOpacity", "1"));
                setting.numericOpacity.Value = Convert.ToInt32(this.Opacity * 100);
            }
            catch (Exception ex)
            {
            }
            dayView1.NewAppointment += new NewAppointmentEventHandler(dayView1_NewAppointment);
            dayView1.SelectionChanged += new EventHandler(dayView1_SelectionChanged);
            //dayView1.ResolveAppointments += new ResolveAppointmentsEventHandler(this.dayView1_ResolveAppointments);
            dayView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dayView1_MouseMove);
            timer1.Interval = 60000;
            timer1.Start();
            //锁定到桌面
            //try
            //{
            //    if (Environment.OSVersion.Version.Major < 6)
            //    {
            //        base.SendToBack();

            //        IntPtr hWndNewParent = User32.FindWindow("Progman", null);
            //        User32.SetParent(base.Handle, hWndNewParent);
            //    }
            //    else
            //    {
            //        User32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, User32.SE_SHUTDOWN_PRIVILEGE);
            //    }
            //}
            //catch (ApplicationException exx)
            //{
            //    MessageBox.Show(this, exx.Message, "Pin to Desktop");
            //}
            Center();
            dayView1.MouseWheel += CalendarWhell;
            //IntPtr hWndMyWindow = FindWindow(null, this.Name);//通过窗口的标题获得句柄
            //IntPtr hWndDesktop = FindWindow("Progman", "Program Manager");//获得桌面句柄
            //SetParent(hWndMyWindow, hWndDesktop); //将窗口设置为桌面的子窗体
            //SetToDeskTop();
            dayView1.AllowInplaceEditing = false;
            dayView1.AllowNew = false;
            FreshMenu();
            RefreshCalender();
            showhide.Start();
        }

        public void SetMonday()
        {
            DateTime startWeek = DateTime.Today;
            if (startWeek.DayOfWeek.ToString("d") != "0")
            {
                startWeek = startWeek.AddDays(1 - Convert.ToInt32(startWeek.DayOfWeek.ToString("d")));
            }
            else
            {
                startWeek = startWeek.AddDays(-6);
            }

            setting.dTPicker_StartDay.Value = startWeek;
            dayView1.StartDate = startWeek;
        }

        public void FreshMenu()
        {
            for (int i = Menu.Items.Count - 1; i > 0; i--)
            {
                string name = Menu.Items[i].Text;
                if (name != "完成" && name != "Comment" && name != "打开导图"&&name != "打开设置" && name != "番茄钟" && name != "解锁")
                {
                    Menu.Items.RemoveAt(i);
                }
            }

            #region 添加菜单

            try
            {
                System.Xml.XmlDocument timeblockmm = new XmlDocument();
                timeblockmm.Load(ini.ReadString("TimeBlock", "mindmap", ""));
                foreach (XmlNode node in timeblockmm.GetElementsByTagName("node"))
                {
                    if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "金钱")
                    {
                        System.Windows.Forms.ToolStripItem newMenu = this.Menu.Items.Add("金钱", global::DocearReminder.Properties.Resources.square_ok, SetMoney);
                        newMenu.BackColor = Color.Yellow;
                        SearchNode_Money(node, newMenu);
                    }
                    if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "卡路里")
                    {
                        System.Windows.Forms.ToolStripItem newMenu = this.Menu.Items.Add("卡路里", global::DocearReminder.Properties.Resources.square_ok, SetKA);
                        newMenu.BackColor = Color.White;
                        SearchNode_KA(node, newMenu);
                    }
                    else if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "进步")
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
                    else if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "事件类别")
                    {
                        SearchNode(node, null);
                    }
                }
                if (timeblockColors.Count > 0)
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
            catch (Exception ex)
            {
            }

            #endregion 添加菜单
        }

        public void SearchNode(XmlNode node, ToolStripItem menuitem)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode Subnode in node.ChildNodes)
                {
                    if (Subnode.Attributes["TEXT"] != null && Subnode.Attributes["TEXT"].Value != "未分类")
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

        public void SearchNode_Money(XmlNode node, ToolStripItem menuitem)
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
                            newMenu = this.Menu.Items.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetMoney);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        else
                        {
                            newMenu = ((ToolStripMenuItem)menuitem).DropDownItems.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetMoney);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        SearchNode_Money(Subnode, newMenu);
                    }
                }
            }
        }

        public void SearchNode_KA(XmlNode node, ToolStripItem menuitem)
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
                            newMenu = this.Menu.Items.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetKA);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        else
                        {
                            newMenu = ((ToolStripMenuItem)menuitem).DropDownItems.Add(Subnode.Attributes["TEXT"].Value, global::DocearReminder.Properties.Resources.square_ok, SetKA);
                            newMenu.BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber));
                            newMenu.Tag = GetFatherNodeName(Subnode);
                        }
                        SearchNode_KA(Subnode, newMenu);
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
                        if (node.ParentNode.ParentNode.Name == "map" || node.ParentNode.Attributes["TEXT"].Value == "事件类别")
                        {
                            break;
                        }
                        s = node.ParentNode.Attributes["TEXT"].Value + (s != "" ? "|" : "") + s;
                        node = node.ParentNode;
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                return s;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //yixiaozi.WinForm.Common.AutoSizeForm asc = new AutoSizeForm();
        public void MainPage_Load(object sender, EventArgs e)
        {
            //asc.controllInitializeSize(this);
            //this.Height = this.MaximumSize.Height;
            //this.Width = this.MaximumSize.Width;
            this.WindowState = FormWindowState.Maximized;
            //asc.controlAutoSize(this);
            Center();
        }

        public void MainPage_SizeChanged(object sender, EventArgs e)
        {
            //asc.controlAutoSize(this);
        }

        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
        }
        /// <summary>
        /// 添加时间块
        /// </summary>
        /// <param name="TaskName"></param>
        /// <param name="ID"></param>
        /// <param name="mindmap">导图或者颜色</param>
        /// <param name="tasktime"></param>
        /// <param name="taskTime"></param>
        /// <param name="mindmapName"></param>
        /// <param name="tag"></param>
        /// <param name="comment"></param>
        /// <param name="detailComment"></param>
        /// <param name="timelong"></param>
        public static void reminderObjectJsonAdd(string TaskName, string ID, string mindmap, double tasktime, DateTime taskTime, string mindmapName, object tag = null, string comment = "", string detailComment = "", double timelong = 0)
        {
            try
            {
                ReminderItem item = reminderObject.reminders.Where(m => m.time <= DateTime.Now && (m.mindmap == "TimeBlock" || (m.mindmap == "FanQie" && m.name.Length != 5)) && m.isCompleted == false).OrderBy(m => m.TimeEnd).LastOrDefault();//查找最后一个就行了不计算时长了
                if (tasktime == 0)//如果没有记录时常，需要计算开始时间
                {
                    try
                    {
                        if (timelong == 0)
                        {
                            if (item != null)
                            {
                                DateTime endtime = item.time.AddMinutes(item.tasktime);
                                //如果endTime小于当天时间，则设置为当天时间
                                if (endtime < DateTime.Today)
                                {
                                    endtime = DateTime.Today;
                                }
                                tasktime = (taskTime - endtime).TotalMinutes;
                                taskTime = endtime;
                                if (tasktime <= 1)
                                {
                                    tasktime = 2;
                                }
                            }
                        }
                        else
                        {
                            taskTime = taskTime.AddMinutes(0 - timelong);
                            tasktime = timelong;
                            if (tasktime <= 1)
                            {
                                tasktime = 2;
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        taskTime = DateTime.Now;
                        tasktime = 30;
                    }
                }
                
                if (item != null)
                {
                    //如果要添加的和最后一个一样，则直接设置最后一个的值
                    //231107添加限制，只有同一天才可以合并
                    if (item.nameFull == (tag != null ? tag.ToString() : "") && item.name == TaskName&&item.time.Day== taskTime.Day)
                    {
                        item.tasktime = item.tasktime + tasktime;
                        item.comment +=((item.comment!=""&& item.comment != null? Environment.NewLine:"")+comment);
                        item.DetailComment += ((item.DetailComment != ""&& item.DetailComment != null ? Environment.NewLine : "") + detailComment);
                        return;
                    }
                }
                reminderObject.reminders.Add(new ReminderItem
                {
                    name = TaskName,
                    time = taskTime,
                    mindmapPath = mindmap,
                    mindmap = mindmapName,
                    ID = ID,
                    tasktime = tasktime,
                    nameFull = tag != null ? tag.ToString() : "",
                    comment = comment,
                    DetailComment = detailComment
                });
                FreshCalendarBool = true;
            }
            catch (Exception ex)
            {
            }
        }

        public void reminderObjectJsonTimeBlockColor(string ID, Color color)
        {
            try
            {
                ReminderItem item = reminderObject.reminders.FirstOrDefault(m => m.ID == ID);
                if (item != null)
                {
                    item.mindmapPath = color.ToArgb().ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void dayView1_NewAppointment(object sender, NewAppointmentEventArgs args)
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
            log = (DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "    " + log);
            log = encryptlog.EncryptString(log);
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\log.txt", true))
            {
                if (log != "")
                {
                    file.WriteLine(log);
                }
            }
        }

        public void dayView1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        public void dayView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                lastId = dayView1.SelectedAppointment.ID;
                lastName = dayView1.SelectedAppointment.Title;
            }
            //测试是否变化
            foreach (Appointment m_App in m_Appointments)
            {
                if (m_App.ID == lastId && m_App.Title != lastName && !m_App.Title.Contains("("))
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

        public void dayView1_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
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

        public void Form1_Load(object sender, EventArgs e)
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

        public void button6_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 1;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 3;
        }

        public void Button4_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 5;
        }

        public void button5_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 17;
        }

        #endregion MyRegion

        public void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            dayView1.StartDate = setting.dTPicker_StartDay.Value;
        }

        public void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dayView1.DaysToShow = (int)setting.numericUpDown1.Value;
            //RefreshCalender();
        }

        public void CalendarForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        public void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dayView1.StartDate = setting.dTPicker_StartDay.Value;
            RefreshCalender();
        }

        public static bool FreshCalendarBool = false;

        public void UpdateCalendar(object sender, EventArgs e)
        {
            DocearReminder.Tools.timeblockduiqi_Click(null, null);
            FreshMenu();
            RefreshCalender();
        }

        public void RefreshCalender()
        {
            if (setting.CaptureScreen.Checked || setting.JieTucheckBox.Checked || setting.CameracheckBox.Checked || setting.HTML.Checked || setting.ShowNodes.Checked || setting.AllFile.Checked || setting.ShowClipboard.Checked || setting.ActionLog.Checked)
            {
                m_Appointments = new List<Appointment>();
                if (setting.AllFile.Checked)
                {
                    ShowAllFile();
                }
                if (setting.ShowNodes.Checked)
                {
                    ShowAllNodes();
                }
                if (setting.ActionLog.Checked)
                {
                    ShowActionLog();
                }
                if (setting.CaptureScreen.Checked || setting.JieTucheckBox.Checked || setting.CameracheckBox.Checked || setting.HTML.Checked || setting.ShowClipboard.Checked)
                {
                    ShowCaptureScreen();
                }
                return;
            }
            m_Appointments = new List<Appointment>();
            //拆解一下下面的判断逻辑
            IEnumerable<ReminderItem> items = reminderObject.reminders.Where(m =>
                m.time >= setting.dTPicker_StartDay.Value &&
                m.time <= setting.dTPicker_StartDay.Value.AddDays((double)setting.numericUpDown1.Value) &&
                (((!m.isCompleted) && (!m.isview) && (!m.isEBType) && m.mindmapPath.Contains(mindmappath) &&
                  m.mindmap != "TimeBlock" && m.mindmap != "FanQie" && m.mindmap != "Progress" &&
                  m.mindmap != "Mistake" && !setting.c_timeBlock.Checked) ||
                 (setting.c_timeBlock.Checked && m.mindmap == "TimeBlock") || (setting.c_done.Checked && m.isCompleted) ||
                 (setting.c_fanqie.Checked && m.mindmap == "FanQie" && !m.isCompleted &&
                  !(m.name.Length == 5 && m.name[2] == ':')) || (setting.c_progress.Checked && m.mindmap == "Progress") ||
                 (setting.c_mistake.Checked && m.mindmap == "Mistake") || (setting.c_Money.Checked && m.mindmap == "Money") ||
                 (setting.Ka_c.Checked && m.mindmap == "KA") || (setting.c_timeBlock.Checked && m.time > DateTime.Today &&
                                                         m.mindmapPath.Contains(mindmappath) &&
                                                         (!m.isview || (setting.isview_c.Checked && m.isview)) &&
                                                         !m.isCompleted)));
            //&& !setting.c_timeBlock.Checked && !setting.c_fanqie.Checked && !setting.c_done.Checked &&!setting.c_progress.Checked && !setting.c_mistake.Checked && !setting.c_Money.Checked && !setting.Ka_c.Checked
            if (mindmappath == "")//当所有的时候排除金钱
            {
                items = items.Where(m => m.mindmap != "Money");
            }
            //if (setting.workfolder_combox.SelectedItem != null&&setting.workfolder_combox.SelectedItem.ToString() != "all"&& mindmappath !="")
            //{
            //    mindmapitems = mindmapitems.Where(m => m.mindmap == "FanQie" || m.mindmapPath.Contains(mindmappath));//!hasinworkfolder(m.mindmapPath));
            //}
            if (setting.workfolder_combox.SelectedItem != null && setting.workfolder_combox.SelectedItem.ToString() == "rootPath")
            {
                items = items.Where(m => m.mindmap == "FanQie" || !hasinworkfolder(m.mindmapPath));
            }

            //if (setting.workfolder_combox.SelectedItem != null && setting.workfolder_combox.SelectedItem.ToString() == "all")
            //{
            //    mindmapitems = mindmapitems.Where(m => m.mindmap == "FanQie" || !hasinworkfolder(m.mindmapPath));
            //}

            if (setting.c_timeBlock.Checked && !setting.isShowTask.Checked)
            {
                items = items.Where(m => m.mindmap == "TimeBlock");
            }

            foreach (ReminderItem item in items)//这里还有问题,先不折腾逻辑了
            {
                try//过滤字符串
                {
                    if (item.comment == null)
                    {
                        item.comment = "";
                    }
                    if (item.DetailComment == null)
                    {
                        item.DetailComment = "";
                    }
                    if (setting.textBox_searchwork.Text != "")
                    {
                        if (!(MyContains(item.name, setting.textBox_searchwork.Text) || MyContains(item.mindmapPath, setting.textBox_searchwork.Text) || MyContains(item.nameFull, setting.textBox_searchwork.Text) || MyContains(item.comment, setting.textBox_searchwork.Text) || MyContains(item.DetailComment, setting.textBox_searchwork.Text)))
                        {
                            continue;
                        }
                    }
                    if (setting.exclude.Text != "")
                    {
                        if ((MyContains(item.name, setting.exclude.Text)) || (MyContains(item.mindmapPath, setting.exclude.Text)) || (MyContains(item.nameFull, setting.exclude.Text)) || (MyContains(item.comment, setting.exclude.Text)) || (MyContains(item.DetailComment, setting.exclude.Text)))

                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                var m_Appointment = new Appointment
                {
                    StartDate = item.time
                };
                string taskname = item.name;
                string common = item.comment;
                string detailCommon = item.DetailComment;
                try//解决有点只保存结束时间的问题
                {
                    if (item.mindmap == "FanQie" && item.tasktime == 0 && item.comleteTime != null)
                    {
                        item.tasktime = ((TimeSpan)(item.comleteTime - item.time)).TotalMinutes;
                    }
                }
                catch (Exception e)
                { }
                double time = item.tasktime;
                if (time == 0)
                {
                    time = 30;//默认显示30分钟
                }

                m_Appointment.taskTime = time;
                m_Appointment.EndDate = item.time.AddMinutes(time);

                taskname = GetZhuangbiStr(taskname);
                if (common != null && common != "")
                {
                    common = GetZhuangbiStr(common);
                }
                if (showfatchertimeblock && item.mindmap == "TimeBlock")
                {
                    m_Appointment.Title = item.nameFull + (item.nameFull != null && item.nameFull != "" ? "|" : "") + taskname;
                }
                else
                {
                    m_Appointment.Title = taskname;
                }
                m_Appointment.Comment = common;
                m_Appointment.DetailComment = detailCommon;
                if (showcomment && m_Appointment.Comment != null && m_Appointment.Comment != "")
                {
                    m_Appointment.Title += ("(" + m_Appointment.Comment + ")");
                }
                if (showcomment && m_Appointment.DetailComment != null && m_Appointment.DetailComment != "")
                {
                    m_Appointment.Title += "*";
                }
                m_Appointment.value = item.mindmapPath;
                m_Appointment.ID = item.ID != null ? item.ID.ToString() : "";
                int zhongyao = item.tasklevel;
                if (setting.numericUpDown2.Value > 0)
                {
                    if (zhongyao < setting.numericUpDown2.Value)
                    {
                        continue;
                    }
                }
                else if (setting.numericUpDown2.Value < 0)
                {
                    if (zhongyao > setting.numericUpDown2.Value)
                    {
                        continue;
                    }
                }
                m_Appointment.Color = System.Drawing.Color.White;
                m_Appointment.BorderColor = System.Drawing.Color.White;
                if (item.mindmap == "TimeBlock" || item.mindmap == "Mistake" || item.mindmap == "Progress" || item.mindmap == "Money" || item.mindmap == "KA")
                {
                    try
                    {
                        if (!setting.TimeBlockColor.Checked)
                        {
                            m_Appointment.Color = Color.FromArgb(Int32.Parse(item.mindmapPath));
                            m_Appointment.BorderColor = Color.FromArgb(Int32.Parse(item.mindmapPath));
                        }
                        else
                        {
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
                            else if (zhongyao == -1)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(220, 220, 220);
                            }
                            else if (zhongyao == -2)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(211, 211, 211);
                            }
                            else if (zhongyao == -3)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(192, 192, 192);
                            }
                            else if (zhongyao == -4)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(169, 169, 169);
                            }
                            else if (zhongyao == -5)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(128, 128, 128);
                            }
                            else if (zhongyao == -6)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(105, 105, 105);
                            }
                            else if (zhongyao == -7)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(85, 85, 85);
                            }
                            else if (zhongyao == -8)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(65, 65, 65);
                            }
                            else if (zhongyao == -9)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(48, 48, 48);
                            }
                            else if (zhongyao <= -10)
                            {
                                m_Appointment.Color = m_Appointment.BorderColor = Color.Black;
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
                        }

                        if (item.time < DateTime.Today.AddDays(-1) && DateTime.Today.Day != 28 && DateTime.Today.Day != 29 && DateTime.Today.Day != 30 && DateTime.Today.Day != 31)//时间块禁止编辑？一天以前的禁止编辑,每个月最后几天允许编辑
                        {
                            //m_Appointment.Locked = true;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
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
                    else if (zhongyao == -1)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(220, 220, 220);
                    }
                    else if (zhongyao == -2)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(211, 211, 211);
                    }
                    else if (zhongyao == -3)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(192, 192, 192);
                    }
                    else if (zhongyao == -4)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(169, 169, 169);
                    }
                    else if (zhongyao == -5)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(128, 128, 128);
                    }
                    else if (zhongyao == -6)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(105, 105, 105);
                    }
                    else if (zhongyao == -7)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(85, 85, 85);
                    }
                    else if (zhongyao == -8)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(65, 65, 65);
                    }
                    else if (zhongyao == -9)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.FromArgb(48, 48, 48);
                    }
                    else if (zhongyao <= -10)
                    {
                        m_Appointment.Color = m_Appointment.BorderColor = Color.Black;
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

                    //如果是任务类型,时间块选择,小于当前时间的任务格式应该不同
                    if (item.mindmap != "FanQie" && item.time < DateTime.Now && setting.c_timeBlock.Checked)
                    {
                        m_Appointment.Color = System.Drawing.Color.Red;
                        m_Appointment.BorderColor = Color.FromArgb(150, 150, 150); 
                    }
                }
                switch (item.mindmap)
                {
                    case "FanQie":
                        m_Appointment.Type = "番茄钟";
                        m_Appointment.Locked = true;
                        break;

                    case "TimeBlock":
                        m_Appointment.Type = "时间块";
                        break;

                    case "Progress":
                        m_Appointment.Type = "进步";
                        break;

                    case "Mistake":
                        m_Appointment.Type = "错误";
                        break;

                    case "Money":
                        m_Appointment.Type = "金钱";
                        m_Appointment.Title += "(" + (m_Appointment.EndDate - m_Appointment.StartDate).TotalMinutes + "元)";
                        break;

                    case "KA":
                        m_Appointment.Type = "卡路里";
                        break;

                    default:
                        m_Appointment.Type = "任务";
                        break;
                }
                m_Appointment.Tag = item;
                if (!setting.subClass.Checked)
                {
                    if (item.nameFull != null && item.nameFull != "")
                    {
                        if (item.nameFull.Contains("收入"))
                        {
                            continue;
                        }
                        if (item.nameFull.Contains("消耗"))
                        {
                            continue;
                        }
                    }
                }
                if ((m_Appointment.EndDate - m_Appointment.StartDate).TotalHours > 20)//一般不会有超过1200的支出或者收入吧
                {
                    m_Appointment.EndDate = m_Appointment.StartDate.AddHours(3);
                    //m_Appointment.Color = Color.DarkRed;//使用默认颜色就可以了
                    m_Appointment.Locked = true;
                    //如何把原来的值记录到时间块上呢？
                }
                if (m_Appointment.EndDate.Day > m_Appointment.StartDate.Day)
                {
                    m_Appointment.StartDate = Convert.ToDateTime(m_Appointment.StartDate.ToString("yyyy/MM/dd"));
                    m_Appointment.EndDate = m_Appointment.StartDate.AddHours(3);
                    //m_Appointment.Color = Color.DarkRed;//使用默认颜色就可以了
                    m_Appointment.Locked = true;
                }
                m_Appointments.Add(m_Appointment);
            }
            try
            {
                if (setting.checkBox_jinian.Checked)
                {
                    ShowJinian();
                }
                if (setting.checkBox_enddate.Checked)
                {
                    ShowEnd();
                }
            }
            catch (Exception ex)
            {
            }
            dayView1.Refresh();
        }

        public void ShowActionLog()
        {
            GetLog();
        }

        public void GetLog()
        {
            string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            GetFileLog(fileName);
            try
            {
                foreach (string item in System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + @"\log"))
                {
                    if (item.EndsWith("txt"))
                    {
                        GetFileLog(item);
                    }
                }
            }
            catch (Exception ex)
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
                    DateTime dt = DateTime.Now.AddYears(-5);
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Length > 10 && !line.Contains("程序开启"))
                        {
                            if (line.StartsWith("***"))
                            {
                                line = DecryptStringlog(line);
                            }
                            if (line.Length > 21)
                            {
                                string timeString = line.Substring(0, 21);
                                try
                                {
                                    dt = Convert.ToDateTime(timeString.Trim());
                                }
                                catch (Exception ex)
                                {
                                }
                                ActionLogAddCalanderItem(line, "", line, dt, fileName);
                            }
                        }
                    }
                }
            }
        }

        #region 数据加密解密

        public const string initVector = "yixiaoziyixiaozi";
        public const int keysize = 256;
        public static string logpass = "niqishihenhao";

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
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion 数据加密解密

        public void ShowCaptureScreen()
        {
            //string Year = dayView1.StartDate.Year.ToString();
            //string YearTo = dayView1.StartDate.AddDays(dayView1.DaysToShow).Month.ToString();
            //string Month = dayView1.StartDate.Month.ToString();
            //string MonthTo = dayView1.StartDate.AddDays(dayView1.DaysToShow).Month.ToString();
            //m_Appointments = new List<Appointment>();//清空
            Appointment m_Appointment = new Appointment();
            foreach (FileInfo file in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles("*.*", SearchOption.AllDirectories).Where(file => file.Name.ToLower().EndsWith(".png") || file.Name.ToLower().EndsWith(".jpg") || file.Name.ToLower().EndsWith(".setting.HTML") || file.Name.ToLower().EndsWith(".txt")).ToList())
            {
                if ((setting.CaptureScreen.Checked && file.FullName.Contains("setting.CaptureScreen")) || (setting.CameracheckBox.Checked && file.FullName.Contains("Camera")) || (setting.JieTucheckBox.Checked && file.FullName.Contains("images")) || (setting.HTML.Checked && file.FullName.Contains("setting.HTML")) || (setting.ShowClipboard.Checked && file.FullName.EndsWith(".txt")))
                {
                    //string minute = file.Name.Substring(8, 2);
                    //if (!"00,15,30,45".Contains(minute))
                    //{
                    //    file.Delete();
                    //}

                    DateTime dt = ConverStringToDate(file.Name);

                    if (file.FullName.EndsWith(".txt"))//剪切板文件
                    {
                        //.*\d.\\\d.\d*\.txt
                        string patten = @".*\d.\\\d.\d*\.txt";
                        Regex reg = new Regex(patten);
                        if (!reg.IsMatch(file.FullName))//判断是不是剪切板文件
                        {
                            continue;
                        }
                        else
                        {
                            try
                            {
                                string[] split = file.FullName.Split('\\');
                                dt = Convert.ToDateTime(split[split.Length - 3] + "/" + split[split.Length - 2] + "/" + split[split.Length - 1].Split('.')[0] + " 1:1");
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }
                    else if (!file.FullName.Contains("setting.HTML"))
                    {
                        if (dt == DateTime.Today && file.Name.Substring(6, 2) != "00")
                        {
                            continue;
                        }
                    }
                    else
                    {
                        dt = file.CreationTime;
                    }
                    //if (file.FullName.Contains("images"))不清楚怎么回事，有的截图时间没有问题，有的不行
                    //{
                    //    dt = dt.AddHours(8);
                    //}
                    if (dt > dayView1.StartDate && dt < dayView1.StartDate.AddDays(dayView1.DaysToShow))
                    {
                        if (file.FullName.EndsWith(".txt"))//剪切板文件，要显示里面的内容，所以单独分支
                        {
                            ShowClipboardOfTxt(file.FullName);
                        }
                        else
                        {
                            m_Appointment = new Appointment
                            {
                                StartDate = dt
                            };
                            m_Appointment.taskTime = 15;
                            m_Appointment.EndDate = dt.AddMinutes(15);
                            m_Appointment.Title = dt.ToString("HH:mm");
                            //m_Appointment.Comment = common;
                            //m_Appointment.DetailComment = detailCommon;
                            m_Appointment.value = file.FullName;
                            m_Appointment.ID = file.FullName;
                            if (file.FullName.Contains("setting.CaptureScreen"))
                            {
                                m_Appointment.Color = System.Drawing.Color.White;
                                m_Appointment.BorderColor = System.Drawing.Color.White;
                            }
                            else if (file.FullName.Contains("Camera"))
                            {
                                m_Appointment.Color = System.Drawing.Color.Yellow;
                                m_Appointment.BorderColor = System.Drawing.Color.Yellow;
                            }
                            else if (file.FullName.Contains("images"))
                            {
                                m_Appointment.Color = System.Drawing.Color.Green;
                                m_Appointment.BorderColor = System.Drawing.Color.Green;
                            }
                            else if (file.FullName.Contains("setting.HTML"))
                            {
                                m_Appointment.Color = System.Drawing.Color.BlueViolet;
                                m_Appointment.BorderColor = System.Drawing.Color.BlueViolet;
                                m_Appointment.Title = file.Name;
                                m_Appointment.taskTime = 60;
                                m_Appointment.EndDate = dt.AddMinutes(60);
                            }
                            m_Appointment.Type = "屏幕截图";
                            m_Appointment.Locked = true;
                            //m_Appointment.Tag = item;
                            m_Appointments.Add(m_Appointment);
                        }
                    }
                }
            }
            dayView1.Refresh();
        }

        public void ShowClipboardOfTxt(string fileName)
        {
            if (fileName.Contains("File") || fileName.Contains("key"))
            {
                return;
            }
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(fileName))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    string time = "";//时间判断，如果和timenew不一致，说明是下一个记录
                    string timenew = "";//新的时间，每次更新
                    string Content = "";
                    DateTime dt = DateTime.Now;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Trim() == "")
                        {
                            continue;
                        }
                        try
                        {
                            if (line.StartsWith("20"))
                            {
                                timenew = line.Substring(0, 20);
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            dt = Convert.ToDateTime(timenew.Trim());
                            if (timenew != time)
                            {
                                time = timenew;//更新time
                                               //使用Content添加一条，因为这是新的一条了，要把Content内容保存。
                                ClipboardAddCalanderItem(Content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0], Content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0], Content, dt, fileName);
                                Content = line.Substring(20);
                            }
                            else//如果一样的话
                            {
                                Content += (Environment.NewLine + line);
                            }
                            //result r = new result { words = (!line.StartsWith("20") ? time : "") + line, path = fileName, Time = dt };
                        }
                        catch (Exception ex)//如果不是时间
                        {
                            Content += (Environment.NewLine + line);
                        }
                    }
                    //添加最后一条
                    ClipboardAddCalanderItem(Content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0], Content.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[0], Content, dt, fileName);
                }
            }
        }

        public void ClipboardAddCalanderItem(string Title, string Common, String CommonDetail, DateTime dt, string fullname)
        {
            Appointment m_Appointment = new Appointment
            {
                StartDate = dt
            };
            m_Appointment.taskTime = 15;
            m_Appointment.EndDate = dt.AddMinutes(15);
            m_Appointment.Title = GetZhuangbiStr(Title);
            m_Appointment.Comment = Common;
            m_Appointment.DetailComment = CommonDetail;
            m_Appointment.value = fullname;
            m_Appointment.ID = "";
            m_Appointment.Color = System.Drawing.Color.Orange;
            m_Appointment.BorderColor = System.Drawing.Color.Orange;
            m_Appointment.Type = "剪切板";
            m_Appointment.Locked = true;
            m_Appointment.Tag = null;
            m_Appointments.Add(m_Appointment);
        }

        public void ActionLogAddCalanderItem(string Title, string Common, String CommonDetail, DateTime dt, string fullname)
        {
            Appointment m_Appointment = new Appointment
            {
                StartDate = dt
            };
            m_Appointment.taskTime = 15;
            m_Appointment.EndDate = dt.AddMinutes(15);
            m_Appointment.Title = GetZhuangbiStr(Title);
            m_Appointment.Comment = Common;
            m_Appointment.DetailComment = CommonDetail;
            m_Appointment.value = fullname;
            m_Appointment.ID = "";
            m_Appointment.Color = System.Drawing.Color.Orange;
            m_Appointment.BorderColor = System.Drawing.Color.Orange;
            m_Appointment.Type = "操作日志";
            m_Appointment.Locked = true;
            m_Appointment.Tag = null;
            m_Appointments.Add(m_Appointment);
        }

        public void ShowAllNodes()
        {
            //string Year = dayView1.StartDate.Year.ToString();
            //string YearTo = dayView1.StartDate.AddDays(dayView1.DaysToShow).Month.ToString();
            //string Month = dayView1.StartDate.Month.ToString();
            //string MonthTo = dayView1.StartDate.AddDays(dayView1.DaysToShow).Month.ToString();
            //m_Appointments = new List<Appointment>();//清空
            Appointment m_Appointment = new Appointment();
            foreach (node nodeitem in DocearReminderForm.nodes.Where(m => m.Time > dayView1.StartDate && m.Time > dayView1.StartDate && m.Time < dayView1.StartDate.AddDays(dayView1.DaysToShow)))
            {
                m_Appointment = new Appointment
                {
                    StartDate = nodeitem.Time
                };
                m_Appointment.taskTime = 15;
                m_Appointment.EndDate = nodeitem.Time.AddMinutes(15);
                m_Appointment.Title = GetZhuangbiStr(nodeitem.Text);
                m_Appointment.Comment = nodeitem.mindmapName;
                m_Appointment.DetailComment = nodeitem.ParentNodePath;
                m_Appointment.value = nodeitem.mindmapPath;
                m_Appointment.ID = nodeitem.IDinXML;
                m_Appointment.Color = System.Drawing.Color.White;
                m_Appointment.BorderColor = System.Drawing.Color.White;
                m_Appointment.Type = "导图节点";
                m_Appointment.Locked = true;
                m_Appointment.Tag = nodeitem;
                m_Appointments.Add(m_Appointment);
            }
            dayView1.Refresh();
        }

        public DateTime ConverStringToDate(string str)
        {
            try
            {
                string year = "20" + str.Substring(0, 2);
                string month = str.Substring(2, 2);
                string day = str.Substring(4, 2);
                string hour = str.Substring(6, 2);
                string minute = str.Substring(8, 2);
                if (str.Contains("-"))
                {
                    year = str.Substring(0, 4);
                    month = str.Substring(5, 2);
                    day = str.Substring(8, 2);
                    hour = str.Substring(11, 2);
                    minute = str.Substring(14, 2);
                }
                return Convert.ToDateTime(year + "/" + month + "/" + day + " " + hour + ":" + minute);
            }
            catch (Exception ex)
            {
                return DateTime.Today;
            }
        }

        public string GetZhuangbiStr(string str)
        {
            if (isZhuangbi)
            {
                string patten = @"(\S)";
                Regex reg = new Regex(patten);
                str = reg.Replace(str, "*");
            }
            return str;
        }

        public void ShowEnd()
        {
            //m_Appointments = new List<Appointment>();
            Appointment m_Appointment = new Appointment();
            IEnumerable<ReminderItem> items = reminderObject.reminders.Where(m => ((!m.isCompleted) && m.EndDate != null));

            foreach (ReminderItem item in items)
            {
                m_Appointment = new Appointment
                {
                    StartDate = (DateTime)item.EndDate
                };
                string taskname = item.name;
                string common = item.comment;
                string detailCommon = item.DetailComment;
                double time = 60;

                m_Appointment.taskTime = time;
                m_Appointment.EndDate = m_Appointment.StartDate.AddMinutes(time);

                taskname = GetZhuangbiStr(taskname);
                if (common != null && common != "")
                {
                    common = GetZhuangbiStr(common);
                }
                m_Appointment.Comment = common;
                m_Appointment.DetailComment = detailCommon;
                if (showcomment && m_Appointment.Comment != null && m_Appointment.Comment != "")
                {
                    m_Appointment.Title += ("(" + m_Appointment.Comment + ")");
                    if (m_Appointment.DetailComment != null && m_Appointment.DetailComment != "")
                    {
                        m_Appointment.Title += "*";
                    }
                }
                m_Appointment.Title = "截止日：" + taskname;
                m_Appointment.value = item.mindmapPath;
                m_Appointment.ID = item.ID != null ? item.ID.ToString() : "";
                int zhongyao = item.tasklevel;
                if (setting.numericUpDown2.Value >= 0)
                {
                    if (zhongyao < setting.numericUpDown2.Value)
                    {
                        continue;
                    }
                }
                else
                {
                    if (zhongyao > setting.numericUpDown2.Value)
                    {
                        continue;
                    }
                }
                m_Appointment.Color = System.Drawing.Color.Red;
                m_Appointment.BorderColor = System.Drawing.Color.Red;
                m_Appointment.Type = "结束日";
                m_Appointment.Locked = true;
                m_Appointment.Tag = item;
                m_Appointments.Add(m_Appointment);
            }
            //dayView1.Refresh();
        }

        public void ShowJinian()
        {
            //m_Appointments = new List<Appointment>();
            Appointment m_Appointment = new Appointment();
            IEnumerable<ReminderItem> items = reminderObject.reminders.Where(m => ((!m.isCompleted) && m.JinianDate != null));

            foreach (ReminderItem item in items)
            {
                m_Appointment = new Appointment
                {
                    StartDate = (DateTime)item.JinianDate
                };
                while ((DateTime.Now - m_Appointment.StartDate).TotalDays > 30)
                {
                    m_Appointment.StartDate = m_Appointment.StartDate.AddYears(1);
                }
                string taskname = item.name;
                string common = item.comment;
                string detailCommon = item.DetailComment;
                double time = 60;

                m_Appointment.taskTime = time;
                m_Appointment.EndDate = m_Appointment.StartDate.AddMinutes(time);

                taskname = GetZhuangbiStr(taskname);
                if (common != null && common != "")
                {
                    common = GetZhuangbiStr(common);
                }
                m_Appointment.Title = "纪念日：" + taskname;
                m_Appointment.Comment = common;
                m_Appointment.DetailComment = detailCommon;
                if (showcomment && m_Appointment.Comment != null && m_Appointment.Comment != "")
                {
                    m_Appointment.Title += ("(" + m_Appointment.Comment + ")");
                    if (m_Appointment.DetailComment != null && m_Appointment.DetailComment != "")
                    {
                        m_Appointment.Title += "*";
                    }
                }
                m_Appointment.value = item.mindmapPath;
                m_Appointment.ID = item.ID != null ? item.ID.ToString() : "";
                int zhongyao = item.tasklevel;
                if (setting.numericUpDown2.Value >= 0)
                {
                    if (zhongyao < setting.numericUpDown2.Value)
                    {
                        continue;
                    }
                }
                else
                {
                    if (zhongyao > setting.numericUpDown2.Value)
                    {
                        continue;
                    }
                }
                m_Appointment.Color = System.Drawing.Color.Red;
                m_Appointment.BorderColor = System.Drawing.Color.Red;
                m_Appointment.Type = "纪念日";
                m_Appointment.Locked = true;
                m_Appointment.Tag = item;
                m_Appointments.Add(m_Appointment);
            }
            //dayView1.Refresh();
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

        public void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (setting.CaptureScreen.Checked || setting.JieTucheckBox.Checked || setting.CameracheckBox.Checked || setting.HTML.Checked || setting.ShowNodes.Checked || setting.AllFile.Checked || setting.ShowClipboard.Checked)
                {
                    //当在查看文件时，不更新，因为需要计算量比较大（需要读取磁盘）
                    return;
                }
                if (DocearReminderForm.section != setting.workfolder_combox.SelectedItem.ToString() && setting.workfolder_combox.SelectedItem.ToString() != "All")
                {
                    for (int i = 0; i < setting.workfolder_combox.Items.Count; i++)
                    {
                        if (setting.workfolder_combox.Items[i].ToString() == DocearReminderForm.section)
                        {
                            setting.workfolder_combox.SelectedIndex = i;
                        }
                    }
                }
                else
                {
                    if (!isMenuShow)
                    {
                        FreshMenu();
                        RefreshCalender();
                    }
                }
            }
            catch (Exception ex)
            {
            }
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

        public void 截图_Click(object sender, EventArgs e)
        {
            jietu();
        }

        public void Timer2_Tick(object sender, EventArgs e)
        {
            //jietu();
        }

        public void CalendarForm_KeyUp(object sender, KeyEventArgs e)
        {
            //if (!Console.CapsLock && !islock)
            if (!islock)
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
                            if (setting.workfolder_combox.SelectedIndex > 0)
                            {
                                setting.workfolder_combox.SelectedIndex--;
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
                            if (setting.workfolder_combox.SelectedIndex < setting.workfolder_combox.Items.Count - 1)
                            {
                                setting.workfolder_combox.SelectedIndex++;
                            }
                        }
                        break;

                    case Keys.Left:
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (setting.numericUpDown1.Value == 7 && setting.dTPicker_StartDay.Value.DayOfWeek == DayOfWeek.Monday)
                            {
                                setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(-7);
                            }
                            else if (setting.numericUpDown1.Value == 14)
                            {
                                setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(-14);
                            }
                            else
                            {
                                setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(-1);
                            }
                        }
                        break;

                    case Keys.Right:
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (setting.numericUpDown1.Value == 7)
                            {
                                setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(7);
                            }
                            else if (setting.numericUpDown1.Value == 14)
                            {
                                setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(14);
                            }
                            else
                            {
                                setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(1);
                            }
                        }
                        break;

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
                            if (setting.workfolder_combox.SelectedIndex > 0)
                            {
                                setting.workfolder_combox.SelectedIndex--;
                            }
                        }
                        else
                        {
                            if (setting.numericUpDown1.Value < 14 && !setting.c_lock.Checked)
                            {
                                setting.numericUpDown1.Value++;
                                return;
                            }
                            if (dayView1.Focused && (dayView1.SelectedAppointment != null))
                            {
                                dayView1.SelectedAppointment.StartDate = dayView1.SelectedAppointment.StartDate.AddMinutes(-1);
                                dayView1.SelectedAppointment.EndDate = dayView1.SelectedAppointment.EndDate.AddMinutes(-1);
                                ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(dayView1.SelectedAppointment.value) && m.ID == dayView1.SelectedAppointment.ID);
                                if (current != null)
                                {
                                    current.time = dayView1.SelectedAppointment.StartDate;
                                    current.tasktime = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes;
                                }
                                dayView1.Refresh();
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
                            if (setting.workfolder_combox.SelectedIndex < setting.workfolder_combox.Items.Count - 1)
                            {
                                setting.workfolder_combox.SelectedIndex++;
                            }
                        }
                        else
                        {
                            if (setting.numericUpDown1.Value >= 2 && !setting.c_lock.Checked)
                            {
                                setting.numericUpDown1.Value--;
                                return;
                            }
                            if (dayView1.Focused && (dayView1.SelectedAppointment != null))
                            {
                                dayView1.SelectedAppointment.StartDate = dayView1.SelectedAppointment.StartDate.AddMinutes(1);
                                dayView1.SelectedAppointment.EndDate = dayView1.SelectedAppointment.EndDate.AddMinutes(1);
                                ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(dayView1.SelectedAppointment.value) && m.ID == dayView1.SelectedAppointment.ID);
                                if (current != null)
                                {
                                    current.time = dayView1.SelectedAppointment.StartDate;
                                    current.tasktime = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes;
                                }
                                dayView1.Refresh();
                            }
                        }
                        break;

                    case Keys.Left:
                        if (setting.c_lock.Checked)
                        {
                            if (dayView1.Focused && (dayView1.SelectedAppointment != null))
                            {
                                if ((dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes > 3)
                                {
                                    dayView1.SelectedAppointment.EndDate = dayView1.SelectedAppointment.EndDate.AddMinutes(-1);
                                    ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(dayView1.SelectedAppointment.value) && m.ID == dayView1.SelectedAppointment.ID);
                                    if (current != null)
                                    {
                                        current.time = dayView1.SelectedAppointment.StartDate;
                                        current.tasktime = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes;
                                    }
                                    dayView1.Refresh();
                                }
                            }
                            return;
                        }
                        if (setting.numericUpDown1.Value == 7 && setting.dTPicker_StartDay.Value.DayOfWeek == DayOfWeek.Monday)
                        {
                            setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(-7);
                        }
                        else if (setting.numericUpDown1.Value == 14)
                        {
                            setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(-14);
                        }
                        else
                        {
                            setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(-1);
                        }

                        break;

                    case Keys.Right:
                        if (setting.c_lock.Checked)
                        {
                            if (dayView1.Focused && (dayView1.SelectedAppointment != null))
                            {
                                dayView1.SelectedAppointment.EndDate = dayView1.SelectedAppointment.EndDate.AddMinutes(1);
                                ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(dayView1.SelectedAppointment.value) && m.ID == dayView1.SelectedAppointment.ID);
                                if (current != null)
                                {
                                    current.time = dayView1.SelectedAppointment.StartDate;
                                    current.tasktime = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes;
                                }
                                dayView1.Refresh();
                            }
                            return;
                        }
                        if (setting.numericUpDown1.Value == 7)
                        {
                            setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(7);
                        }
                        else if (setting.numericUpDown1.Value == 14)
                        {
                            setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(14);
                        }
                        else
                        {
                            setting.dTPicker_StartDay.Value = setting.dTPicker_StartDay.Value.AddDays(1);
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
                            setting.c_fanqie.Checked = !setting.c_fanqie.Checked;
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
                            setting.c_timeBlock.Checked = !setting.c_timeBlock.Checked;
                        }
                        break;

                    case Keys.Q:
                        this.Close();
                        break;

                    case Keys.PageUp:
                        if (setting.numericUpDown2.Value <= 99)
                        {
                            setting.numericUpDown2.Value++;
                        }
                        break;

                    case Keys.PageDown:
                        if (setting.numericUpDown2.Value >= 0)
                        {
                            setting.numericUpDown2.Value--;
                        }
                        break;

                    case Keys.Home:
                        setting.dTPicker_StartDay.Value = DateTime.Today;
                        break;

                    case Keys.I:
                        setting.textBox_searchwork.Focus();
                        break;

                    case Keys.R:
                        RefreshCalender();
                        break;

                    case Keys.Escape:
                        if (dayView1.Focused)
                        {
                            this.WindowState= FormWindowState.Minimized;
                            this.Hide();
                        }
                        else
                        {
                            dayView1.Focus();
                        }
                        break;

                    case Keys.Delete:
                        toolStripMenuItem2_Click(null, null);
                        break;

                    case Keys.CapsLock:
                        setting.c_lock.Checked = !setting.c_lock.Checked;
                        break;
                    case Keys.Oem3:
                        setting.Show();
                        break;

                    default:
                        break;
                }
                dayView1.AllowInplaceEditing = false;
                dayView1.AllowNew = false;
            }
        }

        public void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            dayView1.StartDate = dayView1.StartDate;//用于刷新
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setting.workfolder_combox.SelectedItem.ToString() == "All")
            {
                mindmappath = "";
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//用于刷新
            }
            else if (setting.workfolder_combox.SelectedItem.ToString() == "rootPath")
            {
                mindmappath = ini.ReadString("path", "rootPath", ""); ;
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//用于刷新
            }
            else
            {
                mindmappath = ini.ReadString("path", setting.workfolder_combox.SelectedItem.ToString(), "");
                RefreshCalender();
                dayView1.StartDate = dayView1.StartDate;//用于刷新
            }
            UsedLogRenew();
        }

        public void dayView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                try
                {
                    this.Text = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes.ToString("N") + "分钟" + "|" + dayView1.SelectedAppointment.Title;
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void dayView1_AppoinmentMove(object sender, AppointmentEventArgs e)
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
                            if (item.Name == "hook" && !isHashook && item.Attributes != null && item.Attributes["NAME"].Value== "plugins/TimeManagementReminder.xml")
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
                        XmlAttribute TASKTIME = x.CreateAttribute("TASKTIME");
                        node.Attributes.Append(TASKTIME);
                        node.Attributes["TASKTIME"].Value = ((int)tasktime).ToString();
                        node.Attributes["TEXT"].Value = TaskName;
                        x.Save(path);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                        th.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
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
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        //修改时间
        public void dayView1_MouseUp(object sender, MouseEventArgs e)
        {
            toolTip2.Hide(this);
            //if (logfile.Contains("fanqie"))
            //{
            //    return;
            //}
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
                    if (dayView1.SelectedAppointment.Type != "时间块" && dayView1.SelectedAppointment.Type != "进步" && dayView1.SelectedAppointment.Type != "错误" && dayView1.SelectedAppointment.Type != "金钱" & dayView1.SelectedAppointment.Type != "卡路里")
                    {
                        EditTask(dayView1.SelectedAppointment.value, dayView1.SelectedAppointment.ID, dayView1.SelectedAppointment.StartDate, (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes, dayView1.SelectedAppointment.Title);
                    }
                    ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(dayView1.SelectedAppointment.value) && m.ID == dayView1.SelectedAppointment.ID);
                    if (current != null)
                    {
                        current.time = dayView1.SelectedAppointment.StartDate;
                        current.tasktime = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes;
                        current.name = dayView1.SelectedAppointment.Title.Split('(')[0];
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (app != null)
            {
                ismovetask = false;
                try
                {
                    if (!setting.c_timeBlock.Checked)
                    {
                        EditTask(app.value, app.ID, app.StartDate, (app.EndDate - app.StartDate).TotalMinutes, app.Title);
                    }
                    ReminderItem current = reminderObject.reminders.FirstOrDefault(m => !m.isCompleted && m.mindmapPath.Contains(app.value) && m.ID == app.ID);
                    if (current != null)
                    {
                        current.time = app.StartDate;
                        current.tasktime = (app.EndDate - app.StartDate).TotalMinutes;
                        current.name = app.Title.Split('(')[0]; ;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void TextBox_searchwork_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    RefreshCalender();
                    UsedLogRenew();
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

        public void CalendarWhell(object sender, MouseEventArgs e)
        {
            try
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (setting.numericUpDown3.Value == 20)
                    {
                        dayView1.StartHour = 0;
                    }
                    if (e.Delta > 0)
                    {
                        setting.numericUpDown3.Value += 2;
                    }
                    else
                    {
                        if (setting.numericUpDown3.Value > 20)
                        {
                            setting.numericUpDown3.Value -= 2;
                        }
                    }
                }
                else
                {
                    if (setting.c_lock.Checked)
                    {
                        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                        {
                            if (setting.numericUpDown3.Value == 20)
                            {
                                return;
                            }
                            if (e.Delta > 0)
                            {
                                if (dayView1.StartHour < 18)
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
                    }
                    else
                    {
                        if (setting.numericUpDown3.Value == 20)
                        {
                            return;
                        }
                        if (e.Delta > 0)
                        {
                            if (dayView1.StartHour < 18)
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
                }
                dayView1.Refresh();
            }
            catch (Exception ex)
            {
                dayView1.Refresh();
            }
        }

        public void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        public void dayView1_DoubleClick(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                ismovetask = false;
                try
                {
                    System.Diagnostics.Process.Start(dayView1.SelectedAppointment.value);
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (dayView1.SelectedAppointment.Type == "时间块" || dayView1.SelectedAppointment.Type == "进步" || dayView1.SelectedAppointment.Type == "错误" || dayView1.SelectedAppointment.Type == "金钱")
                        {
                            commentToolStripMenuItem_Click(null, null);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void numericOpacity_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = Convert.ToDouble(setting.numericOpacity.Value / 100);
        }

        public void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            UsedLogRenew();
        }

        public void dayView1_NewAppointment_1(object sender, NewAppointmentEventArgs args)
        {
        }

        public bool islock = true;
        public bool showfatchertimeblock;
        public bool showcomment = true;
        public int lastX;
        public int lastY;
        public bool nomoretooltip;

        public void lockButton_Click(object sender, EventArgs e)
        {
            if (setting.lockButton.Text == "锁定")
            {
                setting.lockButton.Text = "解锁";
                islock = true;
            }
            else
            {
                setting.lockButton.Text = "锁定";
                islock = false;
            }
        }

        public void SetTimeBlock(object sender, EventArgs e)
        {
            Appointment m_Appointment = new Appointment();
            if ((dayView1.SelectionEnd - dayView1.SelectionStart).TotalMinutes > 2 && (dayView1.SelectionEnd - dayView1.SelectionStart).TotalMinutes < 1000)
            {
                m_Appointment.StartDate = dayView1.SelectionStart;
                m_Appointment.EndDate = dayView1.SelectionEnd;
            }
            else if (dayView1.SelectedAppointment != null && dayView1.SelectedAppointment.Type == "时间块")//方便设置时间块并行
            {
                m_Appointment.StartDate = dayView1.SelectedAppointment.StartDate;
                m_Appointment.EndDate = dayView1.SelectedAppointment.EndDate;
            }
            else
            {
                ReminderItem item = reminderObject.reminders.Where(m =>m.time <= dayView1.SelectionStart && (m.mindmap == "TimeBlock" || (m.mindmap == "FanQie" && m.name.Length != 5)) && m.isCompleted == false).OrderBy(m => m.TimeEnd).LastOrDefault();
                if (item != null)
                {
                    m_Appointment.StartDate = item.TimeEnd;
                    if (m_Appointment.StartDate < DateTime.Today && dayView1.SelectionEnd > DateTime.Today)//如果今天还没有过，处理开始时间为今天
                    {
                        m_Appointment.StartDate = DateTime.Today;
                    }
                }
                else
                {
                    //return;
                    m_Appointment.StartDate = DateTime.Today;
                }
                if (dayView1.SelectionEnd < DateTime.Now)
                {
                    if (dayView1.SelectionEnd < m_Appointment.StartDate)
                    {
                        m_Appointment.EndDate = DateTime.Now;
                    }
                    else
                    {
                        m_Appointment.EndDate = dayView1.SelectionEnd;
                    }
                }
                else
                {
                    m_Appointment.EndDate = DateTime.Now;
                }
            }
            dayView1.SelectionStart = dayView1.SelectionEnd = DateTime.Now;//清空选择
            m_Appointment.Title = ((System.Windows.Forms.ToolStripItem)sender).Text;
            m_Appointment.value = ((System.Windows.Forms.ToolStripItem)sender).BackColor.ToArgb().ToString();
            m_Appointment.ID = Guid.NewGuid().ToString();
            m_Appointment.Type = "TimeBlock";
            string comment = "";
            string commentDetail = "";
            string fanqieid = "";
            if (dayView1.SelectedAppointment != null && dayView1.SelectedAppointment.Type == "番茄钟")
            {
                m_Appointment.StartDate = dayView1.SelectedAppointment.StartDate;
                m_Appointment.EndDate = dayView1.SelectedAppointment.EndDate;
                if (dayView1.SelectedAppointment.Title.Contains("|"))
                {
                    m_Appointment.Comment = dayView1.SelectedAppointment.Title.Split('|')[0];
                    m_Appointment.DetailComment = dayView1.SelectedAppointment.Title.Split('|')[1];
                    comment = dayView1.SelectedAppointment.Title.Split('|')[0];
                    commentDetail = dayView1.SelectedAppointment.Title.Split('|')[1];
                }
                else
                {
                    if (dayView1.SelectedAppointment.DetailComment != null && dayView1.SelectedAppointment.DetailComment != "")
                    {
                        m_Appointment.Comment = dayView1.SelectedAppointment.Title;
                        m_Appointment.DetailComment = dayView1.SelectedAppointment.DetailComment;
                        comment = dayView1.SelectedAppointment.Title;
                        commentDetail = dayView1.SelectedAppointment.DetailComment;
                    }
                    else
                    {
                        m_Appointment.Comment = dayView1.SelectedAppointment.Title;
                        comment = dayView1.SelectedAppointment.Title;
                    }
                }
                fanqieid = dayView1.SelectedAppointment.ID;
                m_Appointments.Remove(dayView1.SelectedAppointment);
            }

            unchecked
            {
                m_Appointment.Color = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
                m_Appointment.BorderColor = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
            }

            m_Appointments.Add(m_Appointment);
            reminderObjectJsonAdd(m_Appointment.Title, m_Appointment.ID, m_Appointment.value, (m_Appointment.EndDate - m_Appointment.StartDate).TotalMinutes, m_Appointment.StartDate, "TimeBlock", ((System.Windows.Forms.ToolStripItem)sender).Tag, comment, commentDetail);
            SetFanQieComplete(fanqieid);
            RefreshCalender();
        }

        public void SetProgress(object sender, EventArgs e)
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

        public void SetMistake(object sender, EventArgs e)
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

        public void SetMoney(object sender, EventArgs e)
        {
            Appointment m_Appointment = new Appointment();
            if ((dayView1.SelectionEnd - dayView1.SelectionStart).TotalMinutes > 2)
            {
                m_Appointment.StartDate = dayView1.SelectionStart;
                m_Appointment.EndDate = dayView1.SelectionEnd;
            }
            else
            {
                ReminderItem item = reminderObject.reminders.Where(m => m.time >= DateTime.Today.AddDays(-2) && m.time <= DateTime.Now && m.mindmap == "Money" && m.isCompleted == false).OrderBy(m => m.TimeEnd).LastOrDefault();
                if (item != null)
                {
                    m_Appointment.StartDate = item.time.AddMinutes(item.tasktime);
                }
                else
                {
                    //return;
                    m_Appointment.StartDate = DateTime.Today;
                }
                if (dayView1.SelectionEnd < DateTime.Now)
                {
                    m_Appointment.EndDate = dayView1.SelectionEnd;
                }
                else
                {
                    m_Appointment.EndDate = DateTime.Now;
                }
            }
            m_Appointment.Title = ((System.Windows.Forms.ToolStripItem)sender).Text;
            m_Appointment.value = ((System.Windows.Forms.ToolStripItem)sender).BackColor.ToArgb().ToString();
            m_Appointment.ID = Guid.NewGuid().ToString();
            m_Appointment.Type = "Money";
            string comment = "";
            unchecked
            {
                m_Appointment.Color = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
                m_Appointment.BorderColor = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
            }

            m_Appointments.Add(m_Appointment);
            dayView1.Refresh();
            reminderObjectJsonAdd(m_Appointment.Title, m_Appointment.ID, m_Appointment.value, (m_Appointment.EndDate - m_Appointment.StartDate).TotalMinutes, m_Appointment.StartDate, "Money", ((System.Windows.Forms.ToolStripItem)sender).Tag, comment);
        }

        /// <summary>
        /// 设置卡路里
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetKA(object sender, EventArgs e)
        {
            Appointment m_Appointment = new Appointment();
            m_Appointment = new Appointment
            {
                StartDate = dayView1.SelectionStart
            };
            m_Appointment.EndDate = dayView1.SelectionEnd;
            string Title = ((System.Windows.Forms.ToolStripItem)sender).Text;
            //Title = Title.Replace("：",":");
            string nengliang = "";
            double nengliangNum = 0;
            string ggg = "";
            double nenglaingMax = 0;
            try
            {
                if (Title.Contains('|'))
                {
                    nengliang = Title.Split('|')[1];
                    ggg = Title.Split('|')[2];
                    Title = Title.Split('|')[0];
                }
                if (nengliang.Contains("kj"))
                {
                    nengliangNum = Convert.ToInt16(nengliang.Replace("kj", "")) / 4.184;
                }
                else
                {
                    nengliangNum = Convert.ToInt16(nengliang.Replace("kcal", ""));
                }
                nenglaingMax = nengliangNum * Convert.ToDouble(ggg) / 10;
            }
            catch (Exception ex)
            {
            }

            if (nenglaingMax != 0)
            {
                m_Appointment.EndDate = dayView1.SelectionStart.AddMinutes(nenglaingMax);
            }

            m_Appointment.Title = Title;
            m_Appointment.value = ((System.Windows.Forms.ToolStripItem)sender).BackColor.ToArgb().ToString();
            m_Appointment.ID = Guid.NewGuid().ToString();
            m_Appointment.Type = "卡路里";
            string comment = "";
            unchecked
            {
                m_Appointment.Color = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
                m_Appointment.BorderColor = ((System.Windows.Forms.ToolStripItem)sender).BackColor;
            }

            m_Appointments.Add(m_Appointment);
            dayView1.Refresh();
            reminderObjectJsonAdd(m_Appointment.Title, m_Appointment.ID, m_Appointment.value, (m_Appointment.EndDate - m_Appointment.StartDate).TotalMinutes, m_Appointment.StartDate, "KA", ((System.Windows.Forms.ToolStripItem)sender).Tag, comment);
        }

        public void SetTimeBlockColor(object sender, EventArgs e)
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
                if (node.ParentNode.ParentNode != null)
                {
                    foreach (XmlNode item in node.ParentNode.ParentNode.ParentNode.ChildNodes)
                    {
                        if (item.Name == "edge")
                        {
                            return item.Attributes["COLOR"].Value;
                        }
                    }
                    if (node.ParentNode.ParentNode.ParentNode.ParentNode != null)
                    {
                        foreach (XmlNode item in node.ParentNode.ParentNode.ParentNode.ParentNode.ChildNodes)
                        {
                            if (item.Name == "edge")
                            {
                                return item.Attributes["COLOR"].Value;
                            }
                        }
                        if (node.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode != null)
                        {
                            foreach (XmlNode item in node.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ChildNodes)
                            {
                                if (item.Name == "edge")
                                {
                                    return item.Attributes["COLOR"].Value;
                                }
                            }
                            if (node.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode != null)
                            {
                                foreach (XmlNode item in node.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ChildNodes)
                                {
                                    if (item.Name == "edge")
                                    {
                                        return item.Attributes["COLOR"].Value;
                                    }
                                }
                            }
                        }
                    }
                }
                return "#ffffff";
            }
            catch (Exception ex)
            {
                return "#ffffff";
            }
        }

        public void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                reminderObject.reminders.RemoveAll(m => m.ID == dayView1.SelectedAppointment.ID);
                m_Appointments.Remove(dayView1.SelectedAppointment);
                dayView1.Refresh();
            }
            catch (Exception ex)
            {
            }
        }

        public void SetFanQieComplete(string id)
        {
            try
            {
                ReminderItem item = reminderObject.reminders.FirstOrDefault(m => m.ID == id);
                if (item != null)
                {
                    item.isCompleted = true;
                }
                m_Appointments.Remove(dayView1.SelectedAppointment);
            }
            catch
            {
                // ignored
            }
        }

        public void 打开导图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(dayView1.SelectedAppointment.value);
            }
            catch (Exception ex)
            {
            }
        }

        public void c_timeBlock_CheckedChanged(object sender, EventArgs e)
        {
            if (setting.c_timeBlock.Checked)
            {
                dayView1.StartHour = 0;
                dayView1.HalfHourHeight = 20;
                setting.c_Money.Checked = false;
                setting.Ka_c.Checked = false;
                RefreshCalender();
            }
            CheckboxAlldisabled();
            UsedLogRenew();
        }

        public void CheckboxAlldisabled()
        {
            if (!setting.c_timeBlock.Checked && !setting.c_Money.Checked && !setting.Ka_c.Checked)
            {
                dayView1.StartHour = 0;
                dayView1.HalfHourHeight = 20;
                RefreshCalender();
            }
        }

        public void SwitchdTPicker(object sender, EventArgs e)
        {
            if (setting.dTPicker_StartDay.Value == DateTime.Today)
            {
                setting.btn_SwitchdTPicker.Text = "昨天";
                setting.dTPicker_StartDay.Value = DateTime.Today.AddDays(-1);
            }
            else if (setting.dTPicker_StartDay.Value == DateTime.Today.AddDays(-1) && setting.dTPicker_StartDay.Value.DayOfWeek != DayOfWeek.Monday)
            {
                setting.btn_SwitchdTPicker.Text = "周一";
                SetMonday();
            }
            else
            {
                setting.btn_SwitchdTPicker.Text = "今天";
                setting.dTPicker_StartDay.Value = DateTime.Today;
            }
            //将按钮的文字记录到配置文件
            ini.WriteString("Calendar", "CalendarStartDate", setting.btn_SwitchdTPicker.Text);
        }
        public void SetBtnSwitchdTPickerText()
        {
            if(setting.btn_SwitchdTPicker.Text == "今天")
            {
                setting.dTPicker_StartDay.Value = DateTime.Today;
            }
            else if (setting.btn_SwitchdTPicker.Text == "昨天")
            {
                setting.dTPicker_StartDay.Value = DateTime.Today.AddDays(-1);
            }
            else
            {
                SetMonday();
            }
        }   

        public void c_fanqie_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            dayView1.StartDate = dayView1.StartDate;//用于刷新
        }

        public void dayView1_AppointmentMouseHover(object sender, yixiaozi.WinForm.Control.DayView.AppointmentMouseHoverEventArgs args)
        {
            //基本不会运行
            //toolTip1.ToolTipTitle = "任务";
            //toolTip1.Show(args.Title + Environment.NewLine + args.EndDate.ToShortDateString(), this, new System.Drawing.Point(Control.MousePosition.X+ 1, Control.MousePosition.Y + 1), int.MaxValue);
        }

        public void dayView1_AppointmentMouseLeave(object sender, yixiaozi.WinForm.Control.DayView.AppointmentMouseLeaveEventArgs args)
        {
            //toolTip1.Hide(this);
        }

        public void dayView1_AppointmentMouseMove(object sender, Appointment args, MouseEventArgs e)
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
                    if (args != null && args.Tag != null && ((ReminderItem)args.Tag).editCount > 0)
                    {
                        editinfo += Environment.NewLine;
                        editinfo += ("编辑次数:" + ((ReminderItem)args.Tag).editCount);
                        editinfo += Environment.NewLine;
                        int count = 0;
                        foreach (DateTime item in ((ReminderItem)args.Tag).editTime)
                        {
                            editinfo += (item.ToString("MM月dd-HH:mm") + ";");
                            count++;
                            if (count % 5 == 0)
                            {
                                editinfo += Environment.NewLine;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                string timeblocktop = "";
                try
                {
                    if (args.Type == "时间块" || args.Type == "金钱" || args.Type == "卡路里")
                    {
                        timeblocktop = ((ReminderItem)args.Tag).nameFull;
                        if (timeblocktop != "")
                        {
                            timeblocktop += "|";
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                string Title = "任务";
                string info = "";
                switch (args.Type)
                {
                    case "金钱":
                        info = (args.EndDate - args.StartDate).TotalMinutes.ToString() + "元";
                        break;

                    case "卡路里":
                        info = (args.EndDate - args.StartDate).TotalMinutes * 10 + "千卡";
                        info += Environment.NewLine;
                        info += ((args.EndDate - args.StartDate).TotalMinutes * 10 / 9.46).ToString("F") + "克脂肪";
                        break;

                    default:
                        info = args.StartDate.ToShortTimeString() + "-" + args.EndDate.ToShortTimeString();
                        break;
                }
                string showresult = timeblocktop + args.Title.Split('(')[0] + Environment.NewLine + info + (args.Comment != null ? (Environment.NewLine + ToString20Lenght(args.Comment)) : "") + (args.DetailComment != null ? (Environment.NewLine + ToString20Lenght(args.DetailComment)) : "") + editinfo;
                showresult = showresult.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);//去掉连续的换行
                toolTip2.Show(showresult, dayView1, new System.Drawing.Point(Control.MousePosition.X - this.Left + 1, Control.MousePosition.Y - this.Top + 1), int.MaxValue);
            }
        }

        public void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        public string ToString20Lenght(string name)
        {
            string result = "";
            int maxLine = 5;
            foreach (string item in name.Split('\n'))
            {
                maxLine--;
                if (maxLine < 0)
                {
                    break;
                }
                for (int i = 0; i < item.Length; i++)
                {
                    if (i % 20 == 0 && i != 0)
                    {
                        result += item[i];
                        result += Environment.NewLine;
                    }
                    else
                    {
                        result += item[i];
                    }
                }
                result += Environment.NewLine;
            }
            return result;
        }

        public void toolTip2_Popup(object sender, PopupEventArgs e)
        {
        }

        public static string ShowDialog(string text, string detailCommon, string timelong, string caption)
        {
            isMenuShow = true;
            Form prompt = new Form()
            {
                Width = 420,
                Height = 480,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            RichTextBox time = new RichTextBox() { Left = 10, Top = 10, Width = 380, Height = 30 };
            time.Text = timelong;
            RichTextBox textBox = new RichTextBox() { Left = 10, Top = 50, Width = 380, Height = 30 };
            textBox.Text = text;
            RichTextBox detailCommonBox = new RichTextBox() { Left = 10, Top = 90, Width = 380, Height = 300 };
            detailCommonBox.Text = detailCommon;
            Button confirmation = new Button() { Text = "Ok", Left = 50, Width = 300, Height = 30, Top = 400, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { isMenuShow = false; prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(time);
            prompt.Controls.Add(detailCommonBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            textBox.Focus();

            return prompt.ShowDialog() == DialogResult.OK ? time.Text + "ulgniy" + textBox.Text + "ulgniy" + detailCommonBox.Text : "";
        }

        public void commentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string promptValue = "";
            if (dayView1.SelectedAppointment != null)
            {
                string timelong = "";
                try
                {
                    timelong = (dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes.ToString();
                }
                catch (Exception ex)
                {
                }
                promptValue = ShowDialog(dayView1.SelectedAppointment.Comment ?? "", dayView1.SelectedAppointment.DetailComment ?? "", timelong, dayView1.SelectedAppointment.Title.Split('(')[0] == "" ? "编辑详细" : dayView1.SelectedAppointment.Title.Split('(')[0]);
                if (promptValue != "")
                {
                    string common = Regex.Split(promptValue, @"ulgniy", RegexOptions.IgnoreCase)[1];
                    string detail = Regex.Split(promptValue, @"ulgniy", RegexOptions.IgnoreCase)[2];
                    string time = Regex.Split(promptValue, @"ulgniy", RegexOptions.IgnoreCase)[0];
                    double timeNum = 0;
                    try
                    {
                        timeNum = Convert.ToDouble(time);
                    }
                    catch (Exception ex)
                    {
                    }
                    dayView1.SelectedAppointment.Comment = common;
                    dayView1.SelectedAppointment.DetailComment = detail;
                    if (timeNum != 0)
                    {
                        dayView1.SelectedAppointment.EndDate = dayView1.SelectedAppointment.StartDate.AddMinutes(timeNum);
                    }
                    dayView1.Refresh();
                    ReminderItem current = reminderObject.reminders.FirstOrDefault(m => m.ID == dayView1.SelectedAppointment.ID);
                    if (current != null)
                    {
                        current.comment = common;
                        current.DetailComment = detail;
                        if (timeNum != 0)
                        {
                            current.tasktime = timeNum;
                        }
                    }
                    RefreshCalender();
                }
            }
        }

        public void dayView1_KeyDown(object sender, KeyEventArgs e)
        {
            toolTip2.Hide(this);
            nomoretooltip = true;
        }

        public void c_done_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            UsedLogRenew();
        }

        public void c_progress_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            UsedLogRenew();
        }

        public void c_mistake_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            UsedLogRenew();
        }

        public void 番茄钟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread th = new Thread(() => OpenFanQie(Convert.ToInt32((dayView1.SelectedAppointment.EndDate - dayView1.SelectedAppointment.StartDate).TotalMinutes), dayView1.SelectedAppointment.Title, dayView1.SelectedAppointment.value, GetPosition()));
                tomatoCount += 1;
                th.Start();
            }
            catch (Exception ex)
            {
            }
        }

        public void OpenFanQie(int time, string name, string mindmap, int fanqieCount, bool isnotDefault = false, int tasklevelNum = 0)
        {
            Tomato fanqie = new Tomato(new DateTime().AddMinutes(time), name, mindmap, GetPosition(), isnotDefault, tasklevelNum);
            fanqie.ShowDialog();
        }

        #region 添加使用记录

        public Guid currentUsedTimerId;
        public string usedTimeLog = "";

        public void UseTime_Activated(object sender, EventArgs e)
        {
            UsedLogRenew();
        }

        public void UseTime_Deactivate(object sender, EventArgs e)
        {
            UsedLogRenew(false);
        }

        public void UsedLogRenew(bool newlog = true, bool newid = true)
        {
            //结束之前的ID记录，并生成新的ID
            if (newid || currentUsedTimerId == null)
            {
                DocearReminderForm.usedTimer.SetEndDate(currentUsedTimerId);
                Thread th = new Thread(() => DocearReminderForm.SaveUsedTimerFile(DocearReminderForm.usedTimer)) { IsBackground = true };
                th.Start();
                currentUsedTimerId = Guid.NewGuid();
            }
            //添加一个新的记录
            if (newlog)
            {
                string file = "";
                string name;
                if (setting.c_timeBlock.Checked)
                {
                    name = "时间块";
                }
                else if (setting.c_fanqie.Checked)
                {
                    name = "番茄钟";
                }
                else if (setting.c_progress.Checked)
                {
                    name = "进步";
                }
                else if (setting.c_mistake.Checked)
                {
                    name = "错误";
                }
                else if (setting.c_Money.Checked)
                {
                    name = "金钱";
                }
                else if (setting.c_done.Checked)
                {
                    name = "已完成";
                }
                else
                {
                    name = "任务";
                }
                file = setting.workfolder_combox.SelectedItem.ToString();
                DocearReminderForm.usedTimer.NewOneTime(currentUsedTimerId, file, name, usedTimeLog, "日历");
                usedTimeLog = "";
            }
        }

        #endregion 添加使用记录

        public void isview_c_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
            UsedLogRenew();
        }

        public static bool isMenuShow = false;

        public void Menu_Opened(object sender, EventArgs e)
        {
            isMenuShow = true;
        }

        public void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            isMenuShow = false;
        }

        public void c_Money_CheckedChanged(object sender, EventArgs e)
        {
            if (setting.c_Money.Checked)
            {
                dayView1.StartHour = 0;
                dayView1.HalfHourHeight = 65;
                setting.c_timeBlock.Checked = false;
                setting.Ka_c.Checked = false;
                setting.subClass.Checked = false;
                RefreshCalender();
            }

            CheckboxAlldisabled();
            UsedLogRenew();
        }

        public void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            dayView1.HalfHourHeight = (int)setting.numericUpDown3.Value;
        }

        public void Menu_Opening(object sender, CancelEventArgs e)
        {
        }

        public void exclude_TextChanged(object sender, EventArgs e)
        {
        }

        public void textBox_searchwork_TextChanged(object sender, EventArgs e)
        {
        }

        public void checkBox_jinian_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
        }

        public void checkBox_enddate_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
        }

        public void Ka_c_CheckedChanged(object sender, EventArgs e)
        {
            if (setting.Ka_c.Checked)
            {
                dayView1.StartHour = 0;
                dayView1.HalfHourHeight = 40;
                setting.c_timeBlock.Checked = false;
                setting.c_Money.Checked = false;
                setting.subClass.Checked = true;
                RefreshCalender();
            }

            CheckboxAlldisabled();
            UsedLogRenew();
        }

        public void 解锁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dayView1.SelectedAppointment.Locked = false;
        }

        public void CaptureScreen_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
        }

        public void ShowAllFile()
        {
            string NewFileExtension = ini.ReadString("path", "NewFileExtension", "");
            //m_Appointments = new List<Appointment>();//清空
            Appointment m_Appointment = new Appointment();
            foreach (FileInfo file in new DirectoryInfo(ini.ReadString("path", "rootpath", "")).GetFiles("*.*", SearchOption.AllDirectories).Where(file => NewFileExtension.Contains(file.Extension)).ToList())
            {
                if (!file.FullName.Contains(AppDomain.CurrentDomain.BaseDirectory) && !file.FullName.Contains(".git") && !file.FullName.Contains("_data"))
                {
                    DateTime dt = file.CreationTime;
                    if (dt > dayView1.StartDate && dt < dayView1.StartDate.AddDays(dayView1.DaysToShow))
                    {
                        m_Appointment = new Appointment
                        {
                            StartDate = dt
                        };
                        m_Appointment.taskTime = 23;
                        m_Appointment.EndDate = dt.AddMinutes(23);
                        m_Appointment.Title = file.Name;
                        m_Appointment.Comment = file.FullName;
                        m_Appointment.DetailComment = "";
                        m_Appointment.value = file.FullName;
                        m_Appointment.ID = file.FullName;
                        m_Appointment.Color = System.Drawing.Color.Yellow;
                        m_Appointment.BorderColor = System.Drawing.Color.Yellow;
                        m_Appointment.Type = "新文件";
                        m_Appointment.Locked = true;
                        //m_Appointment.Tag = item;
                        m_Appointments.Add(m_Appointment);
                    }
                }
            }
            dayView1.Refresh();
        }

        public void freshCalender_Tick(object sender, EventArgs e)
        {
            if (FreshCalendarBool)
            {
                UpdateCalendar(null, null);
                FreshCalendarBool = false;
            }
        }

        #region 将窗体钉在桌面上

        public class Win32
        {
            public const int SE_SHUTDOWN_PRIVILEGE = 0x13;
            public const int WM_SYSCOMMAND = 0x0112;
            public const int SC_MOVE = 0xF010;
            public const int HTCAPTION = 0x0002;//无边框窗体移动

            [DllImport("user32.dll")]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll")]
            public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

            [DllImport("user32.dll")]
            public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx,
                int cy, uint uFlags);

            [DllImport("user32.dll")]
            public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();

            [DllImport("user32.dll")]
            public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        }

        public void SetToDeskTop()
        {
            try
            {
                if (Environment.OSVersion.Version.Major < 6)
                {
                    base.SendToBack();
                    IntPtr hWndNewParent = Win32.FindWindow("Progman", null);
                    Win32.SetParent(base.Handle, hWndNewParent);
                }
                else
                {
                    IntPtr desktopHwnd = GetDesktopPtr();
                    IntPtr ownHwnd = base.Handle;
                    IntPtr result = Win32.SetParent(ownHwnd, desktopHwnd);
                }
            }
            catch (ApplicationException exx)
            {
                MessageBox.Show(this, exx.Message, "Pin to Desktop");
            }
        }

        public IntPtr GetDesktopPtr()
        {
            //http://blog.csdn.net/mkdym/article/details/7018318
            // 情况一
            IntPtr hwndWorkerW = IntPtr.Zero;
            IntPtr hShellDefView = IntPtr.Zero;
            IntPtr hwndDesktop = IntPtr.Zero;
            IntPtr hProgMan = Win32.FindWindow("ProgMan", null);
            if (hProgMan != IntPtr.Zero)
            {
                hShellDefView = Win32.FindWindowEx(hProgMan, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (hShellDefView != IntPtr.Zero)
                {
                    hwndDesktop = Win32.FindWindowEx(hShellDefView, IntPtr.Zero, "SysListView32", null);
                }
            }
            if (hwndDesktop != IntPtr.Zero) return hwndDesktop;

            // 情况二
            while (hwndDesktop == IntPtr.Zero)
            {//必须存在桌面窗口层次
                hwndWorkerW = Win32.FindWindowEx(IntPtr.Zero, hwndWorkerW, "WorkerW", null);//获得WorkerW类的窗口
                if (hwndWorkerW == IntPtr.Zero) break;//未知错误
                hShellDefView = Win32.FindWindowEx(hwndWorkerW, IntPtr.Zero, "SysListView32", null);
                if (hShellDefView == IntPtr.Zero) continue;
                hwndDesktop = Win32.FindWindowEx(hShellDefView, IntPtr.Zero, "SysListView32", null);
            }
            return hwndDesktop;
        }

        public void MainForm_Activated(object sender, EventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                Win32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, Win32.SE_SHUTDOWN_PRIVILEGE);
            }
        }

        public void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                Win32.SetWindowPos(base.Handle, 1, 0, 0, 0, 0, Win32.SE_SHUTDOWN_PRIVILEGE);
            }
        }

        #endregion 将窗体钉在桌面上

        public void showhide_Tick(object sender, EventArgs e)
        {
            if (isNewOpen&&!this.Focused)
            {
                this.StartPosition= FormStartPosition.Manual;
                this.Location = new System.Drawing.Point(0, 0);
                //窗体最大化
                this.WindowState = FormWindowState.Maximized;
                this.ShowInTaskbar = true;
                //置顶
                this.TopMost = true;
                this.Show();
                this.Activate();
                this.TopMost = false;
                this.Location = new System.Drawing.Point(0, 0);
                this.WindowState = FormWindowState.Maximized;
                isNewOpen = false;
            }
            //else
            //{
            //    this.Hide();
            //}
        }

        public void isShowTask_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCalender();
        }

        public void 打开设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setting.Show();
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
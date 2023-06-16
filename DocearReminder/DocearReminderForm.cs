using Gma.UserActivityMonitor;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using yixiaozi.Config;
using yixiaozi.Model.DocearReminder;
using yixiaozi.MyConvert;
using yixiaozi.Security;
using yixiaozi.Windows;
using yixiaozi.WinForm.Control;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using DayOfWeek = System.DayOfWeek;
using Reminder = yixiaozi.Model.DocearReminder.Reminder;
using System.Reflection;
using NAudio.Wave;
using AForge.Controls;
using AForge.Video.DirectShow;
using System.Windows.Media.Imaging;
using System.Windows;
using Clipboard = System.Windows.Forms.Clipboard;
using IDataObject = System.Windows.Forms.IDataObject;
using DataFormats = System.Windows.Forms.DataFormats;
using DataObject = System.Windows.Forms.DataObject;
using Size = System.Drawing.Size;
using System.Drawing.Imaging;
using FerretLib.WinForms.Controls;
using yixiaozi.WinForm.Control.Calendar;
using yixiaozi.API.Todoist;
using yixiaozi.API.Todoist.Models;
using System.Resources;

namespace DocearReminder
{
    public partial class DocearReminderForm : Form
    {
        #region 全局变量
        public System.Windows.Forms.Timer hoverTimer = new System.Windows.Forms.Timer();
        public System.Windows.Forms.Timer addFanQieTimer = new System.Windows.Forms.Timer();
        //密码
        public static string PassWord = "";
        public static int tomatoCount = 0;
        private bool IsSelectReminder = true;
        private int reminderSelectIndex = -1;
        private int mindmapSelectIndex = -1;
        private bool InMindMapBool = true;
        private bool isCodeFenlei = false;
        SoundPlayer simpleSound = new SoundPlayer();
        public static bool[] fanqiePosition = new bool[(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 30) / 120];
        private HotKeys hotKeys = new HotKeys();
        public static IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
        private string[] noFolder = new string[] { };
        private string[] noFiles = new string[] { };
        private CustomCheckedListBox mindmaplis1 = new CustomCheckedListBox();
        private CustomCheckedListBox.ObjectCollection mindmaplist_backup;
        private List<MyListBoxItem> mindmaplist_all = new List<MyListBoxItem>();
        public string CurrentLanguage = "";
        public string jsonHasMindmaps = "";
        public List<mindmapfile> mindmapfiles = new List<mindmapfile>();
        public List<String> remindmapsList = new List<String>();
        public List<mindmapfile> mindmapfilesAll = new List<mindmapfile>();
        public static List<node> nodes = new List<node>();
        public List<node> nodesicon = new List<node>();
        public List<node> timeblocks = new List<node>();
        public List<node> allfiles = new List<node>();
        private DirectoryInfo rootrootpath = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
        private DirectoryInfo rootpath = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
        public bool isInReminderlistSelect = false;
        public string mindmapPath = "";
        string CalendarImagePath = "";
        public static bool isZhuangbi = false;
        AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
        public bool showfenge = false;
        Reminder reminderObjectOut = new Reminder();
        string showMindmapName = "";//用于Tree中当前导图的名称
        string renameTaskName = "";
        string renameMindMapFileIDParent = "";
        string renameMindMapFileID = "";
        bool isRename = false;
        bool isRenameTimeBlock = false;
        List<string> pathArr = new List<string>();
        public static List<string> ignoreSuggest = new List<string>();
        public static string command = "ga;gc;";
        public static List<string> RecentOpenedMap = new List<string>();
        public static List<string> IconNodesSelected = new List<string>();
        public static List<string> TimeBlockSelected = new List<string>();
        string timeblockcolor = "";
        string timeblockfather = "";
        public static List<string> Xnodes = new List<string>();
        public static List<string> OpenedInRootSearch = new List<string>();
        public static List<string> QuickOpenLog = new List<string>();
        public static List<string> unchkeckmindmap = new List<string>();
        public static List<string> AddTaskWithDate = new List<string>();
        public static DateTime TimeBlocklastTime = DateTime.Today;
        RecordController record = new RecordController();

        public static Color BackGroundColor = Color.White;
        bool isRefreshMindmap = false;
        public List<MyListBoxItemRemind> reminderboxList = new List<MyListBoxItemRemind>();
        int ebconfig = 98765432;
        string ebdefault = "";
        List<MyListBoxItemRemind> RemindersOtherPath = new List<MyListBoxItemRemind>();
        string nodeIconString = "";
        string timeblockString = "";
        bool lockForm = false;
        bool isSearchFileOrNode = false;
        DirectoryInfo fileTreePath;
        bool isPlaySound = false;
        bool playBackGround = false;
        static string logpass = "niqishihenhao";
        bool allFloder = false;
        bool IsEncryptBool = false;
        object reminderlistSelectedItem;
        List<StationInfo> suggestListData = new List<StationInfo>();
        Encrypt encrypt;
        Encrypt encryptlog;
        bool selectedpath = true;
        public static UsedTimer usedTimer = new UsedTimer();
        Guid currentUsedTimerId;
        /// <summary>
        /// 窗口活动时间
        /// </summary>
        DateTime formActiveTime;
        TimeSpan leaveSpan = new TimeSpan();
        bool isneedreminderlistrefresh = true;
        bool isneedKeyUpEventWork = true;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        public string usedTimeLog = "";
        public string todoistKey = "";
        public bool Camera = false;
        public int maxwidth = 1444;
        public int middlewidth = 1180;
        public int normalwidth = 888;

        public int maxheight = 880;
        public int normalheight = 545;

        public int nodetreeTop = 506;
        public int nodetreeTopTop = 9;
        public int nodetreeHeight = 322;
        public int nodeTreeHeightMax = 818;
        
        #endregion
        public DocearReminderForm()
        {
            InitializeComponent();
            pictureBox1.Height = 0;
            this.Width = middlewidth;
            formActiveTime = DateTime.Now;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            try
            {
                SwitchToLanguageMode();
                GetIniFile();
                ReadBookmarks();
                if (RecentlyFileHelper.GetStartFiles() != null)
                {
                    suggestListData.AddRange(RecentlyFileHelper.GetStartFiles());
                }
                //频繁刷新导致界面闪烁解决方法我也不知道有没有用
                pathArr.Add(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                mindmapPath = ini.ReadString("path", "rootpath", "");
                todoistKey = ini.ReadString("todoist", "key", "");
                lockForm = ini.ReadString("appearance", "lock", "") == "true";
                HookManager.KeyDown += HookManager_KeyDown;
                HookManager.KeyUp += HookManager_KeyDown_saveKeyBoard;
                this.DoubleBuffered = true;//设置本窗体
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
                SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
                SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
                UpdateStyles();
                hoverTimer.Interval = 100;//定时周期3秒
                hoverTimer.Tick += new EventHandler(Hover);//到3秒了自动隐藏
                hoverTimer.Enabled = false; //是否不断重复定时器操作
                hoverTimer.Start();
                addFanQieTimer.Interval = 60 * 1000 - DateTime.Now.Second * 1000 - DateTime.Now.Millisecond;
                addFanQieTimer.Tick += new EventHandler(AddFanQie);
                addFanQieTimer.Start();

                InitVoice();
                //CameraTimer_Tick(null, null);//打开软件时截屏，其实没有必要

                #region 用来监视文件编号的代码，暂时不用，有git监控就可以了
                //this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
                //fileSystemWatcher1.IncludeSubdirectories = true;
                //fileSystemWatcher1.NotifyFilter = NotifyFilters.LastWrite
                //             | NotifyFilters.FileName | NotifyFilters.DirectoryName;// NotifyFilters.LastAccess |
                //((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
                //this.fileSystemWatcher1.EnableRaisingEvents = true;
                //this.fileSystemWatcher1.SynchronizingObject = this;
                //this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
                //this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
                //this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
                //this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
                //this.fileSystemWatcher1.Path = ini.ReadString("path", "rootpath", "");
                //((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
                #endregion

                //设置之前保存到皮肤
                if (ini.ReadString("Skin", "src", "") != "")
                {
                    skinEngine1.SkinFile = ini.ReadString("Skin", "src", "");
                }
                addirisSkinToolStripMenuItem(new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\Skins"), null);

                mindmaplist.Height = 424;
                ReminderlistBoxChange();
                
                //设置透明度
                try
                {
                    this.Opacity = Convert.ToDouble(ini.ReadString("appearance", "Opacity", ""));
                }
                catch (Exception ex)
                {
                }

                //加载Tag
                ReadTagFile();

                SearchText_suggest.ScrollAlwaysVisible = false;
                SearchText_suggest.MaximumSize = new Size(600, 444);
                SearchText_suggest.Top = 31;
                SearchText_suggest.DrawItem += SuggestText_DrawItem;
                SearchText_suggest.DrawMode = DrawMode.OwnerDrawVariable;
                mindmaplist.DrawItem += Mindmaplist_DrawItem;
                nodetree.DrawMode = TreeViewDrawMode.OwnerDrawText;
                FileTreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
                mindmaplist.DrawMode = DrawMode.OwnerDrawVariable;
                TimeBlockDate.Value = DateTime.Today;
                MoneyDateTimePicker.Value = DateTime.Today;
                KADateTimePicker.Value = DateTime.Today;
                reminderList.Top = 51;
                reminderListBox.Top = 51;
                mindmaplist.DisplayMember = "Text";
                mindmaplist.ValueMember = "Value";
                reminderList.DisplayMember = "Text";
                reminderList.ValueMember = "Value";
                mindmaplist.Items.Clear();
                reminderList.Items.Clear();
                string no = ini.ReadString("path", "no", "");
                string noFilesString = ini.ReadString("path", "nofiles", "");
                CalendarImagePath = ini.ReadStringDefault("path", "CalendarImagePath", "");
                ebdefault = ini.ReadString("path", "ebdefault", "");
                ebconfig = Convert.ToInt32(ini.ReadString("config", "ebconfig", ""));
                isPlaySound = ini.ReadString("sound", "playsounddefault", "") == "true";
                playBackGround = ini.ReadString("sound", "playBackGround", "") == "true";
                onlyZhouqi.Checked = ini.ReadString("config", "IsCycleOnly", "") == "true";
                Camera = ini.ReadString("config", "IsCycleOnly", "") == "true";

                string scorestr = ini.ReadString("info", "score", "");
                fenshu.Text = scorestr;
                command = ini.ReadString("config", "command", "");
                logpass = ini.ReadString("password", "i", "");
                encryptlog = new Encrypt(logpass);
                if (!Directory.Exists(System.IO.Path.GetFullPath(ini.ReadStringDefault("path", "rootpath", ""))))
                {
                    Directory.CreateDirectory(System.IO.Path.GetFullPath(ini.ReadStringDefault("path", "rootpath", "")));
                    File.Copy(System.AppDomain.CurrentDomain.BaseDirectory + @"\Demo\calander.mm", ini.ReadStringDefault("path", "rootpath", "") + @"\calander.mm");
                    Process.Start(System.IO.Path.GetFullPath(ini.ReadStringDefault("path", "rootpath", "")));
                }
                rootpath = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadStringDefault("path", "rootpath", "")));
                if (!System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"\reminder.json"))
                {
                    File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"\reminder.json", CompressToBase64(new JavaScriptSerializer
                    {
                        MaxJsonLength = Int32.MaxValue
                    }.Serialize(reminderObject)));
                }
                else
                {
                    FileInfo reminderjsonfile = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"\reminder.json");
                    using (StreamReader sw = reminderjsonfile.OpenText())
                    {
                        string s = sw.ReadToEnd();
                        var serializer = new JavaScriptSerializer()
                        {
                            MaxJsonLength = Int32.MaxValue
                        };
                        reminderObject = serializer.Deserialize<Reminder>(ReplaceJsonDateToDateString(DecompressFromBase64(s)));
                    }
                }

                #region 加载一些配置文件
                rootrootpath = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadStringDefault("path", "rootpath", "")));
                ignoreSuggest = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\ignoreSuggest.txt");
                RecentOpenedMap = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\RecentOpenedMap.txt");
                IconNodesSelected = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\IconNodesSelected.txt");
                TimeBlockSelected = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\TimeBlockSelected.txt");
                Xnodes = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\Xnodes.txt");
                OpenedInRootSearch = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\OpenedInRootSearch.txt");
                QuickOpenLog = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\QuickOpenLog.txt");
                unchkeckmindmap = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\unchkeckmindmap.txt");
                remindmapsList = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\remindmaps.txt");
                AddTaskWithDate = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\AddTaskWithDate.txt"); 
                #endregion

                #region UsedTimer
                fathernode.Text = "";
                UsedTimerOnLoad();
                #endregion
                FileTreeView.AfterSelect += FileTreeView_AfterSelect;
                string calanderpath = ini.ReadString("path", "calanderpath", "");
                IntPtr nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
                fileTreePath = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                this.Height = normalheight; showMindmapName = "";
                noterichTextBox.LoadFile(ini.ReadString("path", "note", System.AppDomain.CurrentDomain.BaseDirectory + @"\note.txt"));
                richTextSubNode.Height = 0;
                try
                {
                    SetTitle();
                    foreach (string item in calanderpath.Split(';'))
                    {
                        if (item != "")
                        {
                            int i = PathcomboBox.Items.Add(item);
                        }
                    }
                    PathcomboBox.Items.Add("all");
                    hopeNote.LoadFile(System.AppDomain.CurrentDomain.BaseDirectory + @"rootPath.txt");
                }
                catch (Exception ex)
                {
                }		

                noFolder = no.Split(';');
                noFiles = noFilesString.Split(';');
                nodes.Clear();
                JavaScriptSerializer js = new JavaScriptSerializer
                {
                    MaxJsonLength = Int32.MaxValue
                };
                FileInfo fi;
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"allnode.json"))
                {
                    try
                    {
                        fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"allnode.json");
                        using (StreamReader sw = fi.OpenText())
                        {
                            string s = sw.ReadToEnd();
                            nodes = js.Deserialize<List<node>>(s);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"allfiles.json"))
                {
                    try
                    {
                        fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"allfiles.json");
                        using (StreamReader sw = fi.OpenText())
                        {
                            string s = sw.ReadToEnd();
                            allfiles = js.Deserialize<List<node>>(s);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                taskcount.Text = "0";
                isRefreshMindmap = true;
                LoadFile(rootpath);
                for (int i = 0; i < mindmaplist.Items.Count; i++)
                {
                    setmindmapcheck = true;
                    mindmaplist.SetItemChecked(i, true);
                    string file = ((MyListBoxItem)mindmaplist.Items[i]).Value;
                    if (unchkeckmindmap.Contains(file))
                    {
                        mindmaplist.SetItemChecked(i, false);
                        mindmaplist.Refresh();
                    }
                    setmindmapcheck = false;
                }
                isRefreshMindmap = false;
                mindmaplist_backup = mindmaplis1.Items;
                mindmaplist_backup.Clear();
                mindmaplist_backup.AddRange(mindmaplist.Items);
                foreach (var item in mindmaplist.Items)
                {
                    if (!mindmaplist_all.Any(m => m.Value == ((MyListBoxItem)item).Value))
                    {
                        mindmaplist_all.Add(((MyListBoxItem)item));
                    }
                }
                mindmaplist_count.Text = mindmaplist.Items.Count.ToString();
                RRReminderlist();
                hotKeys.Regist(this.Handle, (int)HotKeys.HotkeyModifiers.Shift, Keys.Space, CallBack);
                hotKeys.Regist(this.Handle, (int)HotKeys.HotkeyModifiers.Alt, Keys.Space, showcalander);

                foreach (var item in mindmapfiles)
                {
                    acsc.Add(item.name);
                }
                GetAllFilesJsonIconFile();
                GetAllNodeJsonFile();
                GetAllFilesJsonFile();
                GetTimeBlock();
                titleTimer.Start();
                //创建桌面快捷方式（好像不会重复创建）
                if (ini.ReadString("config", "CreateShortcutOnDesktop", "") == "ture")
                {
                    yixiaozi.Windows.ShortcutCreator.CreateShortcutOnDesktop("DocearReminder", System.AppDomain.CurrentDomain.BaseDirectory + "DocearReminder.exe");
                }
                SaveLog("打开程序。");
                SetTimeBlockLasTime();
                timeblockcheck.Text = "";
                #region 添加提示信息
                try
                {
                    Dictionary<Control, string> dic = new Dictionary<Control, string>();
                    dic.Add(n_days, "日期数");
                    dic.Add(c_day, "日周期");
                    dic.Add(c_week, "周周期");
                    dic.Add(c_hour, "小时周期");
                    dic.Add(c_month, "月周期");
                    dic.Add(c_year, "年周期");
                    dic.Add(c_Sunday, "周日");
                    dic.Add(c_Saturday, "周六");
                    dic.Add(c_Friday, "周五");
                    dic.Add(c_Wednesday, "周三");
                    dic.Add(c_Tuesday, "周二");
                    dic.Add(c_Thursday, "周四");
                    dic.Add(c_Monday, "周一");
                    dic.Add(c_Jinian, "纪念日");
                    dic.Add(c_ViewModel, "查看模式");
                    dic.Add(dateTimePicker, "任务时间");
                    dic.Add(taskTime, "任务时长");
                    dic.Add(tasklevel, "任务等级");
                    //dic.Add(richTextSubNode, "子节点");
                    //dic.Add(tagCloudControl, "标签云");
                    dic.Add(fathernode, "父节点信息");
                    dic.Add(hourLeft, "当日剩余小时");
                    dic.Add(Hours, "任务时间总计");
                    dic.Add(labeltaskinfo, "任务信息：任务（只读任务）|周期任务（记忆任务）|不重要任务|密码任务");
                    dic.Add(reminder_count, "任务数量");
                    dic.Add(fenshu, "分数");
                    dic.Add(usedtimelabel, "总使用时长");
                    dic.Add(todayusedtime, "当日使用时长");
                    dic.Add(usedCount, "打开次数");
                    dic.Add(mindmaplist_count, "导图数");
                    dic.Add(taskcount, "任务总数");
                    dic.Add(c_remember, "任务总数");
                    dic.Add(c_speechcontrol, "语音控制");
                    dic.Add(searchword, "task@mindmap==.task +Enter" + Environment.NewLine + @"subnote +Enter" + Environment.NewLine + @"subtask +Shift+Enter==subtask. +Enter" + Environment.NewLine +
                        "@mindmap  detail of mindmap" + Environment.NewLine + "@mindmap +Shift open mindmap" +
                        Environment.NewLine + "#searchfiles" +
                        Environment.NewLine + "*searchnodes" +
                        Environment.NewLine + "!recentopenmindmap" +
                        Environment.NewLine + "`recentopenfiles" +
                        Environment.NewLine + "others:clipse,clipf,clipF,showlog,tool,mindmaps,allicons,allnodes");
                    MoveOverInfoTip.SettingMutiTipInfo(dic);
                    Thread thnewfies = new Thread(() => NewFiles());
                    thnewfies.Start();
                }
                catch (Exception ex)
                {
                }
                #endregion
            }
            catch (Exception ex)
            {
                AddErrorLog(ex);
                MessageBox.Show(ex.ToString());
            }
        }

        #region 压缩方法
        /// <summary>
        /// 压缩成base64格式
        /// </summary>
        /// <param name="content">原文</param>
        /// <returns></returns>
        public static string CompressToBase64(string content)
        {
            return content;
            try
            {
                if (!string.IsNullOrEmpty(content))
                {
                    content = content.Trim();
                    if (content.Length > 0)
                    {
                        content = LZStringCSharp.LZString.CompressToBase64(content);
                    }
                }
            }
            catch (Exception ex)
            { }
            return content;
        }

        /// <summary>
        /// 从base64解压数据
        /// </summary>
        /// <param name="content">密文</param>
        /// <returns></returns>
        public static string DecompressFromBase64(string content)
        {
            return content;
            try
            {
                if (!string.IsNullOrEmpty(content))
                {
                    content = content.Trim();
                    if (content.Length > 0)
                    {
                        if (IsBase64(content))
                        {
                            content = LZStringCSharp.LZString.DecompressFromBase64(content);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return content;
        }
        private static char[] base64CodeArray = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4',  '5', '6', '7', '8', '9', '+', '/', '='
        };

        /// <summary>
        /// 是否base64字符串
        /// </summary>
        /// <param name="base64Str">要判断的字符串</param>
        /// <param name="bytes">字符串转换成的字节数组</param>
        /// <returns></returns>
        public static bool IsBase64(string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str))
                return false;
            else
            {
                if (base64Str.Contains(","))
                    base64Str = base64Str.Split(',')[1];
                if (base64Str.Length % 4 != 0)
                    return false;
                if (base64Str.Any(c => !base64CodeArray.Contains(c)))
                    return false;
            }
            try
            {
                var bytes = Convert.FromBase64String(base64Str);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
        } 
        #endregion

        public void SetTitle()
        {
            string birthday = ini.ReadString("info", "birthday", "");
            string loseweight = ini.ReadString("info", "loseweight", "");
            string loseweighttarget = ini.ReadString("info", "loseweighttarget", "");
            string leavechina = ini.ReadString("info", "leavechina", "");
            DateTime birthdayD = Convert.ToDateTime(birthday);
            TimeSpan diff = DateTime.Today - birthdayD;
            this.Text = ini.ReadString("info", "myword", ""); //"开心，高效，认真，专注";
            this.Text += ("   " + diff.TotalDays);
            int yeardiff = DateTime.Now.Year - birthdayD.Year;
            int monthdiff = DateTime.Now.Month - birthdayD.Month;
            if (monthdiff < 0)
            {
                yeardiff--;
                monthdiff += 12;
            }
            int daydiff = DateTime.Now.Day - birthdayD.Day;
            if (daydiff < 0)
            {
                monthdiff--;
                if (monthdiff < 0)
                {
                    yeardiff--;
                    monthdiff += 12;
                }
                daydiff += 30;
            }
            string birthString = (yeardiff.ToString() + "年" + (monthdiff != 0 ? monthdiff.ToString() + "月" : "") + (daydiff != 0 ? daydiff.ToString() + "天" : ""));
            //添加减肥目标 add lose weight target
            try
            {
                string loseWeightStr = "";
                if (ini.ReadString("info", "showweight", "") == "true")
                {
                    DateTime loseWeightDate = Convert.ToDateTime(loseweight);
                    loseWeightStr = " |" + (loseWeightDate - DateTime.Today).TotalDays.ToString() + "天减到" + loseweighttarget + "|";
                }
                string leaveChainaStr = "";
                if (ini.ReadString("info", "showleavechina", "") == "true")
                {
                    DateTime leaveChinaDate = Convert.ToDateTime(leavechina);
                    leaveChainaStr = (leaveChinaDate - DateTime.Today).TotalDays.ToString() + "天离开中国";
                }
                this.Text += ("   " + birthString) + loseWeightStr + leaveChainaStr + " @  " + DateTime.Now.ToString("HH:mm");
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 将Json格式的时间字符串替换为"yyyy-MM-dd HH:mm:ss"格式的字符串
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string ReplaceJsonDateToDateString(string json)
        {
            return Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
        }

        private void UsedTimerOnLoad()
        {
            if (!System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json"))
            {
                SaveUsedTimerFile(new UsedTimer());
            }
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json");
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer()
                {
                    MaxJsonLength = Int32.MaxValue
                };
                usedTimer = serializer.Deserialize<UsedTimer>(ReplaceJsonDateToDateString(s));
            }
            currentUsedTimerId = Guid.NewGuid();
            usedCount.Text = usedTimer.Count.ToString();
            usedtimelabel.Text = usedTimer.AllTime.ToString(@"dd\.hh\:mm\:ss");
            todayusedtime.Text = usedTimer.todayTime.TotalMinutes.ToString("N0");
            UsedLogRenew(true, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder">Skins</param>
        /// <param name="menuitem"></param>
        public void addirisSkinToolStripMenuItem(DirectoryInfo folder, ToolStripItem menuitem)
        {
            System.Windows.Forms.ToolStripItem newMenu;
            if (folder.GetFiles().Count() > 0)
            {
                foreach (FileInfo file in folder.GetFiles())
                {
                    if (file.Name.Contains(".ssk"))
                    {
                        if (menuitem == null)
                        {
                            newMenu = irisSkinToolStripMenuItem.DropDownItems.Add(file.Name, global::DocearReminder.Properties.Resources.square_ok, SetSkin);
                            newMenu.Tag = file.FullName;
                        }
                        else
                        {
                            newMenu = ((ToolStripMenuItem)menuitem).DropDownItems.Add(file.Name, global::DocearReminder.Properties.Resources.square_ok, SetSkin);
                            newMenu.Tag = file.FullName;
                        }
                    }
                }
            }
            foreach (DirectoryInfo item in folder.GetDirectories())
            {
                if (menuitem == null)
                {
                    newMenu = irisSkinToolStripMenuItem.DropDownItems.Add(item.Name, global::DocearReminder.Properties.Resources.square_ok, null);
                }
                else
                {
                    newMenu = ((ToolStripMenuItem)menuitem).DropDownItems.Add(item.Name, global::DocearReminder.Properties.Resources.square_ok, null);
                }
                addirisSkinToolStripMenuItem(item, newMenu);

            }
        }

        private void SetSkin(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = ((System.Windows.Forms.ToolStripItem)sender).Tag.ToString();
            ini.WriteString("Skin", "src", skinEngine1.SkinFile);
        }
        //加一个控制，一分钟写一次就好了
        public static List<string> usetimelist = new List<string>();
        public static string usedtimerJson = "";
        public static void SaveUsedTimerFile(UsedTimer data)
        {
            if (!usetimelist.Contains(DateTime.Now.ToString("yy-MM-dd-HH-mm")))
            {
                usetimelist.Add(DateTime.Now.ToString("yy-MM-dd-HH-mm"));
                try
                {
                    usedtimerJson = new JavaScriptSerializer()
                    {
                        MaxJsonLength = Int32.MaxValue
                    }.Serialize(data);
                    File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json", usedtimerJson);
                }
                catch (Exception ex)
                {
                }
                //FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json");
                //using (StreamWriter sw = fi.AppendText())
                //{
                //    sw.Write(json);
                //}
            }
        }

        private void Load_Click(object sender, EventArgs e)
        {
            List<MyListBoxItemRemind> Reminders = reminderList.Items.Cast<MyListBoxItemRemind>().ToList();
            RemindersOtherPath.AddRange(Reminders);
            mindmaplist.Items.Clear();
            reminderList.Items.Clear();
            //nodes.Clear();
            //if (!pathArr.Contains(rootpath.FullName))
            //{
            //    Thread th = new Thread(() => GetAllNode(rootpath));
            //    th.Start();
            //}

            taskcount.Text = "0";
            isRefreshMindmap = true;
            LoadFile(rootpath);
            //for (int i = 0; i < mindmaplist.Items.Count; i++)
            //{
            //    string file = ((MyListBoxItem)mindmaplist.Items[i]).Value;
            //    if (unchkeckmindmap.Contains(file))
            //    {
            //        mindmaplist.SetSelected(i, false);
            //    }
            //}
            mindmaplist_backup.Clear();
            mindmaplist_backup.AddRange(mindmaplist.Items);
            foreach (var item in mindmaplist.Items)
            {
                if (!mindmaplist_all.Any(m => m.Value == ((MyListBoxItem)item).Value))
                {
                    mindmaplist_all.Add(((MyListBoxItem)item));
                }
            }
            mindmaplist_count.Text = mindmaplist.Items.Count.ToString();
            for (int i = 0; i < mindmaplist.Items.Count; i++)
            {
                mindmaplist.SetItemChecked(i, true);
                string file = ((MyListBoxItem)mindmaplist.Items[i]).Value;
                if (unchkeckmindmap.Contains(file))
                {
                    mindmaplist.SetItemChecked(i, false);
                }
            }
            isRefreshMindmap = false;
            ReSetValue();
            RRReminderlist();
        }

        #region 窗体事件
        bool thisactive = true;
        private void DocearReminderForm_Deactivate(object sender, EventArgs e)
        {
            thisactive = false;
            if (lockForm)
            {

            }
            else
            {
                MyHide();
            }
        }
        private void Hover(object O, EventArgs ev)
        {
            //if (this.Location.X < 5 && ((Cursor.Position.X < this.Location.X || Cursor.Position.Y < this.Location.Y) || (Cursor.Position.X > this.Location.X + 836 || Cursor.Position.Y > this.Location.Y + 544)))
            //{
            //    if (this.Location.X < 5 && Cursor.Position.X > 5)
            //    {
            //        Center();//= new Point(-825, this.Location.Y);sdfadfsf
            //    }
            //}
        }
        private void DocearReminderForm_SizeChanged(object sender, EventArgs e)
        {
            //asc = new AutoSizeFormClass();
            //asc.controlAutoSize(this);
            Center();
            if (this.Height == maxheight)
            {
                fathernode.Visible = false;
            }
            else
            {
                fathernode.Visible = true;
            }
        }

        private void DocearReminderForm_Load(object sender, EventArgs e)
        {
            Center();
        }
        //按下快捷键时被调用的方法
        public void CallBack()
        {
            if (this.Visible == true)
            {
                if (thisactive)
                {
                    MyHide();
                }
                else
                {
                    this.Activate();
                }
            }
            else
            {
                this.BackColor = BackGroundColor;
                PlaySimpleSound("show");
                isInReminderlistSelect = false;
                MyShow();
                usedCount.Text = usedTimer.Count.ToString();
                usedtimelabel.Text = usedTimer.AllTime.ToString(@"dd\.hh\:mm\:ss");
                todayusedtime.Text = usedTimer.todayTime.TotalMinutes.ToString("N0");
                UsedLogRenew();
            }
        }
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int SW_SHOWNOMAL = 1;
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);//显示
            SetForegroundWindow(instance.MainWindowHandle);//当到最前端
        }

        private static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName("CalendarForm");//currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (true || process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\")
             == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        public void showcalander()
        {
            if (mindmaplist.Focused)
            {
                IsSelectReminder = false;
            }
            Thread thCalendarForm = new Thread(() => Application.Run(new CalendarForm(mindmapPath)));
            thCalendarForm.SetApartmentState(ApartmentState.STA); //重点
            thCalendarForm.Start();
            SetTitle();
            MyHide();
            //Process process = RuningInstance();
            //if (process != null)
            //{
            //    HandleRunningInstance(process);
            //    //System.Environment.Exit(1);
            //}
            //else
            //{
            //    Thread thCalendarForm = new Thread(() => Application.Run(new CalendarForm(mindmapPath)));
            //    thCalendarForm.SetApartmentState(ApartmentState.STA); //重点
            //    thCalendarForm.Start();
            //    MyHide();
            //}
        }
        public void showlog()
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new LogForm()));
            thCalendarForm.SetApartmentState(ApartmentState.STA); //重点
            thCalendarForm.Start();
            MyHide();
        }
        public void OpenSearch()
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new Search()));
            thCalendarForm.SetApartmentState(ApartmentState.STA); //重点
            thCalendarForm.Start();
            MyHide();
        }

        #endregion

        #region 番茄钟
        private void AddFanQie(object O, EventArgs ev)
        {
            addFanQieTimer.Interval = 60000;
            RemindersOtherPath.RemoveAll(m => m.rootPath == rootpath.FullName);
            List<MyListBoxItemRemind> Reminders = reminderList.Items.Cast<MyListBoxItemRemind>().ToList();
            RemindersOtherPath.AddRange(Reminders);
            List<string> name = new List<string>();
            if (StartRecordCheckBox.Checked)
            {
                foreach (MyListBoxItemRemind selectedReminder in RemindersOtherPath.Distinct().Where(m => m.Time.DayOfYear == DateTime.Now.DayOfYear && m.Time.Year == DateTime.Now.Year && m.Time.Hour == DateTime.Now.Hour && m.Time.Minute == DateTime.Now.Minute))
                {
                    if (name.Contains(selectedReminder.Name))
                    {
                        continue;
                    }
                    else
                    {
                        name.Add(selectedReminder.Name);
                    }
                    if (selectedReminder != null && selectedReminder.rtaskTime != 0)
                    {
                        if (GetPosition() < 20)
                        {
                            int p = GetPosition();
                            DocearReminderForm.fanqiePosition[p] = true;
                            lockForm = true;
                            Thread th = new Thread(() => OpenFanQie(selectedReminder.rtaskTime, selectedReminder.Name, selectedReminder.Value, p, false, selectedReminder.level));
                            th.Start();
                            lockForm = false;
                            this.Activate();
                        }
                        if (IsURL(selectedReminder.Name.Trim()))
                        {
                            System.Diagnostics.Process.Start(GetUrl(selectedReminder.Name));
                            CompleteTask(selectedReminder);
                        }
                        else if (IsFileUrl(selectedReminder.Name.Trim()))
                        {
                            System.Diagnostics.Process.Start(getFileUrlPath(selectedReminder.Name));
                            CompleteTask(selectedReminder);
                        }
                        //如果是小时循环的，则自动完成
                        if (selectedReminder.remindertype == "hour")
                        {
                            try
                            {
                                CompleteTask(selectedReminder);
                                Thread th1 = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                                th1.Start();
                            }
                            catch (Exception ex)
                            {
                                if (reminderList.Items.Count > 0)
                                {
                                    reminderList.SetSelected(0, true);
                                }

                            }
                        }
                    }
                }
            }
            if (DateTime.Now.Minute == 0 || DateTime.Now.Minute == 15 || DateTime.Now.Minute == 30 || DateTime.Now.Minute == 45)
            {
                //每15分钟拍照
                CameraTimer_Tick(null, null);
                if (DateTime.Now.Hour > 22 || DateTime.Now.Hour < 5 || searchword.Focused || hopeNote.Focused || nodetreeSearch.Focused)
                {

                }
                else
                {
                    int p = GetPosition();
                    DocearReminderForm.fanqiePosition[p] = true;
                    Thread th = new Thread(() => OpenFanQie(1, DateTime.Now.ToString("HH:mm"), "", p, false));
                    th.Start();
                    //每小时刷新所有导图的数据
                    GetAllNodeJsonFile();
                    #region 刷新左侧文件，有问题
                    //isRefreshMindmap = true;
                    //LoadFile(rootpath);
                    //for (int i = 0; i < mindmaplist.Items.Count; i++)
                    //{
                    //    mindmaplist.SetItemChecked(i, true);
                    //    string file = ((MyListBoxItem)mindmaplist.Items[i]).Value;
                    //    if (unchkeckmindmap.Contains(file))
                    //    {
                    //        mindmaplist.SetItemChecked(i, false);
                    //    }
                    //}
                    //isRefreshMindmap = false;
                    #endregion
                }
            }
            if (DateTime.Now.Minute == 41 || DateTime.Now.Minute == 11)
            {
                NewFiles();
            }
            try
            {
                string json = new JavaScriptSerializer()
                {
                    MaxJsonLength = Int32.MaxValue
                }.Serialize(reminderObject);
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"\reminder.json", CompressToBase64(json));
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region 窗体大小
        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
        }
        public void MyHide()
        {
            try
            {
                PlaySimpleSound("hide");
                SearchText_suggest.Visible = false;
                this.Hide();
                UsedLogRenew(false);
            }
            catch (Exception ex)
            {
            }
        }
        public void UsedLogRenew(bool newlog = true, bool newid = true)
        {
            //结束之前的ID记录，并生成新的ID
            if (newid)
            {
                LeaveTime();
                if (leaveSpan >= new TimeSpan(0, 0, 60))
                {
                    usedTimer.SetEndDate(currentUsedTimerId, Convert.ToInt32(leaveSpan.TotalSeconds));
                }
                else
                {
                    usedTimer.SetEndDate(currentUsedTimerId);
                }
                SaveUsedTimerFile(usedTimer);
                currentUsedTimerId = Guid.NewGuid();
            }
            //添加一个新的记录
            if (newlog)
            {
                usedTimer.NewOneTime(currentUsedTimerId, PathcomboBox.SelectedItem == null ? "rootPath" : PathcomboBox.SelectedItem.ToString(), showMindmapName, usedTimeLog, "主窗口");
                usedTimeLog = "";
            }
        }

        //判断操作间隔，如果大于60s则记录离开时间，避免窗口一直显示对统计使用时间的影响
        public void LeaveTime()
        {
            if ((DateTime.Now - formActiveTime) >= new TimeSpan(0, 0, 60))
            {
                leaveSpan = leaveSpan.Add(DateTime.Now - formActiveTime);
            }
            else
            {
                formActiveTime = DateTime.Now;
                leaveSpan = new TimeSpan(0, 0, 0);
            }
        }

        public void MyShow()
        {
            keyJ.Stop();
            if (this.Height < maxheight)//把高度自动调整一下，免得看到不是固定高度难受
            {
                this.Height = normalheight;
            }
            Center();
            this.Show();
            this.Activate();
            UsedLogRenew(true, false);
            formActiveTime = DateTime.Now;
            leaveSpan = new TimeSpan(0, 0, 0);
            this.Text = this.Text.Split('@')[0] + "@  " + DateTime.Now.ToString("HH:mm:ss");
            try
            {
                usedCount.Text = usedTimer.Count.ToString();
                usedtimelabel.Text = usedTimer.AllTime.ToString(@"dd\.hh\:mm\:ss");
                todayusedtime.Text = usedTimer.todayTime.TotalMinutes.ToString("N0");
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region 没用过的方法
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (IsClip.Checked||true)
            {
                const int WM_DRAWCLIPBOARD = 0x308;
                const int WM_CHANGECBCHAIN = 0x030D;
                //const int WM_NCPAINT = 0x85;
                switch (m.Msg)
                {
                    case WM_DRAWCLIPBOARD:
                        DisplayClipboardData();
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        break;

                    case WM_CHANGECBCHAIN:
                        if (m.WParam == nextClipboardViewer)
                        {
                            nextClipboardViewer = m.LParam;
                        }
                        else
                        {
                            SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                        }

                        break;
                    //case WM_NCPAINT:
                    //    IntPtr hdc = GetWindowDC(m.HWnd);
                    //    if ((int)hdc != 0)
                    //    {
                    //        Graphics g = Graphics.FromHdc(hdc);
                    //        g.FillRectangle(Brushes.Red, new Rectangle(0, 0, 4800, 23));
                    //        g.Flush();
                    //        ReleaseDC(m.HWnd, hdc);
                    //    }
                    //    break;
                    default:
                        //HookManager_KeyDown_saveKeyBoard(m.WParam);
                        hotKeys.ProcessHotKey(m);//快捷键消息处理
                        base.WndProc(ref m);
                        break;
                }
            }
        }
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);
        void KillProcess(string processname)
        {
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.ProcessName.ToLower() + ".exe" == processname.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                    {
                        p.Threads[i].Dispose();
                    }

                    p.Kill();
                    break;
                }
            }
        }
        private bool closeProc(string ProcName)
        {
            bool result = false;
            System.Collections.ArrayList procList = new System.Collections.ArrayList();
            string tempName = "";
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                tempName = thisProc.ProcessName;
                if (tempName.Contains("ocea"))
                {
                    MessageBox.Show(tempName);
                }
                procList.Add(tempName);
                if (tempName == ProcName)
                {
                    if (!thisProc.CloseMainWindow())
                    {
                        thisProc.Kill(); //当发送关闭窗口命令无效时强行结束进程                    
                    }

                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 切换输入法
        /// </summary>
        /// <param name="cultureType">语言项，如zh-CN，en-US</param>
        private void SwitchToLanguageMode(string cultureType = "en-US")
        {
            
            var installedInputLanguages = InputLanguage.InstalledInputLanguages;
            if (installedInputLanguages.Cast<InputLanguage>().Any(i => i.LayoutName== "小狼毫"))
            {
                foreach (InputLanguage item in installedInputLanguages)
                {
                    if (item.LayoutName == "小狼毫")
                    {
                        InputLanguage.CurrentInputLanguage = item;
                        IntPtr prt = ImmGetContext(InputLanguage.CurrentInputLanguage.Handle);
                        if (cultureType == "en-US")
                        {
                            ImmSetConversionStatus(prt, 0, 0);
                        }
                        else
                        {
                            ImmSetConversionStatus(prt, 1, 0);
                        }

                        break;
                    }
                }
            }
        }
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr hIMC,
            ref int conversion, ref int sentence);

        [DllImport("imm32.dll")]
        public static extern bool ImmSetConversionStatus(IntPtr hIMC, int conversion, int sentence);
        #endregion

        #region allnode,allicon,allfile等数据加载
        public void GetAllNode(DirectoryInfo path)
        {
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                if (!noFiles.Contains(file.Name) && file.Name[0] != '~')
                {
                    try
                    {
                        System.Xml.XmlDocument x = new XmlDocument();
                        x.Load(file.FullName);
                        string fileName = file.Name.Substring(0, file.Name.Length - 3);
                        List<string> contents = new List<string>();
                        foreach (XmlNode node in x.GetElementsByTagName("node"))
                        {
                            try
                            {
                                if (node.Attributes["TEXT"] == null || node.Attributes["ID"] == null)
                                {
                                    continue;
                                }
                                if (node.Attributes["TEXT"].Value != "")
                                {
                                    if (node.Attributes["TEXT"].Value.Length <= 4 && node.Attributes["TEXT"].Value.All(char.IsDigit))
                                    {
                                        continue;
                                    }
                                    string father = GetFatherNodeName(node);
                                    if (father.Contains("Folder|"))
                                    {
                                        continue;
                                    }
                                    if (!contents.Contains(node.Attributes["TEXT"].Value))
                                    {
                                        DateTime CREATEDdt = DateTime.Now;
                                        DateTime MODIFIEDdt = DateTime.Now;
                                        string CREATED = GetAttribute(node, "CREATED");
                                        string MODIFIED = GetAttribute(node, "MODIFIED");
                                        long unixTimeStampCREATED = Convert.ToInt64(CREATED);
                                        long unixTimeStampMODIFIED = Convert.ToInt64(MODIFIED);
                                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                        CREATEDdt = startTime.AddMilliseconds(unixTimeStampCREATED);
                                        MODIFIEDdt = startTime.AddMilliseconds(unixTimeStampMODIFIED);

                                        nodes.Add(new node
                                        {
                                            Text = node.Attributes["TEXT"].Value,
                                            mindmapName = fileName,
                                            mindmapPath = file.FullName,
                                            editDateTime = MODIFIEDdt,
                                            Time = CREATEDdt,
                                            IDinXML = node.Attributes["ID"].Value,
                                            ParentNodePath = father
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        foreach (XmlNode node in x.GetElementsByTagName("richcontent"))
                        {
                            try
                            {
                                if (node.Attributes["TEXT"] == null)
                                {
                                    continue;
                                }
                                if (!contents.Contains(node.Attributes["TEXT"].Value))
                                {
                                    DateTime CREATEDdt = DateTime.Now;
                                    DateTime MODIFIEDdt = DateTime.Now;
                                    string CREATED = GetAttribute(node, "CREATED");
                                    string MODIFIED = GetAttribute(node, "CREATED");
                                    long unixTimeStampCREATED = Convert.ToInt64(CREATED);
                                    long unixTimeStampMODIFIED = Convert.ToInt64(MODIFIED);
                                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                    CREATEDdt = startTime.AddMilliseconds(unixTimeStampCREATED);
                                    MODIFIEDdt = startTime.AddMilliseconds(unixTimeStampMODIFIED);
                                    nodes.Add(new node
                                    {
                                        Text = node.InnerText,
                                        mindmapName = fileName,
                                        mindmapPath = file.FullName,
                                        editDateTime = MODIFIEDdt,
                                        Time = CREATEDdt,
                                        IDinXML = node.Attributes["ID"].Value
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        //重新书写思维导图的创建时间
                        DateTime dt = DateTime.Today;
                        string reminder = GetAttribute(x.FirstChild.ChildNodes[x.FirstChild.ChildNodes.Count - 1], "CREATED");
                        if (reminder != "")
                        {
                            long unixTimeStamp = Convert.ToInt64(reminder);
                            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                            dt = startTime.AddMilliseconds(unixTimeStamp);
                            if (dt != DateTime.Today)
                            {
                                file.CreationTime = dt;
                            }
                        }
                        //如果project="17DAB3A24CC7NGK3HWY5ERX3AURZZAJ2PT99" project_last_home="file:/E:/yixiaozi/"
                        if (GetAttribute(x.FirstChild, "project")!= "17DAB3A24CC7NGK3HWY5ERX3AURZZAJ2PT99"|| GetAttribute(x.FirstChild, "project_last_home") != "file:/E:/yixiaozi/")
                        {
                            if (x.FirstChild.Attributes["project"] != null)
                            {
                                x.FirstChild.Attributes["project"].Value = "17DAB3A24CC7NGK3HWY5ERX3AURZZAJ2PT99";
                                x.FirstChild.Attributes["project_last_home"].Value = "file:/E:/yixiaozi/";
                                x.Save(file.FullName);
                                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(file.FullName));
                                th.Start();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        public void GetAllNodeIcon(DirectoryInfo path)
        {
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                if (!noFiles.Contains(file.Name) && file.Name[0] != '~')
                {
                    try
                    {
                        System.Xml.XmlDocument x = new XmlDocument();
                        x.Load(file.FullName);
                        string fileName = file.Name.Substring(0, file.Name.Length - 3);
                        List<string> contents = new List<string>();
                        foreach (XmlNode node in x.GetElementsByTagName("icon"))
                        {
                            try
                            {
                                //图标文件
                                if (node.Attributes["BUILTIN"].Value != "" && node.Attributes["BUILTIN"].Value != "button_ok")
                                {
                                    string filename = "";
                                    if (node.ParentNode.Attributes["TEXT"] == null)
                                    {
                                        foreach (XmlNode item in node.ParentNode.ChildNodes)
                                        {
                                            if (item.Attributes["TYPE"] != null && item.Attributes["TYPE"].Value == "NODE")
                                            {
                                                filename = new HtmlToString().StripHTML(((System.Xml.XmlElement)item).InnerXml).Replace("|", "").Replace("@", "").Replace("\r", "").Replace("\n", "");
                                                string link = GetAttribute(node.ParentNode, "LINK");
                                                if (filename != "")
                                                {
                                                    nodeIconString += filename;
                                                    nodeIconString += "|";
                                                    nodeIconString += Tools.GetFirstSpell(filename);
                                                    nodeIconString += "|";
                                                    nodeIconString += Tools.ConvertToAllSpell(filename);
                                                    nodeIconString += "|";
                                                    nodeIconString += Tools.GetFirstSpell(filename);
                                                    nodeIconString += "|";
                                                    nodeIconString += "true";
                                                    nodeIconString += "|";
                                                    nodeIconString += node.ParentNode.Attributes["ID"].Value;
                                                    nodeIconString += "|";
                                                    nodeIconString += file.FullName;
                                                    nodeIconString += "|";
                                                    nodeIconString += link;
                                                    nodeIconString += "@";
                                                }
                                                contents.Add(node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "");//可能会让没有ID的节点不显示，但是如果没有就不需要呀!

                                                break;
                                            }
                                        }
                                    }
                                    else if (!contents.Contains(node.ParentNode.Attributes["TEXT"].Value))
                                    {
                                        if (node.ParentNode.Attributes["TEXT"].Value != "")
                                        {
                                            string parentNodePath = GetFatherNodeName(node.ParentNode);
                                            string link = GetAttribute(node.ParentNode, "LINK");
                                            nodesicon.Add(new node
                                            {
                                                Text = node.ParentNode.Attributes["TEXT"].Value,
                                                mindmapName = fileName,
                                                mindmapPath = file.FullName,
                                                editDateTime = DateTime.Now,
                                                Time = DateTime.Now,
                                                IDinXML = node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "",
                                            });
                                            filename = node.ParentNode.Attributes["TEXT"].Value.Replace("|", "").Replace("@", "").Replace("\r", "").Replace("\n", "");
                                            nodeIconString += filename;
                                            nodeIconString += "|";
                                            nodeIconString += Tools.GetFirstSpell(filename);
                                            nodeIconString += "|";
                                            nodeIconString += Tools.ConvertToAllSpell(filename);
                                            nodeIconString += "|";
                                            nodeIconString += Tools.GetFirstSpell(filename);
                                            nodeIconString += "|";
                                            nodeIconString += "true";
                                            nodeIconString += "|";
                                            nodeIconString += node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "";
                                            nodeIconString += "|";
                                            nodeIconString += file.FullName;
                                            nodeIconString += "|";
                                            nodeIconString += parentNodePath;
                                            nodeIconString += "|";
                                            nodeIconString += link;
                                            nodeIconString += "@";
                                        }
                                        contents.Add(node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "");//可能会让没有ID的节点不显示，但是如果没有就不需要呀!
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        //将当前任务也添加进去，这样做方便
                        foreach (XmlNode node in x.GetElementsByTagName("hook"))
                        {
                            try
                            {
                                if (node.Attributes["NAME"].Value != "" && node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml")
                                {
                                    string filename = "";
                                    if (node.ParentNode.Attributes["TEXT"] == null)
                                    {
                                        foreach (XmlNode item in node.ParentNode.ChildNodes)
                                        {
                                            if (item.Attributes["TYPE"] != null && item.Attributes["TYPE"].Value == "NODE")
                                            {
                                                filename = new HtmlToString().StripHTML(((System.Xml.XmlElement)item).InnerXml).Replace("|", "").Replace("@", "").Replace("\r", "").Replace("\n", "");
                                                string link = GetAttribute(node.ParentNode, "LINK");
                                                if (filename != "")
                                                {
                                                    nodeIconString += filename;
                                                    nodeIconString += "|";
                                                    nodeIconString += Tools.GetFirstSpell(filename);
                                                    nodeIconString += "|";
                                                    nodeIconString += Tools.ConvertToAllSpell(filename);
                                                    nodeIconString += "|";
                                                    nodeIconString += Tools.GetFirstSpell(filename);
                                                    nodeIconString += "|";
                                                    nodeIconString += "true";
                                                    nodeIconString += "|";
                                                    nodeIconString += node.ParentNode.Attributes["ID"].Value;
                                                    nodeIconString += "|";
                                                    nodeIconString += file.FullName;
                                                    nodeIconString += "|";
                                                    nodeIconString += link;
                                                    nodeIconString += "@";
                                                }
                                                contents.Add(node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "");//可能会让没有ID的节点不显示，但是如果没有就不需要呀!

                                                break;
                                            }
                                        }
                                    }
                                    else if (!contents.Contains(node.ParentNode.Attributes["TEXT"].Value))
                                    {
                                        if (node.ParentNode.Attributes["TEXT"].Value != "")
                                        {
                                            string parentNodePath = GetFatherNodeName(node.ParentNode);
                                            string link = GetAttribute(node.ParentNode, "LINK");
                                            nodesicon.Add(new node
                                            {
                                                Text = node.ParentNode.Attributes["TEXT"].Value,
                                                mindmapName = fileName,
                                                mindmapPath = file.FullName,
                                                editDateTime = DateTime.Now,
                                                Time = DateTime.Now,
                                                IDinXML = node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "",
                                            });
                                            filename = node.ParentNode.Attributes["TEXT"].Value.Replace("|", "").Replace("@", "").Replace("\r", "").Replace("\n", "");
                                            nodeIconString += filename;
                                            nodeIconString += "|";
                                            nodeIconString += Tools.GetFirstSpell(filename);
                                            nodeIconString += "|";
                                            nodeIconString += Tools.ConvertToAllSpell(filename);
                                            nodeIconString += "|";
                                            nodeIconString += Tools.GetFirstSpell(filename);
                                            nodeIconString += "|";
                                            nodeIconString += "true";
                                            nodeIconString += "|";
                                            nodeIconString += node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "";
                                            nodeIconString += "|";
                                            nodeIconString += file.FullName;
                                            nodeIconString += "|";
                                            nodeIconString += parentNodePath;
                                            nodeIconString += "|";
                                            nodeIconString += link;
                                            nodeIconString += "@";
                                            contents.Add(node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null ? node.ParentNode.Attributes["ID"].Value : "");//可能会让没有ID的节点不显示，但是如果没有就不需要呀!
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        public void GetAllTimeBlock()
        {
            try
            {
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(ini.ReadString("TimeBlock", "mindmap", ""));
                List<string> contents = new List<string>();
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "事件类别")
                    {
                        GetTimeBlockItem(node);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void GetTimeBlockItem(XmlNode node)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode Subnode in node.ChildNodes)
                {
                    if (Subnode.Attributes["TEXT"] != null && Subnode.Attributes["TEXT"].Value != "未分类")
                    {
                        string nodename = Subnode.Attributes["TEXT"].Value;
                        //string father = GetFatherNodeName(Subnode).Replace(">","|").Replace("事件类别|","");
                        string father = GetFatherNodeName(Subnode).Replace("事件类别>", "");
                        string BackColor = Color.FromArgb(Int32.Parse((GetColor(Subnode).Replace("#", "ff")), System.Globalization.NumberStyles.HexNumber)).ToArgb().ToString();
                        timeblocks.Add(new node
                        {
                            Text = nodename,
                            mindmapName = nodename,
                            mindmapPath = father + "|" + nodename,
                            IDinXML = node != null && node.Attributes != null && node.Attributes["ID"] != null ? node.Attributes["ID"].Value : "",
                        });
                        //修改bug | xgbug | xiugaibug | xgbug | true | ID_1693863157 | 编程     | DocearReminder | 编程 | DocearReminder | -205
                        //疫情    | yq    | yiqing    | yq    | true | ID_1614751649 | 事件类别 | 事件类别        | -10066330
                        //第几个是ID呢？
                        timeblockString += nodename;
                        timeblockString += "|";
                        timeblockString += Tools.GetFirstSpell(nodename);
                        timeblockString += "|";
                        timeblockString += Tools.ConvertToAllSpell(nodename);
                        timeblockString += "|";
                        timeblockString += Tools.GetFirstSpell(nodename);
                        timeblockString += "|";
                        timeblockString += "true";
                        timeblockString += "|";
                        timeblockString += node != null && node.Attributes != null && node.Attributes["ID"] != null ? node.Attributes["ID"].Value : "";
                        timeblockString += "|";
                        timeblockString += father;
                        timeblockString += "|";
                        timeblockString += father;
                        timeblockString += "|";
                        timeblockString += BackColor;//link,记录颜色
                        timeblockString += "@";
                        GetTimeBlockItem(Subnode);
                    }
                }
            }
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
        //public static void CopyValue(object origin, object target)
        //{
        //    System.Reflection.PropertyInfo[] properties = (target.GetType()).GetProperties();
        //    System.Reflection.FieldInfo[] fields = (origin.GetType()).GetFields();
        //    for (int i = 0; i < fields.Length; i++)
        //    {
        //        for (int j = 0; j < properties.Length; j++)
        //        {
        //            if (fields[i].Name == properties[j].Name && properties[j].CanWrite)
        //            {
        //                properties[j].SetValue(target, fields[i].GetValue(origin), null);
        //            }
        //        }
        //    }
        //}

        public void EditMODIFIEDAndTaskName(XmlNode node, string TaskName)
        {
            try
            {
                if (node.Attributes["MODIFIEDLog"] == null)
                {
                    XmlAttribute MODIFIEDLog = node.OwnerDocument.CreateAttribute("MODIFIEDLog");
                    MODIFIEDLog.Value = node.Attributes["MODIFIED"].Value;
                    node.Attributes.Append(MODIFIEDLog);
                }
                else
                {
                    node.Attributes["MODIFIEDLog"].Value += ">" + node.Attributes["MODIFIED"].Value;
                }

                if (node.Attributes["TEXTLOG"] == null)
                {
                    XmlAttribute TEXTLOG = node.OwnerDocument.CreateAttribute("TEXTLOG");
                    TEXTLOG.Value = node.Attributes["TEXT"].InnerText;
                    node.Attributes.Append(TEXTLOG);
                }
                else
                {
                    node.Attributes["TEXTLOG"].Value += ">" + node.Attributes["TEXT"].InnerText;
                }
                node.Attributes["MODIFIED"].Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                node.Attributes["TEXT"].InnerText = TaskName;
            }
            catch (Exception ex)
            {
            }
        }

        public void NewFiles(DirectoryInfo path)
        {
            string str1 = "node";
            string str2 = "TEXT";
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                if (!noFiles.Contains(file.Name) && file.Name[0] != '~')
                {
                    try
                    {
                        System.Xml.XmlDocument x = new XmlDocument();
                        System.Xml.XmlDocument y = new XmlDocument();
                        x.Load(file.FullName);
                        bool needsave = false;
                        y = (XmlDocument)x.CloneNode(true);
                        foreach (XmlNode node in y.GetElementsByTagName(str1))
                        {
                            if (node.Attributes[str2] == null || node.Attributes["ID"] == null)
                            {
                                continue;
                            }
                            try
                            {
                                if (node.Attributes[str2].Value != "")
                                {
                                    string nodename = node.Attributes[str2].Value;//@"Folder|D|C:\下载";
                                    DirectoryInfo path1 = new DirectoryInfo(nodename.Split('|')[2]);
                                    string estr = "";
                                    if (nodename.Split('|').Length > 3)
                                    {
                                        estr = nodename.Split('|')[3];
                                    }
                                    string md5withDate = "";
                                    if (nodename.Split('|').Length > 4)
                                    {
                                        md5withDate = nodename.Split('|')[4];
                                    }
                                    string toTask = "";
                                    if (nodename.Split('|').Length > 5)
                                    {
                                        toTask = nodename.Split('|')[5];
                                    }
                                    bool toTaskbool = true;
                                    if (toTask != "")
                                    {
                                        toTaskbool = false;
                                    }
                                    if (nodename.StartsWith("Folder|D"))
                                    {
                                        foreach (FileInfo file1 in path1.GetFiles("*.*", SearchOption.AllDirectories))
                                        {
                                            if (file1.CreationTime <= DateTime.Today.AddDays(-7))
                                            {
                                                continue;
                                            }
                                            if (estr != "" && MyContains(file1.FullName, estr.Split(';')))
                                            {
                                                continue;
                                            }
                                            try
                                            {
                                                string md5 = GetMD5HashFromFile(file1.FullName) + (md5withDate == "" ? file1.CreationTime.ToOADate().ToString() : "") + file1.FullName;
                                                if (!node.InnerXml.Contains(md5))
                                                {
                                                    needsave = true;
                                                    AddFileTaskToMap(x, file.FullName, nodename, file1.Name, file1.FullName, md5, file1.CreationTime, "", toTaskbool);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }

                                    }
                                    if (nodename.StartsWith("Folder|F"))
                                    {
                                        foreach (FileInfo file1 in path1.GetFiles("*.*", SearchOption.AllDirectories))
                                        {
                                            if (file1.CreationTime <= DateTime.Today.AddDays(-7))
                                            {
                                                continue;
                                            }
                                            if (estr != "" && MyContains(file1.FullName, estr.Split(';')))
                                            {
                                                continue;
                                            }
                                            try
                                            {
                                                string md5 = GetMD5HashFromFile(file1.FullName) + (md5withDate == "" ? file1.CreationTime.ToOADate().ToString() : "") + file1.FullName;
                                                if (!node.InnerXml.Contains(md5))
                                                {
                                                    needsave = true;
                                                    AddFileTaskToMap(x, file.FullName, nodename, file1.Name, file1.FullName, md5, file1.CreationTime, file1.FullName.Substring(path.FullName.Length), toTaskbool);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        if (needsave)
                        {
                            x.Save(file.FullName);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(file.FullName));
                            th.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        public void GetAllNodeIconout()
        {
            nodesicon.Clear();
            nodeIconString = "";
            GetAllNodeIcon(rootrootpath);
            if (!System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"allnodeicon.json"))
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"allnodeicon.json", "");
            }
            else
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"allnodeicon.json", "");
            }
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"allnodeicon.json");
            JavaScriptSerializer js = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            string json = js.Serialize(nodesicon);
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
            RecordLogallnodesicon(nodeIconString);
        }
        public void GetTimeBlockOut()
        {
            timeblocks.Clear();
            timeblockString = "";
            GetAllTimeBlock();
            if (!System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.json"))
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.json", "");
            }
            else
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.json", "");
            }
            if (!System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.txt"))
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.txt", "");
            }
            else
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.txt", "");
            }
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.json");
            JavaScriptSerializer js = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            string json = js.Serialize(timeblocks);
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
            RecordTimeBlockJson(timeblockString);
        }

        public void GetAllNodeout()
        {
            if (nodes == null)
            {
                nodes = new List<node>();
            }
            nodes.Clear();
            GetAllNode(rootrootpath);
            if (!System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"allnode.json"))
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"allnode.json", "");
            }
            else
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"allnode.json", "");
            }
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"allnode.json");
            JavaScriptSerializer js = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            string json = js.Serialize(nodes);
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
        }
        public void GetAllNodeJsonFile()
        {
            try
            {
                Thread th = new Thread(() => GetAllNodeout())
                {
                    IsBackground = true
                };
                th.Start();
            }
            catch (Exception ex)
            {
                
            }
        }
        public void GetAllFilesJsonIconFile()
        {
            try
            {
                Thread th = new Thread(() => GetAllNodeIconout())
                {
                    IsBackground = true
                };
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GetTimeBlock()
        {
            try
            {
                Thread th = new Thread(() => GetTimeBlockOut())
                {
                    IsBackground = true
                };
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void NewFiles()
        {
            try
            {
                Thread th = new Thread(() => NewFiles(rootrootpath))
                {
                    IsBackground = true
                };
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void GetAllFiles(DirectoryInfo path)
        {
            foreach (FileInfo file in path.GetFiles("*.*"))
            {
                allfiles.Add(new node
                {
                    Text = file.FullName,
                    mindmapName = file.Name,
                    mindmapPath = file.FullName,
                    editDateTime = DateTime.Now,
                    Time = DateTime.Now
                });
            }
            if (path.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo subPath in path.GetDirectories())
                {
                    allfiles.Add(new node
                    {
                        Text = subPath.FullName,
                        mindmapName = subPath.Name,
                        mindmapPath = subPath.FullName,
                        editDateTime = DateTime.Now,
                        Time = DateTime.Now
                    });
                    GetAllFiles(subPath);
                }
            }
        }
        public void GetAllFilesout()
        {
            if (allfiles == null)
            {
                allfiles = new List<node>();
            }
            else
            {
                allfiles.Clear();
            }
            GetAllFiles(rootrootpath);
            //File.WriteAllText(@"allfiles.json", "");
            ClearTxt(System.AppDomain.CurrentDomain.BaseDirectory + @"allfiles.json");
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"allfiles.json");
            JavaScriptSerializer js = new JavaScriptSerializer
            {
                MaxJsonLength = Int32.MaxValue
            };
            string json = js.Serialize(allfiles);
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
        }
        public void GetAllFilesJsonFile()
        {
            try
            {
                Thread thfiles = new Thread(() => GetAllFilesout());
                thfiles.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion
        //将所有节点放到内存里，方便查询，不知道要用多大阿
        #region 未整理

        public static void RecordLogallnodesicon(string Content)
        {
            string logSite = AppDomain.CurrentDomain.BaseDirectory + "\\allnodesicon.txt";//本地文件
            StreamWriter sw = new StreamWriter(logSite, false, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(Content);
            sw.Close();
            sw.Dispose();
        }
        public static void RecordTimeBlockJson(string Content)
        {
            string logSite = AppDomain.CurrentDomain.BaseDirectory + "\\timeblock.txt";//本地文件
            StreamWriter sw = new StreamWriter(logSite, false, Encoding.GetEncoding("GB2312"));
            sw.WriteLine(Content);
            sw.Close();
            sw.Dispose();
        }

        public void ClearTxt(String txtPath)
        {
            FileStream stream = File.Open(txtPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            stream.Close();
        }

        //获取包含任务的导图
        public void LoadFile(DirectoryInfo path)
        {
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                if (mindmapfiles.FirstOrDefault(m => m.filePath == file.FullName) == null)//如果创建的文件，rr就可以获取到了。（文件要在当前文件范围之内）
                {
                    mindmapfiles.Add(new mindmapfile { name = file.Name.Substring(0, file.Name.Length - 3), filePath = file.FullName });
                }
                string subPath = file.DirectoryName;
                if (!noFiles.Contains(file.Name) && file.Name[0] != '~' && !".svn".Contains(subPath) && (allFloder || (PathcomboBox.SelectedItem.ToString() == "rootPath" && !MyContains(file.FullName, noFolder)) || PathcomboBox.SelectedItem.ToString() != "rootPath") && subPath[0] != '.')
                {
                    try
                    {
                        if (!remindmapsList.Contains(file.FullName))
                        {
                            //暂时取消remindmapsList的作用，因为避免总是需要考虑这个问题。
                            //continue;
                        }
                        string str1 = "hook";
                        string str2 = "NAME";
                        string str3 = "plugins/TimeManagementReminder.xml";
                        System.Xml.XmlDocument x = new XmlDocument();
                        x.Load(file.FullName);
                        int number = 0;
                        foreach (XmlNode node in x.GetElementsByTagName(str1))
                        {
                            try
                            {
                                if (node.Attributes != null && node.Attributes[str2] != null && node.Attributes[str2].Value == str3)
                                {
                                    if (node.ParentNode.Attributes["TEXT"] != null && node.ParentNode.Attributes["TEXT"].Value != "bin")
                                    {
                                        number++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        if (number > 0)
                        {
                            string todayaddnodecount = "";
                            if (nodes != null)
                            {
                                todayaddnodecount = nodes.Count(m => m.mindmapPath.Contains(file.Name) && (m.Time - DateTime.Today).TotalMinutes > 0).ToString();
                                if (todayaddnodecount == "0")
                                {
                                    todayaddnodecount = "";
                                }
                                else
                                {
                                    todayaddnodecount = "|" + todayaddnodecount;
                                }
                            }
                            mindmaplist.Items.Insert(0, new MyListBoxItem { Text = lenghtString(number.ToString(), 2) + " " + file.Name.Substring(0, file.Name.Length - 3) + todayaddnodecount, Value = file.FullName });

                            taskcount.Text = (Convert.ToInt64(taskcount.Text) + number).ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(file.FullName);
                    }
                }
            }
            mindmaplist.Sorted = false;
            mindmaplist.Sorted = true;
        }
        //public void AddmindmapfilesOnly(DirectoryInfo path)
        //{
        //    try{
        //        foreach (FileInfo file in path.GetFiles("*.mm"))
        //        {
        //            if (!noFiles.Contains(file.Name) && file.Name[0] != '~')
        //            {
        //                try
        //                {
        //                    if (mindmapfiles.FirstOrDefault(m => m.filePath == file.FullName) == null)
        //                    {
        //                        mindmapfiles.Add(new mindmapfile { name = file.Name.Substring(0, file.Name.Length - 3), filePath = file.FullName });
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                }
        //            }
        //        }
        //        if (path.GetDirectories().Length > 0)
        //        {
        //            foreach (DirectoryInfo subPath in path.GetDirectories())
        //            {
        //                //需要排除的文件夹
        //                if (!".svn".Contains(subPath.Name) && (allFloder || (!allFloder && !noFolder.Contains(subPath.Name))) && subPath.Name[0] != '.')
        //                {
        //                    AddmindmapfilesOnly(subPath);
        //                }
        //                else
        //                {
        //                    if (noFolder.Contains(subPath.Name))
        //                    {
        //                        AddmindmapfilesOnly(subPath);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //    }
        //}
        public static Reminder reminderObject = new Reminder();
        public void SetTimeBlockLasTime()
        {
            //避免reminderObject为空
            if (reminderObject == null || reminderObject.reminders == null || reminderObject.reminders.Count == 0)
            {
                return;
            }
            try
            {
                //将TimeBlockLastTime设置
                TimeBlocklastTime = reminderObject.reminders.Where(m => m.time < DateTime.Now).OrderBy(m => m.TimeEnd).LastOrDefault(m => m.mindmap == "TimeBlock").TimeEnd;
                //TimeBlocklastTime = TimeBlocklastTime.AddMinutes(reminderObject.reminders.Where(m => m.time < DateTime.Now).OrderBy(m => m.TimeEnd).LastOrDefault(m => m.mindmap == "TimeBlock").tasktime);
            }
            catch (Exception ex)
            {
            }
        }
        //选中任务列表的第N行
        public void ReminderListSelectedIndex(int index)
        {
            if (reminderList.Items.Count > index)
            {
                reminderList.SelectedIndex = index;
                reminderList.SelectedItem = reminderList.Items[index];
            }
        }
        /// <summary>
        /// 刷新任务控件
        /// </summary>
        public void RRReminderlist()
        {
            //清空值
            timeblockcheck.Text = Hours.Text = fathernode.Text = "";

            SetTimeBlockLasTime();

            #region 显示时间块
            if (showTimeBlock.Checked)
            {
                reminderList.Items.Clear();
                reminderListBox.Visible = false;
                int beginDateDiff = 0;
                string searchWords = searchword.Text;
                if (searchword.Text != "" && searchword.Text.Contains(" "))
                {
                    try
                    {
                        beginDateDiff = Convert.ToInt32(searchword.Text.Split(' ')[1]);
                        searchWords = searchword.Text.Split(' ')[0];
                    }
                    catch (Exception ex)
                    {
                    }
                }
                foreach (ReminderItem item in reminderObject.reminders.Where(m => m.time >= TimeBlockDate.Value.AddDays(0 - beginDateDiff) && m.time < TimeBlockDate.Value.AddDays(1) && m.mindmap == "TimeBlock" && (searchword.Text == "" || (searchword.Text != "" && (m.name.SafeToString().Contains(searchWords) || m.comment.SafeToString().Contains(searchWords) || m.DetailComment.SafeToString().Contains(searchWords) || m.nameFull.SafeToString().Contains(searchWords))))).OrderBy(m => m.time))
                {
                    if (OnlyLevel.Checked && item.tasklevel != 0)
                    {
                        continue;
                    }
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = (searchWords == "" ? item.time.ToString("    HH:mm") : item.time.ToString("yyyy-MM-dd HH:mm")) + FormatTimeLenght(Convert.ToInt16(item.tasktime).ToString(), 4) + "  " + item.name + (item.comment != "" ? "(" : "") + item.comment + (item.comment != "" ? ")" : "") + (item.DetailComment != null && item.DetailComment != "" ? "*" : ""),
                        Name = item.name,
                        Time = item.time,
                        Value = "TimeBlock",
                        IsShow = true,
                        remindertype = item.DetailComment,
                        rhours = 0,
                        rdays = 0,
                        rMonth = 00,
                        rWeek = 0,
                        ryear = 0,
                        rtaskTime = Convert.ToInt16(item.tasktime),
                        IsDaka = item.comment,
                        IDinXML = item.ID,
                        link = item.nameFull,
                        level = item.tasklevel
                    });
                }
                //更新简要信息
                UpdateSummary();
                reminderList.Focus();
            }
            else if (ShowMoney.Checked)
            {
                reminderList.Items.Clear();
                reminderListBox.Visible = false;
                int beginDateDiff = 0;
                string searchWords = searchword.Text;
                if (searchword.Text != "" && searchword.Text.Contains(" "))
                {
                    try
                    {
                        beginDateDiff = Convert.ToInt32(searchword.Text.Split(' ')[1]);
                        searchWords = searchword.Text.Split(' ')[0];
                    }
                    catch (Exception ex)
                    {
                    }
                }
                foreach (ReminderItem item in reminderObject.reminders.Where(m => m.time >= MoneyDateTimePicker.Value.AddDays(0 - beginDateDiff) && m.time < MoneyDateTimePicker.Value.AddDays(1) && m.mindmap == "Money" && (searchword.Text == "" || (searchword.Text != "" && (m.name.SafeToString().Contains(searchWords) || m.comment.SafeToString().Contains(searchWords) || m.DetailComment.SafeToString().Contains(searchWords) || m.nameFull.SafeToString().Contains(searchWords))))).OrderBy(m => m.time))
                {
                    if (OnlyLevel.Checked && item.tasklevel != 0)
                    {
                        continue;
                    }
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = (searchWords == "" ? item.time.ToString("   HH:mm") : item.time.ToString("yyyy-MM-dd HH:mm")) + FormatTimeLenght(Convert.ToInt16(item.tasktime).ToString(), 4) + "元  " + item.name + (item.comment != "" ? "(" : "") + item.comment + (item.comment != "" ? ")" : "") + (item.DetailComment != null && item.DetailComment != "" ? "*" : ""),
                        Name = item.name,
                        Time = item.time,
                        Value = "Money",
                        IsShow = true,
                        remindertype = item.DetailComment,
                        rhours = 0,
                        rdays = 0,
                        rMonth = 00,
                        rWeek = 0,
                        ryear = 0,
                        rtaskTime = Convert.ToInt16(item.tasktime),
                        IsDaka = item.comment,
                        IDinXML = item.ID,
                        link = item.nameFull,
                        level = item.tasklevel
                    });
                }
                //更新简要信息
                UpdateSummary();
                reminderList.Focus();
            }
            else if (ShowKA.Checked)
            {
                reminderList.Items.Clear();
                reminderListBox.Visible = false;
                int beginDateDiff = 0;
                string searchWords = searchword.Text;
                if (searchword.Text != "" && searchword.Text.Contains(" "))
                {
                    try
                    {
                        beginDateDiff = Convert.ToInt32(searchword.Text.Split(' ')[1]);
                        searchWords = searchword.Text.Split(' ')[0];
                    }
                    catch (Exception ex)
                    {
                    }
                }
                foreach (ReminderItem item in reminderObject.reminders.Where(m => m.time >= KADateTimePicker.Value.AddDays(0 - beginDateDiff) && m.time < KADateTimePicker.Value.AddDays(1) && m.mindmap == "KA" && (searchword.Text == "" || (searchword.Text != "" && (m.name.SafeToString().Contains(searchWords) || m.comment.SafeToString().Contains(searchWords) || m.DetailComment.SafeToString().Contains(searchWords) || m.nameFull.SafeToString().Contains(searchWords))))).OrderBy(m => m.time))
                {
                    if (OnlyLevel.Checked && item.tasklevel != 0)
                    {
                        continue;
                    }
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = (searchWords == "" ? item.time.ToString("   HH:mm") : item.time.ToString("yyyy-MM-dd HH:mm")) + FormatTimeLenght(Convert.ToInt16(item.tasktime * 10 / 9.46).ToString(), 4) + "克脂肪  " + item.name + (item.comment != "" ? "(" : "") + item.comment + (item.comment != "" ? ")" : "") + (item.DetailComment != null && item.DetailComment != "" ? "*" : ""),
                        //((args.EndDate - args.StartDate).TotalMinutes * 10/9.46).ToString("F")+"克脂肪"
                        Name = item.name,
                        Time = item.time,
                        Value = "TimeBlock",
                        IsShow = true,
                        remindertype = item.DetailComment,
                        rhours = 0,
                        rdays = 0,
                        rMonth = 00,
                        rWeek = 0,
                        ryear = 0,
                        rtaskTime = Convert.ToInt16(item.tasktime),
                        IsDaka = item.comment,
                        IDinXML = item.ID,
                        link = item.nameFull,
                        level = item.tasklevel
                    });
                }
                //更新简要信息
                UpdateSummary();
                reminderList.Focus();
            }
            ReminderlistBoxChange();
            if ((showTimeBlock.Checked || ShowKA.Checked || ShowMoney.Checked))
            {
                if (OnlyLevel.Checked)
                {
                    OnlyLevel.Checked = false;
                    RRReminderlist();
                }
                return;
            }
            #endregion
            //if (mindmapSearch.Text != "")//清空一下这里的值，不然总是显示，很难受
            //{
            //    mindmapSearch.Text = "";
            //}
            reminderSelectIndex = reminderList.SelectedIndex;
            int task = 0;
            int ctask = 0;//周期任务个数
            int vtask = 0;//不重要任务数量
            int ebtask = 0;//Eb类型任务
            int passtask = 0;
            int isviewtask = 0;
            bool hasMorning = false;
            bool hasNoon = false;
            bool hasAfter = false;
            bool hasNight = false;
            List<MyListBoxItemRemind> reminderlistItems = new List<MyListBoxItemRemind>();

            IsEncryptBool = false;
            foreach (ReminderItem item in reminderObject.reminders.Where(m => !m.isCompleted))
            {
                if (mindmaplist.CheckedItems.Cast<MyListBoxItem>().Any(m => m.Value.IndexOf(item.mindmap) > 0))
                {
                    item.isCurrect = false;
                    item.isNew = false;
                    item.isview = false;
                    item.isEBType = false;
                }
            }
            if (!c_ViewModel.Checked && mindmapornode.Text == "")
            {
                reminderList.Items.Clear();
                //如果SS的时候只能当前类型的。
                List<MyListBoxItem> showMindmaps = new List<MyListBoxItem>();
                if (searchword.Text.StartsWith("ss"))
                {
                    foreach (MyListBoxItem item in mindmaplist_all)
                    {
                        showMindmaps.Add(item);
                    }
                }
                else
                {
                    CustomCheckedListBox.CheckedItemCollection sourceMindmap = mindmaplist.CheckedItems;
                    foreach (MyListBoxItem item in sourceMindmap)
                    {
                        showMindmaps.Add(item);
                    }
                }
                foreach (MyListBoxItem path in showMindmaps)
                {
                    if (!path.Value.EndsWith("mm"))
                    {
                        return;
                    }
                    string str1 = "hook";
                    string str2 = "NAME";
                    string str3 = "plugins/TimeManagementReminder.xml";
                    System.Xml.XmlDocument x = new XmlDocument();
                    try
                    {
                        x.Load(path.Value);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    if (x.GetElementsByTagName(str1).Count == 0)
                    {
                        continue;
                    }
                    if (path.Text == "bin")
                    {
                        //continue;
                    }
                    foreach (XmlNode node in x.GetElementsByTagName(str1))
                    {
                        try
                        {
                            if (node.Attributes[str2].Value == str3)
                            {
                                string reminder = "";
                                DateTime dt = DateTime.Now;
                                DateTime jiniantimeDT = new DateTime();
                                DateTime? jiniantimeDT1 = null;
                                DateTime endtimeDT = new DateTime();
                                DateTime? endtimeDT1 = null;
                                if (node.InnerXml != "")
                                {
                                    reminder = node.InnerXml.Split('\"')[1];
                                    long unixTimeStamp = Convert.ToInt64(reminder);
                                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                    dt = startTime.AddMilliseconds(unixTimeStamp);
                                }
                                else
                                {
                                    reminder = GetAttribute(node.ParentNode, "RememberTime");
                                    if (reminder == "")
                                    {
                                    }
                                    else
                                    {
                                        long unixTimeStamp = Convert.ToInt64(reminder);
                                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                        dt = startTime.AddMilliseconds(unixTimeStamp);
                                    }
                                }
                                //添加提醒到提醒清单
                                string dakainfo = "";
                                if (GetAttribute(node.ParentNode, "ISDAKA") == "true")
                                {
                                    dakainfo = " | " + GetAttribute(node.ParentNode, "DAKADAY");
                                }
                                //纪念日
                                string jinianriInfo = "";
                                if (GetAttribute(node.ParentNode, "IsJinian") == "true")
                                {
                                    try
                                    {
                                        string JinianBeginTime = GetAttribute(node.ParentNode, "JinianBeginTime");
                                        long unixTimeStamp = Convert.ToInt64(JinianBeginTime);
                                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                        jiniantimeDT = startTime.AddMilliseconds(unixTimeStamp);
                                        jiniantimeDT1 = startTime.AddMilliseconds(unixTimeStamp);
                                        jinianriInfo = " |Start:" + jiniantimeDT.ToString("yy-MM-dd") + ":" + GetTimeSpanStr(Convert.ToInt32((DateTime.Now - jiniantimeDT).TotalDays));
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                                //结束日
                                string EndDateInfo = "";
                                if (GetAttribute(node.ParentNode, "EndDate") != "")
                                {
                                    string enddatetime = GetAttribute(node.ParentNode, "EndDate");
                                    long unixTimeStamp = Convert.ToInt64(enddatetime);
                                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                    endtimeDT = startTime.AddMilliseconds(unixTimeStamp);
                                    endtimeDT1 = startTime.AddMilliseconds(unixTimeStamp);
                                    EndDateInfo = " |End To:" + endtimeDT.ToString("yy-MM-dd") + "";
                                    EndDateInfo += ":" + GetTimeSpanStr(Convert.ToInt32((endtimeDT - DateTime.Now).TotalDays)) + "";
                                }
                                int editTime = 0;
                                string taskid = GetAttribute(node.ParentNode, "ID");
                                IEnumerable<ReminderItem> reminderthis = reminderObject.reminders.Where(m => m.ID == taskid && m.mindmap == path.Value.Split('\\')[path.Value.Split('\\').Length - 1].Split('.')[0]);
                                if (reminderthis.Count() > 0)
                                {
                                    foreach (ReminderItem item in reminderthis)
                                    {
                                        if (item.ID == taskid && item.mindmap == path.Value.Split('\\')[path.Value.Split('\\').Length - 1].Split('.')[0])
                                        {
                                            item.isCurrect = true;
                                            item.isNew = false;
                                            item.remindertype = GetAttribute(node.ParentNode, "REMINDERTYPE");
                                            item.rdays = MyToInt16(GetAttribute(node.ParentNode, "RDAYS"));
                                            item.rMonth = MyToInt16(GetAttribute(node.ParentNode, "RMONTH"));
                                            item.rWeek = MyToInt16(GetAttribute(node.ParentNode, "RWEEK"));
                                            item.name = GetAttribute(node.ParentNode, "TEXT");
                                            item.rweeks = GetAttribute(node.ParentNode, "RWEEKS").ToCharArray();
                                            item.ryear = MyToInt16(GetAttribute(node.ParentNode, "RYEAR"));
                                            item.tasktime = MyToInt16(GetAttribute(node.ParentNode, "TASKTIME"));
                                            item.tasklevel = MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL"));
                                            item.ebstring = MyToInt16(GetAttribute(node.ParentNode, "EBSTRING"));
                                            item.mindmapPath = path.Value;
                                            item.isCompleted = false;
                                            if (item.time.ToString("yyyy/MM/dd HH:mm") != dt.ToString("yyyy/MM/dd HH:mm"))
                                            {
                                                item.time = dt;
                                                item.editCount += 1;
                                                reminderObject.editCount += 1;
                                                if (item.editTime == null)
                                                {
                                                    item.editTime = new List<DateTime>();
                                                }
                                                item.editTime.Add(DateTime.Now);
                                                break;
                                            }
                                            editTime = item.editCount;
                                            item.ID = GetAttribute(node.ParentNode, "ID");
                                            item.isview = GetAttribute(node.ParentNode, "ISVIEW") == "true" || MyToBoolean(GetAttribute(node.ParentNode, "ISReminderOnly"));
                                            item.isEBType = GetAttribute(node.ParentNode, "REMINDERTYPE") == "eb";
                                            item.JinianDate = jiniantimeDT1;
                                            item.EndDate = endtimeDT1;
                                        }
                                    }
                                }
                                else
                                {
                                    ReminderItem newitem = new ReminderItem
                                    {
                                        name = GetAttribute(node.ParentNode, "TEXT"),
                                        mindmap = path.Value.Split('\\')[path.Value.Split('\\').Length - 1].Split('.')[0],
                                        time = dt,
                                        isNew = true,
                                        remindertype = GetAttribute(node.ParentNode, "REMINDERTYPE"),
                                        rhours = MyToInt16(GetAttribute(node.ParentNode, "RHOURS")),
                                        rdays = MyToInt16(GetAttribute(node.ParentNode, "RDAYS")),
                                        rMonth = MyToInt16(GetAttribute(node.ParentNode, "RMONTH")),
                                        rWeek = MyToInt16(GetAttribute(node.ParentNode, "RWEEK")),
                                        rweeks = GetAttribute(node.ParentNode, "RWEEKS").ToCharArray(),
                                        ryear = MyToInt16(GetAttribute(node.ParentNode, "RYEAR")),
                                        tasktime = MyToInt16(GetAttribute(node.ParentNode, "TASKTIME")),
                                        tasklevel = MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")),
                                        ebstring = MyToInt16(GetAttribute(node.ParentNode, "EBSTRING")),
                                        mindmapPath = path.Value,
                                        ID = GetAttribute(node.ParentNode, "ID"),
                                        isview = GetAttribute(node.ParentNode, "ISVIEW") == "true" || MyToBoolean(GetAttribute(node.ParentNode, "ISReminderOnly")),
                                        isEBType = GetAttribute(node.ParentNode, "REMINDERTYPE") == "eb",
                                        JinianDate = jiniantimeDT1,
                                        EndDate = endtimeDT1
                                    };
                                    reminderObject.reminders.Add(newitem);
                                    reminderObject.reminderCount += 1;
                                }

                                //剩余打开次数
                                string LeftDakaDays = "";
                                if (GetAttribute(node.ParentNode, "LeftDakaDays") != "")
                                {
                                    LeftDakaDays = " | [" + GetAttribute(node.ParentNode, "LeftDakaDays") + "次]";
                                    if (LeftDakaDays.Contains("[0次"))
                                    {
                                        LeftDakaDays = string.Empty;
                                    }
                                }

                                bool IsShow = true;
                                //if (ISLevel.Checked)
                                //{
                                //    if (MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")) != tasklevel.Value && reminderList.SelectedIndex < 0)
                                //    {
                                //        IsShow = false;
                                //    }
                                //}
                                //else
                                //{
                                //    //if (MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")) < tasklevel.Value && reminderList.SelectedIndex < 0)
                                //    //{
                                //    //    IsShow = false;
                                //    //}
                                //}
                                //逆反，不需要了，按钮也删除掉
                                //if (nifan.Checked && reminderlist.SelectedIndex < 0)
                                //{
                                //    IsShow = !IsShow;
                                //}
                                //show if less than today not now
                                if (dt < DateTime.Today)
                                {
                                    IsShow = true;
                                }
                                else if ((dt.Day.Equals(DateTime.Now.Day) && dt.Month.Equals(DateTime.Now.Month) && dt.Year.Equals(DateTime.Now.Year)))//当天
                                {
                                    if (dt.Hour <= 8 && !morning.Checked)
                                    {
                                        IsShow = false;
                                    }
                                    if (dt.Hour <= 8)
                                    {
                                        hasMorning = true;
                                    }
                                    if (dt.Hour > 8 && dt.Hour < 12 && !day.Checked)
                                    {
                                        IsShow = false;
                                    }
                                    if (dt.Hour > 8 && dt.Hour < 12)
                                    {
                                        hasNoon = true;
                                    }
                                    if (dt.Hour >= 12 && dt.Hour < 18 && !afternoon.Checked)
                                    {
                                        IsShow = false;
                                    }
                                    if (dt.Hour >= 12 && dt.Hour < 18)
                                    {
                                        hasAfter = true;
                                    }
                                    if (dt.Hour >= 18)
                                    {
                                        hasNight = true;
                                    }
                                    if (dt.Hour >= 18 && dt.Hour < 24 && !night.Checked)
                                    {
                                        IsShow = false;
                                    }
                                }
                                else if (dt < (DateTime.Now.AddDays(1).AddHours(24 - DateTime.Now.Hour)))//明天
                                {
                                    if (!showtomorrow.Checked)
                                    {
                                        IsShow = false;
                                    }
                                }
                                else if ((true) && (dt) < DateTime.Now.AddDays(7).AddHours(24 - DateTime.Now.Hour))//一周
                                {
                                    if (!reminder_week.Checked)
                                    {
                                        IsShow = false;
                                    }
                                }
                                else if ((true) && dt < DateTime.Now.AddDays(30).AddHours(24 - DateTime.Now.Hour))//一个月
                                {
                                    if (!reminder_month.Checked)
                                    {
                                        IsShow = false;
                                    }
                                }
                                else if ((true) && (dt) < DateTime.Now.AddDays(365).AddHours(24 - DateTime.Now.Hour))//一年
                                {
                                    if (!reminder_year.Checked)
                                    {
                                        IsShow = false;
                                    }
                                }
                                else if ((true) && (dt) >= DateTime.Now.AddDays(365).AddHours(24 - DateTime.Now.Hour))//一年以后
                                {
                                    if (!reminder_yearafter.Checked)
                                    {
                                        IsShow = false;
                                    }
                                }
                                bool timebool = IsShow;
                                bool iSReminderOnly = MyToBoolean(GetAttribute(node.ParentNode, "ISReminderOnly"));

                                string tasktype = GetAttribute(node.ParentNode, "REMINDERTYPE");
                                //GetAttribute(node.ParentNode, "ISVIEW") == "true"
                                if (IsShow)
                                {
                                    //只要是查看任务，就加1
                                    if (iSReminderOnly)
                                    {
                                        vtask++;
                                    }
                                    else
                                    {
                                        if (tasktype != "")
                                        {
                                            if (tasktype == "eb")
                                            {
                                                ebtask++;
                                            }
                                            else
                                            {
                                                ctask++;
                                            }
                                        }
                                        else
                                        {
                                            task++;
                                        }
                                    }
                                }
                                // 判断是什么类型的标签
                                //不显示周期的时候，所有周期类型的都不显示
                                if (!showcyclereminder.Checked && (GetAttribute(node.ParentNode, "REMINDERTYPE") == "day" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "week" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "month" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "year" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "hour"))
                                {
                                    IsShow = false;
                                }
                                //选择周期时，且只选择周期时，所有非周期都不显示
                                else if (showcyclereminder.Checked && onlyZhouqi.Checked && !(GetAttribute(node.ParentNode, "REMINDERTYPE") == "day" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "week" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "month" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "year" || GetAttribute(node.ParentNode, "REMINDERTYPE") == "hour"))
                                {
                                    IsShow = false;
                                }
                                if (!ebcheckBox.Checked && GetAttribute(node.ParentNode, "REMINDERTYPE") == "eb")
                                {
                                    IsShow = false;
                                }
                                if (ebcheckBox.Checked && GetAttribute(node.ParentNode, "REMINDERTYPE") != "eb")
                                {
                                    IsShow = false;
                                }
                                if (searchword.Text != "" && !searchword.Text.StartsWith("ss"))
                                {
                                    if (node.ParentNode.Attributes["TEXT"].Value.Contains(searchword.Text.Trim()) || node.ParentNode.InnerXml.Contains(searchword.Text.Trim()))//可以搜索子任务了
                                    {
                                        IsShow = true;
                                    }
                                    else
                                    {
                                        IsShow = false;
                                    }
                                }
                                if (!c_ViewModel.Checked && GetAttribute(node.ParentNode, "ISVIEW") == "true")
                                {
                                    IsShow = false;
                                }
                                if (GetAttribute(node.ParentNode, "ISVIEW") == "true")
                                {
                                    isviewtask++;
                                    if (tasktype != "")
                                    {
                                        if (tasktype == "eb")
                                        {
                                            ebtask--;
                                        }
                                        else
                                        {
                                            ctask--;
                                        }
                                    }
                                    else
                                    {
                                        if (iSReminderOnly)
                                        {
                                            vtask--;
                                        }
                                        else
                                        {
                                            task--;
                                        }
                                    }
                                }
                                string taskName = "";
                                string taskNameDis = "";
                                bool isEncrypted = false;
                                taskName = GetAttribute(node.ParentNode, "TEXT");
                                if (taskName.Length > 6)
                                {
                                    if (taskName.Substring(0, 3) == "***")
                                    {
                                        passtask++;
                                        if (tasktype != "")
                                        {
                                            if (tasktype == "eb")
                                            {
                                                ebtask--;
                                            }
                                            else
                                            {
                                                ctask--;
                                            }
                                        }
                                        else
                                        {
                                            if (iSReminderOnly)
                                            {
                                                vtask--;
                                            }
                                            else
                                            {
                                                task--;
                                            }
                                        }
                                        if (PassWord == "")
                                        {
                                            IsShow = false;
                                        }
                                        else
                                        {
                                            taskName = encrypt.DecryptString(node.ParentNode.Attributes["TEXT"].Value);
                                            isEncrypted = true;
                                        }
                                    }
                                }
                                taskNameDis = taskName;
                                if (IsFileUrl(taskName))
                                {
                                    if (Path.GetExtension(taskName) != "")
                                    {
                                        taskNameDis = "#" + Path.GetFileName(taskName);
                                    }
                                    else
                                    {
                                        taskNameDis = "Path:" + Path.GetFullPath(taskName).Split('\\').Last(m => m != "");
                                    }
                                }
                                if (c_Jinian.Checked)
                                {
                                    if (jinianriInfo != "")
                                    {
                                        IsShow = true;
                                    }
                                    else
                                    {
                                        IsShow = false;
                                    }
                                }
                                if (c_endtime.Checked)
                                {
                                    if (EndDateInfo != "")
                                    {
                                        IsShow = true;
                                    }
                                    else
                                    {
                                        IsShow = false;
                                    }
                                }
                                //搜索任务模式
                                if (searchword.Text.StartsWith("ss"))
                                {
                                    string searchwordText = searchword.Text.Substring(2);
                                    if (taskName.Contains(searchwordText) || Tools.ConvertToAllSpell(taskName).Contains(searchwordText) || Tools.GetFirstSpell(taskName).Contains(searchwordText))
                                    {
                                        IsShow = true;
                                    }
                                    else
                                    {
                                        IsShow = false;
                                    }
                                }
                                //处理提醒 
                                if (timebool)
                                {
                                    if (IsReminderOnlyCheckBox.Checked)
                                    {
                                        if (iSReminderOnly)
                                        {
                                            IsShow = true;
                                        }
                                        else
                                        {
                                            IsShow = false;
                                        }

                                    }
                                    else
                                    {
                                        if (iSReminderOnly)
                                        {
                                            IsShow = false;
                                        }
                                        else
                                        {
                                        }
                                    }

                                }
                                if (showwaittask)
                                {
                                    if (GetAttribute(node.ParentNode, "ISVIEW") == "true")
                                    {
                                        IsShow = true;
                                    }
                                    else
                                    {
                                        IsShow = false;
                                    }
                                }
                                //判断ID的重复，避免在Box中同时显示
                                string nodeid = GetAttribute(node.ParentNode, "ID");
                                if (reminderboxList.Where(m => m.IDinXML == nodeid).Count() > 0)
                                {
                                    IsShow = false;
                                }
                                if (IsFileUrl(GetAttribute(node.ParentNode, "LINK").Replace("file:/", "")) && !System.IO.File.Exists(GetAttribute(node.ParentNode, "LINK").Replace("file:/", "")))
                                {
                                    IsShow = false;
                                }
                                if (Xnodes.Any(m => m.Contains(nodeid)) && reminderboxList.Where(m => m.IDinXML == nodeid).Count() == 0)
                                {
                                    if (taskName.ToLower() != "bin")
                                    {
                                        if (isZhuangbi)
                                        {
                                            string patten = @"(\S)";
                                            Regex reg = new Regex(patten);
                                            taskNameDis = reg.Replace(taskNameDis, "*");
                                        }
                                        reminderboxList.Add(new MyListBoxItemRemind
                                        {
                                            Text = (c_Jinian.Checked ? jiniantimeDT.ToString("dd HH:mm") : (c_endtime.Checked ? endtimeDT.ToString("dd HH:mm") : dt.ToString("dd HH:mm"))) + @"" + GetAttribute(node.ParentNode, "TASKTIME", 4) + @" " + taskNameDis + dakainfo + LeftDakaDays + jinianriInfo + EndDateInfo,
                                            Name = taskName,
                                            Time = dt,
                                            jinianDatetime = jiniantimeDT,
                                            Value = path.Value,
                                            IsShow = IsShow,
                                            remindertype = GetAttribute(node.ParentNode, "REMINDERTYPE"),
                                            rhours = MyToInt16(GetAttribute(node.ParentNode, "RHOUR")),
                                            rdays = MyToInt16(GetAttribute(node.ParentNode, "RDAYS")),
                                            rMonth = MyToInt16(GetAttribute(node.ParentNode, "RMONTH")),
                                            rWeek = MyToInt16(GetAttribute(node.ParentNode, "RWEEK")),
                                            rweeks = GetAttribute(node.ParentNode, "RWEEKS").ToCharArray(),
                                            ryear = MyToInt16(GetAttribute(node.ParentNode, "RYEAR")),
                                            rtaskTime = MyToInt16(GetAttribute(node.ParentNode, "TASKTIME")),
                                            IsDaka = GetAttribute(node.ParentNode, "ISDAKA"),
                                            IsView = GetAttribute(node.ParentNode, "ISVIEW"),
                                            DakaDay = MyToInt16(GetAttribute(node.ParentNode, "DAKADAY")),
                                            level = MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")),
                                            ebstring = MyToInt16(GetAttribute(node.ParentNode, "EBSTRING")),
                                            DakaDays = StrToInt(GetAttribute(node.ParentNode, "DAKADAYS").Split(',')),
                                            editTime = editTime,
                                            isEncrypted = isEncrypted,
                                            IDinXML = GetAttribute(node.ParentNode, "ID"),
                                            link = GetAttribute(node.ParentNode, "LINK"),
                                            rootPath = rootpath.FullName,
                                            ISReminderOnly = iSReminderOnly,
                                            Content = ((System.Xml.XmlElement)node.ParentNode).InnerXml
                                        });
                                        IsShow = false;
                                    }
                                }
                                if (IsShow)
                                {
                                    if (taskName.ToLower() != "bin")
                                    {
                                        if (isZhuangbi)
                                        {
                                            string patten = @"(\S)";
                                            Regex reg = new Regex(patten);
                                            taskNameDis = reg.Replace(taskNameDis, "*");
                                        }
                                        reminderlistItems.Add(new MyListBoxItemRemind
                                        {
                                            Text = (c_Jinian.Checked ? jiniantimeDT.ToString("dd HH:mm") : (c_endtime.Checked ? endtimeDT.ToString("dd HH:mm") : dt.ToString("dd HH:mm"))) + @"" + GetAttribute(node.ParentNode, "TASKTIME", 4) + @" " + taskNameDis + dakainfo + LeftDakaDays + jinianriInfo + EndDateInfo,
                                            Name = taskName,
                                            Time = dt,
                                            jinianDatetime = jiniantimeDT,
                                            Value = path.Value,
                                            IsShow = IsShow,
                                            remindertype = GetAttribute(node.ParentNode, "REMINDERTYPE"),
                                            rhours = MyToInt16(GetAttribute(node.ParentNode, "RHOUR")),
                                            rdays = MyToInt16(GetAttribute(node.ParentNode, "RDAYS")),
                                            rMonth = MyToInt16(GetAttribute(node.ParentNode, "RMONTH")),
                                            rWeek = MyToInt16(GetAttribute(node.ParentNode, "RWEEK")),
                                            rweeks = GetAttribute(node.ParentNode, "RWEEKS").ToCharArray(),
                                            ryear = MyToInt16(GetAttribute(node.ParentNode, "RYEAR")),
                                            rtaskTime = MyToInt16(GetAttribute(node.ParentNode, "TASKTIME")),
                                            IsDaka = GetAttribute(node.ParentNode, "ISDAKA"),
                                            IsView = GetAttribute(node.ParentNode, "ISVIEW"),
                                            DakaDay = MyToInt16(GetAttribute(node.ParentNode, "DAKADAY")),
                                            level = MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")),
                                            ebstring = MyToInt16(GetAttribute(node.ParentNode, "EBSTRING")),
                                            DakaDays = StrToInt(GetAttribute(node.ParentNode, "DAKADAYS").Split(',')),
                                            editTime = editTime,
                                            isEncrypted = isEncrypted,
                                            IDinXML = GetAttribute(node.ParentNode, "ID"),
                                            link = GetAttribute(node.ParentNode, "LINK"),
                                            rootPath = rootpath.FullName,
                                            ISReminderOnly = iSReminderOnly,
                                            Content = ((System.Xml.XmlElement)node.ParentNode).InnerXml
                                        });
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            else
            {
                if (mindmaplist.SelectedItem == null && mindmapornode.Text == "")
                {
                    return;
                }
                reminderList.Items.Clear();
                reminderListBox.Items.Clear();
                System.Xml.XmlDocument x = new XmlDocument();
                string currentPath = "";
                if (mindmapornode.Text != "")
                {
                    if (mindmapornode.Text.Contains(">"))
                    {
                        currentPath = showMindmapName;
                    }
                    else
                    {
                        mindmapfile file = mindmapfiles.FirstOrDefault(m => m.name.ToLower() == mindmapornode.Text.ToLower());//不区分大小写 //是否需要优化下这个逻辑呢？？
                        if (file == null)
                        {
                            return;
                        }
                        currentPath = file.filePath;
                    }
                }
                else
                {
                    currentPath = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
                }
                try
                {
                    x.Load(currentPath);
                }
                catch (Exception ex)
                {
                    return;
                }
                if (x.GetElementsByTagName("hook").Count == 0)
                {
                    return;
                }
                string str1 = "hook";
                string str2 = "NAME";
                string str3 = "plugins/TimeManagementReminder.xml";
                if (mindmapornode.Text.Contains(">>"))//不再显示此节点内容
                {
                    foreach (XmlNode node in x.GetElementsByTagName("node"))
                    {
                        if (node.ParentNode != null && node.ParentNode.Attributes != null && node.ParentNode.Attributes["ID"] != null && node.ParentNode.Attributes["ID"].InnerText == renameMindMapFileID)
                        {
                            try
                            {
                                DateTime dt = DateTime.Now;
                                string reminder = GetAttribute(node, "CREATED");
                                long unixTimeStamp = Convert.ToInt64(reminder);
                                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                dt = startTime.AddMilliseconds(unixTimeStamp);
                                //解决富文本的问题。
                                string TextString = "";
                                if (node.Attributes["TEXT"] == null)
                                {
                                    foreach (XmlNode item in node.ChildNodes)
                                    {
                                        if (item.Attributes["TYPE"] != null && item.Attributes["TYPE"].Value == "NODE")
                                        {
                                            TextString = new HtmlToString().StripHTML(((System.Xml.XmlElement)item).InnerXml).Replace("|", "").Replace("@", "").Replace("\r", "").Replace("\n", "");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    TextString = GetAttribute(node, "TEXT");
                                }
                                reminderList.Items.Add(new MyListBoxItemRemind
                                {
                                    Text = dt.ToString("yyyy/MM/dd HH:mm") + GetAttribute(node.ParentNode, "TASKTIME", 4) + "  " + TextString,
                                    Name = TextString,
                                    Time = dt,
                                    Value = currentPath,
                                    IsShow = false,
                                    remindertype = GetAttribute(node, "REMINDERTYPE"),
                                    rhours = MyToInt16(GetAttribute(node, "RHOUR")),
                                    rdays = MyToInt16(GetAttribute(node, "RDAYS")),
                                    rMonth = MyToInt16(GetAttribute(node, "RMONTH")),
                                    rWeek = MyToInt16(GetAttribute(node, "RWEEK")),
                                    rweeks = GetAttribute(node, "RWEEKS").ToCharArray(),
                                    ryear = MyToInt16(GetAttribute(node, "RYEAR")),
                                    rtaskTime = MyToInt16(GetAttribute(node, "TASKTIME")),
                                    IsDaka = GetAttribute(node, "ISDAKA"),
                                    IsView = GetAttribute(node, "ISVIEW"),
                                    DakaDay = MyToInt16(GetAttribute(node, "DAKADAY")),
                                    level = MyToInt16(GetAttribute(node, "TASKLEVEL")),
                                    ebstring = MyToInt16(GetAttribute(node, "EBSTRING")),
                                    DakaDays = StrToInt(GetAttribute(node, "DAKADAYS").Split(',')),
                                    editTime = 0,
                                    IDinXML = GetAttribute(node, "ID"),
                                    link = GetAttribute(node, "LINK")
                                });
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                else
                {
                    foreach (XmlNode node in x.GetElementsByTagName(str1))
                    {
                        try
                        {
                            if (node.Attributes[str2].Value == str3)
                            {
                                string reminder = "";
                                DateTime dt = DateTime.Now;
                                if (node.InnerXml != "")
                                {
                                    reminder = node.InnerXml.Split('\"')[1];
                                    long unixTimeStamp = Convert.ToInt64(reminder);
                                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                    dt = startTime.AddMilliseconds(unixTimeStamp);
                                }
                                else
                                {
                                    reminder = GetAttribute(node.ParentNode, "RememberTime");
                                    if (reminder == "")
                                    {
                                    }
                                    else
                                    {
                                        long unixTimeStamp = Convert.ToInt64(reminder);
                                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                        dt = startTime.AddMilliseconds(unixTimeStamp);
                                    }
                                }
                                //添加提醒到提醒清单
                                string dakainfo = "";
                                if (GetAttribute(node.ParentNode, "ISDAKA") == "true")
                                {
                                    dakainfo = " | " + GetAttribute(node.ParentNode, "DAKADAY");
                                }
                                string taskName = "";
                                string taskNameDis = "";
                                bool isEncrypted = false;
                                taskName = node.ParentNode.Attributes["TEXT"].Value;
                                if (taskName.Length > 6)
                                {
                                    if (taskName.Substring(0, 3) == "***")
                                    {
                                        if (PassWord != "")
                                        {
                                            taskName = encrypt.DecryptString(node.ParentNode.Attributes["TEXT"].Value);
                                            isEncrypted = true;
                                        }
                                    }
                                }
                                taskNameDis = taskName;
                                if (IsFileUrl(taskName))
                                {
                                    if (Path.GetExtension(taskName) != "")
                                    {
                                        taskNameDis = "#" + Path.GetFileName(taskName);
                                    }
                                    else
                                    {
                                        taskNameDis = "Path:" + Path.GetFullPath(taskName).Split('\\').Last(m => m != "");
                                    }
                                }
                                if (GetAttribute(node.ParentNode, "ISVIEW") == "true")
                                {
                                    taskNameDis = "待：" + taskNameDis;
                                }
                                if (taskName.ToLower() != "bin")
                                {
                                    reminderlistItems.Add(new MyListBoxItemRemind
                                    {
                                        Text = dt.ToString("yy-MM-dd-HH:mm") + GetAttribute(node.ParentNode, "TASKTIME", 4) + "  " + taskNameDis + dakainfo,
                                        Name = taskName,
                                        Time = dt,
                                        Value = currentPath,
                                        IsShow = false,
                                        remindertype = GetAttribute(node.ParentNode, "REMINDERTYPE"),
                                        rhours = MyToInt16(GetAttribute(node.ParentNode, "RHOUR")),
                                        rdays = MyToInt16(GetAttribute(node.ParentNode, "RDAYS")),
                                        rMonth = MyToInt16(GetAttribute(node.ParentNode, "RMONTH")),
                                        rWeek = MyToInt16(GetAttribute(node.ParentNode, "RWEEK")),
                                        rweeks = GetAttribute(node.ParentNode, "RWEEKS").ToCharArray(),
                                        ryear = MyToInt16(GetAttribute(node.ParentNode, "RYEAR")),
                                        rtaskTime = MyToInt16(GetAttribute(node.ParentNode, "TASKTIME")),
                                        IsDaka = GetAttribute(node.ParentNode, "ISDAKA"),
                                        IsView = GetAttribute(node.ParentNode, "ISVIEW"),
                                        DakaDay = MyToInt16(GetAttribute(node.ParentNode, "DAKADAY")),
                                        level = MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")),
                                        ebstring = MyToInt16(GetAttribute(node.ParentNode, "EBSTRING")),
                                        DakaDays = StrToInt(GetAttribute(node.ParentNode, "DAKADAYS").Split(',')),
                                        editTime = 0,
                                        isEncrypted = isEncrypted,
                                        link = GetAttribute(node.ParentNode, "LINK"),
                                        IDinXML = GetAttribute(node.ParentNode, "ID"),
                                        Content = ((System.Xml.XmlElement)node.ParentNode).InnerXml
                                    });
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            if (!searchword.Text.StartsWith("ss") && showfenge)
            {
                //添加一个当期时间条目
                if (!c_Jinian.Checked)
                {
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = "此时",
                        Name = "当前时间",
                        Time = DateTime.Now,
                        Value = System.AppDomain.CurrentDomain.BaseDirectory,
                        IsShow = true,
                        remindertype = "",
                        rhours = 0,
                        rdays = 0,
                        rMonth = 0,
                        rWeek = 0,
                        rweeks = new char[0],
                        ryear = 0,
                        rtaskTime = 0,
                        IsDaka = "",
                        IsView = "",
                        DakaDay = 0,
                        level = 0,
                        DakaDays = new int[0],
                        editTime = 0,
                        isEncrypted = false,
                        isTask = false
                    });
                }
                if (hasMorning && hasNoon && morning.Checked && !c_Jinian.Checked)
                {
                    //添加一个当期时间条目
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = "上午",
                        Name = "当前时间",
                        Time = DateTime.Today.AddHours(8.5),
                        Value = System.AppDomain.CurrentDomain.BaseDirectory,
                        IsShow = true,
                        remindertype = "",
                        rhours = 0,
                        rdays = 0,
                        rMonth = 0,
                        rWeek = 0,
                        rweeks = new char[0],
                        ryear = 0,
                        rtaskTime = 0,
                        IsDaka = "",
                        IsView = "",
                        DakaDay = 0,
                        level = 0,
                        DakaDays = new int[0],
                        editTime = 0,
                        isEncrypted = false,
                        isTask = false
                    });
                }
                if (hasNoon && hasAfter && day.Checked && !c_Jinian.Checked)
                {
                    //添加一个当期时间条目
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = "下午",
                        Name = "当前时间",
                        Time = DateTime.Today.AddHours(12),
                        Value = System.AppDomain.CurrentDomain.BaseDirectory,
                        IsShow = true,
                        remindertype = "",
                        rhours = 0,
                        rdays = 0,
                        rMonth = 0,
                        rWeek = 0,
                        rweeks = new char[0],
                        ryear = 0,
                        rtaskTime = 0,
                        IsDaka = "",
                        IsView = "",
                        DakaDay = 0,
                        level = 0,
                        DakaDays = new int[0],
                        editTime = 0,
                        isEncrypted = false,
                        isTask = false

                    });
                }
                if (hasAfter && hasNight && afternoon.Checked && night.Checked && !c_Jinian.Checked)
                {
                    //添加一个当期时间条目
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = "晚上",
                        Name = "当前时间",
                        Time = DateTime.Today.AddHours(18),
                        Value = System.AppDomain.CurrentDomain.BaseDirectory,
                        IsShow = true,
                        remindertype = "",
                        rhours = 0,
                        rdays = 0,
                        rMonth = 0,
                        rWeek = 0,
                        rweeks = new char[0],
                        ryear = 0,
                        rtaskTime = 0,
                        IsDaka = "",
                        IsView = "",
                        DakaDay = 0,
                        level = 0,
                        DakaDays = new int[0],
                        editTime = 0,
                        isEncrypted = false,
                        isTask = false
                    });
                }
            }
            foreach (MyListBoxItemRemind item in reminderlistItems.OrderBy(m => m.Time))
            {
                reminderList.Items.Add(item);
            }
            reminderListBox.Items.Clear();
            foreach (MyListBoxItemRemind item in reminderboxList.OrderBy(m => m.Time))
            {
                reminderListBox.Items.Add(item);
            }
            ReminderlistBoxChange();
            ////reminderList.Refresh();
            try
            {
                if (reminderList.Items.Count > reminderSelectIndex)
                {
                    isneedreminderlistrefresh = false;
                    ReminderListSelectedIndex(reminderSelectIndex);
                    isneedreminderlistrefresh = true;
                }
                else
                {
                    isneedreminderlistrefresh = false;
                    ReminderListSelectedIndex(0);
                    isneedreminderlistrefresh = true;
                }
            }
            catch (Exception ex)
            {
            }
            foreach (ReminderItem item in reminderObject.reminders.Where(m => !(m.isCurrect || m.isNew) && !m.isCompleted))
            {
                if (mindmaplist.CheckedItems.Cast<MyListBoxItem>().Any(m => m.Value.IndexOf(item.mindmap) > 0))
                {
                    item.isCompleted = true;
                    item.isCurrect = false;
                    item.isNew = false;
                    item.comleteTime = DateTime.Now;
                }
            };
            //删除重复项
            //List<ReminderItem> repeatItems = new List<ReminderItem>();
            //foreach (ReminderItem item in reminderObject.reminders.Where(m => !m.isCompleted))
            //{
            //    foreach (ReminderItem itemComplete in reminderObject.reminders.Where(m => m.isCompleted))
            //    {
            //        if (item.name == itemComplete.name && itemComplete.mindmap == item.mindmap)
            //        {
            //            if (itemComplete.editTime != null)
            //            {
            //                if (item.editTime == null)
            //                {
            //                    item.editTime = new List<DateTime>();
            //                }
            //                item.editTime.AddRange(itemComplete.editTime);
            //            }
            //            item.editCount += itemComplete.editCount;
            //            reminderObject.editCount += 1;
            //            repeatItems.Add(itemComplete);
            //        }
            //    }
            //}
            //foreach (ReminderItem item in repeatItems)
            //{
            //    reminderObject.reminders.Remove(item);
            //}

            hourLeft.Text = (24 - DateTime.Now.Hour - (float)DateTime.Now.Minute / 60).ToString("N2");
            setTaskInfo(task + "(" + isviewtask + ")|" + ctask + "(" + ebtask + ")|" + vtask + "|" + passtask);
            //如果没有非紧急任务，就显示其他任务
            if (IsReminderOnlyCheckBox.Checked && reminderList.Items.Count == 0)
            {
                IsReminderOnlyCheckBox.Checked = false;
                RRReminderlist();
            }
            //如果非周期里面没有，就显示周期内容
            if (reminderList.Items.Count == 0)
            {
                if (isAutoChangeView)
                {
                    isAutoChangeView = false;
                    showcyclereminder.Checked = !showcyclereminder.Checked;
                    //RRReminderlist();
                }
                else
                {
                    isAutoChangeView = true;
                }
            }
            SortReminderList();
            reminderListBox.Refresh();
            ReminderListBox_SizeChanged(null, null);
            reminderlistSelectedItem = null;//刷新后应该清空
        }

        private void UpdateSummary()
        {
            if (showTimeBlock.Checked)
            {
                Double actionNumber = 0;
                double hours = 0;
                double hoursLeft = 0;
                double max = (DateTime.Now - DateTime.Today).TotalMinutes * 10;
                double min = -(DateTime.Now - DateTime.Today).TotalMinutes * 10;
                if (TimeBlockDate.Value != DateTime.Today)
                {
                    max = min = 24 * 60 * 10;
                }
                //down
                double down = 0;
                int downcount = 0;
                //up
                double up = 0;
                int upcount = 0;
                double all = up + down;
                int qingxuCount = 0;
                //遍历reminderlist
                foreach (MyListBoxItemRemind item in reminderList.Items)
                {
                    actionNumber++;
                    hours += item.rtaskTime;
                    if (item.level > 0)
                    {
                        up += item.rtaskTime * item.level;
                        qingxuCount++;
                        upcount++;
                    }
                    else if (item.level < 0)
                    {
                        down += item.rtaskTime * item.level;
                        qingxuCount++;
                        downcount++;
                    }
                    else
                    {

                    }
                }

                reminder_count.Text = actionNumber.ToString();
                hoursLeft = 24 - DateTime.Now.Hour - (float)DateTime.Now.Minute / 60;
                labeltaskinfo.Text = (hours / 60).ToString("F1") + "小时" + "   " + hoursLeft.ToString("F1") + "小时";
                hourLeft.Text = "";
                Hours.Text = fathernode.Text = "";
                if (actionNumber == 0)
                {
                    actionNumber = 1;
                }
                timeblockcheck.Text = up.ToString("F0") + (down == 0 ? " " : "") + (down == 0 ? "" : down.ToString("F0")) + "=" + (up + down).ToString("F0") + " 满意度：" + (upcount / actionNumber).ToString("P1") + "|" + (up / max).ToString("P1") + " 糟糕度：" + (downcount / actionNumber).ToString("P1") + "|" + (down / min).ToString("P1") + " 总：" + ((upcount - downcount) / actionNumber).ToString("P1") + "|" + ((up + down) / max).ToString("P1") + " 情绪比：" + (qingxuCount / actionNumber).ToString("P1");
            }
        }

        bool isAutoChangeView = true;
        /// <summary>
        /// 刷新任务列表
        /// </summary>
        public void SortReminderList()
        {
            reminderList.Sorted = false;
            reminderList.Sorted = true;
            reminderList.Refresh();
            reminderListBox.Sorted = false;
            reminderListBox.Sorted = true;
            reminderListBox.Refresh();
        }
        /// <summary>
        /// 导图栏双击打开思维导图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mindmaplist_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(((MyListBoxItem)mindmaplist.SelectedItem).Value);
                MyHide();
                searchword.Focus();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 任务栏双击打开思维导图，或者打开链接或者文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reminderlist_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (searchword.Text.ToLower().StartsWith("`") || searchword.Text.ToLower().StartsWith("·"))
                {
                    System.Diagnostics.Process.Start(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                    SaveLog("打开：    " + ((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                    MyHide();
                    return;
                }
                if (IsURL(((MyListBoxItemRemind)reminderlistSelectedItem).Name.Trim()))
                {
                    System.Diagnostics.Process.Start(GetUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                    SaveLog("打开：    " + ((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                }
                else if (IsFileUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name.Trim()))
                {
                    System.Diagnostics.Process.Start(getFileUrlPath(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                    SaveLog("打开：    " + ((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                }
                else
                {
                    System.Diagnostics.Process.Start(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                }
                MyHide();
                searchword.Focus();
            }
            catch (Exception ex)
            {

            }
        }
        private void encryptbutton_Click(object sender, EventArgs e)
        {
            DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", ""))); //System.AppDomain.CurrentDomain.BaseDirectory);
            mindmaplist.Items.Clear();
            reminderList.Items.Clear();
            LoadEncryptFile(path);
            mindmaplist_count.Text = mindmaplist.Items.Count.ToString();
        }
        public void LoadEncryptFile(DirectoryInfo path)
        {
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                if ((file.OpenText().ReadToEnd()).Contains("ENCRYPTED_CONTENT"))
                {
                    mindmaplist.Items.Insert(0, new MyListBoxItem { Text = GetTopString(file) + file.Name, Value = file.FullName });
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mindmaplist.Items.Clear();
            reminderList.Items.Clear();
            DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", ""))); //System.AppDomain.CurrentDomain.BaseDirectory);
            string keyword = yixiaozi.Model.DocearReminder.Helper.ConvertString(searchword.Text);
            string searchPattern = "*.mm";
            if (!IsEncryptBool)
            {
                if (false)
                {
                    //searchPattern = "*";
                }
                foreach (FileInfo file in path.GetFiles(searchPattern, SearchOption.AllDirectories))
                {
                    if (file.Name.Contains(searchword.Text))
                    {
                        mindmaplist.Items.Insert(0, new MyListBoxItem { Text = GetTopString(file) + file.Name, Value = file.FullName });
                    }
                    else
                    {
                        if (".mm.txt.html".IndexOf(file.Extension) >= 0)
                        {
                            if ((file.OpenText().ReadToEnd().ToLower()).Contains(keyword.ToLower()))
                            {
                                mindmaplist.Items.Insert(0, new MyListBoxItem { Text = GetTopString(file) + file.Name, Value = file.FullName });
                            }
                        }
                    }
                }
                if (false)
                {
                    //mindmaplist.Sorted = false;
                    //for (int i = 1; i < mindmaplist.Items.Count; i++)
                    //{
                    //    MoveItem(i);
                    //}
                    //for (int i = 0; i < mindmaplist.Items.Count; i++)
                    //{
                    //    ((MyListBoxItem)mindmaplist.Items[i]).Text = ((MyListBoxItem)mindmaplist.Items[i]).Text.Substring(((MyListBoxItem)mindmaplist.Items[i]).Text.Split(' ')[0].Length + 1);
                    //}
                }
            }
            else
            {
                foreach (FileInfo file in path.GetFiles(searchPattern, SearchOption.AllDirectories))
                {
                    if (Regex.Matches((file.OpenText().ReadToEnd()), @"\*\*\*.*\*\*\*").Count > 0)
                    {
                        foreach (Match item in Regex.Matches((file.OpenText().ReadToEnd()), @"\*\*\*.*\*\*\*"))
                        {
                            if (keyword == "")
                            {
                                try
                                {
                                    string str = item.ToString();
                                    while (str.Contains("****"))
                                    {
                                        str = str.Replace("****", "***");
                                    }
                                    if (encrypt.DecryptString(str) != "")
                                    {
                                        reminderList.Items.Add(new MyListBoxItemRemind
                                        {
                                            Text = encrypt.DecryptString(str),
                                            Name = encrypt.DecryptString(str),
                                            Time = DateTime.Now.AddHours(1),
                                            Value = file.FullName
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                                try
                                {
                                    string str = item.ToString();
                                    while (str.Contains("****"))
                                    {
                                        str = str.Replace("****", "***");
                                    }
                                    if (encrypt.DecryptString(str).Contains(keyword))
                                    {
                                        reminderList.Items.Add(new MyListBoxItemRemind
                                        {
                                            Text = encrypt.DecryptString(str),
                                            Name = encrypt.DecryptString(str),
                                            Time = DateTime.Now.AddHours(1),
                                            Value = file.FullName
                                        });
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                }
            }
            mindmaplist_count.Text = mindmaplist.Items.Count.ToString();
        }
        /// <summary>
        /// 任务栏渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reminderlist_DrawItem(object sender, DrawItemEventArgs e)
        {

            if (e.Index >= 0)
            {
                int zhongyao = 0;
                string name = "";
                zhongyao = ((MyListBoxItemRemind)reminderList.Items[e.Index]).level;
                name = ((MyListBoxItemRemind)reminderList.Items[e.Index]).Name;
                System.Drawing.Brush mybsh = Brushes.Gray;
                Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
                Rectangle rectleft = new Rectangle(e.Bounds.X + e.Bounds.Height, e.Bounds.Y, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height);
                if (zhongyao == 0)
                {
                    SolidBrush zeroColor = new SolidBrush(Color.FromArgb(238, 238, 242));
                    if (searchword.Text.StartsWith("#") || searchword.Text.StartsWith("*"))
                    {
                        zeroColor = new SolidBrush(Color.White);
                    }
                    e.Graphics.FillRectangle(zeroColor, rect);
                    mybsh = new SolidBrush(Color.FromArgb(238, 238, 242));
                    if (name == "当前时间")
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 238, 242)), e.Bounds);
                        mybsh = Brushes.Gray;
                    }
                }
                else if (zhongyao == 1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Azure), rect);
                    mybsh = new SolidBrush(Color.Azure);
                }
                else if (zhongyao == -1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, 220, 220)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(220, 220, 220));
                }
                else if (zhongyao == -2)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(211, 211, 211)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(211, 211, 211));
                }
                else if (zhongyao == -3)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, 192, 192)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(192, 192, 192));
                }
                else if (zhongyao == -4)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(169, 169, 169)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(169, 169, 169));
                }
                else if (zhongyao == -5)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(128, 128, 128));
                }
                else if (zhongyao == -6)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(105, 105, 105)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(105, 105, 105));
                }
                else if (zhongyao == -7)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 85, 85)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(85, 85, 85));
                }
                else if (zhongyao == -8)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(65, 65, 65)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(65, 65, 65));
                }
                else if (zhongyao == -9)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(48, 48, 48)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(48, 48, 48));
                }
                else if (zhongyao <= -10)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
                    mybsh = new SolidBrush(Color.Black);
                }
                else if (zhongyao == 2)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.PowderBlue), rect);
                    mybsh = new SolidBrush(Color.PowderBlue);
                }
                else if (zhongyao == 3)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightSkyBlue), rect);
                    mybsh = new SolidBrush(Color.LightSkyBlue);
                }
                else if (zhongyao == 4)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.DeepSkyBlue), rect);
                    mybsh = new SolidBrush(Color.DeepSkyBlue);
                }
                else if (zhongyao == 5)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.CadetBlue), rect);
                    mybsh = new SolidBrush(Color.CadetBlue);
                }
                else if (zhongyao == 6)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gold), rect);
                    mybsh = new SolidBrush(Color.Gold);
                }
                else if (zhongyao == 7)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Orange), rect);
                    mybsh = new SolidBrush(Color.Orange);
                }
                else if (zhongyao == 8)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.OrangeRed), rect);
                    mybsh = new SolidBrush(Color.OrangeRed);
                }
                else if (zhongyao == 9)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Crimson), rect);
                    mybsh = new SolidBrush(Color.Crimson);
                }
                else if (zhongyao >= 10)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), rect);
                    mybsh = new SolidBrush(Color.Red);
                }
                if (e.Index == reminderList.SelectedIndex)
                {
                    e.Graphics.FillRectangle(mybsh, rect);
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), rectleft);
                }
                else
                {
                    e.Graphics.FillRectangle(mybsh, rect);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), rectleft);
                }
                if (searchword.Text.StartsWith("#"))
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);

                }
                else if (searchword.Text.StartsWith("*"))
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                }
                else if (!((MyListBoxItemRemind)reminderList.Items[e.Index]).isTask)
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text.Substring(0, 3), e.Font, mybsh, rect, StringFormat.GenericDefault);
                    string taskname = ((MyListBoxItemRemind)reminderList.Items[e.Index]).Text.Substring(3);
                    if (taskname.Length > 100)
                    {
                        taskname = taskname.Substring(0, 100);
                    }
                    if (showTimeBlock.Checked || ((MyListBoxItemRemind)reminderList.Items[e.Index]).link == null || ((MyListBoxItemRemind)reminderList.Items[e.Index]).link == "")
                    {
                        e.Graphics.DrawString(taskname, e.Font, Brushes.Gray, rectleft, StringFormat.GenericDefault);
                    }
                    else
                    {
                        e.Graphics.DrawString(taskname, e.Font, Brushes.DeepSkyBlue, rectleft, StringFormat.GenericDefault);
                    }

                    ((MyListBoxItemRemind)reminderList.Items[e.Index]).IsShow = true;
                    e = new DrawItemEventArgs(e.Graphics,
                                            e.Font,
                                            e.Bounds,
                                            e.Index,
                                            e.State,
                                            e.ForeColor,
                                            Color.White);
                    ////e.DrawFocusRectangle();
                }
            }
        }

        /// <summary>
        /// 设置任务信息
        /// </summary>
        /// <param name="info"></param>
        public void setTaskInfo(string info)
        {
            int remindCount = 0;
            int remindHours = 0;
            labeltaskinfo.Text = info;
            foreach (MyListBoxItemRemind item in reminderList.Items)
            {
                if (item.IsShow)
                {
                    remindCount += 1;
                    remindHours += item.rtaskTime;
                }
            }
            foreach (MyListBoxItemRemind item in reminderListBox.Items)
            {
                if (item.IsShow)
                {
                    remindCount += 1;
                    remindHours += item.rtaskTime;
                }
            }
            Hours.Text = ((float)remindHours / 60).ToString("N2");
            reminder_count.Text = remindCount.ToString();
        }
        /// <summary>
        /// 窗体双击，刷新任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (true)
            {
                ReSetValue();
                RRReminderlist();
            }
        }
        private void showtomorrow_CheckedChanged(object sender, EventArgs e)
        {
            if (!showtomorrow.Checked)
            {
                reminder_week.Checked = false;
                reminder_month.Checked = false;
                reminder_year.Checked = false;
                reminder_yearafter.Checked = false;
            }
            if (!(reminder_week.Checked || reminder_month.Checked || reminder_year.Checked || reminder_yearafter.Checked))
            {
                if (true)
                {
                    //ChangeReminder();
                }
            }
        }
        private void Reminder_week_CheckedChanged(object sender, EventArgs e)
        {
            if (!reminder_week.Checked)
            {
                reminder_month.Checked = false;
                reminder_year.Checked = false;
                reminder_yearafter.Checked = false;
            }
            else
            {
                showtomorrow.Checked = true;
            }
            if (!(reminder_month.Checked || reminder_year.Checked || reminder_yearafter.Checked))
            {
                if (true)
                {
                    //ChangeReminder();
                }
            }
        }
        private void Reminder_month_CheckedChanged(object sender, EventArgs e)
        {
            if (!reminder_month.Checked)
            {
                reminder_year.Checked = false;
                reminder_yearafter.Checked = false;
            }
            else
            {
                showtomorrow.Checked = true;
                reminder_week.Checked = true;
            }
            if (!(reminder_year.Checked || reminder_yearafter.Checked))
            {
                if (true)
                {
                    //ChangeReminder();
                }
            }
        }
        private void Reminder_year_CheckedChanged(object sender, EventArgs e)
        {
            if (reminder_year.Checked)
            {
                showtomorrow.Checked = true;
                reminder_week.Checked = true;
                reminder_month.Checked = true;
            }
            else
            {
                reminder_yearafter.Checked = false;
            }
            if (!reminder_yearafter.Checked)
            {
                if (true)
                {
                    //ChangeReminder();
                }
            }
        }
        private void Reminder_yearafter_CheckedChanged(object sender, EventArgs e)
        {
            if (reminder_yearafter.Checked)
            {
                showtomorrow.Checked = true;
                reminder_week.Checked = true;
                reminder_month.Checked = true;
                reminder_year.Checked = true;
            }
            if (true)
            {
                //ChangeReminder();
            }
        }
        public string GetFileSize(double len)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
        public string GetTopString(FileInfo file)
        {
            string topString = "";
            //if (false)
            //{
            //    topString = file.CreationTime.ToString("yyyy/MM/dd HH:mm") + "   ";
            //}
            //else if (false)
            //{
            //    topString = file.LastWriteTime.ToString("yyyy/MM/dd HH:mm") + "   ";
            //}
            //else if (false)
            //{
            //    topString = file.Length.ToString() + " " + GetFileSize(file.Length) + "   ";
            //}
            return topString;
        }
        public void MoveItem(int n)
        {
            for (int i = n; i > 0; i--)
            {
                if (Convert.ToInt64(((MyListBoxItem)mindmaplist.Items[i - 1]).Text.Split(' ')[0]) > Convert.ToInt64(((MyListBoxItem)mindmaplist.Items[i]).Text.Split(' ')[0]))
                {
                    object item = mindmaplist.Items[i - 1];
                    mindmaplist.Items.RemoveAt(i - 1);
                    mindmaplist.Items.Insert(i, item);
                }
            }
        }
        private void Form1_MouseHover(object sender, EventArgs e)
        {
            //LeaveTime();
            //hoverTimer.Stop();//关闭计时器
            //hoverTimer.Start();//重新计时
        }
        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            //LeaveTime();
            //if (this.Location.Y < 30 && ((Cursor.Position.X < this.Location.X || Cursor.Position.Y < this.Location.Y) || (Cursor.Position.X > this.Location.X + 836 || Cursor.Position.Y > this.Location.Y + 544)))
            //Center();//= new Point(this.Location.X, -543);
        }
        private void taskComplete_btn_Click(object sender, EventArgs e)
        {
            CompleteSelectedTask();
        }
        public void CompleteSelectedTask()
        {
            try
            {
                int reminderIndex = reminderList.SelectedIndex;
                bool isreminderlist = true;
                if (reminderListBox.Focused)
                {
                    isreminderlist = false;
                    reminderIndex = reminderListBox.SelectedIndex;
                    reminderboxList.Remove((MyListBoxItemRemind)reminderlistSelectedItem);
                    Xnodes.RemoveAll(m => m.Contains(((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML));
                    //添加去重
                    List<string> xnodesRemoveSame = new List<string>();
                    foreach (string item in Xnodes)
                    {
                        if (!xnodesRemoveSame.Contains(item))
                        {
                            xnodesRemoveSame.Add(item);
                        }
                    }
                    Xnodes = xnodesRemoveSame;
                    new TextListConverter().WriteListToTextFile(Xnodes, System.AppDomain.CurrentDomain.BaseDirectory + @"\Xnodes.txt");
                }
                MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
                CompleteTask(selectedReminder);
                taskTime.Value = 0;
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                //Thread th1 = new Thread(() => AddTaskToFile("log.mm", "完成", selectedReminder.Name, false));
                //th1.Start();
                if (isreminderlist)
                {
                    reminderList.Items.RemoveAt(reminderIndex);
                    if (reminderIndex <= reminderList.Items.Count - 1)//1,0 0>=0
                    {
                        ReminderListSelectedIndex(reminderIndex);
                    }
                    else
                    {
                        try//完成的是最后一个的时候
                        {
                            ReminderListSelectedIndex(reminderList.Items.Count - 1);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                else
                {
                    reminderListBox.Items.RemoveAt(reminderIndex);
                    ReminderlistBoxChange();
                    if (reminderIndex <= reminderListBox.Items.Count - 1)//1,0 0>=0
                    {
                        reminderListBox.SelectedIndex = reminderIndex;
                    }
                    else
                    {
                        try//完成的是最后一个的时候
                        {
                            reminderListBox.SelectedIndex = reminderListBox.Items.Count - 1;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                if (reminderList.Items.Count == 0 && ebcheckBox.Checked)
                {
                    ebcheckBox.Checked = false;
                }

                fenshuADD(selectedReminder.level > 0 ? selectedReminder.level : 1);
            }
            catch (Exception ex)
            {
                if (reminderList.Items.Count > 0)
                {
                    reminderList.SetSelected(0, true);
                }

            }
        }
        public static DateTime GetNextTime(DateTime dt, TimeSpan span)
        {
            do
            {
                dt += span;
            } while (dt < DateTime.Now);
            return dt;
        }
        public static TimeSpan getAddTime(int zhouqi, int zhouqinum)
        {
            switch (zhouqi)
            {
                case 1:
                    if (zhouqinum < 6)
                    {
                        return new TimeSpan(0, 5, 0);
                    }
                    else
                    {
                        return new TimeSpan(0, 30, 0);
                    }
                case 2:
                    if (zhouqinum < 24)
                    {
                        return new TimeSpan(0, 30, 0);
                    }
                    else
                    {
                        return new TimeSpan(12, 0, 0);
                    }
                case 3:
                    if (zhouqinum < 2)
                    {
                        return new TimeSpan(12, 0, 0);
                    }
                    else
                    {
                        return new TimeSpan(1, 0, 0, 0);
                    }
                case 4:
                    if (zhouqinum < 2)
                    {
                        return new TimeSpan(1, 0, 0, 0);
                    }
                    else
                    {
                        return new TimeSpan(2, 0, 0, 0);
                    }
                case 5:
                    if (zhouqinum < 2)
                    {
                        return new TimeSpan(2, 0, 0, 0);
                    }
                    else
                    {
                        return new TimeSpan(4, 0, 0, 0);
                    }
                case 6:
                    if (zhouqinum < 2)
                    {
                        return new TimeSpan(4, 0, 0, 0);
                    }
                    else
                    {
                        return new TimeSpan(7, 0, 0, 0);
                    }
                case 7:
                    if (zhouqinum < 2)
                    {
                        return new TimeSpan(7, 0, 0, 0);
                    }
                    else
                    {
                        return new TimeSpan(15, 0, 0, 0);
                    }
                case 8:
                    return new TimeSpan(15 + zhouqinum, 0, 0, 0);
                default:
                    return new TimeSpan(1, 0, 0);
            }
        }
        public static int GetNextZhouqi(int zhouqi, int zhouqinum)
        {
            switch (zhouqi)
            {
                case 0:
                    return 1;
                case 1:
                    if (zhouqinum < 6)
                    {
                        return zhouqi;
                    }
                    break;
                case 2:
                    if (zhouqinum < 24)
                    {
                        return zhouqi;
                    }
                    break;
                case 3:
                    if (zhouqinum < 2)
                    {
                        return zhouqi;
                    }
                    break;
                case 4:
                    if (zhouqinum < 2)
                    {
                        return zhouqi;
                    }
                    break;
                case 5:
                    if (zhouqinum < 2)
                    {
                        return zhouqi;
                    }
                    break;
                case 6:
                    if (zhouqinum < 2)
                    {
                        return zhouqi;
                    }
                    break;
                case 7:
                    if (zhouqinum < 2)
                    {
                        return zhouqi;
                    }
                    break;
                case 8:
                    return zhouqi;
                default:
                    break;
            }
            return zhouqi + 1;
        }
        public static int GetNextZhouqiNum(int zhouqi, int zhouqinum)
        {
            switch (zhouqi)
            {
                case 0:
                    return 1;
                case 1:
                    if (zhouqinum < 6)
                    {
                        return zhouqinum + 1;
                    }
                    break;
                case 2:
                    if (zhouqinum < 24)
                    {
                        return zhouqinum + 1;
                    }
                    break;
                case 3:
                    if (zhouqinum < 2)
                    {
                        return zhouqinum + 1;
                    }
                    break;
                case 4:
                    if (zhouqinum < 2)
                    {
                        return zhouqinum + 1;
                    }
                    break;
                case 5:
                    if (zhouqinum < 2)
                    {
                        return zhouqinum + 1;
                    }
                    break;
                case 6:
                    if (zhouqinum < 2)
                    {
                        return zhouqinum + 1;
                    }
                    break;
                case 7:
                    if (zhouqinum < 2)
                    {
                        return zhouqinum + 1;
                    }
                    break;
                case 8:
                    return zhouqinum + 1;
                default:
                    break;
            }
            return 1;
        }
        public void CompleteTask(MyListBoxItemRemind selectedReminder)
        {
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("node"))
            {
                if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == selectedReminder.IDinXML)
                {
                    //Thread th = new Thread(() => writeLog(selectedReminder.Text.Substring(29), selectedReminder.Value.Split('\\')[selectedReminder.Value.Split('\\').Length - 1], selectedReminder.Datetime, "button_ok"));
                    //th.Start();
                    SaveLog("完成任务：" + selectedReminder.Name + "    导图" + selectedReminder.Value.Split('\\')[selectedReminder.Value.Split('\\').Length - 1]);
                    if (selectedReminder.IsDaka == "true")
                    {
                        node.Attributes["DAKADAY"].Value = (selectedReminder.DakaDay += 1).ToString();
                    }
                    if (selectedReminder.remindertype == "" || selectedReminder.remindertype == "onetime")
                    {
                        XmlNode newElem = x.CreateElement("icon");
                        XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                        BUILTIN.Value = "button_ok";
                        newElem.Attributes.Append(BUILTIN);
                        node.AppendChild(newElem);
                        //添加子节点
                        if (searchword.Text != "")
                        {
                            XmlNode newNote = x.CreateElement("node");
                            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                            newNotetext.Value = searchword.Text;
                            if (IsURL(newNotetext.Value))
                            {
                                string title = GetWebTitle(newNotetext.Value);
                                if (title != "" && title != "忘记了，后面再改")
                                {
                                    //添加属性
                                    XmlAttribute TASKLink = x.CreateAttribute("LINK");
                                    TASKLink.Value = newNotetext.Value;
                                    newNote.Attributes.Append(TASKLink);
                                    newNotetext.Value = title;
                                }
                            }
                            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                            newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                            newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            XmlAttribute TASKID = x.CreateAttribute("ID");
                            newNote.Attributes.Append(TASKID);
                            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                            newNote.Attributes.Append(newNotetext);
                            newNote.Attributes.Append(newNoteCREATED);
                            newNote.Attributes.Append(newNoteMODIFIED);
                            node.AppendChild(newNote);
                            searchword.Text = "";
                        }
                        //删除提醒
                        //node.RemoveChild(node);
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            if (item != null && item.Attributes != null && item.Attributes["NAME"] != null && item.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml")
                            {
                                node.RemoveChild(item);
                                break;
                            }
                        }
                    }
                    else
                    {
                        switch (selectedReminder.remindertype)
                        {
                            case "hour":
                                if (selectedReminder.rhours == 0)
                                {
                                    selectedReminder.rhours = 1;
                                }
                                do
                                {
                                    selectedReminder.Time = selectedReminder.Time.AddHours(selectedReminder.rhours);
                                }
                                while (selectedReminder.Time < DateTime.Now);
                                break;
                            case "day":
                                if (selectedReminder.rdays == 0)
                                {
                                    selectedReminder.rdays = 1;
                                }
                                do
                                {
                                    selectedReminder.Time = selectedReminder.Time.AddDays(selectedReminder.rdays);
                                }
                                while (selectedReminder.Time < DateTime.Now);
                                break;
                            case "week":
                                do
                                {
                                    selectedReminder.Time = selectedReminder.Time.AddDays(1);
                                    if (selectedReminder.Time.DayOfWeek.ToString() == "Sunday")
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddDays(selectedReminder.rWeek * 7);
                                    }
                                }
                                while ((selectedReminder.rweeks != new char[] { } && !selectedReminder.rweeks.Contains(GetWeekIndex(selectedReminder.Time))) || DateTime.Now > selectedReminder.Time);
                                break;
                            case "month":
                                if (selectedReminder.rMonth == 0)
                                {
                                    selectedReminder.rMonth = 1;
                                }
                                do
                                {
                                    selectedReminder.Time = selectedReminder.Time.AddMonths(selectedReminder.rMonth);
                                }
                                while (selectedReminder.Time < DateTime.Now);

                                break;
                            case "year":
                                if (selectedReminder.ryear == 0)
                                {
                                    selectedReminder.ryear = 1;
                                }
                                do
                                {
                                    selectedReminder.Time = selectedReminder.Time.AddYears(selectedReminder.ryear);
                                }
                                while (selectedReminder.Time < DateTime.Now);
                                break;
                            case "eb":
                                if (selectedReminder.ebstring == 0 || selectedReminder.ebstring + 10000000 < ebconfig)
                                {
                                    selectedReminder.ebstring += 10000000;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddMinutes(30);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring + 1000000 < ebconfig)
                                {
                                    selectedReminder.ebstring += 1000000;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddHours(1);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring + 100000 < ebconfig)
                                {
                                    selectedReminder.ebstring += 100000;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddHours(5);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring + 10000 < ebconfig)
                                {
                                    selectedReminder.ebstring += 10000;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddDays(1);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring + 1000 < ebconfig)
                                {
                                    selectedReminder.ebstring += 1000;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddDays(2);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring + 100 < ebconfig)
                                {
                                    selectedReminder.ebstring += 100;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddDays(4);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring + 10 < ebconfig)
                                {
                                    selectedReminder.ebstring += 10;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddDays(7);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring + 1 < ebconfig)
                                {
                                    selectedReminder.ebstring += 1;
                                    if (selectedReminder.Time > DateTime.Now)
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddDays(15);
                                    }
                                    else
                                    {
                                        selectedReminder.Time = DateTime.Now.AddMinutes(30);

                                    }
                                }
                                else if (selectedReminder.ebstring >= ebconfig - 1)//这个逻辑更复杂稍后在写
                                {
                                    selectedReminder.ebstring += 1;
                                    selectedReminder.Time = selectedReminder.Time.AddDays(15 + selectedReminder.ebstring - ebconfig);
                                }
                                else
                                {
                                    selectedReminder.Time = selectedReminder.Time.AddDays(1);
                                }
                                if (node.Attributes["EBSTRING"] == null)
                                {
                                    XmlAttribute ebstringAttribute = x.CreateAttribute("EBSTRING");
                                    ebstringAttribute.Value = "0";
                                    node.Attributes.Append(ebstringAttribute);
                                }
                                node.Attributes["EBSTRING"].Value = selectedReminder.ebstring.ToString();
                                break;
                        }
                        //node.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((selectedReminder.Time - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            if (item != null && item.Attributes != null && item.Attributes["NAME"] != null && item.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml")
                            {
                                item.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((selectedReminder.Time - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                                break;
                            }
                        }
                        //添加子节点
                        //if (searchword.Text != "")
                        //{
                        //    XmlNode newNote = x.CreateElement("node");
                        //    XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                        //    newNotetext.Value = searchword.Text;
                        //    XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                        //    newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        //    XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                        //    newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        //    newNote.Attributes.Append(newNotetext);
                        //    newNote.Attributes.Append(newNoteCREATED);
                        //    newNote.Attributes.Append(newNoteMODIFIED);
                        //    node.AppendChild(newNote);
                        //    searchword.Text = "";
                        //}
                    }
                    x.Save(selectedReminder.Value);
                    return;
                }
            }
        }


        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //if (dateTimePicker.Focused && taskTime.Value == 0)
            //{
            //    taskTime.Value = 10;
            //}
            //if (dateTimePicker.Focused && tasklevel.Value == 0 && mindmapornode.Text.Contains(">"))
            //{
            //    tasklevel.Value = 1;
            //}
            //if (dateTimePicker.Focused && dateTimePicker.Value < DateTime.Now && dateTimePicker.Value.Day == 1)
            //{
            //    dateTimePicker.Value = dateTimePicker.Value.AddMonths(1);
            //}
        }
        bool isInReminderList = false;
        private void reminderlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            isSettingSyncWeek = false;
            //if (showTimeBlock.Checked)//没有什么用
            //{
            //    //showMindmapName = ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML;
            //}
            if (reminderlistSelectedItem != null)
            {
                isInReminderList = true;
            }
            else
            {
                isInReminderList = false;
            }
            if (reminderListBox.Focused)
            {
                reminderlistSelectedItem = reminderListBox.SelectedItem;
                reminderSelectIndex = reminderListBox.SelectedIndex;
            }
            else
            {
                reminderlistSelectedItem = reminderList.SelectedItem;
                reminderSelectIndex = reminderList.SelectedIndex;
            }
            if (reminderListBox.Visible)
            {
                reminderListBox.Refresh();
            }
            if (isneedreminderlistrefresh)
            {
                reminderList.Refresh();
            }
            if (reminderlistSelectedItem == null)
            {
                return;
            }
            if (searchword.Text.ToLower().StartsWith("`") || searchword.Text.ToLower().StartsWith("·"))
            {
                if (reminderlistSelectedItem != null)
                {
                    richTextSubNode.Text = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                }
                return;
            }
            PlaySimpleSound("next");
            isInReminderlistSelect = true;
            if (searchword.Text.StartsWith("#"))
            {
                richTextSubNode.Text = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                return;
            }
            if (searchword.Text.StartsWith("node"))
            {
                if (reminderlistSelectedItem != null)
                {
                    richTextSubNode.Text = ((MyListBoxItemRemind)reminderlistSelectedItem).Text;

                }
                try
                {
                    //
                    string mindmap = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                    for (int i = 0; i < mindmaplist.Items.Count; i++)
                    {
                        if (mindmap == ((MyListBoxItem)mindmaplist.Items[i]).Value)
                        {
                            IsSelectReminder = true;
                            mindmaplist.SetSelected(i, true);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {

                    return;
                }
                return;
            }
            reminderSelectIndex = reminderList.SelectedIndex;
            if (reminderListBox.Focused)
            {
                reminderSelectIndex = reminderListBox.SelectedIndex;
            }
            else
            {
                ReminderListSelectedIndex(reminderSelectIndex);
            }
            if (reminderlistSelectedItem == null)
            {
                return;
            }
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            if (this.Height == maxheight)
            {
                SelectTreeNode(nodetree.Nodes, selectedReminder.IDinXML);
            }
            if (selectedReminder.isEncrypted)
            {
                IsEncryptBool = true;
            }
            else
            {
                IsEncryptBool = false;
            }

            if (true)
            {
                ShowSubNode();
            }
            else
            {
                //ShowHTML();
            }
            //暂时不显示这些信息了
            //if (selectedReminder.IsDaka == "true")
            //{
            //    DAKAINFO.Text = String.Format("平均: {0}  ,  最高: {1}  ,  总天数： {2}   ，总次数： {3}    ，延迟：{4}", GetAVAge(selectedReminder.DakaDays, selectedReminder.DakaDay).ToString("0.##"), GetMax(selectedReminder.DakaDays, selectedReminder.DakaDay), GetSUM(selectedReminder.DakaDays, selectedReminder.DakaDay), GetNUM(selectedReminder.DakaDays, selectedReminder.DakaDay), selectedReminder.editTime);
            //}
            //else
            //{
            //    DAKAINFO.Text = String.Format("延迟：{0}", selectedReminder.editTime);
            //}
            tasklevel.Value = selectedReminder.level;
            c_day.Checked = c_week.Checked = c_hour.Checked =
                c_month.Checked =
                c_year.Checked =
                c_Saturday.Checked =
                c_Monday.Checked =
                c_Tuesday.Checked =
                c_Wednesday.Checked =
                c_Thursday.Checked =
                c_Friday.Checked =
                c_Saturday.Checked =
                c_remember.Checked =
                c_Sunday.Checked = false;
            button_cycle.Text = "设置周期";
            n_days.Value = 0;
            taskTime.Value = selectedReminder.rtaskTime;
            dateTimePicker.Value = selectedReminder.Time;
            if (selectedReminder.Time.Year != DateTime.Now.Year)
            {
                dateTimePicker.CustomFormat = "yyyy MM月dd dddd";
            }
            else
            {
                dateTimePicker.CustomFormat = "HH:mm MM月dd dddd";
            }
            if (selectedReminder.remindertype != "")
            {
                switch (selectedReminder.remindertype)
                {
                    case "hour":
                        c_hour.Checked = true;
                        n_days.Value = Convert.ToInt64(selectedReminder.rhours);
                        break;
                    case "day":
                        c_day.Checked = true;
                        n_days.Value = Convert.ToInt64(selectedReminder.rdays);
                        break;
                    case "week":
                        c_week.Checked = true;
                        n_days.Value = Convert.ToInt64(selectedReminder.rWeek);
                        for (int i = 0; i < selectedReminder.rweeks.Length; i++)
                        {
                            switch (selectedReminder.rweeks[i])
                            {
                                case '1':
                                    c_Monday.Checked = true;
                                    break;
                                case '2':
                                    c_Tuesday.Checked = true;
                                    break;
                                case '3':
                                    c_Wednesday.Checked = true;
                                    break;
                                case '4':
                                    c_Thursday.Checked = true;
                                    break;
                                case '5':
                                    c_Friday.Checked = true;
                                    break;
                                case '6':
                                    c_Saturday.Checked = true;
                                    break;
                                case '7':
                                    c_Sunday.Checked = true;
                                    break;
                            }
                        }
                        break;
                    case "month":
                        c_month.Checked = true;
                        n_days.Value = Convert.ToInt64(selectedReminder.rMonth);
                        break;
                    case "year":
                        c_year.Checked = true;
                        n_days.Value = Convert.ToInt64(selectedReminder.ryear);
                        break;
                    case "eb":
                        c_remember.Checked = true;
                        button_cycle.Text = selectedReminder.ebstring.ToString();
                        break;

                }
            }
            if (!c_ViewModel.Checked)
            {
                for (int i = 0; i < mindmaplist.Items.Count; i++)
                {
                    if (selectedReminder.Value == ((MyListBoxItem)mindmaplist.Items[i]).Value)
                    {
                        IsSelectReminder = true;
                        mindmaplist.SetSelected(i, true);
                        return;
                    }
                }
            }
            if (reminderList.Focused)
            {
                reminderList.Refresh();
                ReminderListSelectedIndex(reminderSelectIndex);
            }

        }
        private void button_cycle_Click(object sender, EventArgs e)
        {
            try
            {
                SetCycleTask();
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                RRReminderlist();
                reminderList.Focus();
            }
            catch (Exception ex)
            {
                if (reminderList.Items.Count > 0)
                {
                    reminderList.SetSelected(0, true);
                }
            }
        }
        public string GetAttribute(XmlNode node, string name, int resultLenght = 0)
        {
            string resultdefault = "";
            for (int i = 0; i < resultLenght; i++)
            {
                resultdefault += " ";
            }
            try
            {
                if (node == null || node.Attributes == null || (name != "TEXT" && node.Attributes[name] == null))
                {
                    return resultdefault;
                }
                else if (node == null || node.Attributes == null || (name == "TEXT" && node.Attributes[name] == null))
                {
                    try
                    {
                        if (node.FirstChild.Name == "richcontent")
                        {
                            return new HtmlToString().StripHTML(node.FirstChild.InnerText);
                        }
                    }
                    catch (Exception ex)
                    {
                        return "未找到richcontent";
                    }
                }
                string result = "";
                result = node.Attributes[name].Value;
                result = FormatTimeLenght(result, resultLenght);
                return result;
            }
            catch (Exception ex)
            {
                return resultdefault;
            }
        }
        public string FormatTimeLenght(string result, int resultLenght)
        {
            for (int i = result.Length; i < resultLenght; i++)
            {
                result = " " + result;
            }
            if (resultLenght != 0 && result.Trim() == "0")
            {
                result = result.Replace("0", " ");
            }
            return result;
        }
        public string lenghtString(string result, int resultLenght = 0)
        {
            for (int i = result.Length; i < resultLenght; i++)
            {
                result = "0" + result;
            }
            if (resultLenght != 0 && result.Trim() == "0")
            {
                result = result.Replace("0", " ");
            }
            return result;
        }

        public int MyToInt16(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return 0;
            }
            return Int32.Parse(value, CultureInfo.CurrentCulture);
        }
        public bool MyToBoolean(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region

        public void SetCycleTask()
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                    {
                        if (selectedReminder.remindertype == "")
                        {
                            XmlNode newElem = x.CreateElement("icon");
                            XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                            BUILTIN.Value = "revision";
                            newElem.Attributes.Append(BUILTIN);
                            node.ParentNode.AppendChild(newElem);
                            XmlAttribute REMINDERTYPE = x.CreateAttribute("REMINDERTYPE");
                            XmlAttribute RDAYS = x.CreateAttribute("RDAYS");
                            XmlAttribute RWEEK = x.CreateAttribute("RWEEK");
                            XmlAttribute RMONTH = x.CreateAttribute("RMONTH");
                            XmlAttribute RWEEKS = x.CreateAttribute("RWEEKS");
                            XmlAttribute RYEAR = x.CreateAttribute("RYEAR");
                            XmlAttribute RHOUR = x.CreateAttribute("RHOUR");
                            node.ParentNode.Attributes.Append(REMINDERTYPE);
                            node.ParentNode.Attributes.Append(RDAYS);
                            node.ParentNode.Attributes.Append(RWEEK);
                            node.ParentNode.Attributes.Append(RMONTH);
                            node.ParentNode.Attributes.Append(RWEEKS);
                            node.ParentNode.Attributes.Append(RYEAR);
                            node.ParentNode.Attributes.Append(RHOUR);
                        }
                        //避免周期设置成0的问题
                        if (n_days.Value == 0)
                        {
                            n_days.Value = 1;
                        }
                        if (c_day.Checked)
                        {
                            node.ParentNode.Attributes["REMINDERTYPE"].Value = "day";
                            selectedReminder.remindertype = "day";
                            node.ParentNode.Attributes["RDAYS"].Value = n_days.Value.ToString();
                            selectedReminder.rdays = (int)n_days.Value;
                        }
                        else if (c_week.Checked)
                        {
                            node.ParentNode.Attributes["REMINDERTYPE"].Value = "week";
                            selectedReminder.remindertype = "week";
                            node.ParentNode.Attributes["RWEEK"].Value = n_days.Value.ToString();
                            selectedReminder.rWeek = (int)n_days.Value;
                            node.ParentNode.Attributes["RWEEKS"].Value = GetWeekStr();
                            selectedReminder.rweeks = GetWeekStr().ToArray();
                        }
                        else if (c_month.Checked)
                        {
                            node.ParentNode.Attributes["REMINDERTYPE"].Value = "month";
                            selectedReminder.remindertype = "month";
                            node.ParentNode.Attributes["RMONTH"].Value = n_days.Value.ToString();
                            selectedReminder.rMonth = (int)n_days.Value;
                        }
                        else if (c_year.Checked)
                        {
                            node.ParentNode.Attributes["REMINDERTYPE"].Value = "year";
                            selectedReminder.remindertype = "year";
                            node.ParentNode.Attributes["RYEAR"].Value = n_days.Value.ToString();
                            selectedReminder.ryear = (int)n_days.Value;
                        }
                        else if (c_hour.Checked)
                        {
                            node.ParentNode.Attributes["REMINDERTYPE"].Value = "hour";
                            selectedReminder.remindertype = "hour";
                            node.ParentNode.Attributes["RHOUR"].Value = n_days.Value.ToString();
                            selectedReminder.rhours = (int)n_days.Value;
                        }
                        else if (c_remember.Checked)
                        {
                            node.ParentNode.Attributes["REMINDERTYPE"].Value = "eb";
                            selectedReminder.remindertype = "eb";
                            if (node.ParentNode.Attributes["EBSTRING"] == null)
                            {
                                XmlAttribute ebstringAttribute = x.CreateAttribute("EBSTRING");
                                ebstringAttribute.Value = "0";
                                node.ParentNode.Attributes.Append(ebstringAttribute);
                            }
                        }
                        else
                        {
                            node.ParentNode.Attributes["REMINDERTYPE"].Value = "onetime";
                            selectedReminder.remindertype = "onetime";
                        }
                        x.Save(selectedReminder.Value);
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void c_day_CheckedChanged(object sender, EventArgs e)
        {
            if (c_day.Checked)
            {
                c_week.Checked = c_month.Checked = c_year.Checked = c_hour.Checked = c_remember.Checked = false;
                button_cycle.Text = "设置周期";
                if (n_days.Value == 0)
                {
                    n_days.Value = 1;
                }
            }
        }
        private void c_week_CheckedChanged(object sender, EventArgs e)
        {
            if (c_week.Checked)
            {
                c_day.Checked = c_month.Checked = c_year.Checked = c_hour.Checked = c_remember.Checked = false;
                button_cycle.Text = "设置周期";
            }
            if (n_days.Value == 0)
            {
                n_days.Value = 1;
            }
        }
        private void c_month_CheckedChanged(object sender, EventArgs e)
        {
            if (c_month.Checked)
            {
                c_week.Checked = c_day.Checked = c_year.Checked = c_hour.Checked = c_remember.Checked = false;
                button_cycle.Text = "设置周期";
            }
            if (n_days.Value == 0)
            {
                n_days.Value = 1;
            }
        }
        private void c_year_CheckedChanged(object sender, EventArgs e)
        {
            if (c_year.Checked)
            {
                c_week.Checked = c_month.Checked = c_day.Checked = c_hour.Checked = c_remember.Checked = false;
                button_cycle.Text = "设置周期";
            }
            if (n_days.Value == 0)
            {
                n_days.Value = 1;
            }
        }
        private void c_hour_CheckedChanged(object sender, EventArgs e)
        {
            if (c_hour.Checked)
            {
                c_week.Checked = c_month.Checked = c_day.Checked = c_year.Checked = c_remember.Checked = false;
                button_cycle.Text = "设置周期";
            }
            if (n_days.Value == 0)
            {
                n_days.Value = 1;
            }
        }
        private void c_remember_CheckedChanged(object sender, EventArgs e)
        {
            if (c_remember.Checked)
            {
                c_week.Checked = c_month.Checked = c_day.Checked = c_year.Checked = c_hour.Checked = false;
            }
        }
        #endregion

        public char GetWeekIndex(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return '5';
                case DayOfWeek.Monday:
                    return '1';
                case DayOfWeek.Saturday:
                    return '6';
                case DayOfWeek.Sunday:
                    return '7';
                case DayOfWeek.Thursday:
                    return '4';
                case DayOfWeek.Tuesday:
                    return '2';
                case DayOfWeek.Wednesday:
                    return '3';
                default:
                    return '1';//避免编译报错
            }
        }
        /// <summary>
        /// 获取星期字符串，比如周一，周三则返回13
        /// </summary>
        /// <returns></returns>
        public string GetWeekStr()
        {
            string result = "";
            if (c_Monday.Checked)
            {
                result += "1";
            }
            if (c_Tuesday.Checked)
            {
                result += "2";
            }
            if (c_Wednesday.Checked)
            {
                result += "3";
            }
            if (c_Thursday.Checked)
            {
                result += "4";
            }
            if (c_Friday.Checked)
            {
                result += "5";
            }
            if (c_Saturday.Checked)
            {
                result += "6";
            }
            if (c_Sunday.Checked)
            {
                result += "7";
            }
            return result;
        }
        private void EditTime_Clic(object sender, EventArgs e)
        {
            try
            {
                if (reminderList.SelectedIndex != -1)
                {
                    reminderSelectIndex = reminderList.SelectedIndex;
                }
                else
                {
                    reminderSelectIndex = reminderListBox.SelectedIndex;
                }
                EditTask();
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).Time = dateTimePicker.Value;
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).level = (int)tasklevel.Value;
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).rtaskTime = (int)taskTime.Value;
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).Text = newName((MyListBoxItemRemind)(reminderlistSelectedItem)).Text;
                if (reminderList.SelectedIndex != -1)
                {
                    ReminderListSelectedIndex(reminderSelectIndex);
                    reminderList.Focus();
                    reminderList.Refresh();
                }
                else
                {
                    reminderListBox.SelectedIndex = reminderSelectIndex;
                    reminderListBox.Focus();
                    reminderListBox.Refresh();
                }
                fenshuADD(1);
            }
            catch (Exception ex)
            {

            }
        }
        private void edittime_EndDate(bool setEndtime = true)
        {
            try
            {
                if (reminderList.SelectedIndex != -1)
                {
                    reminderSelectIndex = reminderList.SelectedIndex;
                }
                EditTaskEndDate(setEndtime);
                reminderList.Focus();
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).Time = dateTimePicker.Value;
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).level = (int)tasklevel.Value;
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).rtaskTime = (int)taskTime.Value;
                ((MyListBoxItemRemind)(reminderlistSelectedItem)).Text = newName((MyListBoxItemRemind)(reminderlistSelectedItem)).Text;
                taskTime.Value = 0;
                tasklevel.Value = 0;
                reminderList.Sorted = false;
                SortReminderList();
                ReminderListSelectedIndex(reminderSelectIndex);
                reminderlist_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {

            }
        }
        public void EditTask()
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            DateTime dateBefore = selectedReminder.Time;
            int taskTimeBefore = selectedReminder.rtaskTime;
            int tasklevelBefore = selectedReminder.level;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("node"))
            {
                if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == selectedReminder.IDinXML)
                {
                    try
                    {
                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                        bool isHashook = false;
                        foreach (XmlNode item in node.ChildNodes)
                        {
                            if (item.Name == "hook" && !isHashook && item.Attributes != null && item.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml")
                            {
                                isHashook = true;
                                item.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
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
                            remindernodeTime.Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            remindernodeParameters.Attributes.Append(remindernodeTime);
                            remindernode.AppendChild(remindernodeParameters);
                            node.AppendChild(remindernode);
                        }
                        XmlAttribute TASKTIME = x.CreateAttribute("TASKTIME");
                        node.Attributes.Append(TASKTIME);
                        node.Attributes["TASKTIME"].Value = taskTime.Value.ToString();
                        XmlAttribute TASKLEVEL = x.CreateAttribute("TASKLEVEL");
                        node.Attributes.Append(TASKLEVEL);
                        node.Attributes["TASKLEVEL"].Value = tasklevel.Value.ToString();
                        x.Save(selectedReminder.Value);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                        th.Start();
                        SaveLog("修改了任务：" + taskName + "    时间：" + dateBefore.ToString() + ">" + dateTimePicker.Value.ToString() + "    时长：" + taskTimeBefore.ToString() + ">" + taskTime.Value.ToString() + "    等级：" + tasklevelBefore.ToString() + ">" + tasklevel.Value.ToString());
                        return;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        public void EditTaskEndDate(bool setenddate)
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            DateTime dateBefore = selectedReminder.EndDate;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                    {
                        if (setenddate)
                        {
                            if (GetAttribute(node.FirstChild, "EndDate") != "")
                            {
                                node.FirstChild.Attributes["EndDate"].Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            }
                            else
                            {
                                //添加属性
                                XmlAttribute TASKLEVEL = x.CreateAttribute("EndDate");
                                node.ParentNode.Attributes.Append(TASKLEVEL);
                                node.ParentNode.Attributes["EndDate"].Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            }
                        }
                        else//取消设置截止日期
                        {
                            try
                            {
                                node.ParentNode.Attributes.Remove(node.ParentNode.Attributes["EndDate"]);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        x.Save(selectedReminder.Value);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                        th.Start();
                        SaveLog("修改了任务：" + taskName + "    截止时间：" + dateBefore.ToString() + ">" + dateTimePicker.Value.ToString());
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void SetTaskIsView()
        {
            if (!ReminderListFocused())
            {
                return;
            }
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                    {
                        if (selectedReminder.IsView == "true")
                        {
                            node.ParentNode.Attributes["ISVIEW"].Value = "false";
                        }
                        else
                        {
                            XmlAttribute IsView = x.CreateAttribute("ISVIEW");
                            IsView.Value = "true";
                            node.ParentNode.Attributes.Append(IsView);
                        }
                        x.Save(selectedReminder.Value);
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void OpenFanQie(int time, string name, string mindmap, int fanqieCount, bool isnotDefault = false, int tasklevelNum = 0, string timeblockName = "", string timeblockfather = "", string timeblockcolor = "")
        {
            Tomato fanqie = new Tomato(new DateTime().AddMinutes(time), name, mindmap, fanqieCount, isnotDefault, tasklevelNum, timeblockName, timeblockfather, timeblockcolor);
            fanqie.ShowDialog();
        }
        public void OpenMenu()
        {
            Tools menu = new Tools();
            menu.ShowDialog();
        }
        private void Reminder_count_Click(object sender, EventArgs e)
        {
            if (reminderList.SelectedIndex >= 0)
            {
                MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
                if (selectedReminder.rtaskTime > 0)
                {
                    Thread th = new Thread(() => OpenFanQie(selectedReminder.rtaskTime, selectedReminder.Name, selectedReminder.Value, GetPosition(), false, selectedReminder.level));
                    tomatoCount += 1;
                    th.Start();
                }
            }
        }
        public void OpenFanqie(bool isnotdefault = false)
        {
            if (reminderList.SelectedIndex >= 0 || reminderListBox.SelectedIndex >= 0)
            {
                MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
                if (selectedReminder.rtaskTime >= 0 || isnotdefault)
                {
                    Thread th = new Thread(() => OpenFanQie(selectedReminder.rtaskTime, selectedReminder.Name, selectedReminder.Value, GetPosition(), isnotdefault, selectedReminder.level));
                    tomatoCount += 1;
                    th.Start();
                    if (IsURL(((MyListBoxItemRemind)reminderlistSelectedItem).Name.Trim()))
                    {
                        System.Diagnostics.Process.Start(GetUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                        SaveLog("打开：    " + GetUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                    }
                    else if (IsFileUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name.Trim()))
                    {
                        System.Diagnostics.Process.Start(getFileUrlPath(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                        SaveLog("打开：    " + getFileUrlPath(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                    }
                    //如果是小时循环的，则自动完成
                    if (selectedReminder.remindertype == "hour")
                    {
                        try
                        {
                            CompleteTask(selectedReminder);
                            Thread th1 = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                            th1.Start();
                        }
                        catch (Exception ex)
                        {
                            if (reminderList.Items.Count > 0)
                            {
                                reminderList.SetSelected(0, true);
                            }

                        }
                    }
                }
            }
        }
        private void delay_Click(object sender, EventArgs e)
        {
            DelaySelectedTask();
        }
        public void DelaySelectedTask()
        {
            try
            {
                int reminderIndex = reminderList.SelectedIndex;
                MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
                selectedReminder = newName(DelayTask(selectedReminder));
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                reminderList.Refresh();
                reminderList.Items.RemoveAt(reminderIndex);
                ReminderListSelectedIndex(reminderIndex);
                fenshuADD(1);// -selectedReminder.level);
            }
            catch (Exception ex)
            {
                if (reminderList.Items.Count > 0)
                {
                    reminderList.SetSelected(0, true);
                }

            }
        }

        public MyListBoxItemRemind DelayTask(MyListBoxItemRemind selectedReminder, bool isDenyall = false)
        {
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            DateTime DateBefore = selectedReminder.Time;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                    {
                        //if (selectedReminder.IsDaka == "true")
                        //{
                        //    node.ParentNode.Attributes["DAKADAYS"].Value = node.ParentNode.Attributes["DAKADAYS"].Value + "," + node.ParentNode.Attributes["DAKADAY"].Value;
                        //    node.ParentNode.Attributes["DAKADAY"].Value = "0";
                        //}
                        if (selectedReminder.remindertype == "" || selectedReminder.remindertype == "onetime")
                        {
                            //while (selectedReminder.Datetime < DateTime.Now && !DateTime.Equals(selectedReminder.Datetime, DateTime.Now.Date))
                            //{//如果时间小,且不是同一天,则加一天
                            //    selectedReminder.Datetime = selectedReminder.Datetime.AddDays(1);
                            //}
                            do
                            {
                                if (isDenyall)
                                {
                                    isDenyall = false;
                                    continue;
                                }
                                selectedReminder.Time = selectedReminder.Time.AddDays(1);
                            } while (selectedReminder.Time < DateTime.Today);
                        }
                        else
                        {
                            switch (selectedReminder.remindertype)
                            {
                                case "hour":
                                    if (selectedReminder.rhours == 0)
                                    {
                                        selectedReminder.rhours = 1;
                                    }
                                    do
                                    {
                                        selectedReminder.Time = selectedReminder.Time.AddHours(selectedReminder.rhours);
                                    }
                                    while (selectedReminder.Time < DateTime.Now);
                                    break;
                                case "day":
                                    if (selectedReminder.rdays == 0)
                                    {
                                        selectedReminder.rdays = 1;
                                    }
                                    do
                                    {
                                        if (isDenyall)
                                        {
                                            isDenyall = false;
                                            continue;
                                        }
                                        selectedReminder.Time = selectedReminder.Time.AddDays(selectedReminder.rdays);
                                    }
                                    while (selectedReminder.Time < DateTime.Today);//起码延迟到今天，而不是现在
                                    break;
                                case "week":
                                    do
                                    {
                                        if (isDenyall)
                                        {
                                            isDenyall = false;
                                            continue;
                                        }
                                        selectedReminder.Time = selectedReminder.Time.AddDays(1);
                                        if (selectedReminder.Time.DayOfWeek.ToString() == "Sunday")
                                        {
                                            selectedReminder.Time = selectedReminder.Time.AddDays(selectedReminder.rWeek * 7);
                                        }
                                    }
                                    while (!selectedReminder.rweeks.Contains(GetWeekIndex(selectedReminder.Time)) || DateTime.Now > selectedReminder.Time);
                                    break;
                                case "month":
                                    if (selectedReminder.rMonth == 0)
                                    {
                                        selectedReminder.rMonth = 1;
                                    }
                                    do
                                    {
                                        if (isDenyall)
                                        {
                                            isDenyall = false;
                                            continue;
                                        }
                                        selectedReminder.Time = selectedReminder.Time.AddMonths(selectedReminder.rMonth);
                                    }
                                    while (selectedReminder.Time < DateTime.Today);

                                    break;
                                case "year":
                                    if (selectedReminder.ryear == 0)
                                    {
                                        selectedReminder.ryear = 1;
                                    }
                                    do
                                    {
                                        if (isDenyall)
                                        {
                                            isDenyall = false;
                                            continue;
                                        }
                                        selectedReminder.Time = selectedReminder.Time.AddYears(selectedReminder.ryear);
                                    }
                                    while (selectedReminder.Time < DateTime.Today);
                                    break;
                            }
                        }
                        node.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((selectedReminder.Time - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        //取消增加等级
                        //try//避免没有TaskLevel的情况，懒得判断了
                        //{
                        //    if (Convert.ToInt64(node.ParentNode.Attributes["TASKLEVEL"].Value) < 9)
                        //    {
                        //        node.ParentNode.Attributes["TASKLEVEL"].Value = (Convert.ToInt64(node.ParentNode.Attributes["TASKLEVEL"].Value) + 1).ToString();
                        //        selectedReminder.level = Convert.ToInt64(node.ParentNode.Attributes["TASKLEVEL"].Value);
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    XmlAttribute TASKLEVEL = x.CreateAttribute("TASKLEVEL");
                        //    node.ParentNode.Attributes.Append(TASKLEVEL);
                        //    node.ParentNode.Attributes["TASKLEVEL"].Value = "0";
                        //}
                        x.Save(selectedReminder.Value);
                        fenshuADD(-1);
                        SaveLog("推迟了任务:" + taskName + "从" + DateBefore.ToString() + "到" + selectedReminder.Time.ToString());
                        //AddTaskToFile("log.mm", "推迟", taskName + "从" + DateBefore.ToString() + "到" + selectedReminder.Datetime.ToString(), false);
                        return selectedReminder;
                    }
                }
                catch (Exception ex)
                {

                    return selectedReminder;
                }
            }
            return selectedReminder;
        }
        private void showcyclereminder_CheckedChanged(object sender, EventArgs e)
        {
            RRReminderlist();
        }
        public string GetTimeSpanStr(int day)
        {
            string result = "";
            int year = 0;
            int month = 0;
            if (day > 365)
            {
                year = day / 365;
                result += year + "年";
                day %= 365;
            }
            if (day > 30)
            {
                month = day / 30;
                result += month + "月";
                day %= 30;
            }
            result += day + "天";
            return result;
        }

        public void SearchNode()
        {
            isSearchFileOrNode = true;
            if (searchword.Text != "" && searchword.Text.StartsWith("*"))
            {
                string keywords = searchword.Text.Substring(1);
                if (keywords == "")
                {
                    return;
                }
                string[] keywordsArr = keywords.Split(' ');
                reminderList.Items.Clear();
                List<string> files = new List<string>();
                foreach (node item in nodes.Where(m => StringHasArrALL(m.Text, keywordsArr)).OrderByDescending(m => m.editDateTime).Take(200))
                {
                    if (!files.Contains(item.mindmapPath))
                    {
                        files.Add(item.mindmapPath);
                    }
                    reminderList.Items.Add(new MyListBoxItemRemind() { Text = item.Text, Value = item.mindmapPath, Time = item.Time, IDinXML = item.IDinXML });
                }
                mindmaplist.Items.Clear();
                foreach (string item in files)
                {
                    string filename = Path.GetFileName(item);
                    filename = filename.Substring(0, filename.Length - 3);
                    mindmaplist.Items.Insert(0, new MyListBoxItem { Text = filename, Value = item });
                }
                for (int i = 0; i < mindmaplist.Items.Count; i++)
                {
                    mindmaplist.SetItemChecked(i, true);
                }
                return;
            }
        }
        public void AddTask(bool istask)
        {
            fenshuADD(3);
            PlaySimpleSound("add");
            if (searchword.Text != "" && searchword.Text.Contains("clip"))
            {
                IDataObject iData = new DataObject();
                iData = Clipboard.GetDataObject();
                string log = (string)iData.GetData(DataFormats.Text);
                if (log == null || log == "")
                {
                    return;
                }
                if (IsURL(log.Trim()))
                {
                    log = GetWebTitle(log.Trim()) + " | " + log;
                }
                searchword.Text = searchword.Text.Replace("clip", log);
            }
            //搜索任务
            if (searchword.Text != "" && searchword.Text.StartsWith("t:"))
            {
            }
            if (searchword.Text != "" && searchword.Text.Contains("@@"))
            {
                string taskName = searchword.Text.Split('@')[0];
                string nodeName = searchword.Text.Split('@')[2];
                if (taskName == "")
                {
                    RenameNodeByID(nodeName);
                    SaveLog("修改节点名称：" + renameTaskName + "  To  " + searchword.Text);
                    searchword.Text = "";
                    //ChangeReminder();
                    return;
                }
                else
                {
                    AddNodeByID(istask, taskName, nodeName);
                    SaveLog("Add节点名称：" + taskName + "  Map:  " + showMindmapName + "    节点：" + nodeName);
                    searchword.Text = "";
                    mindmapornode.Text = "";
                    if (istask)
                    {
                        RRReminderlist();
                    }
                    return;
                }
            }
            else if (searchword.Text != "" && searchword.Text.Contains("@"))
            {
                string filename = searchword.Text.Split('@')[1];
                string taskName = searchword.Text.Split('@')[0];

                if (filename == "password")
                {
                    DocearReminderForm.PassWord = taskName;
                    searchword.Text = "";
                    RRReminderlist();
                    return;
                }
                mindmapfile file = mindmapfiles.FirstOrDefault(m => m.name.ToLower() == filename.ToLower());//不区分大小写 //是否需要优化下这个逻辑呢？？
                if (file == null)
                {
                    return;
                }
                if (taskName == "")
                {
                    if (istask)//Shift
                    {
                        System.Diagnostics.Process.Start(file.filePath);
                        searchword.Text = "";
                        MyHide();
                        return;
                    }
                    else
                    {
                        //只显示当前导图的任务
                        try
                        {
                            reminderList.Items.Clear();
                            XmlDocument x = new XmlDocument();
                            try
                            {
                                x.Load(file.filePath);
                            }
                            catch (Exception ex)
                            {
                                return;
                            }
                            fathernode.Text = file.filePath;
                            if (x.GetElementsByTagName("hook").Count != 0)
                            {
                                string str1 = "hook";
                                string str2 = "NAME";
                                string str3 = "plugins/TimeManagementReminder.xml";
                                foreach (XmlNode node in x.GetElementsByTagName(str1))
                                {
                                    try
                                    {
                                        if (node.Attributes[str2].Value == str3)
                                        {
                                            string reminder = "";
                                            DateTime dt = DateTime.Now;
                                            if (node.InnerXml != "")
                                            {
                                                reminder = node.InnerXml.Split('\"')[1];
                                                long unixTimeStamp = Convert.ToInt64(reminder);
                                                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                                dt = startTime.AddMilliseconds(unixTimeStamp);
                                            }
                                            else
                                            {
                                                reminder = GetAttribute(node.ParentNode, "RememberTime");
                                                if (reminder == "")
                                                {
                                                }
                                                else
                                                {
                                                    long unixTimeStamp = Convert.ToInt64(reminder);
                                                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                                    dt = startTime.AddMilliseconds(unixTimeStamp);
                                                }
                                            }
                                            //添加提醒到提醒清单
                                            string dakainfo = "";
                                            if (GetAttribute(node.ParentNode, "ISDAKA") == "true")
                                            {
                                                dakainfo = " | " + GetAttribute(node.ParentNode, "DAKADAY");
                                            }
                                            string taskName1 = "";
                                            string taskNameDis = "";
                                            bool isEncrypted = false;
                                            taskName1 = node.ParentNode.Attributes["TEXT"].Value;
                                            if (taskName1.Length > 6)
                                            {
                                                if (taskName1.Substring(0, 3) == "***")
                                                {
                                                    if (PassWord != "")
                                                    {
                                                        taskName1 = encrypt.DecryptString(node.ParentNode.Attributes["TEXT"].Value);
                                                        isEncrypted = true;
                                                    }
                                                }
                                            }
                                            taskNameDis = taskName1;
                                            if (IsFileUrl(taskName1))
                                            {
                                                if (Path.GetExtension(taskName1) != "")
                                                {
                                                    taskNameDis = "#" + Path.GetFileName(taskName1);
                                                }
                                                else
                                                {
                                                    taskNameDis = "Path:" + Path.GetFullPath(taskName1).Split('\\').Last(m => m != "");
                                                }
                                            }
                                            if (GetAttribute(node.ParentNode, "ISVIEW") == "true")
                                            {
                                                taskNameDis = "待：" + taskNameDis;
                                            }
                                            if (taskName1.ToLower() != "bin")
                                            {
                                                reminderList.Items.Add(new MyListBoxItemRemind
                                                {
                                                    Text = dt.ToString("yy-MM-dd-HH:mm") + @"  " + taskNameDis + dakainfo,
                                                    Name = taskName1,
                                                    Time = dt,
                                                    Value = file.filePath,
                                                    IsShow = false,
                                                    remindertype = GetAttribute(node.ParentNode, "REMINDERTYPE"),
                                                    rhours = MyToInt16(GetAttribute(node.ParentNode, "RHOUR")),
                                                    rdays = MyToInt16(GetAttribute(node.ParentNode, "RDAYS")),
                                                    rMonth = MyToInt16(GetAttribute(node.ParentNode, "RMONTH")),
                                                    rWeek = MyToInt16(GetAttribute(node.ParentNode, "RWEEK")),
                                                    rweeks = GetAttribute(node.ParentNode, "RWEEKS").ToCharArray(),
                                                    ryear = MyToInt16(GetAttribute(node.ParentNode, "RYEAR")),
                                                    rtaskTime = MyToInt16(GetAttribute(node.ParentNode, "TASKTIME")),
                                                    IsDaka = GetAttribute(node.ParentNode, "ISDAKA"),
                                                    IsView = GetAttribute(node.ParentNode, "ISVIEW"),
                                                    DakaDay = MyToInt16(GetAttribute(node.ParentNode, "DAKADAY")),
                                                    level = MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")),
                                                    ebstring = MyToInt16(GetAttribute(node.ParentNode, "EBSTRING")),
                                                    DakaDays = StrToInt(GetAttribute(node.ParentNode, "DAKADAYS").Split(',')),
                                                    editTime = 0,
                                                    isEncrypted = isEncrypted,
                                                    link = GetAttribute(node.ParentNode, "LINK"),
                                                    IDinXML = GetAttribute(node.ParentNode, "ID")
                                                });
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                reminderList.Sorted = false;
                                reminderList.Sorted = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        //用树视图打开思维导图
                        reminderlistSelectedItem = new MyListBoxItemRemind() { Name = file.name, Value = file.filePath, IDinXML = "" };
                        mindmapornode.Text = file.name;//进入查看子节点一样的逻辑，按w才能退出
                        ShowMindmap();
                        ShowMindmapFile();
                        nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                        this.Height = maxheight;
                        nodetree.Focus();
                        searchword.Text = "";
                        return;
                    }
                }
                else
                {
                    System.Xml.XmlDocument x = new XmlDocument();
                    x.Load(file.filePath);
                    XmlNode root = x.GetElementsByTagName("node")[0];
                    //if (root.ChildNodes.Cast<XmlNode>().Any(m => m.Attributes[0].Name != "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString()))
                    if (!haschildNode(root, DateTime.Now.Year.ToString()))
                    {
                        XmlNode yearNode = x.CreateElement("node");
                        XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                        yearNodeValue.Value = DateTime.Now.Year.ToString();
                        yearNode.Attributes.Append(yearNodeValue);
                        XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
                    }
                    XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString());
                    if (!haschildNode(year, DateTime.Now.Month.ToString()))
                    {
                        XmlNode monthNode = x.CreateElement("node");
                        XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                        monthNodeValue.Value = DateTime.Now.Month.ToString();
                        monthNode.Attributes.Append(monthNodeValue);
                        XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
                    }
                    XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Month.ToString());
                    if (!haschildNode(month, DateTime.Now.Day.ToString()))
                    {
                        XmlNode dayNode = x.CreateElement("node");
                        XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                        dayNodeValue.Value = DateTime.Now.Day.ToString();
                        dayNode.Attributes.Append(dayNodeValue);
                        XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
                    }
                    XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Day.ToString());
                    XmlNode newNote = x.CreateElement("node");
                    XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                    DateTime taskTime = DateTime.Now;
                    //任务时间
                    if (taskName.Contains("明天"))
                    {
                        taskTime = taskTime.AddDays(1);
                        taskName = taskName.Replace("明天", "");
                    }
                    if (taskName.Contains("后天"))
                    {
                        taskTime = taskTime.AddDays(2);
                        taskName = taskName.Replace("后天", "");
                    }
                    bool isHasHour = false;
                    if (taskName.Contains("10点"))
                    {
                        int hourDiff = 10 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("10点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("11点"))
                    {
                        int hourDiff = 11 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("11点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("12点"))
                    {
                        int hourDiff = 12 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("12点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("13点"))
                    {
                        int hourDiff = 13 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("13点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("14点"))
                    {
                        int hourDiff = 14 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("14点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("15点"))
                    {
                        int hourDiff = 15 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("15点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("16点"))
                    {
                        int hourDiff = 16 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("16点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("17点"))
                    {
                        int hourDiff = 17 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("17点", "");
                    }
                    if (taskName.Contains("18点"))
                    {
                        int hourDiff = 18 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("18点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("19点"))
                    {
                        int hourDiff = 19 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("19点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("20点"))
                    {
                        int hourDiff = 20 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("20点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("21点"))
                    {
                        int hourDiff = 21 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("21点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("22点"))
                    {
                        int hourDiff = 22 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("22点", "");
                    }
                    if (taskName.Contains("4点"))
                    {
                        int hourDiff = 4 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("4点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("5点"))
                    {
                        int hourDiff = 5 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("5点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("6点"))
                    {
                        int hourDiff = 6 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("6点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("7点"))
                    {
                        int hourDiff = 7 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("7点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("7点"))
                    {
                        int hourDiff = 8 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("8点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("9点"))
                    {
                        int hourDiff = 9 - taskTime.Hour;
                        taskTime = taskTime.AddHours(hourDiff);
                        taskName = taskName.Replace("9点", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("15分"))
                    {
                        int hourDiff = 15 - taskTime.Minute;
                        taskTime = taskTime.AddMinutes(hourDiff);
                        taskName = taskName.Replace("15分", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("30分"))
                    {
                        int hourDiff = 30 - taskTime.Minute;
                        taskTime = taskTime.AddMinutes(hourDiff);
                        taskName = taskName.Replace("30分", "");
                        isHasHour = true;
                    }
                    if (taskName.Contains("45分"))
                    {
                        int hourDiff = 45 - taskTime.Minute;
                        taskTime = taskTime.AddMinutes(hourDiff);
                        taskName = taskName.Replace("45分", "");
                        isHasHour = true;
                    }
                    if (isHasHour && taskName.Contains("整"))
                    {
                        int hourDiff = 0 - taskTime.Minute;
                        taskTime = taskTime.AddMinutes(hourDiff);
                        taskName = taskName.Replace("整", "");
                    }
                    if (isHasHour && taskName.Contains("半"))
                    {
                        int hourDiff = 30 - taskTime.Minute;
                        taskTime = taskTime.AddMinutes(hourDiff);
                        taskName = taskName.Replace("半", "");
                    }
                    string taskLevel1 = "1";
                    MatchCollection jc = Regex.Matches(taskName, @"[1-9]\d*j");
                    foreach (Match m in jc)
                    {
                        taskName = taskName.Replace(m.Value, "");
                        taskLevel1 = m.Value.Substring(0, m.Value.Length - 1);
                        break;
                    }
                    MatchCollection Mc = Regex.Matches(taskName, @"[1-9]\d*month");
                    foreach (Match m in Mc)
                    {
                        taskName = taskName.Replace(m.Value, "");
                        taskTime = taskTime.AddMonths(Convert.ToInt32(m.Value.Substring(0, m.Value.Length - 5)));
                        break;
                    }
                    MatchCollection mc = Regex.Matches(taskName, @"[1-9]\d*m");
                    string minutes = "0";
                    foreach (Match m in mc)
                    {
                        taskName = taskName.Replace(m.Value, "");
                        minutes = m.Value.Substring(0, m.Value.Length - 1);
                        break;
                    }
                    //几年以后
                    MatchCollection Yc = Regex.Matches(taskName, @"[1-9]\d*Y");
                    foreach (Match m in Yc)
                    {
                        taskName = taskName.Replace(m.Value, "");
                        taskTime = taskTime.AddYears(Convert.ToInt32(m.Value.Substring(0, m.Value.Length - 1)));
                        break;
                    }

                    MatchCollection Dc = Regex.Matches(taskName, @"[1-9]\d*D");
                    foreach (Match m in Dc)
                    {
                        taskName = taskName.Replace(m.Value, "");
                        taskTime = taskTime.AddDays(Convert.ToInt64(m.Value.Substring(0, m.Value.Length - 1)));
                        break;
                    }
                    MatchCollection Hc = Regex.Matches(taskName, @"[1-9]\d*H");
                    foreach (Match m in Hc)
                    {
                        taskName = taskName.Replace(m.Value, "");
                        taskTime = taskTime.AddHours(Convert.ToInt64(m.Value.Substring(0, m.Value.Length - 1)));
                        break;
                    }
                    Mc = Regex.Matches(taskName, @"[1-9]\d*M");
                    foreach (Match m in Mc)
                    {
                        taskName = taskName.Replace(m.Value, "");
                        taskTime = taskTime.AddMinutes(Convert.ToInt64(m.Value.Substring(0, m.Value.Length - 1)));
                        break;
                    }
                    newNotetext.Value = taskName;
                    XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                    newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                    XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                    newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                    newNote.Attributes.Append(newNotetext);
                    newNote.Attributes.Append(newNoteCREATED);
                    newNote.Attributes.Append(newNoteMODIFIED);
                    XmlAttribute TASKLEVEL = x.CreateAttribute("TASKLEVEL");
                    newNote.Attributes.Append(TASKLEVEL);
                    newNote.Attributes["TASKLEVEL"].Value = taskLevel1;
                    XmlAttribute TASKTIME = x.CreateAttribute("TASKTIME");
                    newNote.Attributes.Append(TASKTIME);
                    newNote.Attributes["TASKTIME"].Value = minutes;
                    XmlAttribute TASKID = x.CreateAttribute("ID");
                    newNote.Attributes.Append(TASKID);
                    newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                    //如果已.开始，不设置任务，只添加文本
                    if (!(taskName.StartsWith(".") || taskName.StartsWith(" ") || taskName.StartsWith("hhh")))
                    {
                        newNotetext.Value = taskName;
                        XmlNode remindernode = x.CreateElement("hook");
                        XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                        remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                        remindernode.Attributes.Append(remindernodeName);
                        XmlNode remindernodeParameters = x.CreateElement("Parameters");
                        XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                        remindernodeTime.Value = (Convert.ToInt64((taskTime - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        remindernodeParameters.Attributes.Append(remindernodeTime);
                        remindernode.AppendChild(remindernodeParameters);
                        newNote.AppendChild(remindernode);
                    }
                    else
                    {
                        newNotetext.Value = taskName.Substring(1);
                    }
                    if (IsURL(newNotetext.Value))
                    {
                        string title = GetWebTitle(newNotetext.Value);
                        if (title != "" && title != "忘记了，后面再改")
                        {
                            //添加属性
                            XmlAttribute TASKLink = x.CreateAttribute("LINK");
                            TASKLink.Value = newNotetext.Value;
                            newNote.Attributes.Append(TASKLink);
                            newNotetext.Value = title;
                        }
                    }
                    day.AppendChild(newNote);
                    searchword.Text = "";
                    if (ebdefault.Contains(new FileInfo(file.filePath).Name))
                    {
                        XmlAttribute REMINDERTYPE = x.CreateAttribute("REMINDERTYPE");
                        REMINDERTYPE.Value = "eb";
                        XmlAttribute RDAYS = x.CreateAttribute("RDAYS");
                        XmlAttribute RWEEK = x.CreateAttribute("RWEEK");
                        XmlAttribute RMONTH = x.CreateAttribute("RMONTH");
                        XmlAttribute RWEEKS = x.CreateAttribute("RWEEKS");
                        XmlAttribute RYEAR = x.CreateAttribute("RYEAR");
                        XmlAttribute RHOUR = x.CreateAttribute("RHOUR");
                        newNote.Attributes.Append(REMINDERTYPE);
                        newNote.Attributes.Append(RDAYS);
                        newNote.Attributes.Append(RWEEK);
                        newNote.Attributes.Append(RMONTH);
                        newNote.Attributes.Append(RWEEKS);
                        newNote.Attributes.Append(RYEAR);
                        newNote.Attributes.Append(RHOUR);
                    }
                    x.Save(file.filePath);
                    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(file.filePath));
                    th.Start();
                    SaveLog("添加任务@：" + taskName + "    导图" + filename);
                    ReSetValue();
                    searchword.Text = "";
                    //如果新添加的思維导图没有，自动刷新，放弃了，刷新一下吧，不自动了
                    RRReminderlist();
                    return;
                }
            }
            if (mindmaplist.SelectedIndex >= 0 && (searchword.Text.EndsWith(".") || searchword.Text.EndsWith("hhh")) && searchword.Text != "")
            {
                string path = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(path);
                XmlNode root = x.GetElementsByTagName("node")[0];
                if (!haschildNode(root, DateTime.Now.Year.ToString()))
                {
                    XmlNode yearNode = x.CreateElement("node");
                    XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                    yearNodeValue.Value = DateTime.Now.Year.ToString();
                    yearNode.Attributes.Append(yearNodeValue);
                    XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
                }
                XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString());
                if (!haschildNode(year, DateTime.Now.Month.ToString()))
                {
                    XmlNode monthNode = x.CreateElement("node");
                    XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                    monthNodeValue.Value = DateTime.Now.Month.ToString();
                    monthNode.Attributes.Append(monthNodeValue);
                    XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
                }
                XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Month.ToString());
                if (!haschildNode(month, DateTime.Now.Day.ToString()))
                {
                    XmlNode dayNode = x.CreateElement("node");
                    XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                    dayNodeValue.Value = DateTime.Now.Day.ToString();
                    dayNode.Attributes.Append(dayNodeValue);
                    XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
                }
                XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Day.ToString());
                XmlNode newNote = x.CreateElement("node");
                string changedtaskname = "";
                XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                string taskname = searchword.Text.Substring(0, searchword.Text.Length - 1);
                if (IsEncryptBool)
                {
                    if (PassWord == "")
                    {
                        return;
                    }
                    changedtaskname = encrypt.EncryptString(taskname);
                    IsEncryptBool = false;
                }
                else
                {
                    if (IsURL(taskname.Trim()))
                    {
                        changedtaskname = GetWebTitle(taskname.Trim()) + " | " + taskname;
                    }
                    else
                    {
                        changedtaskname = taskname;
                    }
                }
                SaveLog("添加任务：" + changedtaskname + "    导图：" + ((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3));
                newNotetext.Value = changedtaskname;
                if (IsURL(newNotetext.Value))
                {
                    string title = GetWebTitle(newNotetext.Value);
                    if (title != "" && title != "忘记了，后面再改")
                    {
                        //添加属性
                        XmlAttribute TASKLink = x.CreateAttribute("LINK");
                        TASKLink.Value = newNotetext.Value;
                        newNote.Attributes.Append(TASKLink);
                        newNotetext.Value = title;
                    }
                }
                XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                newNote.Attributes.Append(newNotetext);
                newNote.Attributes.Append(newNoteCREATED);
                newNote.Attributes.Append(newNoteMODIFIED);
                XmlAttribute TASKID = x.CreateAttribute("ID");
                newNote.Attributes.Append(TASKID);
                newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                XmlAttribute TASKLEVEL = x.CreateAttribute("TASKLEVEL");
                newNote.Attributes.Append(TASKLEVEL);
                newNote.Attributes["TASKLEVEL"].Value = "1";
                XmlNode remindernode = x.CreateElement("hook");
                XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                remindernode.Attributes.Append(remindernodeName);
                XmlNode remindernodeParameters = x.CreateElement("Parameters");
                XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                remindernodeParameters.Attributes.Append(remindernodeTime);
                remindernode.AppendChild(remindernodeParameters);
                newNote.AppendChild(remindernode);
                day.AppendChild(newNote);
                x.Save(path);
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                ReSetValue();
                taskname = "";
                searchword.Text = "";
                RRReminderlist();
            }
            //给任务添加节点
            if ((reminderList.SelectedIndex >= 0 || reminderListBox.SelectedIndex >= 0) && searchword.Text != "" && IsSelectReminder)
            {
                string currentPath = "";
                MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
                if (mindmapornode.Text != "")
                {
                    if (mindmapornode.Text.Contains(">"))
                    {
                        currentPath = showMindmapName;
                    }
                    else
                    {
                        mindmapfile file = mindmapfiles.FirstOrDefault(m => m.name.ToLower() == mindmapornode.Text.ToLower());//不区分大小写 //是否需要优化下这个逻辑呢？？
                        if (file == null)
                        {
                            return;
                        }
                        currentPath = file.filePath;
                    }
                }
                else
                {
                    currentPath = selectedReminder.Value;
                }
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(currentPath);
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    try
                    {
                        if (node == null || node.Attributes == null || node.Attributes["ID"] == null)
                        {
                            continue;
                        }
                        if (node.Attributes["ID"].Value == selectedReminder.IDinXML)
                        {
                            bool isadddate = false;
                            string taskName = searchword.Text;
                            if (taskName.StartsWith("."))
                            {
                                isadddate = true;
                                taskName = taskName.Substring(1);
                            }
                            else if (AddTaskWithDate.Contains(selectedReminder.Name))
                            {
                                isadddate = true;
                            }
                            XmlNode newNote = x.CreateElement("node");
                            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                            newNotetext.Value = taskName;
                            if (IsURL(newNotetext.Value))
                            {
                                string title = GetWebTitle(newNotetext.Value);
                                if (title != "" && title != "忘记了，后面再改")
                                {
                                    //添加属性
                                    XmlAttribute TASKLink = x.CreateAttribute("LINK");
                                    TASKLink.Value = newNotetext.Value;
                                    newNote.Attributes.Append(TASKLink);
                                    newNotetext.Value = title;
                                }
                            }

                            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                            newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                            newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            newNote.Attributes.Append(newNotetext);
                            newNote.Attributes.Append(newNoteCREATED);
                            newNote.Attributes.Append(newNoteMODIFIED);
                            XmlAttribute TASKID = x.CreateAttribute("ID");
                            newNote.Attributes.Append(TASKID);
                            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                            XmlAttribute TASKLEVEL = x.CreateAttribute("TASKLEVEL");
                            newNote.Attributes.Append(TASKLEVEL);
                            newNote.Attributes["TASKLEVEL"].Value = "1";
                            if (istask)
                            {
                                XmlNode remindernode = x.CreateElement("hook");
                                XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                                remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                                remindernode.Attributes.Append(remindernodeName);
                                XmlNode remindernodeParameters = x.CreateElement("Parameters");
                                XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                                //如果是子节点，时间和子节点一样
                                if (dateTimePicker.Value > DateTime.Now)
                                {
                                    remindernodeTime.Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                                }
                                else
                                {
                                    remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                                }
                                remindernodeParameters.Attributes.Append(remindernodeTime);
                                remindernode.AppendChild(remindernodeParameters);
                                newNote.AppendChild(remindernode);
                            }

                            if (isadddate)
                            {
                                XmlNode root = node;
                                if (!haschildNode(root, DateTime.Now.Year.ToString()))
                                {
                                    XmlNode yearNode = x.CreateElement("node");
                                    XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                                    yearNodeValue.Value = DateTime.Now.Year.ToString();
                                    yearNode.Attributes.Append(yearNodeValue);
                                    XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
                                }
                                XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString());
                                if (!haschildNode(year, DateTime.Now.Month.ToString()))
                                {
                                    XmlNode monthNode = x.CreateElement("node");
                                    XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                                    monthNodeValue.Value = DateTime.Now.Month.ToString();
                                    monthNode.Attributes.Append(monthNodeValue);
                                    XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
                                }
                                XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Month.ToString());
                                if (!haschildNode(month, DateTime.Now.Day.ToString()))
                                {
                                    XmlNode dayNode = x.CreateElement("node");
                                    XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                                    dayNodeValue.Value = DateTime.Now.Day.ToString();
                                    dayNode.Attributes.Append(dayNodeValue);
                                    XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
                                }
                                XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Day.ToString());
                                day.AppendChild(newNote);
                            }
                            else
                            {
                                node.AppendChild(newNote);
                            }
                            SaveLog("添加子节点：" + taskName + "      @节点：" + selectedReminder.Name + "    导图：" + ((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3));
                            x.Save(selectedReminder.Value);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                            th.Start();
                            searchword.Text = "";
                            reminderSelectIndex = reminderList.SelectedIndex;
                            try
                            {
                                if (focusedList == 0)
                                {
                                    isneedreminderlistrefresh = false;
                                    ReminderListSelectedIndex(reminderSelectIndex);
                                    reminderList.Focus();
                                }
                                else
                                {
                                    reminderListBox.Focus();
                                }
                            }
                            catch (Exception ex)
                            {
                                reminderList.Focus();
                            }
                            if (istask)
                            {
                                ReSetValue();
                                RRReminderlist();
                            }
                            else
                            {
                                ShowSubNode();
                            }
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            //给导图添加节点
            if (mindmaplist.SelectedIndex >= 0 && searchword.Text != "" && !IsSelectReminder)
            {
                string path = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(path);
                XmlNode root = x.GetElementsByTagName("node")[0];
                if (!haschildNode(root, DateTime.Now.Year.ToString()))
                {
                    XmlNode yearNode = x.CreateElement("node");
                    XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                    yearNodeValue.Value = DateTime.Now.Year.ToString();
                    yearNode.Attributes.Append(yearNodeValue);
                    XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
                }
                XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString());
                if (!haschildNode(year, DateTime.Now.Month.ToString()))
                {
                    XmlNode monthNode = x.CreateElement("node");
                    XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                    monthNodeValue.Value = DateTime.Now.Month.ToString();
                    monthNode.Attributes.Append(monthNodeValue);
                    XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
                }
                XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Month.ToString());
                if (!haschildNode(month, DateTime.Now.Day.ToString()))
                {
                    XmlNode dayNode = x.CreateElement("node");
                    XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                    dayNodeValue.Value = DateTime.Now.Day.ToString();
                    dayNode.Attributes.Append(dayNodeValue);
                    XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
                }
                XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Day.ToString());

                XmlNode newNote = x.CreateElement("node");
                string changedtaskname = searchword.Text;
                XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                newNotetext.Value = changedtaskname;
                if (IsURL(newNotetext.Value))
                {
                    string title = GetWebTitle(newNotetext.Value);
                    if (title != "" && title != "忘记了，后面再改")
                    {
                        //添加属性
                        XmlAttribute TASKLink = x.CreateAttribute("LINK");
                        TASKLink.Value = newNotetext.Value;
                        newNote.Attributes.Append(TASKLink);
                        newNotetext.Value = title;
                    }
                }
                XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                newNote.Attributes.Append(newNotetext);
                newNote.Attributes.Append(newNoteCREATED);
                newNote.Attributes.Append(newNoteMODIFIED);
                XmlAttribute TASKID = x.CreateAttribute("ID");
                newNote.Attributes.Append(TASKID);
                newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                XmlAttribute TASKLEVEL = x.CreateAttribute("TASKLEVEL");
                newNote.Attributes.Append(TASKLEVEL);
                newNote.Attributes["TASKLEVEL"].Value = "1";
                XmlNode newElem = x.CreateElement("icon");
                XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                BUILTIN.Value = "flag-orange";
                newElem.Attributes.Append(BUILTIN);
                newNote.AppendChild(newElem);
                if (istask)
                {
                    XmlNode remindernode = x.CreateElement("hook");
                    XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                    remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                    remindernode.Attributes.Append(remindernodeName);
                    XmlNode remindernodeParameters = x.CreateElement("Parameters");
                    XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                    //如果是子节点，时间和子节点一样
                    if (dateTimePicker.Value > DateTime.Now)
                    {
                        remindernodeTime.Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                    }
                    else
                    {
                        remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                    }
                    remindernodeParameters.Attributes.Append(remindernodeTime);
                    remindernode.AppendChild(remindernodeParameters);
                    newNote.AppendChild(remindernode);
                }
                day.AppendChild(newNote);
                x.Save(path);
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                SaveLog("添加任务：" + changedtaskname + "    导图：" + ((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3));
                searchword.Text = "";
                RRReminderlist();
            }
            else if (searchword.Text != "" && !searchword.Text.EndsWith(".") && !searchword.Text.EndsWith("hhh"))
            {
                AddTaskToFile(ini.ReadString("path", "binmm", ""), "Tasks", searchword.Text, true);
                tasklevel.Value = 0;
                taskTime.Value = 0;
                searchword.Text = "";
                RRReminderlist();
            }
        }
        public void AddNodeByID(bool istask, string taskName, string parent)
        {
            try
            {
                bool isadddate = false;
                if (taskName.StartsWith("."))
                {
                    isadddate = true;
                    taskName = taskName.Substring(1);
                }
                else if (AddTaskWithDate.Contains(parent))
                {
                    isadddate = true;
                }
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(showMindmapName);
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == renameMindMapFileID)
                    {
                        XmlNode newNote = x.CreateElement("node");
                        XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                        newNotetext.Value = taskName;
                        if (IsURL(newNotetext.Value))
                        {
                            string title = GetWebTitle(newNotetext.Value);
                            if (title != "" && title != "忘记了，后面再改")
                            {
                                //添加属性
                                XmlAttribute TASKLink = x.CreateAttribute("LINK");
                                TASKLink.Value = newNotetext.Value;
                                newNote.Attributes.Append(TASKLink);
                                newNotetext.Value = title;
                            }
                        }
                        XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                        newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                        newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        newNote.Attributes.Append(newNotetext);
                        newNote.Attributes.Append(newNoteCREATED);
                        newNote.Attributes.Append(newNoteMODIFIED);
                        XmlAttribute TASKID = x.CreateAttribute("ID");
                        newNote.Attributes.Append(TASKID);
                        newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                        //XmlNode newElem = x.CreateElement("icon");
                        //XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                        //BUILTIN.Value = "flag-orange";
                        //newElem.Attributes.Append(BUILTIN);
                        //newNote.AppendChild(newElem);
                        if (istask)
                        {
                            XmlNode remindernode = x.CreateElement("hook");
                            XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                            remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                            remindernode.Attributes.Append(remindernodeName);
                            XmlNode remindernodeParameters = x.CreateElement("Parameters");
                            XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                            remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            remindernodeParameters.Attributes.Append(remindernodeTime);
                            remindernode.AppendChild(remindernodeParameters);
                            newNote.AppendChild(remindernode);
                            fenshuADD(3);
                        }
                        if (isadddate)
                        {
                            XmlNode root = node;
                            if (!haschildNode(root, DateTime.Now.Year.ToString()))
                            {
                                XmlNode yearNode = x.CreateElement("node");
                                XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                                yearNodeValue.Value = DateTime.Now.Year.ToString();
                                yearNode.Attributes.Append(yearNodeValue);
                                XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
                            }
                            XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString());
                            if (!haschildNode(year, DateTime.Now.Month.ToString()))
                            {
                                XmlNode monthNode = x.CreateElement("node");
                                XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                                monthNodeValue.Value = DateTime.Now.Month.ToString();
                                monthNode.Attributes.Append(monthNodeValue);
                                XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
                            }
                            XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Month.ToString());
                            if (!haschildNode(month, DateTime.Now.Day.ToString()))
                            {
                                XmlNode dayNode = x.CreateElement("node");
                                XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                                dayNodeValue.Value = DateTime.Now.Day.ToString();
                                dayNode.Attributes.Append(dayNodeValue);
                                XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
                            }
                            XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Day.ToString());
                            day.AppendChild(newNote);
                        }
                        else
                        {
                            node.AppendChild(newNote);
                        }
                        searchword.Text = "";
                        x.Save(showMindmapName);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                        th.Start();
                        ShowSubNode();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public XmlNode AddNodeInNodeTree(string taskName)
        {
            try
            {
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(showMindmapName);
                //x.GetElementById(id).RemoveAll(); ;
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (nodetree.Nodes.Contains(nodetree.SelectedNode))
                    {
                        if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == nodetree.Nodes[0].Name)
                        {
                            XmlNode newNote = x.CreateElement("node");
                            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                            newNotetext.Value = taskName;
                            if (IsURL(newNotetext.Value))
                            {
                                string title = GetWebTitle(newNotetext.Value);
                                if (title != "" && title != "忘记了，后面再改")
                                {
                                    //添加属性
                                    XmlAttribute TASKLink = x.CreateAttribute("LINK");
                                    TASKLink.Value = newNotetext.Value;
                                    newNote.Attributes.Append(TASKLink);
                                    newNotetext.Value = title;
                                }
                            }
                            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                            newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                            newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            newNote.Attributes.Append(newNotetext);
                            newNote.Attributes.Append(newNoteCREATED);
                            newNote.Attributes.Append(newNoteMODIFIED);
                            XmlAttribute TASKID = x.CreateAttribute("ID");
                            newNote.Attributes.Append(TASKID);
                            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                            node.ParentNode.AppendChild(newNote);//根站点的父亲，应该就是根节点
                            searchword.Text = "";
                            x.Save(showMindmapName);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                            th.Start();
                            return newNote;
                        }
                    }
                    else
                    {
                        if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == nodetree.SelectedNode.Parent.Name)
                        {
                            XmlNode newNote = x.CreateElement("node");
                            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                            newNotetext.Value = taskName;
                            if (IsURL(newNotetext.Value))
                            {
                                string title = GetWebTitle(newNotetext.Value);
                                if (title != "" && title != "忘记了，后面再改")
                                {
                                    //添加属性
                                    XmlAttribute TASKLink = x.CreateAttribute("LINK");
                                    TASKLink.Value = newNotetext.Value;
                                    newNote.Attributes.Append(TASKLink);
                                    newNotetext.Value = title;
                                }
                            }
                            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                            newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                            newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            newNote.Attributes.Append(newNotetext);
                            newNote.Attributes.Append(newNoteCREATED);
                            newNote.Attributes.Append(newNoteMODIFIED);
                            XmlAttribute TASKID = x.CreateAttribute("ID");
                            newNote.Attributes.Append(TASKID);
                            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                            node.AppendChild(newNote);
                            searchword.Text = "";
                            x.Save(showMindmapName);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                            th.Start();
                            return newNote;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void mindmaplist_MouseUp(object sender, MouseEventArgs e)
        {
            LeaveTime();
            IsSelectReminder = false;
        }
        private void feeling_Click(object sender, EventArgs e)
        {
            AddTaskToFile("home.mm", "feeling", searchword.Text, false);
            searchword.Text = "";
        }
        public bool haschildNode(XmlNode node, string child)
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
        public void WriteLog(string task, string mindmap, DateTime taskTime, String Btnstring)
        {
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load("Home.mm");
            XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == "feeling");
            //if (root.ChildNodes.Cast<XmlNode>().Any(m => m.Attributes[0].Name != "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString()))
            if (!haschildNode(root, taskTime.Year.ToString()))
            {
                XmlNode yearNode = x.CreateElement("node");
                XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                yearNodeValue.Value = taskTime.Year.ToString();
                yearNode.Attributes.Append(yearNodeValue);
                XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
            }
            XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes["TEXT"].Value == taskTime.Year.ToString());
            if (!haschildNode(year, taskTime.Month.ToString()))
            {
                XmlNode monthNode = x.CreateElement("node");
                XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                monthNodeValue.Value = taskTime.Month.ToString();
                monthNode.Attributes.Append(monthNodeValue);
                XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
            }
            XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes["TEXT"].Value == taskTime.Month.ToString());
            if (!haschildNode(month, taskTime.Day.ToString()))
            {
                XmlNode dayNode = x.CreateElement("node");
                XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                dayNodeValue.Value = taskTime.Day.ToString();
                dayNode.Attributes.Append(dayNodeValue);
                XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
            }
            XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes["TEXT"].Value == taskTime.Day.ToString());
            if (!haschildNode(day, "Task"))
            {
                XmlNode taskNodeAdd = x.CreateElement("node");
                XmlAttribute taskNodeValue = x.CreateAttribute("TEXT");
                taskNodeValue.Value = "Task";
                taskNodeAdd.Attributes.Append(taskNodeValue);
                day.AppendChild(taskNodeAdd);
            }
            XmlNode taskNode = day.ChildNodes.Cast<XmlNode>().First(m => m.Attributes["TEXT"].Value == "Task");
            if (!haschildNode(taskNode, mindmap))
            {
                XmlNode mindmapNodeAdd = x.CreateElement("node");
                XmlAttribute mindmapNodeValue = x.CreateAttribute("TEXT");
                mindmapNodeValue.Value = mindmap;
                mindmapNodeAdd.Attributes.Append(mindmapNodeValue);
                taskNode.AppendChild(mindmapNodeAdd);
            }
            XmlNode mindmapNode = taskNode.ChildNodes.Cast<XmlNode>().First(m => m.Attributes["TEXT"].Value == mindmap);
            XmlNode newNote = x.CreateElement("node");
            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
            newNotetext.Value = task;
            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
            newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
            newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            newNote.Attributes.Append(newNotetext);
            newNote.Attributes.Append(newNoteCREATED);
            newNote.Attributes.Append(newNoteMODIFIED);
            XmlAttribute TASKID = x.CreateAttribute("ID");
            newNote.Attributes.Append(TASKID);
            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
            XmlNode newElem = x.CreateElement("icon");
            XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
            BUILTIN.Value = Btnstring;
            switch (Btnstring)
            {
                case "button_ok":
                    SaveLog("完成任务：" + task + "    导图" + mindmap);
                    break;
                case "button_cancel":
                    SaveLog("取消任务：" + task + "    导图" + mindmap);
                    break;
                default:
                    break;
            }
            newElem.Attributes.Append(BUILTIN);
            newNote.AppendChild(newElem);
            mindmapNode.AppendChild(newNote);
            x.Save("Home.mm");
            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile("Home.mm"));
            th.Start();
        }
        private void DelayAllTask(object sender, EventArgs e)
        {
            try
            {
                List<string> pathList = new List<string>();
                foreach (MyListBoxItemRemind item in reminderList.Items)
                {
                    if (item.Time <= DateTime.Today)
                    {
                        DelayTask(item, true);
                        if (!pathList.Contains(item.Value))
                        {
                            pathList.Add(item.Value);
                        }
                    }
                }
                //将pathList的所有文件转换，这样只需要转换一次，也不需要多次DelayAll了
                foreach (string path in pathList)
                {
                    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                    th.Start();
                }

                ReSetValue();
                RRReminderlist();
                PlaySimpleSound("deny");
            }
            catch (Exception ex)
            {
            }
            ReminderListSelectedIndex(0);
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            CanceSelectedlTask();
        }
        public void CanceSelectedlTask(bool IsAddIcon = true)
        {
            try
            {
                int reminderIndex = reminderList.SelectedIndex;
                if (reminderListBox.Focused)
                {
                    reminderIndex = reminderListBox.SelectedIndex;
                }
                CancelTask(IsAddIcon);
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                //shaixuanfuwei();
                //ChangeReminder();
                PlaySimpleSound("deny");
                if (reminderList.Focused)
                {
                    reminderList.Items.RemoveAt(reminderIndex);
                    ReminderListSelectedIndex(reminderIndex);
                }
                else if (reminderListBox.Focused)
                {
                    reminderboxList.Remove((MyListBoxItemRemind)reminderlistSelectedItem);
                    Xnodes.RemoveAll(m => m.Contains(((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML));
                    //添加去重
                    List<string> xnodesRemoveSame = new List<string>();
                    foreach (string item in Xnodes)
                    {
                        if (!xnodesRemoveSame.Contains(item))
                        {
                            xnodesRemoveSame.Add(item);
                        }
                    }
                    Xnodes = xnodesRemoveSame;
                    new TextListConverter().WriteListToTextFile(Xnodes, System.AppDomain.CurrentDomain.BaseDirectory + @"\Xnodes.txt");

                    reminderListBox.Items.RemoveAt(reminderIndex);
                    ReminderlistBoxChange();
                    reminderListBox.SelectedIndex = reminderIndex;
                }

            }
            catch (Exception ex)
            {
                if (reminderList.Focused)
                {
                    if (reminderList.Items.Count > 0)
                    {
                        reminderList.SetSelected(0, true);
                    }
                }
                else if (reminderListBox.Focused)
                {
                    ReminderlistBoxChange();
                    if (reminderListBox.Items.Count > 0)
                    {
                        reminderListBox.SetSelected(0, true);
                    }
                }
            }
        }
        public void RemoveMyListBoxItemRemind()
        {
            try
            {
                int reminderIndex = reminderList.SelectedIndex;
                RemoveRemember();
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                reminderList.Items.RemoveAt(reminderIndex);
                ReminderListSelectedIndex(reminderIndex);
            }
            catch (Exception ex)
            {
                if (reminderList.Items.Count > 0)
                {
                    reminderList.SetSelected(0, true);
                }

            }
        }
        public void RemoveRemember()
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            foreach (XmlNode node in x.GetElementsByTagName("icon"))
            {
                try
                {
                    if (node.Attributes["BUILTIN"].Value == "flag-orange" && node.ParentNode.Attributes["TEXT"].Value == selectedReminder.Name)
                    {
                        node.Attributes["BUILTIN"].Value = "flag-yellow";
                        x.Save(selectedReminder.Value);
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public void deleteNodeByID(string id)
        {
            try
            {
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(showMindmapName);
                fenshuADD(1);
                //x.GetElementById(id).RemoveAll(); ;
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == id)
                    {
                        node.ParentNode.RemoveChild(node);
                        x.Save(showMindmapName);
                        PlaySimpleSound("delete");
                        nodetree.SelectedNode.Remove();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void UPNodeByID(string id)
        {
            try
            {

                XElement file = XElement.Load(showMindmapName);
                XElement element = file.DescendantsAndSelf().Where(x => x.Attribute("ID") != null && x.Attribute("ID").Value == id).SingleOrDefault();
                MoveElementUp(element);
                file.Save(showMindmapName);
            }
            catch (Exception ex)
            {
            }
        }
        public void DownNodeByID(string id)
        {
            try
            {
                XElement file = XElement.Load(showMindmapName);
                XElement element = file.DescendantsAndSelf().Where(x => x.Attribute("ID") != null && x.Attribute("ID").Value == id).SingleOrDefault();
                MoveElementDown(element);
                file.Save(showMindmapName);
            }
            catch (Exception ex)
            {
            }
        }
        public void LeftNodeByID(string id)
        {
            try
            {
                XElement file = XElement.Load(showMindmapName);
                XElement element = file.DescendantsAndSelf().Where(x => x.Attribute("ID") != null && x.Attribute("ID").Value == id).SingleOrDefault();
                MoveElementLeft(element);
                file.Save(showMindmapName);
            }
            catch (Exception ex)
            {
            }
        }
        public void RightNodeByID(string id)
        {
            try
            {
                XElement file = XElement.Load(showMindmapName);
                XElement element = file.DescendantsAndSelf().Where(x => x.Attribute("ID") != null && x.Attribute("ID").Value == id).SingleOrDefault();
                MoveElementRight(element);
                file.Save(showMindmapName);
            }
            catch (Exception ex)
            {
            }
        }
        public bool SetTaskNodeByID(string id)
        {
            try
            {
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(showMindmapName);
                fenshuADD(1);
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == id && !istask(node))
                    {
                        XmlNode remindernode = x.CreateElement("hook");
                        XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                        remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                        remindernode.Attributes.Append(remindernodeName);
                        XmlNode remindernodeParameters = x.CreateElement("Parameters");
                        XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                        remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        remindernodeParameters.Attributes.Append(remindernodeTime);
                        remindernode.AppendChild(remindernodeParameters);
                        node.AppendChild(remindernode);
                        //图标
                        XmlNode newElem = x.CreateElement("icon");
                        XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                        BUILTIN.Value = "clock";
                        newElem.Attributes.Append(BUILTIN);
                        node.AppendChild(newElem);
                        x.Save(showMindmapName);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool istask(XmlNode node)
        {
            try
            {
                foreach (XmlNode item in node.ChildNodes)
                {
                    if (item.Name == "hook" && item.Attributes != null && item.Attributes["NAME"] != null && item.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 编辑节点名称
        /// </summary>
        /// <param name="taskName"></param>
        public void RenameNodeByID(string taskName)
        {
            try
            {
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(showMindmapName);
                fenshuADD(2);
                //x.GetElementById(id).RemoveAll(); ;
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == renameMindMapFileID)
                    {
                        //修改文件的名字
                        try
                        {
                            if (GetAttribute(node, "LINK") != "")
                            {
                                FileInfo file = new FileInfo(GetAttribute(node, "LINK").Replace("file:/", ""));
                                if (node.Attributes["TEXT"].InnerText == file.Name)
                                {
                                    file.MoveTo(file.DirectoryName + "\\" + taskName);
                                    node.Attributes["LINK"].Value = "file:/" + file.DirectoryName + "\\" + taskName;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        EditMODIFIEDAndTaskName(node, taskName);

                        x.Save(showMindmapName);
                        PlaySimpleSound("edittask");

                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                        th.Start();
                        if (renameMindMapFileIDParent != "")
                        {
                            renameMindMapFileID = renameMindMapFileIDParent;
                            renameMindMapFileIDParent = "";
                        }
                        //((MyListBoxItemRemind)reminderlistSelectedItem).Text = ();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void AddFileTaskToMap(System.Xml.XmlDocument x, string mindmap, string rootNode, string taskName, string link, string md5, DateTime createDate, string path = "", bool toTaskbool = true)
        {
            if (taskName == "")
            {
                return;
            }
            XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == rootNode);
            XmlNode day = null;
            if (path == "")
            {
                if (!haschildNode(root, createDate.Year.ToString()))
                {
                    XmlNode yearNode = x.CreateElement("node");
                    XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                    yearNodeValue.Value = createDate.Year.ToString();
                    yearNode.Attributes.Append(yearNodeValue);
                    XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
                }
                XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == createDate.Year.ToString());
                if (!haschildNode(year, createDate.Month.ToString()))
                {
                    XmlNode monthNode = x.CreateElement("node");
                    XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                    monthNodeValue.Value = createDate.Month.ToString();
                    monthNode.Attributes.Append(monthNodeValue);
                    XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
                }
                XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == createDate.Month.ToString());
                if (!haschildNode(month, createDate.Day.ToString()))
                {
                    XmlNode dayNode = x.CreateElement("node");
                    XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                    dayNodeValue.Value = createDate.Day.ToString();
                    dayNode.Attributes.Append(dayNodeValue);
                    XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
                }
                day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == createDate.Day.ToString());
            }
            else
            {
                day = addSubNodes(root, path);
            }
            XmlNode newNote = x.CreateElement("node");
            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
            newNotetext.Value = taskName;
            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
            newNoteCREATED.Value = (Convert.ToInt64((createDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
            newNoteMODIFIED.Value = (Convert.ToInt64((createDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            if (toTaskbool)
            {
                XmlNode remindernode = x.CreateElement("hook");
                XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                remindernode.Attributes.Append(remindernodeName);
                XmlNode remindernodeParameters = x.CreateElement("Parameters");
                XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                remindernodeTime.Value = (Convert.ToInt64((createDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                remindernodeParameters.Attributes.Append(remindernodeTime);
                remindernode.AppendChild(remindernodeParameters);
                newNote.AppendChild(remindernode);
            }

            XmlAttribute TASKLink = x.CreateAttribute("LINK");
            TASKLink.Value = link;
            newNote.Attributes.Append(TASKLink);

            XmlAttribute MD5 = x.CreateAttribute("MD5");
            MD5.Value = md5;
            newNote.Attributes.Append(MD5);

            newNote.Attributes.Append(newNotetext);
            newNote.Attributes.Append(newNoteCREATED);
            newNote.Attributes.Append(newNoteMODIFIED);
            XmlAttribute TASKID = x.CreateAttribute("ID");
            newNote.Attributes.Append(TASKID);
            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
            day.AppendChild(newNote);
        }
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
            }
        }
        public XmlNode getchildNode(XmlNode node, string child)
        {
            foreach (XmlNode item in node.ChildNodes.Cast<XmlNode>().Where(m => m.Name == "node"))
            {
                if (item.Attributes.Cast<XmlAttribute>().Any(m => m.Name == "TEXT"))
                {
                    if (item.Attributes["TEXT"].Value == child)
                    {
                        return item;
                    }
                }
            }
            return node;
        }
        public XmlNode addSubNodes(XmlNode root, string path)
        {
            XmlNode subnode = root;
            foreach (string item in path.Split('\\'))
            {
                if (!item.Contains("."))
                {
                    if (!haschildNode(subnode, item))
                    {
                        XmlNode yearNode = subnode.OwnerDocument.CreateElement("node");
                        XmlAttribute yearNodeValue = subnode.OwnerDocument.CreateAttribute("TEXT");
                        yearNodeValue.Value = item;
                        yearNode.Attributes.Append(yearNodeValue);
                        XmlAttribute yearNodeTASKID = subnode.OwnerDocument.CreateAttribute("ID");
                        yearNode.Attributes.Append(yearNodeTASKID);
                        yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString();
                        subnode.AppendChild(yearNode);
                        subnode = yearNode;
                    }
                    else
                    {
                        subnode = getchildNode(subnode, item);
                    }
                }
            }
            return subnode;
        }
        public void CancelTask(bool IsAddIcon = true)
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("node"))
            {
                if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == selectedReminder.IDinXML)
                {
                    try
                    {
                        if (mindmapornode.Text == "" || (mindmapornode.Text != "" && !mindmapornode.Text.Contains(">")))
                        {
                            try
                            {
                                if (node.Attributes["MD5"] != null && node.Attributes["MD5"].InnerText != "")
                                {
                                    File.Delete(node.Attributes["LINK"].InnerText);
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                            foreach (XmlNode item in node.ChildNodes)
                            {
                                if (item.Name == "hook")
                                {
                                    item.ParentNode.RemoveChild(item);
                                }
                            }
                            if (IsAddIcon)
                            {
                                XmlNode newElem = x.CreateElement("icon");
                                XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                                BUILTIN.Value = "button_cancel";
                                newElem.Attributes.Append(BUILTIN);
                                node.AppendChild(newElem);
                                SaveLog("取消任务：" + taskName + "    导图" + selectedReminder.Value);
                            }
                            else
                            {
                                SaveLog("结束周期任务：" + taskName + "    导图" + selectedReminder.Value);
                            }
                        }
                        else
                        {
                            if (mindmapornode.Text.Contains(">"))
                            {
                                node.ParentNode.RemoveChild(node);
                                SaveLog("删除节点：" + taskName + "    导图" + selectedReminder.Value);
                            }
                        }
                        x.Save(selectedReminder.Value);
                        return;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        private void Daka(bool setdakai = true)
        {
            try
            {
                SetDaka(setdakai);
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                ReSetValue();
                RRReminderlist();
            }
            catch (Exception ex)
            {
                if (reminderList.Items.Count > 0)
                {
                    reminderList.SetSelected(0, true);
                }

            }
        }
        private void setjinianfunction(bool setstartData = true)
        {
            try
            {
                SetJinian(setstartData);
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                ReSetValue();
                RRReminderlist();
            }
            catch (Exception ex)
            {
                if (reminderList.Items.Count > 0)
                {
                    reminderList.SetSelected(0, true);
                }

            }
        }
        #endregion

        #region 打卡
        public void SetDaka(bool setdakai = true)
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                    {
                        if (!setdakai)
                        {
                            node.ParentNode.Attributes["ISDAKA"].Value = "false";
                        }
                        else
                        {
                            XmlNode newElem = x.CreateElement("icon");
                            XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                            BUILTIN.Value = "addition";
                            newElem.Attributes.Append(BUILTIN);
                            node.ParentNode.AppendChild(newElem);

                            if (node.ParentNode.Attributes["ISDAKA"] != null)
                            {
                                node.ParentNode.Attributes["ISDAKA"].Value = "true";
                            }
                            else
                            {
                                XmlAttribute IsJinian = x.CreateAttribute("ISDAKA");
                                IsJinian.Value = "true";
                                node.ParentNode.Attributes.Append(IsJinian);
                            }

                            if (node.ParentNode.Attributes["DAKADAY"] == null)
                            {
                                XmlAttribute DAKADAY = x.CreateAttribute("DAKADAY");
                                DAKADAY.Value = "0";
                                node.ParentNode.Attributes.Append(DAKADAY);
                            }
                            if (node.ParentNode.Attributes["LeftDakaDays"] == null)
                            {
                                XmlAttribute DAKADAY = x.CreateAttribute("LeftDakaDays");
                                DAKADAY.Value = "0";
                                node.ParentNode.Attributes.Append(DAKADAY);
                            }
                            if (node.ParentNode.Attributes["DAKADAYSr"] == null)
                            {
                                XmlAttribute DAKADAYS = x.CreateAttribute("DAKADAYS");
                                DAKADAYS.Value = "";
                                node.ParentNode.Attributes.Append(DAKADAYS);
                            }
                        }
                        x.Save(selectedReminder.Value);
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

        #region 日志

        //添加一个方法，保存错误日志到软件跟站点的error.txt文件中
        public void AddErrorLog(Exception e)
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\error.txt";
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }
            string str = "错误时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
            str += "错误信息：" + e.Message + "\r\n";
            str += "错误源：" + e.Source + "\r\n";
            str += "堆栈信息：" + e.StackTrace + "\r\n";
            str += "触发方法：" + e.TargetSite + "\r\n";
            str += "\r\n\r\n";
            System.IO.File.AppendAllText(path, str);
        }
        #endregion


        public void SetJinian(bool setstartData = true)
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                    {
                        if (setstartData)
                        {
                            XmlNode newElem = x.CreateElement("icon");
                            XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                            BUILTIN.Value = "addition";
                            newElem.Attributes.Append(BUILTIN);
                            node.ParentNode.AppendChild(newElem);
                            if (node.ParentNode.Attributes["IsJinian"] != null)
                            {
                                node.ParentNode.Attributes["IsJinian"].Value = "true";
                            }
                            else
                            {
                                XmlAttribute IsJinian = x.CreateAttribute("IsJinian");
                                IsJinian.Value = "true";
                                node.ParentNode.Attributes.Append(IsJinian);
                            }
                            //时间
                            if (node.ParentNode.Attributes["JinianBeginTime"] == null)
                            {
                                XmlAttribute JinianBeginTime = x.CreateAttribute("JinianBeginTime");
                                JinianBeginTime.Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                                node.ParentNode.Attributes.Append(JinianBeginTime);
                            }
                            else
                            {
                                node.ParentNode.Attributes["JinianBeginTime"].Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            }
                        }
                        else
                        {
                            if (node.ParentNode.Attributes["IsJinian"] != null && node.ParentNode.Attributes["IsJinian"].Value == "true")
                            {
                                node.ParentNode.Attributes["IsJinian"].Value = "false";
                            }
                        }

                        x.Save(selectedReminder.Value);
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public int[] StrToInt(string[] str)
        {
            int[] result = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == "")
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = int.Parse(str[i]);
                }
            }
            return result;
        }
        public static string intostringwithlenght(int num, int lenght)
        {
            string resut = num.ToString();
            for (int i = resut.Length; i < lenght; i++)
            {
                resut = " " + resut;
            }
            return resut;
        }
        public double GetAVAge(int[] arr, int dakaday)
        {
            int num = 0;
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    num += 1;
                    sum += arr[i];
                }
            }
            if (dakaday != 0)
            {
                num += 1;
                sum += dakaday;
            }
            return (double)sum / num;
        }
        public int GetNUM(int[] arr, int dakaday)
        {
            int num = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    num += 1;
                }
            }
            if (dakaday != 0)
            {
                num += 1;
            }
            return num;
        }
        public int GetSUM(int[] arr, int dakaday)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    sum += arr[i];
                }
            }
            if (dakaday != 0)
            {
                sum += dakaday;
            }
            return sum;
        }
        public int GetMax(int[] arr, int dakaday)
        {
            if (arr.Max() > dakaday)
            {
                return arr.Max();
            }
            else
            {
                return dakaday;
            }
        }
        private void Reminderlist_MouseUp(object sender, MouseEventArgs e)
        {
            LeaveTime();
            if (e.Button == MouseButtons.Right && reminderList.SelectedIndex >= 0)
            {
                Clipboard.SetDataObject(((MyListBoxItemRemind)reminderlistSelectedItem).Name);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if it is a hotkey, return true; otherwise, return false
            switch (keyData)
            {
                //case Keys.NumPad0:
                //    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void tasklevel_ValueChanged(object sender, EventArgs e)
        {
            // if (reminderList.SelectedIndex<0||reminderList.SelectedIndex<0)
            // {
            //     RRReminderlist();
            // }
        }
        private void taskTime_ValueChanged(object sender, EventArgs e)
        {
            // if (reminderListFocused())
            // {
            //     RRReminderlist();
            // }
        }
        private void nifan_CheckedChanged(object sender, EventArgs e)
        {
            RRReminderlist();
        }
        public void ReSetValue()
        {
            taskTime.Value = 0;
            tasklevel.Value = 0;
        }
        public void AddTaskToFile(string mindmap, string rootNode, string taskName, bool hasTime)
        {
            if (taskName == "")
            {
                return;
            }
            if (IsEncryptBool)
            {
                if (PassWord == "")
                {
                    MessageBox.Show("请设置密码！");
                    return;
                }
                taskName = encrypt.EncryptString(taskName);
                IsEncryptBool = false;
            }
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(mindmap);
            XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == rootNode);
            //if (root.ChildNodes.Cast<XmlNode>().Any(m => m.Attributes[0].Name != "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString()))
            if (!haschildNode(root, DateTime.Now.Year.ToString()))
            {
                XmlNode yearNode = x.CreateElement("node");
                XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                yearNodeValue.Value = DateTime.Now.Year.ToString();
                yearNode.Attributes.Append(yearNodeValue);
                XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
            }
            XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Year.ToString());
            if (!haschildNode(year, DateTime.Now.Month.ToString()))
            {
                XmlNode monthNode = x.CreateElement("node");
                XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                monthNodeValue.Value = DateTime.Now.Month.ToString();
                monthNode.Attributes.Append(monthNodeValue);
                XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
            }
            XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Month.ToString());
            if (!haschildNode(month, DateTime.Now.Day.ToString()))
            {
                XmlNode dayNode = x.CreateElement("node");
                XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                dayNodeValue.Value = DateTime.Now.Day.ToString();
                dayNode.Attributes.Append(dayNodeValue);
                XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
            }
            XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == DateTime.Now.Day.ToString());
            XmlNode newNote = x.CreateElement("node");
            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
            string pstr = "";
            if (!hasTime)
            {
                pstr = DateTime.Now.ToString("HH:mm") + "    ";
            }
            newNotetext.Value = pstr + taskName;
            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
            newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
            newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            if (ebdefault.Contains(new FileInfo(mindmap).Name))
            {
                XmlAttribute REMINDERTYPE = x.CreateAttribute("REMINDERTYPE");
                REMINDERTYPE.Value = "eb";
                XmlAttribute RDAYS = x.CreateAttribute("RDAYS");
                XmlAttribute RWEEK = x.CreateAttribute("RWEEK");
                XmlAttribute RMONTH = x.CreateAttribute("RMONTH");
                XmlAttribute RWEEKS = x.CreateAttribute("RWEEKS");
                XmlAttribute RYEAR = x.CreateAttribute("RYEAR");
                XmlAttribute RHOUR = x.CreateAttribute("RHOUR");
                newNote.Attributes.Append(REMINDERTYPE);
                newNote.Attributes.Append(RDAYS);
                newNote.Attributes.Append(RWEEK);
                newNote.Attributes.Append(RMONTH);
                newNote.Attributes.Append(RWEEKS);
                newNote.Attributes.Append(RYEAR);
                newNote.Attributes.Append(RHOUR);
                hasTime = true;
            }
            if (hasTime)
            {
                XmlNode remindernode = x.CreateElement("hook");
                XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                remindernode.Attributes.Append(remindernodeName);
                XmlNode remindernodeParameters = x.CreateElement("Parameters");
                XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                remindernodeParameters.Attributes.Append(remindernodeTime);
                remindernode.AppendChild(remindernodeParameters);
                newNote.AppendChild(remindernode);
            }
            newNote.Attributes.Append(newNotetext);
            newNote.Attributes.Append(newNoteCREATED);
            newNote.Attributes.Append(newNoteMODIFIED);
            XmlAttribute TASKID = x.CreateAttribute("ID");
            newNote.Attributes.Append(TASKID);
            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
            day.AppendChild(newNote);
            x.Save(mindmap);
            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(mindmap));
            th.Start();
        }
        public void AddClipToTask(bool istask = false)
        {
            IDataObject iData = new DataObject();
            iData = Clipboard.GetDataObject();
            string log = (string)iData.GetData(DataFormats.Text);
            if (log == null || log == "")
            {
                return;
            }
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            foreach (XmlNode node in x.GetElementsByTagName("node"))
            {
                try
                {
                    if (node.Attributes["ID"].Value == selectedReminder.IDinXML)
                    {
                        XmlNode newNote = x.CreateElement("node");
                        XmlAttribute newNotetext = x.CreateAttribute("TEXT");
                        newNotetext.Value = log;
                        if (IsURL(newNotetext.Value))
                        {
                            string title = GetWebTitle(newNotetext.Value);
                            if (title != "" && title != "忘记了，后面再改")
                            {
                                //添加属性
                                XmlAttribute TASKLink = x.CreateAttribute("LINK");
                                TASKLink.Value = newNotetext.Value;
                                newNote.Attributes.Append(TASKLink);
                                newNotetext.Value = title;
                            }
                        }
                        XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
                        newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
                        newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                        newNote.Attributes.Append(newNotetext);
                        newNote.Attributes.Append(newNoteCREATED);
                        newNote.Attributes.Append(newNoteMODIFIED);
                        XmlAttribute TASKID = x.CreateAttribute("ID");
                        newNote.Attributes.Append(TASKID);
                        newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
                        //XmlNode newElem = x.CreateElement("icon");
                        //XmlAttribute BUILTIN = x.CreateAttribute("BUILTIN");
                        //BUILTIN.Value = "flag-orange";
                        //newElem.Attributes.Append(BUILTIN);
                        //newNote.AppendChild(newElem);
                        if (istask)
                        {
                            XmlNode remindernode = x.CreateElement("hook");
                            XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                            remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                            remindernode.Attributes.Append(remindernodeName);
                            XmlNode remindernodeParameters = x.CreateElement("Parameters");
                            XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                            remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                            remindernodeParameters.Attributes.Append(remindernodeTime);
                            remindernode.AppendChild(remindernodeParameters);
                            newNote.AppendChild(remindernode);
                        }
                        node.AppendChild(newNote);
                        SaveLog("添加子节点：" + searchword.Text + "      @节点：" + selectedReminder.Name + "    导图：" + ((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3));
                        searchword.Text = "";
                        x.Save(selectedReminder.Value);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                        th.Start();
                        searchword.Text = "";
                        RRReminderlist();
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            tasklevel.Value = 0;
            taskTime.Value = 0;
            RRReminderlist();
            Clipboard.Clear();
        }
        private void mindmaplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Updateunchkeckmindmap();
            InMindMapBool = true;
            if (isInReminderList)
            {
                isInReminderList = false;
                return;
            }
            if (searchword.Text.StartsWith("#"))
            {
                return;
            }
            if (searchword.Text.StartsWith("*"))
            {
                if (searchword.Text.StartsWith("*"))
                {
                    for (int i = 0; i < mindmaplist.Items.Count; i++)
                    {
                        if (mindmaplist.CheckedItems.IndexOf(mindmaplist.Items[i]) == -1)
                        {
                            for (int k = reminderList.Items.Count - 1; k > 0; k--)
                            {
                                if (((MyListBoxItemRemind)reminderList.Items[k]).Value == ((MyListBoxItem)mindmaplist.Items[i]).Value)
                                {
                                    reminderList.Items.RemoveAt(k);
                                }
                            }
                        }
                    }
                }
                return;
            }
            if (c_ViewModel.Checked)
            {
                RRReminderlist();
            }
        }
        public bool IsURL(string url)
        {
            string matchStr = @"http(s)?://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]";
            return Regex.IsMatch(url, matchStr);
        }
        public string GetUrl(string str)
        {
            string matchStr = @"http(s)?://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]";
            return Regex.Match(str, matchStr).Value;
        }
        private String GetWebTitle(String url)
        {
            System.Net.WebRequest wb;
            //请求资源
            try
            {
                wb = System.Net.WebRequest.Create(url.Trim());
            }
            catch (Exception ex)
            {

                return "";
            }
            //响应请求
            WebResponse webRes = null;
            //将返回的数据放入流中
            Stream webStream = null;
            try
            {
                webRes = wb.GetResponse();
                webStream = webRes.GetResponseStream();
            }
            catch (Exception ex)
            {
                return "";
            }
            //从流中读出数据
            StreamReader sr = new StreamReader(webStream, System.Text.Encoding.UTF8);
            //创建可变字符对象，用于保存网页数据
            StringBuilder sb = new StringBuilder();
            //读出数据存入可变字符中
            String str = "";
            while ((str = sr.ReadLine()) != null)
            {
                sb.Append(str);
            }
            //建立获取网页标题正则表达式
            String regex = @"(?<=<title>).+(?=</title>)";
            //返回网页标题
            String title = Regex.Match(sb.ToString(), regex).ToString();
            title = Regex.Replace(title, " ", "");
            //返回网页标题  
            title = Regex.Replace(title, @"[\""]+", "");
            if (title.Length > 50)
            {
                if (title.Contains('<'))
                {
                    title = title.Split('<')[0];
                    return title;
                }
                return title.Substring(0, 49);
            }
            title = title.Replace("<title>", "").Replace("</title>", "");
            title = Regex.Replace(title, @"((?=[\x21-\x7e]+)[^A-Za-z0-9])", "");
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "\\html\\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "\\html\\");
                }
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "\\html\\" + ReplaceSpecialCharacterV2(DateTime.Now.ToString() + title) + ".html", sb.ToString());
            }
            catch (Exception ex)
            {
                return title;
            }
            return title;
        }
        public string ReplaceSpecialCharacterV2(string str)
        {
            List<string> charArr = new List<string>() { "\\", "/", "*", "?", "<", ">", "|", ":", "\"" };
            return charArr.Aggregate(str, (current, c) => current.Replace(c, ""));
        }
        private void hiddenmenu_DoubleClick(object sender, EventArgs e)
        {
            Thread th = new Thread(() => OpenMenu());
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            //Center();//= new Point(this.Location.X, -1569);
            MyHide();
        }
        private void Home_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Home.mm");
        }
        private void bin_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ini.ReadString("path", "binmm", ""));
        }

        public static int GetPosition()
        {
            for (int i = 0; i < fanqiePosition.Length; i++)
            {
                if (!fanqiePosition[i])
                {
                    return i;
                }
            }
            return 100;
        }
        public void AddClip()
        {
            IDataObject iData = new DataObject();
            iData = Clipboard.GetDataObject();
            string log = (string)iData.GetData(DataFormats.Text);
            if (log == null || log == "" || mindmaplist.SelectedItem == null)
            {
                return;
            }
            if (IsURL(log.Trim()))
            {
                log = GetWebTitle(log.Trim()) + " | " + log;
            }
            string path = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(path);
            DateTime dt = DateTime.Now;
            //XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == rootNode);
            XmlNode root = x.GetElementsByTagName("node")[0];
            //if (root.ChildNodes.Cast<XmlNode>().Any(m => m.Attributes[0].Name != "TEXT" && m.Attributes["TEXT"].Value == dt.Year.ToString()))
            if (!haschildNode(root, dt.Year.ToString()))
            {
                XmlNode yearNode = x.CreateElement("node");
                XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                yearNodeValue.Value = dt.Year.ToString();
                yearNode.Attributes.Append(yearNodeValue);
                XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
            }
            XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == dt.Year.ToString());
            if (!haschildNode(year, dt.Month.ToString()))
            {
                XmlNode monthNode = x.CreateElement("node");
                XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                monthNodeValue.Value = dt.Month.ToString();
                monthNode.Attributes.Append(monthNodeValue);
                XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
            }
            XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == dt.Month.ToString());
            if (!haschildNode(month, dt.Day.ToString()))
            {
                XmlNode dayNode = x.CreateElement("node");
                XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                dayNodeValue.Value = dt.Day.ToString();
                dayNode.Attributes.Append(dayNodeValue);
                XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
            }
            XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == dt.Day.ToString());
            XmlNode newNote = x.CreateElement("node");
            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
            newNotetext.Value = log;
            if (IsURL(newNotetext.Value))
            {
                string title = GetWebTitle(newNotetext.Value);
                if (title != "" && title != "忘记了，后面再改")
                {
                    //添加属性
                    XmlAttribute TASKLink = x.CreateAttribute("LINK");
                    TASKLink.Value = newNotetext.Value;
                    newNote.Attributes.Append(TASKLink);
                    newNotetext.Value = title;
                }
            }
            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
            newNoteCREATED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
            newNoteMODIFIED.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            newNote.Attributes.Append(newNotetext);
            newNote.Attributes.Append(newNoteCREATED);
            newNote.Attributes.Append(newNoteMODIFIED);
            XmlAttribute TASKID = x.CreateAttribute("ID");
            newNote.Attributes.Append(TASKID);
            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
            XmlNode remindernode = x.CreateElement("hook");
            XmlAttribute remindernodeName = x.CreateAttribute("NAME");
            remindernodeName.Value = "plugins/TimeManagementReminder.xml";
            remindernode.Attributes.Append(remindernodeName);
            XmlNode remindernodeParameters = x.CreateElement("Parameters");
            XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
            remindernodeTime.Value = (Convert.ToInt64((DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            remindernodeParameters.Attributes.Append(remindernodeTime);
            remindernode.AppendChild(remindernodeParameters);
            newNote.AppendChild(remindernode);
            day.AppendChild(newNote);
            x.Save(path);
            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
            th.Start();
            tasklevel.Value = 0;
            taskTime.Value = 0;
            RRReminderlist();
            Clipboard.Clear();
        }


        private void IsEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                if (reminderlistSelectedItem == null || PassWord == "")
                {
                    return;
                }
                MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(selectedReminder.Value);
                string taskName = selectedReminder.Name;
                if (selectedReminder.isEncrypted)
                {
                    taskName = encrypt.EncryptString(taskName);
                }
                foreach (XmlNode node in x.GetElementsByTagName("hook"))
                {
                    try
                    {
                        if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                        {
                            if (selectedReminder.isEncrypted)
                            {
                                node.ParentNode.Attributes["TEXT"].Value = encrypt.DecryptString(taskName);
                            }
                            else
                            {
                                node.ParentNode.Attributes["TEXT"].Value = encrypt.EncryptString(taskName);
                            }
                            x.Save(selectedReminder.Value);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                string path = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(path));
                th.Start();
                ReSetValue();
                RRReminderlist();
            }
            catch (Exception ex)
            {
                if (reminderList.Items.Count > 0)
                {
                    reminderList.SetSelected(0, true);
                }

            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ReminderListFocused() || reminderListBox.Focused || mindmaplist.Focused)
            {
                if (e.KeyCode != Keys.Space && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down && e.KeyCode != Keys.D1 && e.KeyCode != Keys.D2 && e.KeyCode != Keys.D3 && e.KeyCode != Keys.D4 && e.KeyCode != Keys.D5 && e.KeyCode != Keys.D6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.D8 && e.KeyCode != Keys.D9 && e.KeyCode != Keys.D0)
                {
                    e.SuppressKeyPress = true;
                }
            }
        }
        public bool keyNotWork(KeyEventArgs e)
        {
            return !(PathcomboBox.Focused || searchword.Focused || nodetreeSearch.Focused || hopeNote.Focused || richTextSubNode.Focused || mindmapSearch.Focused || TimeBlockDate.Focused || (noterichTextBox.Focused && !(e.Modifiers.CompareTo(Keys.Alt) == 0 && e.KeyCode == Keys.N)));
        }
        private async void DocearReminderForm_KeyUp(object sender, KeyEventArgs e)
        {
            LeaveTime();
            if (!keyNotWork(e) && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Escape && e.KeyCode != Keys.Down && e.KeyCode != Keys.F1 && e.KeyCode != Keys.F2 && e.KeyCode != Keys.F4 && e.KeyCode != Keys.F3 && e.KeyCode != Keys.F5 && e.KeyCode != Keys.F6 && e.KeyCode != Keys.D7 && e.KeyCode != Keys.F8 && e.KeyCode != Keys.D9 && e.KeyCode != Keys.F11 && e.KeyCode != Keys.F10 && e.KeyCode != Keys.F12)
            {
                return;
            }
            if (!isneedKeyUpEventWork)
            {
                isneedKeyUpEventWork = true;
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                    {
                        //todo 这个好像没用
                        allFloder = !allFloder;
                    }
                    else
                    {
                        if (IsReminderOnlyCheckBox.Checked)
                        {
                            IsReminderOnlyCheckBox.Checked = false;
                            ReSetValue();
                            RRReminderlist();
                        }
                        else
                        {
                            showcyclereminder.Checked = !showcyclereminder.Checked;
                            ReSetValue();
                            RRReminderlist();
                        }
                    }
                    break;
                case Keys.Add:
                    break;
                case Keys.Alt:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Attn:
                    break;
                case Keys.B:
                    PlaySimpleSound("treeview");
                    if (ReminderListFocused())
                    {
                        if (this.Height != maxheight)
                        {
                            ShowMindmap(true);
                            ShowMindmapFile();
                            this.Height = maxheight;
                            nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                            nodetree.Focus();
                        }
                        else
                        {
                            if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                ShowMindmap(true);
                                ShowMindmapFile();
                                nodetree.Focus();
                                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                            }
                            else
                            {
                                this.Height = normalheight; showMindmapName = "";
                                nodetree.Top = nodetreeTop;
                                FileTreeView.Top = nodetreeTop;
                                nodetree.Height = nodetreeHeight;
                                FileTreeView.Height = nodetreeHeight;
                                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = false;
                                reminderList.Focus();
                            }
                        }
                        Center();
                    }
                    break;
                case Keys.Back:
                    needSuggest = true;
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.C:
                    if (ReminderListFocused())
                    {
                        if (showTimeBlock.Checked || ShowMoney.Checked || ShowKA.Checked)
                        {
                            Clipboard.SetDataObject(((MyListBoxItemRemind)reminderlistSelectedItem).IsDaka);
                            MyHide();
                            return;
                        }
                        if (((MyListBoxItemRemind)reminderlistSelectedItem).link != "")
                        {
                            //如果链接是文件，则直接复制文件本身
                            if (File.Exists(((MyListBoxItemRemind)reminderlistSelectedItem).link))
                            {
                                if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                                {
                                    Clipboard.SetDataObject(((MyListBoxItemRemind)reminderlistSelectedItem).link);
                                }
                                else
                                {
                                    string[] file = new string[1];
                                    file[0] = ((MyListBoxItemRemind)reminderlistSelectedItem).link;
                                    DataObject dataObject = new DataObject();
                                    dataObject.SetData(DataFormats.FileDrop, file);
                                    Clipboard.SetDataObject(dataObject, true);
                                }
                            }
                            else
                            {
                                Clipboard.SetDataObject(((MyListBoxItemRemind)reminderlistSelectedItem).link);
                            }
                        }
                        else
                        {
                            Clipboard.SetDataObject(((MyListBoxItemRemind)reminderlistSelectedItem).Name);
                        }
                        MyHide();
                    }
                    else if (nodetree.Focused && nodetree.SelectedNode != null)
                    {
                        Clipboard.SetDataObject(nodetree.SelectedNode.Text);
                        MyHide();
                    }
                    else if (FileTreeView.Focused && FileTreeView.SelectedNode != null)
                    {
                        Clipboard.SetDataObject(FileTreeView.SelectedNode.Name);
                        MyHide();
                    }
                    else if (searchword.Focused)//这里不会生效的，以为最前面已经拦截了，直接输入ccc就可以了
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            Clipboard.SetDataObject(searchword.Text);
                            MyHide();
                        }
                    }
                    break;
                case Keys.Cancel:
                    break;
                case Keys.CapsLock:

                    break;
                case Keys.Clear:
                    break;
                case Keys.Control:
                    break;
                case Keys.ControlKey:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.D:
                    if (keyNotWork(e))
                    {
                        if (showTimeBlock.Checked)
                        {
                            try
                            {
                                reminderSelectIndex = reminderList.SelectedIndex;
                                reminderObject.reminders.RemoveAll(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML);
                                RRReminderlist();
                                ReminderListSelectedIndex(reminderSelectIndex);
                            }
                            catch (Exception ex)
                            {
                                return;
                            }
                        }
                        //设置任务是待选的快捷键是Ctrl+d，进入待选的快捷键是w
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            int reminderIndex = reminderList.SelectedIndex;
                            SetTaskIsView();
                            try
                            {
                                reminderList.Items.RemoveAt(reminderIndex);
                                RRReminderlist();
                                ReminderListSelectedIndex(reminderIndex);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        //去选中当前思维导图
                        else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            int reminderIndex = reminderList.SelectedIndex;
                            try
                            {
                                for (int i = 0; i < mindmaplist.Items.Count; i++)
                                {
                                    if (((MyListBoxItem)mindmaplist.Items[i]).Value == ((MyListBoxItemRemind)reminderlistSelectedItem).Value)
                                    {
                                        if (mindmaplist.GetItemCheckState(i) == CheckState.Checked)
                                        {
                                            mindmaplist.SetItemChecked(i, false);
                                            mindmaplist.Refresh();
                                            break;
                                        }
                                    }
                                }
                                RRReminderlist();
                                ReminderListSelectedIndex(reminderIndex);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            cancel_btn_Click(null, null);
                        }
                    }
                    break;
                case Keys.D0:
                    if (!(searchword.Focused || taskTime.Focused || tasklevel.Focused || dateTimePicker.Focused))
                    {
                        night.Checked = !night.Checked;
                    }
                    break;
                case Keys.D1:
                    if (keyNotWork(e))
                    {
                        if (isSettingSyncWeek)
                        {
                            c_Monday.Checked = !c_Monday.Checked;
                        }
                        else
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                showtomorrow.Checked = !showtomorrow.Checked;
                                RRReminderlist();
                            }
                            else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                c_ViewModel.Checked = !c_ViewModel.Checked;
                            }
                            else
                            {
                                IsSelectReminder = false;
                                mindmaplist.Focus();
                            }
                        }
                    }
                    break;
                case Keys.D2:
                    if (keyNotWork(e))
                    {
                        if (isSettingSyncWeek)
                        {
                            c_Tuesday.Checked = !c_Tuesday.Checked;
                        }
                        else
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                reminder_week.Checked = !reminder_week.Checked;
                                RRReminderlist();
                            }
                            else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                //moshiview.Checked = !moshiview.Checked;
                            }
                            else
                            {
                                IsSelectReminder = true;
                                ReminderListSelectedIndex(reminderSelectIndex);
                                reminderList.Focus();
                                if (reminderList.SelectedIndex < 0 || reminderList.SelectedIndex > reminderList.Items.Count - 1)
                                {
                                    try
                                    {
                                        ReminderListSelectedIndex(0);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.D3:
                    if (keyNotWork(e))
                    {
                        if (isSettingSyncWeek)
                        {
                            c_Wednesday.Checked = !c_Wednesday.Checked;
                        }
                        else
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                reminder_month.Checked = !reminder_month.Checked;
                                RRReminderlist();
                            }
                            else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                //quanxuan.Checked = !quanxuan.Checked;
                            }
                            else
                            {
                                IsSelectReminder = false;
                                searchword.Focus();
                            }
                        }
                    }
                    break;
                case Keys.D4:
                    if (keyNotWork(e))
                    {
                        if (isSettingSyncWeek)
                        {
                            c_Thursday.Checked = !c_Thursday.Checked;
                        }
                        else
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                reminder_year.Checked = !reminder_year.Checked;
                                RRReminderlist();
                            }
                            else
                            {
                                if (this.Height == maxheight)
                                {
                                    nodetree.Focus();
                                }
                                else
                                {
                                    richTextSubNode.Focus();
                                }
                                //FormX = this.Location.Y;
                                //Center();//= new Point(this.Location.X, -1569);
                                //System.Diagnostics.Process.Start(new DirectoryInfo(((MyListBoxItemRemind)reminderlistSelectedItem).Value).FullName.Substring(0, ((MyListBoxItemRemind)reminderlistSelectedItem).Value.Length - Path.GetFileName(((MyListBoxItemRemind)reminderlistSelectedItem).Value).Length));
                            }
                        }
                    }
                    break;
                case Keys.D5:
                    if (keyNotWork(e))
                    {
                        if (isSettingSyncWeek)
                        {
                            c_Friday.Checked = !c_Friday.Checked;
                        }
                        else
                        {
                            if (this.Height == maxheight)
                            {
                                FileTreeView.Focus();
                            }
                            else
                            {
                                if (e.Modifiers.CompareTo(Keys.Control) == 0)
                                {
                                    reminder_yearafter.Checked = !reminder_yearafter.Checked;
                                }
                                else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                                {
                                    onlyZhouqi.Checked = !onlyZhouqi.Checked;
                                    ReSetValue();
                                    RRReminderlist();
                                }
                                else
                                {
                                    if (IsReminderOnlyCheckBox.Checked)
                                    {
                                        IsReminderOnlyCheckBox.Checked = false;
                                        RRReminderlist();
                                    }
                                    else
                                    {
                                        showcyclereminder.Checked = !showcyclereminder.Checked;
                                        ReSetValue();
                                        RRReminderlist();
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.D6:
                    if (isSettingSyncWeek)
                    {
                        c_Saturday.Checked = !c_Saturday.Checked;
                    }
                    else
                    {
                        IsReminderOnlyCheckBox.Checked = !IsReminderOnlyCheckBox.Checked;
                        RRReminderlist();
                        try
                        {
                            ReminderListSelectedIndex(0);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    break;
                case Keys.D7:
                    if (isSettingSyncWeek)
                    {
                        c_Sunday.Checked = !c_Sunday.Checked;
                    }
                    else
                    {
                        if (!(searchword.Focused || taskTime.Focused || tasklevel.Focused || dateTimePicker.Focused))
                        {
                            morning.Checked = !morning.Checked;
                        }
                    }
                    break;
                case Keys.D8:
                    if (!(searchword.Focused || taskTime.Focused || tasklevel.Focused || dateTimePicker.Focused))
                    {
                        day.Checked = !day.Checked;
                    }
                    break;
                case Keys.D9:
                    if (!(searchword.Focused || taskTime.Focused || tasklevel.Focused || dateTimePicker.Focused))
                    {
                        afternoon.Checked = !afternoon.Checked;
                    }
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Delete:
                    if (nodetree.Focused)
                    {
                        if (nodetree.SelectedNode.Name != null)
                        {
                            try
                            {
                                string deleteNodeName = nodetree.SelectedNode.Text;
                                deleteNodeByID(nodetree.SelectedNode.Name);
                                SaveLog("删除节点：" + deleteNodeName + "    导图" + showMindmapName.Split('\\')[showMindmapName.Split('\\').Length - 1]);
                                fenshuADD(1);
                                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                                th.Start();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        try
                        {
                            if (System.IO.File.Exists(FileTreeView.SelectedNode.Name))
                            {
                                System.IO.File.Delete(FileTreeView.SelectedNode.Name);
                                SaveLog("删除文件：" + FileTreeView.SelectedNode.Name);
                                fenshuADD(1);

                            }
                            else if (System.IO.Directory.Exists(FileTreeView.SelectedNode.Name))
                            {
                                System.IO.Directory.Delete(FileTreeView.SelectedNode.Name);
                                SaveLog("删除文件：" + FileTreeView.SelectedNode.Name);
                                fenshuADD(1);
                            }
                            FileTreeView.SelectedNode.Remove();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (ReminderListFocused())
                    {
                        if (searchword.Text.ToLower().StartsWith("!") || searchword.Text.ToLower().StartsWith("！"))
                        {
                            OpenedInRootSearch.Remove(((MyListBoxItemRemind)reminderlistSelectedItem).Name + "|" + ((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                            new TextListConverter().WriteListToTextFile(OpenedInRootSearch, System.AppDomain.CurrentDomain.BaseDirectory + @"\OpenedInRootSearch.txt");
                            reminderList.Items.RemoveAt(reminderList.SelectedIndex);
                        }
                        else if (searchword.Text.ToLower().StartsWith("`") || searchword.Text.ToLower().StartsWith("·"))
                        {
                            RecentlyFileHelper.DeleteRecentlyFiles(((MyListBoxItemRemind)reminderlistSelectedItem).Name);
                            reminderList.Items.RemoveAt(reminderList.SelectedIndex);
                        }
                    }
                    break;
                case Keys.Divide:
                    break;
                case Keys.Down:
                    leftIndex = 0;
                    if (searchword.Focused && !SearchText_suggest.Visible)
                    {
                        reminderList.Focus();
                        reminderList.Refresh();
                    }
                    else if (nodetree.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            DownNodeByID(nodetree.SelectedNode.Name);
                            Extensions.MoveDown(nodetree.SelectedNode);
                            fenshuADD(1);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                            th.Start();
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            Extensions.MoveDown(FileTreeView.SelectedNode);
                            fenshuADD(1);
                        }
                    }
                    break;
                case Keys.E:
                    if (keyNotWork(e))
                    {
                        c_endtime.Checked = !c_endtime.Checked;//结束时间
                    }
                    break;
                case Keys.End:
                    break;
                case Keys.Enter:
                    if (ReminderListFocused())
                    {
                        try
                        {
                            if (reminderlistSelectedItem == null)
                            {
                                return;
                            }
                            //最近文档
                            if (searchword.Text.ToLower().StartsWith("`") || searchword.Text.ToLower().StartsWith("·"))
                            {
                                isSearchFileOrNode = true;
                                System.Diagnostics.Process.Start(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                                try
                                {
                                    FileInfo file = new FileInfo(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                                    string name = file.Name + "|" + file.FullName;
                                    if (!OpenedInRootSearch.Contains(name))//放到这里也可以放到最终也可以暂时放这里
                                    {
                                        OpenedInRootSearch.Add(name);
                                    }
                                    else
                                    {
                                        OpenedInRootSearch.Remove(name);
                                        OpenedInRootSearch.Add(name);
                                    }
                                    new TextListConverter().WriteListToTextFile(OpenedInRootSearch, System.AppDomain.CurrentDomain.BaseDirectory + @"\OpenedInRootSearch.txt");
                                }
                                catch (Exception ex)
                                {
                                }
                                MyHide();
                                return;
                            }
                            if (IsURL(((MyListBoxItemRemind)reminderlistSelectedItem).Name.Trim()))
                            {
                                System.Diagnostics.Process.Start(GetUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                                SaveLog("打开：    " + GetUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                            }
                            else if (IsFileUrl(((MyListBoxItemRemind)reminderlistSelectedItem).Name.Trim()))
                            {
                                System.Diagnostics.Process.Start(getFileUrlPath(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                                SaveLog("打开：    " + getFileUrlPath(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                            }
                            else if (((MyListBoxItemRemind)reminderlistSelectedItem).link != "" && ((MyListBoxItemRemind)reminderlistSelectedItem).link != null)
                            {
                                //转换相对路径
                                string link = ((MyListBoxItemRemind)reminderlistSelectedItem).link;
                                if (link.StartsWith("."))
                                {
                                    FileInfo file = new FileInfo(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                                    string mindmapfolderPath = file.Directory.FullName;
                                    link = mindmapfolderPath + "\\" + link;
                                    link = link.Replace("/", "\\");
                                    link = link.Replace(@"\\", @"\");
                                }
                                //将链接复制到剪切板
                                Clipboard.SetText(link);
                                System.Diagnostics.Process.Start(link);
                                SaveLog("打开：    " + link);
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                            }
                            try
                            {
                                FileInfo file = new FileInfo(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                                string name = file.Name + "|" + file.FullName;
                                if (!OpenedInRootSearch.Contains(name))//放到这里也可以放到最终也可以暂时放这里
                                {
                                    OpenedInRootSearch.Add(name);
                                }
                                else
                                {
                                    OpenedInRootSearch.Remove(name);
                                    OpenedInRootSearch.Add(name);
                                }
                                new TextListConverter().WriteListToTextFile(OpenedInRootSearch, System.AppDomain.CurrentDomain.BaseDirectory + @"\OpenedInRootSearch.txt");
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        MyHide();
                    }
                    else if (mindmaplist.Focused)
                    {
                        if (mindmaplist.SelectedIndex > -1)
                        {
                            System.Diagnostics.Process.Start(((MyListBoxItem)mindmaplist.SelectedItem).Value);
                            MyHide();
                        }
                    }
                    else if (searchword.Focused)
                    {
                        if (SearchText_suggest.Visible)
                        {
                            return;
                        }
                        if (isRename)
                        {
                            RenameNodeByID(searchword.Text);
                            SaveLog("修改节点名称：" + renameTaskName + "  To  " + searchword.Text);
                            searchword.Text = "";
                            RRReminderlist();
                            return;
                        }
                        if (isRenameTimeBlock && (showTimeBlock.Checked && !searchword.Text.StartsWith(" ")) && !searchword.Text.Contains("刚刚") && !searchword.Text.Contains("@"))//重命名，也就是修改备注的时候
                        {
                            //SaveLog("修改节点名称：" + renameTaskName + "  To  " + searchword.Text);
                            reminderObject.reminders.First(m => m.ID == showMindmapName).comment = searchword.Text;
                            searchword.Text = "";
                            RRReminderlist();
                            reminderList.SelectedIndex = reminderSelectIndex;
                            isRenameTimeBlock = false;
                            return;
                        }
                        if ((showTimeBlock.Checked && !searchword.Text.StartsWith(" ")) && !searchword.Text.Contains("刚刚") && !searchword.Text.Contains("@"))//也就是时间块的详细记录里不允许添加@符号
                        {
                            if (reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).comment == "")//如果时间块上还没有设置备注，就直接设置备注，而不是添加详细信息
                            {
                                reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).comment = searchword.Text;
                                searchword.Text = "";
                                RRReminderlist();
                                ReminderListSelectedIndex(reminderSelectIndex);
                                isRenameTimeBlock = false;
                                return;
                            }
                            else
                            {
                                isRenameTimeBlock = false;
                                reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).DetailComment += ((((MyListBoxItemRemind)reminderlistSelectedItem).remindertype != "" ? Environment.NewLine : "") + searchword.Text);
                                searchword.Text = "";
                                RRReminderlist();
                                ReminderListSelectedIndex(reminderSelectIndex);
                                return;
                            }
                        }
                        if (((showTimeBlock.Checked && !searchword.Text.StartsWith(" ")) && (searchword.Text.Contains("@"))) || searchword.Text.Contains(" @"))
                        {
                            //不管有没有敲击刚刚，则添加刚刚两个字在最前面
                            searchword.Text = "刚刚" + searchword.Text;
                        }

                        //去掉前面的空格
                        while (searchword.Text.StartsWith(" "))
                        {
                            searchword.Text = searchword.Text.Substring(1);
                        }
                        if (searchword.Text.Contains(" @"))
                        {
                            searchword.Text = searchword.Text.Replace(" @", "@");
                        }
                        if (searchword.Text.StartsWith("path:"))
                        {
                            try
                            {
                                if (searchword.Text.Length < 7)
                                {
                                    new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                                    //rootpath = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
                                }
                                else
                                {
                                    string changePath = searchword.Text.Substring(5);
                                    for (int i = 0; i < PathcomboBox.Items.Count; i++)
                                    {
                                        if (PathcomboBox.Items[i].ToString() == changePath)
                                        {
                                            selectedpath = false;
                                            PathcomboBox.SelectedIndex = i;
                                            PathcomboBox_SelectedIndexChanged(null, null);
                                            loadHopeNote();
                                            section = PathcomboBox.SelectedItem.ToString();
                                            break;
                                        }
                                    }
                                    if (changePath.ToLower() == "rss")
                                    {
                                    }
                                    else
                                    {
                                    }
                                    if (changePath.Contains('\\'))
                                    {
                                        rootpath = new DirectoryInfo(searchword.Text.Substring(5));
                                    }
                                    else
                                    {
                                        try
                                        {
                                            rootpath = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", changePath, "")));
                                        }
                                        catch (Exception ex)
                                        {
                                            rootpath = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                                        }
                                    }
                                }
                                if (!pathArr.Contains(rootpath.FullName))
                                {
                                    pathArr.Add(rootpath.FullName);
                                }
                                mindmapPath = rootpath.FullName;

                                searchword.Text = "";
                                UsedLogRenew();
                                Load_Click(null, null);
                                reminderList.Focus();
                                return;
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else if (searchword.Text.ToLower().StartsWith("lock"))
                        {
                            lockForm = true;
                            searchword.Text = "";
                            //保存锁定状态到配置文件
                            ini.WriteString("appearance", "lock", "true");
                        }
                        else if (searchword.Text.ToLower().StartsWith("unlock"))
                        {
                            lockForm = false;
                            searchword.Text = "";
                            //保存锁定状态到配置文件
                            ini.WriteString("appearance", "lock", "false");
                        }
                        else if (searchword.Text.ToLower().StartsWith("o="))
                        {
                            try
                            {
                                string num = searchword.Text.Substring(2);
                                this.Opacity = Convert.ToDouble(num);
                                ini.WriteString("appearance", "Opacity", num);
                                searchword.Text = "";
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        if (searchword.Text.ToLower().StartsWith("gc"))
                        {
                            string gitCommand = "git";
                            //string gitAddArgument = @"add -A";
                            string gitCommitArgument = @"commit -a -m " + searchword.Text.Substring(2);
                            //string gitPushArgument = @"push our_remote";
                            //System.Diagnostics.Process.Start(gitCommand, gitAddArgument);
                            //Thread.Sleep(2000);
                            System.Diagnostics.Process.Start(gitCommand, gitCommitArgument);
                            //System.Diagnostics.Process.Start(gitCommand, gitPushArgument);
                            SaveLog("git commit:" + searchword.Text);
                            if (searchword.Text.EndsWith("e"))
                            {
                                Application.Exit();
                            }
                            else
                            {
                                searchword.Text = "";
                            }
                            return;
                        }
                        else if (searchword.Text.StartsWith("#"))
                        {
                            if (searchword.Text.Length >= 2)
                            {
                                SearchFiles();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (searchword.Text.StartsWith("*"))
                        {
                            if (searchword.Text.Length >= 2)
                            {
                                SearchNode();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (searchword.Text.ToLower().StartsWith("ss"))
                        {
                            ReSetValue();
                            RRReminderlist();
                        }
                        else if (searchword.Text.ToLower().StartsWith("p"))
                        {
                            GetPassWord();
                            searchword.Text = "";
                        }
                        else if (searchword.Text.ToLower().StartsWith("link:"))
                        {
                            SetLink(searchword.Text.Substring(5));
                            searchword.Text = "";
                        }
                        else if (searchword.Text.ToLower().StartsWith("ggg"))
                        {
                            try
                            {
                                if (searchword.Text.Length == 3)
                                {
                                    searchword.Text = "";
                                    WriteTagFile();
                                }
                                else
                                {
                                    tagCloudControl.AddItem(searchword.Text.Substring(3));
                                    SaveLog("添加Tag:    " + searchword.Text.Substring(3).Trim());
                                    searchword.Text = "";
                                    WriteTagFile();
                                    fenshuADD(1);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else if (searchword.Text.ToLower().StartsWith("`") || searchword.Text.ToLower().StartsWith("·"))
                        {
                            try
                            {
                                reminderList.Items.Clear();
                                //reminderlist.Items.AddRange(RecentlyFileHelper.GetRecentlyFiles());
                                foreach (var file in RecentlyFileHelper.GetRecentlyFiles(searchword.Text.Substring(1)))
                                {
                                    try
                                    {
                                        reminderList.Items.Add(file);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                                reminderList.Sorted = false;
                                reminderList.Sorted = true;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else if (searchword.Text.ToLower().StartsWith("pass="))
                        {
                            try
                            {
                                string password = searchword.Text.Substring(5);
                                if (password != "")
                                {
                                    PassWord = password;
                                    encrypt = new Encrypt(PassWord);
                                    IsEncryptBool = true;
                                }
                                else
                                {
                                    PassWord = "";
                                    IsEncryptBool = false;
                                }
                                searchword.Text = "";
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        //else if (searchword.Text.Contains(ini.ReadString("money", "cost", "")) || searchword.Text.Contains(ini.ReadString("money", "income", "")) || searchword.Text.Contains(ini.ReadString("money", "save", "")) || searchword.Text.Contains(ini.ReadString("money", "waste", "")))
                        //{
                        //    bool ishasSound = false;
                        //    if (searchword.Text.Contains(ini.ReadString("money", "save", "")))
                        //    {
                        //        PlaySimpleSound("save");
                        //        ishasSound = true;
                        //        searchword.Text = searchword.Text.Replace(ini.ReadString("money", "save", ""), ini.ReadString("money", "save", "") + ini.ReadString("money", "income", ""));
                        //    }
                        //    if (searchword.Text.Contains(ini.ReadString("money", "waste", "")))
                        //    {
                        //        PlaySimpleSound("waste");
                        //        ishasSound = true;
                        //        searchword.Text = searchword.Text.Replace(ini.ReadString("money", "waste", ""), ini.ReadString("money", "waste", "") + ini.ReadString("money", "cost", ""));
                        //    }
                        //    string taskName = searchword.Text;
                        //    string account = ini.ReadString("money", "account", "");
                        //    string currentAccount = "";
                        //    string money = "0";
                        //    bool isIncome = false;
                        //    MatchCollection jc = Regex.Matches(taskName, ini.ReadString("money", "cost", "") + @"[1-9]\d*\.?\d*");
                        //    foreach (Match m in jc)
                        //    {
                        //        taskName = taskName.Replace(m.Value, "");
                        //        money = m.Value.Substring(2);
                        //        break;
                        //    }
                        //    MatchCollection sr = Regex.Matches(taskName, ini.ReadString("money", "income", "") + @"[1-9]\d*\.?\d*");
                        //    foreach (Match m in sr)
                        //    {
                        //        taskName = taskName.Replace(m.Value, "");
                        //        money = m.Value.Substring(2);
                        //        isIncome = true;
                        //        break;
                        //    }
                        //    foreach (string item in account.Split(';'))
                        //    {
                        //        if (item == "")
                        //        {
                        //            continue;
                        //        }
                        //        if (taskName.Contains(item))
                        //        {
                        //            currentAccount = item;
                        //            taskName = taskName.Replace(currentAccount, "");
                        //        }
                        //    }
                        //    if (!ishasSound)
                        //    {
                        //        if (isIncome)
                        //        {
                        //            PlaySimpleSound("income");
                        //        }
                        //        else
                        //        {
                        //            PlaySimpleSound("cost");
                        //        }
                        //    }

                        //    AddMoney(ini.ReadString("money", "money", ""), currentAccount, money, isIncome, taskName);
                        //    try
                        //    {
                        //        if (searchword.Text.Contains(ini.ReadString("money", "save", "")) || searchword.Text.Contains(ini.ReadString("money", "waste", "")))
                        //        {
                        //            showMoneyLeft(ini.ReadString("money", "money", ""), "saveAccount");
                        //        }
                        //        else
                        //        {
                        //            showMoneyLeft(ini.ReadString("money", "money", ""), "balanceAccount");
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //    }
                        //    searchword.Text = "";
                        //}//取消赚钱
                        else if (searchword.Text.StartsWith("#"))
                        {
                            SearchFiles();
                            return;
                        }
                        //else if (searchword.Text.StartsWith("remind"))//直接用mindmaps好了
                        //{
                        //    DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                        //    string content = "";
                        //    foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
                        //    {
                        //        string filename = Path.GetFileNameWithoutExtension(file.FullName);
                        //        content += filename;
                        //        content += "|";
                        //        content += Tools.GetFirstSpell(filename);
                        //        content += "|";
                        //        content += Tools.ConvertToAllSpell(filename);
                        //        content += "|";
                        //        content += Tools.GetFirstSpell(filename);
                        //        content += "@";
                        //    }
                        //    Tools.RecordLog(content);
                        //    searchword.Text = "";
                        //    return;
                        //}
                        else if (searchword.Text.StartsWith("rss"))
                        {
                            //try
                            //{
                            //    string rss = searchword.Text.Substring(3);
                            //    if (rss == "" || !IsUri(rss))
                            //    {
                            //        return;
                            //    }

                            //    string path = ini.ReadString("path", "rss", "");
                            //    string domin = GetTopDomin(rss);
                            //    if (!System.IO.Directory.Exists(path + "\\" + domin))
                            //    {
                            //        System.IO.Directory.CreateDirectory(path + "\\" + domin);
                            //    }
                            //    WebClient webClient = new WebClient();
                            //    webClient.Headers.Add("user-agent", "MyRSSReader/1.0");
                            //    XmlReader readers = XmlReader.Create(webClient.OpenRead(rss));
                            //    XmlDocument doc = new XmlDocument(); // 创建文档对象
                            //    try
                            //    {
                            //        doc.Load(readers);//加载XML 包括HTTP：// 和本地
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        MessageBox.Show(ex.Message);//异常处理
                            //    }
                            //    string titleStr = doc.GetElementsByTagName("title")[0].InnerText;
                            //    titleStr = Regex.Replace(titleStr, @"[\""]+", "");
                            //    titleStr = titleStr.Replace(" ", "");
                            //    titleStr = Regex.Replace(titleStr, @"((?=[\x21-\x7e]+)[^A-Za-z0-9])", "");
                            //    string demoPath = ini.ReadString("path", "rssdemo", "");
                            //    string fileName = path + "\\" + domin + "\\" + titleStr + ".mm";// getTitle(rss);
                            //    if (!File.Exists(fileName))//&&//将所有RSS记录一下，如果包含就不操作了。
                            //    {
                            //        System.IO.File.Copy(demoPath, fileName);
                            //        TextContentReplace(fileName, "####URL####", rss);
                            //    }
                            //    XmlNodeList list = doc.GetElementsByTagName("item");  // 获得项           
                            //    System.Xml.XmlDocument x = new XmlDocument();
                            //    x.Load(fileName);
                            //    foreach (XmlNode node in list)  // 循环每一项
                            //    {
                            //        XmlElement ele = (XmlElement)node;
                            //        string title = ele.GetElementsByTagName("title")[0].InnerText;//获得标题
                            //        string link = ele.GetElementsByTagName("link")[0].InnerText;//获得联接
                            //        string description = ele.GetElementsByTagName("description")[0].InnerText;//获得联接
                            //        string guidurl = ele.GetElementsByTagName("guid").Count == 0 ? "" : ele.GetElementsByTagName("guid")[0].InnerText;//获得联接
                            //        DateTime dt = DateTime.Now;
                            //        try
                            //        {
                            //            dt = Convert.ToDateTime(((System.Xml.XmlElement)ele.PreviousSibling).InnerText);
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //        }
                            //        //添加到列表内
                            //        ListViewItem item = new ListViewItem
                            //        {
                            //            Text = title,
                            //            Tag = link
                            //        };
                            //        AddTaskToFile(x, "文章", title, link, description, guidurl, dt);
                            //    }
                            //    x.Save(fileName);
                            //    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(fileName));
                            //    th.Start();
                            //    searchword.Text = "";
                            //}
                            //catch (Exception ex)
                            //{
                            //}
                        }
                        else if (searchword.Text.StartsWith("t"))
                        {
                            try
                            {
                                string taskname = searchword.Text.Substring(1);
                                MatchCollection mc = Regex.Matches(taskname, @"[1-9]\d*m");
                                int tasktime = 5;
                                foreach (Match m in mc)
                                {
                                    taskname = taskname.Replace(m.Value, "");
                                    tasktime = Convert.ToInt32(m.Value.Substring(0, m.Value.Length - 1));
                                    break;
                                }
                                Thread th = new Thread(() => OpenFanQie(tasktime, taskname, System.AppDomain.CurrentDomain.BaseDirectory, GetPosition(), false, 2));
                                tomatoCount += 1;
                                th.Start();
                                searchword.Text = "";
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else if (searchword.Text.StartsWith("T") || searchword.Text.EndsWith("TT"))
                        {
                            try
                            {
                                string taskname = "";
                                if (searchword.Text.StartsWith("T"))
                                {
                                    taskname = searchword.Text.Substring(1);
                                }
                                else if (searchword.Text.EndsWith("TT"))
                                {
                                    taskname = searchword.Text.Substring(0, searchword.Text.Length - 2);
                                }
                                if (taskname.Contains("@"))
                                {
                                    string timeblockname = taskname.Split('@')[1];
                                    taskname = taskname.Split('@')[0];
                                    Thread th = new Thread(() => OpenFanQie(0, taskname, System.AppDomain.CurrentDomain.BaseDirectory, GetPosition(), true, 2, timeblockname, timeblockfather, timeblockcolor));
                                    th.Start();
                                }
                                else
                                {
                                    Thread th = new Thread(() => OpenFanQie(0, taskname, System.AppDomain.CurrentDomain.BaseDirectory, GetPosition(), true, 2));
                                    th.Start();
                                }
                                tomatoCount += 1;
                                searchword.Text = "";
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else if (searchword.Text.StartsWith("刚刚") || searchword.Text.EndsWith("刚刚") || searchword.Text.Contains("刚刚@"))
                        {
                            string task = searchword.Text.Replace("刚刚", "").Split('@')[0];
                            string taskDetail = "";
                            DateTime dt = DateTime.Now;
                            if (task.Contains("|"))
                            {
                                taskDetail = task.Split('|')[1];
                                task = task.Split('|')[0];
                            }
                            string timeblockname = searchword.Text.Split('@')[1];
                            double tasktime = 0;
                            try
                            {
                                MatchCollection mc = Regex.Matches(task, @"[1-9]\d*分钟");
                                string minutes = "0";
                                foreach (Match m in mc)
                                {
                                    task = task.Replace(m.Value, "");
                                    minutes = m.Value.Substring(0, m.Value.Length - 2);
                                    tasktime = Convert.ToDouble(minutes);
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                            try
                            {
                                task = task.Replace("：", ":");
                                MatchCollection mc = Regex.Matches(task, @"\d\d:\d\d");
                                foreach (Match m in mc)
                                {
                                    task = task.Replace(m.Value, "");
                                    dt = DateTime.Today.AddHours(Convert.ToInt16(m.Value.Split(':')[0])).AddMinutes(Convert.ToInt16(m.Value.Split(':')[0]));
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                            if (searchword.Text.Contains("@"))
                            {
                                CalendarForm.reminderObjectJsonAdd(timeblockname, Guid.NewGuid().ToString(), timeblockcolor, 0, dt, "TimeBlock", timeblockfather.Replace('>', '|'), task, taskDetail, tasktime);
                            }
                            else
                            {
                                CalendarForm.reminderObjectJsonAdd(task, Guid.NewGuid().ToString(), Color.GreenYellow.ToArgb().ToString(), 0, dt, "FanQie", "", "", taskDetail, tasktime);
                            }
                            searchword.Text = "";
                            if (showTimeBlock.Checked)//若是是时间块模式，可以直接刷新
                            {
                                RRReminderlist();
                                reminderList.Focus();
                            }
                            SetTimeBlockLasTime();
                        }
                        else if (searchword.Text.StartsWith("@@"))//这个是干嘛的？没有看懂,放着吧，应该是避免所选节点为空
                        {
                            searchword.Text = "";
                            ////显示当前导图的所有任务
                            //RRReminderlist();
                            //reminderList.Focus();
                            //if (reminderList.Items.Count > 0)
                            //{
                            //    ReminderListSelectedIndex( 0;
                            //}
                            //只显示当前导图的任务
                            FileInfo file = new FileInfo(showMindmapName);
                            try
                            {
                                reminderList.Items.Clear();
                                XmlDocument x = new XmlDocument();
                                try
                                {
                                    x.Load(file.FullName);
                                }
                                catch (Exception ex)
                                {
                                    return;
                                }
                                if (x.GetElementsByTagName("hook").Count != 0)
                                {
                                    string str1 = "hook";
                                    string str2 = "NAME";
                                    string str3 = "plugins/TimeManagementReminder.xml";
                                    foreach (XmlNode node in x.GetElementsByTagName(str1))
                                    {
                                        try
                                        {
                                            if (node.Attributes[str2].Value == str3)
                                            {
                                                string reminder = "";
                                                DateTime dt = DateTime.Now;
                                                if (node.InnerXml != "")
                                                {
                                                    reminder = node.InnerXml.Split('\"')[1];
                                                    long unixTimeStamp = Convert.ToInt64(reminder);
                                                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                                    dt = startTime.AddMilliseconds(unixTimeStamp);
                                                }
                                                else
                                                {
                                                    reminder = GetAttribute(node.ParentNode, "RememberTime");
                                                    if (reminder == "")
                                                    {
                                                    }
                                                    else
                                                    {
                                                        long unixTimeStamp = Convert.ToInt64(reminder);
                                                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                                                        dt = startTime.AddMilliseconds(unixTimeStamp);
                                                    }
                                                }
                                                //添加提醒到提醒清单
                                                string dakainfo = "";
                                                if (GetAttribute(node.ParentNode, "ISDAKA") == "true")
                                                {
                                                    dakainfo = " | " + GetAttribute(node.ParentNode, "DAKADAY");
                                                }
                                                string taskName1 = "";
                                                string taskNameDis = "";
                                                bool isEncrypted = false;
                                                taskName1 = node.ParentNode.Attributes["TEXT"].Value;
                                                if (taskName1.Length > 6)
                                                {
                                                    if (taskName1.Substring(0, 3) == "***")
                                                    {
                                                        if (PassWord != "")
                                                        {
                                                            taskName1 = encrypt.DecryptString(node.ParentNode.Attributes["TEXT"].Value);
                                                            isEncrypted = true;
                                                        }
                                                    }
                                                }
                                                taskNameDis = taskName1;
                                                if (IsFileUrl(taskName1))
                                                {
                                                    if (Path.GetExtension(taskName1) != "")
                                                    {
                                                        taskNameDis = "#" + Path.GetFileName(taskName1);
                                                    }
                                                    else
                                                    {
                                                        taskNameDis = "Path:" + Path.GetFullPath(taskName1).Split('\\').Last(m => m != "");
                                                    }
                                                }
                                                if (GetAttribute(node.ParentNode, "ISVIEW") == "true")
                                                {
                                                    taskNameDis = "待：" + taskNameDis;
                                                }
                                                if (taskName1.ToLower() != "bin")
                                                {
                                                    reminderList.Items.Add(new MyListBoxItemRemind
                                                    {
                                                        Text = dt.ToString("yy-MM-dd-HH:mm") + @"  " + taskNameDis + dakainfo,
                                                        Name = taskName1,
                                                        Time = dt,
                                                        Value = file.FullName,
                                                        IsShow = false,
                                                        remindertype = GetAttribute(node.ParentNode, "REMINDERTYPE"),
                                                        rhours = MyToInt16(GetAttribute(node.ParentNode, "RHOUR")),
                                                        rdays = MyToInt16(GetAttribute(node.ParentNode, "RDAYS")),
                                                        rMonth = MyToInt16(GetAttribute(node.ParentNode, "RMONTH")),
                                                        rWeek = MyToInt16(GetAttribute(node.ParentNode, "RWEEK")),
                                                        rweeks = GetAttribute(node.ParentNode, "RWEEKS").ToCharArray(),
                                                        ryear = MyToInt16(GetAttribute(node.ParentNode, "RYEAR")),
                                                        rtaskTime = MyToInt16(GetAttribute(node.ParentNode, "TASKTIME")),
                                                        IsDaka = GetAttribute(node.ParentNode, "ISDAKA"),
                                                        IsView = GetAttribute(node.ParentNode, "ISVIEW"),
                                                        DakaDay = MyToInt16(GetAttribute(node.ParentNode, "DAKADAY")),
                                                        level = MyToInt16(GetAttribute(node.ParentNode, "TASKLEVEL")),
                                                        ebstring = MyToInt16(GetAttribute(node.ParentNode, "EBSTRING")),
                                                        DakaDays = StrToInt(GetAttribute(node.ParentNode, "DAKADAYS").Split(',')),
                                                        editTime = 0,
                                                        isEncrypted = isEncrypted,
                                                        link = GetAttribute(node.ParentNode, "LINK"),
                                                        IDinXML = GetAttribute(node.ParentNode, "ID")
                                                    });
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                    reminderList.Sorted = false;
                                    reminderList.Sorted = true;
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                            //用树视图打开思维导图
                            reminderlistSelectedItem = new MyListBoxItemRemind() { Name = file.Name, Value = file.FullName, IDinXML = "" };
                            mindmapornode.Text = file.FullName;//进入查看子节点一样的逻辑，按w才能退出
                            ShowMindmap();
                            SelectTreeNode(nodetree.Nodes, renameMindMapFileID);
                            ShowMindmapFile();
                            showMindmapName = "";//为什么要清空呢先取消试试
                            renameMindMapFileID = "";
                            nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                            this.Height = maxheight;
                            nodetree.Focus();
                            searchword.Text = "";
                            return;
                        }
                        else
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                ReSetValue();
                                RRReminderlist();
                            }
                            else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                AddTask(true);
                            }
                            else
                            {
                                AddTask(false);
                            }
                        }
                    }
                    else if (nodetree.Focused)
                    {
                        if (IsMindMapNodeEdit)
                        {
                            IsMindMapNodeEdit = false;
                            return;
                        }
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            //将树节点设置成任务
                            if (nodetree.SelectedNode.Name != null)
                            {
                                try
                                {
                                    if (SetTaskNodeByID(nodetree.SelectedNode.Name))
                                    {
                                        nodetree.SelectedNode.Text = DateTime.Now.ToString("MMddHH ") + nodetree.SelectedNode.Text;
                                        SaveLog("设置节点为任务：" + nodetree.SelectedNode.Text + "    导图" + showMindmapName.Split('\\')[showMindmapName.Split('\\').Length - 1]);
                                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                                        th.Start();
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                        else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (nodetree.SelectedNode != null && !nodetree.SelectedNode.IsEditing)
                            {
                                TreeNode node;
                                if (nodetree.SelectedNode.Parent != null)
                                {
                                    node = nodetree.SelectedNode.Parent.Nodes.Add("");
                                }
                                else
                                {
                                    node = nodetree.Nodes.Add("");
                                }
                                nodetree.SelectedNode = node;
                                node.BeginEdit();
                                IsMindMapNodeEdit = true;
                            }
                        }
                        else
                        {
                            if (nodetree.SelectedNode.ForeColor == Color.DeepSkyBlue)
                            {
                                try
                                {
                                    string link = GetAttribute(((XmlNode)nodetree.SelectedNode.Tag), "LINK");
                                    Clipboard.SetText(link);
                                    if (IsURL(link))
                                    {
                                        Process.Start(link);
                                    }
                                    else if (IsFileUrl(link))
                                    {
                                        Process.Start(getFileUrlPath(link));
                                    }
                                    else if (link.StartsWith("."))
                                    {
                                        FileInfo file = new FileInfo(((XmlNode)nodetree.SelectedNode.Tag).BaseURI.Substring(8));
                                        string mindmapfolderPath = file.Directory.FullName;
                                        link = mindmapfolderPath + "\\" + link;
                                        link = link.Replace("/", "\\");
                                        link = link.Replace(@"\\", @"\");
                                        Process.Start(getFileUrlPath(link));
                                    }
                                    MyHide();
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            else
                            {
                                //暂时不将树视图放到最大了
                                if (nodetree.Top != nodetreeTopTop)
                                {
                                    nodetree.Top = FileTreeView.Top = nodetreeTopTop;
                                    nodetree.Height = FileTreeView.Height = nodeTreeHeightMax;
                                }
                                else
                                {
                                    nodetree.Top = FileTreeView.Top = nodetreeTop;
                                    nodetree.Height = FileTreeView.Height = nodetreeHeight;
                                }
                            }
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            if (nodetree.Top != 9)
                            {
                                nodetree.Top = FileTreeView.Top = nodetreeTopTop;
                                nodetree.Height = FileTreeView.Height = nodeTreeHeightMax;
                            }
                            else
                            {
                                nodetree.Top = FileTreeView.Top = nodetreeTop;
                                nodetree.Height = FileTreeView.Height = nodetreeHeight;
                            }
                            FileTreeView.Focus();
                        }
                        else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (IsFileNodeEdit)
                            {
                                IsFileNodeEdit = false;
                                return;
                            }
                            if (FileTreeView.SelectedNode != null && !FileTreeView.SelectedNode.IsEditing)
                            {
                                TreeNode node;
                                if (FileTreeView.SelectedNode.Parent != null)
                                {
                                    node = FileTreeView.SelectedNode.Parent.Nodes.Add("");
                                }
                                else
                                {
                                    node = FileTreeView.Nodes.Add("");
                                }
                                FileTreeView.SelectedNode = node;
                                node.BeginEdit();
                                IsFileNodeEdit = true;
                            }
                        }
                        else
                        {
                            if (IsFileNodeEdit)
                            {
                                IsFileNodeEdit = false;
                                return;
                            }
                            try
                            {
                                System.Diagnostics.Process.Start(FileTreeView.SelectedNode.Name);
                                SaveLog("打开：    " + FileTreeView.SelectedNode.Name);
                                MyHide();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else if (e.Modifiers.CompareTo(Keys.Shift) == 0 && dateTimePicker.Focused)
                    {
                        edittime_EndDate();
                    }
                    else if ((int)Control.ModifierKeys == (int)Keys.Shift + (int)Keys.Alt && dateTimePicker.Focused)
                    {
                        edittime_EndDate(false);
                    }
                    else if (e.Modifiers.CompareTo(Keys.Control) == 0 && dateTimePicker.Focused)
                    {
                        setjinianfunction();
                    }
                    else if ((int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt && dateTimePicker.Focused)
                    {
                        setjinianfunction(false);
                    }
                    else if (e.Modifiers.CompareTo(Keys.Control) == 0 && taskTime.Focused)
                    {
                        Daka();
                    }
                    else if (e.Modifiers.CompareTo(Keys.Shift) == 0 && taskTime.Focused)
                    {
                        Daka(false);
                    }
                    else if (dateTimePicker.Focused || taskTime.Focused || tasklevel.Focused)
                    {
                        if (dateTimePicker.Focused && showTimeBlock.Checked)
                        {
                            reminderSelectIndex = reminderList.SelectedIndex;
                            reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).time = dateTimePicker.Value;
                            RRReminderlist();
                            if (reminderList.Items.Count > reminderSelectIndex)
                            {
                                ReminderListSelectedIndex(reminderSelectIndex);
                            }
                        }
                        else if (taskTime.Focused && showTimeBlock.Checked)
                        {
                            reminderSelectIndex = reminderList.SelectedIndex;
                            reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).tasktime = (int)taskTime.Value;
                            RRReminderlist();
                            if (reminderList.Items.Count > reminderSelectIndex)
                            {
                                ReminderListSelectedIndex(reminderSelectIndex);
                            }
                        }
                        else if (tasklevel.Focused && showTimeBlock.Checked)
                        {
                            reminderSelectIndex = reminderList.SelectedIndex;
                            reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).tasklevel = (int)tasklevel.Value;
                            RRReminderlist();
                            if (reminderList.Items.Count > reminderSelectIndex)
                            {
                                ReminderListSelectedIndex(reminderSelectIndex);
                            }
                        }
                        else
                        {
                            EditTime_Clic(null, null);
                        }
                    }
                    else if (n_days.Focused)
                    {
                        button_cycle_Click(null, null);
                    }
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Escape:
                    if (!keyNotWork(e))
                    {
                        #region 将一些值清空
                        isRenameTimeBlock = false;
                        #endregion
                        if (searchword.Focused && c_ViewModel.Checked)
                        {
                            mindmaplist.Focus();
                        }
                        else if (dateTimePicker.Focused)
                        {
                            reminderList.Focus();
                        }
                        else if (TimeBlockDate.Focused)
                        {
                            reminderList.Focus();
                        }
                        else if (noterichTextBox.Focused)
                        {
                            nodetree.Focus();
                        }
                        else if (nodetreeSearch.Focused)
                        {
                            nodetree.Focus();
                        }
                        else
                        {
                            if (searchword.Text.Contains("@"))//如果包含@
                            {
                                SearchText_suggest.Visible = false;
                                if (searchword.Text.Contains("@@"))
                                {

                                }
                                else
                                {
                                    mindmapornode.Text = searchword.Text.Split('@')[1];
                                }
                                searchword.Text = "";
                                //显示当前导图的所有任务
                                RRReminderlist();
                                reminderList.Focus();
                                if (reminderList.Items.Count > 0)
                                {
                                    ReminderListSelectedIndex(0);
                                }
                            }
                            else if (searchword.Text.StartsWith("#"))
                            {
                                searchword.Text = "";
                                reminderList.Focus();
                                RRReminderlist();
                                ReminderListSelectedIndex(reminderSelectIndex);
                            }
                            else
                            {
                                searchword.Text = "";
                                reminderList.Focus();
                                //解决任务栏会闪动的问题，其实没有必要刷新
                                //RRReminderlist();
                                //ReminderListSelectedIndex(reminderSelectIndex);
                            }
                        }
                    }
                    break;
                case Keys.Execute:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.F:
                    if (keyNotWork(e))
                    {
                        if (ReminderListFocused())
                        {
                            OpenFanqie();
                            MyHide();
                        }
                        else if (nodetree.Focused)
                        {
                            FileTreeView.Focus();
                        }
                    }
                    break;
                case Keys.F1:
                    if (keyNotWork(e))
                    {
                        try
                        {
                            MyHide();
                            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"\DocearReminder.chm");
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    break;
                case Keys.F10:
                    Jietu();
                    break;
                case Keys.F11:
                    if (e.Modifiers.CompareTo(Keys.Control) == 0)
                    {
                        Application.Restart();
                        Process.GetCurrentProcess()?.Kill();
                    }
                    else
                    {
                        //DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                        //foreach (FileInfo file in path.GetFiles("~*.mm", SearchOption.AllDirectories))
                        //{
                        //    file.Delete();
                        //}
                        Application.Exit();
                    }
                    break;
                case Keys.F12:
                    if (keyNotWork(e))
                    {
                        showlog();
                    }
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
                case Keys.F2:
                    if (keyNotWork(e) && !FileTreeView.Focused && !nodetree.Focused)
                    {
                        isRename = true;
                        reminderSelectIndex = reminderList.SelectedIndex;
                        searchword.Text = ((MyListBoxItemRemind)reminderlistSelectedItem).Name;
                        renameTaskName = ((MyListBoxItemRemind)reminderlistSelectedItem).Name;
                        showMindmapName = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                        if (mindmapornode.Text.Contains(">"))
                        {
                            renameMindMapFileIDParent = renameMindMapFileID;
                        }
                        renameMindMapFileID = ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML;
                        searchword.Focus();
                    }
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
                case Keys.F3:
                    if (keyNotWork(e))
                    {
                        //if (moshiview.Checked)
                        //{
                        //    setUnCheck();
                        //    fenlei_pc.Checked = true;
                        //}
                        //else
                        //{
                        //    fenlei_pc.Checked = !fenlei_pc.Checked;
                        //}
                    }
                    break;
                case Keys.F4:
                    if (keyNotWork(e))
                    {
                        //if (moshiview.Checked)
                        //{
                        //    setUnCheck();
                        //    fenlei_learn.Checked = true;
                        //}
                        //else
                        //{
                        //    fenlei_learn.Checked = !fenlei_learn.Checked;
                        //}
                    }
                    break;
                case Keys.F5:
                    SearchText_suggest.Visible = false;
                    RRReminderlist();
                    break;
                case Keys.F6:
                    if (keyNotWork(e))
                    {
                        //if (moshiview.Checked)
                        //{
                        //    setUnCheck();
                        //    fenlei_todo.Checked = true;
                        //}
                        //else
                        //{
                        //    fenlei_todo.Checked = !fenlei_todo.Checked;
                        //}
                        MyHide();
                        OpenSearch();
                    }
                    break;
                case Keys.F7:
                    if (keyNotWork(e))
                    {
                        //if (moshiview.Checked)
                        //{
                        //    setUnCheck();
                        //    fenlei_keepme.Checked = true;
                        //}
                        //else
                        //{
                        //    fenlei_keepme.Checked = !fenlei_keepme.Checked;
                        //}
                        MyHide();
                        Btn_OpenFolder_Click();
                    }
                    break;
                case Keys.F8:
                    if (keyNotWork(e))
                    {
                        //if (moshiview.Checked)
                        //{
                        //    setUnCheck();
                        //    fenlei_rest.Checked = true;
                        //}
                        //else
                        //{
                        //    fenlei_rest.Checked = !fenlei_rest.Checked;
                        //}
                        MyHide();
                        btn_OpenFile_MouseClick();
                    }
                    break;
                case Keys.F9:
                    if (keyNotWork(e))
                    {
                        //if (moshiview.Checked)
                        //{
                        //    setUnCheck();
                        //    fenlei_wonderfull.Checked = true;
                        //}
                        //else
                        //{
                        //    fenlei_wonderfull.Checked = !fenlei_wonderfull.Checked;
                        //}
                        MyHide();
                        Tools menu = new Tools();
                        menu.ShowDialog();
                    }
                    break;
                case Keys.FinalMode:
                    break;
                case Keys.G:
                    leftIndex = 0;
                    if (keyNotWork(e))
                    {
                        if (ReminderListFocused())
                        {
                            OpenFanqie(true);
                            MyHide();
                        }
                        else if (FileTreeView.Focused)
                        {
                            nodetree.Focus();
                        }
                    }
                    break;
                case Keys.H:
                    leftIndex = 0;
                    if (nodetree.Focused)
                    {
                        try
                        {
                            if (nodetree.SelectedNode.Parent != null)
                            {
                                nodetree.SelectedNode.Parent.Collapse();
                                //treeView1.SelectedNode = treeView1.SelectedNode.Parent;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        try
                        {
                            if (FileTreeView.SelectedNode.Parent != null)
                            {
                                FileTreeView.SelectedNode.Parent.Collapse();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (ReminderListFocused() || mindmaplist.Focused || this.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (showTimeBlock.Checked)
                            {
                                TimeBlockDate.Value = TimeBlockDate.Value.AddDays(-1);
                                RRReminderlist();
                                reminderList.Focus();
                                if (reminderList.Items.Count > 0)
                                {
                                    ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                }
                            }
                            else if (ShowMoney.Checked)
                            {
                                MoneyDateTimePicker.Value = MoneyDateTimePicker.Value.AddDays(-1);
                                RRReminderlist();
                                reminderList.Focus();
                                if (reminderList.Items.Count > 0)
                                {
                                    ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                }
                            }
                            else if (ShowKA.Checked)
                            {
                                KADateTimePicker.Value = KADateTimePicker.Value.AddDays(-1);
                                RRReminderlist();
                                reminderList.Focus();
                                if (reminderList.Items.Count > 0)
                                {
                                    ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                }
                            }
                            else
                            {
                                selectedpath = true;
                                if (PathcomboBox.SelectedIndex > 0)
                                {
                                    PathcomboBox.SelectedIndex--;
                                }
                                else
                                {
                                    PathcomboBox.SelectedIndex = PathcomboBox.Items.Count - 1;
                                }
                                PathcomboBox_SelectedIndexChanged(null, null);
                            }
                        }
                        else if (ReminderListFocused())
                        {
                            try
                            {
                                if (quietmode.Checked)
                                {
                                    Thread th = new Thread(() => yixiaozi.Media.Audio.Audio.SpeakText(((MyListBoxItemRemind)reminderlistSelectedItem).Name));
                                    th.Start();
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    break;
                case Keys.HanguelMode:
                    break;
                case Keys.HanjaMode:
                    break;
                case Keys.Help:
                    break;
                case Keys.Home:
                    break;
                case Keys.I:
                    if (keyNotWork(e))
                    {
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            if (showTimeBlock.Checked)
                            {
                                isRenameTimeBlock = true;
                                reminderSelectIndex = reminderList.SelectedIndex;
                                searchword.Text = ((MyListBoxItemRemind)reminderlistSelectedItem).IsDaka;
                                renameTaskName = ((MyListBoxItemRemind)reminderlistSelectedItem).IsDaka;
                                showMindmapName = ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML;
                                searchword.Focus();
                            }
                            else
                            {
                                isRename = true;
                                reminderSelectIndex = reminderList.SelectedIndex;
                                searchword.Text = ((MyListBoxItemRemind)reminderlistSelectedItem).Name;
                                renameTaskName = ((MyListBoxItemRemind)reminderlistSelectedItem).Name;
                                showMindmapName = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                                if (mindmapornode.Text.Contains(">"))
                                {
                                    renameMindMapFileIDParent = renameMindMapFileID;
                                }
                                renameMindMapFileID = ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML;
                                searchword.Focus();
                            }
                        }
                        else
                        {

                            isRename = false;
                            reminderSelectIndex = reminderList.SelectedIndex;
                            searchword.Focus();
                        }
                    }
                    break;
                case Keys.IMEAccept:
                    break;
                case Keys.IMEConvert:
                    break;
                case Keys.IMEModeChange:
                    break;
                case Keys.IMENonconvert:
                    break;
                case Keys.Insert:
                    if (nodetree.Focused)
                    {
                        if (nodetree.SelectedNode != null && !nodetree.SelectedNode.IsEditing)
                        {
                            TreeNode node = nodetree.SelectedNode.Nodes.Add("");
                            nodetree.SelectedNode.ExpandAll();
                            nodetree.SelectedNode = node;
                            node.BeginEdit();
                            IsMindMapNodeEdit = true;
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        if (FileTreeView.SelectedNode != null && !FileTreeView.SelectedNode.IsEditing)
                        {
                            TreeNode node = FileTreeView.SelectedNode.Nodes.Add("");
                            FileTreeView.SelectedNode.ExpandAll();
                            FileTreeView.SelectedNode = node;
                            node.BeginEdit();
                            IsFileNodeEdit = true;
                        }
                    }
                    break;
                case Keys.J:
                    leftIndex = 0;
                    if (keyNotWork(e))
                    {
                        if ((ReminderListFocused() && (reminderList.Items.Count != 0) || reminderListBox.Items.Count != 0))
                        {
                            if (c_ViewModel.Checked && (int)Control.ModifierKeys == (int)Keys.Shift + (int)Keys.Control)//查看模式快速切换导图
                            {
                                if (mindmaplist.SelectedIndex + 1 < mindmaplist.Items.Count)
                                {
                                    mindmaplist.SelectedIndex++;
                                }
                                else
                                {
                                    mindmaplist.SelectedIndex = 0;
                                }
                                RRReminderlist();
                            }
                            else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                if (showTimeBlock.Checked)
                                {
                                    if (tasklevel.Value < 100)
                                    {
                                        tasklevel.Value += 1;
                                        reminderSelectIndex = reminderList.SelectedIndex;
                                        reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).tasklevel = (int)tasklevel.Value;
                                        if (!OnlyLevel.Checked)
                                        {
                                            RRReminderlist();
                                        }

                                        ReminderListSelectedIndex(reminderSelectIndex);
                                    }
                                }
                                else
                                {
                                    if (tasklevel.Value < 100)
                                    {
                                        tasklevel.Value += 1;
                                        EditTime_Clic(null, null);
                                    }
                                }
                            }
                            else if (e.Modifiers.CompareTo(Keys.Alt) == 0)
                            {
                                if (showTimeBlock.Checked)
                                {
                                    if (taskTime.Value <= 240)
                                    {
                                        taskTime.Value += 1;
                                        reminderSelectIndex = reminderList.SelectedIndex;
                                        reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).tasktime = (int)taskTime.Value;
                                        if (!OnlyLevel.Checked)
                                        {
                                            RRReminderlist();
                                        }
                                        ReminderListSelectedIndex(reminderSelectIndex);
                                    }
                                }
                                else
                                {
                                    if (taskTime.Value < 700)
                                    {
                                        taskTime.Value += 10;
                                        EditTime_Clic(null, null);
                                    }
                                }
                            }
                            else if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                if (showTimeBlock.Checked)
                                {
                                    if (TimeBlockDate.Value != null)
                                    {
                                        TimeBlockDate.Value = TimeBlockDate.Value.AddDays(1);
                                        RRReminderlist();
                                    }
                                }
                                else
                                {
                                    if (dateTimePicker.Value != null)
                                    {
                                        dateTimePicker.Value = dateTimePicker.Value.AddHours(1);
                                        EditTime_Clic(null, null);
                                    }
                                }
                            }
                            else
                            {
                                if (reminderList.Focused)
                                {
                                    if (reminderList.SelectedIndex + 1 < reminderList.Items.Count)
                                    {
                                        reminderList.SelectedIndex++;
                                    }
                                    else
                                    {
                                        if (reminderListBox.Items.Count > 0)
                                        {
                                            reminderListBox.Focus();
                                            reminderListBox.SelectedIndex = 0;
                                            ReminderListSelectedIndex(-1);
                                        }
                                        else
                                        {
                                            reminderList.Focus();
                                            ReminderListSelectedIndex(0);
                                            reminderListBox.SelectedIndex = -1;
                                            reminderListBox.Refresh();
                                        }
                                    }
                                    reminderList.Refresh();
                                }
                                else if (reminderListBox.Focused)
                                {
                                    if (reminderListBox.SelectedIndex + 1 < reminderListBox.Items.Count)
                                    {
                                        reminderListBox.SelectedIndex++;
                                    }
                                    else
                                    {
                                        if (reminderList.Items.Count > 0)
                                        {
                                            reminderList.Focus();
                                            ReminderListSelectedIndex(0);
                                            reminderListBox.SelectedIndex = -1;
                                            reminderList.Refresh();
                                        }
                                        else
                                        {
                                            reminderListBox.Focus();
                                            reminderListBox.SelectedIndex = 0;
                                            ReminderListSelectedIndex(-1);
                                        }
                                    }
                                    reminderListBox.Refresh();
                                }
                            }
                        }
                        else if (mindmaplist.Focused && mindmaplist.Items.Count != 0)
                        {
                            if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                for (int i = mindmaplist.SelectedIndex + 1; i < mindmaplist.Items.Count; i++)
                                {
                                    if (mindmaplist.GetItemCheckState(i) == CheckState.Checked)
                                    {
                                        mindmaplist.SelectedIndex = i;
                                        mindmaplist.Refresh();
                                        return;
                                    }
                                }
                                for (int i = 0; i < mindmaplist.SelectedIndex; i++)
                                {
                                    if (mindmaplist.GetItemCheckState(i) == CheckState.Checked)
                                    {
                                        mindmaplist.SelectedIndex = i;
                                        mindmaplist.Refresh();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (mindmaplist.SelectedIndex + 1 < mindmaplist.Items.Count)
                                {
                                    mindmaplist.SelectedIndex++;
                                }
                                else
                                {
                                    mindmaplist.SelectedIndex = 0;
                                }
                                mindmaplist.Refresh();
                            }
                        }
                    }
                    if (taskTime.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {

                        }
                        else
                        {
                            if (taskTime.Value < 5)
                            {
                                taskTime.Value += 1;
                            }
                            else if (taskTime.Value <= 718)
                            {
                                taskTime.Value += 5;
                            }
                        }
                    }
                    else if (tasklevel.Focused)
                    {
                        tasklevel.Value += 1;
                    }
                    else if (n_days.Focused)
                    {
                        n_days.Value += 1;
                    }
                    else if (dateTimePicker.Focused)
                    {
                        dateTimePicker.Value = dateTimePicker.Value.AddHours(1);
                    }
                    else if (nodetree.Focused)
                    {
                        try
                        {
                            if (nodetree.SelectedNode.NextNode != null)
                            {
                                nodetree.SelectedNode = nodetree.SelectedNode.NextNode;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        try
                        {
                            if (FileTreeView.SelectedNode.NextNode != null)
                            {
                                FileTreeView.SelectedNode = FileTreeView.SelectedNode.NextNode;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    break;
                case Keys.JunjaMode:
                    break;
                case Keys.K:
                    leftIndex = 0;
                    if (keyNotWork(e))
                    {
                        if (ReminderListFocused())
                        {
                            if (c_ViewModel.Checked && (int)Control.ModifierKeys == (int)Keys.Shift + (int)Keys.Control)//查看模式快速切换导图
                            {
                                if (mindmaplist.SelectedIndex > 0)
                                {
                                    mindmaplist.SelectedIndex--;
                                }
                                else
                                {
                                    mindmaplist.SelectedIndex = mindmaplist.Items.Count - 1;
                                }
                                RRReminderlist();
                            }
                            else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                if (showTimeBlock.Checked)
                                {
                                    if (tasklevel.Value >= -10)
                                    {
                                        tasklevel.Value -= 1;
                                        reminderSelectIndex = reminderList.SelectedIndex;
                                        reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).tasklevel = (int)tasklevel.Value;
                                        if (!OnlyLevel.Checked)
                                        {
                                            RRReminderlist();
                                        }
                                        ReminderListSelectedIndex(reminderSelectIndex);
                                    }
                                }
                                else
                                {
                                    if (tasklevel.Value >= -10)
                                    {
                                        tasklevel.Value -= 1;
                                        EditTime_Clic(null, null);
                                    }
                                }
                            }
                            else if (e.Modifiers.CompareTo(Keys.Alt) == 0)
                            {
                                if (showTimeBlock.Checked)
                                {
                                    if (taskTime.Value >= 1)
                                    {
                                        taskTime.Value -= 1;
                                        reminderSelectIndex = reminderList.SelectedIndex;
                                        reminderObject.reminders.First(m => m.ID == ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML).tasktime = (int)taskTime.Value;
                                        if (!OnlyLevel.Checked)
                                        {
                                            RRReminderlist();
                                        }
                                        ReminderListSelectedIndex(reminderSelectIndex);
                                    }
                                }
                                else
                                {
                                    if (taskTime.Value >= 10)
                                    {
                                        taskTime.Value -= 10;
                                        EditTime_Clic(null, null);
                                    }
                                }
                            }
                            else if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                if (showTimeBlock.Checked)
                                {
                                    if (TimeBlockDate.Value != null)
                                    {
                                        TimeBlockDate.Value = TimeBlockDate.Value.AddDays(-1);
                                        RRReminderlist();
                                    }
                                }
                                else
                                {
                                    if (dateTimePicker.Value != null)
                                    {
                                        dateTimePicker.Value = dateTimePicker.Value.AddHours(-1);
                                        EditTime_Clic(null, null);
                                    }
                                }
                            }
                            else
                            {
                                if (reminderList.Focused)
                                {
                                    if (reminderList.SelectedIndex > 0)
                                    {
                                        reminderList.SelectedIndex--;
                                    }
                                    else
                                    {
                                        if (reminderListBox.Items.Count > 0)
                                        {
                                            reminderListBox.Focus();
                                            reminderListBox.SelectedIndex = reminderListBox.Items.Count - 1;
                                            ReminderListSelectedIndex(-1);
                                            reminderListBox.Refresh();
                                        }
                                        else
                                        {
                                            reminderList.Focus();
                                            ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                            reminderListBox.SelectedIndex = -1;
                                        }
                                    }
                                    reminderList.Refresh();
                                }
                                else if (reminderListBox.Focused)
                                {
                                    if (reminderListBox.SelectedIndex > 0)
                                    {
                                        reminderListBox.SelectedIndex--;
                                    }
                                    else
                                    {
                                        if (reminderList.Items.Count > 0)
                                        {
                                            reminderList.Focus();
                                            ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                            reminderListBox.SelectedIndex = -1;
                                            reminderList.Refresh();
                                        }
                                        else
                                        {
                                            reminderListBox.Focus();
                                            reminderListBox.SelectedIndex = reminderListBox.Items.Count - 1;
                                            ReminderListSelectedIndex(-1);
                                        }
                                    }
                                    reminderListBox.Refresh();
                                }
                            }
                        }
                        else if (mindmaplist.Focused)
                        {
                            if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                for (int i = mindmaplist.SelectedIndex - 1; i >= 0; i--)
                                {
                                    if (mindmaplist.GetItemCheckState(i) == CheckState.Checked)
                                    {
                                        mindmaplist.SelectedIndex = i;
                                        mindmaplist.Refresh();
                                        return;
                                    }
                                }
                                for (int i = mindmaplist.Items.Count - 1; i > mindmaplist.SelectedIndex; i--)
                                {
                                    if (mindmaplist.GetItemCheckState(i) == CheckState.Checked)
                                    {
                                        mindmaplist.SelectedIndex = i;
                                        mindmaplist.Refresh();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (mindmaplist.SelectedIndex > 0)
                                {
                                    mindmaplist.SelectedIndex--;
                                }
                                else
                                {
                                    mindmaplist.SelectedIndex = mindmaplist.Items.Count - 1;
                                }
                            }
                        }
                    }
                    if (taskTime.Focused)
                    {
                        if (taskTime.Value > 5)
                        {
                            taskTime.Value -= 5;
                        }
                        else
                        {
                            if (taskTime.Value > 1)
                            {
                                taskTime.Value -= 1;
                            }
                        }
                    }
                    else if (tasklevel.Focused)
                    {
                        if (tasklevel.Value >= 0)
                        {
                            tasklevel.Value -= 1;
                        }
                    }
                    else if (n_days.Focused)
                    {
                        if (n_days.Value >= 1)
                        {
                            n_days.Value -= 1;
                        }
                    }

                    else if (dateTimePicker.Focused)
                    {
                        dateTimePicker.Value = dateTimePicker.Value.AddHours(-1);
                    }
                    else if (nodetree.Focused)
                    {
                        if (nodetree.SelectedNode.PrevNode != null)
                        {
                            nodetree.SelectedNode = nodetree.SelectedNode.PrevNode;
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        if (FileTreeView.SelectedNode.PrevNode != null)
                        {
                            FileTreeView.SelectedNode = FileTreeView.SelectedNode.PrevNode;
                        }
                    }
                    break;
                case Keys.KeyCode:
                    break;
                case Keys.L:
                    leftIndex = 0;
                    if (keyNotWork(e))
                    {
                        if (nodetree.Focused)
                        {
                            try
                            {
                                if (e.Modifiers.CompareTo(Keys.Alt) == 0)
                                {
                                    显示右侧ToolStripMenuItem_Click(null, null);
                                }
                                else
                                {
                                    if (nodetree.SelectedNode.Nodes != null && nodetree.SelectedNode.Nodes.Count != 0)
                                    {
                                        nodetree.SelectedNode = nodetree.SelectedNode.Nodes[0];
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else if (FileTreeView.Focused)
                        {
                            try
                            {
                                if (e.Modifiers.CompareTo(Keys.Alt) == 0)
                                {
                                    显示右侧ToolStripMenuItem_Click(null, null);
                                }
                                else
                                {
                                    if (FileTreeView.SelectedNode.Nodes != null && FileTreeView.SelectedNode.Nodes.Count != 0)
                                    {
                                        FileTreeView.SelectedNode = FileTreeView.SelectedNode.Nodes[0];
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else if (ReminderListFocused() || mindmaplist.Focused || this.Focused)
                        {
                            if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                if (showTimeBlock.Checked)
                                {
                                    TimeBlockDate.Value = TimeBlockDate.Value.AddDays(1);
                                    RRReminderlist();
                                    reminderList.Focus();
                                    if (reminderList.Items.Count > 0)
                                    {
                                        ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                    }
                                }
                                else if (ShowMoney.Checked)
                                {
                                    MoneyDateTimePicker.Value = MoneyDateTimePicker.Value.AddDays(1);
                                    RRReminderlist();
                                    reminderList.Focus();
                                    if (reminderList.Items.Count > 0)
                                    {
                                        ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                    }
                                }
                                else if (ShowKA.Checked)
                                {
                                    KADateTimePicker.Value = KADateTimePicker.Value.AddDays(1);
                                    RRReminderlist();
                                    reminderList.Focus();
                                    if (reminderList.Items.Count > 0)
                                    {
                                        ReminderListSelectedIndex(reminderList.Items.Count - 1);
                                    }
                                }
                                else
                                {
                                    selectedpath = true;
                                    if (PathcomboBox.SelectedIndex < PathcomboBox.Items.Count - 1)
                                    {
                                        PathcomboBox.SelectedIndex++;
                                    }
                                    else
                                    {
                                        PathcomboBox.SelectedIndex = 0;
                                    }
                                    PathcomboBox_SelectedIndexChanged(null, null);
                                }
                            }
                            else if (e.Modifiers.CompareTo(Keys.Alt) == 0)
                            {
                                显示右侧ToolStripMenuItem_Click(null, null);
                            }
                            else
                            {
                                tasklevel.Focus();
                            }
                        }
                    }
                    if (searchword.Text.StartsWith("deny"))
                    {
                        searchword.Text = "";
                        reminderList.Focus();
                    }
                    break;
                //case Keys.LButton:
                //break;
                case Keys.LControlKey:
                    break;
                case Keys.LMenu:
                    break;
                case Keys.LShiftKey:
                    break;
                case Keys.LWin:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.Left:
                    if (nodetree.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            if (nodetree.Nodes.IndexOf(nodetree.SelectedNode) < 0)//如果是根节点，禁止左移
                            {
                                LeftNodeByID(nodetree.SelectedNode.Name);
                                Extensions.MoveToFather(nodetree.SelectedNode);
                                fenshuADD(1);
                                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                                th.Start();
                            }
                        }
                        else if (nodetree.Nodes.IndexOf(nodetree.SelectedNode) >= 0)
                        {
                            nodetree.Refresh();
                            if (!nodetree.SelectedNode.IsExpanded)
                            {
                                if (leftIndex == 1)
                                {
                                    leftIndex = 0;
                                    FileTreeView.Focus();
                                }
                                else
                                {
                                    leftIndex = 1;
                                }
                            }
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            if (FileTreeView.Nodes.IndexOf(FileTreeView.SelectedNode) < 0)//如果是根节点，禁止左移
                            {
                                if (File.Exists(FileTreeView.SelectedNode.Name))
                                {
                                    //文件
                                    FileInfo fi = new FileInfo(FileTreeView.SelectedNode.Name);
                                    fi.MoveTo(fi.Directory.Parent.FullName + "\\" + fi.Name);
                                    FileTreeView.SelectedNode.Name = fi.Directory.Parent.FullName + "\\" + fi.Name;
                                }
                                else if (Directory.Exists(FileTreeView.SelectedNode.Name))
                                {
                                    //文件夹
                                    DirectoryInfo folder = new DirectoryInfo(FileTreeView.SelectedNode.Name);
                                    folder.MoveTo(folder.Parent.Parent.FullName + "\\" + folder.Name);
                                    FileTreeView.SelectedNode.Name = folder.Parent.Parent.FullName + "\\" + folder.Name;
                                }
                                Extensions.MoveToFather(FileTreeView.SelectedNode);
                                fenshuADD(1);
                            }
                        }
                    }
                    else if (ReminderListFocused())
                    {
                        mindmaplist.Focus();
                    }

                    break;
                case Keys.LineFeed:
                    break;
                case Keys.M:
                    if (keyNotWork(e))
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            ShowMoney.Checked = !ShowMoney.Checked;
                        }
                        else if (ReminderListFocused() || dateTimePicker.Focused || tasklevel.Focused)
                        {
                            taskTime.Focus();
                        }
                    }
                    break;
                case Keys.MButton:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.Menu:
                    break;
                case Keys.Modifiers:
                    break;
                case Keys.Multiply:
                    break;
                case Keys.N:
                    if (keyNotWork(e))
                    {
                        if (showTimeBlock.Checked || ShowMoney.Checked || ShowKA.Checked)
                        {
                            return;
                        }
                        PlaySimpleSound("treeview");
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            //下面窗口设置一下
                            nodetree.Top = FileTreeView.Top = nodetreeTop;
                            nodetree.Height = FileTreeView.Height = nodetreeHeight;
                            fathernode.Visible = false;
                            nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = false;
                            this.Height = normalheight; showMindmapName = "";
                            if (focusedList == 0)
                            {
                                reminderList.Focus();
                            }
                            else
                            {
                                reminderListBox.Focus();
                            }
                            Center();
                            //刷新一下任务
                            if (mindmapornode.Text != "")
                            {
                                mindmapornode.Text = "";
                                tasklevel.Value = 0;
                                taskTime.Value = 0;
                                RRReminderlist();
                            }
                        }
                        else if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            string mindmapPath = "";
                            if (ReminderListFocused())
                            {
                                if (reminderList.SelectedIndex < 0 && reminderListBox.SelectedIndex < 0)
                                {
                                    return;
                                }
                                if (((MyListBoxItemRemind)reminderlistSelectedItem).Name == "当前时间")
                                {
                                    return;
                                }
                                mindmapPath = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                                //如果是文件的话，就打开文件所在的目录
                                string link = ((MyListBoxItemRemind)reminderlistSelectedItem).link;
                                if (link != null && link != "")
                                {
                                    if (File.Exists(link))
                                    {
                                        mindmapPath = link;
                                    }
                                }
                            }
                            else if (mindmaplist.Focused)
                            {
                                mindmapPath = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
                            }
                            if (mindmapPath == "")
                            {
                                return;
                            }
                            System.Diagnostics.Process.Start(new System.IO.FileInfo(mindmapPath).Directory.FullName);
                            MyHide();
                        }
                        else if (e.Modifiers.CompareTo(Keys.Alt) == 0)
                        {
                            if (noterichTextBox.Focused)
                            {
                                nodetree.Top = FileTreeView.Top = nodetreeTop;
                                nodetree.Height = FileTreeView.Height = nodetreeHeight;
                                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = false;
                                this.Height = normalheight; showMindmapName = "";
                                reminderList.Focus();
                            }
                            else
                            {
                                this.Height = maxheight;
                                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                                noterichTextBox.Focus();
                            }
                        }
                        else
                        {
                            if (ReminderListFocused())
                            {
                                ShowMindmap();
                                ShowMindmapFile(false);
                                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                                this.Height = maxheight;
                                nodetree.Focus();
                            }
                            else if (mindmaplist.Focused)
                            {
                                ShowMindmap();
                                ShowMindmapFile(false);
                                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                                this.Height = maxheight;
                                nodetree.Focus();
                            }
                            else if (nodetree.Focused || FileTreeView.Focused)
                            {
                                //下面窗口设置一下
                                nodetree.Top = FileTreeView.Top = nodetreeTop;
                                nodetree.Height = FileTreeView.Height = nodetreeHeight;
                                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = false;
                                this.Height = normalheight; showMindmapName = "";
                                if (focusedList == 0)
                                {
                                    reminderList.Focus();
                                }
                                else
                                {
                                    reminderListBox.Focus();
                                }
                            }
                        }
                        Center();
                    }
                    break;
                case Keys.Next:
                    break;
                case Keys.NoName:
                    break;
                case Keys.None:
                    break;
                case Keys.NumLock:
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
                case Keys.O:
                    if (keyNotWork(e))
                    {
                        if (ReminderListFocused())
                        {
                            if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                do
                                {
                                    dateTimePicker.Value = dateTimePicker.Value.AddHours(1);
                                }
                                while (dateTimePicker.Value < DateTime.Now);
                                EditTime_Clic(null, null);
                            }
                            else if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                CanceSelectedlTask(false);
                            }
                            else
                            {
                                taskComplete_btn_Click(null, null);
                                PlaySimpleSound("Done");
                            }
                        }
                        else if (dateTimePicker.Focused)
                        {
                            dateTimePicker.Value = dateTimePicker.Value.AddDays(1);
                        }
                    }
                    break;
                case Keys.Oem1:
                    break;
                case Keys.Oem102:
                    break;
                case Keys.Oem2:
                    break;
                case Keys.Oem3:
                    if (keyNotWork(e))
                    {
                        showcyclereminder.Checked = !showcyclereminder.Checked;
                    }
                    break;
                case Keys.Oem6:
                    if (ReminderListFocused())
                    {
                        n_days.Focus();
                    }
                    break;
                case Keys.Oem5:
                    break;
                case Keys.Oem4:
                    if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                    {
                        c_day.Checked = c_week.Checked = c_hour.Checked = c_month.Checked = c_year.Checked = false;
                        button_cycle_Click(null, null);
                    }
                    else
                    {
                        isSettingSyncWeek = false;
                        if (c_day.Checked == c_week.Checked == c_hour.Checked == c_month.Checked == c_year.Checked == false)
                        {
                            c_day.Checked = true;
                        }
                        else if (c_day.Checked)
                        {
                            c_week.Checked = true;
                            isSettingSyncWeek = true;
                        }
                        else if (c_week.Checked)
                        {
                            c_month.Checked = true;
                        }
                        else if (c_month.Checked)
                        {
                            c_year.Checked = true;
                        }
                        else if (c_year.Checked)
                        {
                            c_hour.Checked = true;
                        }
                        else if (c_hour.Checked)
                        {
                            c_remember.Checked = true;
                        }
                        else if (c_remember.Checked)
                        {
                            c_day.Checked = true;
                        }
                    }
                    break;
                case Keys.Oem7:
                    break;
                case Keys.Oem8:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.OemMinus:
                    break;
                case Keys.OemPeriod:
                    break;
                case Keys.P:
                    if (keyNotWork(e))
                    {
                        if (ReminderListFocused())
                        {
                            if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                dateTimePicker.Value = dateTimePicker.Value.AddHours(-1);
                                EditTime_Clic(null, null);

                            }
                            else
                            {
                                DelaySelectedTask();
                            }
                        }
                        else if (dateTimePicker.Focused)
                        {
                            dateTimePicker.Value = dateTimePicker.Value.AddDays(-1);
                        }
                    }
                    break;
                case Keys.Pa1:
                    break;
                case Keys.Packet:
                    break;
                case Keys.PageUp:
                    int n = pathArr.IndexOf(rootpath.FullName);
                    if (e.Modifiers.CompareTo(Keys.Shift) == 0)//pagedown被占用
                    {
                        if (n == 0)
                        {
                            rootpath = new DirectoryInfo(pathArr[pathArr.Count - 1]);
                        }
                        else
                        {
                            rootpath = new DirectoryInfo(pathArr[n - 1]);
                        }
                    }
                    else
                    {
                        if (n + 1 >= pathArr.Count)
                        {
                            rootpath = new DirectoryInfo(pathArr[0]);
                        }
                        else
                        {
                            rootpath = new DirectoryInfo(pathArr[n + 1]);
                        }
                    }
                    mindmapPath = rootpath.FullName;
                    searchword.Text = "";
                    UsedLogRenew();
                    Load_Click(null, null);
                    break;
                case Keys.Pause:
                    break;
                case Keys.Play:
                    break;
                case Keys.Print:
                    break;
                case Keys.PrintScreen:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Q:
                    if (keyNotWork(e))
                    {
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            showcalander();
                        }
                        else if (e.Modifiers.CompareTo(Keys.Alt) == 0)
                        {
                            ebcheckBox.Checked = !ebcheckBox.Checked;
                        }
                        else if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            showcalander();
                        }
                        else
                        {
                            mindmapornode.Text = "";
                            showTimeBlock.Checked = !showTimeBlock.Checked;
                            reminderList.Refresh();
                        }
                    }
                    break;
                case Keys.R:
                    if (keyNotWork(e))
                    {
                        if (false)
                        {

                        }
                        else
                        {
                            if (searchword.Text.StartsWith("*"))
                            {
                                SearchNode();
                                //AddTask(false);
                            }
                            else
                            {
                                if (nodetree.Focused)
                                {
                                    reminderList.Focus();
                                    ShowMindmap();
                                    ShowMindmapFile();
                                    nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                                    this.Height = maxheight;
                                    nodetree.Focus();
                                }
                                else if (reminderListBox.Focused)
                                {
                                    reminderListBox.Sorted = false;
                                    reminderListBox.Sorted = true;
                                    reminderListBox.Refresh();
                                }
                                else
                                {
                                    ReSetValue();
                                    int reminderIndex = reminderList.SelectedIndex;
                                    RRReminderlist();
                                    try
                                    {
                                        if (reminderIndex > reminderList.Items.Count - 1)
                                        {
                                            ReminderListSelectedIndex(0);
                                        }
                                        else
                                        {
                                            ReminderListSelectedIndex(reminderIndex);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.RControlKey:
                    break;
                case Keys.RMenu:
                    break;
                case Keys.RShiftKey:

                //case Keys.RWin:
                //break;
                case Keys.Right:
                    leftIndex = 0;
                    if (nodetree.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            RightNodeByID(nodetree.SelectedNode.Name);
                            Extensions.MoveToChildren(nodetree.SelectedNode);
                            fenshuADD(1);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                            th.Start();
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Control) == 0)
                        {
                            if (FileTreeView.Nodes.IndexOf(FileTreeView.SelectedNode) == 0)//如果是根节点，禁止左移
                            {
                                int index = FileTreeView.SelectedNode.Parent.Nodes.IndexOf(FileTreeView.SelectedNode);
                                string dir = "";
                                if (index == 0)
                                {
                                    return;
                                }
                                for (int i = index - 1; i >= 0; i--)
                                {
                                    if (Directory.Exists(FileTreeView.SelectedNode.Parent.Nodes[i].Name))
                                    {
                                        dir = FileTreeView.SelectedNode.Parent.Nodes[i].Name;
                                    }
                                }
                                if (dir != "")
                                {
                                    if (File.Exists(FileTreeView.SelectedNode.Name))
                                    {
                                        //文件
                                        FileInfo fi = new FileInfo(FileTreeView.SelectedNode.Name);
                                        fi.MoveTo(dir + "\\" + fi.Name);
                                        FileTreeView.SelectedNode.Name = dir + "\\" + fi.Name;
                                    }
                                    else if (Directory.Exists(FileTreeView.SelectedNode.Name))
                                    {
                                        //文件夹
                                        DirectoryInfo folder = new DirectoryInfo(FileTreeView.SelectedNode.Name);
                                        folder.MoveTo(dir + "\\" + folder.Name);
                                        FileTreeView.SelectedNode.Name = dir + "\\" + folder.Name;
                                    }
                                }
                                Extensions.MoveToChildren(FileTreeView.SelectedNode);
                                fenshuADD(1);
                            }
                        }
                        else if (FileTreeView.SelectedNode.Name.EndsWith(".mm"))
                        {
                            leftIndex = 0;
                            nodetree.Focus();
                        }
                    }
                    else if (mindmaplist.Focused)
                    {
                        reminderList.Focus();
                        try
                        {
                            if (reminderList.SelectedIndex < 0)
                            {
                                ReminderListSelectedIndex(0);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    break;
                case Keys.S:
                    if (keyNotWork(e))
                    {
                        if (nodetree.Focused || FileTreeView.Focused)
                        {
                            nodetreeSearch.Focus();
                        }
                        else
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)//瘦的意思，这样好记点
                            {
                                ShowKA.Checked = !ShowKA.Checked;
                            }
                            else
                            {
                                c_ViewModel.Checked = !c_ViewModel.Checked;
                                if (!c_ViewModel.Checked)
                                {
                                    ReSetValue();
                                    RRReminderlist();
                                    reminderList.Focus();
                                }
                                else
                                {
                                    ReminderListSelectedIndex(-1);
                                    reminderSelectIndex = -1;
                                    IsSelectReminder = false;
                                    //mindmaplist.Focus();
                                    RRReminderlist();
                                }
                            }
                        }
                    }
                    break;
                case Keys.Scroll:
                    break;
                case Keys.Select:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Shift:

                    break;
                case Keys.ShiftKey:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.Space:

                    if (ReminderListFocused())
                    {
                        if (isInReminderlistSelect)
                        {
                            MyHide();
                        }
                    }
                    else if ((FileTreeView.Focused || nodetree.Focused) && e.Modifiers.CompareTo(Keys.Control) == 0)
                    {
                        noterichTextBox.Focus();
                    }
                    break;
                case Keys.Subtract:
                    break;
                case Keys.T:
                    if (e.Modifiers.CompareTo(Keys.Control) == 0)
                    {
                        mindmapornode.Text = "";
                        showTimeBlock.Checked = !showTimeBlock.Checked;
                        reminderList.Refresh();
                    }
                    else if (ReminderListFocused() || reminderListBox.Focused || taskTime.Focused || tasklevel.Focused)
                    {
                        if (showTimeBlock.Checked && e.Modifiers.CompareTo(Keys.Shift) == 0)//暂时保持简单吧，用Ctrl+JK来设置每天
                        {
                            TimeBlockDate.Focus();
                        }
                        else
                        {
                            if (dateTimePicker.Value < DateTime.Today)
                            {
                                dateTimePicker.Value = dateTimePicker.Value.AddDays(DateTime.Today.Day - dateTimePicker.Value.Day);
                                dateTimePicker.Value = dateTimePicker.Value.AddMonths(DateTime.Today.Month - dateTimePicker.Value.Month);
                                dateTimePicker.Value = dateTimePicker.Value.AddYears(DateTime.Today.Year - dateTimePicker.Value.Year);
                            }
                            dateTimePicker.Focus();
                        }

                    }
                    break;
                case Keys.Tab:
                    //ShiftTab切换区域
                    if (e.Modifiers.CompareTo(Keys.Control) == 0)
                    {
                        PathcomboBox.DroppedDown = true;
                        PathcomboBox.Focus();
                    }
                    else if (searchword.Focused)
                    {
                        reminderList.Focus();
                        ReminderListSelectedIndex(0);
                    }
                    break;
                case Keys.U:
                    if (ReminderListFocused() || reminderListBox.Focused || taskTime.Focused || tasklevel.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            SetLeftDakaDays(-1);
                        }
                        else
                        {
                            //设置任务剩余次数
                            SetLeftDakaDays(1);
                        }
                    }
                    else if (FileTreeView.Focused)
                    {
                        if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                        {
                            //暂时不将树视图放到最大了
                            if (nodetree.Top != 9)
                            {
                                nodetree.Top = FileTreeView.Top = nodetreeTopTop;
                                nodetree.Height = FileTreeView.Height = nodeTreeHeightMax;
                            }
                            else
                            {
                                nodetree.Top = FileTreeView.Top = nodetreeTop;
                                nodetree.Height = FileTreeView.Height = nodetreeHeight;
                            }
                        }
                        else
                        {
                            ShowMindmapFileUp();
                        }
                    }
                    else if (nodetree.Focused)
                    {
                        reminderList.Focus();
                    }
                    break;
                case Keys.Up:
                    leftIndex = 0;
                    if (keyNotWork(e))
                    {
                        if (ReminderListFocused())
                        {
                            reminderList.Refresh();
                            //if (reminderList.SelectedIndex==0)
                            //{
                            //    ReminderListSelectedIndex( reminderList.Items.Count - 1;
                            //}
                        }
                        else if (nodetree.Focused)
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                UPNodeByID(nodetree.SelectedNode.Name);
                                Extensions.MoveUp(nodetree.SelectedNode);
                                fenshuADD(1);
                                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                                th.Start();
                            }
                        }
                        else if (FileTreeView.Focused)
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                Extensions.MoveUp(FileTreeView.SelectedNode);
                                fenshuADD(1);
                            }
                        }
                        else if (mindmaplist.Focused)
                        {
                            //if (mindmaplist.SelectedIndex == 0)
                            //{
                            //    mindmaplist.SelectedIndex = mindmaplist.Items.Count - 1;
                            //}
                        }
                    }
                    break;
                case Keys.V:
                    if (keyNotWork(e))
                    {
                        if (ReminderListFocused())
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                AddClipToTask(true);
                            }
                            else
                            {
                                AddClipToTask();
                            }
                        }
                        else if (mindmaplist.Focused)
                        {
                            AddClip();
                        }
                    }
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.W:
                    searchword.Text = "";
                    if (showTimeBlock.Checked || ShowKA.Checked || ShowMoney.Checked)
                    {
                        //切换OnlyLevel
                        OnlyLevel.Checked = !OnlyLevel.Checked;
                        //mindmapornode.Text = "";
                        //showTimeBlock.Checked=ShowKA.Checked=ShowMoney.Checked =  false;
                        //reminderList.Refresh();
                        return;
                    }
                    else if (mindmapornode.Text != "")
                    {
                        mindmapornode.Text = "";
                        tasklevel.Value = 0;
                        taskTime.Value = 0;
                    }
                    else
                    {
                        showwaittask = !showwaittask;
                    }
                    RRReminderlist();
                    break;
                case Keys.X:
                    if (keyNotWork(e))
                    {
                        //if (mindmaplist.Focused)
                        //{
                        //    IsSelectReminder = false;
                        //}
                        //Thread thFileTreeForm = new Thread(() => Application.Run(new FileTreeForm(rootpath))); 
                        //thFileTreeForm.SetApartmentState(ApartmentState.STA);
                        //thFileTreeForm.Name = "FileTreeForm";
                        //thFileTreeForm.Start();
                        //MyHide();
                        if (reminderList.Focused)
                        {
                            if (e.Modifiers.CompareTo(Keys.Control) == 0)
                            {
                                AddTaskWithDate.Add(((MyListBoxItemRemind)reminderlistSelectedItem).Name);
                                new TextListConverter().WriteListToTextFile(AddTaskWithDate, System.AppDomain.CurrentDomain.BaseDirectory + @"\AddTaskWithDate.txt");
                                AddTaskWithDate = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\AddTaskWithDate.txt");
                            }
                            else if (e.Modifiers.CompareTo(Keys.Shift) == 0)
                            {
                                AddTaskWithDate.Remove(((MyListBoxItemRemind)reminderlistSelectedItem).Name);
                                new TextListConverter().WriteListToTextFile(AddTaskWithDate, System.AppDomain.CurrentDomain.BaseDirectory + @"\AddTaskWithDate.txt");
                                AddTaskWithDate = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\AddTaskWithDate.txt");
                            }
                            else
                            {
                                int index = reminderList.SelectedIndex;
                                reminderListBox.Items.Add((MyListBoxItemRemind)reminderlistSelectedItem);
                                if (todoistKey != null)
                                {
                                    try
                                    {
                                        ITodoistClient client = new TodoistClient(todoistKey);
                                        var quickAddItem = new QuickAddItem(((MyListBoxItemRemind)reminderlistSelectedItem).Name + " #yixiaozi");
                                        var task = await client.Items.QuickAddAsync(quickAddItem);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }

                                if (!Xnodes.Any(m => m.Contains(((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML)))
                                {
                                    Xnodes.Add(((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML);
                                }
                                //else
                                //{
                                //    Xnodes.RemoveAll(m => m.Contains(((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML));
                                //    Xnodes.Add(((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML);
                                //}
                                //添加去重
                                List<string> xnodesRemoveSame = new List<string>();
                                foreach (string item in Xnodes)
                                {
                                    if (!xnodesRemoveSame.Contains(item))
                                    {
                                        xnodesRemoveSame.Add(item);
                                    }
                                }
                                Xnodes = xnodesRemoveSame;
                                new TextListConverter().WriteListToTextFile(Xnodes, System.AppDomain.CurrentDomain.BaseDirectory + @"\Xnodes.txt");
                                reminderboxList.Add((MyListBoxItemRemind)reminderlistSelectedItem);
                                ReminderlistBoxChange();
                                reminderList.Items.RemoveAt(reminderList.SelectedIndex);
                                try
                                {
                                    if (reminderList.Items.Count - 1 >= index)
                                    {
                                        ReminderListSelectedIndex(index);
                                    }
                                    else
                                    {
                                        ReminderListSelectedIndex(index - 1);
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                        else if (reminderListBox.Focused)
                        {
                            int index = reminderListBox.SelectedIndex;
                            reminderboxList.Remove((MyListBoxItemRemind)reminderListBox.SelectedItem);
                            Xnodes.RemoveAll(m => m.Contains(((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML));
                            //添加去重
                            List<string> xnodesRemoveSame = new List<string>();
                            foreach (string item in Xnodes)
                            {
                                if (!xnodesRemoveSame.Contains(item))
                                {
                                    xnodesRemoveSame.Add(item);
                                }
                            }
                            Xnodes = xnodesRemoveSame;
                            new TextListConverter().WriteListToTextFile(Xnodes, System.AppDomain.CurrentDomain.BaseDirectory + @"\Xnodes.txt");
                            reminderListBox.Items.RemoveAt(reminderListBox.SelectedIndex);
                            ReminderlistBoxChange();
                            if (reminderListBox.Items.Count == 0)
                            {
                                reminderList.Focus();
                                ReminderListSelectedIndex(0);
                            }
                            else
                            {
                                try
                                {
                                    if (reminderListBox.Items.Count - 1 >= index)
                                    {
                                        reminderListBox.SelectedIndex = index;
                                    }
                                    else
                                    {
                                        reminderListBox.SelectedIndex = index;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                        Xnodes = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\Xnodes.txt");
                        return;
                    }
                    break;
                case Keys.XButton1:
                    break;
                case Keys.XButton2:
                    break;
                case Keys.Y:
                    c_Jinian.Checked = !c_Jinian.Checked;
                    break;
                case Keys.Z:
                    if (e.Modifiers.CompareTo(Keys.Control) == 0)
                    {
                        n_days.Focus();
                    }
                    else
                    {
                        //提醒任务
                        int selectindex = reminderList.SelectedIndex;
                        SetReminderOnly((MyListBoxItemRemind)reminderlistSelectedItem);
                        RRReminderlist();
                        if (selectindex < reminderList.Items.Count - 1)
                        {
                            ReminderListSelectedIndex(selectindex);
                        }
                    }

                    break;
                case Keys.Zoom:
                    break;
                default:
                    break;
            }
        }

        private void SetLink(string link)
        {
            if (reminderList.SelectedIndex >= 0)
            {
                MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(selectedReminder.Value);
                string taskName = selectedReminder.Name;
                DateTime dateBefore = selectedReminder.EndDate;
                if (selectedReminder.isEncrypted)
                {
                    taskName = encrypt.EncryptString(taskName);
                }
                foreach (XmlNode node in x.GetElementsByTagName("hook"))
                {
                    try
                    {
                        if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                        {
                            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                            if (GetAttribute(node.FirstChild, "LINK") != "")
                            {
                                node.FirstChild.Attributes["LINK"].Value = link;
                            }
                            else
                            {
                                //添加属性
                                XmlAttribute TASKLEVEL = x.CreateAttribute("LINK");
                                node.ParentNode.Attributes.Append(TASKLEVEL);
                                node.ParentNode.Attributes["LINK"].Value = link;
                            }
                            x.Save(selectedReminder.Value);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                            th.Start();
                            SaveLog("修改了任务：" + taskName + "    截止时间：" + dateBefore.ToString() + ">" + dateTimePicker.Value.ToString());
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        public string getPicture(XmlNode FatherNode, string filename)
        {
            string result = "";
            foreach (XmlNode node in FatherNode.ChildNodes)
            {
                try
                {
                    if (((System.Xml.XmlElement)node).Name == "hook" && node.Attributes["NAME"].Value == "ExternalObject")
                    {
                        result = node.Attributes["URI"].Value;
                        Directory.SetCurrentDirectory(new FileInfo(filename).Directory.FullName);
                        result = System.IO.Path.GetFullPath(result);
                        Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
                        piclink = result;
                        break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return result;
        }
        public void SetPicture(string filepath)
        {
            Stream s = File.Open(filepath, FileMode.Open);
            pictureBox1.Image = Image.FromStream(s);
            s.Close();
        }
        public string getPictureByID(string id, string mindmap)
        {
            try
            {
                string result = "";
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(mindmap);
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == id)
                    {
                        result = getPicture(node, mindmap);
                        piclink = result;
                        break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void GetPassWord()
        {
            Encrypt e = new Encrypt("pass");
            string password = e.EncryptString(searchword.Text.Substring(1), false);
            password = password.Replace("a", "@");
            password = password.Replace("b", "+");
            password = password.Replace("c", "-");
            password = password.Replace("e", "%");
            password = password.Replace("=", "#");
            if (password.Length > 13)
            {
                password = password.Substring(0, 13);
            }
            password += ".";
            Clipboard.SetDataObject(password, true);
        }

        public bool ReminderListFocused()
        {
            if (reminderList.Focused)
            {
                reminderlistSelectedItem = reminderList.SelectedItem;
                return true;
            }
            else if (reminderListBox.Focused)
            {
                reminderlistSelectedItem = reminderListBox.SelectedItem;
                return true;
            }

            return reminderList.Focused || reminderListBox.Focused;
        }
        public void SetReminderOnly(MyListBoxItemRemind selectedReminder)//selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
        {
            if (reminderList.SelectedIndex >= 0)
            {
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(selectedReminder.Value);
                string taskName = selectedReminder.Name;
                if (selectedReminder.isEncrypted)
                {
                    taskName = encrypt.EncryptString(taskName);
                }
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    if (node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == selectedReminder.IDinXML)
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
                                    item.FirstChild.Attributes["REMINDUSERAT"].Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
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
                                remindernodeTime.Value = (Convert.ToInt64((dateTimePicker.Value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                                remindernodeParameters.Attributes.Append(remindernodeTime);
                                remindernode.AppendChild(remindernodeParameters);
                                node.AppendChild(remindernode);
                            }
                            if (node.Attributes["ISReminderOnly"] != null)
                            {
                                node.Attributes["ISReminderOnly"].Value = (!selectedReminder.ISReminderOnly).ToString();
                            }
                            else
                            {
                                XmlAttribute ISReminderOnly = x.CreateAttribute("ISReminderOnly");
                                node.Attributes.Append(ISReminderOnly);
                                node.Attributes["ISReminderOnly"].Value = (!selectedReminder.ISReminderOnly).ToString();
                            }
                            x.Save(selectedReminder.Value);
                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                            th.Start();
                            SaveLog("修改了任务(是否是任务)：" + taskName + "    ：" + selectedReminder.ISReminderOnly.ToString());
                            return;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }


        }
        //获取顶级域名
        public string GetTopDomin(string url)
        {
            try
            {
                return url.Split('.')[url.Split('.').Length - 2];
            }
            catch (Exception ex)
            {
                return url;
            }
        }
        public void AddTaskToFile(XmlDocument x, string rootNode, string taskName, string link, string taskContent, string guid, DateTime dt)
        {
            if (taskName == "")
            {
                return;
            }
            //System.Xml.XmlDocument x = new XmlDocument();
            //x.Load(mindmap);
            XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == rootNode);
            //if (root.ChildNodes.Cast<XmlNode>().Any(m => m.Attributes[0].Name != "TEXT" && m.Attributes["TEXT"].Value == dt.Year.ToString()))
            if (!haschildNode(root, dt.Year.ToString()))
            {
                XmlNode yearNode = x.CreateElement("node");
                XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                yearNodeValue.Value = dt.Year.ToString();
                yearNode.Attributes.Append(yearNodeValue);
                XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
            }
            XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == dt.Year.ToString());
            if (!haschildNode(year, dt.Month.ToString()))
            {
                XmlNode monthNode = x.CreateElement("node");
                XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                monthNodeValue.Value = dt.Month.ToString();
                monthNode.Attributes.Append(monthNodeValue);
                XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
            }
            XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == dt.Month.ToString());
            if (!haschildNode(month, dt.Day.ToString()))
            {
                XmlNode dayNode = x.CreateElement("node");
                XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                dayNodeValue.Value = dt.Day.ToString();
                dayNode.Attributes.Append(dayNodeValue);
                XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
            }
            XmlNode day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == dt.Day.ToString());
            XmlNode newNote = x.CreateElement("node");
            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
            string pstr = "";
            newNotetext.Value = pstr + taskName;
            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
            newNoteCREATED.Value = (Convert.ToInt64((dt - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
            newNoteMODIFIED.Value = (Convert.ToInt64((dt - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            XmlAttribute TASKID = x.CreateAttribute("ID");
            newNote.Attributes.Append(TASKID);
            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
            XmlNode remindernode = x.CreateElement("hook");
            XmlAttribute remindernodeName = x.CreateAttribute("NAME");
            remindernodeName.Value = "plugins/TimeManagementReminder.xml";
            remindernode.Attributes.Append(remindernodeName);
            XmlNode remindernodeParameters = x.CreateElement("Parameters");
            XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
            remindernodeTime.Value = Convert.ToInt64((dt - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds).ToString();
            remindernodeParameters.Attributes.Append(remindernodeTime);
            remindernode.AppendChild(remindernodeParameters);
            newNote.AppendChild(remindernode);
            XmlAttribute guidurlAtt = x.CreateAttribute("guidurl");
            guidurlAtt.Value = guid;
            XmlAttribute urlAtt = x.CreateAttribute("Link");
            urlAtt.Value = link;
            XmlNode taskContentnode = x.CreateElement("node");
            XmlAttribute taskContentValue = x.CreateAttribute("TEXT");
            taskContentValue.Value = taskContent;
            taskContentnode.Attributes.Append(taskContentValue);
            newNote.AppendChild(taskContentnode);
            newNote.Attributes.Append(newNotetext);
            newNote.Attributes.Append(newNoteCREATED);
            newNote.Attributes.Append(newNoteMODIFIED);
            newNote.Attributes.Append(guidurlAtt);
            newNote.Attributes.Append(urlAtt);
            day.AppendChild(newNote);
        }
        public static void TextContentReplace(string file, string oldvalue, string newvalue)
        {
            String strFile = File.ReadAllText(file);
            strFile = strFile.Replace(oldvalue, newvalue);
            File.WriteAllText(file, strFile);
        }
        /// <summary>
        /// 是否为Uri
        /// </summary>
        /// <param name="s">判断字符串</param>
        /// <returns></returns>
        public static bool IsUri(string s)
        {
            return Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out Uri u);
        }
        public void SetLeftDakaDays(int num)
        {
            if (reminderlistSelectedItem == null)
            {
                return;
            }
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.Name;
            if (selectedReminder.isEncrypted)
            {
                taskName = encrypt.EncryptString(taskName);
            }
            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml" && node.ParentNode.Attributes["TEXT"].Value == taskName)
                    {
                        if (node.ParentNode.Attributes["LeftDakaDays"] == null)
                        {
                            XmlAttribute DAKADAY = x.CreateAttribute("LeftDakaDays");
                            DAKADAY.Value = num.ToString();
                            node.ParentNode.Attributes.Append(DAKADAY);
                        }
                        else
                        {
                            node.ParentNode.Attributes["LeftDakaDays"].Value = (Convert.ToInt64(node.ParentNode.Attributes["LeftDakaDays"].Value) + num).ToString();
                        }
                        x.Save(selectedReminder.Value);
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                        th.Start();
                        RRReminderlist();
                        return;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void SearchFiles()
        {
            isSearchFileOrNode = true;
            string keywords = searchword.Text.Substring(1);
            if (keywords == "")
            {
                return;
            }
            string[] keywordsArr = keywords.Split(' ');
            reminderList.Items.Clear();
            List<string> files = new List<string>();
            foreach (node item in allfiles.Where(m => StringHasArrALL(m.mindmapPath, keywordsArr)).OrderByDescending(m => m.editDateTime).Take(200))
            {
                if (!files.Contains(item.mindmapPath))
                {
                    files.Add(item.mindmapPath);
                }
                string filename = item.Text.Replace(rootpath.ToString(), "");
                filename = filename.Replace(".files", "");
                filename = filename.Replace(".images", "");
                filename = filename.Replace("\\\\", "\\");
                reminderList.Items.Add(new MyListBoxItemRemind() { Text = filename, Value = item.mindmapPath, Name = item.Text });
            }
            mindmaplist.Items.Clear();
            foreach (string item in files)
            {
                if (System.IO.Directory.Exists(item))
                {
                    mindmaplist.Items.Insert(0, new MyListBoxItem { Text = item.Split('\\')[item.Split('\\').Length - 1], Value = item });
                }
            }
            for (int i = 0; i < mindmaplist.Items.Count; i++)
            {
                mindmaplist.SetItemChecked(i, true);
            }
        }
        public static MyListBoxItemRemind newName(MyListBoxItemRemind reminder)
        {
            reminder.Text = reminder.Time.ToString("dd HH:mm") + intostringwithlenght(reminder.rtaskTime, 4) + @" " + reminder.Name;
            return reminder;
        }
        public static bool IsFileUrl(string str)
        {
            if (IsUri(str))
            {
                return false;
            }
            if (str != "")
            {
                str = str.Replace('/', '\\');
                if (Regex.IsMatch(str, @"(\w:\\)?([\w|.|:]*\\)?\w*\\{1}"))
                {
                    return true;
                }
            }
            return false;
        }
        public static string getFileUrlPath(string str)
        {
            string path = str;
            //我自己都看不懂。。。
            //if (Regex.IsMatch(str, @"(\w:\\)?([\w|.|:]*\\)?\w*\\{1}.*\.\w*"))
            //{
            //    path = Regex.Match(str, @"(\w:\\)?([\w|.|:]*\\)?\w*\\{1}.*\.\w*").ToString();
            //}
            //else
            //{
            //    path = Regex.Match(str, @"(\w:\\)?([\w|.|:]*\\)?\w*\\{1}").ToString();
            //}
            if (path[0] == '\\')
            {
                path = "." + path;
            }
            path = Path.GetFullPath(path);
            return path;
        }
        private void AddBin_btn_Paint(object sender, PaintEventArgs e)
        {
        }
        public void SaveLog(string log)
        {
            log = log.Replace("\r", " ").Replace("\n", " ");
            log = (DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "    " + log);
            try//
            {
                usedTimeLog += ((usedTimeLog == "" ? "" : Environment.NewLine) + log);
            }
            catch (Exception ex)
            {
            }
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
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSourcePlayer1 != null && videoSourcePlayer1.IsRunning)
            {
                videoSourcePlayer1.SignalToStop();
                videoSourcePlayer1.WaitForStop();
            }
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //是否取消close操作
                e.Cancel = true;
                MyHide();
            }
            try
            {
                string json = new JavaScriptSerializer()
                {
                    MaxJsonLength = Int32.MaxValue
                }.Serialize(reminderObject);
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"\reminder.json", CompressToBase64(json));
            }
            catch (Exception ex)
            {
            }
            icon.Dispose();
            SaveLog("关闭程序。");
        }
        //private void fenlei_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (((c_ViewModel.Checked && !InMindMapBool) || isHasNoFenleiModel) && mindmaplist.SelectedIndex != -1)
        //    {
        //        //设置分类-已经不需要了
        //        //jsonHasMindmaps = reminderObject.mindmaps;
        //        //foreach (Control item in this.Controls)
        //        //{
        //        //    if (item.Name.Contains("fenlei_") && !reminderObject.Fenleis.Any(m => m.Name == item.Text))
        //        //    {
        //        //        reminderObject.Fenleis.Add(new Fenlei { Name = item.Text, MindMaps = new List<string>() });
        //        //    }
        //        //}
        //        //if (mindmaplist.SelectedItem != null)
        //        //{
        //        //    foreach (Control item in this.Controls)
        //        //    {
        //        //        if (item.Name.Contains("fenlei_"))
        //        //        {
        //        //            if (((CheckBox)item).Checked && !reminderObject.Fenleis.First(m => m.Name == item.Text).MindMaps.Contains(((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3)))
        //        //            {
        //        //                reminderObject.Fenleis.First(m => m.Name == item.Text).MindMaps.Add(((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3));
        //        //            }
        //        //            if (((MyListBoxItem)mindmaplist.SelectedItem != null))
        //        //            {
        //        //                if (!((CheckBox)item).Checked && reminderObject.Fenleis.First(m => m.Name == item.Text).MindMaps.Contains(((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3)))
        //        //                {
        //        //                    while (reminderObject.Fenleis.First(m => m.Name == item.Text).MindMaps.Contains(((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3)))
        //        //                    {
        //        //                        reminderObject.Fenleis.First(m => m.Name == item.Text).MindMaps.Remove(((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3));
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"reminder.json");
        //        List<string> mindmaps = new List<string>();
        //        using (StreamReader sw = fi.OpenText())
        //        {
        //            string s = sw.ReadToEnd();
        //            var serializer = new JavaScriptSerializer()
        //            {
        //                MaxJsonLength = Int32.MaxValue
        //            };
        //            reminderObjectOut = serializer.Deserialize<Reminder>(s);
        //            jsonHasMindmaps = reminderObjectOut.mindmaps;
        //            if (false) //fenleidanxuan.Checked
        //            {
        //                //foreach (Control item in this.Controls)
        //                //{
        //                //    if (item.Name.Contains("fenlei_") && ((CheckBox)item).Checked)
        //                //    {
        //                //        isCodeFenlei = true;
        //                //        ((CheckBox)item).Checked = false;
        //                //        isCodeFenlei = false;
        //                //    }
        //                //}
        //                //CheckBox currentCheckBox = sender as CheckBox;
        //                //isCodeFenlei = true;
        //                //currentCheckBox.Checked = true;
        //                //isCodeFenlei = false;
        //                //mindmaps.AddRange(reminderObjectOut.Fenleis.First(m => m.Name == currentCheckBox.Text).MindMaps);
        //            }
        //            else
        //            {
        //                int selectedfenleicount = 0;
        //                //在这里将某个分类的添加进去。
        //                foreach (Control item in this.Controls)
        //                {
        //                    if (item.Name.Contains("fenlei_") && ((CheckBox)item).Checked)
        //                    {
        //                        selectedfenleicount++;
        //                        mindmaps.AddRange(reminderObjectOut.Fenleis.First(m => m.Name == item.Text).MindMaps);
        //                    }
        //                }
        //                //如果一个都没选择，就把没有分类的显示出来，方便分类。
        //            }
        //            //mindmaplist = mindmaplist_backup;
        //            mindmaplist.Items.Clear();
        //            mindmaplist.Items.AddRange(mindmaplist_backup);
        //            //for (int i = 0; i < mindmaplist.Items.Count; i++)
        //            //{
        //            //    if (mindmaps.Contains(((MyListBoxItem)mindmaplist.Items[i]).Text))
        //            //    {
        //            //        mindmaplist.SetItemCheckState(i, CheckState.Checked);
        //            //    }
        //            //    else
        //            //    {
        //            //        mindmaplist.Items.RemoveAt(i);
        //            //        //mindmaplist.SetItemCheckState(i, CheckState.Unchecked);
        //            //    }
        //            //}
        //            for (int i = mindmaplist.Items.Count - 1; i >= 0; i--)
        //            {
        //                if (mindmaps.Contains(((MyListBoxItem)mindmaplist.Items[i]).Text))
        //                {
        //                    mindmaplist.SetItemCheckState(i, CheckState.Checked);
        //                }
        //                else
        //                {
        //                    if (c_ViewModel.Checked)
        //                    {
        //                        if (!jsonHasMindmaps.Contains(((MyListBoxItem)mindmaplist.Items[i]).Text))
        //                        {
        //                            MyListBoxItem newitem = new MyListBoxItem
        //                            {
        //                                Text = ((MyListBoxItem)mindmaplist.Items[i]).Text,
        //                                Value = ((MyListBoxItem)mindmaplist.Items[i]).Value,
        //                                IsSpecial = true
        //                            };
        //                            mindmaplist.Items.RemoveAt(i);
        //                            mindmaplist.Items.Insert(i, newitem);
        //                        }
        //                        mindmaplist.SetItemCheckState(i, CheckState.Unchecked);
        //                    }
        //                    else
        //                    {
        //                        mindmaplist.Items.RemoveAt(i);
        //                    }
        //                }
        //            }
        //        }
        //        tasklevel.Value = 0;
        //        taskTime.Value = 0;
        //        //将没有分过类的导图设置颜色
        //        RRReminderlist();
        //    }
        //}
        private void mindmaplist_MouseHover(object sender, EventArgs e)
        {
            InMindMapBool = true;
            SwitchToLanguageMode();
            //if (!searchword.Focused)
            //{
            //    mindmaplist.Focus();
            //}
        }
        private void mindmaplist_MouseLeave(object sender, EventArgs e)
        {
            InMindMapBool = false;
        }

        public void SetUnCheck()
        {
            isCodeFenlei = false;
            foreach (Control item in this.Controls)
            {
                if (item.Name.Contains("fenlei_") && ((CheckBox)item).Checked)
                {
                    ((CheckBox)item).Checked = false;
                }
            }
            isCodeFenlei = true;
        }
        private void Quanxuan_CheckedChanged(object sender, EventArgs e)
        {
            return;
            //isHasNoFenleiModel = false;
            //if (true)
            //{
            //    IsViewModel.Checked = false;
            //    //moshiview.Checked = false;
            //    //fenleidanxuan.Checked = false;
            //    isCodeFenlei = false;
            //    foreach (Control item in this.Controls)
            //    {
            //        if (item.Name.Contains("fenlei_") && !((CheckBox)item).Checked)
            //        {
            //            ((CheckBox)item).Checked = true;
            //        }
            //    }
            //    isCodeFenlei = true;
            //    fenlei_CheckedChanged(null, null);
            //}
            //else
            //{
            //    isCodeFenlei = false;
            //    foreach (Control item in this.Controls)
            //    {
            //        if (item.Name.Contains("fenlei_") && ((CheckBox)item).Checked)
            //        {
            //            ((CheckBox)item).Checked = false;
            //        }
            //    }
            //    isCodeFenlei = true;
            //    fenlei_CheckedChanged(null, null);
            //}
        }
        private void IsViewModel_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void morning_CheckedChanged(object sender, EventArgs e)
        {
            if (true)
            {
                taskTime.Value = 0;
                tasklevel.Value = 0;
                RRReminderlist();
            }
        }
        public void fenshuADD(int n)
        {
            try
            {
                fenshu.Text = (Convert.ToInt64(fenshu.Text) + n).ToString();
                ini.WriteInt("info", "score", Convert.ToInt32(fenshu.Text) + n);
            }
            catch (Exception ex)
            {
            }
        }
        private void IsRememberModel_CheckedChanged(object sender, EventArgs e)
        {
            c_ViewModel.Checked = false;
            try
            {
                if (mindmaplist.SelectedItem != null)
                {
                    fathernode.Text = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
                }
            }
            catch (Exception ex)
            {
            }
            UsedLogRenew();
            Load_Click(null, null);
        }
        private void IsShowSub_CheckedChanged(object sender, EventArgs e)
        {
            //ShowSubNode();
        }
        public void ShowMindmapFile(bool isShowSub = false, int level = 3)
        {
            mostShowFiles = 200;
            if (reminderlistSelectedItem == null && mindmaplist.SelectedItem == null)
            {
                return;
            }
            FileTreeView.Nodes.Clear();
            string mindmapPath = "";
            string Name = "";
            if (ReminderListFocused() || searchword.Focused)
            {
                if (reminderlistSelectedItem == null || ((MyListBoxItemRemind)reminderlistSelectedItem).Name == "当前时间")
                {
                    return;
                }
                Name = ((MyListBoxItemRemind)reminderlistSelectedItem).Name;
                mindmapPath = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
            }
            else if (mindmaplist.Focused)
            {
                if (mindmaplist.SelectedItem == null)
                {
                    return;
                }
                mindmapPath = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
                Name = ((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3);
            }
            if (mindmapPath == "")
            {
                return;
            }
            fileTreePath = new FileInfo(mindmapPath).Directory;
            BuildTree(fileTreePath, FileTreeView.Nodes, true, level);
            FileTreeView.Sort();
            //选中当前的文件
            //string fileName = Path.GetFileName(mindmapPath);
            //foreach (TreeNode item in FileTreeView.Nodes)
            //{
            //    if (item.Text == fileName || item.Text.Contains(Name))
            //    {
            //        FileTreeView.SelectedNode = item;
            //        break;
            //    }
            //}
        }
        

        public void ShowMindmapFileUp()
        {
            FileTreeView.Nodes.Clear();
            fileTreePath = fileTreePath.Parent;
            BuildTree(fileTreePath, FileTreeView.Nodes, true);
            FileTreeView.Sort();
            FileTreeView.Focus();
            try
            {
                //默认选择第一个；
                FileTreeView.SelectedNode = FileTreeView.Nodes[0];
            }
            catch (Exception ex)
            {
            }
        }
        public static int mostShowFiles = 200;
        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe, bool isRoot, int level = 3)
        {
            if (level <= 0)
            {
                return;
            }
            TreeNode curNode = new TreeNode();
            if (isRoot)
            {
                foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
                {
                    if ((subdir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        BuildTree(subdir, FileTreeView.Nodes, false, level - 1);
                    }
                }
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (file.Name.StartsWith("~") || (file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue;
                    }
                    TreeNode newnode = FileTreeView.Nodes.Add(file.FullName, file.Name);
                    if (System.IO.Directory.Exists(file.FullName))
                    {
                        newnode.Tag = string.Join(",", System.IO.Directory.GetFiles(file.FullName, "*.*", SearchOption.AllDirectories));
                    }
                }
            }
            else
            {
                mostShowFiles--;
                if (mostShowFiles<=0)
                {
                    return;
                }
                curNode = addInMe.Add(directoryInfo.FullName, " " + directoryInfo.Name);
                if (System.IO.Directory.Exists(directoryInfo.FullName))
                {
                    curNode.Tag = string.Join(",", System.IO.Directory.GetFiles(directoryInfo.FullName, "*.*", SearchOption.AllDirectories));
                }
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    if (file.Name.StartsWith("~") && (file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        continue;
                    }
                    curNode.Nodes.Add(file.FullName, file.Name);
                }
                foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
                {
                    if ((subdir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    {
                        BuildTree(subdir, curNode.Nodes, false, level - 1);
                    }
                }
            }
        }


        private void FileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            nodetree.Visible = true;
            try
            {
                fathernode.Text = e.Node.Name;
            }
            catch (Exception ex)
            {
            }
            if (e.Node.Name.ToLower().EndsWith("txt"))
            {
                //richTextBox.Visible = true;
                //StreamReader reader = new StreamReader(e.Node.Name, System.Text.Encoding.Default);
                //richTextBox.Text = reader.ReadToEnd();
                //reader.Close();
            }
            //*.jpg,*.jpeg,*.bmp,*.gif,*.ico,*.png,*.tif,*.wmf
            else if (e.Node.Name.ToLower().EndsWith("jpg") || e.Node.Name.ToLower().EndsWith("gif") || e.Node.Name.ToLower().ToLower().EndsWith("bmp") || e.Node.Name.ToLower().EndsWith("png") || e.Node.Name.ToLower().EndsWith("jpeg") || e.Node.Name.ToLower().EndsWith("ico") || e.Node.Name.ToLower().EndsWith("bmp") || e.Node.Name.ToLower().EndsWith("bmp") || e.Node.Name.ToLower().EndsWith("bmp") || e.Node.Name.ToLower().EndsWith("bmp") || e.Node.Name.ToLower().EndsWith("bmp") || e.Node.Name.ToLower().EndsWith("bmp") || e.Node.Name.ToLower().EndsWith("bmp"))
            {
                //pictureBox1.Image = Image.FromFile(e.Node.Name);
                SetPicture(e.Node.Name);
            }
            else if (e.Node.Name.ToLower().EndsWith("mm"))
            {
                string mindmapPath = e.Node.Name;
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(mindmapPath);
                nodetree.Nodes.Clear();
                TreeNode tNode = new TreeNode
                {
                    Text = "Root"
                };
                if (showMindmapName == mindmapPath)
                {
                    //如果为空则继续显示
                    if (nodetree.Nodes.Count != 0)
                    {
                        return;
                    }
                }
                else
                {
                    showMindmapName = mindmapPath;
                }
                AddNode(x.DocumentElement, tNode, true);
                nodetree.Visible = true;
            }
            else if (e.Node.Name.ToLower().EndsWith("pdf"))
            {
            }
            else if (e.Node.Name.ToLower().EndsWith("docx") || e.Node.Name.ToLower().EndsWith("doc") || e.Node.Name.ToLower().EndsWith("ppt") || e.Node.Name.ToLower().EndsWith("pptx") || e.Node.Name.ToLower().EndsWith("xlsx") || e.Node.Name.ToLower().EndsWith("xls"))
            {
            }
        }



        public void ShowMindmap(bool isShowSub = false)
        {
            lastEdit = DateTime.Now.AddYears(-10);
            string taskid = "";//节点ID，选中
            lastnodeID = "";
            fathernode.Visible = false;
            if (searchword.Text.StartsWith("#") || (reminderlistSelectedItem == null && mindmaplist.SelectedItem == null))
            {
                return;
            }
            string id = "";
            string rootname = "Root";
            if (ReminderListFocused() || searchword.Focused)
            {
                if (reminderlistSelectedItem == null || ((MyListBoxItemRemind)reminderlistSelectedItem).Name == "当前时间")
                {
                    return;
                }
                taskid = ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML;
                showMindmapName = ((MyListBoxItemRemind)reminderlistSelectedItem).Value;
                id = ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML;
                if (isShowSub)
                {
                    rootname = "RootWithTime-NOMORE";//禁止显示时间了
                }
            }
            else if (mindmaplist.Focused)
            {
                if (mindmaplist.SelectedItem == null)
                {
                    return;
                }
                showMindmapName = ((MyListBoxItem)mindmaplist.SelectedItem).Value;
                //taskid = ((MyListBoxItem)mindmaplist.SelectedItem).Text.Substring(3);
            }
            if (showMindmapName == "")
            {
                return;
            }
            UsedLogRenew();//切换导图时更新
            richTextSubNode.Clear();
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(showMindmapName);
            nodetree.Nodes.Clear();
            TreeNode tNode = new TreeNode
            {
                Text = rootname// "RootWithTime"
            };
            if (isShowSub)
            {
                XmlNode parentNode = x.FirstChild;
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    try
                    {
                        if (node != null && node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == id)
                        {
                            parentNode = node;
                            AddNode(parentNode, tNode, true);
                            if (taskid != "")
                            {
                                //寻找此节点后面的最晚的任务
                                SelectTreeNodeBehindNode(nodetree.Nodes, taskid);
                            }
                            else if (lastnodeID != "")
                            {
                                SelectTreeNode(nodetree.Nodes, lastnodeID);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            else
            {
                AddNode(x.DocumentElement, tNode, true);
                if (taskid != "")
                {
                    SelectTreeNodeBehindNode(nodetree.Nodes, taskid);
                    //SelectTreeNode(nodetree.Nodes, taskid);
                }
                else if (lastnodeID != "")
                {
                    SelectTreeNode(nodetree.Nodes, lastnodeID);
                }
            }

        }






        public DateTime lastEdit;
        public string lastnodeID;
        /// <summary>
        /// Renders a node of XML into a TreeNode. Recursive if inside the node there are more child nodes.
        /// </summary>
        /// <param name="inXmlNode"></param>
        /// <param name="inTreeNode"></param>
        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode, bool isSubNode = false)
        {
            XmlNode xNode;
            TreeNode tNode = new TreeNode();
            XmlNodeList nodeList;
            int i;
            if (!inXmlNode.HasChildNodes)
            {
                return;
            }
            nodeList = inXmlNode.ChildNodes;
            for (i = 0; i <= nodeList.Count - 1; i++)
            {
                xNode = inXmlNode.ChildNodes[i];
                if (xNode.Name == "hook" || xNode.Name == "icon" || xNode.Name == "edge")
                {
                    continue;
                }
                if (isSubNode && xNode.Name == "node" && xNode.Attributes != null && xNode.Attributes["TEXT"] != null)
                {
                    //CREATED
                    DateTime dt = DateTime.Now;
                    string reminder = GetAttribute(xNode, "CREATED");
                    if (reminder != "")
                    {
                        long unixTimeStamp = Convert.ToInt64(reminder);
                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                        dt = startTime.AddMilliseconds(unixTimeStamp);
                    }

                    if (dt >= lastEdit)
                    {
                        lastEdit = dt;
                        lastnodeID = GetAttribute(xNode, "ID");
                    }
                    if (showMindmapName.EndsWith("\\" + xNode.Attributes["TEXT"].InnerText + ".mm"))//必须和文件名完全一样才认为是根节点
                    {
                        AddNode(xNode, inTreeNode, true);
                    }
                    else if (xNode.Attributes["TEXT"].InnerText == "bin")//不显示bin节点
                    {
                        continue;
                    }
                    else
                    {
                        TreeNode inTreeNodeAdd;
                        if (inTreeNode.Text.StartsWith("Root"))
                        {
                            inTreeNodeAdd = nodetree.Nodes.Add(xNode.Attributes["ID"].Value, gettasktime(xNode) + (reminder != "" && inTreeNode.Text == "RootWithTime" ? dt.ToString("MMddHH ") : "") + xNode.Attributes["TEXT"].InnerText);
                        }
                        else
                        {
                            inTreeNodeAdd = inTreeNode.Nodes.Add(xNode.Attributes["ID"].Value, (gettasktime(xNode) + xNode.Attributes["TEXT"].InnerText));
                        }
                        inTreeNodeAdd.Tag = xNode;
                        if (GetAttribute(xNode, "LINK") != "")
                        {
                            inTreeNodeAdd.ForeColor = Color.DeepSkyBlue;
                        }
                        if (xNode.HasChildNodes)
                        {
                            AddNode(xNode, inTreeNodeAdd, true);
                        }
                    }
                }
            }
        }
        public string gettasktime(XmlNode node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item != null && item.Attributes != null && item.Attributes["NAME"] != null && item.Attributes["NAME"].Value == "plugins/TimeManagementReminder.xml")
                {
                    long unixTimeStamp = Convert.ToInt64(item.FirstChild.Attributes["REMINDUSERAT"].Value);
                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                    return startTime.AddMilliseconds(unixTimeStamp).ToString("MMddHH") + ":";
                }
            }
            return "";
        }

        public void SelectTreeNode(TreeNodeCollection node, string taskid)
        {
            foreach (TreeNode item in node)
            {
                try
                {
                    if (item.Name == (taskid))
                    {
                        nodetree.SelectedNode = item;
                        nodetree.SelectedNode.Expand();//展开当前节点
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
                SelectTreeNode(item.Nodes, taskid);
            }
        }
        public void SelectTreeNodeBehindNode(TreeNodeCollection node, string taskid)
        {
            foreach (TreeNode item in node)
            {
                try
                {
                    if (item.Name == (taskid))
                    {
                        if (item.Nodes.Count == 0)
                        {
                            nodetree.SelectedNode = item;
                            nodetree.SelectedNode.Expand();//展开当前节点
                            return;
                        }
                        else
                        {
                            lastEdit = DateTime.Today.AddYears(-10);
                            lastnodeID = "";
                            FindLastEditNode(item.Nodes);
                            SelectTreeNode(item.Nodes, lastnodeID);//展开当前节点
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
                SelectTreeNodeBehindNode(item.Nodes, taskid);
            }
        }
        public void FindLastEditNode(TreeNodeCollection node)
        {
            foreach (TreeNode item in node)
            {
                try
                {
                    //CREATED
                    DateTime dt = DateTime.Now;
                    string reminder = GetAttribute((XmlNode)item.Tag, "CREATED");
                    if (reminder != "")
                    {
                        long unixTimeStamp = Convert.ToInt64(reminder);
                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                        dt = startTime.AddMilliseconds(unixTimeStamp);
                    }

                    if (dt >= lastEdit)
                    {
                        lastEdit = dt;
                        lastnodeID = GetAttribute((XmlNode)item.Tag, "ID");
                    }
                }
                catch (Exception ex)
                {
                }
                FindLastEditNode(item.Nodes);
            }
        }
        public void ShowSubNode()
        {
            try
            {
                if (showTimeBlock.Checked || ShowMoney.Checked || ShowKA.Checked)
                {
                    //((MyListBoxItemRemind)reminderlistSelectedItem).remindertype
                    if (((MyListBoxItemRemind)reminderlistSelectedItem).remindertype != "")
                    {
                        richTextSubNode.Clear();
                        richTextSubNode.AppendText(((MyListBoxItemRemind)reminderlistSelectedItem).remindertype);
                    }
                    else
                    {
                        richTextSubNode.Clear();
                    }
                    //显示时间块的分类
                    Hours.Text = ((MyListBoxItemRemind)reminderlistSelectedItem).link;
                }
                if (searchword.Text.StartsWith("#") || searchword.Text.StartsWith("！") || searchword.Text.StartsWith("·") || searchword.Text.StartsWith("~") || nodetree.Focused || FileTreeView.Focused || reminderlistSelectedItem == null)
                {
                    return;
                }
                if (((MyListBoxItemRemind)reminderlistSelectedItem).Name == "当前时间" || reminderlistSelectedItem == null || !((MyListBoxItemRemind)reminderlistSelectedItem).Value.EndsWith("mm"))
                {
                    return;
                }
                richTextSubNode.Clear();

                //当任务长度大于某个长度时，将其显示在子节点框
                if (((MyListBoxItemRemind)reminderlistSelectedItem).Text != null && ((MyListBoxItemRemind)reminderlistSelectedItem).Text.Length > 45)
                {
                    richTextSubNode.AppendText((richTextSubNode.Text == "" ? "" : Environment.NewLine) + ((MyListBoxItemRemind)reminderlistSelectedItem).Name);
                    richTextSubNode.AppendText(Environment.NewLine);
                }
                if (((MyListBoxItemRemind)reminderlistSelectedItem).link != "")
                {
                    richTextSubNode.AppendText((richTextSubNode.Text == "" ? "" : Environment.NewLine) + ((MyListBoxItemRemind)reminderlistSelectedItem).link);
                    richTextSubNode.AppendText(Environment.NewLine);
                }
                System.Xml.XmlDocument x = new XmlDocument();
                string id = "";
                try//解决文件被占用时报错
                {
                    x.Load(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                }
                catch (Exception ex)
                {
                    return;
                }
                id = ((MyListBoxItemRemind)reminderlistSelectedItem).IDinXML;
                if (richTextSubNodeID == id)
                {
                    richTextSubNodeID = id;
                }
                if (x.GetElementsByTagName("node").Count == 0)
                {
                    return;
                }
                string subNodesInfo = "";
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    try
                    {
                        if (node != null && node.Attributes != null && node.Attributes["ID"] != null && node.Attributes["ID"].InnerText == id)
                        {
                            try
                            {
                                //显示父节点
                                fathernode.Text = GetFatherNodeName(node);
                                try
                                {
                                    string pictureUrl = getPicture(node, ((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                                    if (pictureUrl != "")
                                    {
                                        SetPicture(pictureUrl);
                                        //pictureBox1.Image = Image.FromFile(pictureUrl);
                                        if (pictureBox1.Image.Height >= pictureBox1.Image.Width)
                                        {
                                            pictureBox1.Height = pictureBox1.MaximumSize.Height;
                                        }
                                        else
                                        {
                                            pictureBox1.Height = Convert.ToInt16(pictureBox1.MaximumSize.Height * (Convert.ToDouble(pictureBox1.Image.Height) / pictureBox1.Image.Width));
                                        }
                                    }
                                    else
                                    {
                                        pictureBox1.Height = 0;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    pictureBox1.Height = 0;
                                }
                                List<string> mytags = new List<string>();
                                tagList.Tags = mytags;
                                foreach (XmlNode subNode in node.ChildNodes)
                                {
                                    if (subNode.Attributes != null && subNode.Attributes["TEXT"] != null && subNode.Attributes["TEXT"].Value.ToLower() != "ok")
                                    {
                                        subNodesInfo += (subNode.Attributes["TEXT"].Value + Environment.NewLine);
                                    }

                                    //如果TYPE等于DETAILS显示节点Tag
                                    if (subNode.Attributes != null && subNode.Attributes["TYPE"] != null && subNode.Attributes["TYPE"].Value == "DETAILS")
                                    {
                                        string tagStr = new HtmlToString().StripHTML((subNode.InnerText).Replace("|", "").Replace("@", "").Replace("\r", "").Replace("\n", "")); ;
                                        if (tagStr != "")
                                        {
                                            mytags.AddRange(tagStr.Split('#'));
                                            tagList.Tags = mytags;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                richTextSubNode.AppendText(subNodesInfo);
                                subNodesInfo = "";
                                continue;
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                richTextSubNode.AppendText(subNodesInfo);
            }
            catch (Exception ex)
            {
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
                        if (node.ParentNode.ParentNode == null || node.ParentNode.ParentNode.Name == "map")
                        {
                            break;
                        }
                        s = (node.ParentNode.Attributes["TEXT"] != null ? node.ParentNode.Attributes["TEXT"].Value : "") + (s != "" ? ">" : "") + s;
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

        public void ShowHTML()
        {
            return;
            //if (searchword.Text.StartsWith("#"))
            //{
            //    return;
            //}
            //if (reminderList.SelectedIndex < 0)
            //{
            //    return;
            //}
            //if (((MyListBoxItemRemind)reminderlistSelectedItem).Name == "当前时间")
            //{
            //    return;
            //}
            //richTextSubNode.Clear();
            //string str1 = "hook";
            //string str2 = "NAME";
            //string str3 = "plugins/TimeManagementReminder.xml";
            //System.Xml.XmlDocument x = new XmlDocument();
            //string Name = "";
            //if (x.GetElementsByTagName(str1).Count == 0)
            //{
            //    return;
            //}
            //foreach (XmlNode node in x.GetElementsByTagName(str1))
            //{
            //    try
            //    {
            //        if (node.Attributes[str2].Value == str3 && node.ParentNode.Attributes["TEXT"].Value == Name)
            //        {
            //            try
            //            {
            //                //rsslinktextBox.Text = node.ParentNode.Attributes["Link"].Value;
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //            foreach (XmlNode subnode in node.ParentNode.ChildNodes)
            //            {
            //                if (subnode.Name == "node")
            //                {
            //                    try
            //                    {
            //                        using (WebBrowser webBrowser1 = new WebBrowser())
            //                        {
            //                            webBrowser1.Visible = false;
            //                            webBrowser1.DocumentText = subnode.Attributes["TEXT"].Value;
            //                            webBrowser1.Document.Write(subnode.Attributes["TEXT"].Value);
            //                            if (webBrowser1.Document.Body.InnerText.StartsWith("\r\n"))
            //                            {
            //                                //richTextBox1.Text = webBrowser1.Document.Body.InnerText.Substring(4).Replace("\r\n\r\n", "\r\n"); ;
            //                            }
            //                            else
            //                            {
            //                                //richTextBox1.Text = webBrowser1.Document.Body.InnerText.Replace("\r\n\r\n", "\r\n");
            //                            }
            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex) { }
            //}
        }
        public void GetNowIndex()
        {
            //{
            //    if (((MyListBoxItemRemind)item).Text.Contains("当前时间")) {

            //        return;
            //    }
            //}
            for (int i = 0; i < reminderList.Items.Count; i++)
            {
                MyListBoxItemRemind item = (MyListBoxItemRemind)reminderList.Items[i];
                if (item.Text.Contains("此时") && item.Name == "当前时间")
                {
                    if (reminderList.Items.Count >= i + 1)
                    {
                        ReminderListSelectedIndex(i);
                        return;
                    }
                    //会自动加1的
                    //ReminderListSelectedIndex( i;
                    //return;
                }
            }
        }
        private void Searchword_TextChanged(object sender, EventArgs e)
        {
            if (searchword.Text.ToLower().StartsWith("ss") && !searchword.Text.ToLower().EndsWith("jj"))
            {
                return;
            }
            try
            {
                //单子打字练习
                if (mindmaplist.SelectedItem != null)
                {
                    if (((MyListBoxItem)mindmaplist.SelectedItem).Text.Contains("单词"))
                    {
                        if (searchword.Text == ((MyListBoxItemRemind)reminderlistSelectedItem).Name)
                        {
                            taskComplete_btn_Click(null, null);
                            searchword.Text = "";
                            reminderList.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            if (searchword.Text.ToLower().StartsWith("exit") || searchword.Text.ToLower().EndsWith("exit"))
            {
                Application.Exit();
            }
            else if (searchword.Text.StartsWith("paizhao"))//开始拍照
            {
                CameraTimer_Tick(null, null);
                searchword.Text = "";
            }
            else if (searchword.Text.StartsWith("luyinstart"))//开始录音
            {
                StartRecordCheckBox.Checked = true;
                searchword.Text = "";
            }
            else if (searchword.Text.StartsWith("luyinend"))//结束录音
            {
                StartRecordCheckBox.Checked = false;
                searchword.Text = "";
            }
            else if (searchword.Text.StartsWith("jietu"))//截图快速
            {
                searchword.Text = "";
                Jietu();
                return;
            }
            else if (searchword.Text.StartsWith("rtimeblock"))//打开时间块报告
            {
                Thread thCalendarForm = new Thread(() => Application.Run(new TimeBlockReport()));
                thCalendarForm.Start();
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.StartsWith("rkeyboard"))//打开键盘报告
            {
                Thread thCalendarForm = new Thread(() => Application.Run(new KeyHours()));
                thCalendarForm.Start();
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.StartsWith("rmindmap"))//打开导图分析报告
            {
                Thread thCalendarForm = new Thread(() => Application.Run(new MindMapDataReport()));
                thCalendarForm.Start();
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.EndsWith("rmindmap"))//打开导图分析报告
            {
                string showday = searchword.Text.Replace("rmindmap", "");
                int showdays = -1;
                if (showday != "")
                {
                    try
                    {
                        showdays = Convert.ToInt16(showday);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                Thread thCalendarForm = new Thread(() => Application.Run(new MindMapDataReport(showdays)));
                thCalendarForm.Start();
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.StartsWith("rusetime"))//打开使用分析报告
            {
                Thread thCalendarForm = new Thread(() => Application.Run(new UseTime()));
                thCalendarForm.Start();
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.StartsWith("rtarget"))//打开目标报告
            {
                Thread thCalendarForm = new Thread(() => Application.Run(new Target()));
                thCalendarForm.Start();
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.StartsWith("rtrend"))//打开趋势报告
            {
                Thread thCalendarForm = new Thread(() => Application.Run(new TimeBlockTrend()));
                thCalendarForm.Start();
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.ToLower().StartsWith("githome"))//打开Git
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory);
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.ToLower().StartsWith("githubhome"))//打开Github
            {
                Process.Start("https://github.com/yixiaozi/DocearReminder");
                MyHide();
                searchword.Text = "";
                return;
            }
            else if (searchword.Text.StartsWith("rr") || searchword.Text.EndsWith("rrr"))
            {
                searchword.Text = "";
                mindmapornode.Text = "";
                Load_Click(null, null);
            }
            else if (searchword.Text.StartsWith("NNN") || searchword.Text.EndsWith("NNN"))
            {
                searchword.Text = "";
                //下面窗口设置一下
                nodetree.Top = FileTreeView.Top = nodetreeTop;
                nodetree.Height = FileTreeView.Height = nodetreeHeight;
                fathernode.Visible = false;
                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = false;
                this.Height = normalheight; showMindmapName = "";
                if (focusedList == 0)
                {
                    reminderList.Focus();
                }
                else
                {
                    reminderListBox.Focus();
                }
                Center();
                //刷新一下任务
                if (mindmapornode.Text != "")
                {
                    mindmapornode.Text = "";
                    tasklevel.Value = 0;
                    taskTime.Value = 0;
                    RRReminderlist();
                }
            }
            else if (searchword.Text.ToLower().StartsWith("clipse"))
            {
                searchword.Text = "";
                MyHide();
                OpenSearch();
            }
            else if (searchword.Text.StartsWith("clipF"))
            {
                searchword.Text = "";
                MyHide();
                Btn_OpenFolder_Click();
            }
            else if (searchword.Text.ToLower().StartsWith("clipf"))
            {
                searchword.Text = "";
                MyHide();
                btn_OpenFile_MouseClick();
            }
            else if (searchword.Text.ToLower().StartsWith("gread"))
            {
                searchword.Text = "";
                ReadTagFile();
            }
            else if (searchword.Text.ToLower().StartsWith("gwrite"))
            {
                searchword.Text = "";
                WriteTagFile();
            }
            else if (searchword.Text.ToLower() == "`" || searchword.Text.ToLower() == "·")
            {
                isSearchFileOrNode = true;
                reminderList.Items.Clear();
                foreach (var file in RecentlyFileHelper.GetRecentlyFiles(""))
                {
                    try
                    {
                        reminderList.Items.Add(file);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                reminderList.Sorted = false;
                reminderList.Sorted = true;
            }
            else if (searchword.Text.ToLower().StartsWith("playsound"))
            {
                searchword.Text = "";
                isPlaySound = !isPlaySound;
            }
            else if (searchword.Text.ToLower().StartsWith("playback"))
            {
                searchword.Text = "";
                playBackGround = !playBackGround;
            }
            else if (searchword.Text.ToLower().StartsWith("ga"))
            {
                this.Close();
                string gitCommand = "git";
                string gitAddArgument = @"add -A";
                System.Diagnostics.Process.Start(gitCommand, gitAddArgument);
                searchword.Text = "";

                return;
            }
            else if (searchword.Text.ToLower().StartsWith("gitpush"))
            {
                this.Close();
                string gitCommand = "git";
                string gitAddArgument = @"push";
                System.Diagnostics.Process.Start(gitCommand, gitAddArgument);
                searchword.Text = "";

                return;
            }
            else if (searchword.Text.ToLower().StartsWith("help"))
            {
                try
                {
                    searchword.Text = "";
                    Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + "DocearReminder.chm");
                    MyHide();
                }
                catch (Exception ex)
                {
                }
                return;
            }
            else if (searchword.Text.ToLower().StartsWith("showtimeblock") || searchword.Text.ToLower().StartsWith("showtb") || searchword.Text.ToLower().StartsWith("ttt") || searchword.Text.ToLower().StartsWith("qqq"))
            {
                searchword.Text = "";
                showTimeBlock.Checked = !showTimeBlock.Checked;
                return;
            }
            else if (searchword.Text.StartsWith("deltemp"))
            {
                try
                {
                    ////首先删除临时文件
                    DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", ""))); //System.AppDomain.CurrentDomain.BaseDirectory);
                    foreach (FileInfo file in path.GetFiles("~*.mm", SearchOption.AllDirectories))
                    {
                        file.Delete();
                    }
                    foreach (FileInfo file in path.GetFiles("*.MM", SearchOption.AllDirectories))
                    {
                        System.IO.File.Move(file.FullName, file.FullName.Substring(0, file.FullName.Length - 2) + "mm");
                    }
                    searchword.Text = "";
                }
                catch (Exception ex)
                {
                }
            }
            else if (searchword.Text.ToLower().StartsWith("usedsug"))
            {
                searchword.Text = "";
                RecentOpenedMap = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\RecentOpenedMap.txt");
            }
            else if (searchword.Text.ToLower().StartsWith("usedsu2"))
            {
                IconNodesSelected = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\IconNodesSelected.txt");
                searchword.Text = "";
            }
            else if (searchword.Text.ToLower().StartsWith("usedsu3"))
            {
                OpenedInRootSearch = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\OpenedInRootSearch.txt");
                searchword.Text = "";
            }
            else if (searchword.Text.ToLower().EndsWith("ccc"))
            {
                Clipboard.SetText(searchword.Text.Replace("ccc", ""));
                searchword.Text = "";
            }
            else if (searchword.Text.ToLower().StartsWith("jj") || searchword.Text.ToLower().Contains("jjj"))
            {
                if (searchword.Text.StartsWith("#"))
                {
                    searchword.Text = searchword.Text.Replace("jjj", "");
                    if (focusedList == 0)
                    {
                        reminderList.Focus();
                        if (reminderList.Items.Count > 0)
                        {
                            ReminderListSelectedIndex(0);
                        }
                    }
                    else
                    {
                        reminderListBox.Focus();
                    }
                }
                else
                {
                    searchword.Text = "";
                    reminderList.Focus();
                    GetNowIndex();
                }
            }
            else if (searchword.Text.StartsWith("allfile"))
            {
                searchword.Text = "";
                GetAllFilesJsonFile();
                yixiaozi.Model.DocearReminder.StationInfo.StationData = null;
            }
            else if (searchword.Text.StartsWith("allicon"))
            {
                searchword.Text = "";
                GetAllFilesJsonIconFile();
                yixiaozi.Model.DocearReminder.StationInfo.NodeData = null;
            }
            else if (searchword.Text.ToLower().StartsWith("timeblock"))
            {
                searchword.Text = "";
                GetTimeBlock();
                yixiaozi.Model.DocearReminder.StationInfo.TimeBlockData = null;
            }
            else if (searchword.Text.ToLower().StartsWith("moneym"))
            {
                searchword.Text = "";
                GetTimeBlock();
                yixiaozi.Model.DocearReminder.StationInfo.TimeBlockData = null;
            }
            else if (searchword.Text.ToLower().StartsWith("kaka"))
            {
                searchword.Text = "";
                GetTimeBlock();
                yixiaozi.Model.DocearReminder.StationInfo.TimeBlockData = null;
            }
            else if (searchword.Text.StartsWith("addtaskdate"))
            {
                searchword.Text = "";
                AddTaskWithDate = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\AddTaskWithDate.txt");
            }
            else if (searchword.Text.StartsWith("addtaskdatt"))
            {
                searchword.Text = "";
                Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"\AddTaskWithDate.txt");
            }
            else if (searchword.Text.StartsWith("newfiles"))
            {
                searchword.Text = "";
                NewFiles();
                yixiaozi.Model.DocearReminder.StationInfo.NodeData = null;
            }
            else if (searchword.Text.StartsWith("mvt"))//移动文件到指定目录
            {
                string foldername = searchword.Text.Substring(3);
                if (ini.ReadStringDefault("movefile", foldername, "").Trim() != "" && ((MyListBoxItemRemind)reminderlistSelectedItem).link != "" && IsFileUrl(((MyListBoxItemRemind)reminderlistSelectedItem).link))
                {
                    System.IO.File.Move(((MyListBoxItemRemind)reminderlistSelectedItem).link.Replace("file:/", ""), ini.ReadStringDefault("movefile", foldername, "").Trim() + "\\" + new FileInfo(((MyListBoxItemRemind)reminderlistSelectedItem).link.Replace("file:/", "")).Name);
                    searchword.Text = "";
                    NewFiles();
                    yixiaozi.Model.DocearReminder.StationInfo.NodeData = null;
                }
            }
            else if (searchword.Text.StartsWith("showlog"))
            {
                searchword.Text = "";
                showlog();
            }
            else if (searchword.Text.StartsWith("fenge"))
            {
                searchword.Text = "";
                showfenge = !showfenge;
            }
            else if (searchword.Text.StartsWith("allnode"))
            {
                searchword.Text = "";
                GetAllNodeJsonFile();
            }
            else if (searchword.Text.StartsWith("links"))
            {
                searchword.Text = "";
                GetIniFile();
            }
            else if (searchword.Text.StartsWith("delayall"))
            {
                searchword.Text = "";
                DelayAllTask(null, null);
                isneedKeyUpEventWork = false;
            }
            else if (searchword.Text.ToLower().StartsWith("mindmaps"))
            {
                searchword.Text = "";
                //多线程调用createsuggest_fun,从而加快反应速度
                Thread thread = new Thread(new ThreadStart(Tools.createsuggest_fun));
                thread.Start();

                DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
                {
                    if (file.Name.StartsWith("`"))
                    {
                        continue;
                    }
                    //如果原来没有再添加进去
                    if (mindmapfiles.FirstOrDefault(m => m.filePath == file.FullName) == null)
                    {
                        mindmapfiles.Add(new mindmapfile { name = file.Name.Substring(0, file.Name.Length - 3), filePath = file.FullName });
                    }
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
                                //如果路径包含根目录，则不再循环，这里只检查根目录以外的目录
                                if (!ini.ReadString("path", item, "").Contains(ini.ReadString("path", "rootpath", "")))
                                {
                                    DirectoryInfo pathout = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", item, "")));
                                    foreach (FileInfo file in pathout.GetFiles("*.mm", SearchOption.AllDirectories))
                                    {
                                        if (mindmapfiles.FirstOrDefault(m => m.filePath == file.FullName) == null)
                                        {
                                            mindmapfiles.Add(new mindmapfile { name = file.Name.Substring(0, file.Name.Length - 3), filePath = file.FullName });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (searchword.Text.ToLower().StartsWith("remindmaps"))
            {
                searchword.Text = "";
                remindmapsList.Clear();
                DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
                {
                    string text = System.IO.File.ReadAllText(file.FullName);
                    if (text.Contains(@"TimeManagementReminder.xml"))
                    {
                        remindmapsList.Add(file.FullName);
                    }
                }
                new TextListConverter().WriteListToTextFile(remindmapsList, System.AppDomain.CurrentDomain.BaseDirectory + @"\remindmaps.txt");
            }
            else if (searchword.Text.ToLower().StartsWith("tool"))
            {
                searchword.Text = "";
                int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
                int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
                this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
                MyHide();         //窗体的起始位置为(x,y)
                Tools menu = new Tools();
                menu.ShowDialog();
            }
            else if (searchword.Text.ToLower().StartsWith("ok"))
            {
                taskComplete_btn_Click(null, null);
                searchword.Text = "";
                reminderList.Focus();
            }
            else if (searchword.Text.ToLower().StartsWith("!") || searchword.Text.ToLower().StartsWith("！"))
            {
                isSearchFileOrNode = true;
                reminderList.Items.Clear();
                DateTime time = DateTime.Now;
                for (int i = OpenedInRootSearch.Count - 1; i > -1; i--)
                {
                    string item = OpenedInRootSearch[i];
                    time = time.AddDays(1);
                    try
                    {
                        reminderList.Items.Add(new MyListBoxItemRemind
                        {
                            Text = item.Split('|')[0],
                            Name = item.Split('|')[0],
                            Value = item.Split('|')[1],
                            Time = time,
                            isTask = false
                        });
                    }
                    catch (Exception ex)
                    {
                    }
                }
                reminderList.Sorted = false;
                reminderList.Sorted = true;
            }
            else
            {
                if (searchword.Text == "" && isSearchFileOrNode)
                {
                    isSearchFileOrNode = false;
                    //重新进入导图模式
                    searchword.Text = "";
                    UsedLogRenew();
                    Load_Click(null, null);
                    reminderList.Focus();
                }
                else
                {

                }
            }
        }
        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            encryptbutton_Click(null, null);
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
        private void Hiddenmenu_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Searchword_Enter(object sender, EventArgs e)
        {
            PlaySimpleSound("input");
            SwitchToLanguageMode("zh-CN");
        }
        private void Searchword_Leave(object sender, EventArgs e)
        {
            SwitchToLanguageMode();
        }

        private void mindmaplist_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (setmindmapcheck)
            {
                return;
            }
            if (searchword.Text.Contains("*"))
            {
                for (int i = 0; i < mindmaplist.Items.Count; i++)
                {
                    if (mindmaplist.CheckedItems.IndexOf(mindmaplist.Items[i]) == -1)
                    {
                        for (int k = reminderList.Items.Count - 1; k > 0; k--)
                        {
                            if (((MyListBoxItemRemind)reminderList.Items[k]).Value == ((MyListBoxItem)mindmaplist.Items[i]).Value)
                            {
                                reminderList.Items.RemoveAt(k);
                            }
                        }
                    }
                }
            }
            else
            {
                Updateunchkeckmindmap();
            }
        }
        public void Updateunchkeckmindmap()
        {
            if (isRefreshMindmap)
            {
                return;
            }
            if (setmindmapcheck)
            {
                return;
            }
            for (int i = 0; i < mindmaplist.Items.Count; i++)
            {
                if (((MyListBoxItem)mindmaplist.Items[i]).Value == "")
                {
                    continue;
                }
                if (mindmaplist.CheckedItems.IndexOf(mindmaplist.Items[i]) == -1)
                {
                    if (!unchkeckmindmap.Contains(((MyListBoxItem)mindmaplist.Items[i]).Value))
                    {
                        unchkeckmindmap.Add(((MyListBoxItem)mindmaplist.Items[i]).Value);
                        new TextListConverter().WriteListToTextFile(unchkeckmindmap, System.AppDomain.CurrentDomain.BaseDirectory + @"\unchkeckmindmap.txt");
                    }
                }
                else
                {
                    while (unchkeckmindmap.Contains(((MyListBoxItem)mindmaplist.Items[i]).Value))
                    {
                        unchkeckmindmap.Remove(((MyListBoxItem)mindmaplist.Items[i]).Value);
                        new TextListConverter().WriteListToTextFile(unchkeckmindmap, System.AppDomain.CurrentDomain.BaseDirectory + @"\unchkeckmindmap.txt");
                    }
                }
            }
        }
        private void RichSubTest_Enter(object sender, EventArgs e)
        {
        }
        private void RichSubTest_Leave(object sender, EventArgs e)
        {
        }
        private void RichSubTest_MouseHover(object sender, EventArgs e)
        {
        }
        private void RichSubTest_MouseLeave(object sender, EventArgs e)
        {
        }
        private void Panel4_Paint(object sender, PaintEventArgs e)
        {
        }
        public void Jietu()
        {
            //截图
            Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
            g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
                                                                                           //g.CopyFromScreen(panel游戏区 .PointToScreen(Point.Empty), Point.Empty, panel游戏区.Size);//只保存某个控件（这里是panel游戏区）
            try
            {
                bit.Save(CalendarImagePath + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好
            }
            catch (Exception ex)
            {
            }
        }
        public void pingmu()
        {
            //截图
            Bitmap bit = captureScreen();//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);//保存整个窗体为图片
                                                                           //g.CopyFromScreen(panel游戏区 .PointToScreen(Point.Empty), Point.Empty, panel游戏区.Size);//只保存某个控件（这里是panel游戏区）
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "\\CaptureScreen\\"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "\\CaptureScreen\\");
                }
                string picName = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\CaptureScreen\\" + DateTime.Now.ToString("yyMMddHHmmss") + ".png";

                picName = picName.Replace("\\\\", "\\");
                if (File.Exists(picName))
                {
                    File.Delete(picName);
                }
                bit.Save(picName);
                //CompressImage(picName, picName.Replace("A.png", ".png"),100,50);
                //File.Delete(picName);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>  
        ///  抓取整个屏幕  
        /// </summary>  
        /// <returns></returns>  
        public static Bitmap captureScreen()
        {
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            return captureScreen(0, 0, screenSize.Width, screenSize.Height);
        }
        /// <summary>  
        /// 抓取屏幕(层叠的窗口)  
        /// </summary>  
        /// <param name="x">左上角的横坐标</param>  
        /// <param name="y">左上角的纵坐标</param>  
        /// <param name="width">抓取宽度</param>  
        /// <param name="height">抓取高度</param>  
        /// <returns></returns>  
        public static Bitmap captureScreen(int x, int y, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(new System.Drawing.Point(x, y), new System.Drawing.Point(0, 0), bmp.Size);
                g.Dispose();
            }
            //bit.Save(@"capture2.png");  
            return bmp;
        }
        bool needSuggest = true;
        private void Searchword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                SearchText_suggest.Visible = false;
                return;
            }
            if (searchword.Text.ToLower().StartsWith("ss"))
            {
                return;
            }
            if (searchword.Text.ToLower().StartsWith("sjj")) //startjj
            {
                searchword.Text = "";
                MyHide();
                keyJ.Start();
                return;
            }
            if (!isRename && !isRenameTimeBlock)//重命名的时候，可以移动光标
            {
                searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
            }
            if (searchword.Text.Length < 2 && !searchword.Text.StartsWith("@"))
            {
                needSuggest = true;
                SearchText_suggest.Visible = false;
                //ShowSubNode();
                return;
            }
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Space)
            {
                if (e.KeyCode == Keys.Back && e.Modifiers.CompareTo(Keys.Control) == 0)
                {
                    searchword.Text = "";
                    return;
                }
                else
                {
                    needSuggest = true;
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (SearchText_suggest.SelectedIndex > 0)
                {
                    SearchText_suggest.SelectedIndex--;
                }
                else if (SearchText_suggest.SelectedIndex == 0)
                {
                    SearchText_suggest.SelectedIndex = SearchText_suggest.Items.Count - 1;
                }
                else
                {
                    try
                    {
                        SearchText_suggest.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                    }
                }

                return;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (SearchText_suggest.SelectedIndex < SearchText_suggest.Items.Count - 1)
                {
                    SearchText_suggest.SelectedIndex++;
                }
                else
                {
                    try
                    {
                        SearchText_suggest.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                    }
                }

                return;
            }
            if (searchword.Text != "" && (searchword.Text.ToLower().StartsWith("t") || searchword.Text.ToLower().StartsWith("刚刚") || searchword.Text.ToLower().EndsWith("刚刚") || searchword.Text.ToLower().Contains("刚刚@") || searchword.Text.ToLower().Contains(" @") || (showTimeBlock.Checked && !searchword.Text.StartsWith(" "))) && searchword.Text.Contains("@"))//选择时间块,如果开始是空格，就不认为是时间块状态
            {
                string taskname = "";
                string type = "";
                if (searchword.Text.ToLower().StartsWith("t"))
                {
                    type = "T";
                    taskname = searchword.Text.Split('@')[0].Substring(1);
                }
                if (searchword.Text.ToLower().StartsWith("刚刚") || searchword.Text.ToLower().EndsWith("刚刚") || searchword.Text.ToLower().Contains("刚刚@") || searchword.Text.ToLower().Contains(" @") || showTimeBlock.Checked)
                {
                    type = "刚刚";
                    taskname = searchword.Text.Split('@')[0].Replace("刚刚", "").Replace("刚刚", "").Replace("刚刚", "").Replace("刚刚", "").Replace(" ", "");
                }
                if (searchword.SelectionStart < searchword.Text.Length)
                {
                    return;
                }
                string filename = searchword.Text.Split('@')[1];
                searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
                //if (SearchText_suggest.SelectedItem != null && filename == (SearchText_suggest.SelectedItem as StationInfo).StationName_CN)
                //{
                //    SearchText_suggest.Visible = false;
                //    return;
                //}
                if (e.KeyCode == Keys.Enter)
                {
                    StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                    if (info.link != null && info.link != "")
                    {
                        searchword.Text = "";
                        searchword.Focus();
                    }
                    info.StationName_CN = info.StationName_CN.Replace("★", "");
                    searchword.Text = type + taskname + "@" + info.StationName_CN;
                    timeblockcolor = info.link;
                    timeblockfather = info.mindmapurl;
                    mindmapornode.Text = info.mindmapurl.Split('\\')[info.mindmapurl.Split('\\').Length - 1] + ">" + info.StationName_CN;
                    showMindmapName = info.mindmapurl;
                    renameMindMapFileID = info.nodeID;
                    SearchText_suggest.Visible = false;
                    searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
                    if (!TimeBlockSelected.Any(m => m.Contains(info.nodeID)))
                    {
                        TimeBlockSelected.Add(info.StationName_CN + "|" + info.StationName_EN + "|" + info.StationName_JX + "|" + info.nodeID + "|" + info.mindmapurl + "|" + info.fatherNodePath + "|" + info.link);
                    }
                    else
                    {
                        TimeBlockSelected.RemoveAll(m => m.Contains(info.nodeID));
                        TimeBlockSelected.Add(info.StationName_CN + "|" + info.StationName_EN + "|" + info.StationName_JX + "|" + info.nodeID + "|" + info.mindmapurl + "|" + info.fatherNodePath + "|" + info.link);
                    }
                    new TextListConverter().WriteListToTextFile(TimeBlockSelected, System.AppDomain.CurrentDomain.BaseDirectory + @"\TimeBlockSelected.txt");
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                    if (TimeBlockSelected.Any(m => m.Contains(info.nodeID)))
                    {
                        TimeBlockSelected.RemoveAll(m => m.Contains(info.nodeID));
                        new TextListConverter().WriteListToTextFile(TimeBlockSelected, System.AppDomain.CurrentDomain.BaseDirectory + @"\TimeBlockSelected.txt");
                    }
                }
                else
                {
                    if (filename != "")
                    {
                        IList<StationInfo> dataSource = StationInfo.GetTimeBlock(filename.Trim());
                        //处理建议，去掉重复（重复的没有意义），曾经选择过的排列在上面
                        for (int i = TimeBlockSelected.Count - 1; i > -1; i--)
                        {
                            if (dataSource.Count(m => m.StationName_CN == TimeBlockSelected[i].Split('|')[0]) > 0)
                            {
                                int index = dataSource.IndexOf(dataSource.FirstOrDefault(m => m.StationName_CN == TimeBlockSelected[i].Split('|')[0]));
                                dataSource = Swap(dataSource, index);
                                dataSource[0].StationName_CN = "★" + dataSource[0].StationName_CN;
                            }
                        }
                        foreach (StationInfo item in dataSource.Where(m => m.StationName_CN.Length > 50))
                        {
                            item.StationName_CN = item.StationName_CN.Substring(0, 50);
                        }
                        if (dataSource.Count > 0)
                        {
                            SearchText_suggest.DataSource = dataSource;
                            SearchText_suggest.DisplayMember = "StationName_CN";
                            SearchText_suggest.ValueMember = "StationValue";
                            SearchText_suggest.Visible = true;
                        }
                        else
                        {
                            SearchText_suggest.Visible = false;
                        }
                    }
                    else
                    {
                        List<StationInfo> result = new List<StationInfo>();
                        for (int i = TimeBlockSelected.Count - 1; i > -1; i--)
                        {
                            try
                            {
                                result.Add(new StationInfo() { StationName_CN = TimeBlockSelected[i].Split('|')[0], StationName_EN = TimeBlockSelected[i].Split('|')[1], StationName_JX = TimeBlockSelected[i].Split('|')[2], nodeID = TimeBlockSelected[i].Split('|')[3], mindmapurl = TimeBlockSelected[i].Split('|')[4], fatherNodePath = TimeBlockSelected[i].Split('|')[5], link = TimeBlockSelected[i].Split('|')[6] });

                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        SearchText_suggest.DataSource = result;
                        SearchText_suggest.DisplayMember = "StationName_CN";
                        SearchText_suggest.ValueMember = "StationValue";
                        SearchText_suggest.Visible = true;
                    }
                }
                if (SearchText_suggest.Visible)
                {
                    try
                    {
                        if (SearchText_suggest.SelectedItem != null)
                        {
                            mindmapSearch.Text = Path.GetFileName(((StationInfo)SearchText_suggest.SelectedItem).mindmapurl);
                        }
                        else
                        {
                            mindmapSearch.Text = "";
                        }

                    }
                    catch (Exception ex)
                    {
                        mindmapSearch.Text = "";
                    }
                }
                SearchText_suggest.Height = SearchText_suggest.PreferredHeight; //12 * SearchText_suggest.Items.Count + 10;
            }
            else if (searchword.Text != "" && searchword.Text.Contains("@@"))
            {
                string taskname = searchword.Text.Split('@')[0];
                if (searchword.SelectionStart < taskname.Length)
                {
                    return;
                }
                string filename = searchword.Text.Split('@')[2];
                searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
                if (SearchText_suggest.SelectedItem != null && filename == (SearchText_suggest.SelectedItem as StationInfo).StationName_CN)
                {
                    SearchText_suggest.Visible = false;
                    return;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                    if (info.link != null && info.link != "")
                    {
                        Clipboard.SetText(info.link);
                        searchword.Text = "";
                        searchword.Focus();
                    }
                    info.StationName_CN = info.StationName_CN.Replace("★", "");
                    searchword.Text = taskname + "@@" + info.StationName_CN;
                    mindmapornode.Text = info.mindmapurl.Split('\\')[info.mindmapurl.Split('\\').Length - 1] + ">" + info.StationName_CN;
                    showMindmapName = info.mindmapurl;
                    renameMindMapFileID = info.nodeID;
                    SearchText_suggest.Visible = false;
                    searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
                    if (!IconNodesSelected.Any(m => m.Contains(info.nodeID)))
                    {
                        IconNodesSelected.Add(info.StationName_CN + "|" + info.StationName_EN + "|" + info.StationName_JX + "|" + info.nodeID + "|" + info.mindmapurl + "|" + info.fatherNodePath);
                    }
                    else
                    {
                        IconNodesSelected.RemoveAll(m => m.Contains(info.nodeID));
                        IconNodesSelected.Add(info.StationName_CN + "|" + info.StationName_EN + "|" + info.StationName_JX + "|" + info.nodeID + "|" + info.mindmapurl + "|" + info.fatherNodePath + "|" + info.link);
                    }
                    new TextListConverter().WriteListToTextFile(IconNodesSelected, System.AppDomain.CurrentDomain.BaseDirectory + @"\IconNodesSelected.txt");
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (filename == "")
                    {
                        StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                        if (IconNodesSelected.Any(m => m.Contains(info.nodeID)))
                        {
                            IconNodesSelected.RemoveAll(m => m.Contains(info.nodeID));
                            new TextListConverter().WriteListToTextFile(IconNodesSelected, System.AppDomain.CurrentDomain.BaseDirectory + @"\IconNodesSelected.txt");
                        }
                    }
                    else
                    {
                        StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                        ignoreSuggest.Add(info.StationName_CN);
                        new TextListConverter().WriteListToTextFile(ignoreSuggest, System.AppDomain.CurrentDomain.BaseDirectory + @"\ignoreSuggest.txt");
                    }
                }
                else
                {
                    if (filename != "")
                    {
                        IList<StationInfo> dataSource = StationInfo.GetNodes(filename.Trim());
                        //处理建议，去掉重复（重复的没有意义），曾经选择过的排列在上面
                        for (int i = IconNodesSelected.Count - 1; i > -1; i--)
                        {
                            if (dataSource.Count(m => m.StationName_CN == IconNodesSelected[i].Split('|')[0]) > 0)
                            {
                                int index = dataSource.IndexOf(dataSource.FirstOrDefault(m => m.StationName_CN == IconNodesSelected[i].Split('|')[0]));
                                dataSource = Swap(dataSource, index);
                                dataSource[0].StationName_CN = "★" + dataSource[0].StationName_CN;
                            }
                        }
                        foreach (StationInfo item in dataSource.Where(m => m.StationName_CN.Length > 50))
                        {
                            item.StationName_CN = item.StationName_CN.Substring(0, 50);
                        }
                        if (dataSource.Count > 0)
                        {
                            SearchText_suggest.DataSource = dataSource;
                            SearchText_suggest.DisplayMember = "StationName_CN";
                            SearchText_suggest.ValueMember = "StationValue";
                            SearchText_suggest.Visible = true;
                        }
                        else
                        {
                            SearchText_suggest.Visible = false;
                        }
                    }
                    else
                    {
                        List<string> dd = IconNodesSelected;
                        //显示之前选过的
                        List<StationInfo> result = new List<StationInfo>();
                        for (int i = IconNodesSelected.Count - 1; i > -1; i--)
                        {
                            try
                            {
                                if (IconNodesSelected[i].Split('|').Length <= 5)
                                {
                                    result.Add(new StationInfo() { StationName_CN = IconNodesSelected[i].Split('|')[0], StationName_EN = IconNodesSelected[i].Split('|')[1], StationName_JX = IconNodesSelected[i].Split('|')[2], nodeID = IconNodesSelected[i].Split('|')[3], mindmapurl = IconNodesSelected[i].Split('|')[4] });
                                }
                                else if (IconNodesSelected[i].Split('|').Length == 6)
                                {
                                    result.Add(new StationInfo() { StationName_CN = IconNodesSelected[i].Split('|')[0], StationName_EN = IconNodesSelected[i].Split('|')[1], StationName_JX = IconNodesSelected[i].Split('|')[2], nodeID = IconNodesSelected[i].Split('|')[3], mindmapurl = IconNodesSelected[i].Split('|')[4], fatherNodePath = IconNodesSelected[i].Split('|')[5] });
                                }
                                else
                                {
                                    result.Add(new StationInfo() { StationName_CN = IconNodesSelected[i].Split('|')[0], StationName_EN = IconNodesSelected[i].Split('|')[1], StationName_JX = IconNodesSelected[i].Split('|')[2], nodeID = IconNodesSelected[i].Split('|')[3], mindmapurl = IconNodesSelected[i].Split('|')[4], fatherNodePath = IconNodesSelected[i].Split('|')[5], link = IconNodesSelected[i].Split('|')[6] });
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        SearchText_suggest.DataSource = result;
                        SearchText_suggest.DisplayMember = "StationName_CN";
                        SearchText_suggest.ValueMember = "StationValue";
                        SearchText_suggest.Visible = true;
                    }
                }
                if (SearchText_suggest.Visible)
                {
                    try
                    {
                        if (SearchText_suggest.SelectedItem != null)
                        {
                            mindmapSearch.Text = Path.GetFileName(((StationInfo)SearchText_suggest.SelectedItem).mindmapurl);
                        }
                        else
                        {
                            mindmapSearch.Text = "";
                        }

                    }
                    catch (Exception ex)
                    {
                        mindmapSearch.Text = "";
                    }
                }
                SearchText_suggest.Height = SearchText_suggest.PreferredHeight; //12 * SearchText_suggest.Items.Count + 10;
            }
            else if (searchword.Text != "" && searchword.Text.Contains("@"))
            {
                string taskname = searchword.Text.Split('@')[0];
                if (searchword.SelectionStart < taskname.Length)
                {
                    return;
                }
                string filename = searchword.Text.Split('@')[1];
                if (filename != "" && command.Contains(filename))
                {
                    SearchText_suggest.Visible = false;
                    return;
                }
                searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
                                                              //if (SearchText_suggest.SelectedItem != null && filename == (SearchText_suggest.SelectedItem as StationInfo).StationName_CN)会导致如果一样就没办法选别的
                                                              //{
                                                              //    SearchText_suggest.Visible = false;
                                                              //    return;
                                                              //}

                if (e.KeyCode == Keys.Enter)
                {
                    StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                    info.StationName_CN = info.StationName_CN.Replace("★", "");
                    if (command.Contains(info.StationName_CN))
                    {
                        searchword.Text = info.StationName_CN;
                    }
                    else
                    {
                        searchword.Text = taskname + "@" + info.StationName_CN;
                    }
                    SearchText_suggest.Visible = false;
                    searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
                    if (!RecentOpenedMap.Contains(info.StationName_CN))//放到这里也可以放到最终也可以暂时放这里
                    {
                        RecentOpenedMap.Add(info.StationName_CN);
                    }
                    else
                    {
                        RecentOpenedMap.Remove(info.StationName_CN);
                        RecentOpenedMap.Add(info.StationName_CN);
                    }
                    new TextListConverter().WriteListToTextFile(RecentOpenedMap, System.AppDomain.CurrentDomain.BaseDirectory + @"\RecentOpenedMap.txt");
                    if (command.Contains(info.StationName_CN))
                    {
                        SendKeys.Send("{ENTER}");
                    }
                    return;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (filename == "")
                    {
                        StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                        if (!RecentOpenedMap.Contains(info.StationName_CN))
                        {
                        }
                        else
                        {
                            RecentOpenedMap.Remove(info.StationName_CN);
                        }
                        new TextListConverter().WriteListToTextFile(RecentOpenedMap, System.AppDomain.CurrentDomain.BaseDirectory + @"\RecentOpenedMap.txt");
                    }
                    else
                    {
                        StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                        ignoreSuggest.Add(info.StationName_CN);
                        new TextListConverter().WriteListToTextFile(ignoreSuggest, System.AppDomain.CurrentDomain.BaseDirectory + @"\ignoreSuggest.txt");
                    }
                }
                else
                {
                    if (filename != "")
                    {
                        IList<StationInfo> dataSource = StationInfo.GetStations(filename.Trim());
                        //处理建议，去掉重复（重复的没有意义），曾经选择过的排列在上面
                        for (int i = RecentOpenedMap.Count - 1; i > -1; i--)
                        {
                            if (dataSource.Count(m => m.StationName_CN == RecentOpenedMap[i]) > 0)
                            {
                                int index = dataSource.IndexOf(dataSource.FirstOrDefault(m => m.StationName_CN == RecentOpenedMap[i]));
                                dataSource = Swap(dataSource, index);
                                dataSource[0].StationName_CN = "★" + dataSource[0].StationName_CN;
                            }
                        }
                        if (dataSource.Count > 0)
                        {
                            SearchText_suggest.DataSource = dataSource;
                            SearchText_suggest.DisplayMember = "StationName_CN";
                            SearchText_suggest.ValueMember = "StationValue";
                            SearchText_suggest.Visible = true;
                        }
                        else
                        {
                            SearchText_suggest.Visible = false;
                        }
                    }
                    else
                    {
                        List<string> dd = RecentOpenedMap;
                        //显示之前选过的
                        List<StationInfo> result = new List<StationInfo>();
                        for (int i = RecentOpenedMap.Count - 1; i > -1; i--)
                        {
                            result.Add(new StationInfo() { StationName_CN = RecentOpenedMap[i] });
                        }
                        SearchText_suggest.DataSource = result;
                        SearchText_suggest.DisplayMember = "StationName_CN";
                        SearchText_suggest.ValueMember = "StationValue";
                        SearchText_suggest.Visible = true;
                    }
                }
                SearchText_suggest.Height = SearchText_suggest.PreferredHeight; //12 * SearchText_suggest.Items.Count + 10;
            }
            else if (needSuggest && (e.KeyCode == Keys.Menu || e.KeyCode == Keys.ControlKey || (e.KeyCode == Keys.Space && searchword.Text.EndsWith(" ")) || e.KeyCode == Keys.Enter))
            {
                List<StationInfo> result = suggestListData.FindAll(m => m.StationName_CN.ToLower().Contains(searchword.Text.Replace(",", " ").ToLower()) || (searchword.Text.Replace(",", " ").Contains(' ') && StringHasArrALL(m.StationName_CN.ToLower() + m.mindmapurl.ToLower(), searchword.Text.Replace(",", " ").ToLower().Split(' '))) || (m.isNode == "" && searchword.Text.Contains(' ') && StringHasArrALL(m.StationName_CN.ToLower() + m.mindmapurl.ToLower(), searchword.Text.Replace(",", " ").ToLower().Split(' '))));
                if (result.Count() > 0)
                {
                    string taskname = searchword.Text;
                    if (e.KeyCode == Keys.Enter)
                    {
                        StationInfo info = SearchText_suggest.SelectedItem as StationInfo;
                        SearchText_suggest.Visible = false;
                        searchword.Select(searchword.Text.Length, 1); //光标定位到文本框最后
                        richTextSubNode.Clear();
                        richTextSubNode.Height = 0;
                        try
                        {
                            Process.Start(info.mindmapurl);
                            SaveLog("打开：    " + info.mindmapurl);
                        }
                        catch (Exception ex)
                        {
                        }
                        if (!QuickOpenLog.Contains(info.StationName_CN))//放到这里也可以放到最终也可以暂时放这里
                        {
                            QuickOpenLog.Add(info.StationName_CN);
                        }
                        else
                        {
                            QuickOpenLog.Remove(info.StationName_CN);
                            QuickOpenLog.Add(info.StationName_CN);
                        }
                        new TextListConverter().WriteListToTextFile(QuickOpenLog, System.AppDomain.CurrentDomain.BaseDirectory + @"\QuickOpenLog.txt");
                        if (command.Contains(info.StationName_CN))
                        {
                            SendKeys.Send("{ENTER}");
                        }
                        searchword.Text = "";
                        SearchText_suggest.Visible = false;
                        MyHide();
                        return;
                    }
                    else
                    {
                        SearchText_suggest.DataSource = result;
                        SearchText_suggest.DisplayMember = "StationName_CN";
                        SearchText_suggest.ValueMember = "mindmapurl";
                        SearchText_suggest.Visible = true;
                        SearchText_suggest.Height = SearchText_suggest.PreferredHeight;// 12 * SearchText_suggest.Items.Count + 10;
                    }
                }
                else
                {
                    needSuggest = false;
                    SearchText_suggest.Visible = false;
                    ShowSubNode();
                }
            }
            else
            {
                SearchText_suggest.Visible = false;
                ShowSubNode();
            }
        }
        private void mindmapSearch_TextChanged(object sender, EventArgs e)
        {
            if (SearchText_suggest.Visible)
            {
                return;
            }
            if (mindmapSearch.Text == "")
            {
                isRefreshMindmap = true;
                mindmaplist.Items.Clear();
                foreach (var item in mindmaplist_all)
                {
                    mindmaplist.Items.Add(item);
                }
                mindmaplist.Sorted = false;
                mindmaplist.Sorted = true;
                for (int i = 0; i < mindmaplist.Items.Count; i++)
                {
                    setmindmapcheck = true;
                    mindmaplist.SetItemChecked(i, true);
                    string file = ((MyListBoxItem)mindmaplist.Items[i]).Value;
                    if (unchkeckmindmap.Contains(file))
                    {
                        mindmaplist.SetItemChecked(i, false);
                        mindmaplist.Refresh();
                    }
                    setmindmapcheck = false;
                }
                isRefreshMindmap = false;
                return;
            }
            for (int i = mindmaplist.Items.Count - 1; i >= 0; i--)
            {
                if (!((MyListBoxItem)mindmaplist.Items[i]).Text.Contains(mindmapSearch.Text))
                {
                    mindmaplist.Items.RemoveAt(i);
                }
            }
        }
        private static IList<StationInfo> Swap<StationInfo>(IList<StationInfo> list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
            return list;
        }
        private static IList<StationInfo> Swap<StationInfo>(IList<StationInfo> list, int index1)
        {
            var temp = list[index1];
            list.RemoveAt(index1);
            list.Insert(0, temp);
            return list;
        }
        private void onlyZhouqi_CheckedChanged(object sender, EventArgs e)
        {
            RRReminderlist();
        }
        private void AddClip_panel_Paint(object sender, PaintEventArgs e)
        {
        }
        private void TreeView1_ParentChanged(object sender, EventArgs e)
        {
        }
        private void TreeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, Rectangle.Inflate(e.Node.Bounds, 4, 0));
                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null)
                {
                    nodeFont = ((TreeView)sender).Font;
                }
                if (e.Node.Text.Length > 50)
                {
                    e.Node.Text = e.Node.Text.Substring(0, 50);
                }
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Gray, Rectangle.Inflate(e.Node.Bounds, 4, 0));
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        private void TreeView1_MouseHover(object sender, EventArgs e)
        {
            //nodetree.Focus();
        }
        private void TreeView1_MouseLeave(object sender, EventArgs e)
        {
            //reminderlist.Focus();
        }
        // Create a node sorter that implements the IComparer interface.
        public class NodeSorter : IComparer
        {
            // Compare the length of the strings, or the strings
            // themselves, if they are the same length.
            public int Compare(object x, object y)
            {
                TreeNode tx = x as TreeNode;
                TreeNode ty = y as TreeNode;
                // Compare the length of the strings, returning the difference.
                if (tx.Text.Length != ty.Text.Length)
                {
                    return tx.Text.Length - ty.Text.Length;
                }
                // If they are the same length, call Compare.
                return string.Compare(tx.Text, ty.Text);
            }
        }
        private void panel5_Click(object sender, EventArgs e)
        {
            //下面窗口设置一下
            nodetree.Top = FileTreeView.Top = nodetreeTop;
            nodetree.Height = FileTreeView.Height = nodetreeHeight;
            this.Height = normalheight; showMindmapName = "";
            reminderList.Focus();
            //Center();
        }
        private void RsscheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void Rsstimer_Tick(object sender, EventArgs e)
        {
            //if (rssrenewend)
            //{
            //    rssrenewend = false;
            //    Thread th = new Thread(() => RSSRenew());
            //    th.Start();
            //}
        }
        public string GetRSSURL(string url)
        {
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(url);
            foreach (XmlNode item in x.GetElementsByTagName("node"))
            {
                if (item.Attributes["ISURL"] != null)
                {
                    return item.Attributes["TEXT"].InnerText;
                }
            }
            return "";
        }
        #region 字符串，XML，List帮助方法
        public bool ISHasInDoc(string file, string str)
        {
            String strFile = File.ReadAllText(file);
            return strFile.Contains(yixiaozi.Model.DocearReminder.Helper.ConvertString(str));

        }
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        private void PlaySimpleSound(string type)
        {
            try
            {
                //SystemSounds.Exclamation.Play();
                //return;
                if (!isPlaySound || type == "")
                {
                    return;
                }
                string path = ini.ReadString("sound", "path", "");
                path += ini.ReadString("sound", type, "");
                //simpleSound.SoundLocation = path;
                ////MediaPlayer simpleSound = new MediaPlayer()
                ////{
                ////    Volume = 100.0f
                ////};
                ////simpleSound.Open(new Uri(new FileInfo(path).FullName));
                ////simpleSound.Play();
                //simpleSound.Load();

                //simpleSound.Play();

                player.SoundLocation = path;
                player.Play();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
        public bool MyContains(string str, string[] arr)
        {
            if (str == null || str == "" || arr.Length == 0)
            {
                return true;
            }
            str = str.ToLower();
            foreach (string item in arr)
            {
                if (str.ToLower().Contains("\\" + item.ToLower().Trim() + "\\"))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool MyContains(string str, string arr)
        {
            if (arr == null || arr == "")
            {
                return true;
            }
            string[] arrr = arr.Replace("  ", " ").Split(' ');
            if (str == null || str == "" || arrr.Length == 0)
            {
                return false;
            }
            str = str.ToLower();
            foreach (string item in arrr)
            {
                if (str.ToLower().Contains(item.ToLower().Trim()))
                {
                    return true;
                }
            }
            return false;
        }
        public bool StringHasArrALL(string str, string[] arr)
        {
            if (str == null || str == "" || arr.Length == 0)
            {
                return false;
            }
            str = str.ToLower();
            foreach (string item in arr)
            {
                if (item.ToLower().StartsWith("e"))
                {
                    if (item.ToLower().Trim() == "e")
                    {
                        return true;
                    }
                    if (str.Contains(item.ToLower().Trim().Substring(1)))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!str.ToLower().Contains(item.ToLower().Trim()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion
        #region RSS
        public void RSSRenew()
        {
            DirectoryInfo path = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rss", "")));
            foreach (FileInfo file in path.GetFiles("*.mm", SearchOption.AllDirectories))
            {
                try
                {
                    string rss = GetRSSURL(file.FullName);
                    if (rss == "" || !IsUri(rss))
                    {
                        continue;
                    }
                    XmlDocument doc = new XmlDocument(); // 创建文档对象
                    WebClient webClient = new WebClient();
                    webClient.Headers.Add("user-agent", "MyRSSReader/1.0");
                    XmlReader readers = XmlReader.Create(webClient.OpenRead(rss));
                    try
                    {
                        doc.Load(readers);//加载XML 包括HTTP：// 和本地
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);//异常处理
                    }
                    string fileName = file.FullName;
                    XmlNodeList list = doc.GetElementsByTagName("item");  // 获得项           
                    System.Xml.XmlDocument x = new XmlDocument();
                    x.Load(fileName);
                    foreach (XmlNode node in list)  // 循环每一项
                    {
                        XmlElement ele = (XmlElement)node;
                        string title = ele.GetElementsByTagName("title")[0].InnerText;//获得标题
                        string link = ele.GetElementsByTagName("link")[0].InnerText;//获得联接
                        string description = ele.GetElementsByTagName("description")[0].InnerText;//获得联接
                        string guidurl = ele.GetElementsByTagName("guid").Count == 0 ? "" : ele.GetElementsByTagName("guid")[0].InnerText;//获得联接
                        if (guidurl != "" && ISHasInDoc(fileName, guidurl))
                        {
                            continue;
                        }
                        if (title != "" && ISHasInDoc(fileName, title))
                        {
                            continue;
                        }
                        if (link != "" && ISHasInDoc(fileName, link))
                        {
                            continue;
                        }
                        DateTime dt = DateTime.Now;
                        try
                        {
                            dt = Convert.ToDateTime(((System.Xml.XmlElement)ele.PreviousSibling).InnerText);
                        }
                        catch (Exception ex)
                        {
                        }
                        AddTaskToFile(x, "文章", title, link, description, guidurl, dt);
                    }
                    x.Save(fileName);
                    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(fileName));
                    th.Start();
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion

        #region 理财
        public void AddMoney(string mindmapPath, string account, string money, bool addOrDel = false, string content = "")
        {
            if (mindmapPath == "" || account == "" || money == "")
            {
                return;
            }
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(mindmapPath);
            XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value.Contains(account + "："));
            double lastMoney = Convert.ToDouble(root.Attributes["TEXT"].Value.Split('：')[1]);
            if (addOrDel)
            {
                lastMoney += Convert.ToDouble(money);
            }
            else
            {
                lastMoney -= Convert.ToDouble(money);
            }
            root.Attributes["TEXT"].Value = account + "：" + lastMoney;
            x.Save(mindmapPath);
            AddTaskToFile(mindmapPath, "记录", content + "|" + money + "|" + account, false);
            richTextSubNode.Text += account + "：" + lastMoney;
            richTextSubNode.Text += "\r\n";
        }
        public void showMoneyLeft(string mindmapPath, string name)
        {
            double money = 0;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(mindmapPath);
            string nameaccount = ini.ReadString("money", name, "");
            foreach (string account in nameaccount.Split(';'))
            {
                XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value.Contains(account + "："));
                double lastMoney = Convert.ToDouble(root.Attributes["TEXT"].Value.Split('：')[1]);
                money += lastMoney;
            }
            XmlNode root1 = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value.Contains(ini.ReadString("money", name + "Name", "") + "："));
            root1.Attributes["TEXT"].Value = name + "：" + money;
            x.Save(mindmapPath);
            richTextSubNode.Text += name + "：" + money;
            richTextSubNode.Text += "\r\n";
        }
        #endregion
        #region 鼠标控制
        #endregion
        void SuggestText_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                //if the item state is selected them change the back color
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e = new DrawItemEventArgs(e.Graphics,
                                              e.Font,
                                              e.Bounds,
                                              e.Index,
                                              e.State ^ DrawItemState.Selected,
                                              e.ForeColor,
                                              Color.LightGray);//Choose the colorYellow
                }

                // Draw the background of the ListBox control for each item.
                e.DrawBackground();
                // Draw the current item text
                e.Graphics.DrawString(((StationInfo)SearchText_suggest.Items[e.Index]).StationName_CN, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
            }
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            //e.DrawFocusRectangle();
        }
        void Mindmaplist_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            //if the item state is selected them change the back color
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State, e.ForeColor, Color.LightGray);
            }
            e.DrawBackground();
            e.Graphics.DrawString(((StationInfo)SearchText_suggest.Items[e.Index]).StationName_CN, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }
        private void tree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                if (nodetree.Focused)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gray), e.Bounds);
                }
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Node.Bounds, e.Node.ForeColor);
        }
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void SearchText_suggest_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                richTextSubNode.Clear();
                if (SearchText_suggest.SelectedItem != null && ((StationInfo)SearchText_suggest.SelectedItem).mindmapurl != null)
                {
                    richTextSubNode.AppendText(((StationInfo)SearchText_suggest.SelectedItem).mindmapurl);
                    richTextSubNode.AppendText(Environment.NewLine);
                    richTextSubNode.AppendText(((StationInfo)SearchText_suggest.SelectedItem).fatherNodePath);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ebcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ebcheckBox.Checked)
            {
                showcyclereminder.Checked = onlyZhouqi.Checked = IsReminderOnlyCheckBox.Checked = false;
            }
            else
            {
                showcyclereminder.Checked = false;
                onlyZhouqi.Checked = true;
                IsReminderOnlyCheckBox.Checked = false;
            }
        }

        #region 文件树，导图树
        private void FileTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Node.Name);
            SaveLog("打开：    " + e.Node.Name);
        }
        bool IsFileNodeEdit = false;

        private void FileTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string newTxt = e.Label;//获取新文本
            string oldTxt = e.Node.Text;//获取原来的文本
            if (oldTxt != "")
            {
                if (newTxt != null && newTxt != oldTxt)
                {
                    FileInfo fi = new FileInfo(FileTreeView.SelectedNode.Name);
                    //新路径这样判断可能出错
                    //fi.MoveTo(FileTreeView.SelectedNode.Name.Replace(id,newTxt));
                    fi.MoveTo(fi.Directory.FullName + "\\" + newTxt);
                    FileTreeView.SelectedNode.Name = fi.Directory.FullName + "\\" + newTxt;
                    //todo需改导图里的地址
                }
            }
            else
            {
                string parentFolder = fileTreePath.FullName;
                if (FileTreeView.Nodes.Contains(FileTreeView.SelectedNode))
                {
                    parentFolder = fileTreePath.FullName;
                }
                else
                {
                    parentFolder = FileTreeView.SelectedNode.Parent.Name;

                }
                if (newTxt.EndsWith(".mm"))
                {
                    FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\default.mm");
                    fi.CopyTo(parentFolder + "\\" + newTxt);
                    FileTreeView.SelectedNode.Name = parentFolder + "\\" + newTxt;
                    StreamReader reader = new StreamReader(FileTreeView.SelectedNode.Name, Encoding.UTF8);
                    String a = reader.ReadToEnd();
                    reader.Close();
                    a = a.Replace("DefaultName", newTxt.Substring(0, newTxt.Length - 3));
                    StreamWriter readTxt = new StreamWriter(FileTreeView.SelectedNode.Name, false, Encoding.UTF8);
                    readTxt.Write(a);
                    readTxt.Flush();
                    readTxt.Close();
                    yixiaozi.Model.DocearReminder.Helper.ConvertFile(FileTreeView.SelectedNode.Name);
                }
                else if (newTxt.EndsWith(".xlsx"))
                {
                    FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\default.xlsx");
                    fi.CopyTo(parentFolder + "\\" + newTxt);
                    FileTreeView.SelectedNode.Name = parentFolder + "\\" + newTxt;
                }
                else if (newTxt.EndsWith(".docx"))
                {
                    FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\default.docx");
                    fi.CopyTo(parentFolder + "\\" + newTxt);
                    FileTreeView.SelectedNode.Name = parentFolder + "\\" + newTxt;
                }
                else if (newTxt.EndsWith(".pptx"))
                {
                    FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\default.pptx");
                    fi.CopyTo(parentFolder + "\\" + newTxt);
                    FileTreeView.SelectedNode.Name = parentFolder + "\\" + newTxt;
                }
                else if (newTxt.EndsWith(".txt"))
                {
                    FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\default.txt");
                    fi.CopyTo(parentFolder + "\\" + newTxt);
                    FileTreeView.SelectedNode.Name = parentFolder + "\\" + newTxt;
                }
                else//其他全部为文件夹
                {
                    newTxt = newTxt.Replace(".", "");
                    System.IO.Directory.CreateDirectory(parentFolder + "\\" + newTxt);
                    FileTreeView.SelectedNode.Name = parentFolder + "\\" + newTxt;
                }
            }
        }
        bool IsMindMapNodeEdit = false;
        private void NodeTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string newTxt = e.Label;//获取新文本
            string oldTxt = e.Node.Text;//获取原来的文本 
            if (oldTxt != "")
            {
                if (newTxt != null && newTxt != oldTxt)
                {
                    if (newTxt.Length >= 7 && newTxt[6] == ':')
                    {
                        newTxt = newTxt.Substring(7).Trim();
                    }
                    RenameNodeByID(newTxt);
                    fenshuADD(1);
                    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                    th.Start();
                    SaveLog("修改节点名称：" + oldTxt + "  To  " + newTxt);
                    return;
                }
            }
            else
            {
                XmlNode newNode = AddNodeInNodeTree(newTxt);
                nodetree.SelectedNode.Tag = newNode;
                nodetree.SelectedNode.Name = GetAttribute(newNode, "ID");
                SaveLog("添加节点名称：" + newTxt);
                fenshuADD(1);
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                th.Start();
            }
        }

        private void NodeTree_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    IsMindMapNodeEdit = true;
                    isRename = true;
                    renameTaskName = nodetree.SelectedNode.Text;
                    renameMindMapFileID = nodetree.SelectedNode.Name;
                    nodetree.SelectedNode.BeginEdit();
                    break;
                default:
                    break;
            }
        }
        private void TreeView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    IsFileNodeEdit = true;
                    FileTreeView.SelectedNode.BeginEdit();
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        public static string section = "rootPath";//用于和日历同步
        private void PathcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Height = 0;
            if (!selectedpath && sender != null)
            {
                selectedpath = true;
                return;
            }
            try
            {
                if (PathcomboBox.SelectedItem.ToString() == "all")
                {
                    allFloder = true;
                    rootpath = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", "rootpath", "")));
                }
                else
                {
                    allFloder = false;
                    rootpath = new DirectoryInfo(System.IO.Path.GetFullPath(ini.ReadString("path", PathcomboBox.SelectedItem.ToString(), "")));
                }
                section = PathcomboBox.SelectedItem.ToString();
                mindmapPath = rootpath.FullName;
                searchword.Text = "";
                loadHopeNote();
                PlaySimpleSound("changepath");
                UsedLogRenew();
                Load_Click(null, null);
                reminderList.Focus();
                Center();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 加载hope笔记
        /// </summary>
        public void loadHopeNote()
        {
            try
            {
                if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + PathcomboBox.SelectedItem.ToString() + @".txt"))
                {
                    hopeNote.LoadFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + PathcomboBox.SelectedItem.ToString() + @".txt");

                }
                else
                {
                    hopeNote.Text = "";
                }
            }
            catch (Exception ex)
            {
            }
        }
        #region 剪切板
        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        //public static string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string clipordFilePath = System.AppDomain.CurrentDomain.BaseDirectory;

        IntPtr nextClipboardViewer;
        private System.Threading.Timer reminder;
        int mouseDisplacement = 20;
        public string log;
        public int hour = 0;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            ChangeClipboardChain(this.Handle, nextClipboardViewer);
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        List<string> logHistory = new List<string>();
        void DisplayClipboardData()
        {
            try
            {
                IDataObject iData = new DataObject();
                iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.Rtf))
                {
                    log = (string)iData.GetData(DataFormats.StringFormat);
                    log = yixiaozi.StringHelper.ChineseStringUtility.ToSimplified(log);//切换成简体字
                }
                else if (iData.GetDataPresent(DataFormats.Text))
                {
                    log = (string)iData.GetData(DataFormats.StringFormat);
                    log = yixiaozi.StringHelper.ChineseStringUtility.ToSimplified(log);//切换成简体字
                    Clipboard.SetDataObject(log);
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    foreach (string item in ConvertStringCollection(Clipboard.GetFileDropList()))
                    {
                        log += (item + "\r");
                        if (System.IO.Directory.Exists(item))
                        {
                            log = LoadTree(item, log, 1);
                        }
                    }
                }
                else if (Clipboard.ContainsImage())
                {
                    System.IO.Directory.CreateDirectory(clipordFilePath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "images");
                    Clipboard.GetImage().Save((clipordFilePath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "images" + "\\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg").Replace(@"\\", @"\"));
                }
                if (log != null)
                {
                    if (log.Length > 5)
                    {
                        if (!logHistory.Contains(log))
                        {
                            SaveLogClip(log);
                            logHistory.Add(log);
                            if (logHistory.Count > 20)
                            {
                                logHistory.RemoveAt(0);
                            }
                        }
                    }
                }
                log = "";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(e.ToString());
            }
        }

        public static List<string> ConvertStringCollection(StringCollection collection)
        {
            List<string> list = new List<string>();
            foreach (string item in collection)
            {
                list.Add(item);
            }
            return list;
        }
        public void SaveLogClip(string log)
        {
            System.IO.Directory.CreateDirectory(clipordFilePath + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\");
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(clipordFilePath + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\" + DateTime.Now.Day.ToString() + ".txt", true))
            {
                if (log != "")
                {
                    file.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "   ");
                    file.WriteLine(log);
                }
            }
        }

        private void btn_OpenFile_MouseClick()
        {
            if (System.IO.File.Exists(clipordFilePath + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\" + DateTime.Now.Day.ToString() + ".txt"))
            {
                System.Diagnostics.Process.Start(clipordFilePath + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\" + DateTime.Now.Day.ToString() + ".txt");
            }
        }

        private void Btn_OpenFolder_Click()
        {
            System.Diagnostics.Process.Start(clipordFilePath + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AutoRun_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取程序执行路径..
            string starupPath = Application.ExecutablePath;
            //class Micosoft.Win32.RegistryKey. 表示Window注册表中项级节点,此类是注册表装.
            RegistryKey loca = Registry.LocalMachine;
            try
            {
                RegistryKey run = loca.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                //SetValue:存储值的名称
                run.SetValue("DocearReminder", starupPath);
                MessageBox.Show("已启用开机运行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loca.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置开机启动请使用管理员权限打开本软件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisAutoRun_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string starupPath = Application.ExecutablePath;
            RegistryKey loca = Registry.LocalMachine;
            try
            {
                //SetValue:存储值的名称
                RegistryKey run = loca.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                run.DeleteValue("DocearReminder");
                MessageBox.Show("已停止开机运行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loca.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("取消开机启动请使用管理员权限打开本软件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string LoadTree(string path, string str, int f)
        {
            string __ = "";
            string str1 = "";
            for (int i = 0; i < f * 2; i++)
            {
                __ += "-";
            }

            str1 += (__ + path + "\r\n");
            foreach (string file in System.IO.Directory.GetFiles(path))
            {
                str1 += (__.Replace('-', ' ').Replace('I', ' ') + file.Split('\\')[file.Split('\\').Length - 1] + "\r\n");
            }
            string[] dirs = Directory.GetDirectories(path);//获取子目录
            if (dirs.Length > 0)
            {
                f += 1;
                foreach (string dir in dirs)
                {
                    str1 = LoadTree(dir, str1, f);
                }
            }
            return str + str1 + "\r\n";
        }

        private void reminder_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute < 2)
            {
                Type shellType = Type.GetTypeFromProgID("Shell.Application");
                object shellObject = System.Activator.CreateInstance(shellType);
                shellType.InvokeMember("ToggleDesktop", System.Reflection.BindingFlags.InvokeMethod, null, shellObject, null);
            }
        }

        public void SearchbuttonClick()
        {
            System.Threading.Thread th = new System.Threading.Thread(() => OpenSearch());
            th.Start();
        }

        public struct Point
        {
            public int p1;
            public int p2;

            public Point(int p1, int p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }
        }
        Point p = new Point(1, 1);
        public void SetAlt()
        {
            if (niazhi)
            {
                keybd_event(18, 0, 0, 0);
            }
            else
            {
                keybd_event(18, 0, 0x2, 0);
            }
        }
        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Visible == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Y && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                GetCursorPos(ref p);
                mouse_event(MOUSEEVENTF_LEFTDOWN, p.p1, p.p2, 0, 0);
            }
            else if (e.KeyCode == Keys.H && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {

                GetCursorPos(ref p);
                mouse_event(MOUSEEVENTF_LEFTUP, p.p1, p.p2, 0, 0);
            }
            else if (e.KeyCode == Keys.J && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                GetCursorPos(ref p);
                SetCursorPos(p.p1 - mouseDisplacement, p.p2);
            }
            else if (e.KeyCode == Keys.L && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                GetCursorPos(ref p);
                SetCursorPos(p.p1 + mouseDisplacement, p.p2);
            }
            else if (e.KeyCode == Keys.I && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                GetCursorPos(ref p);
                SetCursorPos(p.p1, p.p2 - mouseDisplacement);
            }
            else if (e.KeyCode == Keys.K && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                GetCursorPos(ref p);
                SetCursorPos(p.p1, p.p2 + mouseDisplacement);
            }
            else if (e.KeyCode == Keys.U && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {

                GetCursorPos(ref p);
                mouse_event(MOUSEEVENTF_LEFTDOWN, p.p1, p.p2, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, p.p1, p.p2, 0, 0);
            }
            else if (e.KeyCode == Keys.N && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, p.p1, p.p2, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, p.p1, p.p2, 0, 0);
                System.Threading.Thread.Sleep(150);
                mouse_event(MOUSEEVENTF_LEFTDOWN, p.p1, p.p2, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, p.p1, p.p2, 0, 0);
            }
            else if (e.KeyCode == Keys.O && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                GetCursorPos(ref p);
                mouse_event(MOUSEEVENTF_RIGHTDOWN, p.p1, p.p2, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, p.p1, p.p2, 0, 0);
            }
            else if (e.KeyCode == Keys.M && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {

                GetCursorPos(ref p);
                mouse_event(MOUSEEVENTF_MIDDLEDOWN, p.p1, p.p2, 0, 0);
                mouse_event(MOUSEEVENTF_MIDDLEUP, p.p1, p.p2, 0, 0);
                mouse_event(MOUSEEVENTF_MIDDLEDOWN, p.p1, p.p2, 0, 0);
                mouse_event(MOUSEEVENTF_MIDDLEUP, p.p1, p.p2, 0, 0);
            }
            else if (e.KeyCode == Keys.Oem1 && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {

                GetCursorPos(ref p);
                mouse_event(MOUSEEVENTF_WHEEL, p.p1, p.p2, -200, 0);
            }
            else if (e.KeyCode == Keys.Oem7 && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {

                GetCursorPos(ref p);
                mouse_event(MOUSEEVENTF_WHEEL, p.p1, p.p2, 200, 0);
            }
            else if (e.KeyCode == Keys.Oemplus && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                if (mouseDisplacement < 100)
                {
                    mouseDisplacement += 2;
                    //l_mouseDisplacement.Text = mouseDisplacement.ToString();
                }
            }
            else if (e.KeyCode == Keys.OemMinus && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                if (mouseDisplacement > 5)
                {
                    mouseDisplacement -= 2;
                    //l_mouseDisplacement.Text = mouseDisplacement.ToString();
                }
            }

        }
        private void HookManager_KeyDown_saveKeyBoard(object sender, KeyEventArgs e)
        {
            //记录键盘键
            System.IO.Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\");
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\key.txt", true))
            {
                if (DateTime.Now.Hour != hour)
                {
                    hour = DateTime.Now.Hour;
                    file.Write("\r");
                    file.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                }
                if (e.KeyCode.ToString() != "")
                {
                    file.Write(e.KeyCode.ToString() + ";");
                }
            }
        }
        /// <summary>   
        /// 设置鼠标的坐标   
        /// </summary>   
        /// <param name="x">横坐标</param>   
        /// <param name="y">纵坐标</param>   
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);

        [DllImport("user32", CharSet = CharSet.Unicode)]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
            byte bVk,
            byte bScan,
            int dwFlags,  //这里是整数类型  0 为按下，2为释放  
            int dwExtraInfo  //这里是整数类型 一般情况下设成为 0  
        );
        /// <summary>   
        /// 获取鼠标的坐标   
        /// </summary>   
        /// <param name="lpPoint">传址参数，坐标point类型</param>   
        /// <returns>获取成功返回真</returns>   
        [DllImport("User32")]
        public extern static bool GetCursorPos(ref Point lpPoint);
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; // press left mouse button
        const int MOUSEEVENTF_LEFTUP = 0x0004; // release left mouse button
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; // whole screen, not just application window
        const int MOUSEEVENTF_MOVE = 0x0001; // move mouse
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        const int MOUSEEVENTF_WHEEL = 0x800;

        private void niazhi_CheckedChanged(object sender, EventArgs e)
        {
            if (true)
            {
                keybd_event(18, 0, 0, 0);

            }
            else
            {
                //keybd_event(18, 0, 0x2, 0);
            }
        }

        #endregion

        private void Reminderlist_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int posindex = reminderList.IndexFromPoint(new System.Drawing.Point(e.X, e.Y));
                reminderList.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < reminderList.Items.Count)
                {
                    ReminderListSelectedIndex(posindex);
                    menu_reminderlist.Show(reminderList, new System.Drawing.Point(e.X, e.Y));
                }
                else
                {
                    IsReminderOnlyCheckBox.Checked = !IsReminderOnlyCheckBox.Checked;
                    RRReminderlist();
                    reminderList.Refresh();
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                int posindex = reminderList.IndexFromPoint(new System.Drawing.Point(e.X, e.Y));
                reminderList.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < reminderList.Items.Count)
                {
                    //ReminderListSelectedIndex( posindex;
                    //menu_reminderlist.Show(reminderList, new System.Drawing.Point(e.X, e.Y));
                }
                else
                {
                    if (IsReminderOnlyCheckBox.Checked)
                    {
                        IsReminderOnlyCheckBox.Checked = !IsReminderOnlyCheckBox.Checked;
                        RRReminderlist();
                    }
                    else
                    {
                        showcyclereminder.Checked = !showcyclereminder.Checked;
                        RRReminderlist();
                    }
                    reminderList.Refresh();
                }
            }
        }
        private void mindmaplist_MouseDown(object sender, MouseEventArgs e)
        {
            IsSelectReminder = false;
            if (e.Button == MouseButtons.Right)
            {
                int posindex = mindmaplist.IndexFromPoint(new System.Drawing.Point(e.X, e.Y));
                mindmaplist.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < mindmaplist.Items.Count)
                {
                    mindmaplist.SelectedIndex = posindex;
                    menu_mindmaps.Show(mindmaplist, new System.Drawing.Point(e.X, e.Y));
                }
            }
            mindmaplist.Refresh();
        }

        private void nodetree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //e.Node.Name  节点的ID
            if (e.Node.Name != null)
            {
                try
                {
                    string pictureUrl = getPictureByID(e.Node.Name, showMindmapName);
                    if (pictureUrl != "")
                    {
                        //pictureBox1.Image = Image.FromFile(pictureUrl);
                        SetPicture(pictureUrl);

                        if (pictureBox1.Image.Height >= pictureBox1.Image.Width)
                        {
                            pictureBox1.Height = pictureBox1.MaximumSize.Height;
                        }
                        else
                        {
                            pictureBox1.Height = Convert.ToInt16(pictureBox1.MaximumSize.Height * (Convert.ToDouble(pictureBox1.Image.Height) / pictureBox1.Image.Width));
                        }
                    }
                    else
                    {
                        pictureBox1.Height = 0;
                    }
                }
                catch (Exception ex)
                {
                    pictureBox1.Height = 0;
                }
            }
        }

        private void nodetree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                System.Drawing.Point ClickPoint = new System.Drawing.Point(e.X, e.Y);
                TreeNode CurrentNode = nodetree.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    CurrentNode.ContextMenuStrip = menu_nodetree;
                    //name = nodetree.SelectedNode.Text.ToString();//存储节点的文本
                    nodetree.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        }

        private void menu_filetree_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void DocearReminderForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                int posindex = mindmaplist.IndexFromPoint(new System.Drawing.Point(e.X, e.Y));
                menu.Show(this, new System.Drawing.Point(e.X, e.Y));
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            taskComplete_btn_Click(null, null);
            PlaySimpleSound("Done");
        }
        public void GetIniFile()
        {
            suggestListData.Clear();
            #region 这样读取ini所有节点有问题
            //IniFile ini1 = new IniFile(ini.ReadStringDefault("path", Environment.MachineName, ""));
            //List<string> list = ini1.ReadSections();
            //foreach (string item in list)
            //{
            //    if (item.StartsWith("Btn")&&list2.FindAll(m=>m.StationName_CN == ini1.ReadStringDefault(item, "Name", "")).Count()==0)
            //    {
            //        string file = ini1.ReadStringDefault(item, "File", "");
            //        if (file != "")
            //        {
            //            string name = ini1.ReadStringDefault(item, "Name", "");
            //            string proString = "";
            //            if (System.IO.File.Exists(file))
            //            {
            //                proString = "f:";
            //            }
            //            else if (System.IO.Directory.Exists(file))
            //            {
            //                proString = "F:";
            //            }
            //            else if (isURL(file))
            //            {
            //                proString = "B:";
            //            }
            //            else
            //            {
            //            }
            //            list2.Add(new StationInfo { mindmapurl = file, StationName_CN = "f:"+name });
            //        }
            //    }
            //} 
            #endregion
            getinifilenode();
            //处理bookmarks
            GetBookmarksLinks();
            GetFolderToSuggest();
            GetAllLinksToSuggest();
        }
        public void GetAllLinksToSuggest()
        {
            try
            {
                List<string> links = new List<string>();
                links = new TextListConverter().ReadTextFileToList(System.AppDomain.CurrentDomain.BaseDirectory + @"\alllinks.txt");
                foreach (string item in links)
                {
                    if (!item.Contains("|"))
                    {
                        continue;
                    }
                    suggestListData.Add(new StationInfo { StationName_CN = "alllinks:" + item.Split('|')[0], mindmapurl = item.Split('|')[1], isNode = "file" });
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetFolderToSuggest()
        {
            try
            {
                string pathArr = ini.ReadStringDefault("path", Environment.MachineName + "Folders", "");
                foreach (string item in pathArr.Split(';'))
                {
                    string path = ini.ReadStringDefault("path", item, "");
                    foreach (FileInfo file in new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories))
                    {
                        if (file.FullName.Contains(".svn") || file.FullName.Contains(".vs") || file.FullName.Contains(".git") || file.FullName.ToLower().Contains("backup"))
                        {
                            continue;
                        }
                        suggestListData.Add(new StationInfo { StationName_CN = item + ":" + file.Name, mindmapurl = file.FullName, isNode = "file" });
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void getinifilenode()
        {
            try
            {
                const Int32 BufferSize = 128;
                string path = ini.ReadStringDefault("path", Environment.MachineName + "CLaunchIni", "");
                using (var fileStream = File.OpenRead(path))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        String line;
                        string Name = "";
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (line.Trim() == "")
                            {
                                continue;
                            }
                            if (line.ToUpper().StartsWith("NAME"))
                            {
                                Name = line.Substring(5);
                            }
                            if (line.ToUpper().StartsWith("FILE") && Name != "")
                            {
                                if (line.Contains("\\") || line.Contains("/"))
                                {
                                    suggestListData.Add(new StationInfo { StationName_CN = "CLaunch:" + Name, mindmapurl = line.Substring(5) });
                                }
                                Name = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void GetBookmarksLinks()
        {
            try
            {
                string bookmarksmindmap = ini.ReadString("path", "bookmarks", "");
                System.Xml.XmlDocument x = new XmlDocument();
                x.Load(bookmarksmindmap);
                bool isNeedUpdate = false;
                foreach (XmlNode node in x.GetElementsByTagName("node"))
                {
                    try
                    {
                        if (node.Attributes != null && node.Attributes["TEXT"] != null && IsURL(node.Attributes["TEXT"].Value) && node.Attributes["LINK"] == null)
                        {
                            XmlAttribute LINK = x.CreateAttribute("LINK");
                            LINK.Value = node.Attributes["TEXT"].Value;
                            node.Attributes["TEXT"].Value = yixiaozi.Net.HttpHelp.Web.getTitle(node.Attributes["TEXT"].Value);
                            node.Attributes.Append(LINK);
                        }
                        if (node.Attributes != null && node.Attributes["TEXT"] != null && node.Attributes["LINK"] != null)
                        {
                            suggestListData.Add(new StationInfo { mindmapurl = node.Attributes["LINK"].Value, StationName_CN = "bookmarks:" + node.Attributes["TEXT"].Value });
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (isNeedUpdate)
                {
                    x.Save(bookmarksmindmap);
                    yixiaozi.Model.DocearReminder.Helper.ConvertFile(bookmarksmindmap);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void FileTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                System.Drawing.Point ClickPoint = new System.Drawing.Point(e.X, e.Y);
                TreeNode CurrentNode = FileTreeView.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    CurrentNode.ContextMenuStrip = menu_filetree;
                    //name = nodetree.SelectedNode.Text.ToString();//存储节点的文本
                    FileTreeView.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        }

        private void ReminderListBox_SizeChanged(object sender, EventArgs e)
        {
            if (reminderListBox.Items.Count > 0)
            {
                reminderList.Top = reminderListBox.Top + reminderListBox.Height + (reminderListBox.Height == 0 ? 0 : 10);
                reminderList.Height = mindmaplist.Height - reminderListBox.Height - (reminderListBox.Height == 0 ? 0 : 10);//- 51
            }
            else
            {
                reminderList.Top = mindmaplist.Top;
                reminderList.Height = mindmaplist.Height;
            }
        }

        private void ReminderListBox_DataSourceChanged(object sender, EventArgs e)
        {

        }
        public void ReminderlistBoxChange()
        {
            if (reminderListBox.Items.Count > 0 && !(showTimeBlock.Checked || ShowKA.Checked || ShowMoney.Checked))
            {
                reminderListBox.Height = reminderListBox.PreferredHeight;
                reminderListBox.Visible = true;
            }
            else
            {
                reminderListBox.Height = 0;
                reminderListBox.Visible = false;
                reminderList.Top = mindmaplist.Top;
                reminderList.Height = mindmaplist.Height;
                reminderList.Focus();
            }
        }

        private void ReminderlistBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                int zhongyao = 0;
                string name = "";
                zhongyao = ((MyListBoxItemRemind)reminderListBox.Items[e.Index]).level;
                name = ((MyListBoxItemRemind)reminderListBox.Items[e.Index]).Name;
                System.Drawing.Brush mybsh = Brushes.Gray;
                Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
                Rectangle rectleft = new Rectangle(e.Bounds.X + e.Bounds.Height, e.Bounds.Y, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height);
                if (zhongyao == 0)
                {
                    SolidBrush zeroColor = new SolidBrush(Color.FromArgb(238, 238, 242));
                    if (searchword.Text.StartsWith("#") || searchword.Text.StartsWith("*"))
                    {
                        zeroColor = new SolidBrush(Color.White);
                    }
                    e.Graphics.FillRectangle(zeroColor, rect);
                    mybsh = new SolidBrush(Color.FromArgb(238, 238, 242));
                    if (name == "当前时间")
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 238, 242)), e.Bounds);
                        mybsh = Brushes.Gray;
                    }
                }
                else if (zhongyao == 1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Azure), rect);
                    mybsh = new SolidBrush(Color.Azure);
                }
                else if (zhongyao == -1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, 220, 220)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(220, 220, 220));
                }
                else if (zhongyao == -2)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(211, 211, 211)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(211, 211, 211));
                }
                else if (zhongyao == -3)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, 192, 192)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(192, 192, 192));
                }
                else if (zhongyao == -4)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(169, 169, 169)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(169, 169, 169));
                }
                else if (zhongyao == -5)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(128, 128, 128));
                }
                else if (zhongyao == -6)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(105, 105, 105)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(105, 105, 105));
                }
                else if (zhongyao == -7)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 85, 85)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(85, 85, 85));
                }
                else if (zhongyao == -8)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(65, 65, 65)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(65, 65, 65));
                }
                else if (zhongyao == -9)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(48, 48, 48)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(48, 48, 48));
                }
                else if (zhongyao <= -10)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
                    mybsh = new SolidBrush(Color.Black);
                }
                else if (zhongyao == 2)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.PowderBlue), rect);
                    mybsh = new SolidBrush(Color.PowderBlue);
                }
                else if (zhongyao == 3)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightSkyBlue), rect);
                    mybsh = new SolidBrush(Color.LightSkyBlue);
                }
                else if (zhongyao == 4)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.DeepSkyBlue), rect);
                    mybsh = new SolidBrush(Color.DeepSkyBlue);
                }
                else if (zhongyao == 5)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.CadetBlue), rect);
                    mybsh = new SolidBrush(Color.CadetBlue);
                }
                else if (zhongyao == 6)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gold), rect);
                    mybsh = new SolidBrush(Color.Gold);
                }
                else if (zhongyao == 7)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Orange), rect);
                    mybsh = new SolidBrush(Color.Orange);
                }
                else if (zhongyao == 8)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.OrangeRed), rect);
                    mybsh = new SolidBrush(Color.OrangeRed);
                }
                else if (zhongyao == 9)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Crimson), rect);
                    mybsh = new SolidBrush(Color.Crimson);
                }
                else if (zhongyao >= 10)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), rect);
                    mybsh = new SolidBrush(Color.Red);
                }
                if (e.Index == reminderListBox.SelectedIndex)
                {
                    e.Graphics.FillRectangle(mybsh, rect);
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), rectleft); //Yellow
                }
                if (searchword.Text.StartsWith("#"))
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderListBox.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                    //e.DrawFocusRectangle();
                }
                else if (searchword.Text.StartsWith("*"))
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderListBox.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                    //e.DrawFocusRectangle();
                }
                else if (!((MyListBoxItemRemind)reminderListBox.Items[e.Index]).isTask)
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderListBox.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                    //e.DrawFocusRectangle();
                }
                else
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderListBox.Items[e.Index]).Text.Substring(0, 3), e.Font, mybsh, rect, StringFormat.GenericDefault);
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderListBox.Items[e.Index]).Text.Substring(3), e.Font, Brushes.Gray, rectleft, StringFormat.GenericDefault);
                    ((MyListBoxItemRemind)reminderListBox.Items[e.Index]).IsShow = true;
                    //e.DrawFocusRectangle();
                }
            }
        }

        private void richTextSubNode_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Top = richTextSubNode.Top + richTextSubNode.Height + (richTextSubNode.Height != 0 ? 0 : 0);
            hopeNote.Top = pictureBox1.Top + pictureBox1.Height + (pictureBox1.Height != 0 ? 14 : 0);
            tagCloudControl.Top = hopeNote.Top + hopeNote.Height + 14;
            tagCloudControl.Height = 475 - tagCloudControl.Top;
        }

        private void RichTextSubNode_TextChanged(object sender, EventArgs e)
        {
            if (richTextSubNode.Text.Trim() == "")
            {
                richTextSubNode.Height = 0;
                return;
            }
            //this.richTextSubNode.Text = Regex.Replace(this.richTextSubNode.Text, @"(?s)\n\s*\n", "\n");
            int EM_GETLINECOUNT = 0x00BA;//获取总行数的消息号 
            int lc = SendMessage(this.richTextSubNode.Handle, EM_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero);
            int sf = this.richTextSubNode.Font.Height * (lc + 1);
            this.richTextSubNode.Height = sf;
            //richTextSubNode.Height = e.NewRectangle.Height + 10;
            //richTextSubNode.Height = richTextSubNode.Lines.Length * 13;
        }

        private void dateTimePicker_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                reminderList.Focus();
            }
        }
        private int leftIndex = 0;
        private bool isSettingSyncWeek;
        private bool niazhi;

        private void tagCloudControl_ControlAdded(object sender, ControlEventArgs e)
        {
            //if (isnottagcloudonload)
            //{
            //    saveTagFile();
            //}
        }

        private void TagCloudControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            //if (isnottagcloudonload)
            //{
            //    saveTagFile();
            //}
        }
        public void WriteTagFile()
        {
            //FileInfo fi = new FileInfo(@"tagcloud.json");
            //JavaScriptSerializer js = new JavaScriptSerializer
            //{
            //    MaxJsonLength = Int32.MaxValue
            //};
            //string json = js.Serialize(tagCloudControl.GetAllItems());
            //File.WriteAllText(@"tagcloud.json", "");
            //using (StreamWriter sw = fi.AppendText())
            //{
            //    sw.Write(json);
            //}
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + @"tagcloud.xml";
            bool b = tagCloudControl.WriteTagFile(filename);
        }
        public void ReadTagFile()
        {
            string filename = System.AppDomain.CurrentDomain.BaseDirectory + @"tagcloud.xml";
            bool b = tagCloudControl.ReadTagFile(filename);
        }

        private void ReadBookmarks()
        {
            try
            {
                string chromeDistribution = ini.ReadString("path", "chromeDistribution", "");
                foreach (string item in chromeDistribution.Split(';'))
                {
                    try
                    {
                        string path = ini.ReadString("path", item, "");
                        string ChromeDatePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + path;
                        string ChromeBookMarksPath = ChromeDatePath + @"\Bookmarks";
                        if (File.Exists(ChromeBookMarksPath))
                        {
                            ConvertbookmarketToini(GetChromeBookmarksData(ChromeBookMarksPath).roots.bookmark_bar, item);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void ConvertbookmarketToini(datameta bookmarks, string preStr)
        {
            if (bookmarks.type == "url")
            {
                suggestListData.Add(new StationInfo { StationName_CN = preStr + (preStr != "" ? ":" : "") + bookmarks.name, mindmapurl = bookmarks.url });
            }
            if (bookmarks.children != null)
            {
                foreach (datameta item in bookmarks.children)
                {
                    ConvertbookmarketToini(item, preStr);
                }
            }
        }

        /// <summary>
        /// json序列化
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string ListToJson<T>(T data)
        {
            string str = string.Empty;
            try
            {
                if (null != data)
                {
                    str = JsonConvert.SerializeObject(data);
                }
            }
            catch (Exception ex)
            {

            }
            return str;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="jsonstr">数据</param>
        /// <returns></returns>
        public static Object JsonToList<T>(string jsonstr)
        {
            Object obj = null;
            try
            {
                if (null != jsonstr)
                {
                    obj = JsonConvert.DeserializeObject<T>(jsonstr);//反序列化
                }
            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string FileRead(string filePath)
        {
            string rel = File.ReadAllText(filePath);
            return rel;
        }

        /// <summary>
        /// 获取Chrome浏览器书签对象
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ChromeBookmarks GetChromeBookmarksData(string filePath)
        {
            string str = FileRead(filePath);
            object chromeBookmarks = JsonToList<ChromeBookmarks>(str);
            if (chromeBookmarks != null)
            {
                return (ChromeBookmarks)chromeBookmarks;
            }
            return null;
        }

        private void panel_clearSearchWord_Click(object sender, EventArgs e)
        {

        }

        private void noterichTextBox_TextChanged(object sender, EventArgs e)
        {
            noterichTextBox.ForeColor = Color.Gray;
            try
            {
                noterichTextBox.SaveFile(ini.ReadString("path:", "note", System.AppDomain.CurrentDomain.BaseDirectory + @"note.txt"));
            }
            catch (Exception ex)
            {
            }
        }
        #region 右键菜单动作


        private void autoRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取程序执行路径..
            string starupPath = Application.ExecutablePath;
            //class Micosoft.Win32.RegistryKey. 表示Window注册表中项级节点,此类是注册表装.
            RegistryKey loca = Registry.LocalMachine;
            try
            {
                RegistryKey run = loca.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                //SetValue:存储值的名称
                run.SetValue("DocearReminder", starupPath);
                MessageBox.Show("已启用开机运行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loca.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置开机启动请使用管理员权限打开本软件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void disAutoRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string starupPath = Application.ExecutablePath;
            RegistryKey loca = Registry.LocalMachine;
            try
            {
                //SetValue:存储值的名称
                RegistryKey run = loca.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                run.DeleteValue("DocearReminder");
                MessageBox.Show("已停止开机运行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loca.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("取消开机启动请使用管理员权限打开本软件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItem_deny_Click(object sender, EventArgs e)
        {
            DelaySelectedTask();
        }

        private void toolStripMenuItemCalcal_Click(object sender, EventArgs e)
        {
            cancel_btn_Click(null, null);
        }

        private void 打开所在目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new FileInfo(((MyListBoxItemRemind)reminderlistSelectedItem).Value).DirectoryName);
                MyHide();
            }
            catch (Exception ex)
            {
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (nodetree.SelectedNode.Name != null)
            {
                try
                {
                    string deleteNodeName = nodetree.SelectedNode.Text;
                    deleteNodeByID(nodetree.SelectedNode.Name);
                    SaveLog("删除节点：" + deleteNodeName + "    导图" + showMindmapName.Split('\\')[showMindmapName.Split('\\').Length - 1]);
                    fenshuADD(1);
                    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                    th.Start();
                    ShowMindmap();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible == false)
            {
                this.Visible = true;
                //this.notifyIcon1.Visible = false;
                Center();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                this.Visible = true;
                //this.notifyIcon1.Visible = false;
                Center();
            }
        }

        private void 剪切板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchword.Text = "";
            MyHide();
            OpenSearch();
        }

        private void Searchword_MouseDown(object sender, MouseEventArgs e)
        {
            //searchworkmenu.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new FileInfo(((MyListBoxItem)mindmaplist.SelectedItem).Value).DirectoryName);
                MyHide();
            }
            catch (Exception ex)
            {
            }
        }

        private void 打开程序目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        private void 显示右侧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Width == maxwidth)
            {
                this.Width = normalwidth;
            }
            else if (this.Width == middlewidth)
            {
                this.Width = maxwidth;
            }
            else
            {
                this.Width = middlewidth;
            }
            Center();
        }

        private void 是否锁定窗口lockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockForm = !lockForm;
        }

        private void 操作记录F12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showlog();
        }

        private void 剪切板文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchword.Text = "";
            MyHide();
            btn_OpenFile_MouseClick();
        }

        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchword.Text = "";
            MyHide();
            Btn_OpenFolder_Click();
        }

        private void 日历QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showcalander();
        }

        private void 工具箱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hiddenmenu_DoubleClick(null, null);
        }

        private void 查看模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c_ViewModel.Checked = !c_ViewModel.Checked;
            if (!c_ViewModel.Checked)
            {
                ReSetValue();
                RRReminderlist();
                reminderList.Focus();
            }
            else
            {
                ReminderListSelectedIndex(-1);
                reminderSelectIndex = -1;
                IsSelectReminder = false;
                mindmaplist.Focus();
            }
        }

        private void 透明度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void o05ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void o08ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.8;
        }

        private void o1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void 显示树视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMindmap();
            ShowMindmapFile();
            nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
            this.Height = maxheight;
            nodetree.Focus();
        }

        private void 隐藏树视图SnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nodetree.Top = FileTreeView.Top = nodetreeTop;
            nodetree.Height = FileTreeView.Height = nodetreeHeight;
            nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = false;
            this.Height = normalheight; showMindmapName = "";
            reminderList.Focus();
        }

        private void 是否播放声音playsoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isPlaySound = !isPlaySound;
        }

        private void 推出F11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (videoSourcePlayer1 != null && videoSourcePlayer1.IsRunning)
            {
                videoSourcePlayer1.SignalToStop();
                videoSourcePlayer1.WaitForStop();
            }
            Application.Exit();
        }

        private void 仅查看CdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int reminderIndex = reminderList.SelectedIndex;
            SetTaskIsView();
            try
            {
                reminderList.Items.RemoveAt(reminderIndex);
                RRReminderlist();
                ReminderListSelectedIndex(reminderIndex);
            }
            catch (Exception ex)
            {

            }
        }

        private void 非重要ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //提醒任务
            int selectindex = reminderList.SelectedIndex;
            SetReminderOnly((MyListBoxItemRemind)reminderlistSelectedItem);
            RRReminderlist();
            if (selectindex < reminderList.Items.Count - 1)
            {
                ReminderListSelectedIndex(selectindex);
            }
        }

        private void 设置重要xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reminderList.Focused)
            {
                reminderListBox.Items.Add((MyListBoxItemRemind)reminderlistSelectedItem);
                reminderboxList.Add((MyListBoxItemRemind)reminderlistSelectedItem);
                ReminderlistBoxChange();
                reminderList.Items.RemoveAt(reminderList.SelectedIndex);
            }
            else if (reminderListBox.Focused)
            {
                reminderboxList.Remove((MyListBoxItemRemind)reminderListBox.SelectedItem);
                reminderListBox.Items.RemoveAt(reminderListBox.SelectedIndex);
                ReminderlistBoxChange();
                if (reminderListBox.Items.Count == 0)
                {
                    reminderList.Focus();
                    ReminderListSelectedIndex(0);
                }
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(((MyListBoxItemRemind)reminderlistSelectedItem).Value);
                MyHide();
            }
            catch (Exception ex)
            {
            }
        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_Click_1(object sender, EventArgs e)
        {
            //MyShow();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = !this.Visible;
                this.WindowState = FormWindowState.Normal;
                MyShow();
            }
            else if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void labeltaskinfo_Click(object sender, EventArgs e)
        {
            PlaySimpleSound("treeview");
            if (this.Height == maxheight)
            {
                nodetree.Top = FileTreeView.Top = nodetreeTop;
                nodetree.Height = FileTreeView.Height = nodetreeHeight;
                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = false;
                this.Height = normalheight; showMindmapName = "";
                reminderList.Focus();
            }
            else
            {
                ShowMindmap();
                ShowMindmapFile();
                nodetree.Visible = FileTreeView.Visible = noterichTextBox.Visible = nodetreeSearch.Visible = true;
                this.Height = maxheight;
                nodetree.Focus();
            }
        }

        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void 打开文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetTaskNodeByID(nodetree.SelectedNode.Name))
                {
                    SaveLog("设置节点为任务：" + nodetree.SelectedNode.Text + "    导图" + showMindmapName.Split('\\')[showMindmapName.Split('\\').Length - 1]);
                    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(showMindmapName));
                    th.Start();
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
        }

        private void dateTimePicker_MouseLeave(object sender, EventArgs e)
        {
            //EditTime_Clic(null, null);
        }

        private void EditTaskTime_Click(object sender, EventArgs e)
        {
            EditTime_Clic(null, null);
        }
        static void MoveElementUp(XElement element)
        {
            // Walk backwards until we find an element - ignore text nodes
            XNode previousNode = element.PreviousNode;
            while (previousNode != null && !(previousNode is XElement))
            {
                previousNode = previousNode.PreviousNode;
            }
            if (previousNode == null)
            {
                throw new ArgumentException("Nowhere to move element to!");
            }
            element.Remove();
            previousNode.AddBeforeSelf(element);
        }
        static void MoveElementLeft(XElement element)
        {
            // Walk backwards until we find an element - ignore text nodes
            XNode parentNode = element.Parent;
            if (parentNode == null)
            {
                throw new ArgumentException("Nowhere to move element to!");
            }
            element.Remove();
            parentNode.AddAfterSelf(element);
        }
        static void MoveElementRight(XElement element)
        {
            // Walk backwards until we find an element - ignore text nodes
            XNode previousNode = element.PreviousNode;
            if (previousNode == null)
            {
                throw new ArgumentException("Nowhere to move element to!");
            }
            element.Remove();
            if (((XElement)previousNode).Elements().LastOrDefault() != null)
            {
                ((XElement)previousNode).Elements().LastOrDefault().AddAfterSelf(element);
            }
            else
            {
                ((XElement)previousNode).AddFirst(element);
            }
        }
        static void RemoveNamespacePrefix(XElement element)
        {
            //Remove from element
            if (element.Name.Namespace != null)
                element.Name = element.Name.LocalName;

            //Remove from attributes
            var attributes = element.Attributes().ToArray();
            element.RemoveAttributes();
            foreach (var attr in attributes)
            {
                var newAttr = attr;

                if (attr.Name.Namespace != null)
                    newAttr = new XAttribute(attr.Name.LocalName, attr.Value);

                element.Add(newAttr);
            };

            //Remove from children
            foreach (var child in element.Descendants())
                RemoveNamespacePrefix(child);
        }
        static void MoveElementDown(XElement element)
        {
            // Walk backwards until we find an element - ignore text nodes
            XNode previousNode = element.NextNode;
            while (previousNode != null && !(previousNode is XElement))
            {
                previousNode = previousNode.NextNode;
            }
            if (previousNode == null)
            {
                throw new ArgumentException("Nowhere to move element to!");
            }
            element.Remove();
            previousNode.AddAfterSelf(element);
        }

        private void noterichTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Modifiers.CompareTo(Keys.Control) == 0 || e.Modifiers.CompareTo(Keys.Shift) == 0)
                {
                    nodetree.Focus();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                nodetree.Focus();
            }
        }

        private void IsJinianCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RRReminderlist();
        }

        private void FileTreeView_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }
        #region 语音命令
        static SpeechSynthesizer SS = new SpeechSynthesizer();
        private SpeechRecognitionEngine SRE = new SpeechRecognitionEngine(); //语音识别模块
        private bool SRE_listening = false;
        private int wordid;
        private string shibie;
        private string richTextSubNodeID;

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        public void InitVoice()  //语音识别初始化
        {
            SRE.SetInputToDefaultAudioDevice();  // 默认的语音输入设备，也可以设定为去识别一个WAV文

            GrammarBuilder GB = new GrammarBuilder();

            GB.Append(new Choices(new string[] { "查看任务", "打开日历", "退出程序" }));

            DictationGrammar DG = new DictationGrammar();

            Grammar G = new Grammar(GB);

            G.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);  //注册语音识别事件

            SRE.EndSilenceTimeout = TimeSpan.FromSeconds(2);

            SRE.LoadGrammar(G);

        }

        void G_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //Beep(500, 500);//已识别提示音

            string result = e.Result.Text;
            switch (result)
            {
                case "查看任务":
                    shibie = "中国：五星红旗";
                    choice(0);
                    break;
                case "打开日历":
                    shibie = "美国：星条旗";
                    choice(1);
                    break;
                case "退出程序":
                    shibie = "推出程序";
                    choice(2);
                    break;
            }

        }

        private void choice(int id)
        {
            wordid = id;

            Thread t1;
            Thread t2;

            t1 = new Thread(new ThreadStart(ShowAnswer));
            t1.Start();
            t1.Join();
            t2 = new Thread(new ThreadStart(SpeekAnswer));
            t2.Start();
        }
        void ShowAnswer()  //线程
        {
            MethodInvoker mi = new MethodInvoker(this.dosomething);
            this.BeginInvoke(mi);

        }
        void dosomething()
        {
            //textBox1.Text = shibie;
        }
        void SpeekAnswer()  //线程
        {
            switch (wordid)
            {
                case 0:
                    SS.Speak("查看任务");
                    MyShow();
                    break;
                case 1:
                    SS.Speak("打开日历");
                    showcalander();
                    break;
                case 2:
                    SS.Speak("退出程序");
                    Application.Exit();
                    break;
            }
        }
        #endregion
        /// <summary>
        /// 语音控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c_speechcontrol_CheckedChanged(object sender, EventArgs e)
        {
            if (c_speechcontrol.Checked)
            {
                SRE.RecognizeAsync(RecognizeMode.Multiple);
                SRE_listening = true;
            }
            else
            {
                SRE.RecognizeAsyncStop();
                SRE_listening = false;
            }
        }

        public bool MyProcessStart(string path)
        {
            try
            {
                Process.Start(path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #region 文件操作日志
        string firstname = "";
        private bool showwaittask = false;

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                fileSystemWatcher1.EnableRaisingEvents = false;
                //发生改变
                if ((isLog(e.FullPath)) && firstname != e.FullPath.ToString())
                {
                    firstname = e.FullPath.ToString();
                    SaveFileLog("修改：" + e.FullPath.ToString());
                }
            }
            finally
            {
                fileSystemWatcher1.EnableRaisingEvents = true;
            }
        }

        private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {//新增
            SaveFileLog("新增：" + e.FullPath.ToString());
        }

        private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {//删除
            SaveFileLog("删除：" + e.FullPath.ToString());
        }

        private void fileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            SaveFileLog("更名：" + e.OldFullPath.ToString() + "To" + e.Name);
        }
        public bool isLog(string path)
        {
            if (path.Contains(".git") || path.Contains(".svn") || path.Contains(".vs") || path.Contains("~"))
            {
                return false;
            }
            return true;
        }
        public void SaveFileLog(string log)
        {
            if (log.Contains(clipordFilePath))
            {
                return;
            }
            System.IO.Directory.CreateDirectory(clipordFilePath + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\");
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(clipordFilePath + "\\\\" + DateTime.Now.Year + "\\\\" + DateTime.Now.Month + "\\\\" + DateTime.Now.Day.ToString() + "_File.txt", true))
            {
                if (log != "")
                {
                    file.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "   ");
                    file.WriteLine(log);
                }
            }
        }
        #endregion

        private void irisSkinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = "";
            ini.WriteString("Skin", "src", skinEngine1.SkinFile);
        }

        private void c_endtime_CheckedChanged(object sender, EventArgs e)
        {
            RRReminderlist();
        }

        private void 时间快ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new TimeBlockReport()));
            thCalendarForm.Start();
            MyHide();
            return;
        }

        private void 使用记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new UseTime()));
            thCalendarForm.Start();
            MyHide();
            return;
        }

        private void 键盘分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new KeyHours()));
            thCalendarForm.Start();
            MyHide();
            return;
        }

        private void titleTimer_Tick(object sender, EventArgs e)
        {
            this.Text = this.Text.Split('@')[0] + "@未记录时间：" + (DateTime.Now - TimeBlocklastTime).ToString(@"hh\:mm\:ss") + "   现在时间：" + DateTime.Now.ToString("HH:mm:ss");
        }

        private void menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //只需要遍历树的Tab就可以了
                if (nodetreeSearch.Text.Trim() != "")
                {
                    SearchTreeNode(nodetree.Nodes, nodetreeSearch.Text.Trim());
                    SearchTreeNode(FileTreeView.Nodes, nodetreeSearch.Text.Trim());
                }
                else
                {
                    nodetree.CollapseAll();
                    FileTreeView.CollapseAll();
                }
                if (nodetree.Top != nodetreeTopTop)
                {
                    nodetree.Top = FileTreeView.Top = nodetreeTopTop;
                    nodetree.Height = FileTreeView.Height = nodeTreeHeightMax;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void SearchTreeNode(TreeNodeCollection nodes, string searchWorld)
        {
            foreach (TreeNode item in nodes)
            {
                if (searchWorld == "red")
                {
                    item.ForeColor = Color.Gray;
                    if (item.Nodes.Count > 0)
                    {
                        SearchTreeNode(item.Nodes, "red");
                    }
                    continue;
                }
                string tagstring = "";
                try
                {
                    if (item.Tag != null)
                    {
                        tagstring = ((System.Xml.XmlNode)item.Tag).OuterXml;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        if (item.Tag != null)
                        {
                            tagstring = item.Tag.SafeToString();
                        }
                        else
                        {
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                if (tagstring.Contains(searchWorld) || item.Text.Contains(searchWorld))
                {
                    if (item.Text.Contains(searchWorld))
                    {
                        item.ForeColor = Color.Red;
                    }
                    if (item.Nodes.Count > 0)
                    {
                        item.ExpandAll();
                        SearchTreeNode(item.Nodes, searchWorld);
                    }
                }
                else
                {
                    item.ForeColor = Color.Gray;
                    if (item.Nodes.Count > 0)
                    {
                        SearchTreeNode(item.Nodes, "red");
                    }
                    item.Collapse();
                }
            }
        }

        private void nodetreeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //只需要遍历树的Tab就可以了
                if (nodetreeSearch.Text.Trim() != "")
                {
                    SearchTreeNode(nodetree.Nodes, nodetreeSearch.Text.Trim());
                    SearchTreeNode(FileTreeView.Nodes, nodetreeSearch.Text.Trim());
                }
                else
                {
                    nodetree.CollapseAll();
                    FileTreeView.CollapseAll();
                }
                if (nodetree.Top != nodetreeTopTop)
                {
                    nodetree.Top = FileTreeView.Top = nodetreeTopTop;
                    nodetree.Height = FileTreeView.Height = nodeTreeHeightMax;
                }
                nodetree.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                nodetree.Focus();
            }
        }

        private void hopeNote_TextChanged(object sender, EventArgs e)
        {
            try
            {
                hopeNote.ForeColor = Color.Gray;
                if (hopeNote.Text.Trim() == "")
                {
                    hopeNote.Height = 50;
                    return;
                }
                int EM_GETLINECOUNT = 0x00BA;
                int lc = SendMessage(this.hopeNote.Handle, EM_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero);
                int sf = this.hopeNote.Font.Height * (lc + 1);
                this.hopeNote.Height = sf;
                hopeNote.SaveFile(System.AppDomain.CurrentDomain.BaseDirectory + PathcomboBox.SelectedItem.ToString() + @".txt");
            }
            catch (Exception ex)
            {
            }
        }

        private void hopeNote_SizeChanged(object sender, EventArgs e)
        {
            hopeNote.Top = richTextSubNode.Top + richTextSubNode.Height + (richTextSubNode.Height != 0 ? 14 : 0);
            tagCloudControl.Top = hopeNote.Top + hopeNote.Height + 14;
            tagCloudControl.Height = 475 - tagCloudControl.Top;
        }

        private void keyJ_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{j}");
        }
        public int focusedList = 0;
        private bool setmindmapcheck = false;

        private void reminderListBox_Enter(object sender, EventArgs e)
        {
            focusedList = 1;
            SwitchToLanguageMode();
        }

        private void reminderList_Enter(object sender, EventArgs e)
        {
            focusedList = 0;
            SwitchToLanguageMode();
        }

        private void 趋势ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new TimeBlockTrend()));
            thCalendarForm.Start();
            MyHide();
            return;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (StartRecordCheckBox.Checked)
            {
                System.IO.Directory.CreateDirectory(clipordFilePath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "radio");
                record.StartRecord((clipordFilePath + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "radio" + "\\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".wav").Replace(@"\\", @"\"));
            }
            else
            {
                record.StopRecord();
            }
        }

        private void 目标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new Target()));
            thCalendarForm.Start();
            MyHide();
            return;
        }
        //连接摄像头
        public void CameraConn()
        {
            VideoCaptureDevice videoSource = new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[0].MonikerString);
            videoSource.DesiredFrameSize = new System.Drawing.Size(320, 240);
            videoSource.DesiredFrameRate = 1;
            videoSourcePlayer1.VideoSource = videoSource;
            videoSourcePlayer1.Start();
        }

        public void CameraTimer_Tick(object sender, EventArgs e)
        {
            if (!Camera||!checkBox_截图.Checked)
            {
                return;
            }
            try
            {
                //先关闭一次，避免被占用
                if (videoSourcePlayer1 != null && videoSourcePlayer1.IsRunning)
                {
                    videoSourcePlayer1.SignalToStop();
                    videoSourcePlayer1.WaitForStop();
                }
                CameraConn();
                pingmu();
                Thread.Sleep(4000);
                if (videoSourcePlayer1.IsRunning)
                {
                    BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                    videoSourcePlayer1.GetCurrentVideoFrame().GetHbitmap(),
                                    IntPtr.Zero,
                                     Int32Rect.Empty,
                                    BitmapSizeOptions.FromEmptyOptions());
                    PngBitmapEncoder pE = new PngBitmapEncoder();
                    pE.Frames.Add(BitmapFrame.Create(bitmapSource));
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "\\Camera\\"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + "\\Camera\\");
                    }
                    string picName = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\Camera\\" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";
                    picName = picName.Replace("\\\\", "\\");
                    if (File.Exists(picName))
                    {
                        File.Delete(picName);
                    }
                    using (Stream stream = File.Create(picName))
                    {
                        pE.Save(stream);
                    }
                    //CompressImage(picName, picName.Replace("A.jpg", ".jpg"));
                    //File.Delete(picName);
                    //拍照完成后关摄像头并刷新同时关窗体
                    if (videoSourcePlayer1 != null && videoSourcePlayer1.IsRunning)
                    {
                        videoSourcePlayer1.SignalToStop();
                        videoSourcePlayer1.WaitForStop();
                    }
                    //this.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("摄像头异常：" + ex.Message);
                //异常时关闭
                try
                {
                    if (videoSourcePlayer1 != null && videoSourcePlayer1.IsRunning)
                    {
                        videoSourcePlayer1.SignalToStop();
                        videoSourcePlayer1.WaitForStop();
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片地址</param>
        /// <param name="dFile">压缩后保存图片地址</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        public static bool CompressImage(string sFile, string dFile, int flag = 90, int size = 300, bool sfsc = true)
        {
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(sFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                firstFileInfo.CopyTo(dFile);
                return true;
            }
            Image iSource = Image.FromFile(sFile); ImageFormat tFormat = iSource.RawFormat;
            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                    FileInfo fi = new FileInfo(dFile);
                    if (fi.Length > 1024 * size)
                    {
                        flag = flag - 10;
                        CompressImage(sFile, dFile, flag, size, false);
                    }
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }

        private void 导图分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thCalendarForm = new Thread(() => Application.Run(new MindMapDataReport()));
            thCalendarForm.Start();
            MyHide();
            return;
        }
        string piclink = "";
        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Process.Start(piclink);
                MyHide();
            }
            catch (Exception ex)
            {
            }
        }

        private void 打开文件夹ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new FileInfo(piclink).Directory.FullName);
                MyHide();
            }
            catch (Exception ex)
            {
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new FileInfo(piclink).Directory.FullName);
                MyHide();
            }
            catch (Exception ex)
            {
            }
        }

        private void reminderList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            MyListBoxItemRemind selectedReminder = (MyListBoxItemRemind)reminderlistSelectedItem;
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(selectedReminder.Value);
            string taskName = selectedReminder.IDinXML;
            System.Array arr = (System.Array)e.Data.GetData(DataFormats.FileDrop);

            foreach (XmlNode node in x.GetElementsByTagName("hook"))
            {
                try
                {
                    if (node.ParentNode.Attributes["ID"].Value == taskName)
                    {
                        foreach (string item in arr)
                        {
                            if (item.EndsWith("jpg") || item.EndsWith("png"))
                            {
                                DirectoryInfo dir = new FileInfo(selectedReminder.Value).Directory;
                                System.IO.Directory.CreateDirectory(dir.FullName + "\\.images");
                                string filename = dir.FullName + "\\.images" + "\\" + new FileInfo(item).Name;
                                if (System.IO.File.Exists(filename))
                                {
                                    filename = filename.Replace(new FileInfo(item).Name, new FileInfo(item).Name.Replace(".", DateTime.Now.ToFileTimeUtc() + "."));
                                    System.IO.File.Copy(item, filename);
                                }
                                else
                                {
                                    System.IO.File.Copy(item, filename);
                                }
                                if (getPicture(node.ParentNode, selectedReminder.Value) != "")//如果已经有图片
                                {
                                    foreach (XmlNode chilenode in node.ParentNode.ChildNodes)
                                    {
                                        if (((System.Xml.XmlElement)chilenode).Name == "hook" && chilenode.Attributes["NAME"].Value == "ExternalObject")
                                        {
                                            chilenode.Attributes["URI"].Value = ".images" + "/" + new FileInfo(filename).Name;
                                            double size = 200.0 / Image.FromFile(filename).Width;
                                            chilenode.Attributes["SIZE"].Value = size.ToString();
                                            x.Save(selectedReminder.Value);
                                            Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                                            th.Start();
                                            return;
                                        }
                                    }
                                }
                                else//设置图片
                                {
                                    XmlNode newElem = x.CreateElement("hook");
                                    XmlAttribute SIZE = x.CreateAttribute("SIZE");
                                    double size = 200.0 / Image.FromFile(filename).Width;
                                    SIZE.Value = size.ToString();
                                    newElem.Attributes.Append(SIZE);
                                    XmlAttribute NAME = x.CreateAttribute("NAME");
                                    NAME.Value = "ExternalObject";
                                    newElem.Attributes.Append(NAME);
                                    XmlAttribute URI = x.CreateAttribute("URI");
                                    URI.Value = ".images" + "/" + new FileInfo(filename).Name;
                                    newElem.Attributes.Append(URI);
                                    node.ParentNode.AppendChild(newElem);
                                    x.Save(selectedReminder.Value);
                                    Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(selectedReminder.Value));
                                    th.Start();
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        string RelativePath(string absolutePath, string relativeTo)
        {
            //from - www.cnphp6.com

            string[] absoluteDirectories = absolutePath.Split('\\');
            string[] relativeDirectories = relativeTo.Split('\\');

            //Get the shortest of the two paths
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;

            //Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            //Find common root
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;

            //If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
                throw new ArgumentException("Paths do not have a common base");

            //Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            //Add on the ..
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                if (absoluteDirectories[index].Length > 0)
                    relativePath.Append("..\\");

            //Add on the folders
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                relativePath.Append(relativeDirectories[index] + "\\");
            relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);

            return relativePath.ToString();
        }
        private void reminderList_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = System.Windows.Forms.DragDropEffects.All;
            }
            else
            {
                e.Effect = System.Windows.Forms.DragDropEffects.None;
            }
        }

        private void DocearReminderForm_Activated(object sender, EventArgs e)
        {
            thisactive = true;
        }
        public static bool quietmodelbool = false;
        private void quietmode_CheckedChanged(object sender, EventArgs e)
        {
            quietmodelbool = quietmode.Checked;
        }

        private void ShowTimeBlockChange(object sender, EventArgs e)
        {
            if (showTimeBlock.Checked)
            {
                RRReminderlist();
                ShowMoney.Checked = ShowKA.Checked = false;
            }
            IFNoCheckedRRR();
        }

        private void TimeBlockDate_ValueChanged(object sender, EventArgs e)
        {
            RRReminderlist();
            TimeBlockDate.Focus();//继续选中
        }

        private void ShowMoney_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowMoney.Checked)
            {
                RRReminderlist();
                showTimeBlock.Checked = ShowKA.Checked = false;
            }
            IFNoCheckedRRR();
        }

        private void ShowKA_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowKA.Checked)
            {
                RRReminderlist();
                showTimeBlock.Checked = ShowMoney.Checked = false;
            }
            IFNoCheckedRRR();
        }
        public void IFNoCheckedRRR()
        {
            if (!ShowKA.Checked && !showTimeBlock.Checked && !ShowMoney.Checked)
            {
                RRReminderlist();
            }
        }

        private void MoneyDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            RRReminderlist();
            MoneyDateTimePicker.Focus();//继续选中
        }

        private void KADateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            RRReminderlist();
            KADateTimePicker.Focus();//继续选中
        }

        private void timeblockcheck_Click(object sender, EventArgs e)
        {

        }

        private void OnlyLevel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void IsClip_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AllnodeFreshTimer_Tick(object sender, EventArgs e)
        {
            GetAllNodeJsonFile();
        }
    }

    class MoveOverInfoTip
    {
        #region   基础参数
        //信息提示组件
        private static ToolTip _toolTip = new ToolTip();
        #endregion
        #region   公有方法
        /// <summary>
        /// 设置单个控件提示信息
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="t">组件</param>
        /// <param name="tipInfo">需要显示的提示信息</param>
        public static void SettingSingleTipInfo<T>(T t, string tipInfo) where T : Control
        {
            _toolTip.SetToolTip(t, tipInfo);
        }
        /// <summary>
        /// 设置多个同种类型的提示信息
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="dic">组件和提示信息字典</param>
        public static void SettingMutiTipInfo<T>(Dictionary<T, string> dic) where T : Control
        {
            if (dic == null || dic.Count <= 0) return;

            foreach (var item in dic)
            {
                _toolTip.SetToolTip(item.Key, item.Value);
            }

        }
        #endregion
        #region   私有方法
        #endregion
    }
}

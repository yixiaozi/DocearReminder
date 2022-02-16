using System.Windows.Forms;
using yixiaozi.WinForm.Control.TagCloud;
using yixiaozi.WinForm.Control;
using System;

namespace DocearReminder
{
    partial class DocearReminderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocearReminderForm));
            this.searchword = new System.Windows.Forms.TextBox();
            this.searchworkmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reminder_week = new System.Windows.Forms.CheckBox();
            this.reminder_month = new System.Windows.Forms.CheckBox();
            this.reminder_year = new System.Windows.Forms.CheckBox();
            this.reminder_yearafter = new System.Windows.Forms.CheckBox();
            this.mindmaplist_count = new System.Windows.Forms.Label();
            this.reminder_count = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.n_days = new System.Windows.Forms.NumericUpDown();
            this.button_cycle = new System.Windows.Forms.Button();
            this.c_Monday = new System.Windows.Forms.CheckBox();
            this.c_Tuesday = new System.Windows.Forms.CheckBox();
            this.c_Wednesday = new System.Windows.Forms.CheckBox();
            this.c_Thursday = new System.Windows.Forms.CheckBox();
            this.c_Friday = new System.Windows.Forms.CheckBox();
            this.c_Saturday = new System.Windows.Forms.CheckBox();
            this.c_day = new System.Windows.Forms.CheckBox();
            this.c_week = new System.Windows.Forms.CheckBox();
            this.c_month = new System.Windows.Forms.CheckBox();
            this.c_year = new System.Windows.Forms.CheckBox();
            this.c_Sunday = new System.Windows.Forms.CheckBox();
            this.taskTime = new System.Windows.Forms.NumericUpDown();
            this.fenshu = new System.Windows.Forms.Label();
            this.hourLeft = new System.Windows.Forms.Label();
            this.Hours = new System.Windows.Forms.Label();
            this.showcyclereminder = new System.Windows.Forms.CheckBox();
            this.showtomorrow = new System.Windows.Forms.CheckBox();
            this.SearchText_suggest = new System.Windows.Forms.ListBox();
            this.tasklevel = new System.Windows.Forms.NumericUpDown();
            this.c_hour = new System.Windows.Forms.CheckBox();
            this.IsViewModel = new System.Windows.Forms.CheckBox();
            this.afternoon = new System.Windows.Forms.CheckBox();
            this.day = new System.Windows.Forms.CheckBox();
            this.morning = new System.Windows.Forms.CheckBox();
            this.night = new System.Windows.Forms.CheckBox();
            this.mindmapSearch = new System.Windows.Forms.TextBox();
            this.IsJinianCheckBox = new System.Windows.Forms.CheckBox();
            this.onlyZhouqi = new System.Windows.Forms.CheckBox();
            this.nodetree = new System.Windows.Forms.TreeView();
            this.menu_reminderlist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_oktask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_deny = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCalcal = new System.Windows.Forms.ToolStripMenuItem();
            this.仅查看CdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.非重要ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.设置重要xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开所在目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.快捷键说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下一个jToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上一个kToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Rsstimer = new System.Windows.Forms.Timer(this.components);
            this.c_remember = new System.Windows.Forms.CheckBox();
            this.ebcheckBox = new System.Windows.Forms.CheckBox();
            this.FileTreeView = new System.Windows.Forms.TreeView();
            this.PathcomboBox = new System.Windows.Forms.ComboBox();
            this.IsReminderOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.menu_mindmaps = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_nodetree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_filetree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单次ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.周期ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.非重要ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导图查看模式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disAutoRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具箱ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日历QToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切板文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作记录F12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.透明度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.o05ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.o08ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.o1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示树视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏树视图SnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.是否锁定窗口lockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.是否播放声音playsoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示右侧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开程序目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.推出F11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.taskcount = new System.Windows.Forms.Label();
            this.mindmapornode = new System.Windows.Forms.Label();
            this.DAKAINFO = new System.Windows.Forms.Label();
            this.panel_clearSearchWord = new System.Windows.Forms.Panel();
            this.fathernode = new System.Windows.Forms.Label();
            this.labeltaskinfo = new System.Windows.Forms.Label();
            this.usedCount = new System.Windows.Forms.Label();
            this.usedtimelabel = new System.Windows.Forms.Label();
            this.todayusedtime = new System.Windows.Forms.Label();
            this.richTextSubNode = new yixiaozi.WinForm.Control.MyRichTextBox();
            this.tagCloudControl = new yixiaozi.WinForm.Control.TagCloud.TagCloudControl();
            this.noterichTextBox = new yixiaozi.WinForm.Control.MyRichTextBox();
            this.reminderListBox = new yixiaozi.WinForm.Control.SortByTimeListBox();
            this.reminderList = new yixiaozi.WinForm.Control.SortByTimeListBox();
            this.mindmaplist = new yixiaozi.WinForm.Control.CustomCheckedListBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.EditTaskTime = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.searchworkmenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_days)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasklevel)).BeginInit();
            this.menu_reminderlist.SuspendLayout();
            this.menu_mindmaps.SuspendLayout();
            this.menu_nodetree.SuspendLayout();
            this.menu_filetree.SuspendLayout();
            this.menu.SuspendLayout();
            this.panel_clearSearchWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchword
            // 
            this.searchword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchword.ContextMenuStrip = this.searchworkmenu;
            this.searchword.Font = new System.Drawing.Font("宋体", 9F);
            this.searchword.ForeColor = System.Drawing.Color.Gray;
            this.searchword.Location = new System.Drawing.Point(260, 9);
            this.searchword.Name = "searchword";
            this.searchword.Size = new System.Drawing.Size(600, 21);
            this.searchword.TabIndex = 2;
            this.searchword.TabStop = false;
            this.searchword.TextChanged += new System.EventHandler(this.Searchword_TextChanged);
            this.searchword.Enter += new System.EventHandler(this.Searchword_Enter);
            this.searchword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchword_KeyPress);
            this.searchword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Searchword_KeyUp);
            this.searchword.Leave += new System.EventHandler(this.Searchword_Leave);
            this.searchword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Searchword_MouseDown);
            // 
            // searchworkmenu
            // 
            this.searchworkmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.窗口ToolStripMenuItem});
            this.searchworkmenu.Name = "searchworkmenu";
            this.searchworkmenu.Size = new System.Drawing.Size(101, 26);
            // 
            // 窗口ToolStripMenuItem
            // 
            this.窗口ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具toolToolStripMenuItem});
            this.窗口ToolStripMenuItem.Name = "窗口ToolStripMenuItem";
            this.窗口ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.窗口ToolStripMenuItem.Text = "窗口";
            // 
            // 工具toolToolStripMenuItem
            // 
            this.工具toolToolStripMenuItem.Name = "工具toolToolStripMenuItem";
            this.工具toolToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.工具toolToolStripMenuItem.Text = "工具：tool";
            // 
            // reminder_week
            // 
            this.reminder_week.AutoSize = true;
            this.reminder_week.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_week.Location = new System.Drawing.Point(886, 536);
            this.reminder_week.Name = "reminder_week";
            this.reminder_week.Size = new System.Drawing.Size(13, 12);
            this.reminder_week.TabIndex = 7;
            this.reminder_week.TabStop = false;
            this.reminder_week.UseVisualStyleBackColor = true;
            this.reminder_week.Visible = false;
            this.reminder_week.CheckedChanged += new System.EventHandler(this.Reminder_week_CheckedChanged);
            // 
            // reminder_month
            // 
            this.reminder_month.AutoSize = true;
            this.reminder_month.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_month.Location = new System.Drawing.Point(899, 535);
            this.reminder_month.Name = "reminder_month";
            this.reminder_month.Size = new System.Drawing.Size(13, 12);
            this.reminder_month.TabIndex = 8;
            this.reminder_month.TabStop = false;
            this.reminder_month.UseVisualStyleBackColor = true;
            this.reminder_month.Visible = false;
            this.reminder_month.CheckedChanged += new System.EventHandler(this.Reminder_month_CheckedChanged);
            // 
            // reminder_year
            // 
            this.reminder_year.AutoSize = true;
            this.reminder_year.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_year.Location = new System.Drawing.Point(915, 535);
            this.reminder_year.Name = "reminder_year";
            this.reminder_year.Size = new System.Drawing.Size(13, 12);
            this.reminder_year.TabIndex = 9;
            this.reminder_year.TabStop = false;
            this.reminder_year.UseVisualStyleBackColor = true;
            this.reminder_year.Visible = false;
            this.reminder_year.CheckedChanged += new System.EventHandler(this.Reminder_year_CheckedChanged);
            // 
            // reminder_yearafter
            // 
            this.reminder_yearafter.AutoSize = true;
            this.reminder_yearafter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_yearafter.Location = new System.Drawing.Point(872, 553);
            this.reminder_yearafter.Name = "reminder_yearafter";
            this.reminder_yearafter.Size = new System.Drawing.Size(13, 12);
            this.reminder_yearafter.TabIndex = 10;
            this.reminder_yearafter.TabStop = false;
            this.reminder_yearafter.UseVisualStyleBackColor = true;
            this.reminder_yearafter.Visible = false;
            this.reminder_yearafter.CheckedChanged += new System.EventHandler(this.Reminder_yearafter_CheckedChanged);
            // 
            // mindmaplist_count
            // 
            this.mindmaplist_count.AutoSize = true;
            this.mindmaplist_count.Location = new System.Drawing.Point(14, 483);
            this.mindmaplist_count.Name = "mindmaplist_count";
            this.mindmaplist_count.Size = new System.Drawing.Size(11, 12);
            this.mindmaplist_count.TabIndex = 11;
            this.mindmaplist_count.Text = "0";
            // 
            // reminder_count
            // 
            this.reminder_count.AutoSize = true;
            this.reminder_count.Location = new System.Drawing.Point(329, 483);
            this.reminder_count.Name = "reminder_count";
            this.reminder_count.Size = new System.Drawing.Size(11, 12);
            this.reminder_count.TabIndex = 12;
            this.reminder_count.Text = "0";
            this.reminder_count.Click += new System.EventHandler(this.Reminder_count_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CalendarFont = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker.CalendarForeColor = System.Drawing.Color.Gray;
            this.dateTimePicker.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePicker.CalendarTitleBackColor = System.Drawing.Color.White;
            this.dateTimePicker.CalendarTitleForeColor = System.Drawing.Color.Gray;
            this.dateTimePicker.CalendarTrailingForeColor = System.Drawing.Color.Gray;
            this.dateTimePicker.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateTimePicker.CustomFormat = "HH:mm MM月dd dddd";
            this.dateTimePicker.Font = new System.Drawing.Font("宋体", 8F);
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(872, 9);
            this.dateTimePicker.MaxDate = new System.DateTime(2499, 12, 17, 0, 0, 0, 0);
            this.dateTimePicker.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(146, 20);
            this.dateTimePicker.TabIndex = 17;
            this.dateTimePicker.TabStop = false;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            this.dateTimePicker.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker_KeyUp);
            this.dateTimePicker.MouseLeave += new System.EventHandler(this.dateTimePicker_MouseLeave);
            // 
            // n_days
            // 
            this.n_days.ForeColor = System.Drawing.Color.Gray;
            this.n_days.Location = new System.Drawing.Point(1117, 9);
            this.n_days.Name = "n_days";
            this.n_days.Size = new System.Drawing.Size(35, 21);
            this.n_days.TabIndex = 20;
            this.n_days.TabStop = false;
            // 
            // button_cycle
            // 
            this.button_cycle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cycle.ForeColor = System.Drawing.Color.Gray;
            this.button_cycle.Location = new System.Drawing.Point(1027, 33);
            this.button_cycle.Name = "button_cycle";
            this.button_cycle.Size = new System.Drawing.Size(128, 30);
            this.button_cycle.TabIndex = 18;
            this.button_cycle.TabStop = false;
            this.button_cycle.Text = "设置周期";
            this.button_cycle.UseVisualStyleBackColor = true;
            this.button_cycle.Click += new System.EventHandler(this.button_cycle_Click);
            // 
            // c_Monday
            // 
            this.c_Monday.AutoSize = true;
            this.c_Monday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_Monday.Location = new System.Drawing.Point(872, 50);
            this.c_Monday.Name = "c_Monday";
            this.c_Monday.Size = new System.Drawing.Size(13, 12);
            this.c_Monday.TabIndex = 27;
            this.c_Monday.TabStop = false;
            this.c_Monday.UseVisualStyleBackColor = true;
            // 
            // c_Tuesday
            // 
            this.c_Tuesday.AutoSize = true;
            this.c_Tuesday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_Tuesday.Location = new System.Drawing.Point(891, 50);
            this.c_Tuesday.Name = "c_Tuesday";
            this.c_Tuesday.Size = new System.Drawing.Size(13, 12);
            this.c_Tuesday.TabIndex = 28;
            this.c_Tuesday.TabStop = false;
            this.c_Tuesday.UseVisualStyleBackColor = true;
            // 
            // c_Wednesday
            // 
            this.c_Wednesday.AutoSize = true;
            this.c_Wednesday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_Wednesday.Location = new System.Drawing.Point(910, 50);
            this.c_Wednesday.Name = "c_Wednesday";
            this.c_Wednesday.Size = new System.Drawing.Size(13, 12);
            this.c_Wednesday.TabIndex = 29;
            this.c_Wednesday.TabStop = false;
            this.c_Wednesday.UseVisualStyleBackColor = true;
            // 
            // c_Thursday
            // 
            this.c_Thursday.AutoSize = true;
            this.c_Thursday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_Thursday.Location = new System.Drawing.Point(929, 50);
            this.c_Thursday.Name = "c_Thursday";
            this.c_Thursday.Size = new System.Drawing.Size(13, 12);
            this.c_Thursday.TabIndex = 30;
            this.c_Thursday.TabStop = false;
            this.c_Thursday.UseVisualStyleBackColor = true;
            // 
            // c_Friday
            // 
            this.c_Friday.AutoSize = true;
            this.c_Friday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_Friday.Location = new System.Drawing.Point(948, 50);
            this.c_Friday.Name = "c_Friday";
            this.c_Friday.Size = new System.Drawing.Size(13, 12);
            this.c_Friday.TabIndex = 31;
            this.c_Friday.TabStop = false;
            this.c_Friday.UseVisualStyleBackColor = true;
            // 
            // c_Saturday
            // 
            this.c_Saturday.AutoSize = true;
            this.c_Saturday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_Saturday.Location = new System.Drawing.Point(967, 50);
            this.c_Saturday.Name = "c_Saturday";
            this.c_Saturday.Size = new System.Drawing.Size(13, 12);
            this.c_Saturday.TabIndex = 32;
            this.c_Saturday.TabStop = false;
            this.c_Saturday.UseVisualStyleBackColor = true;
            // 
            // c_day
            // 
            this.c_day.AutoSize = true;
            this.c_day.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_day.Location = new System.Drawing.Point(872, 35);
            this.c_day.Name = "c_day";
            this.c_day.Size = new System.Drawing.Size(13, 12);
            this.c_day.TabIndex = 34;
            this.c_day.TabStop = false;
            this.c_day.UseVisualStyleBackColor = true;
            this.c_day.CheckedChanged += new System.EventHandler(this.c_day_CheckedChanged);
            // 
            // c_week
            // 
            this.c_week.AutoSize = true;
            this.c_week.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_week.Location = new System.Drawing.Point(892, 35);
            this.c_week.Name = "c_week";
            this.c_week.Size = new System.Drawing.Size(13, 12);
            this.c_week.TabIndex = 35;
            this.c_week.TabStop = false;
            this.c_week.UseVisualStyleBackColor = true;
            this.c_week.CheckedChanged += new System.EventHandler(this.c_week_CheckedChanged);
            // 
            // c_month
            // 
            this.c_month.AutoSize = true;
            this.c_month.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_month.Location = new System.Drawing.Point(910, 35);
            this.c_month.Name = "c_month";
            this.c_month.Size = new System.Drawing.Size(13, 12);
            this.c_month.TabIndex = 36;
            this.c_month.TabStop = false;
            this.c_month.UseVisualStyleBackColor = true;
            this.c_month.CheckedChanged += new System.EventHandler(this.c_month_CheckedChanged);
            // 
            // c_year
            // 
            this.c_year.AutoSize = true;
            this.c_year.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_year.Location = new System.Drawing.Point(929, 35);
            this.c_year.Name = "c_year";
            this.c_year.Size = new System.Drawing.Size(13, 12);
            this.c_year.TabIndex = 37;
            this.c_year.TabStop = false;
            this.c_year.UseVisualStyleBackColor = true;
            this.c_year.CheckedChanged += new System.EventHandler(this.c_year_CheckedChanged);
            // 
            // c_Sunday
            // 
            this.c_Sunday.AutoSize = true;
            this.c_Sunday.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_Sunday.Location = new System.Drawing.Point(986, 50);
            this.c_Sunday.Name = "c_Sunday";
            this.c_Sunday.Size = new System.Drawing.Size(13, 12);
            this.c_Sunday.TabIndex = 38;
            this.c_Sunday.TabStop = false;
            this.c_Sunday.UseVisualStyleBackColor = true;
            // 
            // taskTime
            // 
            this.taskTime.ForeColor = System.Drawing.Color.Gray;
            this.taskTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.taskTime.Location = new System.Drawing.Point(1027, 9);
            this.taskTime.Maximum = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.taskTime.Name = "taskTime";
            this.taskTime.Size = new System.Drawing.Size(35, 21);
            this.taskTime.TabIndex = 15;
            this.taskTime.TabStop = false;
            this.taskTime.ValueChanged += new System.EventHandler(this.taskTime_ValueChanged);
            // 
            // fenshu
            // 
            this.fenshu.AutoSize = true;
            this.fenshu.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fenshu.Location = new System.Drawing.Point(258, 483);
            this.fenshu.Name = "fenshu";
            this.fenshu.Size = new System.Drawing.Size(11, 12);
            this.fenshu.TabIndex = 76;
            this.fenshu.Text = "0";
            // 
            // hourLeft
            // 
            this.hourLeft.AutoSize = true;
            this.hourLeft.Location = new System.Drawing.Point(560, 483);
            this.hourLeft.Name = "hourLeft";
            this.hourLeft.Size = new System.Drawing.Size(11, 12);
            this.hourLeft.TabIndex = 14;
            this.hourLeft.Text = "0";
            // 
            // Hours
            // 
            this.Hours.AutoSize = true;
            this.Hours.Location = new System.Drawing.Point(512, 483);
            this.Hours.Name = "Hours";
            this.Hours.Size = new System.Drawing.Size(11, 12);
            this.Hours.TabIndex = 13;
            this.Hours.Text = "0";
            // 
            // showcyclereminder
            // 
            this.showcyclereminder.AutoSize = true;
            this.showcyclereminder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.showcyclereminder.Location = new System.Drawing.Point(872, 501);
            this.showcyclereminder.Name = "showcyclereminder";
            this.showcyclereminder.Size = new System.Drawing.Size(13, 12);
            this.showcyclereminder.TabIndex = 43;
            this.showcyclereminder.TabStop = false;
            this.showcyclereminder.UseVisualStyleBackColor = true;
            this.showcyclereminder.Visible = false;
            this.showcyclereminder.CheckedChanged += new System.EventHandler(this.showcyclereminder_CheckedChanged);
            // 
            // showtomorrow
            // 
            this.showtomorrow.AutoSize = true;
            this.showtomorrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.showtomorrow.Location = new System.Drawing.Point(872, 535);
            this.showtomorrow.Name = "showtomorrow";
            this.showtomorrow.Size = new System.Drawing.Size(13, 12);
            this.showtomorrow.TabIndex = 44;
            this.showtomorrow.TabStop = false;
            this.showtomorrow.UseVisualStyleBackColor = true;
            this.showtomorrow.Visible = false;
            this.showtomorrow.CheckedChanged += new System.EventHandler(this.showtomorrow_CheckedChanged);
            // 
            // SearchText_suggest
            // 
            this.SearchText_suggest.BackColor = System.Drawing.Color.White;
            this.SearchText_suggest.Font = new System.Drawing.Font("宋体", 9.75F);
            this.SearchText_suggest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.SearchText_suggest.FormattingEnabled = true;
            this.SearchText_suggest.Location = new System.Drawing.Point(259, 50);
            this.SearchText_suggest.Name = "SearchText_suggest";
            this.SearchText_suggest.Size = new System.Drawing.Size(600, 95);
            this.SearchText_suggest.TabIndex = 83;
            this.SearchText_suggest.Visible = false;
            this.SearchText_suggest.SelectedIndexChanged += new System.EventHandler(this.SearchText_suggest_SelectedIndexChanged);
            // 
            // tasklevel
            // 
            this.tasklevel.ForeColor = System.Drawing.Color.Gray;
            this.tasklevel.Location = new System.Drawing.Point(1071, 9);
            this.tasklevel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tasklevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.tasklevel.Name = "tasklevel";
            this.tasklevel.Size = new System.Drawing.Size(35, 21);
            this.tasklevel.TabIndex = 48;
            this.tasklevel.TabStop = false;
            this.tasklevel.ValueChanged += new System.EventHandler(this.tasklevel_ValueChanged);
            // 
            // c_hour
            // 
            this.c_hour.AutoSize = true;
            this.c_hour.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_hour.Location = new System.Drawing.Point(948, 35);
            this.c_hour.Name = "c_hour";
            this.c_hour.Size = new System.Drawing.Size(13, 12);
            this.c_hour.TabIndex = 57;
            this.c_hour.TabStop = false;
            this.c_hour.UseVisualStyleBackColor = true;
            this.c_hour.CheckedChanged += new System.EventHandler(this.c_hour_CheckedChanged);
            // 
            // IsViewModel
            // 
            this.IsViewModel.AutoSize = true;
            this.IsViewModel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.IsViewModel.Location = new System.Drawing.Point(1005, 50);
            this.IsViewModel.Name = "IsViewModel";
            this.IsViewModel.Size = new System.Drawing.Size(13, 12);
            this.IsViewModel.TabIndex = 59;
            this.IsViewModel.TabStop = false;
            this.IsViewModel.UseVisualStyleBackColor = true;
            this.IsViewModel.CheckedChanged += new System.EventHandler(this.IsViewModel_CheckedChanged);
            // 
            // afternoon
            // 
            this.afternoon.AutoSize = true;
            this.afternoon.Checked = true;
            this.afternoon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.afternoon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.afternoon.Location = new System.Drawing.Point(899, 517);
            this.afternoon.Name = "afternoon";
            this.afternoon.Size = new System.Drawing.Size(13, 12);
            this.afternoon.TabIndex = 72;
            this.afternoon.UseVisualStyleBackColor = true;
            this.afternoon.Visible = false;
            this.afternoon.CheckedChanged += new System.EventHandler(this.morning_CheckedChanged);
            // 
            // day
            // 
            this.day.AutoSize = true;
            this.day.Checked = true;
            this.day.CheckState = System.Windows.Forms.CheckState.Checked;
            this.day.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.day.Location = new System.Drawing.Point(886, 518);
            this.day.Name = "day";
            this.day.Size = new System.Drawing.Size(13, 12);
            this.day.TabIndex = 73;
            this.day.UseVisualStyleBackColor = true;
            this.day.Visible = false;
            this.day.CheckedChanged += new System.EventHandler(this.morning_CheckedChanged);
            // 
            // morning
            // 
            this.morning.AutoSize = true;
            this.morning.Checked = true;
            this.morning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.morning.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.morning.Location = new System.Drawing.Point(872, 517);
            this.morning.Name = "morning";
            this.morning.Size = new System.Drawing.Size(13, 12);
            this.morning.TabIndex = 74;
            this.morning.UseVisualStyleBackColor = true;
            this.morning.Visible = false;
            this.morning.CheckedChanged += new System.EventHandler(this.morning_CheckedChanged);
            // 
            // night
            // 
            this.night.AutoSize = true;
            this.night.Checked = true;
            this.night.CheckState = System.Windows.Forms.CheckState.Checked;
            this.night.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.night.Location = new System.Drawing.Point(915, 517);
            this.night.Name = "night";
            this.night.Size = new System.Drawing.Size(13, 12);
            this.night.TabIndex = 75;
            this.night.UseVisualStyleBackColor = true;
            this.night.Visible = false;
            this.night.CheckedChanged += new System.EventHandler(this.morning_CheckedChanged);
            // 
            // mindmapSearch
            // 
            this.mindmapSearch.BackColor = System.Drawing.Color.White;
            this.mindmapSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mindmapSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.mindmapSearch.Location = new System.Drawing.Point(12, 33);
            this.mindmapSearch.Name = "mindmapSearch";
            this.mindmapSearch.Size = new System.Drawing.Size(231, 14);
            this.mindmapSearch.TabIndex = 84;
            this.mindmapSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mindmapSearch.TextChanged += new System.EventHandler(this.mindmapSearch_TextChanged);
            // 
            // IsJinianCheckBox
            // 
            this.IsJinianCheckBox.AutoSize = true;
            this.IsJinianCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.IsJinianCheckBox.Location = new System.Drawing.Point(1005, 35);
            this.IsJinianCheckBox.Name = "IsJinianCheckBox";
            this.IsJinianCheckBox.Size = new System.Drawing.Size(13, 12);
            this.IsJinianCheckBox.TabIndex = 86;
            this.IsJinianCheckBox.TabStop = false;
            this.IsJinianCheckBox.UseVisualStyleBackColor = true;
            this.IsJinianCheckBox.CheckedChanged += new System.EventHandler(this.IsJinianCheckBox_CheckedChanged);
            // 
            // onlyZhouqi
            // 
            this.onlyZhouqi.AutoSize = true;
            this.onlyZhouqi.Checked = true;
            this.onlyZhouqi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onlyZhouqi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.onlyZhouqi.Location = new System.Drawing.Point(886, 501);
            this.onlyZhouqi.Name = "onlyZhouqi";
            this.onlyZhouqi.Size = new System.Drawing.Size(13, 12);
            this.onlyZhouqi.TabIndex = 89;
            this.onlyZhouqi.TabStop = false;
            this.onlyZhouqi.UseVisualStyleBackColor = true;
            this.onlyZhouqi.Visible = false;
            this.onlyZhouqi.CheckedChanged += new System.EventHandler(this.onlyZhouqi_CheckedChanged);
            // 
            // nodetree
            // 
            this.nodetree.AllowDrop = true;
            this.nodetree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nodetree.ForeColor = System.Drawing.Color.Gray;
            this.nodetree.FullRowSelect = true;
            this.nodetree.ItemHeight = 14;
            this.nodetree.LabelEdit = true;
            this.nodetree.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.nodetree.Location = new System.Drawing.Point(260, 501);
            this.nodetree.Name = "nodetree";
            this.nodetree.ShowNodeToolTips = true;
            this.nodetree.Size = new System.Drawing.Size(600, 307);
            this.nodetree.TabIndex = 100;
            this.nodetree.Visible = false;
            this.nodetree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.NodeTree_AfterLabelEdit);
            this.nodetree.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TreeView1_DrawNode);
            this.nodetree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.nodetree_AfterSelect);
            this.nodetree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NodeTree_KeyUp);
            this.nodetree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.nodetree_MouseDown);
            this.nodetree.MouseLeave += new System.EventHandler(this.TreeView1_MouseLeave);
            this.nodetree.MouseHover += new System.EventHandler(this.TreeView1_MouseHover);
            this.nodetree.ParentChanged += new System.EventHandler(this.TreeView1_ParentChanged);
            // 
            // menu_reminderlist
            // 
            this.menu_reminderlist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_oktask,
            this.toolStripMenuItem_deny,
            this.toolStripMenuItemCalcal,
            this.仅查看CdToolStripMenuItem,
            this.非重要ToolStripMenuItem1,
            this.设置重要xToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.打开所在目录ToolStripMenuItem,
            this.快捷键说明ToolStripMenuItem});
            this.menu_reminderlist.Name = "contextMenuStrip1";
            this.menu_reminderlist.Size = new System.Drawing.Size(163, 202);
            this.menu_reminderlist.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem_oktask
            // 
            this.toolStripMenuItem_oktask.Image = global::DocearReminder.Properties.Resources.square_ok;
            this.toolStripMenuItem_oktask.Name = "toolStripMenuItem_oktask";
            this.toolStripMenuItem_oktask.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem_oktask.Text = "完成 [O]:ok";
            this.toolStripMenuItem_oktask.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem_deny
            // 
            this.toolStripMenuItem_deny.Name = "toolStripMenuItem_deny";
            this.toolStripMenuItem_deny.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem_deny.Text = "推迟 [D]:deny";
            this.toolStripMenuItem_deny.Click += new System.EventHandler(this.ToolStripMenuItem_deny_Click);
            // 
            // toolStripMenuItemCalcal
            // 
            this.toolStripMenuItemCalcal.Name = "toolStripMenuItemCalcal";
            this.toolStripMenuItemCalcal.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItemCalcal.Text = "取消 [C]";
            this.toolStripMenuItemCalcal.Click += new System.EventHandler(this.toolStripMenuItemCalcal_Click);
            // 
            // 仅查看CdToolStripMenuItem
            // 
            this.仅查看CdToolStripMenuItem.Name = "仅查看CdToolStripMenuItem";
            this.仅查看CdToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.仅查看CdToolStripMenuItem.Text = "仅查看[C+d]";
            this.仅查看CdToolStripMenuItem.Click += new System.EventHandler(this.仅查看CdToolStripMenuItem_Click);
            // 
            // 非重要ToolStripMenuItem1
            // 
            this.非重要ToolStripMenuItem1.Name = "非重要ToolStripMenuItem1";
            this.非重要ToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.非重要ToolStripMenuItem1.Text = "非重要[z]";
            this.非重要ToolStripMenuItem1.Click += new System.EventHandler(this.非重要ToolStripMenuItem1_Click);
            // 
            // 设置重要xToolStripMenuItem
            // 
            this.设置重要xToolStripMenuItem.Name = "设置重要xToolStripMenuItem";
            this.设置重要xToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.设置重要xToolStripMenuItem.Text = "重要[x]";
            this.设置重要xToolStripMenuItem.Click += new System.EventHandler(this.设置重要xToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.openFileToolStripMenuItem.Text = "打开文件[enter]";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // 打开所在目录ToolStripMenuItem
            // 
            this.打开所在目录ToolStripMenuItem.Name = "打开所在目录ToolStripMenuItem";
            this.打开所在目录ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.打开所在目录ToolStripMenuItem.Text = "打开文件夹";
            this.打开所在目录ToolStripMenuItem.Click += new System.EventHandler(this.打开所在目录ToolStripMenuItem_Click);
            // 
            // 快捷键说明ToolStripMenuItem
            // 
            this.快捷键说明ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下一个jToolStripMenuItem,
            this.上一个kToolStripMenuItem});
            this.快捷键说明ToolStripMenuItem.Name = "快捷键说明ToolStripMenuItem";
            this.快捷键说明ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.快捷键说明ToolStripMenuItem.Text = "快捷键说明";
            // 
            // 下一个jToolStripMenuItem
            // 
            this.下一个jToolStripMenuItem.Name = "下一个jToolStripMenuItem";
            this.下一个jToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.下一个jToolStripMenuItem.Text = "下一个[j]";
            // 
            // 上一个kToolStripMenuItem
            // 
            this.上一个kToolStripMenuItem.Name = "上一个kToolStripMenuItem";
            this.上一个kToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.上一个kToolStripMenuItem.Text = "上一个[k]";
            // 
            // Rsstimer
            // 
            this.Rsstimer.Interval = 60000;
            this.Rsstimer.Tick += new System.EventHandler(this.Rsstimer_Tick);
            // 
            // c_remember
            // 
            this.c_remember.AutoSize = true;
            this.c_remember.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_remember.Location = new System.Drawing.Point(967, 35);
            this.c_remember.Name = "c_remember";
            this.c_remember.Size = new System.Drawing.Size(13, 12);
            this.c_remember.TabIndex = 110;
            this.c_remember.UseVisualStyleBackColor = true;
            this.c_remember.CheckedChanged += new System.EventHandler(this.c_remember_CheckedChanged);
            // 
            // ebcheckBox
            // 
            this.ebcheckBox.AutoSize = true;
            this.ebcheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ebcheckBox.Location = new System.Drawing.Point(899, 501);
            this.ebcheckBox.Name = "ebcheckBox";
            this.ebcheckBox.Size = new System.Drawing.Size(13, 12);
            this.ebcheckBox.TabIndex = 111;
            this.ebcheckBox.UseVisualStyleBackColor = true;
            this.ebcheckBox.Visible = false;
            this.ebcheckBox.CheckedChanged += new System.EventHandler(this.ebcheckBox_CheckedChanged);
            // 
            // FileTreeView
            // 
            this.FileTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileTreeView.ForeColor = System.Drawing.Color.Gray;
            this.FileTreeView.FullRowSelect = true;
            this.FileTreeView.LabelEdit = true;
            this.FileTreeView.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.FileTreeView.Location = new System.Drawing.Point(10, 501);
            this.FileTreeView.Name = "FileTreeView";
            this.FileTreeView.ShowNodeToolTips = true;
            this.FileTreeView.Size = new System.Drawing.Size(233, 307);
            this.FileTreeView.TabIndex = 112;
            this.FileTreeView.Visible = false;
            this.FileTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.FileTreeView_AfterLabelEdit);
            this.FileTreeView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.TreeView1_DrawNode);
            this.FileTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.FileTreeView_NodeMouseDoubleClick);
            this.FileTreeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TreeView_KeyUp);
            this.FileTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FileTreeView_MouseDown);
            // 
            // PathcomboBox
            // 
            this.PathcomboBox.ForeColor = System.Drawing.Color.Gray;
            this.PathcomboBox.FormattingEnabled = true;
            this.PathcomboBox.Items.AddRange(new object[] {
            "rootPath"});
            this.PathcomboBox.Location = new System.Drawing.Point(12, 9);
            this.PathcomboBox.Name = "PathcomboBox";
            this.PathcomboBox.Size = new System.Drawing.Size(231, 20);
            this.PathcomboBox.TabIndex = 114;
            this.PathcomboBox.Text = "rootPath";
            this.PathcomboBox.SelectedIndexChanged += new System.EventHandler(this.PathcomboBox_SelectedIndexChanged);
            // 
            // IsReminderOnlyCheckBox
            // 
            this.IsReminderOnlyCheckBox.AutoSize = true;
            this.IsReminderOnlyCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.IsReminderOnlyCheckBox.Location = new System.Drawing.Point(915, 501);
            this.IsReminderOnlyCheckBox.Name = "IsReminderOnlyCheckBox";
            this.IsReminderOnlyCheckBox.Size = new System.Drawing.Size(13, 12);
            this.IsReminderOnlyCheckBox.TabIndex = 115;
            this.IsReminderOnlyCheckBox.UseVisualStyleBackColor = true;
            this.IsReminderOnlyCheckBox.Visible = false;
            // 
            // menu_mindmaps
            // 
            this.menu_mindmaps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem10});
            this.menu_mindmaps.Name = "contextMenuStrip2";
            this.menu_mindmaps.Size = new System.Drawing.Size(125, 26);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem10.Text = "打开目录";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // menu_nodetree
            // 
            this.menu_nodetree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7,
            this.toolStripMenuItem8});
            this.menu_nodetree.Name = "contextMenuStrip3";
            this.menu_nodetree.Size = new System.Drawing.Size(180, 48);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem7.Text = "删除(delete)";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(179, 22);
            this.toolStripMenuItem8.Text = "设置任务[C+enter]";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.ToolStripMenuItem8_Click);
            // 
            // menu_filetree
            // 
            this.menu_filetree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.打开文件ToolStripMenuItem,
            this.打开文件夹ToolStripMenuItem});
            this.menu_filetree.Name = "contextMenuStrip4";
            this.menu_filetree.Size = new System.Drawing.Size(169, 70);
            this.menu_filetree.Opening += new System.ComponentModel.CancelEventHandler(this.menu_filetree_Opening);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem5.Text = "删除文件(delete)";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.打开文件ToolStripMenuItem.Text = "打开文件[enter]";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // 打开文件夹ToolStripMenuItem
            // 
            this.打开文件夹ToolStripMenuItem.Name = "打开文件夹ToolStripMenuItem";
            this.打开文件夹ToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.打开文件夹ToolStripMenuItem.Text = "打开文件夹";
            this.打开文件夹ToolStripMenuItem.Click += new System.EventHandler(this.打开文件夹ToolStripMenuItem_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看模式ToolStripMenuItem,
            this.autoRunToolStripMenuItem,
            this.工具箱ToolStripMenuItem,
            this.日历QToolStripMenuItem,
            this.剪切板ToolStripMenuItem,
            this.操作记录F12ToolStripMenuItem,
            this.透明度ToolStripMenuItem,
            this.显示树视图ToolStripMenuItem,
            this.是否锁定窗口lockToolStripMenuItem,
            this.是否播放声音playsoundToolStripMenuItem,
            this.显示右侧ToolStripMenuItem,
            this.打开程序目录ToolStripMenuItem,
            this.推出F11ToolStripMenuItem});
            this.menu.Name = "contextMenuStrip5";
            this.menu.Size = new System.Drawing.Size(212, 290);
            // 
            // 查看模式ToolStripMenuItem
            // 
            this.查看模式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单次ToolStripMenuItem,
            this.周期ToolStripMenuItem,
            this.非重要ToolStripMenuItem,
            this.导图查看模式ToolStripMenuItem});
            this.查看模式ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.查看模式ToolStripMenuItem.Name = "查看模式ToolStripMenuItem";
            this.查看模式ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.查看模式ToolStripMenuItem.Text = "查看模式";
            this.查看模式ToolStripMenuItem.Click += new System.EventHandler(this.查看模式ToolStripMenuItem_Click);
            // 
            // 单次ToolStripMenuItem
            // 
            this.单次ToolStripMenuItem.Name = "单次ToolStripMenuItem";
            this.单次ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.单次ToolStripMenuItem.Text = "单次[5]";
            // 
            // 周期ToolStripMenuItem
            // 
            this.周期ToolStripMenuItem.Name = "周期ToolStripMenuItem";
            this.周期ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.周期ToolStripMenuItem.Text = "周期[5][S+5]";
            // 
            // 非重要ToolStripMenuItem
            // 
            this.非重要ToolStripMenuItem.Name = "非重要ToolStripMenuItem";
            this.非重要ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.非重要ToolStripMenuItem.Text = "非重要[6]";
            // 
            // 导图查看模式ToolStripMenuItem
            // 
            this.导图查看模式ToolStripMenuItem.Name = "导图查看模式ToolStripMenuItem";
            this.导图查看模式ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.导图查看模式ToolStripMenuItem.Text = "导图查看模式[s]";
            // 
            // autoRunToolStripMenuItem
            // 
            this.autoRunToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disAutoRunToolStripMenuItem});
            this.autoRunToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.autoRunToolStripMenuItem.Name = "autoRunToolStripMenuItem";
            this.autoRunToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.autoRunToolStripMenuItem.Text = "开机启动";
            // 
            // disAutoRunToolStripMenuItem
            // 
            this.disAutoRunToolStripMenuItem.Name = "disAutoRunToolStripMenuItem";
            this.disAutoRunToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.disAutoRunToolStripMenuItem.Text = "取消开机启动";
            // 
            // 工具箱ToolStripMenuItem
            // 
            this.工具箱ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.工具箱ToolStripMenuItem.Name = "工具箱ToolStripMenuItem";
            this.工具箱ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.工具箱ToolStripMenuItem.Text = "工具箱:tool";
            this.工具箱ToolStripMenuItem.Click += new System.EventHandler(this.工具箱ToolStripMenuItem_Click);
            // 
            // 日历QToolStripMenuItem
            // 
            this.日历QToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.日历QToolStripMenuItem.Name = "日历QToolStripMenuItem";
            this.日历QToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.日历QToolStripMenuItem.Text = "日历(Q)";
            this.日历QToolStripMenuItem.Click += new System.EventHandler(this.日历QToolStripMenuItem_Click);
            // 
            // 剪切板ToolStripMenuItem
            // 
            this.剪切板ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.剪切板文件ToolStripMenuItem,
            this.文件夹ToolStripMenuItem});
            this.剪切板ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.剪切板ToolStripMenuItem.Name = "剪切板ToolStripMenuItem";
            this.剪切板ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.剪切板ToolStripMenuItem.Text = "剪切板:clipsearch";
            this.剪切板ToolStripMenuItem.Click += new System.EventHandler(this.剪切板ToolStripMenuItem_Click);
            // 
            // 剪切板文件ToolStripMenuItem
            // 
            this.剪切板文件ToolStripMenuItem.Name = "剪切板文件ToolStripMenuItem";
            this.剪切板文件ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.剪切板文件ToolStripMenuItem.Text = "文件:clipf";
            this.剪切板文件ToolStripMenuItem.Click += new System.EventHandler(this.剪切板文件ToolStripMenuItem_Click);
            // 
            // 文件夹ToolStripMenuItem
            // 
            this.文件夹ToolStripMenuItem.Name = "文件夹ToolStripMenuItem";
            this.文件夹ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.文件夹ToolStripMenuItem.Text = "文件夹:clipF";
            this.文件夹ToolStripMenuItem.Click += new System.EventHandler(this.文件夹ToolStripMenuItem_Click);
            // 
            // 操作记录F12ToolStripMenuItem
            // 
            this.操作记录F12ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.操作记录F12ToolStripMenuItem.Name = "操作记录F12ToolStripMenuItem";
            this.操作记录F12ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.操作记录F12ToolStripMenuItem.Text = "操作记录(F12)";
            this.操作记录F12ToolStripMenuItem.Click += new System.EventHandler(this.操作记录F12ToolStripMenuItem_Click);
            // 
            // 透明度ToolStripMenuItem
            // 
            this.透明度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.o05ToolStripMenuItem,
            this.o08ToolStripMenuItem,
            this.o1ToolStripMenuItem});
            this.透明度ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.透明度ToolStripMenuItem.Name = "透明度ToolStripMenuItem";
            this.透明度ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.透明度ToolStripMenuItem.Text = "透明度";
            this.透明度ToolStripMenuItem.Click += new System.EventHandler(this.透明度ToolStripMenuItem_Click);
            // 
            // o05ToolStripMenuItem
            // 
            this.o05ToolStripMenuItem.Name = "o05ToolStripMenuItem";
            this.o05ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.o05ToolStripMenuItem.Text = "50%:o=0.5";
            this.o05ToolStripMenuItem.Click += new System.EventHandler(this.o05ToolStripMenuItem_Click);
            // 
            // o08ToolStripMenuItem
            // 
            this.o08ToolStripMenuItem.Name = "o08ToolStripMenuItem";
            this.o08ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.o08ToolStripMenuItem.Text = "80%:o=0.8";
            this.o08ToolStripMenuItem.Click += new System.EventHandler(this.o08ToolStripMenuItem_Click);
            // 
            // o1ToolStripMenuItem
            // 
            this.o1ToolStripMenuItem.Name = "o1ToolStripMenuItem";
            this.o1ToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.o1ToolStripMenuItem.Text = "100%:o=1";
            this.o1ToolStripMenuItem.Click += new System.EventHandler(this.o1ToolStripMenuItem_Click);
            // 
            // 显示树视图ToolStripMenuItem
            // 
            this.显示树视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.隐藏树视图SnToolStripMenuItem});
            this.显示树视图ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.显示树视图ToolStripMenuItem.Name = "显示树视图ToolStripMenuItem";
            this.显示树视图ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.显示树视图ToolStripMenuItem.Text = "显示树视图[B,N]";
            this.显示树视图ToolStripMenuItem.Click += new System.EventHandler(this.显示树视图ToolStripMenuItem_Click);
            // 
            // 隐藏树视图SnToolStripMenuItem
            // 
            this.隐藏树视图SnToolStripMenuItem.Name = "隐藏树视图SnToolStripMenuItem";
            this.隐藏树视图SnToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.隐藏树视图SnToolStripMenuItem.Text = "隐藏树视图[S+n]";
            this.隐藏树视图SnToolStripMenuItem.Click += new System.EventHandler(this.隐藏树视图SnToolStripMenuItem_Click);
            // 
            // 是否锁定窗口lockToolStripMenuItem
            // 
            this.是否锁定窗口lockToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.是否锁定窗口lockToolStripMenuItem.Name = "是否锁定窗口lockToolStripMenuItem";
            this.是否锁定窗口lockToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.是否锁定窗口lockToolStripMenuItem.Text = "是否锁定窗口:lock";
            this.是否锁定窗口lockToolStripMenuItem.Click += new System.EventHandler(this.是否锁定窗口lockToolStripMenuItem_Click);
            // 
            // 是否播放声音playsoundToolStripMenuItem
            // 
            this.是否播放声音playsoundToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.是否播放声音playsoundToolStripMenuItem.Name = "是否播放声音playsoundToolStripMenuItem";
            this.是否播放声音playsoundToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.是否播放声音playsoundToolStripMenuItem.Text = "是否播放声音:playsound";
            this.是否播放声音playsoundToolStripMenuItem.Click += new System.EventHandler(this.是否播放声音playsoundToolStripMenuItem_Click);
            // 
            // 显示右侧ToolStripMenuItem
            // 
            this.显示右侧ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.显示右侧ToolStripMenuItem.Name = "显示右侧ToolStripMenuItem";
            this.显示右侧ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.显示右侧ToolStripMenuItem.Text = "显示右侧[A+l]";
            this.显示右侧ToolStripMenuItem.Click += new System.EventHandler(this.显示右侧ToolStripMenuItem_Click);
            // 
            // 打开程序目录ToolStripMenuItem
            // 
            this.打开程序目录ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.打开程序目录ToolStripMenuItem.Name = "打开程序目录ToolStripMenuItem";
            this.打开程序目录ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.打开程序目录ToolStripMenuItem.Text = "打开程序目录";
            this.打开程序目录ToolStripMenuItem.Click += new System.EventHandler(this.打开程序目录ToolStripMenuItem_Click);
            // 
            // 推出F11ToolStripMenuItem
            // 
            this.推出F11ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.推出F11ToolStripMenuItem.Name = "推出F11ToolStripMenuItem";
            this.推出F11ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.推出F11ToolStripMenuItem.Text = "退出(F11)";
            this.推出F11ToolStripMenuItem.Click += new System.EventHandler(this.推出F11ToolStripMenuItem_Click);
            // 
            // taskcount
            // 
            this.taskcount.AutoSize = true;
            this.taskcount.Location = new System.Drawing.Point(38, 483);
            this.taskcount.Name = "taskcount";
            this.taskcount.Size = new System.Drawing.Size(11, 12);
            this.taskcount.TabIndex = 116;
            this.taskcount.Text = "0";
            // 
            // mindmapornode
            // 
            this.mindmapornode.Location = new System.Drawing.Point(-272, 1);
            this.mindmapornode.Name = "mindmapornode";
            this.mindmapornode.Size = new System.Drawing.Size(239, 12);
            this.mindmapornode.TabIndex = 1;
            this.mindmapornode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DAKAINFO
            // 
            this.DAKAINFO.AutoSize = true;
            this.DAKAINFO.Location = new System.Drawing.Point(16, 2);
            this.DAKAINFO.Name = "DAKAINFO";
            this.DAKAINFO.Size = new System.Drawing.Size(0, 12);
            this.DAKAINFO.TabIndex = 0;
            this.DAKAINFO.Visible = false;
            // 
            // panel_clearSearchWord
            // 
            this.panel_clearSearchWord.Controls.Add(this.DAKAINFO);
            this.panel_clearSearchWord.Controls.Add(this.mindmapornode);
            this.panel_clearSearchWord.Location = new System.Drawing.Point(260, 31);
            this.panel_clearSearchWord.Name = "panel_clearSearchWord";
            this.panel_clearSearchWord.Size = new System.Drawing.Size(600, 18);
            this.panel_clearSearchWord.TabIndex = 47;
            this.panel_clearSearchWord.Click += new System.EventHandler(this.panel_clearSearchWord_Click);
            // 
            // fathernode
            // 
            this.fathernode.AutoSize = true;
            this.fathernode.Location = new System.Drawing.Point(629, 483);
            this.fathernode.Name = "fathernode";
            this.fathernode.Size = new System.Drawing.Size(41, 12);
            this.fathernode.TabIndex = 120;
            this.fathernode.Text = "父节点";
            // 
            // labeltaskinfo
            // 
            this.labeltaskinfo.AutoSize = true;
            this.labeltaskinfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labeltaskinfo.Location = new System.Drawing.Point(390, 483);
            this.labeltaskinfo.Name = "labeltaskinfo";
            this.labeltaskinfo.Size = new System.Drawing.Size(11, 12);
            this.labeltaskinfo.TabIndex = 121;
            this.labeltaskinfo.Text = "0";
            this.labeltaskinfo.Click += new System.EventHandler(this.labeltaskinfo_Click);
            // 
            // usedCount
            // 
            this.usedCount.AutoSize = true;
            this.usedCount.Location = new System.Drawing.Point(116, 483);
            this.usedCount.Name = "usedCount";
            this.usedCount.Size = new System.Drawing.Size(11, 12);
            this.usedCount.TabIndex = 122;
            this.usedCount.Text = "0";
            // 
            // usedtimelabel
            // 
            this.usedtimelabel.AutoSize = true;
            this.usedtimelabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.usedtimelabel.Location = new System.Drawing.Point(175, 483);
            this.usedtimelabel.Name = "usedtimelabel";
            this.usedtimelabel.Size = new System.Drawing.Size(29, 12);
            this.usedtimelabel.TabIndex = 123;
            this.usedtimelabel.Text = "time";
            // 
            // todayusedtime
            // 
            this.todayusedtime.AutoSize = true;
            this.todayusedtime.Location = new System.Drawing.Point(146, 483);
            this.todayusedtime.Name = "todayusedtime";
            this.todayusedtime.Size = new System.Drawing.Size(23, 12);
            this.todayusedtime.TabIndex = 124;
            this.todayusedtime.Text = "000";
            // 
            // richTextSubNode
            // 
            this.richTextSubNode.BackColor = System.Drawing.Color.White;
            this.richTextSubNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextSubNode.Font = new System.Drawing.Font("宋体", 9.75F);
            this.richTextSubNode.ForeColor = System.Drawing.Color.Gray;
            this.richTextSubNode.Location = new System.Drawing.Point(872, 69);
            this.richTextSubNode.MaximumSize = new System.Drawing.Size(285, 250);
            this.richTextSubNode.Name = "richTextSubNode";
            this.richTextSubNode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextSubNode.Size = new System.Drawing.Size(285, 178);
            this.richTextSubNode.TabIndex = 99;
            this.richTextSubNode.Text = "";
            this.richTextSubNode.SizeChanged += new System.EventHandler(this.richTextSubNode_SizeChanged);
            this.richTextSubNode.TextChanged += new System.EventHandler(this.RichTextSubNode_TextChanged);
            this.richTextSubNode.Enter += new System.EventHandler(this.RichSubTest_Enter);
            this.richTextSubNode.Leave += new System.EventHandler(this.RichSubTest_Leave);
            this.richTextSubNode.MouseLeave += new System.EventHandler(this.RichSubTest_MouseLeave);
            this.richTextSubNode.MouseHover += new System.EventHandler(this.RichSubTest_MouseHover);
            // 
            // tagCloudControl
            // 
            this.tagCloudControl.AllowDrop = true;
            this.tagCloudControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagCloudControl.ControlBackColor = System.Drawing.Color.White;
            this.tagCloudControl.ControlHeight = 183;
            this.tagCloudControl.ControlTextFrame = false;
            this.tagCloudControl.ControlTextUnderline = false;
            this.tagCloudControl.ControlWidth = 298;
            this.tagCloudControl.Location = new System.Drawing.Point(872, 253);
            this.tagCloudControl.Name = "tagCloudControl";
            this.tagCloudControl.Size = new System.Drawing.Size(285, 215);
            this.tagCloudControl.TabIndex = 118;
            this.tagCloudControl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tagCloudControl_ControlAdded);
            this.tagCloudControl.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.TagCloudControl_ControlRemoved);
            // 
            // noterichTextBox
            // 
            this.noterichTextBox.BackColor = System.Drawing.Color.White;
            this.noterichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.noterichTextBox.Font = new System.Drawing.Font("宋体", 9.75F);
            this.noterichTextBox.ForeColor = System.Drawing.Color.Gray;
            this.noterichTextBox.Location = new System.Drawing.Point(872, 501);
            this.noterichTextBox.Name = "noterichTextBox";
            this.noterichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.noterichTextBox.Size = new System.Drawing.Size(285, 307);
            this.noterichTextBox.TabIndex = 119;
            this.noterichTextBox.Text = "";
            this.noterichTextBox.TextChanged += new System.EventHandler(this.noterichTextBox_TextChanged);
            this.noterichTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.noterichTextBox_KeyUp);
            // 
            // reminderListBox
            // 
            this.reminderListBox.BackColor = System.Drawing.Color.White;
            this.reminderListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.reminderListBox.Font = new System.Drawing.Font("宋体", 9.75F);
            this.reminderListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.reminderListBox.FormattingEnabled = true;
            this.reminderListBox.ItemHeight = 14;
            this.reminderListBox.Location = new System.Drawing.Point(259, 171);
            this.reminderListBox.Name = "reminderListBox";
            this.reminderListBox.Size = new System.Drawing.Size(600, 158);
            this.reminderListBox.TabIndex = 117;
            this.reminderListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ReminderlistBox_DrawItem);
            this.reminderListBox.SelectedIndexChanged += new System.EventHandler(this.reminderlist_SelectedIndexChanged);
            this.reminderListBox.DataSourceChanged += new System.EventHandler(this.ReminderListBox_DataSourceChanged);
            this.reminderListBox.SizeChanged += new System.EventHandler(this.ReminderListBox_SizeChanged);
            this.reminderListBox.MouseHover += new System.EventHandler(this.reminderlist_MouseHover);
            this.reminderListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Reminderlist_MouseUp);
            // 
            // reminderList
            // 
            this.reminderList.BackColor = System.Drawing.Color.White;
            this.reminderList.CausesValidation = false;
            this.reminderList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.reminderList.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reminderList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.reminderList.FormattingEnabled = true;
            this.reminderList.HorizontalScrollbar = true;
            this.reminderList.ItemHeight = 14;
            this.reminderList.Location = new System.Drawing.Point(260, 342);
            this.reminderList.Name = "reminderList";
            this.reminderList.Size = new System.Drawing.Size(600, 130);
            this.reminderList.Sorted = true;
            this.reminderList.TabIndex = 3;
            this.reminderList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Reminderlist_DrawItem);
            this.reminderList.SelectedIndexChanged += new System.EventHandler(this.reminderlist_SelectedIndexChanged);
            this.reminderList.DataSourceChanged += new System.EventHandler(this.ReminderListBox_DataSourceChanged);
            this.reminderList.DoubleClick += new System.EventHandler(this.Reminderlist_DoubleClick);
            this.reminderList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Reminderlist_MouseDown);
            this.reminderList.MouseHover += new System.EventHandler(this.reminderlist_MouseHover);
            this.reminderList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Reminderlist_MouseUp);
            // 
            // mindmaplist
            // 
            this.mindmaplist.BackColor = System.Drawing.Color.White;
            this.mindmaplist.CausesValidation = false;
            this.mindmaplist.DrawFocusedIndicator = false;
            this.mindmaplist.Font = new System.Drawing.Font("宋体", 9.5F);
            this.mindmaplist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.mindmaplist.FormattingEnabled = true;
            this.mindmaplist.Location = new System.Drawing.Point(10, 51);
            this.mindmaplist.Margin = new System.Windows.Forms.Padding(0);
            this.mindmaplist.Name = "mindmaplist";
            this.mindmaplist.Size = new System.Drawing.Size(233, 424);
            this.mindmaplist.Sorted = true;
            this.mindmaplist.TabIndex = 1;
            this.mindmaplist.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.mindmaplist_ItemCheck);
            this.mindmaplist.SelectedIndexChanged += new System.EventHandler(this.mindmaplist_SelectedIndexChanged);
            this.mindmaplist.DoubleClick += new System.EventHandler(this.mindmaplist_DoubleClick);
            this.mindmaplist.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mindmaplist_MouseDown);
            this.mindmaplist.MouseLeave += new System.EventHandler(this.mindmaplist_MouseLeave);
            this.mindmaplist.MouseHover += new System.EventHandler(this.mindmaplist_MouseHover);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "1";
            this.notifyIcon1.BalloonTipTitle = "2";
            this.notifyIcon1.ContextMenuStrip = this.menu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "DocearReminder:请作对的事！";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click_1);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick_1);
            // 
            // EditTaskTime
            // 
            this.EditTaskTime.BackColor = System.Drawing.Color.White;
            this.EditTaskTime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.EditTaskTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EditTaskTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditTaskTime.ForeColor = System.Drawing.Color.White;
            this.EditTaskTime.Location = new System.Drawing.Point(860, 8);
            this.EditTaskTime.Name = "EditTaskTime";
            this.EditTaskTime.Size = new System.Drawing.Size(12, 23);
            this.EditTaskTime.TabIndex = 125;
            this.EditTaskTime.UseVisualStyleBackColor = false;
            this.EditTaskTime.Click += new System.EventHandler(this.EditTaskTime_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(927, 535);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 155);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 113;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // DocearReminderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1167, 818);
            this.Controls.Add(this.noterichTextBox);
            this.Controls.Add(this.EditTaskTime);
            this.Controls.Add(this.nodetree);
            this.Controls.Add(this.SearchText_suggest);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextSubNode);
            this.Controls.Add(this.night);
            this.Controls.Add(this.morning);
            this.Controls.Add(this.day);
            this.Controls.Add(this.afternoon);
            this.Controls.Add(this.IsViewModel);
            this.Controls.Add(this.c_hour);
            this.Controls.Add(this.tasklevel);
            this.Controls.Add(this.showtomorrow);
            this.Controls.Add(this.showcyclereminder);
            this.Controls.Add(this.taskTime);
            this.Controls.Add(this.c_Sunday);
            this.Controls.Add(this.c_year);
            this.Controls.Add(this.c_month);
            this.Controls.Add(this.c_week);
            this.Controls.Add(this.c_day);
            this.Controls.Add(this.c_Saturday);
            this.Controls.Add(this.c_Friday);
            this.Controls.Add(this.c_Thursday);
            this.Controls.Add(this.c_Wednesday);
            this.Controls.Add(this.c_Tuesday);
            this.Controls.Add(this.c_Monday);
            this.Controls.Add(this.button_cycle);
            this.Controls.Add(this.n_days);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.reminder_yearafter);
            this.Controls.Add(this.reminder_year);
            this.Controls.Add(this.reminder_month);
            this.Controls.Add(this.reminder_week);
            this.Controls.Add(this.IsJinianCheckBox);
            this.Controls.Add(this.onlyZhouqi);
            this.Controls.Add(this.c_remember);
            this.Controls.Add(this.ebcheckBox);
            this.Controls.Add(this.IsReminderOnlyCheckBox);
            this.Controls.Add(this.tagCloudControl);
            this.Controls.Add(this.reminderListBox);
            this.Controls.Add(this.reminderList);
            this.Controls.Add(this.searchword);
            this.Controls.Add(this.panel_clearSearchWord);
            this.Controls.Add(this.FileTreeView);
            this.Controls.Add(this.mindmaplist_count);
            this.Controls.Add(this.hourLeft);
            this.Controls.Add(this.fenshu);
            this.Controls.Add(this.Hours);
            this.Controls.Add(this.reminder_count);
            this.Controls.Add(this.taskcount);
            this.Controls.Add(this.mindmaplist);
            this.Controls.Add(this.PathcomboBox);
            this.Controls.Add(this.mindmapSearch);
            this.Controls.Add(this.fathernode);
            this.Controls.Add(this.labeltaskinfo);
            this.Controls.Add(this.usedCount);
            this.Controls.Add(this.usedtimelabel);
            this.Controls.Add(this.todayusedtime);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocearReminderForm";
            this.Opacity = 0.9D;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.DocearReminderForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.DocearReminderForm_Load);
            this.SizeChanged += new System.EventHandler(this.DocearReminderForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DocearReminderForm_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.MouseHover += new System.EventHandler(this.Form1_MouseHover);
            this.searchworkmenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.n_days)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tasklevel)).EndInit();
            this.menu_reminderlist.ResumeLayout(false);
            this.menu_mindmaps.ResumeLayout(false);
            this.menu_nodetree.ResumeLayout(false);
            this.menu_filetree.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.panel_clearSearchWord.ResumeLayout(false);
            this.panel_clearSearchWord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        #endregion
        private SortByTimeListBox reminderList;
        private System.Windows.Forms.TextBox searchword;
        private System.Windows.Forms.CheckBox reminder_week;
        private System.Windows.Forms.CheckBox reminder_month;
        private System.Windows.Forms.CheckBox reminder_year;
        private System.Windows.Forms.CheckBox reminder_yearafter;
        private System.Windows.Forms.Label mindmaplist_count;
        private System.Windows.Forms.Label reminder_count;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.NumericUpDown n_days;
        private System.Windows.Forms.Button button_cycle;
        private System.Windows.Forms.CheckBox c_Monday;
        private System.Windows.Forms.CheckBox c_Tuesday;
        private System.Windows.Forms.CheckBox c_Wednesday;
        private System.Windows.Forms.CheckBox c_Thursday;
        private System.Windows.Forms.CheckBox c_Friday;
        private System.Windows.Forms.CheckBox c_Saturday;
        private System.Windows.Forms.CheckBox c_day;
        private System.Windows.Forms.CheckBox c_week;
        private System.Windows.Forms.CheckBox c_month;
        private System.Windows.Forms.CheckBox c_year;
        private System.Windows.Forms.CheckBox c_Sunday;
        private System.Windows.Forms.NumericUpDown taskTime;
        private System.Windows.Forms.CheckBox showcyclereminder;
        private System.Windows.Forms.CheckBox showtomorrow;
        private System.Windows.Forms.Label Hours;
        private System.Windows.Forms.Label hourLeft;
        private System.Windows.Forms.NumericUpDown tasklevel;
        private System.Windows.Forms.CheckBox c_hour;
        private System.Windows.Forms.CheckBox IsViewModel;
        private System.Windows.Forms.CheckBox afternoon;
        private System.Windows.Forms.CheckBox day;
        private System.Windows.Forms.CheckBox morning;
        private System.Windows.Forms.CheckBox night;
        private System.Windows.Forms.Label fenshu;
        private MyRichTextBox richTextSubNode;
        private System.Windows.Forms.ListBox SearchText_suggest;
        private System.Windows.Forms.TextBox mindmapSearch;
        private System.Windows.Forms.CheckBox IsJinianCheckBox;
        private System.Windows.Forms.CheckBox onlyZhouqi;
        private System.Windows.Forms.TreeView nodetree;
        private System.Windows.Forms.ContextMenuStrip menu_reminderlist;
        private System.Windows.Forms.Timer Rsstimer;
        private System.Windows.Forms.CheckBox c_remember;
        private System.Windows.Forms.CheckBox ebcheckBox;
        private CustomCheckedListBox mindmaplist;
        private System.Windows.Forms.TreeView FileTreeView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox PathcomboBox;
        private System.Windows.Forms.CheckBox IsReminderOnlyCheckBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_oktask;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_deny;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCalcal;
        private System.Windows.Forms.ContextMenuStrip menu_mindmaps;
        private System.Windows.Forms.ContextMenuStrip menu_nodetree;
        private System.Windows.Forms.ContextMenuStrip menu_filetree;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.Label taskcount;
        private SortByTimeListBox reminderListBox;
        private TagCloudControl tagCloudControl;
        private System.Windows.Forms.ToolStripMenuItem 打开所在目录ToolStripMenuItem;
        private System.Windows.Forms.Label mindmapornode;
        private System.Windows.Forms.Label DAKAINFO;
        private System.Windows.Forms.Panel panel_clearSearchWord;
        private MyRichTextBox noterichTextBox;
        private Label fathernode;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private ToolStripMenuItem autoRunToolStripMenuItem;
        private ToolStripMenuItem disAutoRunToolStripMenuItem;
        private Label labeltaskinfo;
        private ToolStripMenuItem 工具箱ToolStripMenuItem;
        private Label usedCount;
        private Label usedtimelabel;
        private Label todayusedtime;
        private ToolStripMenuItem 日历QToolStripMenuItem;
        private ToolStripMenuItem 剪切板ToolStripMenuItem;
        private ToolStripMenuItem 剪切板文件ToolStripMenuItem;
        private ToolStripMenuItem 文件夹ToolStripMenuItem;
        private ToolStripMenuItem 操作记录F12ToolStripMenuItem;
        private ToolStripMenuItem 推出F11ToolStripMenuItem;
        private ToolStripMenuItem 查看模式ToolStripMenuItem;
        private ToolStripMenuItem 单次ToolStripMenuItem;
        private ToolStripMenuItem 周期ToolStripMenuItem;
        private ToolStripMenuItem 非重要ToolStripMenuItem;
        private ToolStripMenuItem 导图查看模式ToolStripMenuItem;
        private ToolStripMenuItem 透明度ToolStripMenuItem;
        private ToolStripMenuItem o05ToolStripMenuItem;
        private ToolStripMenuItem o08ToolStripMenuItem;
        private ToolStripMenuItem o1ToolStripMenuItem;
        private ToolStripMenuItem 仅查看CdToolStripMenuItem;
        private ToolStripMenuItem 是否播放声音playsoundToolStripMenuItem;
        private ToolStripMenuItem 是否锁定窗口lockToolStripMenuItem;
        private ToolStripMenuItem 显示树视图ToolStripMenuItem;
        private ToolStripMenuItem 隐藏树视图SnToolStripMenuItem;
        private ToolStripMenuItem 显示右侧ToolStripMenuItem;
        private ToolStripMenuItem 非重要ToolStripMenuItem1;
        private ToolStripMenuItem 设置重要xToolStripMenuItem;
        private ToolStripMenuItem 快捷键说明ToolStripMenuItem;
        private ToolStripMenuItem 下一个jToolStripMenuItem;
        private ToolStripMenuItem 上一个kToolStripMenuItem;
        private ContextMenuStrip searchworkmenu;
        private ToolStripMenuItem 窗口ToolStripMenuItem;
        private ToolStripMenuItem 工具toolToolStripMenuItem;
        private ToolStripMenuItem 打开程序目录ToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private ToolStripMenuItem 打开文件ToolStripMenuItem;
        private ToolStripMenuItem 打开文件夹ToolStripMenuItem;
        private Button EditTaskTime;
    }
}


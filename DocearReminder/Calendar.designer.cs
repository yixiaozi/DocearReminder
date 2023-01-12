using System;
using System.Windows.Forms;
using yixiaozi.WinForm.Control;
using yixiaozi.WinForm.Control.Calendar;

namespace DocearReminder
{
    partial class CalendarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            yixiaozi.WinForm.Control.Calendar.DrawTool drawTool1 = new yixiaozi.WinForm.Control.Calendar.DrawTool();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarForm));
            this.dayView1 = new yixiaozi.WinForm.Control.Calendar.DayView();
            this.Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.完成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开导图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.番茄钟ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解锁ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.截图 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.workfolder_combox = new System.Windows.Forms.ComboBox();
            this.textBox_searchwork = new System.Windows.Forms.TextBox();
            this.numericOpacity = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lockButton = new System.Windows.Forms.Button();
            this.c_timeBlock = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.c_fanqie = new System.Windows.Forms.CheckBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.c_done = new System.Windows.Forms.CheckBox();
            this.c_progress = new System.Windows.Forms.CheckBox();
            this.c_mistake = new System.Windows.Forms.CheckBox();
            this.c_lock = new System.Windows.Forms.CheckBox();
            this.isview_c = new System.Windows.Forms.CheckBox();
            this.c_Money = new System.Windows.Forms.CheckBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.exclude = new System.Windows.Forms.TextBox();
            this.checkBox_jinian = new System.Windows.Forms.CheckBox();
            this.checkBox_enddate = new System.Windows.Forms.CheckBox();
            this.Ka_c = new System.Windows.Forms.CheckBox();
            this.subClass = new System.Windows.Forms.CheckBox();
            this.view = new System.Windows.Forms.Button();
            this.CaptureScreen = new System.Windows.Forms.CheckBox();
            this.JieTucheckBox = new System.Windows.Forms.CheckBox();
            this.CameracheckBox = new System.Windows.Forms.CheckBox();
            this.ShowNodes = new System.Windows.Forms.CheckBox();
            this.AllFile = new System.Windows.Forms.CheckBox();
            this.HTML = new System.Windows.Forms.CheckBox();
            this.ShowClipboard = new System.Windows.Forms.CheckBox();
            this.ActionLog = new System.Windows.Forms.CheckBox();
            this.TimeBlockColor = new System.Windows.Forms.CheckBox();
            this.Menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // dayView1
            // 
            drawTool1.DayView = this.dayView1;
            this.dayView1.ActiveTool = drawTool1;
            this.dayView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dayView1.BackColor = System.Drawing.Color.White;
            this.dayView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dayView1.ContextMenuStrip = this.Menu;
            this.dayView1.DaysToShow = 7;
            this.dayView1.Font = new System.Drawing.Font("楷体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dayView1.ForeColor = System.Drawing.Color.White;
            this.dayView1.HoverAppointment = null;
            this.dayView1.IsShowHover = true;
            this.dayView1.Location = new System.Drawing.Point(0, 0);
            this.dayView1.Margin = new System.Windows.Forms.Padding(0);
            this.dayView1.Name = "dayView1";
            this.dayView1.SelectionEnd = new System.DateTime(2021, 12, 31, 0, 0, 0, 0);
            this.dayView1.SelectionStart = new System.DateTime(2021, 7, 16, 0, 0, 0, 0);
            this.dayView1.Size = new System.Drawing.Size(1904, 1076);
            this.dayView1.StartDate = new System.DateTime(2021, 12, 27, 0, 0, 0, 0);
            this.dayView1.StartHour = 0;
            this.dayView1.TabIndex = 0;
            this.dayView1.Text = "   ";
            this.dayView1.WorkingHourEnd = 23;
            this.dayView1.WorkingHourStart = 4;
            this.dayView1.WorkingMinuteEnd = 0;
            this.dayView1.WorkingMinuteStart = 0;
            this.dayView1.SelectionChanged += new System.EventHandler(this.dayView1_SelectionChanged_1);
            this.dayView1.ResolveAppointments += new yixiaozi.WinForm.Control.Calendar.ResolveAppointmentsEventHandler(this.dayView1_ResolveAppointments);
            this.dayView1.AppointmentMouseHover += new yixiaozi.WinForm.Control.DayView.AppointmentMouseHoverEventHandler(this.dayView1_AppointmentMouseHover);
            this.dayView1.AppointmentMouseLeave += new yixiaozi.WinForm.Control.DayView.AppointmentMouseLeaveEventHandler(this.dayView1_AppointmentMouseLeave);
            this.dayView1.AppointmentMouseMove += new yixiaozi.WinForm.Control.DayView.AppointmentMouseMoveEventHandler(this.dayView1_AppointmentMouseMove);
            this.dayView1.AppoinmentMove += new System.EventHandler<yixiaozi.WinForm.Control.Calendar.AppointmentEventArgs>(this.dayView1_AppoinmentMove);
            this.dayView1.Click += new System.EventHandler(this.dayView1_Click);
            this.dayView1.DoubleClick += new System.EventHandler(this.dayView1_DoubleClick);
            this.dayView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dayView1_KeyDown);
            this.dayView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CalendarForm_KeyUp);
            this.dayView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dayView1_MouseUp);
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.完成ToolStripMenuItem,
            this.commentToolStripMenuItem,
            this.打开导图ToolStripMenuItem,
            this.番茄钟ToolStripMenuItem,
            this.解锁ToolStripMenuItem});
            this.Menu.Name = "contextMenuStrip1";
            this.Menu.Size = new System.Drawing.Size(133, 114);
            this.Menu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.Menu_Closed);
            this.Menu.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_Opening);
            this.Menu.Opened += new System.EventHandler(this.Menu_Opened);
            // 
            // 完成ToolStripMenuItem
            // 
            this.完成ToolStripMenuItem.Name = "完成ToolStripMenuItem";
            this.完成ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.完成ToolStripMenuItem.Text = "完成";
            this.完成ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // commentToolStripMenuItem
            // 
            this.commentToolStripMenuItem.Name = "commentToolStripMenuItem";
            this.commentToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.commentToolStripMenuItem.Text = "Comment";
            this.commentToolStripMenuItem.Click += new System.EventHandler(this.commentToolStripMenuItem_Click);
            // 
            // 打开导图ToolStripMenuItem
            // 
            this.打开导图ToolStripMenuItem.Name = "打开导图ToolStripMenuItem";
            this.打开导图ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.打开导图ToolStripMenuItem.Text = "打开导图";
            this.打开导图ToolStripMenuItem.Click += new System.EventHandler(this.打开导图ToolStripMenuItem_Click);
            // 
            // 番茄钟ToolStripMenuItem
            // 
            this.番茄钟ToolStripMenuItem.Name = "番茄钟ToolStripMenuItem";
            this.番茄钟ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.番茄钟ToolStripMenuItem.Text = "番茄钟";
            this.番茄钟ToolStripMenuItem.Click += new System.EventHandler(this.番茄钟ToolStripMenuItem_Click);
            // 
            // 解锁ToolStripMenuItem
            // 
            this.解锁ToolStripMenuItem.Name = "解锁ToolStripMenuItem";
            this.解锁ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.解锁ToolStripMenuItem.Text = "解锁";
            this.解锁ToolStripMenuItem.Click += new System.EventHandler(this.解锁ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dayView1);
            this.panel1.Location = new System.Drawing.Point(0, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1904, 1076);
            this.panel1.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM月dd";
            this.dateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(111, 21);
            this.dateTimePicker1.TabIndex = 19;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(112, 0);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(31, 21);
            this.numericUpDown1.TabIndex = 20;
            this.numericUpDown1.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(598, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 21);
            this.button1.TabIndex = 23;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // 截图
            // 
            this.截图.Location = new System.Drawing.Point(661, 0);
            this.截图.Name = "截图";
            this.截图.Size = new System.Drawing.Size(65, 21);
            this.截图.TabIndex = 24;
            this.截图.Text = "截图";
            this.截图.UseVisualStyleBackColor = true;
            this.截图.Click += new System.EventHandler(this.截图_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(145, 0);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(33, 21);
            this.numericUpDown2.TabIndex = 25;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // workfolder_combox
            // 
            this.workfolder_combox.FormattingEnabled = true;
            this.workfolder_combox.Items.AddRange(new object[] {
            "rootPath"});
            this.workfolder_combox.Location = new System.Drawing.Point(300, 0);
            this.workfolder_combox.Name = "workfolder_combox";
            this.workfolder_combox.Size = new System.Drawing.Size(76, 20);
            this.workfolder_combox.TabIndex = 26;
            this.workfolder_combox.Text = "rootPath";
            this.workfolder_combox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox_searchwork
            // 
            this.textBox_searchwork.Location = new System.Drawing.Point(443, 0);
            this.textBox_searchwork.Name = "textBox_searchwork";
            this.textBox_searchwork.Size = new System.Drawing.Size(75, 21);
            this.textBox_searchwork.TabIndex = 28;
            this.textBox_searchwork.TextChanged += new System.EventHandler(this.textBox_searchwork_TextChanged);
            this.textBox_searchwork.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_searchwork_KeyUp);
            // 
            // numericOpacity
            // 
            this.numericOpacity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericOpacity.Location = new System.Drawing.Point(177, 0);
            this.numericOpacity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericOpacity.Name = "numericOpacity";
            this.numericOpacity.Size = new System.Drawing.Size(31, 21);
            this.numericOpacity.TabIndex = 29;
            this.numericOpacity.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numericOpacity.ValueChanged += new System.EventHandler(this.numericOpacity_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "任务",
            "不重要",
            "记录",
            "记忆",
            "所有"});
            this.comboBox1.Location = new System.Drawing.Point(372, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(68, 20);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.Text = "任务";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_2);
            // 
            // lockButton
            // 
            this.lockButton.Location = new System.Drawing.Point(727, 0);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(52, 21);
            this.lockButton.TabIndex = 31;
            this.lockButton.Text = "解锁";
            this.lockButton.UseVisualStyleBackColor = true;
            this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
            // 
            // c_timeBlock
            // 
            this.c_timeBlock.AutoSize = true;
            this.c_timeBlock.Checked = true;
            this.c_timeBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c_timeBlock.Location = new System.Drawing.Point(886, 3);
            this.c_timeBlock.Name = "c_timeBlock";
            this.c_timeBlock.Size = new System.Drawing.Size(60, 16);
            this.c_timeBlock.TabIndex = 32;
            this.c_timeBlock.Text = "时间块";
            this.c_timeBlock.UseVisualStyleBackColor = true;
            this.c_timeBlock.CheckedChanged += new System.EventHandler(this.c_timeBlock_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(780, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 21);
            this.button2.TabIndex = 33;
            this.button2.Text = "今天";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // c_fanqie
            // 
            this.c_fanqie.AutoSize = true;
            this.c_fanqie.Checked = true;
            this.c_fanqie.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c_fanqie.Location = new System.Drawing.Point(942, 3);
            this.c_fanqie.Name = "c_fanqie";
            this.c_fanqie.Size = new System.Drawing.Size(48, 16);
            this.c_fanqie.TabIndex = 34;
            this.c_fanqie.Text = "番茄";
            this.c_fanqie.UseVisualStyleBackColor = true;
            this.c_fanqie.CheckedChanged += new System.EventHandler(this.c_fanqie_CheckedChanged);
            // 
            // toolTip2
            // 
            this.toolTip2.ShowAlways = true;
            this.toolTip2.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip2_Popup);
            // 
            // c_done
            // 
            this.c_done.AutoSize = true;
            this.c_done.Location = new System.Drawing.Point(1173, 3);
            this.c_done.Name = "c_done";
            this.c_done.Size = new System.Drawing.Size(60, 16);
            this.c_done.TabIndex = 1;
            this.c_done.Text = "已完成";
            this.c_done.UseVisualStyleBackColor = true;
            this.c_done.CheckedChanged += new System.EventHandler(this.c_done_CheckedChanged);
            // 
            // c_progress
            // 
            this.c_progress.AutoSize = true;
            this.c_progress.Location = new System.Drawing.Point(985, 3);
            this.c_progress.Name = "c_progress";
            this.c_progress.Size = new System.Drawing.Size(48, 16);
            this.c_progress.TabIndex = 1;
            this.c_progress.Text = "进步";
            this.c_progress.UseVisualStyleBackColor = true;
            this.c_progress.CheckedChanged += new System.EventHandler(this.c_progress_CheckedChanged);
            // 
            // c_mistake
            // 
            this.c_mistake.AutoSize = true;
            this.c_mistake.Location = new System.Drawing.Point(1027, 3);
            this.c_mistake.Name = "c_mistake";
            this.c_mistake.Size = new System.Drawing.Size(48, 16);
            this.c_mistake.TabIndex = 1;
            this.c_mistake.Text = "错误";
            this.c_mistake.UseVisualStyleBackColor = true;
            this.c_mistake.CheckedChanged += new System.EventHandler(this.c_mistake_CheckedChanged);
            // 
            // c_lock
            // 
            this.c_lock.AutoSize = true;
            this.c_lock.Checked = true;
            this.c_lock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c_lock.Location = new System.Drawing.Point(246, 3);
            this.c_lock.Name = "c_lock";
            this.c_lock.Size = new System.Drawing.Size(48, 16);
            this.c_lock.TabIndex = 1;
            this.c_lock.Text = "锁定";
            this.c_lock.UseVisualStyleBackColor = true;
            // 
            // isview_c
            // 
            this.isview_c.AutoSize = true;
            this.isview_c.Checked = true;
            this.isview_c.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isview_c.Location = new System.Drawing.Point(1230, 3);
            this.isview_c.Name = "isview_c";
            this.isview_c.Size = new System.Drawing.Size(60, 16);
            this.isview_c.TabIndex = 36;
            this.isview_c.Text = "不重要";
            this.isview_c.UseVisualStyleBackColor = true;
            this.isview_c.CheckedChanged += new System.EventHandler(this.isview_c_CheckedChanged);
            // 
            // c_Money
            // 
            this.c_Money.AutoSize = true;
            this.c_Money.Location = new System.Drawing.Point(1070, 3);
            this.c_Money.Name = "c_Money";
            this.c_Money.Size = new System.Drawing.Size(48, 16);
            this.c_Money.TabIndex = 1;
            this.c_Money.Text = "金钱";
            this.c_Money.UseVisualStyleBackColor = true;
            this.c_Money.CheckedChanged += new System.EventHandler(this.c_Money_CheckedChanged);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown3.Location = new System.Drawing.Point(210, 0);
            this.numericUpDown3.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(31, 21);
            this.numericUpDown3.TabIndex = 37;
            this.numericUpDown3.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // exclude
            // 
            this.exclude.Location = new System.Drawing.Point(521, 0);
            this.exclude.Name = "exclude";
            this.exclude.Size = new System.Drawing.Size(75, 21);
            this.exclude.TabIndex = 38;
            this.exclude.TextChanged += new System.EventHandler(this.exclude_TextChanged);
            this.exclude.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_searchwork_KeyUp);
            // 
            // checkBox_jinian
            // 
            this.checkBox_jinian.AutoSize = true;
            this.checkBox_jinian.Location = new System.Drawing.Point(1285, 3);
            this.checkBox_jinian.Name = "checkBox_jinian";
            this.checkBox_jinian.Size = new System.Drawing.Size(60, 16);
            this.checkBox_jinian.TabIndex = 39;
            this.checkBox_jinian.Text = "纪念日";
            this.checkBox_jinian.UseVisualStyleBackColor = true;
            this.checkBox_jinian.CheckedChanged += new System.EventHandler(this.checkBox_jinian_CheckedChanged);
            // 
            // checkBox_enddate
            // 
            this.checkBox_enddate.AutoSize = true;
            this.checkBox_enddate.Location = new System.Drawing.Point(1339, 3);
            this.checkBox_enddate.Name = "checkBox_enddate";
            this.checkBox_enddate.Size = new System.Drawing.Size(60, 16);
            this.checkBox_enddate.TabIndex = 40;
            this.checkBox_enddate.Text = "截止日";
            this.checkBox_enddate.UseVisualStyleBackColor = true;
            this.checkBox_enddate.CheckedChanged += new System.EventHandler(this.checkBox_enddate_CheckedChanged);
            // 
            // Ka_c
            // 
            this.Ka_c.AutoSize = true;
            this.Ka_c.Location = new System.Drawing.Point(1113, 3);
            this.Ka_c.Name = "Ka_c";
            this.Ka_c.Size = new System.Drawing.Size(60, 16);
            this.Ka_c.TabIndex = 41;
            this.Ka_c.Text = "卡路里";
            this.Ka_c.UseVisualStyleBackColor = true;
            this.Ka_c.CheckedChanged += new System.EventHandler(this.Ka_c_CheckedChanged);
            // 
            // subClass
            // 
            this.subClass.AutoSize = true;
            this.subClass.Location = new System.Drawing.Point(1395, 3);
            this.subClass.Name = "subClass";
            this.subClass.Size = new System.Drawing.Size(48, 16);
            this.subClass.TabIndex = 42;
            this.subClass.Text = "子类";
            this.subClass.UseVisualStyleBackColor = true;
            this.subClass.CheckedChanged += new System.EventHandler(this.subClass_CheckedChanged);
            // 
            // view
            // 
            this.view.Location = new System.Drawing.Point(833, 0);
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(53, 21);
            this.view.TabIndex = 43;
            this.view.Text = "视图";
            this.view.UseVisualStyleBackColor = true;
            this.view.Click += new System.EventHandler(this.view_Click);
            // 
            // CaptureScreen
            // 
            this.CaptureScreen.AutoSize = true;
            this.CaptureScreen.Location = new System.Drawing.Point(1440, 3);
            this.CaptureScreen.Name = "CaptureScreen";
            this.CaptureScreen.Size = new System.Drawing.Size(72, 16);
            this.CaptureScreen.TabIndex = 44;
            this.CaptureScreen.Text = "屏幕截屏";
            this.CaptureScreen.UseVisualStyleBackColor = true;
            this.CaptureScreen.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // JieTucheckBox
            // 
            this.JieTucheckBox.AutoSize = true;
            this.JieTucheckBox.Location = new System.Drawing.Point(1511, 3);
            this.JieTucheckBox.Name = "JieTucheckBox";
            this.JieTucheckBox.Size = new System.Drawing.Size(48, 16);
            this.JieTucheckBox.TabIndex = 45;
            this.JieTucheckBox.Text = "截图";
            this.JieTucheckBox.UseVisualStyleBackColor = true;
            this.JieTucheckBox.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // CameracheckBox
            // 
            this.CameracheckBox.AutoSize = true;
            this.CameracheckBox.Location = new System.Drawing.Point(1558, 3);
            this.CameracheckBox.Name = "CameracheckBox";
            this.CameracheckBox.Size = new System.Drawing.Size(60, 16);
            this.CameracheckBox.TabIndex = 46;
            this.CameracheckBox.Text = "摄像头";
            this.CameracheckBox.UseVisualStyleBackColor = true;
            this.CameracheckBox.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // ShowNodes
            // 
            this.ShowNodes.AutoSize = true;
            this.ShowNodes.Location = new System.Drawing.Point(1780, 3);
            this.ShowNodes.Name = "ShowNodes";
            this.ShowNodes.Size = new System.Drawing.Size(48, 16);
            this.ShowNodes.TabIndex = 47;
            this.ShowNodes.Text = "节点";
            this.ShowNodes.UseVisualStyleBackColor = true;
            this.ShowNodes.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // AllFile
            // 
            this.AllFile.AutoSize = true;
            this.AllFile.Location = new System.Drawing.Point(1824, 3);
            this.AllFile.Name = "AllFile";
            this.AllFile.Size = new System.Drawing.Size(72, 16);
            this.AllFile.TabIndex = 48;
            this.AllFile.Text = "所有文件";
            this.AllFile.UseVisualStyleBackColor = true;
            this.AllFile.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // HTML
            // 
            this.HTML.AutoSize = true;
            this.HTML.Location = new System.Drawing.Point(1612, 3);
            this.HTML.Name = "HTML";
            this.HTML.Size = new System.Drawing.Size(48, 16);
            this.HTML.TabIndex = 49;
            this.HTML.Text = "网页";
            this.HTML.UseVisualStyleBackColor = true;
            this.HTML.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // ShowClipboard
            // 
            this.ShowClipboard.AutoSize = true;
            this.ShowClipboard.Location = new System.Drawing.Point(1655, 3);
            this.ShowClipboard.Name = "ShowClipboard";
            this.ShowClipboard.Size = new System.Drawing.Size(60, 16);
            this.ShowClipboard.TabIndex = 50;
            this.ShowClipboard.Text = "剪切板";
            this.ShowClipboard.UseVisualStyleBackColor = true;
            this.ShowClipboard.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // ActionLog
            // 
            this.ActionLog.AutoSize = true;
            this.ActionLog.Location = new System.Drawing.Point(1708, 3);
            this.ActionLog.Name = "ActionLog";
            this.ActionLog.Size = new System.Drawing.Size(72, 16);
            this.ActionLog.TabIndex = 51;
            this.ActionLog.Text = "操作记录";
            this.ActionLog.UseVisualStyleBackColor = true;
            this.ActionLog.CheckedChanged += new System.EventHandler(this.CaptureScreen_CheckedChanged);
            // 
            // TimeBlockColor
            // 
            this.TimeBlockColor.AutoSize = true;
            this.TimeBlockColor.Location = new System.Drawing.Point(911, 0);
            this.TimeBlockColor.Name = "TimeBlockColor";
            this.TimeBlockColor.Size = new System.Drawing.Size(15, 14);
            this.TimeBlockColor.TabIndex = 52;
            this.TimeBlockColor.UseVisualStyleBackColor = true;
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1900, 1037);
            this.Controls.Add(this.TimeBlockColor);
            this.Controls.Add(this.ActionLog);
            this.Controls.Add(this.ShowClipboard);
            this.Controls.Add(this.HTML);
            this.Controls.Add(this.AllFile);
            this.Controls.Add(this.ShowNodes);
            this.Controls.Add(this.CameracheckBox);
            this.Controls.Add(this.JieTucheckBox);
            this.Controls.Add(this.CaptureScreen);
            this.Controls.Add(this.view);
            this.Controls.Add(this.subClass);
            this.Controls.Add(this.Ka_c);
            this.Controls.Add(this.checkBox_enddate);
            this.Controls.Add(this.checkBox_jinian);
            this.Controls.Add(this.exclude);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.c_Money);
            this.Controls.Add(this.isview_c);
            this.Controls.Add(this.c_lock);
            this.Controls.Add(this.c_mistake);
            this.Controls.Add(this.c_progress);
            this.Controls.Add(this.c_done);
            this.Controls.Add(this.c_fanqie);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.c_timeBlock);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.numericOpacity);
            this.Controls.Add(this.textBox_searchwork);
            this.Controls.Add(this.workfolder_combox);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.截图);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lockButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CalendarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "  ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalendarForm_FormClosed);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.SizeChanged += new System.EventHandler(this.MainPage_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CalendarForm_KeyUp);
            this.Menu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        public DayView dayView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button 截图;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.ComboBox workfolder_combox;
        private System.Windows.Forms.TextBox textBox_searchwork;
        private System.Windows.Forms.NumericUpDown numericOpacity;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem 完成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开导图ToolStripMenuItem;
        private System.Windows.Forms.Button lockButton;
        private System.Windows.Forms.CheckBox c_timeBlock;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox c_fanqie;
        private ToolTip toolTip2;
        private ToolStripMenuItem commentToolStripMenuItem;
        private CheckBox c_done;
        private CheckBox c_progress;
        private CheckBox c_mistake;
        private CheckBox c_lock;
        private ToolStripMenuItem 番茄钟ToolStripMenuItem;
        private CheckBox isview_c;
        private CheckBox c_Money;
        private NumericUpDown numericUpDown3;
        private TextBox exclude;
        private CheckBox checkBox_jinian;
        private CheckBox checkBox_enddate;
        private CheckBox Ka_c;
        private CheckBox subClass;
        private Button view;
        private ToolStripMenuItem 解锁ToolStripMenuItem;
        private CheckBox CaptureScreen;
        private CheckBox JieTucheckBox;
        private CheckBox CameracheckBox;
        private CheckBox ShowNodes;
        private CheckBox AllFile;
        private CheckBox HTML;
        private CheckBox ShowClipboard;
        private CheckBox ActionLog;
        private CheckBox TimeBlockColor;
    }
}


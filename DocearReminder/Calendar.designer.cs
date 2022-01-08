using yixiaozi.WinForm.Control.Calendar;

namespace Calendar
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.完成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开导图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.截图 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.workfolder_combox = new System.Windows.Forms.ComboBox();
            this.taskname = new System.Windows.Forms.Label();
            this.textBox_searchwork = new System.Windows.Forms.TextBox();
            this.tasktime = new System.Windows.Forms.Label();
            this.numericOpacity = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lockButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpacity)).BeginInit();
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
            this.dayView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dayView1.DaysToShow = 7;
            this.dayView1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.dayView1.HalfHourHeight = 20;
            this.dayView1.Location = new System.Drawing.Point(0, 0);
            this.dayView1.Margin = new System.Windows.Forms.Padding(0);
            this.dayView1.Name = "dayView1";
            this.dayView1.SelectionEnd = new System.DateTime(2021, 12, 31, 0, 0, 0, 0);
            this.dayView1.SelectionStart = new System.DateTime(2021, 7, 16, 0, 0, 0, 0);
            this.dayView1.Size = new System.Drawing.Size(1250, 1000);
            this.dayView1.StartDate = new System.DateTime(2021, 12, 27, 0, 0, 0, 0);
            this.dayView1.StartHour = 1;
            this.dayView1.TabIndex = 0;
            this.dayView1.Text = "   ";
            this.dayView1.WorkingHourEnd = 23;
            this.dayView1.WorkingHourStart = 3;
            this.dayView1.WorkingMinuteEnd = 0;
            this.dayView1.WorkingMinuteStart = 0;
            this.dayView1.SelectionChanged += new System.EventHandler(this.dayView1_SelectionChanged_1);
            this.dayView1.ResolveAppointments += new yixiaozi.WinForm.Control.Calendar.ResolveAppointmentsEventHandler(this.dayView1_ResolveAppointments);
            this.dayView1.AppoinmentMove += new System.EventHandler<yixiaozi.WinForm.Control.Calendar.AppointmentEventArgs>(this.dayView1_AppoinmentMove);
            this.dayView1.DoubleClick += new System.EventHandler(this.dayView1_DoubleClick);
            this.dayView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CalendarForm_KeyUp);
            this.dayView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dayView1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.完成ToolStripMenuItem,
            this.打开导图ToolStripMenuItem,
            this.设置时间ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // 完成ToolStripMenuItem
            // 
            this.完成ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.square_ok;
            this.完成ToolStripMenuItem.Name = "完成ToolStripMenuItem";
            this.完成ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.完成ToolStripMenuItem.Text = "完成";
            // 
            // 打开导图ToolStripMenuItem
            // 
            this.打开导图ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.resize_1;
            this.打开导图ToolStripMenuItem.Name = "打开导图ToolStripMenuItem";
            this.打开导图ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开导图ToolStripMenuItem.Text = "打开导图";
            // 
            // 设置时间ToolStripMenuItem
            // 
            this.设置时间ToolStripMenuItem.Image = global::DocearReminder.Properties.Resources.apple;
            this.设置时间ToolStripMenuItem.Name = "设置时间ToolStripMenuItem";
            this.设置时间ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.设置时间ToolStripMenuItem.Text = "设置时间";
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
            this.panel1.Size = new System.Drawing.Size(1250, 1000);
            this.panel1.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MM月dd dddd";
            this.dateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(111, 21);
            this.dateTimePicker1.TabIndex = 19;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(117, 0);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(41, 21);
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
            this.button1.Location = new System.Drawing.Point(650, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
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
            this.截图.Location = new System.Drawing.Point(731, 0);
            this.截图.Name = "截图";
            this.截图.Size = new System.Drawing.Size(75, 21);
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
            this.numericUpDown2.Location = new System.Drawing.Point(164, 0);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(40, 21);
            this.numericUpDown2.TabIndex = 25;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // workfolder_combox
            // 
            this.workfolder_combox.FormattingEnabled = true;
            this.workfolder_combox.Items.AddRange(new object[] {
            "RootPath"});
            this.workfolder_combox.Location = new System.Drawing.Point(259, 0);
            this.workfolder_combox.Name = "workfolder_combox";
            this.workfolder_combox.Size = new System.Drawing.Size(80, 20);
            this.workfolder_combox.TabIndex = 26;
            this.workfolder_combox.Text = "RootPath";
            this.workfolder_combox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // taskname
            // 
            this.taskname.AutoSize = true;
            this.taskname.Location = new System.Drawing.Point(1181, 3);
            this.taskname.Name = "taskname";
            this.taskname.Size = new System.Drawing.Size(53, 12);
            this.taskname.TabIndex = 27;
            this.taskname.Text = "taskname";
            // 
            // textBox_searchwork
            // 
            this.textBox_searchwork.Location = new System.Drawing.Point(445, 0);
            this.textBox_searchwork.Name = "textBox_searchwork";
            this.textBox_searchwork.Size = new System.Drawing.Size(200, 21);
            this.textBox_searchwork.TabIndex = 28;
            this.textBox_searchwork.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_searchwork_KeyUp);
            // 
            // tasktime
            // 
            this.tasktime.AutoSize = true;
            this.tasktime.Location = new System.Drawing.Point(1122, 4);
            this.tasktime.Name = "tasktime";
            this.tasktime.Size = new System.Drawing.Size(53, 12);
            this.tasktime.TabIndex = 1;
            this.tasktime.Text = "tasktime";
            // 
            // numericOpacity
            // 
            this.numericOpacity.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericOpacity.Location = new System.Drawing.Point(210, 0);
            this.numericOpacity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericOpacity.Name = "numericOpacity";
            this.numericOpacity.Size = new System.Drawing.Size(40, 21);
            this.numericOpacity.TabIndex = 29;
            this.numericOpacity.Value = new decimal(new int[] {
            100,
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
            this.comboBox1.Location = new System.Drawing.Point(348, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(90, 20);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.Text = "任务";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_2);
            // 
            // lockButton
            // 
            this.lockButton.Location = new System.Drawing.Point(813, 0);
            this.lockButton.Name = "lockButton";
            this.lockButton.Size = new System.Drawing.Size(75, 21);
            this.lockButton.TabIndex = 31;
            this.lockButton.Text = "锁定";
            this.lockButton.UseVisualStyleBackColor = true;
            this.lockButton.Click += new System.EventHandler(this.lockButton_Click);
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1246, 961);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.numericOpacity);
            this.Controls.Add(this.tasktime);
            this.Controls.Add(this.textBox_searchwork);
            this.Controls.Add(this.taskname);
            this.Controls.Add(this.workfolder_combox);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.截图);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lockButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CalendarForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalendarForm_FormClosed);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.SizeChanged += new System.EventHandler(this.MainPage_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CalendarForm_KeyUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericOpacity)).EndInit();
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
        private System.Windows.Forms.Label taskname;
        private System.Windows.Forms.TextBox textBox_searchwork;
        private System.Windows.Forms.Label tasktime;
        private System.Windows.Forms.NumericUpDown numericOpacity;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 完成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开导图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置时间ToolStripMenuItem;
        private System.Windows.Forms.Button lockButton;
    }
}


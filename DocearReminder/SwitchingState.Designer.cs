namespace DocearReminder
{
    partial class SwitchingState
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SwitchingState));
            this.showcyclereminder = new System.Windows.Forms.CheckBox();
            this.drawioPicBigger = new System.Windows.Forms.PictureBox();
            this.IsDiary = new System.Windows.Forms.CheckBox();
            this.IsClip = new System.Windows.Forms.CheckBox();
            this.checkBox_截图 = new System.Windows.Forms.CheckBox();
            this.KADateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.MoneyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.TimeBlockDate = new System.Windows.Forms.DateTimePicker();
            this.quietmode = new System.Windows.Forms.CheckBox();
            this.StartRecordCheckBox = new System.Windows.Forms.CheckBox();
            this.c_speechcontrol = new System.Windows.Forms.CheckBox();
            this.showtomorrow = new System.Windows.Forms.CheckBox();
            this.reminder_yearafter = new System.Windows.Forms.CheckBox();
            this.reminder_year = new System.Windows.Forms.CheckBox();
            this.reminder_month = new System.Windows.Forms.CheckBox();
            this.reminder_week = new System.Windows.Forms.CheckBox();
            this.onlyZhouqi = new System.Windows.Forms.CheckBox();
            this.ebcheckBox = new System.Windows.Forms.CheckBox();
            this.IsReminderOnlyCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.drawioPicBigger)).BeginInit();
            this.SuspendLayout();
            // 
            // showcyclereminder
            // 
            this.showcyclereminder.AutoSize = true;
            this.showcyclereminder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.showcyclereminder.Location = new System.Drawing.Point(12, 12);
            this.showcyclereminder.Name = "showcyclereminder";
            this.showcyclereminder.Size = new System.Drawing.Size(70, 16);
            this.showcyclereminder.TabIndex = 44;
            this.showcyclereminder.TabStop = false;
            this.showcyclereminder.Text = "显示周期";
            this.showcyclereminder.UseVisualStyleBackColor = true;
            // 
            // drawioPicBigger
            // 
            this.drawioPicBigger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawioPicBigger.Location = new System.Drawing.Point(13, 194);
            this.drawioPicBigger.Name = "drawioPicBigger";
            this.drawioPicBigger.Size = new System.Drawing.Size(100, 50);
            this.drawioPicBigger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.drawioPicBigger.TabIndex = 169;
            this.drawioPicBigger.TabStop = false;
            this.drawioPicBigger.Visible = false;
            // 
            // IsDiary
            // 
            this.IsDiary.AutoSize = true;
            this.IsDiary.Location = new System.Drawing.Point(166, 76);
            this.IsDiary.Name = "IsDiary";
            this.IsDiary.Size = new System.Drawing.Size(72, 16);
            this.IsDiary.TabIndex = 168;
            this.IsDiary.Text = "是否日记";
            this.IsDiary.UseVisualStyleBackColor = true;
            this.IsDiary.CheckedChanged += new System.EventHandler(this.IsDiary_CheckedChanged);
            // 
            // IsClip
            // 
            this.IsClip.AutoSize = true;
            this.IsClip.Checked = true;
            this.IsClip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsClip.Location = new System.Drawing.Point(166, 53);
            this.IsClip.Name = "IsClip";
            this.IsClip.Size = new System.Drawing.Size(60, 16);
            this.IsClip.TabIndex = 167;
            this.IsClip.Text = "剪切板";
            this.IsClip.UseVisualStyleBackColor = true;
            // 
            // checkBox_截图
            // 
            this.checkBox_截图.AutoSize = true;
            this.checkBox_截图.Checked = true;
            this.checkBox_截图.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_截图.Location = new System.Drawing.Point(87, 53);
            this.checkBox_截图.Name = "checkBox_截图";
            this.checkBox_截图.Size = new System.Drawing.Size(72, 16);
            this.checkBox_截图.TabIndex = 166;
            this.checkBox_截图.Text = "截图录像";
            this.checkBox_截图.UseVisualStyleBackColor = true;
            // 
            // KADateTimePicker
            // 
            this.KADateTimePicker.Location = new System.Drawing.Point(13, 125);
            this.KADateTimePicker.Name = "KADateTimePicker";
            this.KADateTimePicker.ShowUpDown = true;
            this.KADateTimePicker.Size = new System.Drawing.Size(113, 21);
            this.KADateTimePicker.TabIndex = 165;
            // 
            // MoneyDateTimePicker
            // 
            this.MoneyDateTimePicker.Location = new System.Drawing.Point(147, 98);
            this.MoneyDateTimePicker.Name = "MoneyDateTimePicker";
            this.MoneyDateTimePicker.ShowUpDown = true;
            this.MoneyDateTimePicker.Size = new System.Drawing.Size(126, 21);
            this.MoneyDateTimePicker.TabIndex = 164;
            // 
            // TimeBlockDate
            // 
            this.TimeBlockDate.Location = new System.Drawing.Point(13, 97);
            this.TimeBlockDate.Name = "TimeBlockDate";
            this.TimeBlockDate.ShowUpDown = true;
            this.TimeBlockDate.Size = new System.Drawing.Size(113, 21);
            this.TimeBlockDate.TabIndex = 163;
            // 
            // quietmode
            // 
            this.quietmode.AutoSize = true;
            this.quietmode.Location = new System.Drawing.Point(11, 53);
            this.quietmode.Name = "quietmode";
            this.quietmode.Size = new System.Drawing.Size(72, 16);
            this.quietmode.TabIndex = 162;
            this.quietmode.Text = "安静模式";
            this.quietmode.UseVisualStyleBackColor = true;
            // 
            // StartRecordCheckBox
            // 
            this.StartRecordCheckBox.AutoSize = true;
            this.StartRecordCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartRecordCheckBox.Location = new System.Drawing.Point(89, 75);
            this.StartRecordCheckBox.Name = "StartRecordCheckBox";
            this.StartRecordCheckBox.Size = new System.Drawing.Size(46, 16);
            this.StartRecordCheckBox.TabIndex = 161;
            this.StartRecordCheckBox.TabStop = false;
            this.StartRecordCheckBox.Text = "录音";
            this.StartRecordCheckBox.UseVisualStyleBackColor = true;
            // 
            // c_speechcontrol
            // 
            this.c_speechcontrol.AutoSize = true;
            this.c_speechcontrol.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.c_speechcontrol.Location = new System.Drawing.Point(13, 75);
            this.c_speechcontrol.Name = "c_speechcontrol";
            this.c_speechcontrol.Size = new System.Drawing.Size(70, 16);
            this.c_speechcontrol.TabIndex = 160;
            this.c_speechcontrol.Text = "语音控制";
            this.c_speechcontrol.UseVisualStyleBackColor = true;
            // 
            // showtomorrow
            // 
            this.showtomorrow.AutoSize = true;
            this.showtomorrow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.showtomorrow.Location = new System.Drawing.Point(11, 30);
            this.showtomorrow.Name = "showtomorrow";
            this.showtomorrow.Size = new System.Drawing.Size(46, 16);
            this.showtomorrow.TabIndex = 156;
            this.showtomorrow.TabStop = false;
            this.showtomorrow.Text = "明天";
            this.showtomorrow.UseVisualStyleBackColor = true;
            this.showtomorrow.CheckedChanged += new System.EventHandler(this.showtomorrow_CheckedChanged);
            // 
            // reminder_yearafter
            // 
            this.reminder_yearafter.AutoSize = true;
            this.reminder_yearafter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_yearafter.Location = new System.Drawing.Point(196, 30);
            this.reminder_yearafter.Name = "reminder_yearafter";
            this.reminder_yearafter.Size = new System.Drawing.Size(46, 16);
            this.reminder_yearafter.TabIndex = 155;
            this.reminder_yearafter.TabStop = false;
            this.reminder_yearafter.Text = "年后";
            this.reminder_yearafter.UseVisualStyleBackColor = true;
            this.reminder_yearafter.CheckedChanged += new System.EventHandler(this.reminder_yearafter_CheckedChanged);
            // 
            // reminder_year
            // 
            this.reminder_year.AutoSize = true;
            this.reminder_year.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_year.Location = new System.Drawing.Point(147, 30);
            this.reminder_year.Name = "reminder_year";
            this.reminder_year.Size = new System.Drawing.Size(46, 16);
            this.reminder_year.TabIndex = 154;
            this.reminder_year.TabStop = false;
            this.reminder_year.Text = "一年";
            this.reminder_year.UseVisualStyleBackColor = true;
            this.reminder_year.CheckedChanged += new System.EventHandler(this.reminder_year_CheckedChanged);
            // 
            // reminder_month
            // 
            this.reminder_month.AutoSize = true;
            this.reminder_month.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_month.Location = new System.Drawing.Point(102, 30);
            this.reminder_month.Name = "reminder_month";
            this.reminder_month.Size = new System.Drawing.Size(46, 16);
            this.reminder_month.TabIndex = 153;
            this.reminder_month.TabStop = false;
            this.reminder_month.Text = "一月";
            this.reminder_month.UseVisualStyleBackColor = true;
            // 
            // reminder_week
            // 
            this.reminder_week.AutoSize = true;
            this.reminder_week.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reminder_week.Location = new System.Drawing.Point(57, 30);
            this.reminder_week.Name = "reminder_week";
            this.reminder_week.Size = new System.Drawing.Size(46, 16);
            this.reminder_week.TabIndex = 152;
            this.reminder_week.TabStop = false;
            this.reminder_week.Text = "一周";
            this.reminder_week.UseVisualStyleBackColor = true;
            this.reminder_week.CheckedChanged += new System.EventHandler(this.reminder_week_CheckedChanged);
            // 
            // onlyZhouqi
            // 
            this.onlyZhouqi.AutoSize = true;
            this.onlyZhouqi.Checked = true;
            this.onlyZhouqi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onlyZhouqi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.onlyZhouqi.Location = new System.Drawing.Point(87, 11);
            this.onlyZhouqi.Name = "onlyZhouqi";
            this.onlyZhouqi.Size = new System.Drawing.Size(58, 16);
            this.onlyZhouqi.TabIndex = 157;
            this.onlyZhouqi.TabStop = false;
            this.onlyZhouqi.Text = "只周期";
            this.onlyZhouqi.UseVisualStyleBackColor = true;
            // 
            // ebcheckBox
            // 
            this.ebcheckBox.AutoSize = true;
            this.ebcheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ebcheckBox.Location = new System.Drawing.Point(196, 11);
            this.ebcheckBox.Name = "ebcheckBox";
            this.ebcheckBox.Size = new System.Drawing.Size(46, 16);
            this.ebcheckBox.TabIndex = 158;
            this.ebcheckBox.Text = "记忆";
            this.ebcheckBox.UseVisualStyleBackColor = true;
            // 
            // IsReminderOnlyCheckBox
            // 
            this.IsReminderOnlyCheckBox.AutoSize = true;
            this.IsReminderOnlyCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.IsReminderOnlyCheckBox.Location = new System.Drawing.Point(151, 11);
            this.IsReminderOnlyCheckBox.Name = "IsReminderOnlyCheckBox";
            this.IsReminderOnlyCheckBox.Size = new System.Drawing.Size(46, 16);
            this.IsReminderOnlyCheckBox.TabIndex = 159;
            this.IsReminderOnlyCheckBox.Text = "任务";
            this.IsReminderOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // SwitchingState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 154);
            this.Controls.Add(this.drawioPicBigger);
            this.Controls.Add(this.IsDiary);
            this.Controls.Add(this.IsClip);
            this.Controls.Add(this.checkBox_截图);
            this.Controls.Add(this.KADateTimePicker);
            this.Controls.Add(this.MoneyDateTimePicker);
            this.Controls.Add(this.TimeBlockDate);
            this.Controls.Add(this.quietmode);
            this.Controls.Add(this.StartRecordCheckBox);
            this.Controls.Add(this.c_speechcontrol);
            this.Controls.Add(this.showtomorrow);
            this.Controls.Add(this.reminder_yearafter);
            this.Controls.Add(this.reminder_year);
            this.Controls.Add(this.reminder_month);
            this.Controls.Add(this.reminder_week);
            this.Controls.Add(this.onlyZhouqi);
            this.Controls.Add(this.ebcheckBox);
            this.Controls.Add(this.IsReminderOnlyCheckBox);
            this.Controls.Add(this.showcyclereminder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SwitchingState";
            this.Text = "开关";
            this.Load += new System.EventHandler(this.SwitchingState_Load);
            ((System.ComponentModel.ISupportInitialize)(this.drawioPicBigger)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox showcyclereminder;
        public System.Windows.Forms.PictureBox drawioPicBigger;
        public System.Windows.Forms.CheckBox IsDiary;
        public System.Windows.Forms.CheckBox IsClip;
        public System.Windows.Forms.CheckBox checkBox_截图;
        public System.Windows.Forms.DateTimePicker KADateTimePicker;
        public System.Windows.Forms.DateTimePicker MoneyDateTimePicker;
        public System.Windows.Forms.DateTimePicker TimeBlockDate;
        public System.Windows.Forms.CheckBox quietmode;
        public System.Windows.Forms.CheckBox StartRecordCheckBox;
        public System.Windows.Forms.CheckBox c_speechcontrol;
        public System.Windows.Forms.CheckBox showtomorrow;
        public System.Windows.Forms.CheckBox reminder_yearafter;
        public System.Windows.Forms.CheckBox reminder_year;
        public System.Windows.Forms.CheckBox reminder_month;
        public System.Windows.Forms.CheckBox reminder_week;
        public System.Windows.Forms.CheckBox onlyZhouqi;
        public System.Windows.Forms.CheckBox ebcheckBox;
        public System.Windows.Forms.CheckBox IsReminderOnlyCheckBox;
    }
}
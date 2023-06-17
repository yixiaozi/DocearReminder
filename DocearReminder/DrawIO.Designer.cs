namespace DocearReminder
{
    partial class DrawIO
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
            this.DrawList = new yixiaozi.WinForm.Control.CustomCheckedListBox();
            this.reminderList = new yixiaozi.WinForm.Control.SortByTimeListBox();
            this.searchword = new System.Windows.Forms.TextBox();
            this.LoadAll = new System.Windows.Forms.Button();
            this.tasklevel = new System.Windows.Forms.NumericUpDown();
            this.taskTime = new System.Windows.Forms.NumericUpDown();
            this.n_days = new System.Windows.Forms.NumericUpDown();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.tasklevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_days)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawList
            // 
            this.DrawList.BackColor = System.Drawing.Color.White;
            this.DrawList.CausesValidation = false;
            this.DrawList.DrawFocusedIndicator = false;
            this.DrawList.Font = new System.Drawing.Font("宋体", 9.5F);
            this.DrawList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.DrawList.FormattingEnabled = true;
            this.DrawList.Location = new System.Drawing.Point(9, 9);
            this.DrawList.Margin = new System.Windows.Forms.Padding(0);
            this.DrawList.Name = "DrawList";
            this.DrawList.Size = new System.Drawing.Size(233, 648);
            this.DrawList.Sorted = true;
            this.DrawList.TabIndex = 2;
            this.DrawList.Click += new System.EventHandler(this.DrawList_Click);
            this.DrawList.DoubleClick += new System.EventHandler(this.DrawList_DoubleClick);
            // 
            // reminderList
            // 
            this.reminderList.AllowDrop = true;
            this.reminderList.BackColor = System.Drawing.Color.White;
            this.reminderList.CausesValidation = false;
            this.reminderList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.reminderList.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reminderList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.reminderList.FormattingEnabled = true;
            this.reminderList.ItemHeight = 14;
            this.reminderList.Location = new System.Drawing.Point(257, 49);
            this.reminderList.Name = "reminderList";
            this.reminderList.Size = new System.Drawing.Size(600, 606);
            this.reminderList.Sorted = true;
            this.reminderList.TabIndex = 4;
            this.reminderList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Reminderlist_DrawItem);
            // 
            // searchword
            // 
            this.searchword.Location = new System.Drawing.Point(257, 12);
            this.searchword.Name = "searchword";
            this.searchword.Size = new System.Drawing.Size(600, 21);
            this.searchword.TabIndex = 5;
            // 
            // LoadAll
            // 
            this.LoadAll.Location = new System.Drawing.Point(863, 49);
            this.LoadAll.Name = "LoadAll";
            this.LoadAll.Size = new System.Drawing.Size(127, 54);
            this.LoadAll.TabIndex = 6;
            this.LoadAll.Text = "加载";
            this.LoadAll.UseVisualStyleBackColor = true;
            this.LoadAll.Click += new System.EventHandler(this.LoadAll_Click);
            // 
            // tasklevel
            // 
            this.tasklevel.ForeColor = System.Drawing.Color.Gray;
            this.tasklevel.Location = new System.Drawing.Point(1128, 13);
            this.tasklevel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tasklevel.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.tasklevel.Name = "tasklevel";
            this.tasklevel.Size = new System.Drawing.Size(35, 21);
            this.tasklevel.TabIndex = 52;
            this.tasklevel.TabStop = false;
            // 
            // taskTime
            // 
            this.taskTime.ForeColor = System.Drawing.Color.Gray;
            this.taskTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.taskTime.Location = new System.Drawing.Point(1086, 13);
            this.taskTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.taskTime.Name = "taskTime";
            this.taskTime.Size = new System.Drawing.Size(38, 21);
            this.taskTime.TabIndex = 49;
            this.taskTime.TabStop = false;
            // 
            // n_days
            // 
            this.n_days.ForeColor = System.Drawing.Color.Gray;
            this.n_days.Location = new System.Drawing.Point(1167, 13);
            this.n_days.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.n_days.Name = "n_days";
            this.n_days.Size = new System.Drawing.Size(35, 21);
            this.n_days.TabIndex = 51;
            this.n_days.TabStop = false;
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
            this.dateTimePicker.CustomFormat = "HH:mm MM月dd yyyy dddd";
            this.dateTimePicker.Font = new System.Drawing.Font("宋体", 8F);
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(863, 13);
            this.dateTimePicker.MaxDate = new System.DateTime(2499, 12, 17, 0, 0, 0, 0);
            this.dateTimePicker.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.ShowUpDown = true;
            this.dateTimePicker.Size = new System.Drawing.Size(217, 20);
            this.dateTimePicker.TabIndex = 50;
            this.dateTimePicker.TabStop = false;
            // 
            // DrawIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 667);
            this.Controls.Add(this.tasklevel);
            this.Controls.Add(this.taskTime);
            this.Controls.Add(this.n_days);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.LoadAll);
            this.Controls.Add(this.searchword);
            this.Controls.Add(this.reminderList);
            this.Controls.Add(this.DrawList);
            this.Name = "DrawIO";
            this.Text = "DrawIO";
            ((System.ComponentModel.ISupportInitialize)(this.tasklevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_days)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private yixiaozi.WinForm.Control.CustomCheckedListBox DrawList;
        private yixiaozi.WinForm.Control.SortByTimeListBox reminderList;
        private System.Windows.Forms.TextBox searchword;
        private System.Windows.Forms.Button LoadAll;
        private System.Windows.Forms.NumericUpDown tasklevel;
        private System.Windows.Forms.NumericUpDown taskTime;
        private System.Windows.Forms.NumericUpDown n_days;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}
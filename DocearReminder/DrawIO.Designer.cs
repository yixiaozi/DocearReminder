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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawIO));
            this.DrawList = new yixiaozi.WinForm.Control.CustomCheckedListBox();
            this.reminderList = new yixiaozi.WinForm.Control.SortByTimeListBox();
            this.searchword = new System.Windows.Forms.TextBox();
            this.LoadAll = new System.Windows.Forms.Button();
            this.tasklevel = new System.Windows.Forms.NumericUpDown();
            this.taskTime = new System.Windows.Forms.NumericUpDown();
            this.n_days = new System.Windows.Forms.NumericUpDown();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.添加图形 = new System.Windows.Forms.Button();
            this.xposition = new System.Windows.Forms.TextBox();
            this.yposition = new System.Windows.Forms.TextBox();
            this.width = new System.Windows.Forms.TextBox();
            this.height = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button2 = new System.Windows.Forms.Button();
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
            this.DrawList.SelectedIndexChanged += new System.EventHandler(this.DrawList_SelectedIndexChanged);
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
            this.reminderList.Click += new System.EventHandler(this.reminderList_Click);
            this.reminderList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Reminderlist_DrawItem);
            this.reminderList.SelectedIndexChanged += new System.EventHandler(this.reminderList_SelectedIndexChanged);
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
            this.LoadAll.Location = new System.Drawing.Point(1131, 80);
            this.LoadAll.Name = "LoadAll";
            this.LoadAll.Size = new System.Drawing.Size(72, 54);
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
            this.dateTimePicker.CustomFormat = "yyyy年 MM月dd dddd";
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1040, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 54);
            this.button1.TabIndex = 53;
            this.button1.Text = "设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 添加图形
            // 
            this.添加图形.Location = new System.Drawing.Point(863, 80);
            this.添加图形.Name = "添加图形";
            this.添加图形.Size = new System.Drawing.Size(70, 51);
            this.添加图形.TabIndex = 54;
            this.添加图形.Text = "添加";
            this.添加图形.UseVisualStyleBackColor = true;
            this.添加图形.Click += new System.EventHandler(this.button2_Click);
            // 
            // xposition
            // 
            this.xposition.Location = new System.Drawing.Point(864, 49);
            this.xposition.Name = "xposition";
            this.xposition.Size = new System.Drawing.Size(71, 21);
            this.xposition.TabIndex = 55;
            // 
            // yposition
            // 
            this.yposition.Location = new System.Drawing.Point(952, 49);
            this.yposition.Name = "yposition";
            this.yposition.Size = new System.Drawing.Size(69, 21);
            this.yposition.TabIndex = 56;
            // 
            // width
            // 
            this.width.Location = new System.Drawing.Point(1128, 49);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(75, 21);
            this.width.TabIndex = 57;
            // 
            // height
            // 
            this.height.Location = new System.Drawing.Point(1039, 50);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(70, 21);
            this.height.TabIndex = 58;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(864, 137);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(158, 51);
            this.button3.TabIndex = 59;
            this.button3.Text = "查找有没有当前名称的图";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(952, 80);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(70, 52);
            this.delete.TabIndex = 60;
            this.delete.Text = "删除";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(864, 307);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(339, 350);
            this.treeView1.TabIndex = 61;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(864, 250);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 51);
            this.button2.TabIndex = 62;
            this.button2.Text = "生成关系树";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // DrawIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 667);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.height);
            this.Controls.Add(this.width);
            this.Controls.Add(this.yposition);
            this.Controls.Add(this.xposition);
            this.Controls.Add(this.添加图形);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tasklevel);
            this.Controls.Add(this.taskTime);
            this.Controls.Add(this.n_days);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.LoadAll);
            this.Controls.Add(this.searchword);
            this.Controls.Add(this.reminderList);
            this.Controls.Add(this.DrawList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DrawIO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button 添加图形;
        private System.Windows.Forms.TextBox xposition;
        private System.Windows.Forms.TextBox yposition;
        private System.Windows.Forms.TextBox width;
        private System.Windows.Forms.TextBox height;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button2;
    }
}
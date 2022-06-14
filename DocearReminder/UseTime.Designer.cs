using System;

namespace DocearReminder
{
    partial class UseTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UseTime));
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.section = new System.Windows.Forms.TextBox();
            this.file = new System.Windows.Forms.TextBox();
            this.log = new System.Windows.Forms.TextBox();
            this.FormNamesSelect = new System.Windows.Forms.ComboBox();
            this.Search = new System.Windows.Forms.Button();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.formsPlot3 = new ScottPlot.FormsPlot();
            this.窗体 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.formsPlot4 = new ScottPlot.FormsPlot();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.percent = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.AutoSize = true;
            this.formsPlot1.Location = new System.Drawing.Point(12, 48);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(1500, 400);
            this.formsPlot1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(75, 10);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(107, 21);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Value = DateTime.Today;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间：";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(256, 10);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(131, 21);
            this.dateTimePicker2.TabIndex = 4;
            this.dateTimePicker2.Value = DateTime.Now;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // section
            // 
            this.section.Location = new System.Drawing.Point(600, 10);
            this.section.Name = "section";
            this.section.Size = new System.Drawing.Size(128, 21);
            this.section.TabIndex = 5;
            this.section.KeyUp += new System.Windows.Forms.KeyEventHandler(this.section_TextChanged);
            // 
            // file
            // 
            this.file.Location = new System.Drawing.Point(776, 10);
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(136, 21);
            this.file.TabIndex = 6;
            this.file.KeyUp += new System.Windows.Forms.KeyEventHandler(this.section_TextChanged);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(960, 10);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(159, 21);
            this.log.TabIndex = 7;
            this.log.KeyUp += new System.Windows.Forms.KeyEventHandler(this.section_TextChanged);
            // 
            // FormNamesSelect
            // 
            this.FormNamesSelect.FormattingEnabled = true;
            this.FormNamesSelect.Items.AddRange(new object[] {
            "主窗口",
            "日历",
            "历史记录",
            "剪切板",
            "工具窗口",
            "报表-时间块",
            "报表-键盘",
            "报表-使用记录",
            "所有"});
            this.FormNamesSelect.Location = new System.Drawing.Point(434, 10);
            this.FormNamesSelect.Name = "FormNamesSelect";
            this.FormNamesSelect.Size = new System.Drawing.Size(121, 20);
            this.FormNamesSelect.TabIndex = 8;
            this.FormNamesSelect.Text = "主窗口";
            this.FormNamesSelect.SelectedIndexChanged += new System.EventHandler(this.FormNamesSelect_SelectedIndexChanged);
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(1142, 8);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(95, 25);
            this.Search.TabIndex = 9;
            this.Search.Text = "统计";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // formsPlot2
            // 
            this.formsPlot2.Location = new System.Drawing.Point(12, 450);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(500, 400);
            this.formsPlot2.TabIndex = 10;
            // 
            // formsPlot3
            // 
            this.formsPlot3.Location = new System.Drawing.Point(512, 450);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(500, 400);
            this.formsPlot3.TabIndex = 11;
            // 
            // 窗体
            // 
            this.窗体.AutoSize = true;
            this.窗体.Location = new System.Drawing.Point(393, 15);
            this.窗体.Name = "窗体";
            this.窗体.Size = new System.Drawing.Size(41, 12);
            this.窗体.TabIndex = 12;
            this.窗体.Text = "窗体：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(558, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "分类：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(734, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "文件：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(915, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "日志：";
            // 
            // formsPlot4
            // 
            this.formsPlot4.Location = new System.Drawing.Point(1012, 454);
            this.formsPlot4.Name = "formsPlot4";
            this.formsPlot4.Size = new System.Drawing.Size(500, 400);
            this.formsPlot4.TabIndex = 16;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(1389, 15);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 19;
            this.checkBox2.Text = "名称";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1346, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "时长";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // percent
            // 
            this.percent.AutoSize = true;
            this.percent.Checked = true;
            this.percent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.percent.Location = new System.Drawing.Point(1291, 15);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(60, 16);
            this.percent.TabIndex = 17;
            this.percent.Text = "百分比";
            this.percent.UseVisualStyleBackColor = true;
            this.percent.CheckedChanged += new System.EventHandler(this.percent_CheckedChanged);
            // 
            // UseTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1511, 861);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.percent);
            this.Controls.Add(this.formsPlot4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.窗体);
            this.Controls.Add(this.formsPlot3);
            this.Controls.Add(this.formsPlot2);
            this.Controls.Add(this.Search);
            this.Controls.Add(this.FormNamesSelect);
            this.Controls.Add(this.log);
            this.Controls.Add(this.file);
            this.Controls.Add(this.section);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formsPlot1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UseTime";
            this.Text = "使用分析";
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.Load += new System.EventHandler(this.UseTime_Load);
            this.Resize += new System.EventHandler(this.UseTime_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox section;
        private System.Windows.Forms.TextBox file;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.ComboBox FormNamesSelect;
        private System.Windows.Forms.Button Search;
        private ScottPlot.FormsPlot formsPlot2;
        private ScottPlot.FormsPlot formsPlot3;
        private System.Windows.Forms.Label 窗体;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private ScottPlot.FormsPlot formsPlot4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox percent;
    }
}
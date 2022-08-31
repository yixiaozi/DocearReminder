using System;

namespace DocearReminder
{
    partial class TimeBlockTrend
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
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.startDt = new System.Windows.Forms.DateTimePicker();
            this.endDT = new System.Windows.Forms.DateTimePicker();
            this.searchword = new System.Windows.Forms.TextBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.emptyDay = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(12, 55);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(1292, 539);
            this.formsPlot1.TabIndex = 0;
            // 
            // startDt
            // 
            this.startDt.Location = new System.Drawing.Point(44, 13);
            this.startDt.Name = "startDt";
            this.startDt.Size = new System.Drawing.Size(137, 21);
            this.startDt.TabIndex = 1;
            this.startDt.Value = new System.DateTime(2022, 8, 24, 0, 0, 0, 0);
            this.startDt.ValueChanged += new System.EventHandler(this.startDt_ValueChanged);
            // 
            // endDT
            // 
            this.endDT.Location = new System.Drawing.Point(221, 13);
            this.endDT.Name = "endDT";
            this.endDT.Size = new System.Drawing.Size(159, 21);
            this.endDT.TabIndex = 2;
            this.endDT.Value = new System.DateTime(2022, 8, 31, 21, 9, 5, 693);
            this.endDT.ValueChanged += new System.EventHandler(this.endDT_ValueChanged);
            // 
            // searchword
            // 
            this.searchword.Location = new System.Drawing.Point(621, 13);
            this.searchword.Name = "searchword";
            this.searchword.Size = new System.Drawing.Size(322, 21);
            this.searchword.TabIndex = 3;
            this.searchword.TextChanged += new System.EventHandler(this.searchword_TextChanged);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(963, 13);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 23);
            this.SearchBtn.TabIndex = 4;
            this.SearchBtn.Text = "统计";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // emptyDay
            // 
            this.emptyDay.AutoSize = true;
            this.emptyDay.Checked = true;
            this.emptyDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.emptyDay.Location = new System.Drawing.Point(389, 15);
            this.emptyDay.Name = "emptyDay";
            this.emptyDay.Size = new System.Drawing.Size(84, 16);
            this.emptyDay.TabIndex = 12;
            this.emptyDay.Text = "显示空白日";
            this.emptyDay.UseVisualStyleBackColor = true;
            this.emptyDay.CheckedChanged += new System.EventHandler(this.emptyDay_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "所有类别",
            "工作",
            "事务",
            "形象改造工程",
            "编程",
            "学习",
            "英语",
            "阅读",
            "精彩",
            "运动",
            "维持",
            "电脑",
            "手机",
            "平板",
            "解构魔幻",
            "休息",
            "浪费",
            "未分类",
            "音乐"});
            this.comboBox1.Location = new System.Drawing.Point(486, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 20);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.Text = "所有类别";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1061, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "平均值";
            // 
            // TimeBlockTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 614);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.emptyDay);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.searchword);
            this.Controls.Add(this.endDT);
            this.Controls.Add(this.startDt);
            this.Controls.Add(this.formsPlot1);
            this.Name = "TimeBlockTrend";
            this.Text = "TimeBlockTrend";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.DateTimePicker startDt;
        private System.Windows.Forms.DateTimePicker endDT;
        private System.Windows.Forms.TextBox searchword;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.CheckBox emptyDay;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}
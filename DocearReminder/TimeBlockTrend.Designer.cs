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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.percent = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            this.startDt.Value = DateTime.Today.AddDays(-7);
            // 
            // endDT
            // 
            this.endDT.Location = new System.Drawing.Point(221, 13);
            this.endDT.Name = "endDT";
            this.endDT.Size = new System.Drawing.Size(159, 21);
            this.endDT.TabIndex = 2;
            this.endDT.Value = DateTime.Now;

            // 
            // searchword
            // 
            this.searchword.Location = new System.Drawing.Point(647, 13);
            this.searchword.Name = "searchword";
            this.searchword.Size = new System.Drawing.Size(296, 21);
            this.searchword.TabIndex = 3;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(963, 15);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 23);
            this.SearchBtn.TabIndex = 4;
            this.SearchBtn.Text = "统计";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(487, 15);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "名称";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(444, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "时长";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // percent
            // 
            this.percent.AutoSize = true;
            this.percent.Checked = true;
            this.percent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.percent.Location = new System.Drawing.Point(389, 15);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(60, 16);
            this.percent.TabIndex = 12;
            this.percent.Text = "百分比";
            this.percent.UseVisualStyleBackColor = true;
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
            this.comboBox1.Location = new System.Drawing.Point(537, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 20);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.Text = "所有类别";
            // 
            // TimeBlockTrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 614);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.percent);
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
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox percent;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
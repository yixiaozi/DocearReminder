﻿
using System;

namespace DocearReminder
{
    partial class TimeBlockReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeBlockReport));
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.label1 = new System.Windows.Forms.Label();
            this.startDt = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.endDT = new System.Windows.Forms.DateTimePicker();
            this.textBox_searchwork = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.percent = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.exclude = new System.Windows.Forms.TextBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.AutoSize = true;
            this.formsPlot1.Location = new System.Drawing.Point(2, 43);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(906, 695);
            this.formsPlot1.TabIndex = 0;
            this.formsPlot1.Load += new System.EventHandler(this.formsPlot1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "开始时间：";
            // 
            // startDt
            // 
            this.startDt.Location = new System.Drawing.Point(72, 13);
            this.startDt.Name = "startDt";
            this.startDt.Size = new System.Drawing.Size(124, 21);
            this.startDt.TabIndex = 2;
            this.startDt.Value = new System.DateTime(2022, 9, 23, 0, 0, 0, 0);
            this.startDt.ValueChanged += new System.EventHandler(this.startDt_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间：";
            // 
            // endDT
            // 
            this.endDT.Location = new System.Drawing.Point(264, 15);
            this.endDT.Name = "endDT";
            this.endDT.Size = new System.Drawing.Size(112, 21);
            this.endDT.TabIndex = 4;
            this.endDT.Value = new System.DateTime(2022, 9, 23, 21, 38, 45, 292);
            this.endDT.ValueChanged += new System.EventHandler(this.startDt_ValueChanged);
            // 
            // textBox_searchwork
            // 
            this.textBox_searchwork.Location = new System.Drawing.Point(699, 16);
            this.textBox_searchwork.Name = "textBox_searchwork";
            this.textBox_searchwork.Size = new System.Drawing.Size(78, 21);
            this.textBox_searchwork.TabIndex = 6;
            this.textBox_searchwork.TextChanged += new System.EventHandler(this.searchword_TextChanged);
            this.textBox_searchwork.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
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
            this.comboBox1.Location = new System.Drawing.Point(587, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Text = "所有类别";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // percent
            // 
            this.percent.AutoSize = true;
            this.percent.Checked = true;
            this.percent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.percent.Location = new System.Drawing.Point(382, 18);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(60, 16);
            this.percent.TabIndex = 8;
            this.percent.Text = "百分比";
            this.percent.UseVisualStyleBackColor = true;
            this.percent.CheckedChanged += new System.EventHandler(this.percent_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(437, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "时长";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(480, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "名称";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // exclude
            // 
            this.exclude.Location = new System.Drawing.Point(786, 15);
            this.exclude.Name = "exclude";
            this.exclude.Size = new System.Drawing.Size(100, 21);
            this.exclude.TabIndex = 11;
            this.exclude.TextChanged += new System.EventHandler(this.exclude_TextChanged);
            this.exclude.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(526, 19);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(60, 16);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "Legend";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // TimeBlockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(909, 739);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.exclude);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.percent);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox_searchwork);
            this.Controls.Add(this.endDT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startDt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formsPlot1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TimeBlockReport";
            this.Text = "时间块";
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.Load += new System.EventHandler(this.TimeBlockReport_Load);
            this.Resize += new System.EventHandler(this.TimeBlockReport_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker startDt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker endDT;
        private System.Windows.Forms.TextBox textBox_searchwork;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox percent;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox exclude;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}
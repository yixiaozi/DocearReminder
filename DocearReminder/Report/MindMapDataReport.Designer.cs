﻿
using System;

namespace DocearReminder
{
    partial class MindMapDataReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MindMapDataReport));
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.textBox_mindmappath = new System.Windows.Forms.TextBox();
            this.fathernodename = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.begin = new System.Windows.Forms.DateTimePicker();
            this.end = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.nodename = new System.Windows.Forms.TextBox();
            this.nodenameexc = new System.Windows.Forms.TextBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.formsPlot3 = new ScottPlot.FormsPlot();
            this.formsPlot4 = new ScottPlot.FormsPlot();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.showtext = new System.Windows.Forms.CheckBox();
            this.checkBox_showEdit = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.AutoSize = true;
            this.formsPlot1.Location = new System.Drawing.Point(2, 42);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(1160, 475);
            this.formsPlot1.TabIndex = 0;
            // 
            // textBox_mindmappath
            // 
            this.textBox_mindmappath.Location = new System.Drawing.Point(74, 18);
            this.textBox_mindmappath.Name = "textBox_mindmappath";
            this.textBox_mindmappath.Size = new System.Drawing.Size(192, 21);
            this.textBox_mindmappath.TabIndex = 6;
            this.textBox_mindmappath.TextChanged += new System.EventHandler(this.searchword_TextChanged);
            this.textBox_mindmappath.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
            // 
            // fathernodename
            // 
            this.fathernodename.Location = new System.Drawing.Point(341, 20);
            this.fathernodename.Name = "fathernodename";
            this.fathernodename.Size = new System.Drawing.Size(175, 21);
            this.fathernodename.TabIndex = 11;
            this.fathernodename.TextChanged += new System.EventHandler(this.exclude_TextChanged);
            this.fathernodename.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1518, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "分析";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1181, 68);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(591, 785);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // begin
            // 
            this.begin.Location = new System.Drawing.Point(1181, 13);
            this.begin.Name = "begin";
            this.begin.Size = new System.Drawing.Size(157, 21);
            this.begin.TabIndex = 14;
            this.begin.Value = new System.DateTime(2009, 1, 31, 0, 0, 0, 0);
            this.begin.ValueChanged += new System.EventHandler(this.begin_ValueChanged);
            // 
            // end
            // 
            this.end.Location = new System.Drawing.Point(1344, 12);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(168, 21);
            this.end.TabIndex = 15;
            this.end.ValueChanged += new System.EventHandler(this.end_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(1587, 10);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(55, 21);
            this.numericUpDown1.TabIndex = 16;
            this.numericUpDown1.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // nodename
            // 
            this.nodename.Location = new System.Drawing.Point(569, 19);
            this.nodename.Name = "nodename";
            this.nodename.Size = new System.Drawing.Size(192, 21);
            this.nodename.TabIndex = 17;
            this.nodename.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
            // 
            // nodenameexc
            // 
            this.nodenameexc.Location = new System.Drawing.Point(832, 17);
            this.nodenameexc.Name = "nodenameexc";
            this.nodenameexc.Size = new System.Drawing.Size(158, 21);
            this.nodenameexc.TabIndex = 18;
            this.nodenameexc.TextChanged += new System.EventHandler(this.nodenameexc_TextChanged);
            this.nodenameexc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_searchwork_KeyUp);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(1648, 10);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(63, 21);
            this.numericUpDown2.TabIndex = 19;
            this.numericUpDown2.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // formsPlot2
            // 
            this.formsPlot2.Location = new System.Drawing.Point(12, 527);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(366, 354);
            this.formsPlot2.TabIndex = 20;
            // 
            // formsPlot3
            // 
            this.formsPlot3.Location = new System.Drawing.Point(384, 527);
            this.formsPlot3.Name = "formsPlot3";
            this.formsPlot3.Size = new System.Drawing.Size(454, 354);
            this.formsPlot3.TabIndex = 21;
            // 
            // formsPlot4
            // 
            this.formsPlot4.Location = new System.Drawing.Point(844, 527);
            this.formsPlot4.Name = "formsPlot4";
            this.formsPlot4.Size = new System.Drawing.Size(331, 354);
            this.formsPlot4.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "导图路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "父节点：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(522, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "节点：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(767, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "节点排除：";
            // 
            // showtext
            // 
            this.showtext.AutoSize = true;
            this.showtext.Checked = true;
            this.showtext.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showtext.Location = new System.Drawing.Point(1713, 12);
            this.showtext.Name = "showtext";
            this.showtext.Size = new System.Drawing.Size(72, 16);
            this.showtext.TabIndex = 27;
            this.showtext.Text = "显示文字";
            this.showtext.UseVisualStyleBackColor = true;
            // 
            // checkBox_showEdit
            // 
            this.checkBox_showEdit.AutoSize = true;
            this.checkBox_showEdit.Location = new System.Drawing.Point(1043, 21);
            this.checkBox_showEdit.Name = "checkBox_showEdit";
            this.checkBox_showEdit.Size = new System.Drawing.Size(72, 16);
            this.checkBox_showEdit.TabIndex = 28;
            this.checkBox_showEdit.Text = "编辑时间";
            this.checkBox_showEdit.UseVisualStyleBackColor = true;
            this.checkBox_showEdit.CheckedChanged += new System.EventHandler(this.checkBox_showEdit_CheckedChanged);
            // 
            // MindMapDataReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1784, 865);
            this.Controls.Add(this.checkBox_showEdit);
            this.Controls.Add(this.showtext);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formsPlot4);
            this.Controls.Add(this.formsPlot3);
            this.Controls.Add(this.formsPlot2);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.nodenameexc);
            this.Controls.Add(this.nodename);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.end);
            this.Controls.Add(this.begin);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fathernodename);
            this.Controls.Add(this.textBox_mindmappath);
            this.Controls.Add(this.formsPlot1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MindMapDataReport";
            this.Text = "导图节点时间分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.Load += new System.EventHandler(this.TimeBlockReport_Load);
            this.Resize += new System.EventHandler(this.TimeBlockReport_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.TextBox textBox_mindmappath;
        private System.Windows.Forms.TextBox fathernodename;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DateTimePicker begin;
        private System.Windows.Forms.DateTimePicker end;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox nodename;
        private System.Windows.Forms.TextBox nodenameexc;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private ScottPlot.FormsPlot formsPlot2;
        private ScottPlot.FormsPlot formsPlot3;
        private ScottPlot.FormsPlot formsPlot4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox showtext;
        private System.Windows.Forms.CheckBox checkBox_showEdit;
    }
}

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
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(2, 67);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(674, 584);
            this.formsPlot1.TabIndex = 0;
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
            startDt.Value = DateTime.Today;
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
            this.endDT.Location = new System.Drawing.Point(264, 13);
            this.endDT.Name = "endDT";
            this.endDT.Size = new System.Drawing.Size(112, 21);
            this.endDT.TabIndex = 4;
            endDT.Value = DateTime.Today;
            this.endDT.ValueChanged += new System.EventHandler(this.startDt_ValueChanged);
            // 
            // TimeBlockReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(681, 657);
            this.Controls.Add(this.endDT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startDt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formsPlot1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TimeBlockReport";
            this.Text = "时间块";
            this.Load += new System.EventHandler(this.TimeBlockReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScottPlot.FormsPlot formsPlot1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker startDt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker endDT;
    }
}
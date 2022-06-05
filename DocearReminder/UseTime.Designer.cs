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
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.AutoSize = true;
            this.formsPlot1.Location = new System.Drawing.Point(13, 50);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(1047, 421);
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
            this.dateTimePicker1.Location = new System.Drawing.Point(75, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(107, 21);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Value = new System.DateTime(2022, 6, 5, 0, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间：";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(258, 10);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(131, 21);
            this.dateTimePicker2.TabIndex = 4;
            this.dateTimePicker2.Value = new System.DateTime(2022, 6, 6, 0, 0, 0, 0);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // section
            // 
            this.section.Location = new System.Drawing.Point(439, 9);
            this.section.Name = "section";
            this.section.Size = new System.Drawing.Size(134, 21);
            this.section.TabIndex = 5;
            this.section.KeyUp += new System.Windows.Forms.KeyEventHandler(this.section_TextChanged);
            // 
            // file
            // 
            this.file.Location = new System.Drawing.Point(615, 10);
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(100, 21);
            this.file.TabIndex = 6;
            this.file.KeyUp += new System.Windows.Forms.KeyEventHandler(this.section_TextChanged);
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(777, 9);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(100, 21);
            this.log.TabIndex = 7;
            this.log.KeyUp += new System.Windows.Forms.KeyEventHandler(this.section_TextChanged);
            // 
            // UseTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1072, 483);
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
    }
}

namespace DocearReminder
{
    partial class LogForm
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
            this.resultlistBox = new DocearReminder.LogForm.SortByTimeListBox();
            this.keyword = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Showbackup = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // resultlistBox
            // 
            this.resultlistBox.FormattingEnabled = true;
            this.resultlistBox.ItemHeight = 12;
            this.resultlistBox.Location = new System.Drawing.Point(12, 42);
            this.resultlistBox.Name = "resultlistBox";
            this.resultlistBox.Size = new System.Drawing.Size(1110, 688);
            this.resultlistBox.TabIndex = 0;
            this.resultlistBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.resultlistBox_MouseClick);
            this.resultlistBox.DoubleClick += new System.EventHandler(this.resultlistBox_DoubleClick);
            this.resultlistBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.resultlistBox_KeyUp);
            // 
            // keyword
            // 
            this.keyword.Location = new System.Drawing.Point(12, 13);
            this.keyword.Name = "keyword";
            this.keyword.Size = new System.Drawing.Size(960, 21);
            this.keyword.TabIndex = 1;
            this.keyword.TextChanged += new System.EventHandler(this.keyword_TextChanged);
            this.keyword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyword_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1047, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "搜索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Showbackup
            // 
            this.Showbackup.AutoSize = true;
            this.Showbackup.Location = new System.Drawing.Point(976, 18);
            this.Showbackup.Name = "Showbackup";
            this.Showbackup.Size = new System.Drawing.Size(72, 16);
            this.Showbackup.TabIndex = 3;
            this.Showbackup.Text = "显示备份";
            this.Showbackup.UseVisualStyleBackColor = true;
            this.Showbackup.CheckedChanged += new System.EventHandler(this.Showbackup_CheckedChanged);
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 735);
            this.Controls.Add(this.Showbackup);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.keyword);
            this.Controls.Add(this.resultlistBox);
            this.Name = "Log";
            this.Text = "Log";
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.SizeChanged += new System.EventHandler(this.MainPage_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Log_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SortByTimeListBox resultlistBox;
        private System.Windows.Forms.TextBox keyword;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox Showbackup;
    }
}
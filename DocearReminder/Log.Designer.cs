
namespace DocearReminder
{
    partial class Log
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
            this.resultlistBox = new DocearReminder.Log.SortByTimeListBox();
            this.keyword = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.resultlistBox.DoubleClick += new System.EventHandler(this.resultlistBox_DoubleClick);
            // 
            // keyword
            // 
            this.keyword.Location = new System.Drawing.Point(12, 13);
            this.keyword.Name = "keyword";
            this.keyword.Size = new System.Drawing.Size(1029, 21);
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
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 735);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.keyword);
            this.Controls.Add(this.resultlistBox);
            this.Name = "Log";
            this.Text = "Log";
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.SizeChanged += new System.EventHandler(this.MainPage_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SortByTimeListBox resultlistBox;
        private System.Windows.Forms.TextBox keyword;
        private System.Windows.Forms.Button button1;
    }
}
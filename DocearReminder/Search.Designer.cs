namespace DocearReminder
{
    partial class Search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.keyword = new System.Windows.Forms.TextBox();
            this.searchbutton = new System.Windows.Forms.Button();
            this.resultlistBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // keyword
            // 
            this.keyword.Location = new System.Drawing.Point(12, 11);
            this.keyword.Name = "keyword";
            this.keyword.Size = new System.Drawing.Size(1059, 21);
            this.keyword.TabIndex = 0;
            this.keyword.TextChanged += new System.EventHandler(this.keyword_TextChanged);
            this.keyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyword_KeyDown);
            // 
            // searchbutton
            // 
            this.searchbutton.Location = new System.Drawing.Point(1077, 12);
            this.searchbutton.Name = "searchbutton";
            this.searchbutton.Size = new System.Drawing.Size(75, 21);
            this.searchbutton.TabIndex = 1;
            this.searchbutton.Text = "搜索";
            this.searchbutton.UseVisualStyleBackColor = true;
            this.searchbutton.Click += new System.EventHandler(this.searchbutton_Click);
            // 
            // resultlistBox
            // 
            this.resultlistBox.FormattingEnabled = true;
            this.resultlistBox.ItemHeight = 12;
            this.resultlistBox.Location = new System.Drawing.Point(13, 36);
            this.resultlistBox.Name = "resultlistBox";
            this.resultlistBox.Size = new System.Drawing.Size(1139, 556);
            this.resultlistBox.TabIndex = 2;
            this.resultlistBox.SelectedIndexChanged += new System.EventHandler(this.resultlistBox_SelectedIndexChanged);
            this.resultlistBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.resultlistBox_MouseDoubleClick);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 599);
            this.Controls.Add(this.resultlistBox);
            this.Controls.Add(this.searchbutton);
            this.Controls.Add(this.keyword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Search";
            this.Text = "Search";
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.SizeChanged += new System.EventHandler(this.MainPage_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyword;
        private System.Windows.Forms.Button searchbutton;
        private System.Windows.Forms.ListBox resultlistBox;
    }
}
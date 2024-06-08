namespace DocearReminder
{
    partial class TagCloud
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
            this.tagCloudControl = new yixiaozi.WinForm.Control.TagCloud.TagCloudControl();
            this.SuspendLayout();
            // 
            // tagCloudControl
            // 
            this.tagCloudControl.AllowDrop = true;
            this.tagCloudControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagCloudControl.ControlBackColor = System.Drawing.Color.White;
            this.tagCloudControl.ControlHeight = 183;
            this.tagCloudControl.ControlTextFrame = false;
            this.tagCloudControl.ControlTextUnderline = false;
            this.tagCloudControl.ControlWidth = 298;
            this.tagCloudControl.Location = new System.Drawing.Point(0, -2);
            this.tagCloudControl.Name = "tagCloudControl";
            this.tagCloudControl.Size = new System.Drawing.Size(287, 234);
            this.tagCloudControl.TabIndex = 119;
            // 
            // TagCloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 231);
            this.Controls.Add(this.tagCloudControl);
            this.Name = "TagCloud";
            this.Text = "TagCloud";
            this.Load += new System.EventHandler(this.TagCloud_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public yixiaozi.WinForm.Control.TagCloud.TagCloudControl tagCloudControl;
    }
}
namespace DocearReminder
{
    partial class TagListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tagPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.tagPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tagPanel
            // 
            this.tagPanel.AutoScroll = true;
            this.tagPanel.BackColor = System.Drawing.Color.White;
            this.tagPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagPanel.Controls.Add(this.txtTag);
            this.tagPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagPanel.Location = new System.Drawing.Point(0, 0);
            this.tagPanel.Name = "tagPanel";
            this.tagPanel.Size = new System.Drawing.Size(277, 78);
            this.tagPanel.TabIndex = 1;
            // 
            // txtTag
            // 
            this.txtTag.Location = new System.Drawing.Point(3, 3);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(141, 21);
            this.txtTag.TabIndex = 0;
            this.txtTag.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTag_KeyUp);
            // 
            // TagListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagPanel);
            this.Name = "TagListControl";
            this.Size = new System.Drawing.Size(277, 78);
            this.tagPanel.ResumeLayout(false);
            this.tagPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public TagLabelControl tagLabelControl1;
        public TagLabelControl tagLabelControl2;
        public TagLabelControl tagLabelControl3;
        public TagLabelControl tagLabelControl4;
        public System.Windows.Forms.TextBox txtTag;
        public System.Windows.Forms.FlowLayoutPanel tagPanel;
    }
}

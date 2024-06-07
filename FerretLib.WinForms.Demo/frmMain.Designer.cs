namespace FerretLib.WinForms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnShowColorPicker = new System.Windows.Forms.Button();
            this.btnShowRageMessageBox = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tagListControl1 = new FerretLib.WinForms.Controls.TagListControl();
            this.SuspendLayout();
            // 
            // btnShowColorPicker
            // 
            this.btnShowColorPicker.Location = new System.Drawing.Point(12, 11);
            this.btnShowColorPicker.Name = "btnShowColorPicker";
            this.btnShowColorPicker.Size = new System.Drawing.Size(215, 72);
            this.btnShowColorPicker.TabIndex = 0;
            this.btnShowColorPicker.Text = "Color Picker demo";
            this.btnShowColorPicker.UseVisualStyleBackColor = true;
            this.btnShowColorPicker.Click += new System.EventHandler(this.btnShowColorPicker_Click);
            // 
            // btnShowRageMessageBox
            // 
            this.btnShowRageMessageBox.Location = new System.Drawing.Point(12, 89);
            this.btnShowRageMessageBox.Name = "btnShowRageMessageBox";
            this.btnShowRageMessageBox.Size = new System.Drawing.Size(215, 72);
            this.btnShowRageMessageBox.TabIndex = 1;
            this.btnShowRageMessageBox.Text = "RageMessageBox demo";
            this.btnShowRageMessageBox.UseVisualStyleBackColor = true;
            this.btnShowRageMessageBox.Click += new System.EventHandler(this.btnShowRageMessageBox_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 72);
            this.button1.TabIndex = 2;
            this.button1.Text = "TagListControl demo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tagListControl1
            // 
            this.tagListControl1.Location = new System.Drawing.Point(271, 11);
            this.tagListControl1.Name = "tagListControl1";
            this.tagListControl1.Size = new System.Drawing.Size(552, 284);
            this.tagListControl1.TabIndex = 3;
            this.tagListControl1.Tags = ((System.Collections.Generic.List<string>)(resources.GetObject("tagListControl1.Tags")));
            this.tagListControl1.TagChanged += new System.EventHandler(this.tagListControl1_TagChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 372);
            this.Controls.Add(this.tagListControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnShowRageMessageBox);
            this.Controls.Add(this.btnShowColorPicker);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowColorPicker;
        private System.Windows.Forms.Button btnShowRageMessageBox;
        private System.Windows.Forms.Button button1;
        private Controls.TagListControl tagListControl1;
    }
}
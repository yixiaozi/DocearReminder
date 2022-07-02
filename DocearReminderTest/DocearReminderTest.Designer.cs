
namespace DocearReminderTest
{
    partial class DocearReminderTest
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.b_FolderStructure = new System.Windows.Forms.Button();
            this.emailreceive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b_FolderStructure
            // 
            this.b_FolderStructure.Location = new System.Drawing.Point(29, 35);
            this.b_FolderStructure.Name = "b_FolderStructure";
            this.b_FolderStructure.Size = new System.Drawing.Size(100, 47);
            this.b_FolderStructure.TabIndex = 0;
            this.b_FolderStructure.Text = "遍历文件夹";
            this.b_FolderStructure.UseVisualStyleBackColor = true;
            this.b_FolderStructure.Click += new System.EventHandler(this.b_FolderStructure_Click);
            // 
            // emailreceive
            // 
            this.emailreceive.Location = new System.Drawing.Point(184, 35);
            this.emailreceive.Name = "emailreceive";
            this.emailreceive.Size = new System.Drawing.Size(95, 47);
            this.emailreceive.TabIndex = 1;
            this.emailreceive.Text = "接收邮件";
            this.emailreceive.UseVisualStyleBackColor = true;
            this.emailreceive.Click += new System.EventHandler(this.emailreceive_Click);
            // 
            // DocearReminderTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.emailreceive);
            this.Controls.Add(this.b_FolderStructure);
            this.Name = "DocearReminderTest";
            this.Text = "DocearReminderTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_FolderStructure;
        private System.Windows.Forms.Button emailreceive;
    }
}


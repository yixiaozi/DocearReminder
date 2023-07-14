namespace DocearReminder
{
	partial class AutoUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUpdate));
            this.label1 = new System.Windows.Forms.Label();
            this.cmdArgs = new System.Windows.Forms.Label();
            this.vers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "已经是最新版本！";
            // 
            // cmdArgs
            // 
            this.cmdArgs.AutoSize = true;
            this.cmdArgs.Location = new System.Drawing.Point(1, 48);
            this.cmdArgs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cmdArgs.Name = "cmdArgs";
            this.cmdArgs.Size = new System.Drawing.Size(41, 12);
            this.cmdArgs.TabIndex = 1;
            this.cmdArgs.Text = "label2";
            // 
            // vers
            // 
            this.vers.AutoSize = true;
            this.vers.Location = new System.Drawing.Point(1, 33);
            this.vers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.vers.Name = "vers";
            this.vers.Size = new System.Drawing.Size(41, 12);
            this.vers.TabIndex = 2;
            this.vers.Text = "label2";
            // 
            // AutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 68);
            this.Controls.Add(this.vers);
            this.Controls.Add(this.cmdArgs);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AutoUpdate";
            this.Text = "Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label cmdArgs;
		private System.Windows.Forms.Label vers;
	}
}


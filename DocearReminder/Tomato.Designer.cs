namespace DocearReminder
{
    partial class Tomato
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tomato));
            this.fanqietime = new System.Windows.Forms.Label();
            this.timerDefault = new System.Windows.Forms.Timer(this.components);
            this.positionTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // fanqietime
            // 
            this.fanqietime.AutoSize = true;
            this.fanqietime.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fanqietime.ForeColor = System.Drawing.Color.Orange;
            this.fanqietime.Location = new System.Drawing.Point(35, 7);
            this.fanqietime.Name = "fanqietime";
            this.fanqietime.Size = new System.Drawing.Size(108, 46);
            this.fanqietime.TabIndex = 0;
            this.fanqietime.Text = "Time";
            this.fanqietime.Click += new System.EventHandler(this.fanqietime_DoubleClick);
            this.fanqietime.Paint += new System.Windows.Forms.PaintEventHandler(this.fanqietime_Paint);
            this.fanqietime.DoubleClick += new System.EventHandler(this.fanqietime_DoubleClick);
            // 
            // timerDefault
            // 
            this.timerDefault.Tick += new System.EventHandler(this.timerDefault_Tick);
            // 
            // positionTimer
            // 
            this.positionTimer.Tick += new System.EventHandler(this.positionTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(-4, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 84);
            this.panel1.TabIndex = 2;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // Tomato
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(234, 76);
            this.Controls.Add(this.fanqietime);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tomato";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "番茄钟";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fanqie_FormClosing);
            this.Load += new System.EventHandler(this.fanqie_Load);
            this.DoubleClick += new System.EventHandler(this.fanqie_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fanqie_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label fanqietime;
        private System.Windows.Forms.Timer timerDefault;
        private System.Windows.Forms.Timer positionTimer;
        private System.Windows.Forms.Panel panel1;
    }
}
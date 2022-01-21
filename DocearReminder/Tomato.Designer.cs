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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.暂停ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.置顶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
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
            this.fanqietime.Paint += new System.Windows.Forms.PaintEventHandler(this.fanqietime_Paint);
            this.fanqietime.DoubleClick += new System.EventHandler(this.fanqietime_DoubleClick);
            this.fanqietime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fanqietime_MouseUp);
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
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.暂停ToolStripMenuItem,
            this.置顶ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 暂停ToolStripMenuItem
            // 
            this.暂停ToolStripMenuItem.Name = "暂停ToolStripMenuItem";
            this.暂停ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.暂停ToolStripMenuItem.Text = "暂停";
            this.暂停ToolStripMenuItem.Click += new System.EventHandler(this.暂停ToolStripMenuItem_Click);
            // 
            // 置顶ToolStripMenuItem
            // 
            this.置顶ToolStripMenuItem.Name = "置顶ToolStripMenuItem";
            this.置顶ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.置顶ToolStripMenuItem.Text = "置顶";
            this.置顶ToolStripMenuItem.Click += new System.EventHandler(this.置顶ToolStripMenuItem_Click);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
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
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Tomato_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tomato_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Tomato_PreviewKeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label fanqietime;
        private System.Windows.Forms.Timer timerDefault;
        private System.Windows.Forms.Timer positionTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 暂停ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 置顶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
    }
}
using System;
using System.Windows.Forms;
using yixiaozi.WinForm.Control;
using yixiaozi.WinForm.Control.Calendar;

namespace DocearReminder
{
    partial class CalendarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            yixiaozi.WinForm.Control.Calendar.DrawTool drawTool1 = new yixiaozi.WinForm.Control.Calendar.DrawTool();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarForm));
            this.dayView1 = new yixiaozi.WinForm.Control.Calendar.DayView();
            this.Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.完成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开导图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.番茄钟ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解锁ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.freshCalender = new System.Windows.Forms.Timer(this.components);
            this.showhide = new System.Windows.Forms.Timer(this.components);
            this.Menu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dayView1
            // 
            drawTool1.DayView = this.dayView1;
            this.dayView1.ActiveTool = drawTool1;
            this.dayView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dayView1.BackColor = System.Drawing.Color.White;
            this.dayView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dayView1.ContextMenuStrip = this.Menu;
            this.dayView1.DaysToShow = 7;
            this.dayView1.Font = new System.Drawing.Font("楷体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dayView1.ForeColor = System.Drawing.Color.White;
            this.dayView1.HoverAppointment = null;
            this.dayView1.IsShowHover = true;
            this.dayView1.Location = new System.Drawing.Point(0, 0);
            this.dayView1.Margin = new System.Windows.Forms.Padding(0);
            this.dayView1.Name = "dayView1";
            this.dayView1.SelectionEnd = new System.DateTime(2021, 12, 31, 0, 0, 0, 0);
            this.dayView1.SelectionStart = new System.DateTime(2021, 7, 16, 0, 0, 0, 0);
            this.dayView1.Size = new System.Drawing.Size(1918, 1076);
            this.dayView1.StartDate = new System.DateTime(2021, 12, 27, 0, 0, 0, 0);
            this.dayView1.StartHour = 0;
            this.dayView1.TabIndex = 0;
            this.dayView1.Text = " ";
            this.dayView1.WorkingHourEnd = 23;
            this.dayView1.WorkingHourStart = 4;
            this.dayView1.WorkingMinuteEnd = 0;
            this.dayView1.WorkingMinuteStart = 0;
            this.dayView1.SelectionChanged += new System.EventHandler(this.dayView1_SelectionChanged_1);
            this.dayView1.ResolveAppointments += new yixiaozi.WinForm.Control.Calendar.ResolveAppointmentsEventHandler(this.dayView1_ResolveAppointments);
            this.dayView1.AppointmentMouseHover += new yixiaozi.WinForm.Control.DayView.AppointmentMouseHoverEventHandler(this.dayView1_AppointmentMouseHover);
            this.dayView1.AppointmentMouseLeave += new yixiaozi.WinForm.Control.DayView.AppointmentMouseLeaveEventHandler(this.dayView1_AppointmentMouseLeave);
            this.dayView1.AppointmentMouseMove += new yixiaozi.WinForm.Control.DayView.AppointmentMouseMoveEventHandler(this.dayView1_AppointmentMouseMove);
            this.dayView1.AppoinmentMove += new System.EventHandler<yixiaozi.WinForm.Control.Calendar.AppointmentEventArgs>(this.dayView1_AppoinmentMove);
            this.dayView1.DoubleClick += new System.EventHandler(this.dayView1_DoubleClick);
            this.dayView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dayView1_KeyDown);
            this.dayView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CalendarForm_KeyUp);
            this.dayView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dayView1_MouseUp);
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.完成ToolStripMenuItem,
            this.commentToolStripMenuItem,
            this.打开导图ToolStripMenuItem,
            this.打开设置ToolStripMenuItem,
            this.番茄钟ToolStripMenuItem,
            this.解锁ToolStripMenuItem});
            this.Menu.Name = "contextMenuStrip1";
            this.Menu.Size = new System.Drawing.Size(133, 136);
            this.Menu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.Menu_Closed);
            this.Menu.Opening += new System.ComponentModel.CancelEventHandler(this.Menu_Opening);
            this.Menu.Opened += new System.EventHandler(this.Menu_Opened);
            // 
            // 完成ToolStripMenuItem
            // 
            this.完成ToolStripMenuItem.Name = "完成ToolStripMenuItem";
            this.完成ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.完成ToolStripMenuItem.Text = "完成";
            this.完成ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // commentToolStripMenuItem
            // 
            this.commentToolStripMenuItem.Name = "commentToolStripMenuItem";
            this.commentToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.commentToolStripMenuItem.Text = "Comment";
            this.commentToolStripMenuItem.Click += new System.EventHandler(this.commentToolStripMenuItem_Click);
            // 
            // 打开导图ToolStripMenuItem
            // 
            this.打开导图ToolStripMenuItem.Name = "打开导图ToolStripMenuItem";
            this.打开导图ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.打开导图ToolStripMenuItem.Text = "打开导图";
            this.打开导图ToolStripMenuItem.Click += new System.EventHandler(this.打开导图ToolStripMenuItem_Click);
            // 
            // 打开设置ToolStripMenuItem
            // 
            this.打开设置ToolStripMenuItem.Name = "打开设置ToolStripMenuItem";
            this.打开设置ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.打开设置ToolStripMenuItem.Text = "打开设置";
            this.打开设置ToolStripMenuItem.Click += new System.EventHandler(this.打开设置ToolStripMenuItem_Click);
            // 
            // 番茄钟ToolStripMenuItem
            // 
            this.番茄钟ToolStripMenuItem.Name = "番茄钟ToolStripMenuItem";
            this.番茄钟ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.番茄钟ToolStripMenuItem.Text = "番茄钟";
            this.番茄钟ToolStripMenuItem.Click += new System.EventHandler(this.番茄钟ToolStripMenuItem_Click);
            // 
            // 解锁ToolStripMenuItem
            // 
            this.解锁ToolStripMenuItem.Name = "解锁ToolStripMenuItem";
            this.解锁ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.解锁ToolStripMenuItem.Text = "解锁";
            this.解锁ToolStripMenuItem.Click += new System.EventHandler(this.解锁ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dayView1);
            this.panel1.Location = new System.Drawing.Point(0, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1904, 1076);
            this.panel1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // toolTip2
            // 
            this.toolTip2.ShowAlways = true;
            this.toolTip2.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip2_Popup);
            // 
            // freshCalender
            // 
            this.freshCalender.Enabled = true;
            this.freshCalender.Tick += new System.EventHandler(this.freshCalender_Tick);
            // 
            // showhide
            // 
            this.showhide.Tick += new System.EventHandler(this.showhide_Tick);
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1900, 1037);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CalendarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.UseTime_Activated);
            this.Deactivate += new System.EventHandler(this.UseTime_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalendarForm_FormClosed);
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.SizeChanged += new System.EventHandler(this.MainPage_SizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CalendarForm_KeyUp);
            this.Menu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

       

        #endregion

        private System.Windows.Forms.Panel panel1;
        public DayView dayView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem 完成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开导图ToolStripMenuItem;
        private ToolTip toolTip2;
        private ToolStripMenuItem commentToolStripMenuItem;
        private ToolStripMenuItem 番茄钟ToolStripMenuItem;
        private ToolStripMenuItem 解锁ToolStripMenuItem;
        private Timer freshCalender;
        private Timer showhide;
        private ToolStripMenuItem 打开设置ToolStripMenuItem;
    }
}


using System;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;
using Color = System.Drawing.Color;

namespace DocearReminder
{

    public partial class Tomato : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public DateTime _fiveM;
        string mindmapPath="";
        bool isRun = false;
        bool isnotdefault = false;
        private int _fanqieCount=0;
        string faneiguid;
        public Tomato(DateTime fiveM,string name ,string mindmap, int fanqieCount,bool isADD=false,int taskLevel=0)
        {
            if (fanqieCount>10)
            {
                return;
            }
            isnotdefault = isADD;
            if (fiveM == new DateTime())
            {
                isnotdefault = true;
            }
            _fanqieCount=fanqieCount;
            DocearReminderForm.fanqiePosition[_fanqieCount] = true;
            mindmapPath = mindmap;
            _fiveM = fiveM;
            InitializeComponent();
            timerDefault.Interval = 1000;
            timerDefault.Start();
            this.Text = name;
            this.Location = new System.Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 320, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 120);
            positionTimer.Interval = 5000;
            positionTimer.Start();
            //添加一个番茄Json
            try
            {
                //yixiaozi.Media.Audio.Audio.SpeakText(name);
                Thread th = new Thread(() => yixiaozi.Media.Audio.Audio.SpeakText(name));
                th.Start();
                faneiguid = Guid.NewGuid().ToString();
                addreminderlog(name, faneiguid, mindmap, taskLevel);
            }
            catch (Exception)
            {
            }
        }
        bool ISOver = false;

        private void timerDefault_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isnotdefault)
                {
                    _fiveM = _fiveM.AddSeconds(1);
                    fanqietime.Text = _fiveM.Hour.ToString("00") + ":" + _fiveM.Minute.ToString("00") + ":" + _fiveM.Second.ToString("00");
                    if (_fiveM.Second == 0)
                    {
                        addreminderlog(faneiguid);
                    }
                }
                else
                {
                    if (ISOver)
                    {
                        _fiveM = _fiveM.AddSeconds(1);
                    }
                    else
                    {
                        _fiveM = _fiveM.AddSeconds(-1);
                    }
                    if (_fiveM == new DateTime()&& this.Text.Length==5&&this.Text[2]==':')
                    {
                        DocearReminderForm.fanqiePosition[_fanqieCount] = false;
                        this.Close();
                    }
                    else if (ISOver && _fiveM == new DateTime().AddHours(1))
                    {
                        DocearReminderForm.fanqiePosition[_fanqieCount] = false;
                        this.Close();
                    }
                    else if (_fiveM <= new DateTime())
                    {
                        fanqietime.ForeColor = Color.Red;
                    }
                    if (_fiveM == new DateTime())
                    {
                        ISOver = true;
                    }
                    fanqietime.Text = _fiveM.Hour.ToString("00") + ":" + _fiveM.Minute.ToString("00") + ":" + _fiveM.Second.ToString("00");
                    if ( _fiveM.Second == 0)
                    {
                        addreminderlog(faneiguid);
                    }
                }
            }
            catch (Exception ex)
            {
                 this.Close();
            }
        }

        private void fanqie_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(mindmapPath);
        }

        private void fanqietime_DoubleClick(object sender, EventArgs e)
        {
            isRun = !isRun;
            if (!isRun)
            {
                timerDefault.Stop();
            }
            else
            {
                timerDefault.Start();
            }
        }
        private void fanqie_FormClosing(object sender, FormClosingEventArgs e)
        {
            DocearReminderForm.fanqiePosition[_fanqieCount] = false;
            addreminderlog(faneiguid);
        }

        private void taskname_Click(object sender, EventArgs e)
        {

        }

        private void fanqie_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 250, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 150 - _fanqieCount * 120);
            fanqietime.Text = _fiveM.Hour.ToString("00") + ":" + _fiveM.Minute.ToString("00") + ":" + _fiveM.Second.ToString("00");
           
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void fanqie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Q)
            {
                this.Close();
            }
        }

        private void positionTimer_Tick(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 250, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 150 - _fanqieCount * 120);
            //this.TopMost = true;
        }
        public void addreminderlog(string TaskName,string ID,string mindmap,int taskLevel)
        {
            try
            {
                DocearReminderForm.reminderObject.reminders.Add(new ReminderItem
                {
                    name = TaskName,
                    time = DateTime.Now,
                    mindmapPath = mindmap,
                    ID = ID,
                    mindmap="FanQie",
                    tasklevel= taskLevel
                });
            }
            catch (Exception)
            {
            }
            
        }

        public void addreminderlog(string ID)
        {
            try
            {
                ReminderItem item = DocearReminderForm.reminderObject.reminders.First(m => m.ID == ID);
                if (item != null)
                {
                    item.comleteTime = DateTime.Now;
                    item.tasktime = (DateTime.Now - item.time).TotalMinutes;
                }
            }
            catch (Exception)
            {
            }
        }


        private void panel1_Click(object sender, EventArgs e)
        {
            //if (this.TopMost)
            //{
            //    this.TopMost = false;
            //    positionTimer.Stop();
            //}
            //else
            //{
            //    positionTimer.Start();
            //}
        }

        private void W(object sender, PaintEventArgs e)
        {

        }

        private void fanqietime_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }

        private void 暂停ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (暂停ToolStripMenuItem.Text == "暂停")
            {
                timerDefault.Stop();
            }
            else
            {
                timerDefault.Start();
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.TopMost)
            {
                置顶ToolStripMenuItem.Text = "取消置顶";
            }
            else
            {
                置顶ToolStripMenuItem.Text = "置顶";
            }
            if (timerDefault.Enabled)
            {
                暂停ToolStripMenuItem.Text = "暂停";
            }
            else
            {
                暂停ToolStripMenuItem.Text = "开始";
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, new System.Drawing.Point(e.X, e.Y));
            }
        }

        private void fanqietime_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, new System.Drawing.Point(e.X, e.Y));
            }
        }

        private void Tomato_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void Tomato_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void Tomato_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, new System.Drawing.Point(e.X, e.Y));
            }
        }
    }
}

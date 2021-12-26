using System;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;

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
        public Tomato(DateTime fiveM,string name ,string mindmap, int fanqieCount,bool isADD=false)
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
                addreminderlog(name, faneiguid, mindmap);
            }
            catch (Exception ex)
            {
            }
        }

        private void timerDefault_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isnotdefault)
                {
                    _fiveM = _fiveM.AddSeconds(1);
                    fanqietime.Text = _fiveM.Hour.ToString("00") + ":" + _fiveM.Minute.ToString("00") + ":" + _fiveM.Second.ToString("00");
                }
                else
                {
                    _fiveM = _fiveM.AddSeconds(-1);
                    if (_fiveM == new DateTime())
                    {
                        DocearReminderForm.fanqiePosition[_fanqieCount] = false;
                        this.Close();
                    }
                    fanqietime.Text = _fiveM.Hour.ToString("00") + ":" + _fiveM.Minute.ToString("00") + ":" + _fiveM.Second.ToString("00");
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
            this.TopMost = true;
        }
        public void addreminderlog(string TaskName,string ID,string mindmap)
        {
            try
            {
                Reminder reminderObject = new Reminder();
                FileInfo fi = new FileInfo(@"fanqie.json");
                if (!System.IO.File.Exists(@"fanqie.json"))
                {
                    File.WriteAllText(@"fanqie.json", "");
                }
                using (StreamReader sw = fi.OpenText())
                {
                    string s = sw.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    reminderObject = serializer.Deserialize<Reminder>(s);
                    reminderObject.reminders.Add(new ReminderItem
                    {
                        name = TaskName,
                        time = DateTime.Now,
                        mindmapPath = mindmap,
                        ID = ID
                    });
                }
                string json = new JavaScriptSerializer().Serialize(reminderObject);
                File.WriteAllText(@"fanqie.json", "");
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.Write(json);
                }
            }
            catch (Exception ex)
            {
            }
            
        }

        public void addreminderlog(string ID)
        {
            try
            {
                Reminder reminderObject = new Reminder();
                FileInfo fi = new FileInfo(@"fanqie.json");
                if (!System.IO.File.Exists(@"fanqie.json"))
                {
                    File.WriteAllText(@"fanqie.json", "");
                }
                using (StreamReader sw = fi.OpenText())
                {
                    string s = sw.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    reminderObject = serializer.Deserialize<Reminder>(s);
                    ReminderItem item = reminderObject.reminders.First(m => m.ID == ID);
                    if (item != null)
                    {
                        item.comleteTime = DateTime.Now;
                    }
                }
                string json = new JavaScriptSerializer().Serialize(reminderObject);
                File.WriteAllText(@"fanqie.json", "");
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.Write(json);
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void panel1_Click(object sender, EventArgs e)
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                positionTimer.Stop();
            }
            else
            {
                positionTimer.Start();
            }
        }

        private void W(object sender, PaintEventArgs e)
        {

        }

        private void fanqietime_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;
using yixiaozi.WinForm.Common;

namespace DocearReminder
{
    public partial class KeyHours : Form
    {
        public KeyHours()
        {
            InitializeComponent();
            Center();
            ShowChart();
        }

        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ShowChart();
        }
        public void ShowChart()
        {
            var plt = formsPlot1.Plot;
            plt.Clear();
            formsPlot1.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot1.Configuration.ScrollWheelZoom = false;
            formsPlot1.Configuration.LeftClickDragPan = false;
            #region 构造数据
            List<keyRecord> result = new List<keyRecord>();
            foreach (string item in System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "key.txt", SearchOption.AllDirectories))
            {
                System.IO.File.Delete(item.Replace(".txt", "1.txt"));
                System.IO.File.Copy(item, item.Replace(".txt", "1.txt"));
                FileInfo file = new FileInfo(item.Replace(".txt", "1.txt"));
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(item.Replace(".txt", "1.txt")))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        String line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            DateTime dt = DateTime.Now.AddDays(1100);
                            string key = "";
                            try
                            {
                                dt = Convert.ToDateTime(line.Substring(0, 17));
                                key = line.Substring(18);
                            }
                            catch (Exception ex)
                            {
                                try
                                {
                                    dt = Convert.ToDateTime(line.Substring(0, 16));
                                    key = line.Substring(17);
                                }
                                catch (Exception)
                                {
                                }
                            }
                            result.Add(new keyRecord
                            {
                                time = dt,
                                Keys = key
                            }); ;
                        }
                    }
                }
                System.IO.File.Delete(item.Replace(".txt", "1.txt"));
            }
            List<double> names = new List<double>();
            List<double> values = new List<double>();
            foreach (keyRecord item in result.OrderBy(m => m.time))
            {
                if (item.time>=dateTimePicker1.Value&&item.time<=dateTimePicker2.Value)
                {
                    names.Add(item.time.ToOADate());
                    values.Add(item.Keys.Split(';').Length);
                }
            }
            #endregion
            var bar = plt.AddBar(values.ToArray(), names.ToArray());
            plt.XAxis.TickLabelFormat("yy-MM-dd HH", dateTimeFormat: true);
            bar.BarWidth = (1.0 / 24) * .8;
            plt.SetAxisLimits(yMin: 0);
            formsPlot1.Refresh();
            string keyorder = "A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;Oemcomma;OemPeriod;Oem1;Oem7;OemOpenBrackets;Oem6;Oem5;LControlKey;LShiftKey;Space;Return;Tab;Capital;LWin;Down;Up;Left;Right;Escape;Back;Delete;PageUp;PageDown;Insert;Home;End;D1;D2;D3;D4;D5;D6;D7;D8;D9;D0;OemMinus;Oemplus";
            int number = keyorder.Split(';').Length;
            List<string> keyorderArr = keyorder.Split(';').ToList();
            int[] keynumbers = new int[3000];
            foreach (keyRecord item in result)
            {
                if (item.time >= dateTimePicker1.Value && item.time <= dateTimePicker2.Value)
                {
                    foreach (string key in item.Keys.Split(';'))
                    {
                        if (key != "")
                        {
                            if (keyorderArr.Contains(key))
                            {
                            }
                            else
                            {
                                keyorderArr.Add(key);
                            }
                            keynumbers[getindex(keyorderArr, key)] += 1;
                        }
                    }
                }
            }
            double[] keynum = new double[number];//keyorderArr.Count];
            double[] position = new double[number];
            string[] labels = new string[number];
            for (int i = 0; i < number; i++)
            {
                keynum[i] = keynumbers[i];
                position[i] = i;
                switch (keyorderArr[i])
                {
                    case "LControlKey":
                        labels[i] = "Ctl";
                        break;
                    case "LShiftKey":
                        labels[i] = "SFT";
                        break;
                    case "LWin":
                        labels[i] = "Win";
                        break;
                    case "Escape":
                        labels[i] = "Esc";
                        break;
                    case "Back":
                        labels[i] = "Back";
                        break;
                    case "PageUp":
                        labels[i] = "PUp";
                        break;
                    case "PageDown":
                        labels[i] = "PDn";
                        break;
                    case "Oemcomma":
                        labels[i] = ",";
                        break;
                    case "OemPeriod":
                        labels[i] = ".";
                        break;
                    case "Oem1":
                        labels[i] = ";";
                        break;
                    case "Oem7":
                        labels[i] = "\"";
                        break;
                    case "OemOpenBrackets":
                        labels[i] = "[";
                        break;
                    case "Oem6":
                        labels[i] = "]";
                        break;
                    case "Oem5":
                        labels[i] = "\\";
                        break;
                    case "D1":
                        labels[i] = "1";
                        break;
                    case "D2":
                        labels[i] = "2";
                        break;
                    case "D3":
                        labels[i] = "3";
                        break;
                    case "D4":
                        labels[i] = "4";
                        break;
                    case "D5":
                        labels[i] = "5";
                        break;
                    case "D6":
                        labels[i] = "6";
                        break;
                    case "D7":
                        labels[i] = "7";
                        break;
                    case "D8":
                        labels[i] = "8";
                        break;
                    case "D9":
                        labels[i] = "9";
                        break;
                    case "D0":
                        labels[i] = "0";
                        break;
                    case "OemMinus":
                        labels[i] = "-";
                        break;
                    case "Oemplus":
                        labels[i] = "=";
                        break; 
                    case "Insert":
                        labels[i] = "Ins";
                        break;
                    case "Delete":
                        labels[i] = "Del";
                        break;
                    case "Return":
                        labels[i] = "Rtn";
                        break;
                    case "Capital":
                        labels[i] = "Cap";
                        break;
                    default:
                        labels[i] = keyorderArr[i];
                        break;
                }
            }
            var plt1 = formsPlot2.Plot;
            plt1.Clear();
            formsPlot2.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot2.Configuration.ScrollWheelZoom = false;
            formsPlot2.Configuration.LeftClickDragPan = false;
            plt1.AddBar(keynum, position);
            plt1.XTicks(position, labels);
            plt1.SetAxisLimits(yMin: 0);
            plt1.Legend();
            formsPlot2.Refresh();
        }
        public static int getindex(List<string> arr, string str)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] == str)
                {
                    return i;
                }
            }
            return 0;
        }
        yixiaozi.WinForm.Common.AutoSizeForm asc = new AutoSizeForm();
        private void KeyHours_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            this.WindowState = FormWindowState.Maximized;
            asc.controlAutoSize(this);
        }

        private void KeyHours_Resize(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
                dateTimePicker1.Value = DateTime.Today;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = checkBox3.Checked = checkBox4.Checked = false;
                dateTimePicker1.Value = DateTime.Today.AddDays(0- DateTime.Today.DayOfWeek);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox1.Checked = checkBox2.Checked = checkBox4.Checked = false;
                dateTimePicker1.Value = DateTime.Today.AddDays(0 - DateTime.Today.Day);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = false;
                dateTimePicker1.Value = Convert.ToDateTime("2022/01/01");
            }
        }
        #region 添加使用记录

        Guid currentUsedTimerId;
        string usedTimeLog = "";
        private void UseTime_Activated(object sender, EventArgs e)
        {
            UsedLogRenew();
        }

        private void UseTime_Deactivate(object sender, EventArgs e)
        {
            UsedLogRenew(false);
        }
        public void UsedLogRenew(bool newlog = true, bool newid = true)
        {
            //结束之前的ID记录，并生成新的ID
            if (newid || currentUsedTimerId == null)
            {
                DocearReminderForm.usedTimer.SetEndDate(currentUsedTimerId);
                Thread th = new Thread(() => DocearReminderForm.SaveUsedTimerFile(DocearReminderForm.usedTimer)){IsBackground = true};
                currentUsedTimerId = Guid.NewGuid();
            }
            //添加一个新的记录
            if (newlog)
            {
                //主窗口
                //时间块
                //历史记录
                //剪切板
                //工具窗口
                //报表 - 时间块
                //报表 - 键盘
                //报表 - 使用记录
                //所有
                DocearReminderForm.usedTimer.NewOneTime(currentUsedTimerId, "", "", "", "报表-键盘");
            }
        }
        private void SaveUsedTimerFile(UsedTimer data)
        {
            string json = new JavaScriptSerializer()
            {
                MaxJsonLength = Int32.MaxValue
            }.Serialize(data);
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json", "");
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json");
            using (StreamWriter sw = fi.AppendText())
            {
                sw.Write(json);
            }
        }
        #endregion
    }
    public class keyRecord
    {
        public DateTime time { get; set; }
        public string Keys { get; set; }
        public List<string> keyList
        {
            get
            {
                List<string> list = new List<string>();
                list.AddRange(Keys.Split());
                return list;
            }
        }
    }
}

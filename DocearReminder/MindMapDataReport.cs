using ScottPlot;
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
using System.Xml;
using yixiaozi.Model.DocearReminder;
using yixiaozi.WinForm.Common;
using static DocearReminder.DocearReminderForm;

namespace DocearReminder
{
    public partial class MindMapDataReport : Form
    {
        public MindMapDataReport()
        {
            InitializeComponent();

            Center();
        }

        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
        }


        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeBlockReport_Load(object sender, EventArgs e)
        {
            begin.Value = DateTime.Today.AddDays(-30);
            end.Value = DateTime.Today.AddDays(1);
            LoadChart();
        }

        public void LoadChart()
        {
            #region 时间
            formsPlot1.Plot.Clear();
            formsPlot1.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot1.Configuration.ScrollWheelZoom = false;
            formsPlot1.Configuration.LeftClickDragPan = false;
            var plt = formsPlot1.Plot;
            List<double> valueList = new List<double>();
            List<double> daysList = new List<double>();
            List<int> countList = new List<int>();
            richTextBox1.Text = "";
            string content = "";


            foreach (IGrouping<double, node> item in DocearReminderForm.nodes.Where(m => MyContains(m.mindmapPath, textBox_mindmappath.Text) && m.Time >= begin.Value && m.Time <= end.Value && MyContains(m.mindmapName, textBox_mindmappath.Text) && MyContains(m.ParentNodePath, fathernodename.Text) && MyContains(m.Text, nodename.Text) && (nodenameexc.Text != "" ? !MyContains(m.Text, nodenameexc.Text) : true)).OrderBy(m => m.Time).GroupBy(m => Convert.ToDateTime(m.Time.ToString("yyyy-MM-dd")).ToOADate()))
            {
                double minute = 0;
                foreach (node ritem in item)
                {
                    if (content.Length < numericUpDown2.Value)
                    {
                        content += (ritem.Time.ToString("yy-MM-dd HH:mm:ss FF 节点：") + ritem.Text + " 导图：" + ritem.mindmapName);
                        content += Environment.NewLine;
                    }
                    minute++;
                }
                if (minute > Convert.ToDouble(numericUpDown1.Value))
                {
                    minute = Convert.ToDouble(numericUpDown1.Value);
                }
                daysList.Add(item.Key);
                valueList.Add(minute);
            }
            richTextBox1.Text = content;
            plt.AddBar(valueList.ToArray(), daysList.ToArray());
            plt.XAxis.TickLabelFormat("yy-MM-dd", dateTimeFormat: true);
            plt.Legend();
            formsPlot1.Refresh();
            #endregion

            #region 时间
            formsPlot3.Plot.Clear();
            formsPlot3.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot3.Configuration.ScrollWheelZoom = false;
            formsPlot3.Configuration.LeftClickDragPan = false;
            var plt3 = formsPlot3.Plot;
            List<double> valueList3 = new List<double>();
            List<string> daysList3 = new List<string>();


            foreach (IGrouping<int, node> item in DocearReminderForm.nodes.Where(m => MyContains(m.mindmapPath, textBox_mindmappath.Text) && m.Time >= begin.Value && m.Time <= end.Value && MyContains(m.mindmapName, textBox_mindmappath.Text) && MyContains(m.ParentNodePath, fathernodename.Text) && MyContains(m.Text, nodename.Text) && (nodenameexc.Text != "" ? !MyContains(m.Text, nodenameexc.Text) : true)).OrderBy(m => m.Time.Hour).GroupBy(m => m.Time.Hour))
            {
                daysList3.Add(item.Key.ToString());
                valueList3.Add(item.Count());
            }
            plt3.AddBar(valueList3.ToArray());
            plt3.XTicks(daysList3.ToArray());
            plt3.Legend();
            formsPlot3.Refresh();
            #endregion

            #region 时间
            formsPlot4.Plot.Clear();
            formsPlot4.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot4.Configuration.ScrollWheelZoom = false;
            formsPlot4.Configuration.LeftClickDragPan = false;
            var plt4 = formsPlot4.Plot;
            List<double> valueList4 = new List<double>();
            List<string> daysList4 = new List<string>();

            foreach (IGrouping<string, node> item in DocearReminderForm.nodes.Where(m => MyContains(m.mindmapPath, textBox_mindmappath.Text) && m.Time >= begin.Value && m.Time <= end.Value && MyContains(m.mindmapName, textBox_mindmappath.Text) && MyContains(m.ParentNodePath, fathernodename.Text) && MyContains(m.Text, nodename.Text) && (nodenameexc.Text != "" ? !MyContains(m.Text, nodenameexc.Text) : true)).OrderBy(m => m.Time).GroupBy(m => m.Time.ToString("ddd")))
            {
                daysList4.Add(item.Key);
                valueList4.Add(item.Count());
            }
            plt4.AddBar(valueList4.ToArray());
            plt4.XTicks(daysList4.ToArray());
            plt4.Legend();
            formsPlot4.Refresh();
            #endregion


            #region 导图
            formsPlot2.Plot.Clear();
            formsPlot2.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot2.Configuration.ScrollWheelZoom = false;
            formsPlot2.Configuration.LeftClickDragPan = false;
            var plt2 = formsPlot2.Plot;
            ReportDataCol reportData = new ReportDataCol();

            foreach (IGrouping<string, node> item in DocearReminderForm.nodes.Where(m => MyContains(m.mindmapPath, textBox_mindmappath.Text) && m.Time >= begin.Value && m.Time <= end.Value && MyContains(m.mindmapName, textBox_mindmappath.Text) && MyContains(m.ParentNodePath, fathernodename.Text) && MyContains(m.Text, nodename.Text) && (nodenameexc.Text != "" ? !MyContains(m.Text, nodenameexc.Text) : true)).OrderBy(m => m.Time).GroupBy(m => m.mindmapName))
            {
                reportData.items.Add(new ReportDataItem
                {
                    name = item.Key,
                    value = item.Count()
                });
            }
            if (reportData.values.Length == 0)
            {
                return;
            }
            var pie2 = plt2.AddPie(reportData.values);
            pie2.SliceLabels = reportData.names;
            //pie2.ShowPercentages = true;
            //pie2.ShowValues = true;
            pie2.ShowLabels = true;
            //plt2.Legend();
            formsPlot2.Refresh();
            #endregion
        }
        public class ReportDataCol
        {
            public ReportDataCol()
            {
                items = new List<ReportDataItem>();
            }
            public List<ReportDataItem> items { get; set; }
            public string[] names
            {
                get
                {
                    string[] _names = new string[items.Count];
                    for (int i = 0; i < items.Count; i++)
                    {
                        _names[i] = items[i].name;

                    }
                    return _names;
                }
            }
            public double[] values
            {
                get
                {
                    double[] _values = new double[items.Count];
                    for (int i = 0; i < items.Count; i++)
                    {
                        _values[i] = items[i].value;

                    }
                    return _values;
                }
            }
            public Color[] colors
            {
                get
                {
                    Color[] _values = new Color[items.Count];
                    for (int i = 0; i < items.Count; i++)
                    {
                        _values[i] = items[i].color;

                    }
                    return _values;
                }
            }
            public Color[] labelcolors
            {
                get
                {
                    Color[] _values = new Color[items.Count];
                    for (int i = 0; i < items.Count; i++)
                    {
                        _values[i] = Color.Black;

                    }
                    return _values;
                }
            }
        }

        public class ReportDataItem
        {
            public string name { get; set; }
            public string fullName { get; set; }
            public double value { get; set; }
            public Color color { get; set; }
        }


        yixiaozi.WinForm.Common.AutoSizeForm asc = new AutoSizeForm();

        private void TimeBlockReport_Resize(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void searchword_TextChanged(object sender, EventArgs e)
        {
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
                DocearReminderForm.usedTimer.NewOneTime(currentUsedTimerId, "", textBox_mindmappath.Text, "", "报表-思维导图时间报告");
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

        private void exclude_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_searchwork_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    LoadChart();
                    break;
                case Keys.Shift:
                    break;
                case Keys.Control:
                    break;
                case Keys.Alt:
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void begin_ValueChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void end_ValueChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            LoadChart();
        }
    }
}
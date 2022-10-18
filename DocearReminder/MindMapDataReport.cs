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
            foreach (IGrouping<double, node> item in DocearReminderForm.nodes.Where(m=> MyContains(m.mindmapPath,textBox_mindmappath.Text) && m.Time >= begin.Value && m.Time <= end.Value && MyContains(m.mindmapName,textBox_mindmappath.Text)&& MyContains(m.ParentNodePath,fathernodename.Text)&& MyContains(m.Text,nodename.Text)&&(nodenameexc.Text!=""?!MyContains(m.Text,nodenameexc.Text):true)).OrderBy(m=>m.Time).GroupBy(m => Convert.ToDateTime(m.Time.ToString("yyyy-MM-dd")).ToOADate()))
            {
                double minute = 0;
                foreach (node ritem in item)
                {
                    if (content.Length< numericUpDown2.Value)
                    {
                        content += (ritem.Time.ToString("yy-MM-dd hh:mm:ss FF |节点：") + ritem.Text + "|导图：" + ritem.mindmapName);
                        content += Environment.NewLine;
                    }
                    minute++;
                }
                if (minute>Convert.ToDouble(numericUpDown1.Value))
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
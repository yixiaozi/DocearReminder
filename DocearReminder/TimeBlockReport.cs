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
using yixiaozi.Model.DocearReminder;
using yixiaozi.WinForm.Common;
using static DocearReminder.DocearReminderForm;

namespace DocearReminder
{
    public partial class TimeBlockReport : Form
    {
        public TimeBlockReport()
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
            LoadChart();
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

        private void startDt_ValueChanged(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            //this.WindowState = FormWindowState.Maximized;
            asc.controlAutoSize(this);
            LoadChart();
        }

        /// <summary>
        /// https://scottplot.net/cookbook/4.1/
        /// </summary>
        public void LoadChart()
        {
            formsPlot1.Plot.Clear();
            formsPlot1.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot1.Configuration.ScrollWheelZoom = false;
            formsPlot1.Configuration.LeftClickDragPan = false;
            var plt = formsPlot1.Plot;
            ReportDataCol reportData = new ReportDataCol();
            foreach (ReminderItem item in DocearReminderForm.reminderObject.reminders)
            {
                if (item.mindmap == "TimeBlock" && item.time.Date >= startDt.Value && item.time.Date <= endDT.Value)
                {
                    if (textBox_searchwork.Text != ""|| exclude.Text != "")
                    {
                        try//过滤字符串
                        {
                            if (item.comment == null)
                            {
                                item.comment = "";
                            }
                            if (item.DetailComment == null)
                            {
                                item.DetailComment = "";
                            }
                            if (textBox_searchwork.Text != "")
                            {
                                if (!(MyContains(item.name, textBox_searchwork.Text) || MyContains(item.mindmapPath, textBox_searchwork.Text) || MyContains(item.nameFull, textBox_searchwork.Text) || MyContains(item.comment, textBox_searchwork.Text) || MyContains(item.DetailComment, textBox_searchwork.Text)))
                                {
                                    continue;
                                }
                            }
                            if (exclude.Text != "")
                            {
                                if ((MyContains(item.name, exclude.Text)) || (MyContains(item.mindmapPath, exclude.Text)) || (MyContains(item.nameFull, exclude.Text)) || (MyContains(item.comment, exclude.Text)) || (MyContains(item.DetailComment, exclude.Text)))

                                {
                                    continue;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (comboBox1.SelectedIndex > 0)
                    {
                        try//过滤分类
                        {
                            if (!(item.name.Contains(comboBox1.SelectedItem.ToString())) && !(item.mindmapPath.Contains(comboBox1.SelectedItem.ToString())) && !(item.nameFull.Contains(comboBox1.SelectedItem.ToString())) && !(item.comment != null && item.comment != "" && item.comment.Contains(comboBox1.SelectedItem.ToString())))
                            {
                                continue;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (reportData.items.Exists(m => m.name == getFirst(item)))
                    {
                        reportData.items.First(m => m.name == getFirst(item)).value += item.tasktime;
                        //是否应该更新下颜色呢？有必要影响效率吗？
                    }
                    else
                    {
                        reportData.items.Add(new ReportDataItem
                        {
                            name = getFirst(item),
                            value = item.tasktime,
                            color = Color.FromArgb(Int32.Parse(item.mindmapPath))
                        }); ;
                    }
                }
            }
            if (reportData.values.Length==0)
            {
                return;
            }
            var pie = plt.AddPie(reportData.values);
            pie.SliceLabels = reportData.names;
            if (comboBox1.SelectedIndex<=0)
            {
                pie.SliceFillColors = reportData.colors;
                pie.SliceLabelColors = reportData.labelcolors;
            }
            if (percent.Checked)
            {
                pie.ShowPercentages = true;
            }
            if (checkBox1.Checked)
            {
                pie.ShowValues = true;
            }
            if (checkBox2.Checked)
            {
                pie.ShowLabels = true;
            }
            if (checkBox3.Checked)
            {
                plt.Legend();
            }
            formsPlot1.Refresh();
        }

        public string getFirst(ReminderItem item)
        {
            try
            {
                return (((item.nameFull != null && item.nameFull != "") ? item.nameFull + "|" : "") + item.name).Replace(comboBox1.SelectedItem.ToString(), "").Split('|').FirstOrDefault(m => m != "");
            }
            catch (Exception)
            {
                return "异常分类";
            }
        }
        yixiaozi.WinForm.Common.AutoSizeForm asc = new AutoSizeForm();

        private void TimeBlockReport_Resize(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void searchword_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void percent_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
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
                DocearReminderForm.usedTimer.NewOneTime(currentUsedTimerId, comboBox1.SelectedItem.ToString(), textBox_searchwork.Text, "", "报表-时间块");
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

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }
    }
}
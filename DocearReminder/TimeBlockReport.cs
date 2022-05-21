using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;
using yixiaozi.WinForm.Common;

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
            Reminder reminderObject = new Reminder();
            string logfile = System.AppDomain.CurrentDomain.BaseDirectory + @"\reminder.json";
            FileInfo fi = new FileInfo(logfile);
            if (!System.IO.File.Exists(logfile))
            {
                File.WriteAllText(logfile, "");
            }
            ReportDataCol reportData = new ReportDataCol();
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer()
                {
                    MaxJsonLength = Int32.MaxValue
                };
                reminderObject = serializer.Deserialize<Reminder>(s);
                if (reminderObject.reminders == null || reminderObject.reminders.Count == 0)
                {
                    return;
                }
                foreach (ReminderItem item in reminderObject.reminders)
                {
                    if (item.mindmap == "TimeBlock" && item.time.AddHours(8).Date >= startDt.Value && item.time.AddHours(8).Date <= endDT.Value)
                    {
                        if (searchword.Text!="")
                        {
                            try//过滤字符串
                            {
                                if (!(item.name.Contains(searchword.Text)) && !(item.mindmapPath.Contains(searchword.Text)) && !(item.nameFull.Contains(searchword.Text)) && !(item.comment != null && item.comment != "" && item.comment.Contains(searchword.Text)))
                                {
                                    continue;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        if (comboBox1.SelectedIndex>0)
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
                                color=Color.FromArgb(Int32.Parse(item.mindmapPath))
                        }); ;
                        }
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
            plt.Legend();
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
            LoadChart();
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
    }
}
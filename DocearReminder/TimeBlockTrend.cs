using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yixiaozi.Model.DocearReminder;

namespace DocearReminder
{
    public partial class TimeBlockTrend : Form
    {
        public TimeBlockTrend()
        {
            InitializeComponent();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {

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
                    if (searchword.Text != "")
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
            if (reportData.values.Length == 0)
            {
                return;
            }
            var pie = plt.AddPie(reportData.values);
            pie.SliceLabels = reportData.names;
            if (comboBox1.SelectedIndex <= 0)
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
        public class ReportDataItem
        {
            public string name { get; set; }
            public string fullName { get; set; }
            public double value { get; set; }
            public Color color { get; set; }
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
    }
}

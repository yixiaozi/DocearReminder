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
            LoadChart();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
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
            List<ReminderItem> reuslt = new List<ReminderItem>();
            foreach (ReminderItem item in DocearReminderForm.reminderObject.reminders)
            {
                if (item.mindmap == "TimeBlock" && item.time.Date >= startDt.Value && item.time.Date <= endDT.Value)
                {
                    if (searchword.Text != "")
                    {
                        try//过滤字符串
                        {
                            if (!(item.name.Contains(searchword.Text)) && !(item.mindmapPath.Contains(searchword.Text)) && !(item.nameFull.Contains(searchword.Text)) && !(item.comment != null && item.comment != "" &&item.comment.Contains(searchword.Text)))
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
                    reuslt.Add(item);
                    
                }
            }
            if (reuslt.Count==0)
            {
                return;
            }
            List<double> valueList = new List<double>();
            List<double> daysList = new List<double>();
            double all =0;
            int i = 0;
            foreach (IGrouping<double,ReminderItem> item in reuslt.OrderBy(m => m.time).GroupBy(m => Convert.ToDateTime(m.time.ToString("yyyy-MM-dd")).ToOADate()))
            {
                double minute = 0;
                foreach (ReminderItem ritem in item)
                {
                    minute += ritem.tasktime;
                }
                if (!emptyDay.Checked&&minute==0)
                {
                    continue;
                }
                valueList.Add(minute/60);
                daysList.Add(item.Key);
                all += (minute / 60);
                i++;
            }
            
            
            plt.AddBar(valueList.ToArray(),daysList.ToArray());
            label1.Text = "平均每天：" + (all / i).ToString("F") + "小时，总时长："+ (all).ToString("F") + "小时";
            plt.XAxis.TickLabelFormat("M\\/dd", dateTimeFormat: true);
  
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

        private void searchword_TextChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void startDt_ValueChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void endDT_ValueChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void emptyDay_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }
    }
}

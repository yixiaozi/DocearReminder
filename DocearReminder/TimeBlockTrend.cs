﻿using System;
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
            Center();
            LoadChart();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            Center();
            LoadChart();
        }
        public void Center()
        {
            int x = (System.Windows.Forms.SystemInformation.WorkingArea.Width - this.Size.Width) / 2;
            int y = (System.Windows.Forms.SystemInformation.WorkingArea.Height - this.Size.Height) / 2;
            this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            this.Location = (System.Drawing.Point)new Size(x, y);         //窗体的起始位置为(x,y)
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
            int totalDay = ((int)(endDT.Value - startDt.Value).TotalDays)+1;
            List<ReminderItem> reuslt = new List<ReminderItem>();
            richTextBox1.Text = "";
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
            int totalDayhasValue = 0;

            int totalCountValue = 0;
            int remarksCount = 0;
            Int64 remarksWordsCount = 0;
            foreach (IGrouping<double,ReminderItem> item in reuslt.OrderBy(m => m.time).GroupBy(m => Convert.ToDateTime(m.time.ToString("yyyy-MM-dd")).ToOADate()))
            {
                double minute = 0;
                foreach (ReminderItem ritem in item)
                {
                    minute += ritem.tasktime;
                    totalCountValue++;
                    if (ritem.comment!=null&&ritem.comment!="")
                    {
                        remarksCount++;
                        remarksWordsCount += ritem.comment.Length;   
                    }
                    string timeblocktop = "";
                    try
                    {
                        timeblocktop = ritem.nameFull;
                        if (timeblocktop != "")
                        {
                            timeblocktop += "|";
                        }
                    }
                    catch (Exception)
                    {
                    }
                    richTextBox1.Text += (ritem.time.ToString("MM-dd HH:mm") + ">" + ritem.time.AddMinutes(ritem.tasktime).ToShortTimeString() + timeblocktop + ritem.name + (ritem.comment!=""?"(":"") + ritem.comment + (ritem.comment != "" ? ")" : "")+ Environment.NewLine);
                }
                valueList.Add(minute/60);
                daysList.Add(item.Key);
                all += (minute / 60);
                totalDayhasValue++;
            }

            int maxDays = 0;
            int maxcurrent = 0;
            List<int> maxList = new List<int>();
            int maxCount = 0;
            double day = 0;
            foreach (double item in daysList)
            {
                if (day+1 == item)
                {
                    maxcurrent += 1;
                }
                else
                {
                    maxList.Add(maxcurrent);
                    maxCount += maxcurrent;
                    if (maxcurrent>maxDays)
                    {
                        maxDays = maxcurrent;
                    }
                    maxcurrent = 0;
                }
                day = item;
            }
            if (maxDays==0)
            {
                maxDays = maxcurrent;
            }
            if (maxList.Count==0)
            {
                maxList.Add(0);
                maxCount = maxcurrent;
            }
            
            plt.AddBar(valueList.ToArray(),daysList.ToArray());
            
            totalDays.Text= totalDay + "天";
            totalDaysWithContent.Text = totalDayhasValue + "天";
            DaysPercent.Text = ((float)totalDayhasValue / totalDay).ToString("P");

            totalTime.Text = (all).ToString("F") + "小时";//总时间
            totalTimeEventDay.Text = (all / totalDayhasValue).ToString("F") + "小时";//每天平均时长（记录时间）
            totalTimeEventDays.Text = (all / totalDay).ToString("F") + "小时";//每天平均时长（记录时间）

            totalcount1.Text = (totalCountValue).ToString("F") + "次";//总次数
            PerCountEveryDays.Text = ((float)totalCountValue / totalDayhasValue).ToString("F") + "次";//每天平均次数（记录时间）
            PerCountEveryDay.Text = (totalCountValue / totalDay).ToString("F") + "次";//每天平均次数（记录时间）

            remarksCount1.Text=remarksCount + "次";//备注总次数
            RemarkPercent.Text = ((float)remarksCount / totalCountValue).ToString("P");//每天平均次数（记录时间）
            label29.Text = ((float)remarksWordsCount / remarksCount).ToString("F") + "字";//每天平均次数（记录时间）

            MaxDays.Text = maxDays + "天";
            PerMaxDays.Text = ((float)maxDays / maxList.Count).ToString("F") + "天";
            
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

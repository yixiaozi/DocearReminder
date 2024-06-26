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
using static DocearReminder.DocearReminderForm;
namespace DocearReminder
{
    public partial class TimeBlockTrend : Form
    {
        public TimeBlockTrend()
        {
            InitializeComponent();
            startDt.Value = Convert.ToDateTime(DateTime.Today.ToString("yyyy/MM/01"));
            endDT.Value = DateTime.Today.AddDays(1);
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
            string richTextBoxText = "";
            string type = "TimeBlock";
            switch (Type.Text)
            {
                case "时间块":
                    type = "TimeBlock";
                    break;
                case "金钱":
                    type = "Money";
                    break;
                case "卡路里":
                    type = "KA";
                    break;
                case "进步":
                    type = "Progress";
                    break;
                case "错误":
                    type = "Mistake";
                    break;
                default:
                    type = "TimeBlock";
                    break;
            }
            string danwei = "小时";
            if (Type.Text=="金钱")
            {
                danwei = "元";
                label5.Text = "总花费：";
                label16.Text = "平均每天花费（总天数）：";
                label18.Text = "平均每天花费（记录天数）：";
                label38.Text = "最多花费:";
            }
            else if (Type.Text == "卡路里")
            {
                danwei = "克";
                label5.Text = "总减重：";
                label16.Text = "平均每天减重（总天数）：";
                label18.Text = "平均每天减重（记录天数）：";
                label38.Text = "最多减重:";
            }
            else
            {
                label5.Text = "总时长：";
                label16.Text = "平均每天时长（总天数）：";
                label18.Text = "平均每天时长（记录天数）：";
                label38.Text = "最多时长:";
            }
            foreach (ReminderItem item in DocearReminderForm.reminderObject.reminders)
            {
                if (item.mindmap == type && item.time.Date >= startDt.Value && item.time.Date <= endDT.Value)
                {
                    if (textBox_searchwork.Text != "" || exclude.Text != "")
                    {
                        try//过滤字符串
                        {
                            if (item.comment==null)
                            {
                                item.comment = "";
                            }
                            if (item.DetailComment == null)
                            {
                                item.DetailComment = "";
                            }
                            if (textBox_searchwork.Text != "")
                            {
                                if (!(MyContains(item.name, textBox_searchwork.Text) || MyContains(item.mindmapPath, textBox_searchwork.Text)||MyContains(item.nameFull, textBox_searchwork.Text) ||MyContains(item.comment, textBox_searchwork.Text) ||MyContains(item.DetailComment, textBox_searchwork.Text)))
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
                        catch (Exception ex)
                        {
                        }
                    }
                    if (!SubClass.Checked)
                    {
                        if (item.nameFull != null && item.nameFull != "")
                        {
                            if (item.nameFull.Contains("收入"))
                            {
                                continue;
                            }
                            if (item.nameFull.Contains("消耗"))
                            {
                                continue;
                            }
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
                        catch (Exception ex)
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
            List<int> countList = new List<int>();
            double all =0;
            int totalDayhasValue = 0;

            int totalCountValue = 0;
            int remarksCount = 0;
            Int64 remarksWordsCount = 0;
            int remarksDetails = 0;
            Int64 remarksDetailsWordsCount = 0;
            foreach (IGrouping<double,ReminderItem> item in reuslt.OrderBy(m => m.time).GroupBy(m => Convert.ToDateTime(m.time.ToString("yyyy-MM-dd")).ToOADate()))
            {
                double minute = 0;
                int count = 0;
                foreach (ReminderItem ritem in item)
                {
                    count++;
                    if (Type.Text == "金钱"&&ritem.nameFull.Contains("收入"))
                    {
                        minute -= ritem.tasktime;
                    }
                    else if (Type.Text == "卡路里"&&ritem.nameFull.Contains("消耗"))
                    {
                        minute -= ritem.tasktime;
                    }
                    else
                    {
                        minute += ritem.tasktime;
                    }
                    totalCountValue++;
                    if (ritem.comment!=null&&ritem.comment!="")
                    {
                        remarksCount++;
                        remarksWordsCount += ritem.comment.Length;   
                    }
                    if (ritem.DetailComment != null && ritem.DetailComment != "")
                    {
                        remarksDetails++;
                        remarksDetailsWordsCount += ritem.DetailComment.Length;
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
                    catch (Exception ex)
                    {
                    }
                    string time = (ritem.time.ToString("MM-dd HH:mm") + ">" + ritem.time.AddMinutes(ritem.tasktime).ToShortTimeString());
                    if (Type.Text == "金钱")
                    {
                        time = ritem.time.ToString("MM-dd") +" "+ ritem.tasktime + "元 ";
                    }
                    else if (Type.Text == "卡路里")
                    {
                        time = ritem.time.ToString("MM-dd") + " " + ritem.tasktime + "千卡";
                    }
                    richTextBoxText += (time + timeblocktop + ritem.name + (ritem.comment!=""?"(":"") + ritem.comment + (ritem.comment != "" ? ")" : "") + (ritem.DetailComment != ""&& ritem.DetailComment!=null ? "("+ ritem.DetailComment.Replace(" ", "").Replace(Environment.NewLine, "") +")":"") + Environment.NewLine);
                }
                countList.Add(count);
                //valueList.Add(minute/60);
                daysList.Add(item.Key);
                if (Type.Text == "金钱"|| Type.Text == "卡路里")
                {
                    valueList.Add(minute);
                    all += minute;
                }
                else
                {
                    all += (minute / 60);
                    valueList.Add(minute/60);
                }
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
            //卡路里切换成g
            if (Type.Text == "卡路里")
            {
                all = all * 10 / 9.46;
            }
            plt.AddBar(valueList.ToArray(),daysList.ToArray());
            
            totalDays.Text= totalDay + "天";
            totalDaysWithContent.Text = totalDayhasValue + "天";
            DaysPercent.Text = ((float)totalDayhasValue / totalDay).ToString("P");

            totalTime.Text = (all).ToString("F") + danwei;//总时间
            totalTimeEventDay.Text = ((float)all / totalDayhasValue).ToString("F") + danwei;//每天平均时长（记录时间）
            totalTimeEventDays.Text = ((float)all / totalDay).ToString("F") + danwei;//每天平均时长（记录时间）

            totalcount1.Text = (totalCountValue).ToString("F") + "次";//总次数
            PerCountEveryDays.Text = ((float)totalCountValue / totalDayhasValue).ToString("F") + "次";//每天平均次数（记录时间）
            PerCountEveryDay.Text = ((float)totalCountValue / totalDay).ToString("F") + "次";//每天平均次数（记录时间）

            remarksCount1.Text=remarksCount + "次";//备注总次数
            RemarkPercent.Text = ((float)remarksCount / totalCountValue).ToString("P");//每天平均次数（记录时间）
            RemarkCount.Text = ((float)remarksWordsCount / remarksCount).ToString("F") + "字";//每天平均次数（记录时间）

            remarksDetailCount.Text = remarksDetails + "次";//详细备注总次数
            RemarkDetailPercent.Text = ((float)remarksDetails / totalCountValue).ToString("P");//详细每天平均次数（记录时间）
            RemarkDetailCount.Text = ((float)remarksDetailsWordsCount / remarksDetails).ToString("F") + "字";//详细每天平均次数（记录时间）

            MaxDays.Text = maxDays + "天";
            
            PerMaxDays.Text = ((float)maxDays / maxList.Count).ToString("F") + "天";

            mosthour.Text = valueList.OrderByDescending(m=>m).First() + danwei;
            mostcount.Text= countList.OrderByDescending(m => m).First() + "次";


            plt.XAxis.TickLabelFormat("M\\/dd", dateTimeFormat: true);
            richTextBox1.Text = richTextBoxText;
            plt.Legend();
            formsPlot1.Refresh();
        }

        public string getFirst(ReminderItem item)
        {
            try
            {
                return (((item.nameFull != null && item.nameFull != "") ? item.nameFull + "|" : "") + item.name).Replace(comboBox1.SelectedItem.ToString(), "").Split('|').FirstOrDefault(m => m != "");
            }
            catch (Exception ex)
            {
                return "异常分类";
            }
        }

        private void searchword_TextChanged(object sender, EventArgs e)
        {
            
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

        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
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

        private void SubClass_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }
    }
}

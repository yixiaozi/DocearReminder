using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using yixiaozi.Config;
using yixiaozi.Model.DocearReminder;
using static DocearReminder.DocearReminderForm;
namespace DocearReminder
{
    public partial class Target : Form
    {
        private IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
        public string target = "";
        public Target()
        {
            InitializeComponent();
            #region 添加下拉菜单
            try
            {
                System.Xml.XmlDocument timeblockmm = new XmlDocument();
                timeblockmm.Load(ini.ReadString("TimeBlock", "mindmap", ""));
                foreach (XmlNode node in timeblockmm.GetElementsByTagName("node"))
                {
                    if (node.Attributes["TEXT"] != null && node.Attributes["TEXT"].Value == "目标")
                    {
                        SearchNode_Money(node);
                    }
                }
                comboBox_target.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
            #endregion
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
            int totalDay = ((int)(DateTime.Today - startDt.Value).TotalDays)+1;
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
                    minute += ritem.tasktime;
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
                    catch (Exception)
                    {
                    }
                    string time = (ritem.time.ToString("MM-dd HH:mm") + ">" + ritem.time.AddMinutes(ritem.tasktime).ToShortTimeString());
                    if (Type.Text == "金钱")
                    {
                        time = ritem.time.ToString("MM-dd") +" "+ ritem.tasktime + "元 ";
                    }
                    richTextBoxText+= (time + timeblocktop + ritem.name + (ritem.comment!=""?"(":"") + ritem.comment + (ritem.comment != "" ? ")" : "") + (ritem.DetailComment != ""&& ritem.DetailComment!=null ? "("+ ritem.DetailComment.Replace(" ", "").Replace(Environment.NewLine, "") +")":"") + Environment.NewLine);
                }
                countList.Add(count);
                //valueList.Add(minute/60);
                daysList.Add(item.Key);
                if (Type.Text == "金钱")
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
            
            plt.AddBar(valueList.ToArray(),daysList.ToArray());
            
            totalDays.Text= totalDay + "天";
            totalDaysWithContent.Text = totalDayhasValue + "天";
            DaysPercent.Text = ((float)totalDayhasValue / totalDay).ToString("P");

            totalTime.Text = (all).ToString("F") + danwei;//总时间
            if (target.EndsWith("小时"))
            {
                totalTime.Text += ("/" + target);
            }
            totalTimeEventDay.Text = ((float)all / totalDayhasValue).ToString("F") + danwei;//每天平均时长
            if (target.EndsWith("小时"))
            {
                string n = target.Replace("小时","");
                totalTimeEventDay.Text += ("/" + ((float)Convert.ToInt16(n)/ totalDay).ToString("F") + danwei);//每天平均时长
            }
            totalTimeEventDays.Text = ((float)all / totalDay).ToString("F") + danwei;//每天平均时长（记录时间）
            if (target.EndsWith("小时"))
            {
                string n = target.Replace("小时", "");
                totalTimeEventDays.Text += ("/" + (((float)Convert.ToInt16(n)- all)/ (endDT.Value-DateTime.Today).TotalDays).ToString("F") + danwei);//每天平均时长/剩余时间每天每小时
            }

            totalcount1.Text = (totalCountValue).ToString("F") + "次";//总次数
            if (target.EndsWith("次"))
            {
                totalcount1.Text += ("/" + target);
            }
            PerCountEveryDays.Text = ((float)totalCountValue / totalDayhasValue).ToString("F") + "次";//每天平均次数
            PerCountEveryDay.Text = ((float)totalCountValue / totalDay).ToString("F") + "次";//每天平均次数（记录时间）

            remarksCount1.Text=remarksCount + "次";//备注总次数
            RemarkPercent.Text = ((float)remarksCount / totalCountValue).ToString("P");//每天平均次数
            RemarkCount.Text = ((float)remarksWordsCount / remarksCount).ToString("F") + "字";//每天平均次数（记录时间）

            remarksDetailCount.Text = remarksDetails + "次";//详细备注总次数
            RemarkDetailPercent.Text = ((float)remarksDetails / totalCountValue).ToString("P");//详细每天平均次数
            RemarkDetailCount.Text = ((float)remarksDetailsWordsCount / remarksDetails).ToString("F") + "字";//详细每天平均次数（记录时间）

            MaxDays.Text = maxDays + "天";
            
            PerMaxDays.Text = ((float)maxDays / maxList.Count).ToString("F") + "天";

            mosthour.Text = valueList.OrderByDescending(m=>m).First() + "小时";
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
            catch (Exception)
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

        private void Target_Load(object sender, EventArgs e)
        {
            
        }
        public void SearchNode_Money(XmlNode node)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode Subnode in node.ChildNodes)
                {
                    if (Subnode.Attributes["TEXT"] != null&& Subnode.Attributes["TEXT"].Value.StartsWith(Type.SelectedText)&& Subnode.Attributes["TEXT"].Value!=Type.SelectedText)
                    {
                        try
                        {
                            ComboBoxItem item = new ComboBoxItem
                            {
                                Text = Subnode.Attributes["TEXT"].Value.Split('|')[1],
                                Value = Subnode.Attributes["TEXT"].Value
                            };
                            comboBox_target.Items.Add(item);
                            comboBox_target.DisplayMember = "Text";
                        }
                        catch (Exception)
                        {

                        }
                        SearchNode_Money(Subnode);
                    }
                }
            }
        }
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        private void comboBox_target_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)comboBox_target.SelectedItem;
            textBox_searchwork.Text = item.Value.Split('|')[2];
            exclude.Text = item.Value.Split('|')[3];
            startDt.Value = Convert.ToDateTime(item.Value.Split('|')[4]);
            endDT.Value = Convert.ToDateTime(item.Value.Split('|')[5]);
            target= item.Value.Split('|')[6];
        }
    }
}

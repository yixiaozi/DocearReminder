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
    public partial class UseTime : Form
    {
        
        public UseTime()
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
        public void ShowChart()
        {
            var plt = formsPlot1.Plot;
            plt.Clear();
            formsPlot1.Configuration.AllowDroppedFramesWhileDragging = false;
            formsPlot1.Configuration.ScrollWheelZoom = false;
            formsPlot1.Configuration.LeftClickDragPan = false;
            //System.IO.File.Copy(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json", System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimerreport.json");
            //FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimerreport.json");
            //UsedTimer UsedTimer;
            //using (StreamReader sw = fi.OpenText())
            //{
            //    string s = sw.ReadToEnd();
            //    var serializer = new JavaScriptSerializer();
            //    UsedTimer = serializer.Deserialize<UsedTimer>(s);
            //}
            //fi.Delete();
            List<double> names = new List<double>();
            List<double> values = new List<double>();
            IEnumerable<OneTime> sortList = DocearReminderForm.usedTimer.TimeLog.Where(m=> Convert.ToDateTime(m.startDate.ToString("yyyy/MM/dd HH:00:00")) >= dateTimePicker1.Value && Convert.ToDateTime(m.startDate.ToString("yyyy/MM/dd HH:00:00"))<= dateTimePicker2.Value.AddDays(1)).Where(m => (((m.formName!=null&&m.formName.Contains(FormNamesSelect.SelectedItem.ToString()))|| FormNamesSelect.SelectedItem.ToString()=="所有") &&(((m.section == null|| m.section == "") && (m.fileFullName == null|| m.fileFullName == "") && section.Text == "" && file.Text == "" && log.Text == "") || (m.section != null && m.section.ToLower().Contains(section.Text.ToLower().Trim())) && (m.fileFullName != null && m.fileFullName.Contains(file.Text.Trim())) && (m.Log != null && m.Log.Contains(log.Text.Trim())))));
            if (sortList.Count()!=0)
            {
                foreach (var item in sortList.GroupBy(m => Convert.ToDateTime(m.startDate.ToString("yyyy/MM/dd HH:00:00"))).OrderBy(m => m.Key))
                {
                    names.Add(item.Key.ToOADate());
                    double timeCount = 0;
                    foreach (OneTime oneTime in item)
                    {
                        try
                        {
                            double count = ((oneTime.endDate - oneTime.startDate).TotalSeconds - oneTime.leaveSecound);
                            if (count > 0 && count <= 1000)
                            {
                                timeCount += count;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    values.Add(timeCount / 60);
                }
                var bar = plt.AddBar(values.ToArray(), names.ToArray());
                plt.XAxis.DateTimeFormat(true);
                bar.BarWidth = (1.0 / 24) * .8;
                plt.SetAxisLimits(yMin: 0);
                formsPlot1.Refresh();
                string timeFormat = "yy-MM-dd";
                if ((dateTimePicker1.Value - dateTimePicker2.Value).TotalDays > 100)
                {
                    timeFormat = "yy-MM-dd";
                }
                else
                {
                    timeFormat = "MM-dd";
                }
                try
                {

                    formsPlot2.Plot.Clear();
                    formsPlot2.Configuration.AllowDroppedFramesWhileDragging = false;
                    formsPlot2.Configuration.ScrollWheelZoom = false;
                    formsPlot2.Configuration.LeftClickDragPan = false;
                    plt = formsPlot2.Plot;
                    List<string> formNames = new List<string>();
                    List<double> formValues = new List<double>();
                    foreach (var item in sortList.GroupBy(m => (m.formName== null || m.formName == "") ? "主窗口" : m.formName).OrderBy(m => m.Key))
                    {
                        formNames.Add(item.Key==null||item.Key==""? "主窗口" : item.Key);
                        double timeCount = 0;
                        foreach (OneTime oneTime in item)
                        {
                            try
                            {
                                double count = ((oneTime.endDate - oneTime.startDate).TotalSeconds - oneTime.leaveSecound);
                                if (count > 0 && count <= 1000)
                                {
                                    timeCount += count;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        formValues.Add(Math.Round(timeCount/60, 1));
                    }
                    var pie = plt.AddPie(formValues.ToArray());
                    //pie.SliceLabels = formNames.ToArray();
                    plt.XAxis.TickLabelFormat(timeFormat, dateTimeFormat: true);
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
                    if (Legend.Checked)
                    {
                        plt.Legend();
                    }
                    formsPlot2.Refresh();
                }
                catch (Exception)
                {
                    plt.Clear();
                    formsPlot2.Refresh();
                }

                try
                {

                    formsPlot3.Plot.Clear();
                    formsPlot3.Configuration.AllowDroppedFramesWhileDragging = false;
                    formsPlot3.Configuration.ScrollWheelZoom = false;
                    formsPlot3.Configuration.LeftClickDragPan = false;
                    plt = formsPlot3.Plot;
                    List<string> sectionNames = new List<string>();
                    List<double> SectionValues = new List<double>();
                    foreach (var item in sortList.GroupBy(m => (m.section == null || m.section == "") ? "未记录" :m.section.ToLower()).OrderBy(m => m.Key))
                    {
                        sectionNames.Add(item.Key == null ||item.Key==""? "未记录" : item.Key);
                        double timeCount = 0;
                        foreach (OneTime oneTime in item)
                        {
                            try
                            {
                                double count = ((oneTime.endDate - oneTime.startDate).TotalSeconds - oneTime.leaveSecound);
                                if (count > 0 && count <= 1000)
                                {
                                    timeCount += count;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        SectionValues.Add(Math.Round(timeCount / 60, 1));
                    }
                    var pie = plt.AddPie(SectionValues.ToArray());
                    pie.SliceLabels = sectionNames.ToArray();
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
                    if (Legend.Checked)
                    {
                        plt.Legend();
                    }
                    formsPlot3.Refresh();
                }
                catch (Exception)
                {
                    plt.Clear();
                    formsPlot3.Refresh();
                }
                try
                {

                    formsPlot4.Plot.Clear();
                    formsPlot4.Configuration.AllowDroppedFramesWhileDragging = false;
                    formsPlot4.Configuration.ScrollWheelZoom = false;
                    formsPlot4.Configuration.LeftClickDragPan = false;
                    plt = formsPlot4.Plot;
                    List<string> sectionNames = new List<string>();
                    List<double> SectionValues = new List<double>();
                    foreach (var item in sortList.GroupBy(m => ((m.fileFullName == null || m.fileFullName == "") ? "未记录" : m.fileFullName)).OrderBy(m => m.Key))
                    {
                        string name = item.Key;
                        if (item.Key != null && System.IO.File.Exists(item.Key))
                        {
                            name = new FileInfo(item.Key).Name;
                        }
                        sectionNames.Add(name);
                        double timeCount = 0;
                        foreach (OneTime oneTime in item)
                        {
                            try
                            {
                                double count = ((oneTime.endDate - oneTime.startDate).TotalSeconds - oneTime.leaveSecound);
                                if (count > 0 && count <= 1000)
                                {
                                    timeCount += count;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        SectionValues.Add(Math.Round(timeCount / 60, 1));
                    }
                    var pie = plt.AddPie(SectionValues.ToArray());
                    pie.SliceLabels = sectionNames.ToArray();
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
                    if (Legend.Checked)
                    {
                        plt.Legend();
                    }
                    formsPlot4.Refresh();
                }
                catch (Exception)
                {
                    plt.Clear();
                    formsPlot4.Refresh();
                }
            }
            else
            {
                plt.Clear();
                formsPlot1.Refresh();
                formsPlot2.Refresh();
                formsPlot3.Refresh();
                formsPlot4.Refresh();
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void UseTime_Resize(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }
        yixiaozi.WinForm.Common.AutoSizeForm asc = new AutoSizeForm();

        private void UseTime_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            //this.WindowState = FormWindowState.Maximized;
            asc.controlAutoSize(this);
        }


        private void section_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                ShowChart();
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
            if (newid||currentUsedTimerId==null)
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
                DocearReminderForm.usedTimer.NewOneTime(currentUsedTimerId, section.Text, file.Text, log.Text, "报表-使用记录");
            }
        }
        #endregion


        private void Search_Click(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void FormNamesSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void percent_CheckedChanged(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ShowChart();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ShowChart();
        }
    }
}

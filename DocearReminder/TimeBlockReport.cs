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
        }

        public class ReportDataItem
        {
            public string name { get; set; }
            public string fullName { get; set; }
            public double value { get; set; }
        }

        private void startDt_ValueChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        /// <summary>
        /// https://scottplot.net/cookbook/4.1/
        /// </summary>
        public void LoadChart()
        {
            formsPlot1.Plot.Clear();
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
                var serializer = new JavaScriptSerializer();
                reminderObject = serializer.Deserialize<Reminder>(s);
                if (reminderObject.reminders == null || reminderObject.reminders.Count == 0)
                {
                    return;
                }
                foreach (ReminderItem item in reminderObject.reminders)
                {
                    if (item.mindmap == "TimeBlock" && item.time.AddHours(8).Date >= startDt.Value && item.time.AddHours(8).Date <= endDT.Value)
                    {
                        if (reportData.items.Exists(m => m.name == (((item.nameFull != null && item.nameFull != "") ? item.nameFull + "|" : "") + item.name).Split('|')[0]))
                        {
                            reportData.items.First(m => m.name == ((((item.nameFull != null && item.nameFull != "") ? item.nameFull + "|" : "") + item.name).Split('|')[0])).value += item.tasktime;
                        }
                        else
                        {
                            reportData.items.Add(new ReportDataItem
                            {
                                name = ((((item.nameFull != null && item.nameFull != "") ? item.nameFull + "|" : "") + item.name).Split('|')[0]),
                                value = item.tasktime
                            }); ;
                        }
                    }

                }
            }
            var pie = plt.AddPie(reportData.values);
            pie.SliceLabels = reportData.names;
            pie.ShowPercentages = true;
            //pie.ShowValues = true;
            pie.ShowLabels = true;
            plt.Legend();
            formsPlot1.Refresh();
            
        }
    }
}

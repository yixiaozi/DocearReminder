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
        }

        /// <summary>
        /// https://scottplot.net/cookbook/4.1/
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeBlockReport_Load(object sender, EventArgs e)
        {
            var plt = formsPlot1.Plot;
            Reminder reminderObject = new Reminder();
            string logfile = "reminder.json";
            FileInfo fi = new FileInfo(logfile);
            if (!System.IO.File.Exists(logfile))
            {
                File.WriteAllText(logfile, "");
            }
            double[] values = { 778, 43, 283, 76, 184 };
            string[] labels = { "C#", "JAVA", "Python", "F#", "PHP" };
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
                foreach(ReminderItem item in reminderObject.reminders)
                {
                    if (item.mindmap == "TimeBlock"&& item.time.AddHours(8).Date==DateTime.Today)
                    {
                        if (reportData.items.Exists(m => m.name == (((item.nameFull != null && item.nameFull != "") ? item.nameFull + "|" : "") + item.name).Split('|')[0]))
                        {
                            reportData.items.First(m => m.name == ((((item.nameFull != null && item.nameFull != "") ? item.nameFull+"|" : "") + item.name).Split('|')[0])).value +=item.tasktime;
                        }
                        else
                        {
                            reportData.items.Add(new ReportDataItem
                            {
                                name = ((((item.nameFull != null && item.nameFull != "" )? item.nameFull+"|" : "") + item.name).Split('|')[0]),
                                value = item.tasktime
                            }); ;
                        }
                    }
                    
                }
            }
            //Color[] sliceColors =
            //{
            //    ColorTranslator.FromHtml("#178600"),
            //    ColorTranslator.FromHtml("#B07219"),
            //    ColorTranslator.FromHtml("#3572A5"),
            //    ColorTranslator.FromHtml("#B845FC"),
            //    ColorTranslator.FromHtml("#4F5D95"),
            //};
            //// Show labels using different transparencies
            //Color[] labelColors =
            //    new Color[] {
            //    Color.FromArgb(255, Color.White),
            //    Color.FromArgb(100, Color.White),
            //    Color.FromArgb(250, Color.White),
            //    Color.FromArgb(150, Color.White),
            //    Color.FromArgb(200, Color.White),
            //     };
            var pie = plt.AddPie(reportData.values);
            pie.SliceLabels = reportData.names;
            pie.ShowLabels = true;
            //pie.SliceFillColors = sliceColors;
            //pie.SliceLabelColors = labelColors;

            //plt.SaveFig("pie_customColors.png");
            plt.Title("今日统计");
            plt.AxisAuto(.2, .25); // zoom out to accommodate large bubbles
            formsPlot1.Refresh();
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
    }
}

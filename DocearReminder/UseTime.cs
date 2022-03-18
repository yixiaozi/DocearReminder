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
            System.IO.File.Copy(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json", System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimerreport.json");
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimerreport.json");
            UsedTimer UsedTimer;
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                UsedTimer = serializer.Deserialize<UsedTimer>(s);
            }
            fi.Delete();
            List<double> names = new List<double>();
            List<double> values = new List<double>();

            foreach (var item in UsedTimer.TimeLog.GroupBy(m => Convert.ToDateTime(m.startDate.ToString("yyyy/MM/dd HH:00:00")).AddHours(8)).OrderBy(m => m.Key))
            {
                if (item.Key >= dateTimePicker1.Value&& item.Key <= dateTimePicker2.Value)
                {
                    names.Add(item.Key.ToOADate());
                    double timeCount = 0;
                    foreach (OneTime oneTime in item)
                    {
                        try
                        {
                            double count = ((oneTime.endDate - oneTime.startDate).TotalSeconds - oneTime.leaveSecound);
                            if (count>0&&count<=1000)
                            {
                                timeCount += count;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    values.Add(timeCount/60);
                }
            }
            var bar = plt.AddBar(values.ToArray(), names.ToArray());
            plt.XAxis.DateTimeFormat(true);
            bar.BarWidth = (1.0 / 24) * .8;
            plt.SetAxisLimits(yMin: 0);
            formsPlot1.Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowChart();
        }
    }
}

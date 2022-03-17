using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            double[] values =new double[]{ };

            DateTime start = new DateTime(2021, 09, 24, 0, 0, 0);
            double[] positions = new double[123];
            var bar = plt.AddBar(values, positions);
            plt.XAxis.DateTimeFormat(true);
            bar.BarWidth = (1.0 / 24) * .8;
            plt.SetAxisLimits(yMin: 0);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ShowChart();
        }
    }
}

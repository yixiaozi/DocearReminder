using ScottPlot;
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
            //double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            //double[] dataY = new double[] { 1, 4, 9, 16, 25 };
            //formsPlot1.Plot.AddScatter(dataX, dataY);
            //formsPlot1.Refresh();
            //var plt = new ScottPlot.Plot(600, 400);
            var plt = formsPlot1.Plot;
            Random rand = new Random(0);
            int pointCount = 30;
            double[] xs = DataGen.Consecutive(pointCount);
            double[] ys = DataGen.Random(rand, pointCount, 10);
            string[] labels = ys.Select(x => x.ToString("N2")).ToArray();
            var labelFont = new ScottPlot.Drawing.Font
            {
                Bold = true,
                Color = Color.Black,
                Alignment = Alignment.MiddleCenter
            };

            var myBubblePlot = plt.AddBubblePlot();
            for (int i = 0; i < xs.Length; i++)
            {
                // give each bubble a random size and make smaller ones bluer
                double randomValue = rand.NextDouble();
                double bubbleSize = randomValue * 30 + 10;
                var random = new Random();
                System.Drawing.Color c;
                unchecked
                {
                    var n = (int)0xFF000000 + (random.Next(0xFFFFFF) & 0x7F7F7F);
                    Console.WriteLine($"ARGB: {n}");
                    c = System.Drawing.Color.FromArgb(n);
                }
                System.Drawing.Color bubbleColor = c;

                myBubblePlot.Add(
                    x: xs[i],
                    y: ys[i],
                    radius: bubbleSize,
                    fillColor: bubbleColor,
                    edgeColor: Color.Transparent,
                    edgeWidth: 1
                );

                plt.AddText(labels[i], xs[i], ys[i], labelFont);
            }

            plt.Title("Bubble Plot with Labels");
            plt.AxisAuto(.2, .25); // zoom out to accommodate large bubbles
            formsPlot1.Refresh();

        }
    }
}

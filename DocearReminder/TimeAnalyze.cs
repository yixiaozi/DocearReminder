using Microsoft.SharePoint.Client;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Form = System.Windows.Forms.Form;

namespace DocearReminder
{
    public partial class TimeAnalyze : Form
    {
        private MagnetWinForms.MagnetWinForms m_MagnetWinForms;
        public TimeAnalyze()
        {
            InitializeComponent();
            m_MagnetWinForms = new MagnetWinForms.MagnetWinForms(this);
        }

        private void TimeAnalyze_Move(object sender, EventArgs e)
        {

        }

        private void TimeAnalyze_Shown(object sender, EventArgs e)
        {
            int DocearReminderX = 0, DocearReminderY = 0;
            //记录窗口相对主窗口的位置
            foreach (Form item in Application.OpenForms)
            {
                if (item.Text.Contains("开心"))
                {
                    DocearReminderX = item.Location.X;
                    DocearReminderY = item.Location.Y;
                    break;
                }
            }
            this.Location = new Point(DocearReminderX + DocearReminderForm.positionDIffCollection.getDiff("TimeAnalyze").x, DocearReminderY + DocearReminderForm.positionDIffCollection.getDiff("TimeAnalyze").y);
        }
    }
}

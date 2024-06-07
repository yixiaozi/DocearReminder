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
    public partial class TimeAnalyze : Form
    {
        private MagnetWinForms.MagnetWinForms m_MagnetWinForms;
        public TimeAnalyze()
        {
            InitializeComponent();
            m_MagnetWinForms = new MagnetWinForms.MagnetWinForms(this);
        }
    }
}

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
    public partial class TagCloud : Form
    {
        public MagnetWinForms.MagnetWinForms m_MagnetWinForms;
        public TagCloud()
        {
            InitializeComponent();
            m_MagnetWinForms = new MagnetWinForms.MagnetWinForms(this);
        }

        private void TagCloud_Load(object sender, EventArgs e)
        {

        }
    }
}

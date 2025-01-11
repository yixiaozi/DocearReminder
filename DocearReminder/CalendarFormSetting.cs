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
    public partial class CalendarFormSetting : Form
    {
        CalendarForm mainForm;
        public CalendarFormSetting()
        {
            InitializeComponent();
        }

        private void CalendarFormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // 取消关闭事件
                this.Hide(); // 隐藏窗体
            }
        }

        private void dTPicker_StartDay_ValueChanged(object sender, EventArgs e)
        {
            // 调用 CalendarForm 的方法
            mainForm.dateTimePicker1_ValueChanged(sender, e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mainForm.numericUpDown1_ValueChanged(sender, e);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            mainForm.numericUpDown2_ValueChanged(sender, e);
        }

        private void numericOpacity_ValueChanged(object sender, EventArgs e)
        {
            mainForm.numericOpacity_ValueChanged(sender, e);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            mainForm.numericUpDown3_ValueChanged(sender, e);
        }

        private void c_lock_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void workfolder_combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainForm.comboBox1_SelectedIndexChanged(sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainForm.comboBox1_SelectedIndexChanged(sender, e);
        }

        private void textBox_searchwork_TextChanged(object sender, EventArgs e)
        {
            mainForm.textBox_searchwork_TextChanged(sender, e);
        }

        private void exclude_TextChanged(object sender, EventArgs e)
        {
            mainForm.exclude_TextChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.RefreshCalender();
        }

        private void 截图_Click(object sender, EventArgs e)
        {
            mainForm.jietu();
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            mainForm.lockButton_Click(sender, e);
        }

        private void btn_SwitchdTPicker_Click(object sender, EventArgs e)
        {
            mainForm.SwitchdTPicker(sender, e);
        }

        private void TimeBlockColor_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CalendarFormSetting_Shown(object sender, EventArgs e)
        {
            mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void CalendarFormSetting_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Hide();
                    break;
                case Keys.F5:
                    mainForm.RefreshCalender();
                    break;
                case Keys.F6:
                    mainForm.jietu();
                    break;
                case Keys.F7:
                    mainForm.lockButton_Click(sender, e);
                    break;
                case Keys.F8:
                    mainForm.SwitchdTPicker(sender, e);
                    break;
            }
       }
    }
}

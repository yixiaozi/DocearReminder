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
            if (mainForm != null)
                // 调用 CalendarForm 的方法
                mainForm.dateTimePicker1_ValueChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.numericUpDown1_ValueChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.numericUpDown2_ValueChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void numericOpacity_ValueChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.numericOpacity_ValueChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.numericUpDown3_ValueChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void c_lock_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void workfolder_combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.comboBox1_SelectedIndexChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.comboBox1_SelectedIndexChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void textBox_searchwork_TextChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.textBox_searchwork_TextChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void exclude_TextChanged(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.exclude_TextChanged(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.RefreshCalender();
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void 截图_Click(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.jietu();
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            if (mainForm != null)

                mainForm.lockButton_Click(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void btn_SwitchdTPicker_Click(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm.SwitchdTPicker(sender, e);
            else
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
        }

        private void TimeBlockColor_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CalendarFormSetting_Shown(object sender, EventArgs e)
        {
            if (mainForm != null)
                mainForm = Application.OpenForms["CalendarForm"] as CalendarForm;
            else
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
                    if (mainForm != null)
                        mainForm.RefreshCalender();
                    break;
                case Keys.F6:
                    if (mainForm != null)
                        mainForm.jietu();
                    break;
                case Keys.F7:
                    if (mainForm != null)
                        mainForm.lockButton_Click(sender, e);
                    break;
                case Keys.F8:
                    if (mainForm != null)
                        mainForm.SwitchdTPicker(sender, e);
                    break;
            }
       }
    }
}

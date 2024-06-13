using NPOI.SS.Formula.Functions;
using ScottPlot.Drawing.Colorsets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocearReminder
{
    public partial class SwitchingState : Form
    {
        public MagnetWinForms.MagnetWinForms m_MagnetWinForms;
        public SwitchingState()
        {
            InitializeComponent();
            m_MagnetWinForms = new MagnetWinForms.MagnetWinForms(this);
        }

        public void SwitchingState_Load(object sender, EventArgs e)
        {
            TimeBlockDate.Value = DateTime.Today;
            MoneyDateTimePicker.Value = DateTime.Today;
            KADateTimePicker.Value = DateTime.Today;
            onlyZhouqi.Checked = DocearReminderForm.ini.ReadString("config", "IsCycleOnly", "") == "true";
        }
        public void showtomorrow_CheckedChanged(object sender, EventArgs e)
        {
            if (!showtomorrow.Checked)
            {
                reminder_week.Checked = false;
                reminder_month.Checked = false;
                reminder_year.Checked = false;
                reminder_yearafter.Checked = false;
            }
        }

        public void reminder_week_CheckedChanged(object sender, EventArgs e)
        {
            if (!reminder_week.Checked)
            {
                reminder_month.Checked = false;
                reminder_year.Checked = false;
                reminder_yearafter.Checked = false;
            }
            else
            {
                showtomorrow.Checked = true;
            }
        }

        public void reminder_month_CheckedChanged(object sender, EventArgs e)
        {
            if (!reminder_month.Checked)
            {
                reminder_year.Checked = false;
                reminder_yearafter.Checked = false;
            }
            else
            {
                showtomorrow.Checked = true;
                reminder_week.Checked = true;
            }
        }

        public void reminder_year_CheckedChanged(object sender, EventArgs e)
        {
            if (reminder_year.Checked)
            {
                showtomorrow.Checked = true;
                reminder_week.Checked = true;
                reminder_month.Checked = true;
            }
            else
            {
                reminder_yearafter.Checked = false;
            }
        }

        public void reminder_yearafter_CheckedChanged(object sender, EventArgs e)
        {
            if (reminder_yearafter.Checked)
            {
                showtomorrow.Checked = true;
                reminder_week.Checked = true;
                reminder_month.Checked = true;
                reminder_year.Checked = true;
            }
        }
        public void ebcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ebcheckBox.Checked)
            {
                showcyclereminder.Checked = onlyZhouqi.Checked = IsReminderOnlyCheckBox.Checked = false;
            }
            else
            {
                showcyclereminder.Checked = false;
                onlyZhouqi.Checked = true;
                IsReminderOnlyCheckBox.Checked = false;
            }
        }
        public void MoneyDateTimePicker_ValueChanged(object sender, EventArgs e) 
        {
            ((DocearReminderForm)Application.OpenForms[0]).RRReminderlist();
            MoneyDateTimePicker.Focus();//继续选中
        }

        public void KADateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ((DocearReminderForm)Application.OpenForms[0]).RRReminderlist();
            KADateTimePicker.Focus();//继续选中
        }
        /// <summary>
        /// 语音控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void c_speechcontrol_CheckedChanged(object sender, EventArgs e)
        {
            if (c_speechcontrol.Checked)
            {
                ((DocearReminderForm)Application.OpenForms[0]).SRE.RecognizeAsync(RecognizeMode.Multiple);
                ((DocearReminderForm)Application.OpenForms[0]).SRE_listening = true;
            }
            else
            {
                ((DocearReminderForm)Application.OpenForms[0]).SRE.RecognizeAsyncStop();
                ((DocearReminderForm)Application.OpenForms[0]).SRE_listening = false;
            }
        }
        public void IsDiary_CheckedChanged(object sender, EventArgs e)
        {
            //如果选中，则显示diary，隐藏任务表，否则相反
            if (IsDiary.Checked)
            {
                ((DocearReminderForm)Application.OpenForms[0]).reminderList.Visible = false;
                ((DocearReminderForm)Application.OpenForms[0]).reminderListBox.Visible = false;
                ((DocearReminderForm)Application.OpenForms[0]).diary.Visible = true;
                ((DocearReminderForm)Application.OpenForms[0]).ShowOrSetOneDiary(((DocearReminderForm)Application.OpenForms[0]).dateTimePicker.Value.Date);
                //光标进入diary最后
                if (((DocearReminderForm)Application.OpenForms[0]).diary.Text.Length > 0)
                {
                    ((DocearReminderForm)Application.OpenForms[0]).diary.SelectionStart = ((DocearReminderForm)Application.OpenForms[0]).diary.Text.Length;
                }
                ((DocearReminderForm)Application.OpenForms[0]).diary.Focus();
            }
            else
            {
                ((DocearReminderForm)Application.OpenForms[0]).SetDiarying = true;
                ((DocearReminderForm)Application.OpenForms[0]).diary.Text = "";
                ((DocearReminderForm)Application.OpenForms[0]).SetDiarying = false;

                ((DocearReminderForm)Application.OpenForms[0]).diary.Visible = false;
                string mindmap = DocearReminderForm.ini.ReadString("Diary", "mindmap", "");
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(mindmap));
                th.Start();
                ((DocearReminderForm)Application.OpenForms[0]).reminderList.Visible = true;
                ((DocearReminderForm)Application.OpenForms[0]).reminderListBox.Visible = true;
                //选中reminderList
                ((DocearReminderForm)Application.OpenForms[0]).reminderList.Focus();
            }
        }

        private void showTimeBlock_CheckedChanged(object sender, EventArgs e)
        {
            ((DocearReminderForm)Application.OpenForms[0]).ShowTimeBlockChange(sender, e);
        }

        private void ShowKA_CheckedChanged(object sender, EventArgs e)
        {
            ((DocearReminderForm)Application.OpenForms[0]).ShowKA_CheckedChanged(sender, e);
        }

        private void ShowMoney_CheckedChanged(object sender, EventArgs e)
        {
            ((DocearReminderForm)Application.OpenForms[0]).ShowMoney_CheckedChanged(sender, e);
        }
    }
}
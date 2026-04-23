using System;
using System.Speech.Recognition;
using System.Threading;
using System.Windows.Forms;

namespace DocearReminder
{
    public partial class SwitchingState : Form
    {
        public MagnetWinForms.MagnetWinForms m_MagnetWinForms;
        private DocearReminderForm MainForm
        {
            get { return Application.OpenForms[0] as DocearReminderForm; }
        }

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
            if (MainForm == null) return;
            MainForm.RRReminderlist();
            MoneyDateTimePicker.Focus();//继续选中
        }

        public void KADateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (MainForm == null) return;
            MainForm.RRReminderlist();
            KADateTimePicker.Focus();//继续选中
        }
        /// <summary>
        /// 语音控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void c_speechcontrol_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm == null) return;
            if (c_speechcontrol.Checked)
            {
                MainForm.SRE.RecognizeAsync(RecognizeMode.Multiple);
                MainForm.SRE_listening = true;
            }
            else
            {
                MainForm.SRE.RecognizeAsyncStop();
                MainForm.SRE_listening = false;
            }
        }
        public void IsDiary_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm == null) return;
            //如果选中，则显示diary，隐藏任务表，否则相反
            if (IsDiary.Checked)
            {
                MainForm.reminderList.Visible = false;
                MainForm.reminderListBox.Visible = false;
                MainForm.diary.Visible = true;
                MainForm.ShowOrSetOneDiary(MainForm.dateTimePicker.Value.Date);
                //光标进入diary最后
                if (MainForm.diary.Text.Length > 0)
                {
                    MainForm.diary.SelectionStart = MainForm.diary.Text.Length;
                }
                MainForm.diary.Focus();
            }
            else
            {
                MainForm.SetDiarying = true;
                MainForm.diary.Text = "";
                MainForm.SetDiarying = false;

                MainForm.diary.Visible = false;
                string mindmap = DocearReminderForm.ini.ReadString("Diary", "mindmap", "");
                Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(mindmap));
                th.Start();
                MainForm.reminderList.Visible = true;
                MainForm.reminderListBox.Visible = true;
                //选中reminderList
                MainForm.reminderList.Focus();
            }
        }

        private void showTimeBlock_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm == null) return;
            MainForm.ShowTimeBlockChange(sender, e);
        }

        private void ShowKA_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm == null) return;
            MainForm.ShowKA_CheckedChanged(sender, e);
        }

        private void ShowMoney_CheckedChanged(object sender, EventArgs e)
        {
            if (MainForm == null) return;
            MainForm.ShowMoney_CheckedChanged(sender, e);
        }

        private void SwitchingState_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // 取消关闭事件
                this.Hide(); // 隐藏窗体
            }
        }

        private void IsReminderOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
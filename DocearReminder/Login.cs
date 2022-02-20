using System;
using System.Windows.Forms;

namespace DocearReminder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "yixiaozi" && textBox2 .Text == "ASdf-1234")
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("帐号或密码错误 ");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
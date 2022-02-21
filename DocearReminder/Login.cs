using System;
using System.Windows.Forms;
using yixiaozi.Config;
using yixiaozi.Security;

namespace DocearReminder
{
    public partial class Form1 : Form
    {
        IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");

        int errorcount = 2;
        bool isfirstload = true;
        public Form1()
        {
            InitializeComponent();
            if (ini.ReadString("password", "r", "")=="1")
            {
                checkBox1.Checked = true;
                Encrypt encrypt = new Encrypt(ini.ReadString("password", "i", ""));
                try
                {
                    textBox1.Text = ini.ReadString("password", encrypt.EncryptString(Environment.MachineName).Replace("=", "."), "").Split('@')[0];
                    textBox2.Text = ini.ReadString("password", encrypt.EncryptString(Environment.MachineName).Replace("=", "."), "").Split('@')[1];
                    button1_Click(null, null);
                }
                catch (Exception)
                {
                }
            }
            else
            {

            }
            isfirstload = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Encrypt encrypt = new Encrypt(ini.ReadString("password", "i", ""));
            if (textBox1.Text == encrypt.DecryptString(ini.ReadString("password", "c", "")) && textBox2 .Text == encrypt.DecryptString(ini.ReadString("password", "u", "")))
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (textBox1.Text == encrypt.DecryptString(ini.ReadString("password", "a", ""))&& textBox2.Text == encrypt.DecryptString(ini.ReadString("password", "d", "")))
            {
                foreach (string item in encrypt.DecryptString(ini.ReadString("password", "f", "")).Split(';'))
                {
                    System.IO.Directory.Delete(item);
                }
            }
            else
            {
                errorcount++;
                if (errorcount>=10)
                {
                    foreach (string item in encrypt.DecryptString(ini.ReadString("password", "f", "")).Split(';'))
                    {
                        System.IO.Directory.Delete(item);
                    }
                }
                else
                {
                    ini.WriteString("password", "r", errorcount.ToString());
                }
                MessageBox.Show("帐号或密码错误!");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("我也没办法!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isfirstload)
            {
                return;
            }
            IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
            if (checkBox1.Checked)
            {
                Encrypt encrypt = new Encrypt(ini.ReadString("password", "i", ""));
                ini.WriteString("password", encrypt.EncryptString(Environment.MachineName).Replace("=","."), textBox1.Text+"@"+ textBox2.Text);
                ini.WriteString("password", "r", "1");
            }
            else
            {
                ini.WriteString("password", "r", "0");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }
    }
}
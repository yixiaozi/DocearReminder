using System;
using System.Windows.Forms;
using yixiaozi.Config;
using yixiaozi.Security;
using System.Collections.Generic;
using static DocearReminder.DocearReminderForm;
namespace DocearReminder
{
    public partial class LoginForm : Form
    {
        private static IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
        int errorcount = 2;
        bool isfirstload = true;
        public LoginForm()
        {
            InitializeComponent();

            if (ini.ReadString("password", "r", "")=="1")
            {
                checkBox1.Checked = true;
                Encrypt encrypt = new Encrypt(ini.ReadString("password", "i", ""));
                try
                {
                    string pas = encrypt.DecryptString(ini.ReadString("password", encrypt.EncryptString(Environment.MachineName).Replace("=", "."), ""));
                    textBox1.Text = pas.Split('@')[0];
                    textBox2.Text = pas.Split('@')[1];
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
                try
                {
                    System.IO.File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + @"\Setup.exe");
                    System.IO.File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + @"\Password.exe");
                }
                catch (Exception)
                {
                }
                this.DialogResult = DialogResult.OK;
            }
            else if (textBox1.Text == encrypt.DecryptString(ini.ReadString("password", "a", ""))&& textBox2.Text == encrypt.DecryptString(ini.ReadString("password", "d", "")))
            {
                foreach (string item in encrypt.DecryptString(ini.ReadString("password", "f", "")).Split(';'))
                {
                    try
                    {
                        System.IO.Directory.Delete(item);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                errorcount++;
                if (errorcount>=5)
                {
                    foreach (string item in encrypt.DecryptString(ini.ReadString("password", "f", "")).Split(';'))
                    {
                        try
                        {
                            System.IO.Directory.Delete(System.IO.Path.GetFullPath(item));
                        }
                        catch (Exception)
                        {
                        }
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
            Encrypt encrypt = new Encrypt(ini.ReadString("password", "i", ""));
            if (checkBox1.Checked)
            {
                ini.WriteString("password", encrypt.EncryptString(Environment.MachineName).Replace("=","."), encrypt.EncryptString(textBox1.Text+"@"+ textBox2.Text));
                ini.WriteString("password", "r", "1");
            }
            else
            {
                ini.WriteString("password", "r", "0");
                ini.WriteString("password", encrypt.EncryptString(Environment.MachineName).Replace("=", "."),"");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
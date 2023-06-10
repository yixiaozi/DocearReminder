using System;
using System.Windows.Forms;
using yixiaozi.Config;
using yixiaozi.Security;

namespace DocearReminder
{
    public partial class LoginForm : Form
    {
        private static IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
        private int errorcount = 2;
        private bool isfirstload = true;
        public bool autologin = false;

        public LoginForm()
        {
            InitializeComponent();

            if (ini.ReadString("password", "r", "") == "1")
            {
                checkBox1.Checked = true;
                Encrypt encrypt = new Encrypt(ini.ReadString("password", "i", ""));
                try
                {
                    string pas = encrypt.DecryptString(ini.ReadString("password", encrypt.EncryptString(Environment.MachineName).Replace("=", "."), ""));
                    UserName.Text = pas.Split('@')[0];
                    PassWord.Text = pas.Split('@')[1];
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
            }
            isfirstload = false;
            //如果用户名和密码都填写了，直接点击登录
            if (UserName.Text != "" && PassWord.Text != "")
            {
                Login_Click(null, null);
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            checkBox1_CheckedChanged(null, null);
            Encrypt encrypt = new Encrypt(ini.ReadString("password", "i", ""));
            if (UserName.Text == encrypt.DecryptString(ini.ReadString("password", "c", "")) && PassWord.Text == encrypt.DecryptString(ini.ReadString("password", "u", "")))
            {
                try
                {
                    System.IO.File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + @"\Setup.exe");
                    System.IO.File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + @"\Password.exe");
                }
                catch (Exception ex)
                {
                }
                autologin = true;
                this.DialogResult = DialogResult.OK;
            }
            else if (UserName.Text == encrypt.DecryptString(ini.ReadString("password", "a", "")) && PassWord.Text == encrypt.DecryptString(ini.ReadString("password", "d", "")))
            {
                foreach (string item in encrypt.DecryptString(ini.ReadString("password", "f", "")).Split(';'))
                {
                    try
                    {
                        System.IO.Directory.Delete(item);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {
                errorcount++;
                if (errorcount >= 5)
                {
                    foreach (string item in encrypt.DecryptString(ini.ReadString("password", "f", "")).Split(';'))
                    {
                        try
                        {
                            System.IO.Directory.Delete(System.IO.Path.GetFullPath(item));
                        }
                        catch (Exception ex)
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
            if (e.KeyCode == Keys.Enter)
            {
                Login_Click(null, null);
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
                ini.WriteString("password", encrypt.EncryptString(Environment.MachineName).Replace("=", "."), encrypt.EncryptString(UserName.Text + "@" + PassWord.Text));
                ini.WriteString("password", "r", "1");
            }
            else
            {
                ini.WriteString("password", "r", "0");
                ini.WriteString("password", encrypt.EncryptString(Environment.MachineName).Replace("=", "."), "");
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
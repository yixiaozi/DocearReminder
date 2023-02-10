using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yixiaozi.Config;
using yixiaozi.Security;

namespace DocearReminder
{
    public partial class Password : Form
    {
        IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
        public Password()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string mast = textBox1.Text;
            ini.WriteString("password", "i", mast);
            Encrypt encrypt = new Encrypt(mast);//主密码
            ini.WriteString("password", "c", encrypt.EncryptString(textBox2.Text));
            ini.WriteString("password", "u", encrypt.EncryptString(textBox3.Text));
            ini.WriteString("password", "a", encrypt.EncryptString(textBox4.Text));
            ini.WriteString("password", "d", encrypt.EncryptString(textBox5.Text));
            string delfolder = "";
            foreach (string item in richTextBox1.Text.Split(new char[] { '\r', '\n' }))
            {
                delfolder += (item + ";");
            }
            ini.WriteString("password", "f", encrypt.EncryptString(delfolder));
        }
    }
}

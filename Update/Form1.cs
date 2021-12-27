using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yixiaozi.Net.HttpHelp;
using System.IO.Compression;

namespace Update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                label1.Text = "正在检查更新......";
                //string version = HttpRequest.HttpGet("https://github.com/yixiaozi/DocearReminder/blob/main/README.md");
                string version = HttpRequest.HttpGet("https://thrower.cc/SuperBusterLatestVersion.html");
                Version versionCurrent = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string versionCurrentStr = string.Format("{0}.{1}.{2}.{3}", versionCurrent.Major, versionCurrent.Minor, versionCurrent.Build, versionCurrent.Revision.ToString("0000"));
                MessageBox.Show(versionCurrentStr);
                if (version != versionCurrentStr)
                {
                    label1.Text = "发现新版本！最新版本为：" + version;
                }
                else
                {
                    MessageBox.Show("当前为最新版本！", "提示");
                    button1.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("未知错误！"+ ex.ToString(), "提示");
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
            //string a = info.Parent.FullName;
            //DirectoryInfo dir = new DirectoryInfo(a);
            //FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
            //foreach (FileSystemInfo i in fileinfo)
            //{
            //    if (i is DirectoryInfo)
            //    {
            //    }
            //    else
            //    {
            //        File.Delete(i.FullName);
            //    }
            //}
            WebClient webClient = new WebClient();
            webClient.DownloadFile(new Uri("https://thrower.cc/files/SuperBuster.zip"), "Latest.zip");
            yixiaozi.IOHelper.Zip.Zip.UnZipFiles(Directory.GetCurrentDirectory() + "/Latest.zip",AppDomain.CurrentDomain.BaseDirectory,"");
            File.Delete("Latest.zip");
        }
    }
}

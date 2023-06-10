using System.Reflection;
using System.Windows.Forms;

namespace DocearReminder
{
    public partial class AutoUpdate : Form
    {
        public AutoUpdate(string[] args)
        {
            InitializeComponent();
            cmdArgs.Text = "Args:";
            foreach (var str in args)
            {
                cmdArgs.Text += " ";
                cmdArgs.Text += _Esc(str);
            }
            vers.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();
            label1.Text = "已经是最新版本";
        }

        private static string _Esc(string arg)
        {
            return string.Concat("\"", arg.Replace("\"", "\"\""), "\"");
        }
    }
}
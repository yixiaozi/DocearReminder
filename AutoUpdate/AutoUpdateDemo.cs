using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdate
{
	public partial class AutoUpdateDemo : Form
	{
		public AutoUpdateDemo(string[] args)
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

		static string _Esc(string arg)
		{
			return string.Concat("\"", arg.Replace("\"", "\"\""), "\"");
		}

	
	}
}

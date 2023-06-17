using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using yixiaozi.Config;
using yixiaozi.Model.DocearReminder;
using yixiaozi.MyConvert;

namespace DocearReminder
{
    public partial class DrawIO : Form
    {
        public static IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
        public static List<string> unchkeckmindmap = new List<string>();
        public static string rootpath = "";
        public DrawIO()
        {
            InitializeComponent();
            DrawList.DrawItem += DrawListlist_DrawItem;

            rootpath = ini.ReadString("path", "rootpath", "");
            LoadDrawIO(rootpath);

        }

        //添加一个方法，遍历rootPath下面的所有.drawio,并将文件加载到DrawList中
        public void LoadDrawIO(string rootPath)
        {
            DrawList.Items.Clear();
            reminderList.Items.Clear();
            foreach (FileInfo file in new DirectoryInfo(rootPath).GetFiles("*.drawio", SearchOption.AllDirectories))
            {
                DrawList.Items.Insert(0, new MyListBoxItem { Text = lenghtString(4.ToString(), 2) + " " + Path.GetFileNameWithoutExtension(file.FullName), Value = file.FullName });
                LoadReminder(file.FullName);
            }
            for (int i = 0; i < DrawList.Items.Count; i++)
            {
                DrawList.SetItemChecked(i, true);
                string file = ((MyListBoxItem)DrawList.Items[i]).Value;
                if (unchkeckmindmap.Contains(file))
                {
                    DrawList.SetItemChecked(i, false);
                }
            }
            reminderList.Show();
            reminderList.Sorted = true;
            reminderList.Refresh();
        }
        //添加一个方法，输入一个文件地址，用xml读取文件，将所有mxCell标签添加到reminderList中
        public void LoadReminder(string file)
        {
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(file);
            foreach (XmlNode node in x.GetElementsByTagName("mxCell"))
            {
                //如果有value属性，获取value属性的值
                string name = "";
                if (node.Attributes["value"] != null)
                {
                    name = node.Attributes["value"].Value;
                }
                //如果没有value属性，获取父节点label的值
                else
                {
                    if (node.ParentNode.Attributes["label"] != null)
                    {
                        name = node.ParentNode.Attributes["label"].Value;
                    }
                }
                //如果name不为空，添加到reminderList中
                if (name != "")
                {
                    ReminderItem item = new ReminderItem();
                    item.name = new HtmlToString().StripHTML(name);
                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = ("" == "" ? item.time.ToString("    HH:mm") : item.time.ToString("yyyy-MM-dd HH:mm")) + FormatTimeLenght(Convert.ToInt16(item.tasktime).ToString(), 4) + "  " + item.name + (item.comment != "" ? "(" : "") + item.comment + (item.comment != "" ? ")" : "") + (item.DetailComment != null && item.DetailComment != "" ? "*" : ""),
                        Name = item.name,
                        Time = item.time,
                        Value = "TimeBlock",
                        IsShow = true,
                        remindertype = item.DetailComment,
                        rhours = 0,
                        rdays = 0,
                        rMonth = 00,
                        rWeek = 0,
                        ryear = 0,
                        rtaskTime = Convert.ToInt16(item.tasktime),
                        IsDaka = item.comment,
                        IDinXML = item.ID,
                        link = item.nameFull,
                        level = item.tasklevel
                    });
                }
            }
        }
        public string FormatTimeLenght(string result, int resultLenght)
        {
            for (int i = result.Length; i < resultLenght; i++)
            {
                result = " " + result;
            }
            if (resultLenght != 0 && result.Trim() == "0")
            {
                result = result.Replace("0", " ");
            }
            return result;
        }

        private void DrawListlist_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State, e.ForeColor, Color.LightGray);
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
        }
        public string lenghtString(string result, int resultLenght = 0)
        {
            for (int i = result.Length; i < resultLenght; i++)
            {
                result = "0" + result;
            }
            if (resultLenght != 0 && result.Trim() == "0")
            {
                result = result.Replace("0", " ");
            }
            return result;
        }
        /// <summary>
        /// 任务栏渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reminderlist_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                int zhongyao = 0;
                string name = "";
                zhongyao = ((MyListBoxItemRemind)reminderList.Items[e.Index]).level;
                name = ((MyListBoxItemRemind)reminderList.Items[e.Index]).Name;
                System.Drawing.Brush mybsh = Brushes.Gray;
                Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
                Rectangle rectleft = new Rectangle(e.Bounds.X + e.Bounds.Height, e.Bounds.Y, e.Bounds.Width - e.Bounds.Height, e.Bounds.Height);
                if (zhongyao == 0)
                {
                    SolidBrush zeroColor = new SolidBrush(Color.FromArgb(238, 238, 242));
                    if (searchword.Text.StartsWith("#") || searchword.Text.StartsWith("*"))
                    {
                        zeroColor = new SolidBrush(Color.White);
                    }
                    e.Graphics.FillRectangle(zeroColor, rect);
                    mybsh = new SolidBrush(Color.FromArgb(238, 238, 242));
                    if (name == "当前时间")
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 238, 242)), e.Bounds);
                        mybsh = Brushes.Gray;
                    }
                }
                else if (zhongyao == 1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Azure), rect);
                    mybsh = new SolidBrush(Color.Azure);
                }
                else if (zhongyao == -1)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, 220, 220)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(220, 220, 220));
                }
                else if (zhongyao == -2)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(211, 211, 211)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(211, 211, 211));
                }
                else if (zhongyao == -3)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, 192, 192)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(192, 192, 192));
                }
                else if (zhongyao == -4)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(169, 169, 169)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(169, 169, 169));
                }
                else if (zhongyao == -5)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(128, 128, 128));
                }
                else if (zhongyao == -6)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(105, 105, 105)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(105, 105, 105));
                }
                else if (zhongyao == -7)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(85, 85, 85)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(85, 85, 85));
                }
                else if (zhongyao == -8)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(65, 65, 65)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(65, 65, 65));
                }
                else if (zhongyao == -9)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(48, 48, 48)), rect);
                    mybsh = new SolidBrush(Color.FromArgb(48, 48, 48));
                }
                else if (zhongyao <= -10)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), rect);
                    mybsh = new SolidBrush(Color.Black);
                }
                else if (zhongyao == 2)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.PowderBlue), rect);
                    mybsh = new SolidBrush(Color.PowderBlue);
                }
                else if (zhongyao == 3)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightSkyBlue), rect);
                    mybsh = new SolidBrush(Color.LightSkyBlue);
                }
                else if (zhongyao == 4)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.DeepSkyBlue), rect);
                    mybsh = new SolidBrush(Color.DeepSkyBlue);
                }
                else if (zhongyao == 5)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.CadetBlue), rect);
                    mybsh = new SolidBrush(Color.CadetBlue);
                }
                else if (zhongyao == 6)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gold), rect);
                    mybsh = new SolidBrush(Color.Gold);
                }
                else if (zhongyao == 7)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Orange), rect);
                    mybsh = new SolidBrush(Color.Orange);
                }
                else if (zhongyao == 8)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.OrangeRed), rect);
                    mybsh = new SolidBrush(Color.OrangeRed);
                }
                else if (zhongyao == 9)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Crimson), rect);
                    mybsh = new SolidBrush(Color.Crimson);
                }
                else if (zhongyao >= 10)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), rect);
                    mybsh = new SolidBrush(Color.Red);
                }
                if (e.Index == reminderList.SelectedIndex)
                {
                    e.Graphics.FillRectangle(mybsh, rect);
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), rectleft);
                }
                else
                {
                    e.Graphics.FillRectangle(mybsh, rect);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), rectleft);
                }
                if (searchword.Text.StartsWith("#"))
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                }
                else if (searchword.Text.StartsWith("*"))
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                }
                else if (!((MyListBoxItemRemind)reminderList.Items[e.Index]).isTask)
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text, e.Font, Brushes.Gray, e.Bounds, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(((MyListBoxItemRemind)reminderList.Items[e.Index]).Text.Substring(0, 3), e.Font, mybsh, rect, StringFormat.GenericDefault);
                    string taskname = ((MyListBoxItemRemind)reminderList.Items[e.Index]).Text.Substring(3);
                    if (taskname.Length > 100)
                    {
                        taskname = taskname.Substring(0, 100);
                    }
                    if (((MyListBoxItemRemind)reminderList.Items[e.Index]).link == null || ((MyListBoxItemRemind)reminderList.Items[e.Index]).link == "")
                    {
                        e.Graphics.DrawString(taskname, e.Font, Brushes.Gray, rectleft, StringFormat.GenericDefault);
                    }
                    else
                    {
                        e.Graphics.DrawString(taskname, e.Font, Brushes.DeepSkyBlue, rectleft, StringFormat.GenericDefault);
                    }

                    ((MyListBoxItemRemind)reminderList.Items[e.Index]).IsShow = true;
                    e = new DrawItemEventArgs(e.Graphics,
                                            e.Font,
                                            e.Bounds,
                                            e.Index,
                                            e.State,
                                            e.ForeColor,
                                            Color.White);
                    ////e.DrawFocusRectangle();
                }
            }
        }

        private void DrawList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(((MyListBoxItem)DrawList.SelectedItem).Value);
                //MyHide();
                searchword.Focus();
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadAll_Click(object sender, EventArgs e)
        {
            LoadDrawIO(rootpath);
        }

        private void DrawList_Click(object sender, EventArgs e)
        {
            //将选中文件的节点添加到reminderList中
            reminderList.Items.Clear();
            string path = ((MyListBoxItem)DrawList.SelectedItem).Value;
            if (path != null && path != "")
            {
                if (File.Exists(path))
                {
                    LoadReminder(path);
                }
            }
        }
    }
}

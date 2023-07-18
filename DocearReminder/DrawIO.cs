using EnvDTE;
using NPOI.POIFS.Properties;
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
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Xml;
using yixiaozi.Config;
using yixiaozi.Model.DocearReminder;
using yixiaozi.MyConvert;
using Color = System.Drawing.Color;
using Brushes = System.Drawing.Brushes;
using System.Text.RegularExpressions;
using Match = System.Text.RegularExpressions.Match;

namespace DocearReminder
{
    public partial class DrawIO : Form
    {
        public static IniFile ini = new IniFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
        public static List<string> unchkeckmindmap = new List<string>();
        public static string rootpath = "";
        public static List<FileInfo> drawioFileList = new List<FileInfo>();
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
            drawioFileList.Clear();
            foreach (FileInfo file in new DirectoryInfo(rootPath).GetFiles("*.drawio", SearchOption.AllDirectories))
            {
                DrawList.Items.Insert(0, new MyListBoxItem { Text = lenghtString(LoadReminderCount(file.FullName).ToString(), 2) + " " + Path.GetFileNameWithoutExtension(file.FullName), Value = file.FullName });
                drawioFileList.Add(file);
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
            DrawList.Sorted = false;
            DrawList.Sorted = true;
            DrawList.Refresh();
            reminderList.Sorted = false;
            reminderList.Sorted = true;
            reminderList.Refresh();
        }
        //添加一个方法，输入一个文件地址，用xml读取文件，将所有mxCell标签添加到reminderList中
        public void LoadReminder(string file)
        {
            reminderList.Items.Clear();
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(file);
            foreach (XmlNode node in x.GetElementsByTagName("mxCell"))
            {
                //如果node的parentnode的name属性不为空，获取name属性的值

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
                    //TaskDate="2024-07-15"
                    //获取TaskDate属性，并且转换为DateTime类型
                    if (node.Attributes["TaskDate"] != null)
                    {
                        item.time = Convert.ToDateTime(node.Attributes["TaskDate"].Value);
                    }
                    //如果没有TaskDate属性，获取父节点的TaskDate属性
                    else
                    {
                        if (node.ParentNode.Attributes["TaskDate"] != null)
                        {
                            try
                            {
                                item.time = Convert.ToDateTime(node.ParentNode.Attributes["TaskDate"].Value);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    //TaskLevel="5"
                    //获取TaskLevel属性，并且转换为int类型
                    if (node.Attributes["TaskLevel"] != null)
                    {
                        item.tasklevel = Convert.ToInt16(node.Attributes["TaskLevel"].Value);
                    }
                    //如果没有TaskLevel属性，获取父节点的TaskLevel属性
                    else
                    {
                        if (node.ParentNode.Attributes["TaskLevel"] != null)
                        {
                            try
                            {
                               item.tasklevel = Convert.ToInt16(node.ParentNode.Attributes["TaskLevel"].Value);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    //TaskTime="30"
                    //获取TaskTime属性，并且转换为int类型
                    if (node.Attributes["TaskTime"] != null)
                    {
                        item.tasktime = Convert.ToInt16(node.Attributes["TaskTime"].Value);
                    }
                    //如果没有TaskTime属性，获取父节点的TaskTime属性
                    else
                    {
                        if (node.ParentNode.Attributes["TaskTime"] != null)
                        {
                            try
                            {
                                item.tasktime = Convert.ToInt16(node.ParentNode.Attributes["TaskTime"].Value);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    //id="wNFkX_FCYwEscR9QWev_-1"
                    //获取id属性
                    if (node.Attributes["id"] != null)
                    {
                        item.ID = node.Attributes["id"].Value;
                    }
                    //如果没有id属性，获取父节点的id属性
                    else
                    {
                        if (node.ParentNode.Attributes["id"] != null)
                        {
                            item.ID = node.ParentNode.Attributes["id"].Value;
                        }
                    }


                    reminderList.Items.Add(new MyListBoxItemRemind
                    {
                        Text = item.time.ToString("   MM-dd") + "  " + item.name,
                        Name = item.name,
                        Time = item.time,
                        Value = file,
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

        public int LoadReminderCount(string file)
        {
            int result = 0;
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
                    result++;
                }
            }
            return result;
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
            string path = ((MyListBoxItem)DrawList.SelectedItem).Value;
            if (path != null && path != "")
            {
                if (File.Exists(path))
                {
                    LoadReminder(path);
                }
            }
        }

        private void reminderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            reminderList.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //设置当前节点的时间，时长，任务等级
            if (reminderList.SelectedIndex >= 0)
            {
                string path = ((MyListBoxItem)DrawList.SelectedItem).Value;
                string id = ((MyListBoxItemRemind)reminderList.SelectedItem).IDinXML;
                if (path != null && path != "")
                {

                    UpdateTask(path, id, searchword.Text, dateTimePicker.Value.ToString(), taskTime.Value.ToString(), tasklevel.Value.ToString());
                }
            }
        }
        //将button1_Click的内容封装成一个方法，用于更新
        //输入参数：path，其余默认为空
        //返回值：无
        public static void UpdateTask(string path,string id,string taskname = "", string taskdate = "", string tasktime = "", string tasklevel = "")
        {
            if (path != null && path != "")
            {
                if (File.Exists(path))
                {
                    System.Xml.XmlDocument x = new XmlDocument();
                    x.Load(path);
                    //x.GetElementById();
                    //获取当前任务的ID
                    if (id != null && id != "")
                    {
                        string[] tagNames = new string[] { "mxCell", "object" };
                        foreach (string tagname in tagNames)
                        {
                            //获取当前任务的节点
                            XmlNodeList xnl = x.GetElementsByTagName(tagname);
                            foreach (XmlNode xn in xnl)
                            {
                                if (xn.Attributes["id"] != null && xn.Attributes["id"].Value == id)
                                {
                                    //如果searchword.Text不为空，则设置label名称
                                    if (taskname != null && taskname != "")
                                    {
                                        //避免没有label属性
                                        if (xn.Attributes["label"] == null)
                                        {
                                            XmlAttribute xa = x.CreateAttribute("label");
                                            xa.Value = taskname;
                                            xn.Attributes.Append(xa);
                                        }
                                        else
                                        {
                                            xn.Attributes["label"].Value = taskname;
                                        }
                                    }
                                    //设置当前任务的时间
                                    if (taskdate != null && taskdate != "")
                                    {
                                        //避免没有TaskDate属性
                                        if (xn.Attributes["TaskDate"] == null)
                                        {
                                            XmlAttribute xa = x.CreateAttribute("TaskDate");
                                            xa.Value = taskdate;
                                            xn.Attributes.Append(xa);
                                        }
                                        else
                                        {
                                            xn.Attributes["TaskDate"].Value = taskdate;
                                        }
                                    }
                                    //设置当前任务的时长
                                    if (tasktime != null && tasktime != "")
                                    {
                                        //避免没有TaskLevel属性
                                        if (xn.Attributes["TaskTime"] == null)
                                        {
                                            XmlAttribute xa = x.CreateAttribute("TaskTime");
                                            xa.Value = tasktime;
                                            xn.Attributes.Append(xa);
                                        }
                                        else
                                        {
                                            xn.Attributes["TaskTime"].Value = tasktime;
                                        }
                                    }
                                    //设置当前任务的任务等级
                                    if (tasklevel != null && tasklevel != "")
                                    {
                                        //避免没有TaskLevel属性
                                        if (xn.Attributes["TaskLevel"] == null)
                                        {
                                            XmlAttribute xa = x.CreateAttribute("TaskLevel");
                                            xa.Value = tasklevel;
                                            xn.Attributes.Append(xa);
                                        }
                                        else
                                        {
                                            xn.Attributes["TaskLevel"].Value = tasklevel;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            //测试AddTask
            string path = ((MyListBoxItem)DrawList.SelectedItem).Value;
            if (path != null && path != "")
            {
                if (File.Exists(path))
                {
                    AddTask(path, searchword.Text, dateTimePicker.Value.ToString("yyyy-MM-dd HH:mm"), taskTime.Value.ToString(), tasklevel.Value.ToString());
                }
            }
            searchword.Text = "";
            //刷新reminderList
            if (path != null && path != "")
            {
                if (File.Exists(path))
                {
                    LoadReminder(path);
                }
            }
        }

        //将GetAddTaskPosition，AddNode结合起来，用于在待处理添加任务
        //输入参数：path，其余默认为空
        //返回值：无
        public static void AddTask(string path, string taskname = "", string taskdate = "", string tasktime = "", string tasklevel = "",string ID="")
        {
            string[] position = GetAddTaskPosition(path);
            AddNode(path, position[0], position[1], position[2], position[3], taskname, taskdate, tasktime, tasklevel, ID);
        }

        //将button2_Click里面的代码封装成一个方法，用于计算要添加任务的位置
        //输入参数：path
        //返回值：x,y,width,height
        public static string[] GetAddTaskPosition(string path)
        {
            int count = GetCount(path);
            string[] position = GetPosition(path);
            int x = int.Parse(position[0]);
            int y = int.Parse(position[1]);
            int width = int.Parse(position[2]);
            int height = int.Parse(position[3]);
            int x1 = x + 10;
            int y1 = y + 40 + 70 * count;
            string[] position1 = new string[] { x1.ToString(), y1.ToString(), "150", "60" };
            return position1;
        }

        private static string[] GetPosition(string path)
        {
            string[] position = new string[4];
            bool hasDefault = true;
            if (path != null && path != "")
            {
                if (File.Exists(path))
                {
                    System.Xml.XmlDocument x = new XmlDocument();
                    x.Load(path);
                    string[] tagNames = new string[] { "mxCell", "object" };
                    foreach (string tagname in tagNames)
                    {
                        //获取当前任务的节点
                        XmlNodeList xnl = x.GetElementsByTagName(tagname);
                        foreach (XmlNode node in xnl)
                        {
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
                            name = new HtmlToString().StripHTML(name);
                            //如果name=待处理，则获取x,y属性
                            if (name.Contains("待处理"))
                            {
                                if (node.ChildNodes[0].Attributes["x"] != null)
                                {
                                    position[0] = node.ChildNodes[0].Attributes["x"].Value;
                                }
                                if (node.ChildNodes[0].Attributes["y"] != null)
                                {
                                    position[1] = node.ChildNodes[0].Attributes["y"].Value;
                                }
                                else
                                {
                                    position[1] = "0";
                                }
                                //width
                                if (node.ChildNodes[0].Attributes["width"] != null)
                                {
                                    position[2] = node.ChildNodes[0].Attributes["width"].Value;
                                }
                                //height
                                //if (node.ChildNodes[0].Attributes["height"] != null)
                                //{
                                //    position[3] = node.ChildNodes[0].Attributes["height"].Value;
                                //}
                                position[3] = "1000";
                                hasDefault = false;
                                break;
                            }
                        }
                    }

                }
            }
            if (hasDefault)
            {
                //如果没有待处理的节点，则返回默认值
                //添加一个默认的待处理节点
                if (File.Exists(path))
                {
                    //x="-280" width="280" height="560" y="0"
                    AddNode(path, "-280", "0", "280", "560", "待处理");
                }
                return new string[] { "-280", "0", "280", "10000" };

            }
            else
            {
                return position;
            }
        }

        //通过GetPosition已获取了"待处理"的位置及大小，计算其他节点，所几个在其范围内
        //输入参数:path
        //返回值：个数
        private static int GetCount(string path)
        {
            int count = 0;
            string[] position = GetPosition(path);
            if (position != null && position.Length == 4)
            {
                if (File.Exists(path))
                {
                    System.Xml.XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(path);
                    string[] tagNames = new string[] { "mxGeometry" };
                    foreach (string tagname in tagNames)
                    {
                        //获取当前任务的节点
                        XmlNodeList xnl = xdoc.GetElementsByTagName(tagname);
                        foreach (XmlNode node in xnl)
                        {
                            double x = 0;
                            double y = 0;
                            if (node.Attributes["x"] != null)
                            {
                                x = Convert.ToDouble(node.Attributes["x"].Value);
                            }
                            if (node.Attributes["y"] != null)
                            {
                                y = Convert.ToDouble(node.Attributes["y"].Value);
                            }
                            //判断是否在position范围内
                            if (x > Convert.ToDouble(position[0]) && x < Convert.ToDouble(position[0]) + Convert.ToDouble(position[2]))
                            {
                                if (y > Convert.ToDouble(position[1]) && y < Convert.ToDouble(position[1]) + Convert.ToDouble(position[3]))
                                {
                                    count++;
                                }
                            }
                        }
                    }

                }
            }
            return count;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        //判断drawioFileList中有没有名称为某个名字的图
        //如果有返回文件地址，没有返回空
        //输入参数：name
        //返回值：path
        public static string GetPath(string name)
        {
            string path = "";
            if (drawioFileList != null && drawioFileList.Count > 0)
            {
                foreach (FileInfo file in drawioFileList)
                {
                    if (Path.GetFileNameWithoutExtension(file.FullName)==name)
                    {
                        path = file.FullName;
                        break;
                    }
                }
            }
            return path;
        }


        //将button3_Click里面的代码封装成一个方法
        //输入参数：path，x,y,width,height,label,TaskDate,taskTime,TaskLevel
        //返回值：无
        public static void AddNode(string path, string x, string y, string width, string height, string label, string TaskDate="", string taskTime="", string TaskLevel="",string ID="")
        {
            //template< object label = "Name" TaskDate = "" TaskLevel = "" TaskTime = "" id = "NxaE-IUndskCNaGzTjTz-4" >< mxCell style = "rounded=1;whiteSpace=wrap;html=1;" vertex = "1" parent = "1" >< mxGeometry x = "-300" width = "120" height = "60" as= "geometry" /></ mxCell ></ object >
            //上面是一个图形的模板
            //将上述模板转换成XmlNode对象
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml("<template><object label = \"Name123\" TaskDate = \"\" TaskLevel = \"\" TaskTime = \"\" id = \"\" ><mxCell style = \"rounded=1;whiteSpace=wrap;html=1;\" vertex = \"1\" parent = \"1\"><mxGeometry x = \"-300\" y = \"-300\" width = \"120\" height = \"60\" as= \"geometry\" /></mxCell></object></template>");
            XmlNode node = xdoc.SelectSingleNode("template/object");
            //设置id为新的guid
            node.Attributes["id"].Value = ID==""?Guid.NewGuid().ToString():ID;
            //设置label为textbox的值
            node.Attributes["label"].Value = label;
            //TaskDate
            node.Attributes["TaskDate"].Value = TaskDate;
            //taskTime
            node.Attributes["TaskTime"].Value = taskTime;
            //TaskLevel
            node.Attributes["TaskLevel"].Value = TaskLevel;

            //如果label是待处理，则则将mxCell的样式修改为style="rounded=0;whiteSpace=wrap;html=1;horizontal=1;verticalAlign=top;"
            if (label.Contains("待处理"))
            {
                node.ChildNodes[0].Attributes["style"].Value = "rounded=0;whiteSpace=wrap;html=1;horizontal=1;verticalAlign=top;";
            }

            //设置x,y,width,height
            node.ChildNodes[0].ChildNodes[0].Attributes["x"].Value = x;
            node.ChildNodes[0].ChildNodes[0].Attributes["y"].Value = y;
            node.ChildNodes[0].ChildNodes[0].Attributes["width"].Value = width;
            node.ChildNodes[0].ChildNodes[0].Attributes["height"].Value = height;

            if (!string.IsNullOrEmpty(path))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(path);
                //在root下面插入node
                XmlNode root = xmldoc.GetElementsByTagName("root")[0];
                root.AppendChild(xmldoc.ImportNode(node, true));
                xmldoc.Save(path);
            }
        }


        //添加一个方法判断source="ID"或者target="ID"是否在文档里
        //输入ID和文档URL两个参数
        //返回true或者false
        public static bool IsExist(string ID, string path)
        {
            bool result = false;
            if (File.Exists(path))
            {
                //读取文本到字符串
                string text = File.ReadAllText(path);
                //判断是否包含source="ID"或者target="ID"
                if (text.Contains("source=\"" + ID + "\"") || text.Contains("target=\"" + ID + "\""))
                {
                    result = true;
                }
            }
            return result;
        }
        //判断一个ID在文档里是否存在，如果存在则返回true，否则返回false
        //输入参数：ID,path
        //返回值：true或者false
        public static bool IDIsExist(string ID, string path)
        {
            bool result = false;
            if (File.Exists(path))
            {
                //读取文本到字符串
                string text = File.ReadAllText(path);
                if (text.Contains("id=\"" + ID + "\""))
                {
                    //判断一下ID出现了几次，如果超过两次，就只保留一次
                    int count = 0;
                    foreach (Match match in Regex.Matches(text, "id=\"" + ID + "\""))
                    {
                        count++;
                    }
                    if (count > 1)
                    {
                        //删除第二次及以后出现的节点
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(path);
                        string[] tagNames = new string[] { "mxCell", "object" };
                        foreach (string tagname in tagNames)
                        {
                            XmlNodeList xnl = xmldoc.GetElementsByTagName(tagname);
                            int index= 0;
                            for (int i = xnl.Count - 1; i >= 0; i--)
                            {                                 
                                if (xnl[i].Attributes["id"] != null && xnl[i].Attributes["id"].Value == ID)
                                {
                                    if (index == 0)
                                    {
                                        index++;
                                        continue;
                                    }
                                    xnl[i].ParentNode.RemoveChild(xnl[i]);
                                }
                            }

                        }
                        xmldoc.Save(path);
                    }
                    result = true;
                }
            }
            return result;
        }

        //删除ID为ID的节点
        //输入参数：ID,path
        //返回值：无
        public static void DeleteNode(string ID, string path)
        {
            if (File.Exists(path))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(path);
                string[] tagNames = new string[] { "mxCell","object" };
                foreach (string tagname in tagNames)
                {
                    XmlNodeList xnl = xmldoc.GetElementsByTagName(tagname);
                    foreach (XmlNode node in xnl)
                    {
                        if (node.Attributes["id"]!=null&&node.Attributes["id"].Value == ID)
                        {
                            node.ParentNode.RemoveChild(node);
                            break;
                        }
                    }
                }
                xmldoc.Save(path);
            }
        }

        private void DrawList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void reminderList_Click(object sender, EventArgs e)
        {
            //将当前选中转换成MyListBoxItemRemind，然后时间，等级，时间赋值到控件
            MyListBoxItemRemind item = (MyListBoxItemRemind)reminderList.SelectedItem;
            if (item != null)
            {
                try
                {

                    //设置时间
                    dateTimePicker.Value = item.Time;
                }
                catch (Exception)
                {
                    //设置时间
                    dateTimePicker.Value = DateTime.Now;
                }
                //设置等级
                taskTime.Value = item.rtaskTime;
                //设置时间
                tasklevel.Value = item.level;
            }

        }

        private void delete_Click(object sender, EventArgs e)
        {
            //删除当前节点
            MyListBoxItemRemind item = (MyListBoxItemRemind)reminderList.SelectedItem;
            string path = item.Value;
            if (item != null)
            {
                //删除节点
                DeleteNode(item.IDinXML, item.Value);
                //刷新列表
                LoadReminder(path);
            }
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开文件点击选项的文件
            if (DrawList.SelectedItem != null)
            {
                MyListBoxItem item = (MyListBoxItem)DrawList.SelectedItem;
                if (item != null)
                {
                    System.Diagnostics.Process.Start(item.Value);
                }
            }
        }

        private void 打开文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开文件夹
            if (DrawList.SelectedItem != null)
            {
                MyListBoxItem item = (MyListBoxItem)DrawList.SelectedItem;
                if (item != null)
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select," + item.Value);
                }
            }
        }

        private void DrawList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int posindex = DrawList.IndexFromPoint(new System.Drawing.Point(e.X, e.Y));
                DrawList.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < DrawList.Items.Count)
                {
                    DrawList.SelectedIndex = posindex;
                    DrawListMenu.Show(DrawList, new System.Drawing.Point(e.X, e.Y));
                }
            }
            DrawList.Refresh();
        }

        private void reminderList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int posindex = reminderList.IndexFromPoint(new System.Drawing.Point(e.X, e.Y));
                reminderList.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < reminderList.Items.Count)
                {
                    reminderList.SelectedIndex = posindex;
                    reminderListMenu.Show(reminderList, new System.Drawing.Point(e.X, e.Y));
                }
            }
            reminderList.Refresh();
        }

        private void 打开文件ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //打开文件夹
            if (reminderList.SelectedItem != null)
            {
                MyListBoxItemRemind item = (MyListBoxItemRemind)reminderList.SelectedItem;
                if (item != null)
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select," + item.Value);
                }
            }
        }

        private void 打开文件夹ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //打开文件夹
            if (reminderList.SelectedItem != null)
            {
                MyListBoxItemRemind item = (MyListBoxItemRemind)reminderList.SelectedItem;
                if (item != null)
                {
                    System.Diagnostics.Process.Start("explorer.exe", "/select," + item.Value);
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyListBoxItemRemind item = (MyListBoxItemRemind)reminderList.SelectedItem;
            string path = item.Value;
            if (item != null)
            {
                //删除节点
                DeleteNode(item.IDinXML, item.Value);
                //刷新列表
                LoadReminder(path);
            }
        }
    }
}

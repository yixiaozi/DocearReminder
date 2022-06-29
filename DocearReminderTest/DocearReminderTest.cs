using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DocearReminderTest
{
    public partial class DocearReminderTest : Form
    {
        public DocearReminderTest()
        {
            InitializeComponent();
        }

        private void b_FolderStructure_Click(object sender, EventArgs e)
        {
            string str1 = "node";
            string str2 = "TEXT";
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(@"E:\yixiaozi\有条不紊\电子设备\PC\华硕灵耀x.mm");
            List<string> contents = new List<string>();
            foreach (XmlNode node in x.GetElementsByTagName(str1))
            {
                if (node.Attributes[str2] == null || node.Attributes["ID"] == null)
                {
                    continue;
                }
                if (node.Attributes[str2].Value != "")
                {
                    string nodename = node.Attributes[str2].Value;//@"Folder|D|C:\下载";
                    if (nodename.StartsWith("Folder|D"))
                    {
                        DirectoryInfo path = new DirectoryInfo(nodename.Split('|')[2]);
                        foreach (FileInfo file in path.GetFiles("*.*", SearchOption.TopDirectoryOnly))
                        {
                            string md5= GetMD5HashFromFile(file.FullName);
                            if (!node.InnerXml.Contains(md5))
                            {
                               AddFileTaskToMap(@"E:\yixiaozi\有条不紊\电子设备\PC\华硕灵耀x.mm", nodename, file.Name, file.FullName, md5, file.CreationTime);
                            }
                        }
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(@"E:\yixiaozi\有条不紊\电子设备\PC\华硕灵耀x.mm"));
                        th.Start();
                    }
                    if (nodename.StartsWith("Folder|F"))
                    {
                        DirectoryInfo path = new DirectoryInfo(nodename.Split('|')[2]);
                        foreach (FileInfo file in path.GetFiles("*.*", SearchOption.AllDirectories))
                        {
                            string md5 = GetMD5HashFromFile(file.FullName);
                            if (!node.InnerXml.Contains(md5))
                            {
                                AddFileTaskToMap(@"E:\yixiaozi\有条不紊\电子设备\PC\华硕灵耀x.mm", nodename, file.Name, file.FullName, md5, file.CreationTime, file.FullName.Substring(path.FullName.Length));
                            }
                        }
                        Thread th = new Thread(() => yixiaozi.Model.DocearReminder.Helper.ConvertFile(@"E:\yixiaozi\有条不紊\电子设备\PC\华硕灵耀x.mm"));
                        th.Start();
                    }
                }
            }
        }
        public void AddFileTaskToMap(string mindmap, string rootNode, string taskName,string link,string md5,DateTime createDate,string path="")
        {
            if (taskName == "")
            {
                return;
            }
            System.Xml.XmlDocument x = new XmlDocument();
            x.Load(mindmap);
            XmlNode root = x.GetElementsByTagName("node").Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == rootNode);
            XmlNode day = null;
            if (path == "")
            {
                if (!haschildNode(root, createDate.Year.ToString()))
                {
                    XmlNode yearNode = x.CreateElement("node");
                    XmlAttribute yearNodeValue = x.CreateAttribute("TEXT");
                    yearNodeValue.Value = createDate.Year.ToString();
                    yearNode.Attributes.Append(yearNodeValue);
                    XmlAttribute yearNodeTASKID = x.CreateAttribute("ID"); yearNode.Attributes.Append(yearNodeTASKID); yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); root.AppendChild(yearNode);
                }
                XmlNode year = root.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == createDate.Year.ToString());
                if (!haschildNode(year, createDate.Month.ToString()))
                {
                    XmlNode monthNode = x.CreateElement("node");
                    XmlAttribute monthNodeValue = x.CreateAttribute("TEXT");
                    monthNodeValue.Value = createDate.Month.ToString();
                    monthNode.Attributes.Append(monthNodeValue);
                    XmlAttribute monthNodeTASKID = x.CreateAttribute("ID"); monthNode.Attributes.Append(monthNodeTASKID); monthNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); year.AppendChild(monthNode);
                }
                XmlNode month = year.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == createDate.Month.ToString());
                if (!haschildNode(month, createDate.Day.ToString()))
                {
                    XmlNode dayNode = x.CreateElement("node");
                    XmlAttribute dayNodeValue = x.CreateAttribute("TEXT");
                    dayNodeValue.Value = createDate.Day.ToString();
                    dayNode.Attributes.Append(dayNodeValue);
                    XmlAttribute dayNodeTASKID = x.CreateAttribute("ID"); dayNode.Attributes.Append(dayNodeTASKID); dayNode.Attributes["ID"].Value = Guid.NewGuid().ToString(); month.AppendChild(dayNode);
                }
                day = month.ChildNodes.Cast<XmlNode>().First(m => m.Attributes[0].Name == "TEXT" && m.Attributes["TEXT"].Value == createDate.Day.ToString());
            }
            else
            {
                day = addSubNodes(root,path);
            }
            XmlNode newNote = x.CreateElement("node");
            XmlAttribute newNotetext = x.CreateAttribute("TEXT");
            newNotetext.Value = taskName;
            XmlAttribute newNoteCREATED = x.CreateAttribute("CREATED");
            newNoteCREATED.Value = (Convert.ToInt64((createDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            XmlAttribute newNoteMODIFIED = x.CreateAttribute("MODIFIED");
            newNoteMODIFIED.Value = (Convert.ToInt64((createDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
            if (true)
            {
                XmlNode remindernode = x.CreateElement("hook");
                XmlAttribute remindernodeName = x.CreateAttribute("NAME");
                remindernodeName.Value = "plugins/TimeManagementReminder.xml";
                remindernode.Attributes.Append(remindernodeName);
                XmlNode remindernodeParameters = x.CreateElement("Parameters");
                XmlAttribute remindernodeTime = x.CreateAttribute("REMINDUSERAT");
                remindernodeTime.Value = (Convert.ToInt64((createDate - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds)).ToString();
                remindernodeParameters.Attributes.Append(remindernodeTime);
                remindernode.AppendChild(remindernodeParameters);
                newNote.AppendChild(remindernode);
            }

            XmlAttribute TASKLink = x.CreateAttribute("LINK");
            TASKLink.Value = link;
            newNote.Attributes.Append(TASKLink);

            XmlAttribute MD5 = x.CreateAttribute("MD5");
            MD5.Value = md5;
            newNote.Attributes.Append(MD5);

            newNote.Attributes.Append(newNotetext);
            newNote.Attributes.Append(newNoteCREATED);
            newNote.Attributes.Append(newNoteMODIFIED);
            XmlAttribute TASKID = x.CreateAttribute("ID");
            newNote.Attributes.Append(TASKID);
            newNote.Attributes["ID"].Value = Guid.NewGuid().ToString();
            day.AppendChild(newNote);
            x.Save(mindmap);
        }
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
            }
        }

        public bool haschildNode(XmlNode node, string child)
        {
            foreach (XmlNode item in node.ChildNodes.Cast<XmlNode>().Where(m => m.Name == "node"))
            {
                if (item.Attributes.Cast<XmlAttribute>().Any(m => m.Name == "TEXT"))
                {
                    if (item.Attributes["TEXT"].Value == child)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public XmlNode getchildNode(XmlNode node, string child)
        {
            foreach (XmlNode item in node.ChildNodes.Cast<XmlNode>().Where(m => m.Name == "node"))
            {
                if (item.Attributes.Cast<XmlAttribute>().Any(m => m.Name == "TEXT"))
                {
                    if (item.Attributes["TEXT"].Value == child)
                    {
                        return item;
                    }
                }
            }
            return node;
        }
        public XmlNode addSubNodes(XmlNode root,string path)
        {
            XmlNode subnode = root;
            foreach (string item in path.Split("\\"))
            {
                if(!item.Contains("."))
                {
                    if (!haschildNode(subnode, item))
                    {
                        XmlNode yearNode = subnode.OwnerDocument.CreateElement("node");
                        XmlAttribute yearNodeValue = subnode.OwnerDocument.CreateAttribute("TEXT");
                        yearNodeValue.Value = item;
                        yearNode.Attributes.Append(yearNodeValue);
                        XmlAttribute yearNodeTASKID = subnode.OwnerDocument.CreateAttribute("ID"); 
                        yearNode.Attributes.Append(yearNodeTASKID);
                        yearNode.Attributes["ID"].Value = Guid.NewGuid().ToString();
                        subnode.AppendChild(yearNode);
                        subnode = yearNode;
                    }
                    else
                    {
                        subnode=getchildNode(subnode, item);
                    }
                }
            }
            return subnode;
        }
    }
}

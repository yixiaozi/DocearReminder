using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Model.DocearReminder
{
    public class StationInfo
    {
        /// <summary>
        /// 站点名称 - 中文
        /// </summary>
        public string StationName_CN { get; set; }
        /// <summary>
        /// 站点名称 - 英文
        /// </summary>
        public string StationName_EN { get; set; }
        /// <summary>
        /// 站点名称 - 简写
        /// </summary>
        public string StationName_JX { get; set; }
        /// <summary>
        /// 站点的值
        /// </summary>
        public string StationValue { get; set; }
        public string isNode { get; set; }
        public string mindmapurl { get; set; }
        public string nodeID { get; set; }
        public string fatherNodePath { get; set; }
        public string link { get; set; }
        /// <summary>
        /// 模糊查询站点
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IList<StationInfo> StationData;
        public static IList<StationInfo> NodeData;
        public static IList<StationInfo> TimeBlockData;
        public static List<string> ignoreSuggest = new List<string>();
        public static string command = "ga;gc;";
        public static IList<StationInfo> GetStations(string filter)
        {
            IList<StationInfo> results = new List<StationInfo>();
            //if (DocearReminderForm.command.Contains(filter))
            //{
            //    return results;
            //}
            if (StationData == null)
            {
                string stations = GetAllStations();
                string[] datas = stations.Split('@');
                foreach (var item in datas)
                {
                    string[] tempArray = item.Split('|');
                    try
                    {
                        StationInfo info = new StationInfo()
                        {
                            StationName_CN = tempArray[0],
                            StationValue = tempArray[1],
                            StationName_EN = tempArray[2],
                            StationName_JX = tempArray[3]
                        };
                        results.Add(info);
                    }
                    catch (Exception)
                    {
                    }

                }
                StationData = results;
            }
            else
            {
                results = StationData;
            }
            return results.Where(
                f => (!ignoreSuggest.Contains(f.StationName_CN)) && (
                (f.StationName_CN.Length >= filter.Length && f.StationName_CN.Contains(filter)) ||
                (f.StationName_EN.Length >= filter.Length && f.StationName_EN.Contains(filter)) ||
                (f.StationName_JX.Length >= filter.Length && Search(f.StationName_JX, filter)) ||
                (f.StationValue.Length >= filter.Length && f.StationValue.Contains(filter)))).ToList<StationInfo>();
            //return results.Where(
            //    f =>
            //    (f.StationName_CN.Length >= filter.Length && Search(f.StationName_CN,filter) )||
            //    (f.StationName_EN.Length >= filter.Length && Search(f.StationName_EN, filter)) ||
            //    (f.StationName_JX.Length >= filter.Length && Search(f.StationName_JX, filter) )||
            //    (f.StationValue.Length >= filter.Length && Search(f.StationValue, filter))).ToList<StationInfo>();
        }

        public static IList<StationInfo> GetNodes(string filter)
        {
            IList<StationInfo> results = new List<StationInfo>();
            if (command.Contains(filter))
            {
                return results;
            }
            if (NodeData == null)
            {
                string stations = GetAllNodes();
                string[] datas = stations.Split('@');
                foreach (var item in datas)
                {
                    string[] tempArray = item.Split('|');
                    try
                    {
                        string fathernodepath = "";
                        try
                        {
                            fathernodepath =tempArray[7];
                        }
                        catch (Exception)
                        {
                        }
                        StationInfo info = new StationInfo()
                        {
                            StationName_CN = tempArray[0],
                            StationValue = tempArray[1],
                            StationName_EN = tempArray[2],
                            StationName_JX = tempArray[3],
                            isNode = tempArray[4],
                            mindmapurl = tempArray[6],
                            nodeID = tempArray[5],
                            link = tempArray[8],
                            fatherNodePath = fathernodepath
                        };
                        results.Add(info);
                    }
                    catch (Exception)
                    {
                    }
                }
                NodeData = results;
            }
            else
            {
                results = NodeData;
            }
            //return results.Where(
            //    f =>
            //    (f.StationName_CN.Length >= filter.Length && f.StationName_CN.Contains(filter)) ||
            //    (f.StationName_EN.Length >= filter.Length && f.StationName_EN.Substring(0, filter.Length) == filter) ||
            //    (f.StationName_JX.Length >= filter.Length && f.StationName_JX.Substring(0, filter.Length) == filter) ||
            //    (f.StationValue.Length >= filter.Length && f.StationValue.Substring(0, filter.Length) == filter)).ToList<StationInfo>();
            return results.Where(
                f => (!ignoreSuggest.Contains(f.StationName_CN)) && (
                (f.StationName_CN.Length >= filter.Length && f.StationName_CN.Contains(filter)) ||
                (f.StationName_EN.Length >= filter.Length && f.StationName_EN.Contains(filter)) ||
                (f.StationName_JX.Length >= filter.Length && Search(f.StationName_JX, filter)) ||
                (f.StationValue.Length >= filter.Length && f.StationValue.Contains(filter)))).ToList<StationInfo>();
            //return results.Where(
            //    f =>
            //    (f.StationName_CN.Length >= filter.Length && Search(f.StationName_CN, filter)) ||
            //    (f.StationName_EN.Length >= filter.Length && Search(f.StationName_EN, filter)) ||
            //    (f.StationName_JX.Length >= filter.Length && Search(f.StationName_JX, filter)) ||
            //    (f.StationValue.Length >= filter.Length && Search(f.StationValue, filter))).ToList<StationInfo>();
        }
        public static IList<StationInfo> GetTimeBlock(string filter)
        {
            IList<StationInfo> results = new List<StationInfo>();
            if (command.Contains(filter))
            {
                return results;
            }
            if (TimeBlockData == null)
            {
                string stations = GetTimeBlockstr();
                string[] datas = stations.Split('@');
                foreach (var item in datas)
                {
                    string[] tempArray = item.Split('|');
                    try
                    {
                        string fathernodepath = "";
                        try
                        {
                            fathernodepath = tempArray[7];
                        }
                        catch (Exception)
                        {
                        }
                        //修改bug | xgbug | xiugaibug | xgbug | true | ID_1693863157 | 编程     | DocearReminder | 编程 | DocearReminder | -205
                        //疫情    | yq    | yiqing    | yq    | true | ID_1614751649 | 事件类别 | 事件类别        | -10066330
                        StationInfo info = new StationInfo()
                        {
                            StationName_CN = tempArray[0],
                            StationValue = tempArray[1],
                            StationName_EN = tempArray[2],
                            StationName_JX = tempArray[3],
                            isNode = tempArray[4],
                            nodeID = tempArray[5],
                            mindmapurl = tempArray[6],
                            link = tempArray[8],
                            fatherNodePath = fathernodepath
                        };
                        results.Add(info);
                    }
                    catch (Exception)
                    {
                    }
                }
                TimeBlockData = results;
            }
            else
            {
                results = TimeBlockData;
            }
            return results.Where(
                f => (!ignoreSuggest.Contains(f.StationName_CN)) && (
                (f.StationName_CN.Length >= filter.Length && f.StationName_CN.Contains(filter)) ||
                (f.StationName_EN.Length >= filter.Length && f.StationName_EN.Contains(filter)) ||
                (f.StationName_JX.Length >= filter.Length && Search(f.StationName_JX, filter)) ||
                (f.StationValue.Length >= filter.Length && f.StationValue.Contains(filter)))).ToList<StationInfo>();
        }
        public static bool Search(string str, string filter)
        {
            char[] strArr = str.ToCharArray();
            char[] filterArr = filter.ToCharArray();
            int index = 0;
            bool result = true;
            for (int i = 0; i < filterArr.Length && result; i++)
            {
                bool has = false;
                for (int j = index; j < strArr.Length; j++)
                {
                    if (strArr[j] == filterArr[i])
                    {
                        index = j + 1;
                        has = true;
                        break;
                    }
                }
                if (!has)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 读取站点数据文件
        /// </summary>
        /// <returns></returns>
        public static string GetAllStations()
        {
            string stationStrs;
            try
            {
                FileStream fileStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory +@"mindmaps.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fileStream, UnicodeEncoding.GetEncoding("GB2312"));
                stationStrs = sr.ReadToEnd().TrimEnd('@');
                sr.Close();
                fileStream.Close();
                return stationStrs;
            }
            catch (IOException ex)
            {
                return "站点文件读取失败！";
            }
        }
        /// <summary>
        /// 读取站点数据文件
        /// </summary>
        /// <returns></returns>
        public static string GetAllNodes()
        {
            string stationStrs;
            try
            {
                FileStream fileStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory +@"allnodesicon.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fileStream, UnicodeEncoding.GetEncoding("GB2312"));
                stationStrs = sr.ReadToEnd().TrimEnd('@');
                sr.Close();
                fileStream.Close();
                return stationStrs;
            }
            catch (IOException ex)
            {
                return "站点文件读取失败！";
            }
        }
        public static string GetTimeBlockstr()
        {
            string stationStrs;
            try
            {
                FileStream fileStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + @"timeblock.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fileStream, UnicodeEncoding.GetEncoding("GB2312"));
                stationStrs = sr.ReadToEnd().TrimEnd('@');
                sr.Close();
                fileStream.Close();
                return stationStrs;
            }
            catch (IOException ex)
            {
                return "站点文件读取失败！";
            }
        }
    }
    //public class ColorCodedCheckedListBox : CheckedListBox
    //{
    //    protected override void OnDrawItem(DrawItemEventArgs e)
    //    {
    //        if (e.Index > 0 && ((DocearReminderForm.MyListBoxItem)this.Items[e.Index]).IsSpecial)
    //        {
    //            Graphics g = e.Graphics;
    //            g.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
    //            g.DrawString(((DocearReminderForm.MyListBoxItem)this.Items[e.Index]).Text, e.Font, new SolidBrush(Color.Red), e.Bounds);
    //            base.OnDrawItem(e);
    //        }
    //        else
    //        {
    //            base.OnDrawItem(e);
    //        }
    //    }
    //}
}

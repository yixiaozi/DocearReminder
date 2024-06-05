using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace yixiaozi.IOHelper
{
    class Disk
    {
        /// <summary> 获取U盘或移动硬盘等设备 </summary> 
        ///http://www.luofenming.com/show.aspx?id=ART2017122900001 
        public static List<string> Get_UsbDisk_List()
        {
            DriveInfo[] di = DriveInfo.GetDrives();//检索计算机上的所有逻辑驱动器的驱动器名称
            List<string> Div = new List<string>();
            foreach (DriveInfo d in di)
            {
                string DriveName = d.Name.ToUpper();
                if (d.DriveType == DriveType.Removable && !DriveName.Contains("A") && !DriveName.Contains("B"))
                {//获取U盘或移动硬盘等设备
                    Div.Add(d.Name);
                }
            }
            return Div;
        }
    }
}

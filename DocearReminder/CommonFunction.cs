using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocearReminder
{
    //输入一个名字，调用drawIO的GetPath方法，如果不为空，则返回。如果返回为空，则返回DocearReminderForm的drawioPath
    public static class CommonFunction
    {
        public static string GetPath(string name)
        {
            string path = DrawIO.GetPath(name);
            if (path != "")
            {
                return path;
            }
            else
            {
                return DocearReminderForm.drawioPath;
            }
        }
    }
}

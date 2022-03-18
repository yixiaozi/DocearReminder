using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using yixiaozi.Model.DocearReminder;

namespace DevpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo fi = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"UsedTimer.json");
            UsedTimer UsedTimer;
            using (StreamReader sw = fi.OpenText())
            {
                string s = sw.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                UsedTimer = serializer.Deserialize<UsedTimer>(s);
            }
            List<double> names = new List<double>();
            List<double> values = new List<double>();

            foreach (var item in UsedTimer.TimeLog.GroupBy(m => Convert.ToDateTime(m.startDate.ToString("yyyy/MM/dd HH:00:00"))).OrderBy(m=>m.Key))
            {
                names.Add(item.Key.ToOADate());
                double timeCount = 0;
                foreach (OneTime oneTime in item)
                {
                    try
                    {
                        timeCount += ((oneTime.endDate - oneTime.startDate).TotalSeconds - oneTime.leaveSecound);
                    }
                    catch (Exception)
                    {
                    }
                    
                }
                values.Add(timeCount);
            }
        }

    }
}
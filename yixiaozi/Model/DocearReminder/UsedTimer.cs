using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Model.DocearReminder
{
    public class UsedTimer
    {
        public UsedTimer()
        {
            TimeLog = new List<OneTime>();
        }
        public TimeSpan AllTime {
            get {
                TimeSpan dts = new TimeSpan();
                if (TimeLog==null)
                {
                    return dts;
                }
                foreach (OneTime item in TimeLog)
                {
                    try//避免结束时间为空的问题
                    {
                        if (item.endDate!=null)
                        {
                            TimeSpan newdts = item.endDate - item.startDate;
                            newdts.Add(new TimeSpan(0,0,0-item.leaveSecound));
                            dts =dts.Add(newdts);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return dts;
            }
        }
        public TimeSpan todayTime { get {
                TimeSpan _todayTime = new TimeSpan();
                if (TimeLog == null)
                {
                    return _todayTime;
                }
                foreach (OneTime item in TimeLog)
                {
                    try//避免结束时间为空的问题
                    {
                        if (item.endDate != null)
                        {
                            if (item.startDate >= DateTime.Today.AddHours(-8))
                            {
                                TimeSpan newdts = item.endDate - item.startDate;
                                newdts.Add(new TimeSpan(0,0,0-item.leaveSecound));
                                _todayTime = _todayTime.Add(newdts);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return _todayTime;
            } }
        public List<OneTime> TimeLog { get; set; }
        public Int64 Count
        {
            get
            {
                if (TimeLog==null)
                {
                    return 0;
                }
                return TimeLog.Count;
            }
        }
        public void SetEndDate(Guid id,int leaveSecond=0) {
            if (leaveSecond>72000)//避免有时候太大造成的问题
            {
                leaveSecond = 0;
            }
            if (TimeLog==null||TimeLog.Count==0)
            {
                return;
            }
            OneTime log= TimeLog.FirstOrDefault(m => m.ID == id);
            if (log!=null)
            {
                if ((DateTime.Now-log.startDate).TotalSeconds+1<leaveSecond)
                {
                    leaveSecond = 0;
                }
                log.endDate = DateTime.Now;
                log.leaveSecound = leaveSecond;
            }
        }
        //以后不用这个了
        //public void NewOneTime(Guid id) {
        //    if (TimeLog.Count(m=>m.ID==id)==0)
        //    {
        //        TimeLog.Add(new OneTime { ID = id, startDate = DateTime.Now ,endDate= DateTime.Now, Log="",leaveSecound=0});
        //    }
        //}
        public void NewOneTime(Guid id,string section,string file,string log,string formname)
        {
            if (TimeLog.Count(m => m.ID == id) == 0)
            {
                TimeLog.Add(new OneTime { ID = id, startDate = DateTime.Now, endDate = DateTime.Now, Log = log ,leaveSecound = 0,section=section,fileFullName=file,formName=formname });
            }
        }
    }
    public class OneTime
    {
        public Guid ID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set;}
        public string Log { get; set; }//存储期间的日志
        public int leaveSecound { get; set; }
        public string section { get; set; }//区域
        public string fileFullName { get; set; }//记录全部名就可以了，文件名再处理，这样就能筛选某个文件夹的处理时间
        public string formName { get; set; }//窗口名这样就可以记录每个创建的激活时间了
    }
}

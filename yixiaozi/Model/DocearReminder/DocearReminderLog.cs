using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Model.DocearReminder
{
    public class DocearReminderLog
    {
    }

    public class LogItem
    {
        public Guid ID { get; set; }
        //打开程序
        //打开文件
        //完成任务
        //取消任务
        //推迟任务
        //修改节点名称
        //添加节点
        //添加任务
        //添加子节点
        //结束周期任务
        //删除节点
        //删除文件
        //设置为任务
        //修改了任务
        //关闭程序
        public string Type { get; set; }
        
        public DateTime Time { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string Title { get; set; }
        public string TitleTo { get; set; }
        public string Log { get; set; }
        public string MindmapFullName { get; set; }
    }
}
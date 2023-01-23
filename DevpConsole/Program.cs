using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Todoist.Net;
using Todoist.Net.Models;

namespace DevpConsole
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            ITodoistClient client = new TodoistClient("");
            var quickAddItem = new QuickAddItem("新建任务" + "");
            //var task = await client.Items.QuickAddAsync(quickAddItem);
            var project =client.Projects.GetAsync();
        }
    }
}
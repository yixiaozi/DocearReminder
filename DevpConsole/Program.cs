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
            List<keyRecord> result = new List<keyRecord>();
            foreach (string item in System.IO.Directory.GetFiles(@"E:\yixiaozi\.files\DocearReminder\2022", "key.txt",SearchOption.AllDirectories))
            {
                System.IO.File.Delete(item.Replace(".txt", "1.txt"));
                System.IO.File.Copy(item, item.Replace(".txt", "1.txt"));
                FileInfo file = new FileInfo(item.Replace(".txt", "1.txt"));
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(item.Replace(".txt", "1.txt")))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                    {
                        String line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            DateTime dt=DateTime.Now.AddDays(1100);
                            string key = "";
                            try
                            {
                                dt = Convert.ToDateTime(line.Substring(0,17));
                                key = line.Substring(18);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    dt = Convert.ToDateTime(line.Substring(0,16));
                                    key = line.Substring(17);
                                }
                                catch (Exception)
                                {
                                }
                            }
                            result.Add(new keyRecord
                            {
                                time = dt,
                                Keys = key
                            }); ;
                        }
                    }
                }
                System.IO.File.Delete(item.Replace(".txt", "1.txt"));
            }
            List<double> names = new List<double>();
            List<double> values = new List<double>();
            foreach (keyRecord item in result.OrderBy(m => m.time))
            {
                names.Add(item.time.ToOADate());
                values.Add(item.Keys.Length);
            }
            string keyorder = "A;B;C;D;E;F;G;H;I;J;K;L;M;N;O;P;Q;R;S;T;U;V;W;X;Y;Z;LControlKey;LShiftKey;Space;Return";
            List<string> keyorderArr = keyorder.Split(';').ToList();
            int[] keynumbers = new int[300];
            foreach (keyRecord item in result)
            {
                foreach (string key in item.Keys.Split(';'))
                {
                    if (key!="")
                    {
                        if (keyorderArr.Contains(key))
                        {
                        }
                        else
                        {
                            keyorderArr.Add(key);
                        }
                        keynumbers[getindex(keyorderArr, key)] += 1;
                    }
                }
            }
            int[] keynum = new int[keyorderArr.Count];
            int[] position= new int[keyorderArr.Count];
            for (int i = 0; i < keyorderArr.Count; i++)
            {
                keynum[i] = keynumbers[i];
                position[i] = i;
            }


        }
        public class keyRecord
        {
            public DateTime time { get; set; }
            public string Keys { get; set; }
            public List<string> keyList
            {
                get
                {
                    List<string> list = new List<string>();
                    list.AddRange(Keys.Split());
                    return list;
                }
            }
        }
        public static int getindex(List<string> arr,string str)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i]==str)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
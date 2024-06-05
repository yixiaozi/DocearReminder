using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace yixiaozi.MyConvert
{
    public class TextListConverter
    {
        //https://blog.csdn.net/wyjun7450088/article/details/42834923
        //测试代码: 
        //List<string> list = ReadTextFileToList(@"C:\topics.txt"); //记取字符串 
        //WriteListToTextFile(list,  @"c:\new.txt" );  //测试生成新的Txt文件 
        //读取文本文件转换为List 
        public List<string> ReadTextFileToList(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            List<string> list = new List<string>();
            StreamReader sr = new StreamReader(fs);
            //使用StreamReader类来读取文件 
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            // 从数据流中读取每一行，直到文件的最后一行 
            string tmp = sr.ReadLine();
            while (tmp != null)
            {
                list.Add(tmp);
                tmp = sr.ReadLine();
            }
            //关闭此StreamReader对象 
            sr.Close();
            fs.Close();
            return list;
        }
        //将List转换为TXT文件 
        public void WriteListToTextFile(List<string> list, string txtFile)
        {
            //创建一个文件流，用以写入或者创建一个StreamWriter 
            FileStream fs = new FileStream(txtFile, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Flush();
            // 使用StreamWriter来往文件中写入内容 
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            sw.BaseStream.SetLength(0);
            //添加去重
            List<string> xnodesRemoveSame = new List<string>();
            foreach (string item in list)
            {
                if (!xnodesRemoveSame.Contains(item))
                {
                    xnodesRemoveSame.Add(item);
                }
            }
            list = xnodesRemoveSame;

            for (int i = 0; i < list.Count; i++) sw.WriteLine(list[i]);
            //关闭此文件 
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}

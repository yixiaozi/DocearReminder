using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace yixiaozi.Model.DocearReminder
{
    public class Helper
    {
        public static void ConvertFile(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                string text = "";
                using (StreamReader textStream = file.OpenText())
                {
                    text = textStream.ReadToEnd();
                }
                text = text.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n", "");
                text = ConvertString(text);
                try
                {
                    File.WriteAllText(path, text);
                }
                catch (Exception)
                {
                    //MessageBox.Show("Don't be quickly");
                }
            }
            catch (Exception)
            {
            }
        }
        public static string ConvertString(string str)
        {
            IEnumerable<string> col = Regex.Matches(str, @"[\u4e00-\u9fbb|\u3002\uff1b\uff0c\uff01\uff1a\u201c\u201d\uff08\uff09\u3001\u300c\uff1f\u300a\u300b\u300d\u300e\u300f\u2018\u2019\u3014\u3015\u3010\u3011\u2014\u2026\u2013\uff0e\u3008\u3009]").OfType<Match>().Select(m => m.Groups[0].Value).Distinct();
            foreach (string item in col)
            {
                str = str.Replace(item, Fallback(item[0]));
            }
            return str;
        }
        public static string Fallback(char charUnknown)
        {
            string d = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[]
                {
                    (int)charUnknown
                });
            return d;
        }
    }
}

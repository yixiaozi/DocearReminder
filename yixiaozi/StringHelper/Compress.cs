using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.StringHelper
{
    class Compress
    {
        private static char[] base64CodeArray = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4',  '5', '6', '7', '8', '9', '+', '/', '='
        };

        /// <summary>
        /// 是否base64字符串
        /// </summary>
        /// <param name="base64Str">要判断的字符串</param>
        /// <param name="bytes">字符串转换成的字节数组</param>
        /// <returns></returns>
        public static bool IsBase64(string base64Str)
        {
            if (string.IsNullOrEmpty(base64Str))
                return false;
            else
            {
                if (base64Str.Contains(","))
                    base64Str = base64Str.Split(',')[1];
                if (base64Str.Length % 4 != 0)
                    return false;
                if (base64Str.Any(c => !base64CodeArray.Contains(c)))
                    return false;
            }
            try
            {
                var bytes = Convert.FromBase64String(base64Str);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩成base64格式
        /// </summary>
        /// <param name="content">原文</param>
        /// <returns></returns>
        public static string CompressToBase64(string content)
        {
            try
            {
                if (!string.IsNullOrEmpty(content))
                {
                    content = content.Trim();
                    if (content.Length > 0)
                    {
                        content = LZStringCSharp.LZString.CompressToBase64(content);
                    }
                }
            }
            catch (Exception ex)
            { }
            return content;
        }

        /// <summary>
        /// 从base64解压数据
        /// </summary>
        /// <param name="content">密文</param>
        /// <returns></returns>
        public static string DecompressFromBase64(string content)
        {
            try
            {
                if (!string.IsNullOrEmpty(content))
                {
                    content = content.Trim();
                    if (content.Length > 0)
                    {
                        if (IsBase64(content))
                        {
                            content = LZStringCSharp.LZString.DecompressFromBase64(content);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return content;
        }
    }
}

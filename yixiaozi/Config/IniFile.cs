﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Config
{
    public class IniFile
    {
        private string m_FileName;

        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileInt(
            string lpAppName,
            string lpKeyName,
            int nDefault,
            string lpFileName
            );

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName
            );

        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName
            );
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, Byte[] retVal, int size, string filePath);
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aFileName">Ini文件路径</param>
        public IniFile(string aFileName)
        {
            this.m_FileName = aFileName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IniFile()
        { }

        /// <summary>
        /// [扩展]读Int数值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public int ReadInt(string section, string name, int def)
        {
            return GetPrivateProfileInt(section, name, def, this.m_FileName);
        }

        /// <summary>
        /// [扩展]读取string字符串
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public string ReadString(string section, string name, string def)
        {
            return ReadStringDefault(section,name,def);
            //do not need any more;
            //StringBuilder vRetSb = new StringBuilder(2048);
            //GetPrivateProfileString(section, name, def, vRetSb, 2048, this.m_FileName);
            //string result = vRetSb.ToString();
            //if (result.Contains(@":\"))
            //{
            //    if (result[0] != System.AppDomain.CurrentDomain.BaseDirectory[0])
            //    {
            //        result = System.AppDomain.CurrentDomain.BaseDirectory[0] + result.Substring(1);
            //    }
            //}
            //return result;
        }
        /// <summary>
        /// [扩展]读取string字符串
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public string ReadStringDefault(string section, string name, string def)
        {
            StringBuilder vRetSb = new StringBuilder(2048);
            GetPrivateProfileString(section, name, def, vRetSb, 2048, this.m_FileName);
            string result = vRetSb.ToString();
            return result;
        }

        /// <summary>
        /// [扩展]写入Int数值，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="Ival">写入值</param>
        public void WriteInt(string section, string name, int Ival)
        {

            WritePrivateProfileString(section, name, Ival.ToString(), this.m_FileName);
        }

        /// <summary>
        /// [扩展]写入String字符串，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="name">键</param>
        /// <param name="strVal">写入值</param>
        public void WriteString(string section, string name, string strVal)
        {
            WritePrivateProfileString(section, name, strVal, this.m_FileName);
        }

        /// <summary>
        /// 删除指定的 节
        /// </summary>
        /// <param name="section"></param>
        public void DeleteSection(string section)
        {
            WritePrivateProfileString(section, null, null, this.m_FileName);
        }

        /// <summary>
        /// 删除全部 节
        /// </summary>
        public void DeleteAllSection()
        {
            WritePrivateProfileString(null, null, null, this.m_FileName);
        }

        /// <summary>
        /// 读取指定 节-键 的值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string IniReadValue(string section, string name)
        {
            StringBuilder strSb = new StringBuilder(256);
            GetPrivateProfileString(section, name, "", strSb, 256, this.m_FileName);
            return strSb.ToString();
        }

        /// <summary>
        /// 写入指定值，如果不存在 节-键，则会自动创建
        /// </summary>
        /// <param name="section"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void IniWriteValue(string section, string name, string value)
        {
            WritePrivateProfileString(section, name, value, this.m_FileName);
        }

        public List<string> ReadSections()
        {
            return ReadSections(this.m_FileName);
        }

        public List<string> ReadSections(string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        public List<string> ReadKeys(String SectionName)
        {
            return ReadKeys(SectionName, this.m_FileName);
        }

        public List<string> ReadKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }
        
    }
}

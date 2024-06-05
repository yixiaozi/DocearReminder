using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace yixiaozi.Config
{
    class AppConfigOpc
    {
        #region   基础参数
        private System.Configuration.Configuration _config = null;

        #endregion

        public AppConfigOpc()
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 添加键和对应值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="value">键的值</param>
        /// <returns>返回结果（true:表示成功）</returns>
        public bool AddAppSetting(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) return false;

            _config.AppSettings.Settings.Remove(key);
            _config.AppSettings.Settings.Add(key, value);
            _config.Save();

            return true;
        }

        /// <summary>
        /// 删除键和对应的值
        /// </summary>
        /// <param name="key">键的名称</param>
        /// <returns>返回结果（true:表示成功）</returns>
        public bool DelAppSetting(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;

            _config.AppSettings.Settings.Remove(key);
            _config.Save();
            return true;
        }

        /// <summary>
        /// 获取到键对应的值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns>返回键对应的值</returns>
        public string GetAppSetting(string key)
        {
            return _config.AppSettings.Settings[key].Value;
        }

        /// <summary>
        /// 修改键和对应的值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="value">键的值</param>
        /// <returns>返回结果（true:表示成功）</returns>
        public bool UpdateAppSetting(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return false;

            _config.AppSettings.Settings.Remove(key);
            _config.AppSettings.Settings.Add(key, value);

            _config.Save();

            return true;
        }


        /// <summary>
        /// 获取到AppSettings下的所有配置信息
        /// </summary>
        /// <returns>返回App.config下的appSettings下的所有配置信息</returns>
        public KeyValueConfigurationCollection GetAllAppSettings()
        {
            return _config.AppSettings.Settings;

        }
        //————————————————
        //版权声明：本文为CSDN博主「牛奶咖啡13」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
        //原文链接：https://blog.csdn.net/xiaochenXIHUA/article/details/118297134
        //1-引用命名空间：using Comon;

        ////2-实例化App.config文件操作类
        //AppConfigOpc appConfigOpc = new AppConfigOpc();

        ////3-具体的操作

        ////3.1、添加配置
        //bool success = appConfigOpc.AddAppSetting("制绒设备01", "192.168.3.25");

        ////3.2、删除配置
        //bool success = appConfigOpc.DelAppSetting("制绒设备01");

        ////3.3、更新配置
        //bool success = appConfigOpc.UpdateAppSetting("制绒设备01", "172,3.5.68");

        ////3.4、查看配置
        ////查看指定键的值
        //string str = appConfigOpc.GetAppSetting("制绒设备01");

        ////查看所有的配置文件信息
        //KeyValueConfigurationCollection keyValueConfigurationCollection = appConfigOpc.GetAllAppSettings();

        //foreach (KeyValueConfigurationElement item in keyValueConfigurationCollection)
        //{
        //   str += item.Key +":"+ item.Value;
        //}
    }
}

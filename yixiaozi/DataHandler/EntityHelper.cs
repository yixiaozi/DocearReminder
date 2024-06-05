using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.DataHandler
{
    class EntityHelper
    {
        /// <summary>
        /// 获取到类中的所有属性和对应值
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">类实体</param>
        /// <returns>返回类中的所有属性和对应值</returns>
        public static string GetAllProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1},", name, value);
                }
                else
                {
                    GetAllProperties(value);
                }
            }
            return tStr;
        }
    }
}

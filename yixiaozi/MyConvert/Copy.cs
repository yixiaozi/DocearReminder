using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.MyConvert
{
    class Copy
    {
        /// <summary>
        /// 深层拷贝
        /// </summary>
        /// https://blog.csdn.net/weixin_30512089/article/details/97523047
        /// <param name="_object"></param>
        /// <returns></returns>
        public static object DeepCopy(object _object)
        {
            Type T = _object.GetType();
            object o = Activator.CreateInstance(T);
            PropertyInfo[] PI = T.GetProperties();
            for (int i = 0; i < PI.Length; i++)
            {
                PropertyInfo p = PI[i];
                p.SetValue(o, p.GetValue(_object));
            }
            return o;
        }
        
    }
}

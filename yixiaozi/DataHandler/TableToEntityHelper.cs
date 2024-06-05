using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace yixiaozi.DataHandler
{
    class TableToEntityHelper
    {
        /// <summary>
        /// DataTable指定行数据转化为实体类
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dataTable">dataTable</param>
        /// <param name="rowIndex">需要解析行的索引</param>
        /// <returns>返回当前指定行的实体类数据</returns>
        public static T DataTableToEntity<T>(DataTable dataTable, int rowIndex) where T : new()
        {
            try
            {
                if (dataTable == null || dataTable.Rows.Count <= 0 || rowIndex < 0)
                {
                    return default(T);
                }

                //实例化实体类
                T t = new T();
                // 获取指定行数据
                DataRow dr = dataTable.Rows[rowIndex];
                // 获取所有列
                DataColumnCollection columns = dataTable.Columns;

                // 获得实体类的所有公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    string name = pi.Name;
                    // 检查DataTable是否包含此列    
                    if (columns.Contains(name))
                    {
                        if (!pi.CanWrite) continue;

                        object value = dr[name];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                return t;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// DataTable所有数据转换成实体类列表
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns>返回实体类列表</returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return new List<T>();
                }

                // 实例化实体类和列表

                List<T> list = new List<T>();

                // 获取所有列
                DataColumnCollection columns = dt.Columns;


                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    // 获得实体类的所有公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();

                    //循环比对且赋值
                    foreach (PropertyInfo p in propertys)
                    {
                        string name = p.Name;
                        // 检查DataTable是否包含此列    
                        if (columns.Contains(name))
                        {
                            if (!p.CanWrite) continue;

                            object value = dr[name];

                            if (value != DBNull.Value)
                            {
                                //if (value is int || value is float || value is decimal || value is double)
                                //{
                                //    p.SetValue(t, value.ToString(), null);
                                //}
                                //else
                                //{
                                //    p.SetValue(t, value, null);
                                //}

                                p.SetValue(t, value, null);
                            }
                        }
                    }
                    list.Add(t);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 实体类列表转换成DataTable
        /// </summary>
        /// <param name="entityList">实体类列表</param>
        /// <param name="excludeFields">排除字段</param>
        /// <returns>返回实体类列表对应的DataTable</returns>
        public static DataTable ListToDataTable<T>(List<T> entityList, List<string> excludeFields = null)
        {
            var countExclude = 0;
            if (excludeFields != null)
                countExclude = excludeFields.Count();

            //检查实体集合不能为空
            if (entityList == null || entityList.Count <= 0)
            {
                throw new Exception("需转换的集合为空");
            }

            //取出第一个实体的所有属性
            Type entityType = entityList[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();



            //实例化DataTable
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                Type colType = entityProperties[i].PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                //排除列
                string fieldName = entityProperties[i].Name;
                if (excludeFields == null || !excludeFields.Contains(fieldName))
                {
                    dt.Columns.Add(fieldName, colType);
                }
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entityList)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[dt.Columns.Count];
                int icount = 0;
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    //排除列
                    string fieldName = entityProperties[i].Name;
                    if (excludeFields == null || !excludeFields.Contains(fieldName))
                    {
                        entityValues[i - icount] = entityProperties[i].GetValue(entity, null);
                    }
                    else
                    {
                        icount++;
                    }
                }
                dt.Rows.Add(entityValues);
            }

            return dt;
        }
        //————————————————
        //版权声明：本文为CSDN博主「牛奶咖啡13」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
        //原文链接：https://blog.csdn.net/xiaochenXIHUA/article/details/118305939
    }
}

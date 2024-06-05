using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace yixiaozi.MyConvert
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 返回Object类型的安全状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SafeToString(this object obj, bool ishandle = false)
        {
            return obj == null ? String.Empty : obj.ToString();
        }

        public static string GetRateStr(this object obj, bool needextendrate = true)
        {
            if (obj == null)
            {
                if (needextendrate)
                {
                    return string.Empty;
                }
                else
                {
                    return "0%";
                }
            }
            else
            {
                if (obj.ToString().Contains("%"))
                {
                    if (needextendrate)
                    {
                        return obj.ToString().TrimEnd('%');
                    }
                    else
                    {
                        return obj.ToString();
                    }
                }
                else
                {
                    if (needextendrate)
                    {
                        return obj.ToString();
                    }
                    else
                    {
                        return obj.ToString() + "%";
                    }
                }
            }
        }

        /// <summary>
        /// 返回Object类型的安全状态
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string SafeToString(this object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }

        public static string SafeToString(this object obj, int defaultValue)
        {

            if (string.IsNullOrEmpty(obj.SafeToString()))
            {
                return defaultValue.ToString();
            }
            return obj.SafeToString();
        }



        public static string SafeToDataTime(this object obj)
        {
            if (obj is DateTime)
                return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");

            return "";
        }

        public static string SafeToDataTime1(this object obj)
        {
            if (obj is DateTime)
                return Convert.ToDateTime(obj).ToString("yyyyMMdd");

            return "";
        }

        public static string SafeSplitBy(this object obj)
        {
            string result = string.Empty;
            if (obj.SafeToString().Contains(";"))
            {
                string[] arrs = obj.SafeToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrs.Length == 2)
                {
                    result = arrs[1];
                }
            }
            else
            {
                result = obj.SafeToString();
            }
            return result;
        }
        public static string SafeSplitBy(this object obj, string[] splitStrs, int count)
        {
            string result = string.Empty;
            string[] arrs = obj.SafeToString().Split(splitStrs, StringSplitOptions.RemoveEmptyEntries);
            if (arrs.Length > count)
            {
                return arrs[count];
            }
            else
            {
                return obj.SafeToString();
            }
        }
        public static string SafeContractSplitBy(this object obj, string[] splitStrs)
        {
            string result = string.Empty;
            string[] arrs = obj.SafeToString().Split(splitStrs, StringSplitOptions.None);
            if (arrs.Length > 0)
            {
                return arrs[arrs.Length - 1];
            }
            else
            {
                return obj.SafeToString();
            }
        }
        public static int SafeLookUp0(this object obj)
        {
            int result = 0;
            if (obj.SafeToString().Contains(";") && obj.SafeToString().Contains("#"))
            {
                string[] arrs = obj.SafeToString().Split(new string[] { ";", "#" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrs.Length == 2)
                {
                    bool b = int.TryParse(arrs[0], out result);
                }
            }
            return result;
        }

        public static string SafeLookUp1(this object obj)
        {
            string result = string.Empty;
            if (obj.SafeToString().Contains(";") && obj.SafeToString().Contains("#"))
            {
                string[] arrs = obj.SafeToString().Split(new string[] { ";", "#" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrs.Length == 2)
                {
                    result = arrs[1];
                }
            }
            else
            {
                result = obj.SafeToString();
            }
            return result;
        }

        /// <summary>
        /// 截取一定长度的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SafeLengthToString(this object obj, int length)
        {
            if (obj.SafeToString().Length <= length)
            {
                return obj.SafeToString();
            }
            else
            {
                return obj.SafeToString().Substring(0, 8) + "...";
            }
        }

        public static string SafeToXml(this object obj)
        {
            string result = string.Empty;
            try
            {
                if (obj.SafeToString().Contains("<div>"))
                {
                    result = XElement.Parse(obj.SafeToString()).Value;
                }
                else
                {
                    result = obj.SafeToString();
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        public static int SafeToLookupID(this object obj)
        {
            int result = 0;
            try
            {
                FieldLookupValue flvobj = obj as FieldLookupValue;
                result = flvobj.LookupId;
            }
            catch (Exception)
            {

            }
            return result;
        }

        public static string SafeToLookupValue(this object obj)
        {
            string result = string.Empty;
            try
            {
                FieldLookupValue flvobj = obj as FieldLookupValue;
                result = flvobj.LookupValue;
            }
            catch (Exception)
            {

            }
            return result;
        }

      

        public static bool ConditionalVerification(this object obj, ListItem applyitem)
        {
            string Conditions = obj.ToString();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Conditions);
            XmlNodeList nodes = xmlDoc.SelectSingleNode("Where").ChildNodes;
            if (nodes.Count > 0)
            {
                return RecursionCondition(nodes[0], applyitem);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 递归caml语句
        /// </summary>
        /// <param name="node">caml节点</param>
        /// <param name="applyitem">申请单条目</param>
        /// <returns></returns>
        private static bool RecursionCondition(XmlNode node, ListItem applyitem)
        {
            string nodename = node.Name.ToLower();
            switch (nodename)
            {
                case "and":
                    return RecursionCondition(node.ChildNodes[0], applyitem) && RecursionCondition(node.ChildNodes[1], applyitem);
                case "or":
                    return RecursionCondition(node.ChildNodes[0], applyitem) || RecursionCondition(node.ChildNodes[1], applyitem);
                default:
                    return CheckFieldCAML(node, applyitem);
            }
        }

        /// <summary>
        /// 判断caml节点结果
        /// </summary>
        /// <param name="node">caml节点</param>
        /// <param name="applyitem">申请单条目</param>
        /// <returns></returns>
        private static bool CheckFieldCAML(XmlNode node, ListItem applyitem)
        {
            string nodename = node.Name.ToLower();
            string fieldname = "";
            string conditioinvalue = "";
            string fieldvalue = "";
            string fieldtype = "";
            if (node.ChildNodes.Count == 1)
            {
                fieldname = node.ChildNodes[0].Attributes["Name"].Value;
            }
            else if (node.ChildNodes.Count == 2)
            {
                fieldname = node.ChildNodes[0].Attributes["Name"].Value;
                fieldtype = node.ChildNodes[1].Attributes["Type"].Value;
                conditioinvalue = node.ChildNodes[1].InnerText;
            }
            if (!string.IsNullOrEmpty(fieldname))
            {
                fieldvalue = GetFieldvalue(applyitem, fieldname);
            }
            switch (nodename)
            {
                case "eq":
                    return fieldvalue.Equals(conditioinvalue);
                case "neq":
                    return !fieldvalue.Equals(conditioinvalue);
                case "geq":
                    return Convert.ToDouble(fieldvalue) >= Convert.ToDouble(conditioinvalue);
                case "leq":
                    return Convert.ToDouble(fieldvalue) <= Convert.ToDouble(conditioinvalue);
                case "gt":
                    return Convert.ToDouble(fieldvalue) > Convert.ToDouble(conditioinvalue);
                case "lt":
                    return Convert.ToDouble(fieldvalue) < Convert.ToDouble(conditioinvalue);
                case "isnull":
                    return string.IsNullOrEmpty(fieldvalue);
                case "isnotnull":
                    return !string.IsNullOrEmpty(fieldvalue);
                case "contains":
                    return fieldvalue.Contains(conditioinvalue);
                case "ncontains":
                    return !fieldvalue.Contains(conditioinvalue);
                case "beginswith":
                    return fieldvalue.StartsWith(conditioinvalue);
                case "endswith":
                    return fieldvalue.EndsWith(conditioinvalue);
            }
            return false;
        }


        public static string GetFieldvalue(ListItem applyitem, string fieldtitle)
        {
            object value = applyitem[fieldtitle.Trim()];
            if (value != null)
            {
                string fieldvalue = string.Empty;
                string fieldtype = value.GetType().Name;
                switch (fieldtype)
                {
                    case "FieldUserValue":
                        FieldUserValue field = value as FieldUserValue;
                        fieldvalue = field.LookupValue;
                        break;
                    case "String":
                        fieldvalue = string.Format("{0}", value);
                        break;
                    case "FieldLookupValue":
                        FieldLookupValue field1 = value as FieldLookupValue;
                        fieldvalue = field1.LookupValue;
                        break;
                    case "FieldNumber":
                        fieldvalue = value.ToString();
                        break;
                    case "Double":
                        double amount = Convert.ToDouble(value);
                        fieldvalue = amount.ToString();//ToString("c");
                        break;
                    case "FieldMultiLineText":
                        fieldvalue = string.Format("{0}", value);
                        break;
                    case "FieldUserValue[]":
                        FieldUserValue[] fieldusers = value as FieldUserValue[];
                        foreach (FieldUserValue u in fieldusers)
                        {
                            fieldvalue += u.LookupValue + ";";
                        }
                        fieldvalue = fieldvalue.TrimEnd(';');
                        break;
                    case "FieldLookupValue[]":
                        FieldLookupValue[] fieldlookups = value as FieldLookupValue[];
                        foreach (FieldLookupValue fl in fieldlookups)
                        {
                            fieldvalue += fl.LookupValue + ";";
                        }
                        fieldvalue = fieldvalue.TrimEnd(';');
                        break;
                    case "Boolean":
                        bool temp = Convert.ToBoolean(value);
                        if (temp)
                        {
                            fieldvalue = "1";
                        }
                        else
                        {
                            fieldvalue = "0";
                        }
                        break;

                    default:
                        fieldvalue = string.Format("{0}", value);
                        break;
                }
                return fieldvalue;
            }
            else
            {
                return string.Empty;
            }
        }

        public static int GetLookUpFieldID(ListItem applyitem, string fieldtitle)
        {
            object value = applyitem[fieldtitle];
            if (value != null)
            {
                FieldLookupValue temp = value as FieldLookupValue;
                return temp.LookupId;
            }
            return 0;
        }
    }
}

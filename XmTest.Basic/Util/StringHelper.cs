using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmTest.Basic.Util
{
    //扩展方法:字符串处理封装类
    //注: 步骤：先定义存储位置(Helpers),定义封装分类（StringHelper 静态类）。
    //          定义具体扩展方法 eg: IsNullOrEmpty()

    public static class StringHelper
    {
        /// <summary>
        /// 扩展： 判断是否为空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T data)
        {
            if (data == null)
                return true;
            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()) || data.ToString().Trim() == string.Empty)
                {
                    return true;
                }
            }
            if (data.GetType() == typeof(DBNull))
                return true;
            return false;
        }
        public static bool IsNotNullOrEmpty<T>(this T data)
        {
            //如果为null
            if (data == null)
                return false;
            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()) || data.ToString().Trim() == string.Empty)
                {
                    return false;
                }
            }
            if (data.GetType() == typeof(DBNull))
                return false;
            //不为空
            return true;
        }
        /// <summary>
        /// 单次换行/多次换行
        /// </summary>
        public static string Rn<T>(this T data,int num=0)
        {
            if(data!=null)
            {
                if (num>1)
                {
                    string str = string.Empty;
                    for (int i = 0; i < num;i++ )
                    {
                        str += "\r\n";
                    }
                    return data.ToString() + str;
                }
                return data.ToString() + "\r\n";
            }
            return "\r\n";;
        }
        /// <summary>
        /// 截取指定字符串之间的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string Substr(string str, string str1, string str2)
        {
            try
            {
                int IndexofA = str.IndexOf(str1);
                int IndexofB = str.IndexOf(str2);
                int Index = IndexofB - IndexofA - str1.Length;
                if (Index < 0)
                    return string.Empty;
                return str.Substring(IndexofA + str1.Length, Index);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
    }
 
}
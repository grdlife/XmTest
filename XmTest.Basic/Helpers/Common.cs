
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XmTest.Basic.Helpers
{
    /*.常用数据帮助工具类有哪些？
  文件类：Excel、Pdf、文件写入\写出、Word、Txt、Image
  信息类：Email 、短信 、日志
  通用类：数据库、加密解密、类型转换、缓存、ip、地图、json、加密解密、 查询、正则、数据库、http传输、反射、字典、数据验证、
  字符类：字符串、时间、计算、枚举、
     */
    /// <summary>
    /// 通用类型
    /// </summary>
    public static class CommonHelper
    {

    }

    /// <summary>
    /// 文件类型
    /// </summary>
    public static class FileHelper
    {

    }

    /// <summary>
    /// 字符串类型
    /// </summary>
    public static class StringHelper
    {

        /// <summary>
        /// 为空验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this T data)
        {
            if (data == null)
                return true;
            if (data.GetType() == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(data.ToString()))
                    return true;
            }
            if (data.GetType() == typeof(DBNull))
                return true;
            return false;
        }

        /// <summary>
        /// 不为空验证
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<T>(this T data)
        {
            if (data == null)
                return false;
            if (data.GetType() == typeof(string))
            {
                if (string.IsNullOrWhiteSpace(data.ToString()))
                    return false;
            }
            if (data.GetType() == typeof(DBNull))
                return false;
            return true;
        }


        //Enum
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DescriptionString(this Enum obj)
        {
           var attribs=(DescriptionAttribute[])obj.GetType().GetField(obj.ToString()).GetCustomAttributes(typeof(DescriptionAttribute),false);
           return attribs.Length > 0 ? attribs[0].Description : obj.ToString();
        }


        #region[   常用字符串格式化处理   ]

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatStrDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        
        #endregion
    }

    /// <summary>
    /// 信息类型
    /// </summary>
    public class MessageHelper
    {

    }


}

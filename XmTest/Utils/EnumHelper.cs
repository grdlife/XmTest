using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace XmTest.Utils
{
    /// <summary>
    /// Enum帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举成员的值(this是扩展方法的标志)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 获取枚举Name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string obj) where T : struct
        {
            if (string.IsNullOrEmpty(obj))
            {
                return default(T);
            }
            try
            {
                return (T)Enum.Parse(typeof(T), obj, true);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        /// <summary>
        /// 获取指定枚举成员的描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum obj)
        {
            var attribs = (DescriptionAttribute[])obj.GetType().GetField(obj.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attribs.Length > 0 ? attribs[0].Description : obj.ToString();
        }
        /// <summary>
        /// 获取指定枚举类型字符串描述
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this string str, Type type)
        {
            FieldInfo fieldInfo = type.GetField(str);
            if (fieldInfo.IsNotNullOrEmpty())
            {
                var attribs = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attribs.Length > 0 ? attribs[0].Description : str;
            }
            return str;
        }

        /// <summary>
        /// 根据枚举值，获取指定枚举类的成员描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDescriptionString(this Type type, int? id)
        {
            var values = from Enum e in Enum.GetValues(type)
                         select new { id = e.ToInt(), name = e.ToDescriptionString() };

            if (!id.HasValue) id = 0;

            try
            {
                return values.ToList().Find(c => c.id == id).name;
            }
            catch
            {
                return "";
            }
        }

        public static List<SelectListItem> GetListItems(this Type type)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            Array arys = Enum.GetValues(type);
            foreach (int item in arys)
            {
                items.Add(new SelectListItem
                {
                    Text = EnumHelper.GetDescriptionString(type, item),
                    Value = item.ToString()
                });
            }
            return items;
        }
    }
}
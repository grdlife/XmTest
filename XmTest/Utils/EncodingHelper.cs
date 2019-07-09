using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
namespace XmTest.Utils
{
    public class EncodingHelper
    {
        /// <summary>
        /// MD5生成
        /// </summary>
        /// <param name="sSource"></param>
        /// <returns></returns>
        public static string EncryptMD5(string sSource)
        {
            try
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(sSource);
                MD5CryptoServiceProvider md5csp = new MD5CryptoServiceProvider();
                byte[] resultData = md5csp.ComputeHash(inputByteArray);
               
                StringBuilder sBuilder = new StringBuilder();
                // Loop through each byte of the hashed data and format each one as a hexadecimal string.
                for (int i = 0; i < resultData.Length; i++)
                    sBuilder.Append(resultData[i].ToString("x2"));//X
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }




    }
}
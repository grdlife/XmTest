using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Script.Serialization;
using XmTest.Models;
namespace XmTest.Utils
{

    /// <summary>
    /// Http帮助类
    /// </summary>
    public class HttpHelper
    {
        //WCF 服务器地址
        protected static string wcfURL = System.Configuration.ConfigurationManager.AppSettings["WCF_URL"];

        /// <summary>  
        /// Http连接操作帮助类  
        /// </summary> 
        public static string OpenReadWithHttps(string URL, string strPostdata)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "post";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] buffer = encoding.GetBytes(strPostdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string PostDateWithoutTid(string WCFservicesName, string functionName, object T)
        {
            try
            {
                string result = "";

                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var content = JsonConvert.SerializeObject(T, settings);
                var url = wcfURL + "/" + WCFservicesName;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + functionName);
                request.Method = "POST";
                request.ContentType = "application/json";

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                request.ContentLength = data.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    //reqStream.Close();
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string PostData(string WCFservicesName, string functionName)
        {
            try
            {
                var tId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                var userInfo = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;
                string result = "";
                var url = wcfURL + "/" + WCFservicesName;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + functionName);
                request.Method = "POST";
                request.Headers.Add("tID", tId);
                request.Headers.Add("uID", userInfo == null ? "" : userInfo.LoginId.ToString());
                request.ContentType = "application/json";
                Stream requestStram = request.GetRequestStream();
                requestStram.Close();
                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string PostDataWtihModel(string WCFservicesName, string functionName, object T)
        {
            try
            {
                string result = "";
                var tId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                var userInfo = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;
                if (tId == null || tId == "")
                {
                    throw new Exception("Error ,Session Is Null!");
                }
                else
                {
                    var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    var content = JsonConvert.SerializeObject(T, settings);
                    var url = wcfURL + "/" + WCFservicesName;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + functionName);

                    request.Method = "POST";
                    request.Headers.Add("tID", tId);
                    request.Headers.Add("uID", userInfo == null ? "" : userInfo.LoginId.ToString());

                    request.ContentType = "application/json";
                    #region 添加Post 参数
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = data.Length;
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                    #endregion

                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    //获取响应内容  
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static T PostDataWtihModel<T>(string WCFservicesName, string functionName, object obj)
        {
            try
            {
                string result = "";
                var tId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                var userInfo = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;
                if (tId == null || tId == "")
                {
                    throw new Exception("Error ,Session Is Null!");
                }
                else
                {
                    var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    var content = JsonConvert.SerializeObject(obj, settings);
                    var url = wcfURL + "/" + WCFservicesName;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + functionName);
                    request.Method = "POST";
                    request.Headers.Add("tID", tId);
                    request.Headers.Add("uID", userInfo == null ? "" : userInfo.LoginId.ToString());
                    request.ContentType = "application/json";

                    #region 添加Post 参数
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = data.Length;
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                    #endregion

                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    //获取响应内容  
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 移动端带tid请求头请求
        /// </summary>
        /// <param name="WCFservicesName"></param>
        /// <param name="functionName"></param>
        /// <param name="T"></param>
        /// <returns></returns>
        public static string PostDataWtihModelMobile(string WCFservicesName, string functionName, object T, string uId, string tId)
        {
            try
            {
                string result = "";

                if (tId == null)
                {
                    tId = HttpContext.Current == null ? GetNewId() : HttpContext.Current.Request.Headers["tID"];
                }
                if (tId == null)
                {
                    tId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                }
                if (uId == null)
                {
                    uId = HttpContext.Current == null ? GetNewId() : HttpContext.Current.Request.Headers["uID"];
                }

                if (uId == null)
                {
                    uId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                }
                var userInfo = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;
                if (tId == null || tId == "")
                {
                    throw new Exception("Error ,Session Is Null!");
                }
                else
                {
                    var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    var content = JsonConvert.SerializeObject(T, settings);
                    var url = wcfURL + "/" + WCFservicesName;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + functionName);

                    request.Method = "POST";
                    request.Headers.Add("tID", tId);
                    request.Headers.Add("uID", uId);
                    request.ContentType = "application/json";
                    #region 添加Post 参数
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = data.Length;
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                    #endregion

                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    //获取响应内容  
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region
        public static T PPostModel<T>(string ServeName, string FUNC, string content) where T : class
        {
            var tid = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
            NameValueCollection NameValueCollections = new NameValueCollection();
            NameValueCollections.Add("content", content);

            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("tID", tid);
            var str_Object = new JavaScriptSerializer().Deserialize<T>(Encoding.UTF8.GetString(new WebClient() { Headers = Headers }.UploadValues(wcfURL + "/" + ServeName + "/" + FUNC, "POST", NameValueCollections)));
            return str_Object;
        }

        /// <summary>
        /// POST 返回Model
        /// </summary>
        /// <typeparam name="T">返回集合的类</typeparam>
        /// <param name="ServeName">服务名字</param>
        /// <param name="FUNC">函数方法</param>
        /// <returns></returns>
        public static T PostModel<T>(string ServeName, string FUNC) where T : class
        {
            var tid = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
            NameValueCollection NameValueCollections = new NameValueCollection();
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("tID", tid);
            var str_Object = new JavaScriptSerializer().Deserialize<T>(Encoding.UTF8.GetString(new WebClient() { Headers = Headers }.UploadValues(wcfURL + "/" + ServeName + "/" + FUNC, "POST", NameValueCollections)));
            return str_Object;
        }

        /// <summary>
        /// POST 返回列表
        /// </summary>
        /// <typeparam name="T">返回集合的类</typeparam>
        /// <param name="ServeName">服务名字</param>
        /// <param name="FUNC">函数方法</param>
        /// <returns></returns>
        public static List<T> PostList<T>(string ServeName, string FUNC) where T : class
        {
            var tid = "";
            if (System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie] != null)
            {
                tid = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
            }
            var userInfo = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;

            NameValueCollection NameValueCollections = new NameValueCollection();
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("tID", tid);
            Headers.Add("uID", userInfo == null ? "" : userInfo.LoginId.ToString());

            var str_Object = new JavaScriptSerializer().Deserialize<List<T>>(Encoding.UTF8.GetString(new WebClient() { Headers = Headers }.UploadValues(wcfURL + "/" + ServeName + "/" + FUNC, "POST", NameValueCollections)));
            return str_Object;
        }

        public static List<T> PostList_GUID<T>(string ServeName, string FUNC, Guid id) where T : class
        {
            var tid = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
            NameValueCollection NameValueCollections = new NameValueCollection();
            NameValueCollections.Add("ID", id.ToString());
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("tID", tid);

            var str_Object = new JavaScriptSerializer().Deserialize<List<T>>(Encoding.UTF8.GetString(new WebClient() { Headers = Headers }.UploadValues(wcfURL + "/" + ServeName + "/" + FUNC, "POST", NameValueCollections)));
            return str_Object;
        }

        public static T PostModel_GUID<T>(string ServeName, string FUNC, Guid id) where T : class
        {
            var tid = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
            NameValueCollection NameValueCollections = new NameValueCollection();
            NameValueCollections.Add("ID", id.ToString());
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("tID", tid);
            var str_Object = new JavaScriptSerializer().Deserialize<T>(Encoding.UTF8.GetString(new WebClient() { Headers = Headers }.UploadValues(wcfURL + "/" + ServeName + "/" + FUNC, "POST", NameValueCollections)));
            return str_Object;
        }


        /// <summary>
        /// Post 返回带参数的列表 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ServeName"></param>
        /// <param name="FUNC"></param>
        /// <returns></returns>
        public static List<T> PostParameterList<T>(string ServeName, string FUNC, T model) where T : class
        {
            try
            {
                List<T> results = new List<T>();
                string result = "";
                var tId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                var userInfo = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;
                if (tId == null || tId == "")
                {
                    throw new Exception("Error ,Session Is Null!");
                }
                else
                {
                    var content = JsonConvert.SerializeObject(model);
                    var url = wcfURL + "/" + ServeName;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + FUNC);
                    request.Method = "POST";
                    request.Headers.Add("tID", tId);
                    request.Headers.Add("uID", userInfo == null ? "" : userInfo.LoginId.ToString());
                    request.ContentType = "application/json";
                    #region 添加Post 参数
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = data.Length;
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                    #endregion
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    //获取响应内容  
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                    var ss = result;
                    results = JsonConvert.DeserializeObject<List<T>>(result);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Post 返回带参数的列表 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ServeName"></param>
        /// <param name="FUNC"></param>
        /// <returns></returns>
        public static T PostParameterModel<T>(string ServeName, string FUNC, T model) where T : class
        {
            try
            {
                T results;
                string result = "";
                var tId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                if (tId == null || tId == "")
                {
                    throw new Exception("Error ,Session Is Null!");
                }
                else
                {
                    var content = JsonConvert.SerializeObject(model);
                    var url = wcfURL + "/" + ServeName;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + FUNC);
                    request.Method = "POST";
                    request.Headers.Add("tID", tId);
                    request.ContentType = "application/json";
                    #region 添加Post 参数
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = data.Length;
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                    #endregion
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    //获取响应内容  
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                    results = JsonConvert.DeserializeObject<T>(result);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool PostParameterBool<T>(string ServeName, string FUNC, T model) where T : class
        {
            try
            {
                bool results;
                string result = "";
                var tId = System.Web.HttpContext.Current.Request.Cookies[WebContent.UserCookie].Value;
                var userInfo = System.Web.HttpContext.Current.Session[WebContent.UserSession] as UserLoginModel;

                if (tId == null || tId == "")
                {
                    throw new Exception("Error ,Session Is Null!");
                }
                else
                {
                    var content = JsonConvert.SerializeObject(model);
                    var url = wcfURL + "/" + ServeName;
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + FUNC);
                    request.Method = "POST";
                    request.Headers.Add("tID", tId);
                    request.Headers.Add("uID", userInfo == null ? "" : userInfo.LoginId.ToString());
                    request.ContentType = "application/json";
                    #region 添加Post 参数
                    byte[] data = Encoding.UTF8.GetBytes(content);
                    request.ContentLength = data.Length;
                    using (Stream reqStream = request.GetRequestStream())
                    {
                        reqStream.Write(data, 0, data.Length);
                        reqStream.Close();
                    }
                    #endregion
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    //获取响应内容   
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                    results = result == "false" ? false : true;
                }

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region
        /// <summary>
        /// 判断请求是否来自于手机端
        /// </summary>
        /// <returns></returns>
        public static bool IsMobile()
        {
            bool flag = false;
            string agent = HttpContext.Current.Request.UserAgent;
            string[] keywords = { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" };
            //排除 Windows 桌面系统 
            if (!string.IsNullOrWhiteSpace(agent) && (!agent.Contains("Windows NT") || (agent.Contains("Windows NT") && agent.Contains("compatible; MSIE 9.0;"))))
            {
                //排除 苹果桌面系统 
                if (!agent.Contains("Windows NT") && !agent.Contains("Macintosh"))
                {
                    foreach (string item in keywords)
                    {
                        if (agent.Contains(item))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                flag = true;
            }
            return flag;
        }
        #endregion
        /// <summary>
        /// 获取20位的独特Id
        /// </summary>
        /// <returns></returns>
        public static string GetNewId()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");
            return guid + guid.Substring(0, 6) + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

    }
}
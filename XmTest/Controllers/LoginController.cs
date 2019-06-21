using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Models;
using System.Diagnostics;
using System.Data;
using System.Data.Entity;
using System.Text;
using XmTest.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XmTest.IRepository.sysBasic;
using XmTest.Repository.sysBasic;
namespace XmTest.Controllers
{
    [AllowAnonymous]//允许跳过登录验证
    public class LoginController : LoginBaseController
    {
        private string userLifeCycle = System.Configuration.ConfigurationManager.AppSettings["CurrentUserCache_LifeCycle"];
        public IX_UserRepository useService = new X_UserRepository();
        // GET: /Login/
        public ActionResult Index()
        {

            var cookie = Request.Cookies[WebContent.UserCookie];
            if(cookie !=null)
            {
                string result = RoleHelper.CheckLogined(cookie.Value);
                if (result!="error")
                {
                    if(HttpContext.Session[WebContent.UserSession]==null)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    HttpContext.Response.Cookies[WebContent.UserCookie].Expires = DateTime.Now.AddDays(-1);//清除本地cookie
                    return View();
                }

            }
            else
            {
                 return View();
            }
        }
        public ActionResult Index2()
        {
            var cookie = Request.Cookies[WebContent.UserCookie];
            if (cookie != null)
            {
                string result = RoleHelper.CheckLogined(cookie.Value);
                if (result != "error")
                {
                    if (HttpContext.Session[WebContent.UserSession] == null)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    HttpContext.Response.Cookies[WebContent.UserCookie].Expires = DateTime.Now.AddDays(-1);//清除本地cookie
                    return View();
                }

            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(string form)
        {
            List<JObject> obj = JsonConvert.DeserializeObject<List<JObject>>(form);

            string Name = obj[0]["value"].ToString();
            string pwd = obj[1]["value"].ToString();
            string msg = string.Empty;
            int loginId;
            if (CheckUser(Name, pwd, out msg,out loginId))
            {
                //1、遍历所有应用服务器cache
                string token = "";
                //将用户登录信息保存在cache中，用于单点登录
                token = Name + "_" + Guid.NewGuid().ToString().Substring(4, 12) + DateTime.Now.Millisecond;
                token = EncodingHelper.EncryptMD5(token);
                CacheHelper.Insert(token, Name, Convert.ToInt32(userLifeCycle));//单点登录SSO服务器端cache添加

                string redirecturl = "http://localhost:2979/Home/Index";
                CreateCookie(token, loginId, Name, redirecturl);
                return Json(new { code = 1, msg = msg });
                //return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.user = "";
                return Json(new { code = -1, msg = msg });
                //return RedirectToAction("Index", "Login");
            }
        }
      
        public  ActionResult Loginout()
        {
            var token = "";
            if (Request.Cookies[WebContent.UserCookie] != null)
            {
                token = Request.Cookies[WebContent.UserCookie].Value;
                HttpContext.Response.Cookies[WebContent.UserCookie].Expires = DateTime.Now.AddDays(-1);//清除本地cookie
                CacheHelper.Remove(token); //跳转单点登录服务器清除单点用户cache
            }
            Session.Remove(WebContent.UserSession);//清除应用服务器Session
            return RedirectToAction("Index", "Login"); 
        }

        public bool CheckUser(string Name, string pwd, out string msg, out int loginId)
        {
            try
            {
                var user = useService.GetModel(x => x.LoginName.Equals(Name.Trim(), StringComparison.CurrentCultureIgnoreCase) && !x.IsDisabled);
                if (user == null)
                {
                    loginId = 0;
                    msg = "用户不存在！";
                    return false;
                }
                if (user.Pwd == pwd.Trim())
                {
                    loginId = user.Id;
                    msg = "登录成功！";
                    return true;
                }
                else
                {
                    loginId = 0;
                    msg = "登录名、密码不准确！";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                loginId = 0;
                msg = ex.Message;
                return false;
            }
            
        }
        public string IsLogined(string token)
        {
            var obj = CacheHelper.Get(token);
            if (obj == null)
            {
                return "error";
            }
            //更新缓存过期时间
            CacheHelper.Remove(token);
            CacheHelper.Insert(token, obj, Convert.ToInt32(userLifeCycle));
            //TODO:将用户相关的信息返回
            return obj.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Models;
using XmTest.Basic;
using XmTest.Basic.Util;
using XmTest.Utils;
namespace XmTest.Controllers
{
    public class LoginBaseController : Controller
    {
        
        //设置Cookie
        public LoginBaseController()
        {
           
        }
        public ActionResult CreateCookie(string token, int LoginId, string loginName, string redirect_url)
        {
            if (token.IsNotNullOrEmpty() && redirect_url != null)
            {
                //单点登录校验cookie
                HttpCookie cookie = new HttpCookie(WebContent.UserCookie);
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddDays(2);
                cookie.Value = token;
                Response.Cookies.Add(cookie);
                //记录用户相关session
                UserLoginModel _user = new UserLoginModel { LoginName = loginName, LoginId = LoginId };
                Session[WebContent.UserSession] = _user;
                return RedirectToAction(redirect_url);
            }
            else
            {
                return Json("error,InputData Error!");
            }
        }
       
    }
}

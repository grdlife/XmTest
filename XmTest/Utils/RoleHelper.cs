using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmTest.Utils
{
    public class RoleHelper
    {
        public static string CheckLogined(string token)
        {
            var v = "";//页面返回状态，error表示验证未通过,成功返回用户名

            //验证是否登录--每个需要登录验证的地方都应该调用
            var iAddress = System.Configuration.ConfigurationManager.AppSettings["iAddress"];
            v = HttpHelper.OpenReadWithHttps(iAddress + "/Login/IsLogined", "token=" + token).ToString();
            return v;
        }

    }
}
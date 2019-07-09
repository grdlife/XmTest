using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XmTest.Models;
using XmTest.Controllers;
using System.Web.Mvc;
using XmTest.Service.Basic;
using XmTest.Data.Factory;
namespace XmTest.Utils
{
    /// <summary>
    /// 代码集锦
    /// </summary>
    public class Helpers
    {
        public static Helpers Instance { get { return DALFactory<Helpers>.Instance; } }
        /// <summary>
        /// 文字编辑=>获取所有类别
        /// </summary>
        /// <returns></returns>
        public static SelectList GetWebItems(string key = "")
        {
            Dictionary<string, string> dic = NotesService.Instance.GetClassifyfield(BaseWebController.GetLoginId());
            SelectList slist = new SelectList(dic, "Key", "Value", key);
            return slist;
        }
        
    }
}
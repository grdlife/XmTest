using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Controllers;
using XmTest.Data.Entity;
using XmTest.Service.Basic;

namespace XmTest.Areas.Blog.Controllers
{
    public class BlogController : BaseWebController
    {
        //博客首页
        // GET: /Blog/Blog/

        public ActionResult BlogCenter()
        {
            var notes = NotesService.Instance.GetMyNotes(loginId)?? new List<Notes>();
            return View(notes);
        }


        /// <summary>
        /// 博客管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BlogManage()
        {
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Controllers;
using XmTest.Service.Basic;

namespace XmTest.Areas.Blog.Controllers
{
    public class BlogController : BaseWebController
    {
        //
        // GET: /Blog/Blog/

        public ActionResult BlogCenter()
        {
            ViewBag.xcList = NotesDAL.Instance.GetMyNotes(loginId);
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Data.Entity;

namespace XmTest.Controllers
{
    public class HomeController : BaseWebController
    {
        //
        // GET: /Home/
       
        public ActionResult Index()
        {
            List<Notes> notes = new List<Notes>();
            if(noteService!=null)
            {
                notes = noteService.GetList(x => x.UserID == loginId);
                if (notes == null && notes.Count == 0)
                    notes = new List<Notes>();
            }
            return View(notes);
        }
        /// <summary>
        /// 导航栏测试页
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Tud()
        {
            return View();
        }
    }
}

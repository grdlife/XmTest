using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Data.Entity;
using XmTest.Service.Basic;
using XmTest.Basic.Web;
using XmTest.Basic.Helpers;
namespace XmTest.Controllers
{
    public class HomeController : BaseWebController
    {
        //
        // GET: /Home/

        public ActionResult Index(Page page)
        {

            List<Notes> notes = new List<Notes>();
            try
            {
                notes = NotesService.Instance.GetNotes(page);
                return View(notes);
            }
            catch (Exception ex)
            {
                LogHelper.Write(ex, "/Home/");
                return View(notes);
            }

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

        /// <summary>
        /// 404错误页
        /// </summary>
        /// <returns></returns>
        public ActionResult ErrorNoFund()
        {
            return View();
        }

        /// <summary>
        /// 通用错误页
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }
    }
}

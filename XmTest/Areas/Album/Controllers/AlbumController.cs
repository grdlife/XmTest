using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XmTest.Areas.Album.Controllers
{
    public class AlbumController : Controller
    {
        //相册/专辑
        // GET: /Album/Album/

        public ActionResult Index()
        {
            return View();
        }

    }
}

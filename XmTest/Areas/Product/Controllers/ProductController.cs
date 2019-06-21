using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmTest.Controllers;
namespace XmTest.Areas.Product.Controllers
{
    public class ProductController : BaseWebController
    {
        //
        // GET: /Product/Product/

        public ActionResult Index()
        {
            return View();
        }

    }
}

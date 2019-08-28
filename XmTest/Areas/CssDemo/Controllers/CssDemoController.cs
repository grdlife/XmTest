using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XmTest.Areas.CssDemo.Controllers
{
    public class CssDemoController : Controller
    {
        //Css样式demo
        // GET: /CssDemo/CssDemo/

        /// <summary>
        /// 多边形
        /// </summary>
        /// <returns></returns>
        public ActionResult Polygon()
        {
            return View();
        }


        /// <summary>
        /// 复选框样式优化
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckBox()
        {
            return View();
        }

        /// <summary>
        /// 古风背景框架
        /// </summary>
        /// <returns></returns>
        public ActionResult AntiquityBG()
        {
            return View();
        }

        public ActionResult BookTopage()
        {
            return View();
        }


        public ActionResult BootStrapTable()
        {
            return View();
        }


        /// <summary>
        /// 销售中心
        /// </summary>
        /// <returns></returns>
        public ActionResult SaleChartCenter()
        {
            return View();
        }


        public ActionResult DateTimePicker()
        {
            Format();
            return View();
        }


        /// <summary>
        /// 魔方相册
        /// </summary>
        /// <returns></returns>
        public ActionResult MagicCubePicture()
        {
            return View();
        }


        /// <summary>
        /// 常用字符串格式化处理
        /// </summary>
        public void Format()
        {
            string str = string.Empty;
            double number;
            number = Math.Round(12.55);//四舍五入，向上取值。13.0
            number = Math.Floor(12.45);//四舍五入，向下取值。 12.0
            number = Math.Abs(-1);//取绝对值。1.0

            number = Math.BigMul(2, 1);//取两值乘积。2.0
            number = Math.Ceiling(6.04);//取最小整数。7.0
            number = Math.IEEERemainder(1, 2);//1
            number = Math.Truncate(5.23);// 5.0
            number = Math.Min(5.23, 4.52);//4.52
            number = Math.Max(5.23, 4.52);//5.23


            //ToString()
            double intt = 1213.041;
            string C = intt.ToString("C");//¥1,213.04
            string E = intt.ToString("E");//1.213041E+003
            string F = intt.ToString("F2");//1213.04
            string M = intt.ToString("N2");//1,213.04
            string P = intt.ToString("P");//121,304.10%
       
        }



    }
}

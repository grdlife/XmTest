
using System.Web.Mvc;

namespace XmTest.Areas.CssDemo
{
    public class CssDemoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CssDemo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CssDemo_default",
                "CssDemo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

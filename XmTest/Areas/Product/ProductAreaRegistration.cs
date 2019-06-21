
using System.Web.Mvc;

namespace XmTest.Areas.Product
{
    public class ProductAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Product"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Product_default",
                "Product/{Controller}/{Action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}
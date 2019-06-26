using System.Web.Mvc;

namespace XmTest.Areas.Album
{
    public class AlbumAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Album";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Album_default",
                "Album/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

using System.Web;
using System.Web.Mvc;

namespace XmTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());//HandleErrorAttribute 应用此属性 可设置错误页 默认显示于~View/Shared/
        }
    }
}
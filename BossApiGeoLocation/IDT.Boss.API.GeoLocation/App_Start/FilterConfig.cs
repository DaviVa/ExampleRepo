using System.Web;
using System.Web.Mvc;

namespace IDT.Boss.API.GeoLocation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

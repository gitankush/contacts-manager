using System.Web;
using System.Web.Mvc;

namespace Evolent.ContactsMgmt.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

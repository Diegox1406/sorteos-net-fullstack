using System.Web;
using System.Web.Mvc;

namespace pe.com.ciberelectrik.ui
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

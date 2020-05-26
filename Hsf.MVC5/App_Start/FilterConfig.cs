using System.Web;
using System.Web.Mvc;

namespace Hsf.MVC5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new Web.Core.Filter.LogExceptionFilter());
        }
    }
}

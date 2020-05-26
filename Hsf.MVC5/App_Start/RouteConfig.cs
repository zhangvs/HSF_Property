using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hsf.MVC5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("test/{*pathInfo}");//忽略路由


            //routes.MapRoute(
            //    name: "About",
            //    url: "about",//不区分大小写
            //    defaults: new { controller = "First", action = "String", id = UrlParameter.Optional }
            //);//静态路由

            //routes.MapRoute("TestStatic","Test/Static", new { controller = "First", action = "Index"});//静态路由

            //routes.MapRoute("TestStatic2", "Test/{action}",new { controller = "First"});//http://localhost:8090/test 报错，虽然注释了忽略路由，因为找不到默认的action

            //routes.MapRoute(
            //        "Regex",
            //         "{controller}/{action}_{Year}_{Month}_{Day}",
            //         new { controller = "First"}
            //         //, new { Year = @"^\d{4}", Month = @"\d{2}", Day = @"\d{2}" }//参数校验
            //           );//正则路由 localhost:8090/First/Time_2019_03_23

            //routes.MapRoute(
            //        "String",
            //         "{controller}/{action}/{idid}",
            //         new { controller = "First",action="string", idid = UrlParameter.Optional }
            //           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }//可空，注意两个同名string方法参数问题
            );
        }
    }
}

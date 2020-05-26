using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Hsf.MVC5.Utility.Filter
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }

        // 
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //actionExecutedContext.Response.Headers.Add("", "");
            actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //base.OnActionExecuted(actionExecutedContext);
        }
    }
}
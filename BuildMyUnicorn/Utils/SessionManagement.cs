using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace BuildMyUnicorn.Utils
{
    public class SessionManagement
    {
    }

    public class SessionTimeout :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            HttpContext httpContext =  HttpContext.Current;

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                actionContext.Result = new RedirectResult("~/Login/Logout");
                return;
            }
            base.OnActionExecuting(actionContext);
        }
    }
}
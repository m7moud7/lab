using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Noor.BackEnd.App_Start
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
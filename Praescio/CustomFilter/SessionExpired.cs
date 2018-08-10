using Praescio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praescio.CustomFilter
{
    public class SessionExpiredAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// MVC Session Expired.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Common.ACCOUNT == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/Login?returnUrl=" + filterContext.HttpContext.Request.Url);
                    return;
                }
                //if (Common.ACCOUNT != null)
                //{
                //    filterContext.Result = new RedirectResult("~/Account/AccountInactive");
                //    return;
                //}
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
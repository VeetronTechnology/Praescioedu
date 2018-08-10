using Praescio.BusinessEntities.Common;
using Praescio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praescio.CustomFilter
{
    public class ChangePasswordFirstLoginAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// MVC Session Expired.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Common.ACCOUNT != null && (Common.ACCOUNT.AccountTypeId == (int)AccountType.Student || Common.ACCOUNT.AccountTypeId == (int)AccountType.IndividualStudent))
                {
                    // Login first time
                    if(Common.PARENTACCOUNT != null && Common.PARENTACCOUNT.UpdatePwdDateTime == null)
                    {
                        filterContext.Result = new RedirectResult("~/Account/AccountInactive?inactiveReason=1");
                        return;
                    }
                }

                if (Common.ACCOUNT != null && Common.ACCOUNT.UpdatePwdDateTime == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/ChangePassword");
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
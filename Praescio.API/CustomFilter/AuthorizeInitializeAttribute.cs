using Praescio.API.Controllers;
using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Praescio.API.CustomFilter
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string ip = string.Empty;
            var ctx = actionExecutedContext.Request.Properties["MS_HttpContext"] as HttpContextWrapper;
            if (ctx != null)
            {
                ip = ctx.Request.UserHostAddress;
            }

            HttpStatusCode status = HttpStatusCode.InternalServerError;

            String message = String.Empty;

            var exceptionType = actionExecutedContext.Exception.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Access to the Web API is not authorized.";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(DivideByZeroException))
            {
                message = "Internal Server Error.";
                status = HttpStatusCode.InternalServerError;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                message = "Null Reference Exception";
                status = HttpStatusCode.BadRequest;
            }
            else
            {
                try
                {
                    message = actionExecutedContext.Exception.Message;
                }
                catch (Exception)
                {
                    message = "not found";
                }
                status = HttpStatusCode.NotFound;
            }

            string strEx = actionExecutedContext.Exception.StackTrace;
            while (actionExecutedContext.Exception.InnerException != null)
            {
                strEx += actionExecutedContext.Exception.InnerException.StackTrace;
                actionExecutedContext.Exception = actionExecutedContext.Exception.InnerException;
            }

            var a = actionExecutedContext.ActionContext.Request.RequestUri;

            Mst_ExceptionLog logger = new Mst_ExceptionLog
            {
                LoggerName = "WebApi",
                ExceptionType = "Application Exception",
                ExceptionMessage = actionExecutedContext.Exception.Message,
                ExceptionStackTrace = strEx,
                IPAddress = ip,
                URL = actionExecutedContext.ActionContext.Request.RequestUri.AbsoluteUri,
                ControllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                CreatedDate = DateTime.Now
            };

            new Praescio.BusinessEntities.BAL.ExceptionLogging().AddLogToDB(logger);
            //Common.SendEmail(Common.GetExceptionContent(logger), ""/*Common.SendErrorTo*/, "", "AOPM WebApi Exception");

            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain"),
                StatusCode = status
            };

            base.OnException(actionExecutedContext);
        }
    }

    public class AuthorizeInitializeAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private PraescioContext db;
        private Mst_Account Account;

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    Dictionary<string, string> credentials = ParseRequestHeaders(actionContext);
                    if (!IsUserValid(credentials))
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

                    ((BaseApiController)actionContext.ControllerContext.Controller).UserIP = credentials["UserIP"].ToString();
                    ((BaseApiController)actionContext.ControllerContext.Controller).LoggedInAccount = Account;
                }
            }
            catch (Exception ex)
            {
                //Common.AddLogToDB(ex.StackTrace, ex.Message, "unauthorized access error");
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public Dictionary<string, string> ParseRequestHeaders(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            Dictionary<string, string> credentials = new Dictionary<string, string>();
            var httpRequestHeader = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
            httpRequestHeader = httpRequestHeader.Substring("Authorization".Length);

            string[] httpRequestHeaderValues = httpRequestHeader.Split(':');
            string username = httpRequestHeaderValues[0];
            string password = httpRequestHeaderValues[1];

            string UserIP = actionContext.Request.Headers.GetValues("UserIP").FirstOrDefault();
            string DomainKey = actionContext.Request.Headers.GetValues("DomainKey").FirstOrDefault();

            credentials.Add("UserName", username);
            credentials.Add("Password", password);
            credentials.Add("DomainKey", DomainKey);
            credentials.Add("UserIP", UserIP);
            return credentials;
        }
        private Boolean IsUserValid(Dictionary<string, string> credentials)
        {
            string username = string.Empty; string password = string.Empty; string domainkey = string.Empty; string connectionstring = string.Empty;
            try
            {
                username = credentials["UserName"];
                password = credentials["Password"];
                domainkey = credentials["DomainKey"];

                PraescioContext configContext = new PraescioContext();
                var account = configContext.Account.FirstOrDefault(x => x.UserName.Trim() == username.Trim() && x.Password == password && x.OrganizationAccount.DomainKey.ToLower() == domainkey.ToLower());
                if (account != null)
                {
                    Account = account;
                    return true;
                }

            }
            catch (Exception ex)
            {
                //Common.AddLogToDB(ex.StackTrace, ex.Message, "");
            }

            return false;
        }
    }
}
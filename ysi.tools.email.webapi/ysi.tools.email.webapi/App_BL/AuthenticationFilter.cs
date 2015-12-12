using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ysi.tools.email.webapi.App_DB;

namespace ysi.tools.email.webapi.App_BL
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IEnumerable<string> tokens;
            string token = string.Empty;
            if (actionContext.Request.Headers.TryGetValues("token", out tokens))
            {
                token = tokens.First();
            }
            try
            {
                if (JsonWebToken.Instance.VerifyToken(token))
                {
                    //continue;
                }
                else
                {
                    HttpContext.Current.Response.AppendHeader("Result", "fail");
                    HttpContext.Current.Response.AppendHeader("Reason", "token invalid");
                    HttpContext.Current.Response.StatusCode = 401;
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    ExceptionLog.Instance.AddExceptionLog("Token : " + token, null, null, "token invalid", "token invalid");
                    return;
                }
            }
            catch (JsonTokenExpireException ex)
            {
                HttpContext.Current.Response.AppendHeader("Result", "fail");
                HttpContext.Current.Response.AppendHeader("Reason", "token expire");
                HttpContext.Current.Response.StatusCode = 401;
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                ExceptionLog.Instance.AddExceptionLog("Token : " + token, null, null, "token expire", "token expire");
                return;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.AppendHeader("Result", "fail");
                HttpContext.Current.Response.AppendHeader("Reason", "server error");
                HttpContext.Current.Response.StatusCode = 401;
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                ExceptionLog.Instance.AddExceptionLog("Token : " + token, null, null, ex.Message, ex.ToString());
                return;
            }
            HttpContext.Current.Response.AppendHeader("Result", "pass");
            HttpContext.Current.Response.StatusCode = 200;
        }
    }
}
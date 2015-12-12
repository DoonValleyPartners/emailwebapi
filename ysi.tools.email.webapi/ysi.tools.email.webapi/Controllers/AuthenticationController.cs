using System;
using System.Web;
using System.Web.Http;
using ysi.tools.email.webapi.App_BL;
using ysi.tools.email.webapi.App_DB;
using ysi.tools.email.webapi.Models;

namespace ysi.tools.email.webapi.Controllers
{
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        [Route("api/Authentication/gettoken")]
        public string GetToken([FromBody] string apiKey)
        {
            string token = string.Empty;
            Client client;
            try
            {
                client = ClientDB.Instance.GetClientByAPIKey(apiKey);
                if (client != null)
                {
                    var payload = new
                    {
                        iss = "dvp",
                        exp = DateTime.UtcNow.AddMinutes(2).ToString(),
                        key = apiKey
                    };
                    token = JsonWebToken.Instance.GetToken(payload, apiKey);
                }
                else
                {
                    HttpContext.Current.Response.AppendHeader("Result", "fail");
                    HttpContext.Current.Response.AppendHeader("Reason", "api key is not valid");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.AppendHeader("Result", "fail");
                HttpContext.Current.Response.AppendHeader("Reason", "server error");
                ExceptionLog.Instance.AddExceptionLog("ApiKey : " + apiKey, null, null, ex.Message, ex.ToString());
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                HttpContext.Current.Response.AppendHeader("Result", "fail");
                HttpContext.Current.Response.AppendHeader("Reason", "token not available");
            }
            else
                HttpContext.Current.Response.AppendHeader("Result", "pass");

            ApplicationLog.Instance.AddLog(client != null ? client.ClientCode : null, null, null, null, null, null, "GetToken", token, null, null);
            return token;
        }
    }
}
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using ysi.tools.email.webapi.App_BL;
using System.Web;
using System;
using ysi.tools.email.webapi.App_BL.emailproviders;

namespace ysi.tools.email.webapi.Controllers
{
    [AuthenticationFilter]
    public class EmailController : ApiController
    {
        [HttpPost]
        [Route("api/Email/sendemail")]
        public void SendEmail()
        {
            MailgunEmailServiceProvider provider = new MailgunEmailServiceProvider();
            provider.SendEmail();
        }
    }
}
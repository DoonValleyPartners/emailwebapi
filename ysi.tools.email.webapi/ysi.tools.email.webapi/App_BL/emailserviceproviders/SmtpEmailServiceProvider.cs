using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ysi.tools.email.webapi.Models;

namespace ysi.tools.email.webapi.App_BL.emailproviders
{
    public class SmtpEmailServiceProvider :IEmailServiceProvider
    {
        public void SendEmail()
        {
            throw new NotImplementedException();
        }

        public string AddTemplate(string templateName)
        {
            throw new NotImplementedException();
        }

        public void RemoveTemplate(string templateCode)
        {
            throw new NotImplementedException();
        }

        public Template UpdateTemplate(string templateId, string templateContent)
        {
            throw new NotImplementedException();
        }

        public TemplateContainer GetAllTemplates()
        {
            throw new NotImplementedException();
        }

        public string ParseTemplate(string templateCode, IDictionary<string, string> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
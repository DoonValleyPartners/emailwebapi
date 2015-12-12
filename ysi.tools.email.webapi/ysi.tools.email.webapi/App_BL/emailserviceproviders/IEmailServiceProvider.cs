using System.Collections.Generic;
using ysi.tools.email.webapi.Models;

namespace ysi.tools.email.webapi.App_BL.emailproviders
{
    public interface IEmailServiceProvider
    {
        void SendEmail();
        string AddTemplate(string templateName);
        void RemoveTemplate(string templateId);
        Template UpdateTemplate(string templateId, string templateContent);
        TemplateContainer GetAllTemplates();
        string ParseTemplate(string templateCode, IDictionary<string, string> tokens);
    }
}
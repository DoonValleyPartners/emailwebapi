using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using ysi.tools.email.webapi.App_BL.Utils;
using ysi.tools.email.webapi.Models;

namespace ysi.tools.email.webapi.App_BL.emailproviders
{
    public class MailgunEmailServiceProvider : IEmailServiceProvider
    {
        public void SendEmail()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(ConfigurationSetting.Instance.MailgunAPIUrl);
            client.Authenticator = new HttpBasicAuthenticator("api", ConfigurationSetting.Instance.MailgunAPIKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", ConfigurationSetting.Instance.MailgunAPIDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <vdua@dvp.co.in>");
            request.AddParameter("to", "vivekdua81@gmail.com");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            IRestResponse res = client.Execute(request);
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
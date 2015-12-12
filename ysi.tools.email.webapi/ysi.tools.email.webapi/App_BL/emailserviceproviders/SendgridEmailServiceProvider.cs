using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ysi.tools.email.webapi.Models;

namespace ysi.tools.email.webapi.App_BL.emailproviders
{
    public class SendgridEmailServiceProvider : IEmailServiceProvider
    {
        string username = "dvpvdua";
        string password = "dvp12345";
        string url = "https://api.sendgrid.com/v3/templates";
        public void SendEmail()
        {
            SendGridMessage message = new SendGridMessage();
            message.From = new System.Net.Mail.MailAddress("vdua@dvp.co.in", "Vivek");
            message.Subject = "This is to test from sendgrid ";
            message.Text = " This is to test";
            Web web = new Web("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");
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("sendId", Guid.NewGuid().ToString());
            message.EnableTemplateEngine("76f64aad-072f-40d9-8913-388b55776720");
            message.AddTo(new List<string> { "vivekdua81@gmail.com", "vdua@scheduleonce.com" });
            message.AddSubstitution(":name", new List<string> { "Vivek", "Dua" });
            message.AddSubstitution(":price", new List<string> { "1234", "56789" });
            message.AddSection("sectionhere", "My section value");
            message.AddUniqueArgs(dic);
            message.EnableClickTracking(true);
            web.DeliverAsync(message);
        }

        public string AddTemplate(string templateName)
        {
            string templateId = string.Empty;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.ContentType = "application/json";
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                myHttpWebRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
                using (var writer = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    writer.Write("{\"name\":\""+ templateName + "\"}");
                }
                HttpWebResponse httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                string result = streamReader.ReadToEnd();
                templateId  = JsonConvert.DeserializeObject<Template>(result).id;
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string result = streamReader.ReadToEnd();
            }

            return templateId;

        }

        public void RemoveTemplate(string templateId)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url+"//"+templateId);
                myHttpWebRequest.Method = "DELETE";
                myHttpWebRequest.ContentType = "application/json";
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                myHttpWebRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
                HttpWebResponse httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                string result = streamReader.ReadToEnd();
                
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string result = streamReader.ReadToEnd();
            }
        }

        public Template UpdateTemplate(string templateId, string templateContent)
        {
            Template template = null;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url+"//"+templateId);
                myHttpWebRequest.Method = "PATCH";
                myHttpWebRequest.ContentType = "application/json";
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                myHttpWebRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
                using (var writer = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    writer.Write("{\"name\":\"" + templateContent + "\"}");
                }
                HttpWebResponse httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                string result = streamReader.ReadToEnd();
                template = JsonConvert.DeserializeObject<Template>(result);
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string result = streamReader.ReadToEnd();
            }

            return template;
        }

        public TemplateContainer GetAllTemplates()
        {
            TemplateContainer deserializedProduct = null;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myHttpWebRequest.Method = "GET";
                myHttpWebRequest.ContentType = "application/json";
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                myHttpWebRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
                HttpWebResponse httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                string result = streamReader.ReadToEnd();
                 deserializedProduct = JsonConvert.DeserializeObject<TemplateContainer>(result);
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string result = streamReader.ReadToEnd();
            }
            return deserializedProduct;
        }

        public string ParseTemplate(string templateCode, IDictionary<string, string> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
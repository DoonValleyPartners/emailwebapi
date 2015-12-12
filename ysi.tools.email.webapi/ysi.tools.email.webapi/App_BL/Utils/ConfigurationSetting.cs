using System;
using System.Configuration;
using System.Linq;

namespace ysi.tools.email.webapi.App_BL.Utils
{
    public class ConfigurationSetting
    {
        static readonly ConfigurationSetting _configurationSetting = new ConfigurationSetting();
        readonly string _mailgunAPIUrl;
        readonly string _mailgunAPIDomain;
        readonly string _mailgunAPIKey;

        private ConfigurationSetting()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains("MailgunAPIKey"))
                _mailgunAPIKey = ConfigurationManager.AppSettings["MailgunAPIKey"].ToString();
            else
                throw new Exception("MailgunAPIKey key is missing from configuration");
            if (ConfigurationManager.AppSettings.AllKeys.Contains("MailgunAPIUrl"))
                _mailgunAPIUrl = ConfigurationManager.AppSettings["MailgunAPIUrl"].ToString();
            else
                throw new Exception("MailgunAPIUrl key is missing from configuration");
            if (ConfigurationManager.AppSettings.AllKeys.Contains("MailgunAPIDomain"))
                _mailgunAPIDomain = ConfigurationManager.AppSettings["MailgunAPIDomain"].ToString();
            else
                throw new Exception("MailgunAPIDomain key is missing from configuration");
        }

        public static ConfigurationSetting Instance { get { return _configurationSetting; } }

        public string MailgunAPIUrl { get { return _mailgunAPIUrl; } }

        public string MailgunAPIDomain { get { return _mailgunAPIDomain; } }

        public string MailgunAPIKey { get { return _mailgunAPIKey; } }
    }
}
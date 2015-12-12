using System;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace ysi.tools.email.webapi.App_DB
{
    public class ApplicationLog
    {
        static ApplicationLog _logDB = new ApplicationLog();
        readonly string _dbConnectionString;
        private ApplicationLog()
        {
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.AsList().Contains("DBConnectionString"))
                _dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
            else
                throw new Exception("DBConnectionString configuration entry for database connection string is missing from configuration file");
        }

        public static ApplicationLog Instance { get { return _logDB; } }

        public void AddLog(string clientCode,
                           string templateCode,
                           string emailContent,
                           string emailRecipient,
                           byte[] attachment,
                           string emailSenderProvider,
                           string actionType,
                           string token,
                           string clientIPAddress,
                           string customMessage)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Query("sp_AddAPILog", new { ClientCode = clientCode, TemplateCode = templateCode, EmailContent = emailContent, EmailRecipient = emailRecipient, AttachmentContent = attachment, EmailSenderProvider = emailSenderProvider, ActionType = actionType, Token = token, ClientIPAddress = clientIPAddress, CustomMessage = customMessage }, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            { }
        }
    }
}
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace ysi.tools.email.webapi.App_DB
{
    public class ExceptionLog
    {
        static ExceptionLog _exceptionLogDB = new ExceptionLog();
        readonly string _dbConnectionString;
        private ExceptionLog()
        {
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.AsList().Contains("DBConnectionString"))
                _dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
            else
                throw new Exception("DBConnectionString configuration entry for database connection string is missing from configuration file");
        }

        public static ExceptionLog Instance { get { return _exceptionLogDB; } }

        public void AddExceptionLog(string clientCode, string clientIPAddress, string emailSenderProvider, string exceptionMessage, string exceptionDetail)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnectionString))
                {
                    connection.Query("sp_AddAPILog", new { ClientCode = clientCode, EmailSenderProvider = emailSenderProvider, ClientIPAddress = clientIPAddress, ExceptionMessage = exceptionMessage, ExceptionDetail = exceptionDetail}, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            { }
        }
    }
}
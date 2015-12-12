using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using ysi.tools.email.webapi.Models;

namespace ysi.tools.email.webapi.App_DB
{
    public class ClientDB
    {
        static ClientDB _clientDB = new ClientDB();
        readonly string _dbConnectionString;
        private ClientDB()
        {
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.AsList().Contains("DBConnectionString"))
                _dbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
            else
                throw new Exception("DBConnectionString configuration entry for database connection string is missing from configuration file");
        }

        public static ClientDB Instance { get { return _clientDB; } }

        public Client GetClientByAPIKey(string apiKey)
        {
            IEnumerable<Client> clients;
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                clients = connection.Query<Client>("sp_GetClient", new { APIKey = apiKey }, commandType: CommandType.StoredProcedure);
            }
            if (clients == null || clients.AsList().Count == 0)
                return null;

            if (clients.AsList().Count > 1)
                throw new Exception("More than 1 client record exists for the given API key which is not allowed.");

            return clients.AsList()[0];
        }
    }
}
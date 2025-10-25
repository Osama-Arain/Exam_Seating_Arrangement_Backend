using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using payday_server.Views.Shared;
using Dapper;
using Microsoft.Data.SqlClient;

namespace payday_server.DapperHelper
{
    public class DapperOperation
    {
        public string connectionString { get; set; }
        public IDbConnection dbcon {get; set;}
        public DapperOperation()
        {
             var configuration = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", false)
                .Build ();
            
            connectionString = configuration.GetConnectionString ("Dapper").ToString ();
            dbcon = new SqlConnection(connectionString);   
        }

        public async Task<IEnumerable<object>> SampleHelperFunction(DateTime _month)
        {
            try
            {
                string query = @" sample ";


                return await dbcon.QueryAsync<UserEventLogsViewModel>(query, new { SelectedMonth = _month });
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
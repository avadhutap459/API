using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestaAdminApi.DatabaseLayer
{
    public sealed class ClsDbConnection
    {
        public static string appDirectory = System.Environment.CurrentDirectory;
        public static string env = string.Empty;
        public static IConfiguration configuration;
        private static ClsDbConnection _Instance;
        private static readonly object lockobject = new object();
        private ClsDbConnection()
        {
            var config = new ConfigurationBuilder().SetBasePath(appDirectory).AddJsonFile("appsettings.json").Build();

            env = config.GetSection("Env").Value;

            configuration = new ConfigurationBuilder().SetBasePath(appDirectory).
                AddJsonFile($"appsettings.{env}.json", optional: false).Build();
        }

        public static ClsDbConnection Instance
        {
            get
            {
                lock(lockobject)
                {
                    if(_Instance == null)
                        _Instance = new ClsDbConnection();
                }
                return _Instance;
            }
        }

        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(configuration["ConnectionString:DefaultConnection"]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NLog;

namespace WebAppForAppServiceTestMysqlConnection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MySQLController : ControllerBase
    {

        static Logger logger = LogManager.GetCurrentClassLogger();


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MySQLController> _logger;

        public MySQLController(ILogger<MySQLController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// get data 
        /// </summary>
        /// <param name="serverName">serverName</param>
        /// <param name="dbName">dbName</param>
        /// <param name="userID">dbUser</param>
        /// <param name="userPsw">dbPsw</param>
        /// <returns></returns>
        [HttpGet]
        public string  Get(string serverName,string dbName,string userID,string userPsw)
        {
            if (string.IsNullOrEmpty(serverName)| string.IsNullOrEmpty(dbName)|string.IsNullOrEmpty(userID)|string.IsNullOrEmpty(userPsw))
            {
                return  $"need query parameter like:https://xxx.azurewebsites.net/mysql?servername=xxx&dbname=xxx&userid=xxx&userpsw=xxx";
            }
            DateTime startTime = DateTime.UtcNow;
            _logger.LogDebug("api start");

            logger.Info("NLog Strat API" + startTime);

            string   MySQLConn = $"data source={serverName};database={dbName};user id={userID}; password ={userPsw};";

           

            try
            {
               
                MySqlConnection conn = new MySqlConnection(MySQLConn);

                conn.Open();
                conn.Close();

               
            }
            catch (Exception ex)
            {

                return  $"mysql connection fail:{ex.Message}";
            }
            return $"mysql connection succefull,connection string: {MySQLConn}";

        }
    }
}

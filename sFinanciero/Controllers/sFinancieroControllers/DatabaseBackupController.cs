using sFinanciero.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sFinanciero.Controllers.sFinancieroControllers
{
    public class DatabaseBackupController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();
        // GET: api/DatabaseBackup
        public IEnumerable<string> Get()
        {
            // read connectionstring from config file
            var connectionString = ConfigurationManager.ConnectionStrings["sisFinancieroEntities"].ConnectionString;

            // read backup folder from config file ("C:/temp/")
            var backupFolder = AppContext.BaseDirectory+"DatabaseBackUp\\";

           // var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                backupFolder,db.Database.Connection.Database,
                DateTime.Now.ToString("yyyy-MM-dd"));
            
            using (var connection = db.Database.Connection as SqlConnection)
            {
                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    db.Database.Connection.Database, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                   // db.Database.ExecuteSqlCommand(command.CommandText, null);
                   
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }


            return new string[] { "value1", "value2" };
        }

        // GET: api/DatabaseBackup/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DatabaseBackup
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DatabaseBackup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DatabaseBackup/5
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace sFinanciero.Controllers
{
    public class BitacoraController : ApiController
    {
        // GET: api/Bitacora
        public IEnumerable<string> Get()
        {
           string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string[] lines = File.ReadAllLines(appPath + "\\LogFile.log", Encoding.UTF8);

            return lines;
        }

        // GET: api/Bitacora/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bitacora
        public void Post([FromBody]string value)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(value);
        }

        // PUT: api/Bitacora/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bitacora/5
        public void Delete(int id)
        {
        }
    }
}

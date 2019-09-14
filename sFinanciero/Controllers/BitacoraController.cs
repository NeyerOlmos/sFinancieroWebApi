using Newtonsoft.Json.Linq;
using sFinanciero.Services;
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
        public BitacoraService BitacoraService { get; }
        public BitacoraController()
        {
            BitacoraService = new BitacoraService();
        }
        // GET: api/Bitacora
        public IEnumerable<string> Get()
        {
            return BitacoraService.getBitacoraReverse();
        }

        // GET: api/Bitacora/5
        [HttpGet]
        [Route("api/Bitacora/BitacoraFile")]
        public IHttpActionResult GetBitacoraFile()
        {
            string logName = "sFianciero.log";
            //adding bytes to memory stream   
            var dataStream = BitacoraService.GetStream();
            return new BitacoraResult(dataStream, Request, logName);
            
        }


        // POST: api/Bitacora
        public void Post([FromBody]log log)
        {
            BitacoraService.Log(log.type, log.message);
        }
        public class log
        {
            public string type { get; set; }
            public string message { get; set; }
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

    public class BitacoraResult : IHttpActionResult
    {
        MemoryStream BitacoraStuff;
        string PdfFileName;
        HttpRequestMessage httpRequestMessage;
        HttpResponseMessage httpResponseMessage;
        public BitacoraResult(MemoryStream data, HttpRequestMessage request, string filename)
        {
            BitacoraStuff = data;
            httpRequestMessage = request;
            PdfFileName = filename;
        }
        public System.Threading.Tasks.Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
            httpResponseMessage.Content = new StreamContent(BitacoraStuff);
            //httpResponseMessage.Content = new ByteArrayContent(bookStuff.ToArray());  
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = PdfFileName;
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            return System.Threading.Tasks.Task.FromResult(httpResponseMessage);
        }
    }

}

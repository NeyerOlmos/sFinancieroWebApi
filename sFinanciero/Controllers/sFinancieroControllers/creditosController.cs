using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using sFinanciero.DAL;

namespace sFinanciero.Controllers.sFinancieroControllers
{
    public class creditosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/creditos
        public IQueryable<credito> Getcredito()
        {
            return db.credito;
        }

        // GET: api/creditos/5
        [ResponseType(typeof(credito))]
        public async Task<IHttpActionResult> Getcredito(int id)
        {
            credito credito = await db.credito.FindAsync(id);
            if (credito == null)
            {
                return NotFound();
            }

            return Ok(credito);
        }

        // PUT: api/creditos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcredito(int id, credito credito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != credito.id)
            {
                return BadRequest();
            }

            db.Entry(credito).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!creditoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/creditos
        [ResponseType(typeof(credito))]
        public async Task<IHttpActionResult> Postcredito(credito credito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.credito.Add(credito);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (creditoExists(credito.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = credito.id }, credito);
        }

        // DELETE: api/creditos/5
        [ResponseType(typeof(credito))]
        public async Task<IHttpActionResult> Deletecredito(int id)
        {
            credito credito = await db.credito.FindAsync(id);
            if (credito == null)
            {
                return NotFound();
            }

            db.credito.Remove(credito);
            await db.SaveChangesAsync();

            return Ok(credito);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool creditoExists(int id)
        {
            return db.credito.Count(e => e.id == id) > 0;
        }
    }
}
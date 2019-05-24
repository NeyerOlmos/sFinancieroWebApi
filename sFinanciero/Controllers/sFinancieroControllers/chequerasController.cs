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
    public class chequerasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/chequeras
        public IQueryable<chequera> Getchequera()
        {
            return db.chequera;
        }

        // GET: api/chequeras/5
        [ResponseType(typeof(chequera))]
        public async Task<IHttpActionResult> Getchequera(int id)
        {
            chequera chequera = await db.chequera.FindAsync(id);
            if (chequera == null)
            {
                return NotFound();
            }

            return Ok(chequera);
        }

        // PUT: api/chequeras/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putchequera(int id, chequera chequera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chequera.id)
            {
                return BadRequest();
            }

            db.Entry(chequera).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!chequeraExists(id))
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

        // POST: api/chequeras
        [ResponseType(typeof(chequera))]
        public async Task<IHttpActionResult> Postchequera(chequera chequera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.chequera.Add(chequera);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = chequera.id }, chequera);
        }

        // DELETE: api/chequeras/5
        [ResponseType(typeof(chequera))]
        public async Task<IHttpActionResult> Deletechequera(int id)
        {
            chequera chequera = await db.chequera.FindAsync(id);
            if (chequera == null)
            {
                return NotFound();
            }

            db.chequera.Remove(chequera);
            await db.SaveChangesAsync();

            return Ok(chequera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool chequeraExists(int id)
        {
            return db.chequera.Count(e => e.id == id) > 0;
        }
    }
}
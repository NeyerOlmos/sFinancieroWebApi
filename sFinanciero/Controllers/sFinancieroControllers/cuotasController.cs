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
    public class cuotasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/cuotas
        public IQueryable<cuota> Getcuota()
        {
            return db.cuota;
        }

        // GET: api/cuotas/5
        [ResponseType(typeof(cuota))]
        public async Task<IHttpActionResult> Getcuota(int id)
        {
            cuota cuota = await db.cuota.FindAsync(id);
            if (cuota == null)
            {
                return NotFound();
            }

            return Ok(cuota);
        }

        // PUT: api/cuotas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcuota(int id, cuota cuota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuota.id)
            {
                return BadRequest();
            }

            db.Entry(cuota).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cuotaExists(id))
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

        // POST: api/cuotas
        [ResponseType(typeof(cuota))]
        public async Task<IHttpActionResult> Postcuota(cuota cuota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cuota.Add(cuota);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (cuotaExists(cuota.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cuota.id }, cuota);
        }

        // DELETE: api/cuotas/5
        [ResponseType(typeof(cuota))]
        public async Task<IHttpActionResult> Deletecuota(int id)
        {
            cuota cuota = await db.cuota.FindAsync(id);
            if (cuota == null)
            {
                return NotFound();
            }

            db.cuota.Remove(cuota);
            await db.SaveChangesAsync();

            return Ok(cuota);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cuotaExists(int id)
        {
            return db.cuota.Count(e => e.id == id) > 0;
        }
    }
}
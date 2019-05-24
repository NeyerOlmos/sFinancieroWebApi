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
    public class detalleMovimientosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/detalleMovimientos
        public IQueryable<detalleMov> GetdetalleMov()
        {
            return db.detalleMov;
        }

        // GET: api/detalleMovimientos/5
        [ResponseType(typeof(detalleMov))]
        public async Task<IHttpActionResult> GetdetalleMov(int id)
        {
            detalleMov detalleMov = await db.detalleMov.FindAsync(id);
            if (detalleMov == null)
            {
                return NotFound();
            }

            return Ok(detalleMov);
        }

        // PUT: api/detalleMovimientos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutdetalleMov(int id, detalleMov detalleMov)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleMov.id)
            {
                return BadRequest();
            }

            db.Entry(detalleMov).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detalleMovExists(id))
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

        // POST: api/detalleMovimientos
        [ResponseType(typeof(detalleMov))]
        public async Task<IHttpActionResult> PostdetalleMov(detalleMov detalleMov)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.detalleMov.Add(detalleMov);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = detalleMov.id }, detalleMov);
        }

        // DELETE: api/detalleMovimientos/5
        [ResponseType(typeof(detalleMov))]
        public async Task<IHttpActionResult> DeletedetalleMov(int id)
        {
            detalleMov detalleMov = await db.detalleMov.FindAsync(id);
            if (detalleMov == null)
            {
                return NotFound();
            }

            db.detalleMov.Remove(detalleMov);
            await db.SaveChangesAsync();

            return Ok(detalleMov);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool detalleMovExists(int id)
        {
            return db.detalleMov.Count(e => e.id == id) > 0;
        }
    }
}
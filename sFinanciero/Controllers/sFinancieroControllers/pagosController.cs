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
    public class pagosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/pagos
        public IQueryable<pago> Getpago()
        {
            return db.pago;
        }

        // GET: api/pagos/5
        [ResponseType(typeof(pago))]
        public async Task<IHttpActionResult> Getpago(int id)
        {
            pago pago = await db.pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            return Ok(pago);
        }

        // PUT: api/pagos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putpago(int id, pago pago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pago.id)
            {
                return BadRequest();
            }

            db.Entry(pago).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pagoExists(id))
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

        // POST: api/pagos
        [ResponseType(typeof(pago))]
        public async Task<IHttpActionResult> Postpago(pago pago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pago.Add(pago);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (pagoExists(pago.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pago.id }, pago);
        }

        // DELETE: api/pagos/5
        [ResponseType(typeof(pago))]
        public async Task<IHttpActionResult> Deletepago(int id)
        {
            pago pago = await db.pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            db.pago.Remove(pago);
            await db.SaveChangesAsync();

            return Ok(pago);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool pagoExists(int id)
        {
            return db.pago.Count(e => e.id == id) > 0;
        }
    }
}
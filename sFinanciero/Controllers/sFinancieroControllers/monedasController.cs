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
    public class monedasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/monedas
        public IQueryable<moneda> Getmoneda()
        {
            return db.moneda;
        }

        // GET: api/monedas/5
        [ResponseType(typeof(moneda))]
        public async Task<IHttpActionResult> Getmoneda(int id)
        {
            moneda moneda = await db.moneda.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            return Ok(moneda);
        }

        // PUT: api/monedas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmoneda(int id, moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moneda.id)
            {
                return BadRequest();
            }

            db.Entry(moneda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!monedaExists(id))
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

        // POST: api/monedas
        [ResponseType(typeof(moneda))]
        public async Task<IHttpActionResult> Postmoneda(moneda moneda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.moneda.Add(moneda);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (monedaExists(moneda.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = moneda.id }, moneda);
        }

        // DELETE: api/monedas/5
        [ResponseType(typeof(moneda))]
        public async Task<IHttpActionResult> Deletemoneda(int id)
        {
            moneda moneda = await db.moneda.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            db.moneda.Remove(moneda);
            await db.SaveChangesAsync();

            return Ok(moneda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool monedaExists(int id)
        {
            return db.moneda.Count(e => e.id == id) > 0;
        }
    }
}
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
    public class tarjetasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tarjetas
        public IQueryable<tarjeta> Gettarjeta()
        {
            return db.tarjeta;
        }

        // GET: api/tarjetas/5
        [ResponseType(typeof(tarjeta))]
        public async Task<IHttpActionResult> Gettarjeta(int id)
        {
            tarjeta tarjeta = await db.tarjeta.FindAsync(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            return Ok(tarjeta);
        }

        // PUT: api/tarjetas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttarjeta(int id, tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarjeta.id)
            {
                return BadRequest();
            }

            db.Entry(tarjeta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tarjetaExists(id))
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

        // POST: api/tarjetas
        [ResponseType(typeof(tarjeta))]
        public async Task<IHttpActionResult> Posttarjeta(tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tarjeta.Add(tarjeta);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tarjetaExists(tarjeta.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tarjeta.id }, tarjeta);
        }

        // DELETE: api/tarjetas/5
        [ResponseType(typeof(tarjeta))]
        public async Task<IHttpActionResult> Deletetarjeta(int id)
        {
            tarjeta tarjeta = await db.tarjeta.FindAsync(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            db.tarjeta.Remove(tarjeta);
            await db.SaveChangesAsync();

            return Ok(tarjeta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tarjetaExists(int id)
        {
            return db.tarjeta.Count(e => e.id == id) > 0;
        }
    }
}
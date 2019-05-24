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
    public class tipoTarjetasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tipoTarjetas
        public IQueryable<tipoTarjeta> GettipoTarjeta()
        {
            return db.tipoTarjeta;
        }

        // GET: api/tipoTarjetas/5
        [ResponseType(typeof(tipoTarjeta))]
        public async Task<IHttpActionResult> GettipoTarjeta(int id)
        {
            tipoTarjeta tipoTarjeta = await db.tipoTarjeta.FindAsync(id);
            if (tipoTarjeta == null)
            {
                return NotFound();
            }

            return Ok(tipoTarjeta);
        }

        // PUT: api/tipoTarjetas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttipoTarjeta(int id, tipoTarjeta tipoTarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoTarjeta.id)
            {
                return BadRequest();
            }

            db.Entry(tipoTarjeta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoTarjetaExists(id))
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

        // POST: api/tipoTarjetas
        [ResponseType(typeof(tipoTarjeta))]
        public async Task<IHttpActionResult> PosttipoTarjeta(tipoTarjeta tipoTarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoTarjeta.Add(tipoTarjeta);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tipoTarjetaExists(tipoTarjeta.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoTarjeta.id }, tipoTarjeta);
        }

        // DELETE: api/tipoTarjetas/5
        [ResponseType(typeof(tipoTarjeta))]
        public async Task<IHttpActionResult> DeletetipoTarjeta(int id)
        {
            tipoTarjeta tipoTarjeta = await db.tipoTarjeta.FindAsync(id);
            if (tipoTarjeta == null)
            {
                return NotFound();
            }

            db.tipoTarjeta.Remove(tipoTarjeta);
            await db.SaveChangesAsync();

            return Ok(tipoTarjeta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoTarjetaExists(int id)
        {
            return db.tipoTarjeta.Count(e => e.id == id) > 0;
        }
    }
}
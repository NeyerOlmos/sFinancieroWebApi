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
    public class transaccionesController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/transacciones
        public IQueryable<transaccion> Gettransaccion()
        {
            return db.transaccion;
        }

        // GET: api/transacciones/5
        [ResponseType(typeof(transaccion))]
        public async Task<IHttpActionResult> Gettransaccion(int id)
        {
            transaccion transaccion = await db.transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return Ok(transaccion);
        }

        // PUT: api/transacciones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttransaccion(int id, transaccion transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaccion.id)
            {
                return BadRequest();
            }

            db.Entry(transaccion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!transaccionExists(id))
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

        // POST: api/transacciones
        [ResponseType(typeof(transaccion))]
        public async Task<IHttpActionResult> Posttransaccion(transaccion transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.transaccion.Add(transaccion);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (transaccionExists(transaccion.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = transaccion.id }, transaccion);
        }

        // DELETE: api/transacciones/5
        [ResponseType(typeof(transaccion))]
        public async Task<IHttpActionResult> Deletetransaccion(int id)
        {
            transaccion transaccion = await db.transaccion.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            db.transaccion.Remove(transaccion);
            await db.SaveChangesAsync();

            return Ok(transaccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool transaccionExists(int id)
        {
            return db.transaccion.Count(e => e.id == id) > 0;
        }
    }
}
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
    public class detalleTransaccionesController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/detalleTransacciones
        public IQueryable<detalleTrans> GetdetalleTrans()
        {
            return db.detalleTrans;
        }

        // GET: api/detalleTransacciones/5
        [ResponseType(typeof(detalleTrans))]
        public async Task<IHttpActionResult> GetdetalleTrans(int id)
        {
            detalleTrans detalleTrans = await db.detalleTrans.FindAsync(id);
            if (detalleTrans == null)
            {
                return NotFound();
            }

            return Ok(detalleTrans);
        }

        // PUT: api/detalleTransacciones/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutdetalleTrans(int id, detalleTrans detalleTrans)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleTrans.id)
            {
                return BadRequest();
            }

            db.Entry(detalleTrans).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detalleTransExists(id))
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

        // POST: api/detalleTransacciones
        [ResponseType(typeof(detalleTrans))]
        public async Task<IHttpActionResult> PostdetalleTrans(detalleTrans detalleTrans)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.detalleTrans.Add(detalleTrans);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (detalleTransExists(detalleTrans.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = detalleTrans.id }, detalleTrans);
        }

        // DELETE: api/detalleTransacciones/5
        [ResponseType(typeof(detalleTrans))]
        public async Task<IHttpActionResult> DeletedetalleTrans(int id)
        {
            detalleTrans detalleTrans = await db.detalleTrans.FindAsync(id);
            if (detalleTrans == null)
            {
                return NotFound();
            }

            db.detalleTrans.Remove(detalleTrans);
            await db.SaveChangesAsync();

            return Ok(detalleTrans);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool detalleTransExists(int id)
        {
            return db.detalleTrans.Count(e => e.id == id) > 0;
        }
    }
}
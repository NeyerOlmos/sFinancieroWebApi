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
    public class detalleServiciosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/detalleServicios
        public IQueryable<detalleServicio> GetdetalleServicio()
        {
            return db.detalleServicio;
        }

        // GET: api/detalleServicios/5
        [ResponseType(typeof(detalleServicio))]
        public async Task<IHttpActionResult> GetdetalleServicio(int id)
        {
            detalleServicio detalleServicio = await db.detalleServicio.FindAsync(id);
            if (detalleServicio == null)
            {
                return NotFound();
            }

            return Ok(detalleServicio);
        }

        // PUT: api/detalleServicios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutdetalleServicio(int id, detalleServicio detalleServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalleServicio.id)
            {
                return BadRequest();
            }

            db.Entry(detalleServicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detalleServicioExists(id))
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

        // POST: api/detalleServicios
        [ResponseType(typeof(detalleServicio))]
        public async Task<IHttpActionResult> PostdetalleServicio(detalleServicio detalleServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.detalleServicio.Add(detalleServicio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (detalleServicioExists(detalleServicio.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = detalleServicio.id }, detalleServicio);
        }

        // DELETE: api/detalleServicios/5
        [ResponseType(typeof(detalleServicio))]
        public async Task<IHttpActionResult> DeletedetalleServicio(int id)
        {
            detalleServicio detalleServicio = await db.detalleServicio.FindAsync(id);
            if (detalleServicio == null)
            {
                return NotFound();
            }

            db.detalleServicio.Remove(detalleServicio);
            await db.SaveChangesAsync();

            return Ok(detalleServicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool detalleServicioExists(int id)
        {
            return db.detalleServicio.Count(e => e.id == id) > 0;
        }
    }
}
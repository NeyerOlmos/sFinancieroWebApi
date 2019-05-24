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
    public class tipoSolicitudController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tipoSolicitud
        public IQueryable<tipoSolicitud> GettipoSolicitud()
        {
            return db.tipoSolicitud;
        }

        // GET: api/tipoSolicitud/5
        [ResponseType(typeof(tipoSolicitud))]
        public async Task<IHttpActionResult> GettipoSolicitud(int id)
        {
            tipoSolicitud tipoSolicitud = await db.tipoSolicitud.FindAsync(id);
            if (tipoSolicitud == null)
            {
                return NotFound();
            }

            return Ok(tipoSolicitud);
        }

        // PUT: api/tipoSolicitud/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttipoSolicitud(int id, tipoSolicitud tipoSolicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoSolicitud.id)
            {
                return BadRequest();
            }

            db.Entry(tipoSolicitud).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoSolicitudExists(id))
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

        // POST: api/tipoSolicitud
        [ResponseType(typeof(tipoSolicitud))]
        public async Task<IHttpActionResult> PosttipoSolicitud(tipoSolicitud tipoSolicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoSolicitud.Add(tipoSolicitud);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tipoSolicitudExists(tipoSolicitud.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoSolicitud.id }, tipoSolicitud);
        }

        // DELETE: api/tipoSolicitud/5
        [ResponseType(typeof(tipoSolicitud))]
        public async Task<IHttpActionResult> DeletetipoSolicitud(int id)
        {
            tipoSolicitud tipoSolicitud = await db.tipoSolicitud.FindAsync(id);
            if (tipoSolicitud == null)
            {
                return NotFound();
            }

            db.tipoSolicitud.Remove(tipoSolicitud);
            await db.SaveChangesAsync();

            return Ok(tipoSolicitud);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoSolicitudExists(int id)
        {
            return db.tipoSolicitud.Count(e => e.id == id) > 0;
        }
    }
}
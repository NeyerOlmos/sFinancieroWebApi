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
    public class solicitudController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/solicitud
        public IQueryable<solicitud> Getsolicitud()
        {
            return db.solicitud;
        }

        // GET: api/solicitud/5
        [ResponseType(typeof(solicitud))]
        public async Task<IHttpActionResult> Getsolicitud(int id)
        {
            solicitud solicitud = await db.solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            return Ok(solicitud);
        }

        // PUT: api/solicitud/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putsolicitud(int id, solicitud solicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitud.id)
            {
                return BadRequest();
            }

            db.Entry(solicitud).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!solicitudExists(id))
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

        // POST: api/solicitud
        [ResponseType(typeof(solicitud))]
        public async Task<IHttpActionResult> Postsolicitud(solicitud solicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.solicitud.Add(solicitud);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (solicitudExists(solicitud.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = solicitud.id }, solicitud);
        }

        // DELETE: api/solicitud/5
        [ResponseType(typeof(solicitud))]
        public async Task<IHttpActionResult> Deletesolicitud(int id)
        {
            solicitud solicitud = await db.solicitud.FindAsync(id);
            if (solicitud == null)
            {
                return NotFound();
            }

            db.solicitud.Remove(solicitud);
            await db.SaveChangesAsync();

            return Ok(solicitud);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool solicitudExists(int id)
        {
            return db.solicitud.Count(e => e.id == id) > 0;
        }
    }
}
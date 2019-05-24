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
    public class tipoServiciosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tipoServicios
        public IQueryable<tipoServicio> GettipoServicio()
        {
            return db.tipoServicio;
        }

        // GET: api/tipoServicios/5
        [ResponseType(typeof(tipoServicio))]
        public async Task<IHttpActionResult> GettipoServicio(int id)
        {
            tipoServicio tipoServicio = await db.tipoServicio.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            return Ok(tipoServicio);
        }

        // PUT: api/tipoServicios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttipoServicio(int id, tipoServicio tipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoServicio.id)
            {
                return BadRequest();
            }

            db.Entry(tipoServicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoServicioExists(id))
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

        // POST: api/tipoServicios
        [ResponseType(typeof(tipoServicio))]
        public async Task<IHttpActionResult> PosttipoServicio(tipoServicio tipoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoServicio.Add(tipoServicio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tipoServicioExists(tipoServicio.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoServicio.id }, tipoServicio);
        }

        // DELETE: api/tipoServicios/5
        [ResponseType(typeof(tipoServicio))]
        public async Task<IHttpActionResult> DeletetipoServicio(int id)
        {
            tipoServicio tipoServicio = await db.tipoServicio.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            db.tipoServicio.Remove(tipoServicio);
            await db.SaveChangesAsync();

            return Ok(tipoServicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoServicioExists(int id)
        {
            return db.tipoServicio.Count(e => e.id == id) > 0;
        }
    }
}
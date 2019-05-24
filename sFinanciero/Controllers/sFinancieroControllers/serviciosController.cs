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
    public class serviciosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/servicios
        public IQueryable<servicio> Getservicio()
        {
            return db.servicio;
        }

        // GET: api/servicios/5
        [ResponseType(typeof(servicio))]
        public async Task<IHttpActionResult> Getservicio(int id)
        {
            servicio servicio = await db.servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            return Ok(servicio);
        }

        // PUT: api/servicios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putservicio(int id, servicio servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != servicio.id)
            {
                return BadRequest();
            }

            db.Entry(servicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!servicioExists(id))
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

        // POST: api/servicios
        [ResponseType(typeof(servicio))]
        public async Task<IHttpActionResult> Postservicio(servicio servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.servicio.Add(servicio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (servicioExists(servicio.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = servicio.id }, servicio);
        }

        // DELETE: api/servicios/5
        [ResponseType(typeof(servicio))]
        public async Task<IHttpActionResult> Deleteservicio(int id)
        {
            servicio servicio = await db.servicio.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            db.servicio.Remove(servicio);
            await db.SaveChangesAsync();

            return Ok(servicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool servicioExists(int id)
        {
            return db.servicio.Count(e => e.id == id) > 0;
        }
    }
}
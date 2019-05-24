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
    public class sociosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/socios
        public IQueryable<socio> Getsocio()
        {
            return db.socio;
        }

        // GET: api/socios/5
        [ResponseType(typeof(socio))]
        public async Task<IHttpActionResult> Getsocio(int id)
        {
            socio socio = await db.socio.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }

            return Ok(socio);
        }

        // PUT: api/socios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putsocio(int id, socio socio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socio.id)
            {
                return BadRequest();
            }

            db.Entry(socio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!socioExists(id))
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

        // POST: api/socios
        [ResponseType(typeof(socio))]
        public async Task<IHttpActionResult> Postsocio(socio socio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.socio.Add(socio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (socioExists(socio.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = socio.id }, socio);
        }

        // DELETE: api/socios/5
        [ResponseType(typeof(socio))]
        public async Task<IHttpActionResult> Deletesocio(int id)
        {
            socio socio = await db.socio.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }

            db.socio.Remove(socio);
            await db.SaveChangesAsync();

            return Ok(socio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool socioExists(int id)
        {
            return db.socio.Count(e => e.id == id) > 0;
        }
    }
}
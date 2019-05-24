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
    public class gruposController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/grupos
        public IQueryable<grupo> Getgrupo()
        {
            return db.grupo;
        }

        // GET: api/grupos/5
        [ResponseType(typeof(grupo))]
        public async Task<IHttpActionResult> Getgrupo(int id)
        {
            grupo grupo = await db.grupo.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }

            return Ok(grupo);
        }

        // PUT: api/grupos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putgrupo(int id, grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupo.id)
            {
                return BadRequest();
            }

            db.Entry(grupo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!grupoExists(id))
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

        // POST: api/grupos
        [ResponseType(typeof(grupo))]
        public async Task<IHttpActionResult> Postgrupo(grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.grupo.Add(grupo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (grupoExists(grupo.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grupo.id }, grupo);
        }

        // DELETE: api/grupos/5
        [ResponseType(typeof(grupo))]
        public async Task<IHttpActionResult> Deletegrupo(int id)
        {
            grupo grupo = await db.grupo.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }

            db.grupo.Remove(grupo);
            await db.SaveChangesAsync();

            return Ok(grupo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool grupoExists(int id)
        {
            return db.grupo.Count(e => e.id == id) > 0;
        }
    }
}
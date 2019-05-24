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
    public class oficinasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/oficinas
        public IQueryable<oficina> Getoficina()
        {
            return db.oficina;
        }

        // GET: api/oficinas/5
        [ResponseType(typeof(oficina))]
        public async Task<IHttpActionResult> Getoficina(int id)
        {
            oficina oficina = await db.oficina.FindAsync(id);
            if (oficina == null)
            {
                return NotFound();
            }

            return Ok(oficina);
        }

        // PUT: api/oficinas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putoficina(int id, oficina oficina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != oficina.id_Of)
            {
                return BadRequest();
            }

            db.Entry(oficina).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!oficinaExists(id))
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

        // POST: api/oficinas
        [ResponseType(typeof(oficina))]
        public async Task<IHttpActionResult> Postoficina(oficina oficina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.oficina.Add(oficina);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (oficinaExists(oficina.id_Of))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = oficina.id_Of }, oficina);
        }

        // DELETE: api/oficinas/5
        [ResponseType(typeof(oficina))]
        public async Task<IHttpActionResult> Deleteoficina(int id)
        {
            oficina oficina = await db.oficina.FindAsync(id);
            if (oficina == null)
            {
                return NotFound();
            }

            db.oficina.Remove(oficina);
            await db.SaveChangesAsync();

            return Ok(oficina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool oficinaExists(int id)
        {
            return db.oficina.Count(e => e.id_Of == id) > 0;
        }
    }
}
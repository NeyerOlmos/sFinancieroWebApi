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
    public class retirosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/retiros
        public IQueryable<retiro> Getretiro()
        {
            return db.retiro;
        }

        // GET: api/retiros/5
        [ResponseType(typeof(retiro))]
        public async Task<IHttpActionResult> Getretiro(int id)
        {
            retiro retiro = await db.retiro.FindAsync(id);
            if (retiro == null)
            {
                return NotFound();
            }

            return Ok(retiro);
        }

        // PUT: api/retiros/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putretiro(int id, retiro retiro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != retiro.id)
            {
                return BadRequest();
            }

            db.Entry(retiro).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!retiroExists(id))
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

        // POST: api/retiros
        [ResponseType(typeof(retiro))]
        public async Task<IHttpActionResult> Postretiro(retiro retiro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.retiro.Add(retiro);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (retiroExists(retiro.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = retiro.id }, retiro);
        }

        // DELETE: api/retiros/5
        [ResponseType(typeof(retiro))]
        public async Task<IHttpActionResult> Deleteretiro(int id)
        {
            retiro retiro = await db.retiro.FindAsync(id);
            if (retiro == null)
            {
                return NotFound();
            }

            db.retiro.Remove(retiro);
            await db.SaveChangesAsync();

            return Ok(retiro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool retiroExists(int id)
        {
            return db.retiro.Count(e => e.id == id) > 0;
        }
    }
}
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
    public class depositoesController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/depositoes
        public IQueryable<deposito> Getdeposito()
        {
            return db.deposito;
        }

        // GET: api/depositoes/5
        [ResponseType(typeof(deposito))]
        public async Task<IHttpActionResult> Getdeposito(int id)
        {
            deposito deposito = await db.deposito.FindAsync(id);
            if (deposito == null)
            {
                return NotFound();
            }

            return Ok(deposito);
        }

        // PUT: api/depositoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdeposito(int id, deposito deposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deposito.id)
            {
                return BadRequest();
            }

            db.Entry(deposito).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!depositoExists(id))
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

        // POST: api/depositoes
        [ResponseType(typeof(deposito))]
        public async Task<IHttpActionResult> Postdeposito(deposito deposito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.deposito.Add(deposito);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (depositoExists(deposito.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deposito.id }, deposito);
        }

        // DELETE: api/depositoes/5
        [ResponseType(typeof(deposito))]
        public async Task<IHttpActionResult> Deletedeposito(int id)
        {
            deposito deposito = await db.deposito.FindAsync(id);
            if (deposito == null)
            {
                return NotFound();
            }

            db.deposito.Remove(deposito);
            await db.SaveChangesAsync();

            return Ok(deposito);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool depositoExists(int id)
        {
            return db.deposito.Count(e => e.id == id) > 0;
        }
    }
}
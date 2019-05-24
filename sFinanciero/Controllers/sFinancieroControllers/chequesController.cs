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
    public class chequesController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/cheques
        public IQueryable<cheque> Getcheque()
        {
            return db.cheque;
        }

        // GET: api/cheques/5
        [ResponseType(typeof(cheque))]
        public async Task<IHttpActionResult> Getcheque(int id)
        {
            cheque cheque = await db.cheque.FindAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }

            return Ok(cheque);
        }

        // PUT: api/cheques/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcheque(int id, cheque cheque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cheque.id)
            {
                return BadRequest();
            }

            db.Entry(cheque).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!chequeExists(id))
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

        // POST: api/cheques
        [ResponseType(typeof(cheque))]
        public async Task<IHttpActionResult> Postcheque(cheque cheque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cheque.Add(cheque);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cheque.id }, cheque);
        }

        // DELETE: api/cheques/5
        [ResponseType(typeof(cheque))]
        public async Task<IHttpActionResult> Deletecheque(int id)
        {
            cheque cheque = await db.cheque.FindAsync(id);
            if (cheque == null)
            {
                return NotFound();
            }

            db.cheque.Remove(cheque);
            await db.SaveChangesAsync();

            return Ok(cheque);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool chequeExists(int id)
        {
            return db.cheque.Count(e => e.id == id) > 0;
        }
    }
}
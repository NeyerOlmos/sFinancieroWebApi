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
    public class tipoDPFsController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tipoDPFs
        public IQueryable<tipoDPF> GettipoDPF()
        {
            return db.tipoDPF;
        }

        // GET: api/tipoDPFs/5
        [ResponseType(typeof(tipoDPF))]
        public async Task<IHttpActionResult> GettipoDPF(int id)
        {
            tipoDPF tipoDPF = await db.tipoDPF.FindAsync(id);
            if (tipoDPF == null)
            {
                return NotFound();
            }

            return Ok(tipoDPF);
        }

        // PUT: api/tipoDPFs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttipoDPF(int id, tipoDPF tipoDPF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoDPF.id)
            {
                return BadRequest();
            }

            db.Entry(tipoDPF).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoDPFExists(id))
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

        // POST: api/tipoDPFs
        [ResponseType(typeof(tipoDPF))]
        public async Task<IHttpActionResult> PosttipoDPF(tipoDPF tipoDPF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoDPF.Add(tipoDPF);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tipoDPFExists(tipoDPF.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoDPF.id }, tipoDPF);
        }

        // DELETE: api/tipoDPFs/5
        [ResponseType(typeof(tipoDPF))]
        public async Task<IHttpActionResult> DeletetipoDPF(int id)
        {
            tipoDPF tipoDPF = await db.tipoDPF.FindAsync(id);
            if (tipoDPF == null)
            {
                return NotFound();
            }

            db.tipoDPF.Remove(tipoDPF);
            await db.SaveChangesAsync();

            return Ok(tipoDPF);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoDPFExists(int id)
        {
            return db.tipoDPF.Count(e => e.id == id) > 0;
        }
    }
}
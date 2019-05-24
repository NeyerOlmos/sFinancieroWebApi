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
    public class DPFsController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/DPFs
        public IQueryable<DPF> GetDPF()
        {
            return db.DPF;
        }

        // GET: api/DPFs/5
        [ResponseType(typeof(DPF))]
        public async Task<IHttpActionResult> GetDPF(int id)
        {
            DPF dPF = await db.DPF.FindAsync(id);
            if (dPF == null)
            {
                return NotFound();
            }

            return Ok(dPF);
        }

        // PUT: api/DPFs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDPF(int id, DPF dPF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dPF.id)
            {
                return BadRequest();
            }

            db.Entry(dPF).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DPFExists(id))
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

        // POST: api/DPFs
        [ResponseType(typeof(DPF))]
        public async Task<IHttpActionResult> PostDPF(DPF dPF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DPF.Add(dPF);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DPFExists(dPF.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dPF.id }, dPF);
        }

        // DELETE: api/DPFs/5
        [ResponseType(typeof(DPF))]
        public async Task<IHttpActionResult> DeleteDPF(int id)
        {
            DPF dPF = await db.DPF.FindAsync(id);
            if (dPF == null)
            {
                return NotFound();
            }

            db.DPF.Remove(dPF);
            await db.SaveChangesAsync();

            return Ok(dPF);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DPFExists(int id)
        {
            return db.DPF.Count(e => e.id == id) > 0;
        }
    }
}
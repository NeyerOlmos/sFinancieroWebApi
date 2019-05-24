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
    public class tipoCambiosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tipoCambios
        public IQueryable<tipoCambio> GettipoCambio()
        {
            return db.tipoCambio;
        }

        // GET: api/tipoCambios/5
        [ResponseType(typeof(tipoCambio))]
        public async Task<IHttpActionResult> GettipoCambio(int id)
        {
            tipoCambio tipoCambio = await db.tipoCambio.FindAsync(id);
            if (tipoCambio == null)
            {
                return NotFound();
            }

            return Ok(tipoCambio);
        }

        // PUT: api/tipoCambios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttipoCambio(int id, tipoCambio tipoCambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoCambio.id)
            {
                return BadRequest();
            }

            db.Entry(tipoCambio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoCambioExists(id))
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

        // POST: api/tipoCambios
        [ResponseType(typeof(tipoCambio))]
        public async Task<IHttpActionResult> PosttipoCambio(tipoCambio tipoCambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoCambio.Add(tipoCambio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tipoCambioExists(tipoCambio.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoCambio.id }, tipoCambio);
        }

        // DELETE: api/tipoCambios/5
        [ResponseType(typeof(tipoCambio))]
        public async Task<IHttpActionResult> DeletetipoCambio(int id)
        {
            tipoCambio tipoCambio = await db.tipoCambio.FindAsync(id);
            if (tipoCambio == null)
            {
                return NotFound();
            }

            db.tipoCambio.Remove(tipoCambio);
            await db.SaveChangesAsync();

            return Ok(tipoCambio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoCambioExists(int id)
        {
            return db.tipoCambio.Count(e => e.id == id) > 0;
        }
    }
}
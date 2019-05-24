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
    public class tipoCuentasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tipoCuentas
        public IQueryable<tipoCuenta> GettipoCuenta()
        {
            return db.tipoCuenta;
        }

        // GET: api/tipoCuentas/5
        [ResponseType(typeof(tipoCuenta))]
        public async Task<IHttpActionResult> GettipoCuenta(int id)
        {
            tipoCuenta tipoCuenta = await db.tipoCuenta.FindAsync(id);
            if (tipoCuenta == null)
            {
                return NotFound();
            }

            return Ok(tipoCuenta);
        }

        // PUT: api/tipoCuentas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttipoCuenta(int id, tipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoCuenta.id)
            {
                return BadRequest();
            }

            db.Entry(tipoCuenta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoCuentaExists(id))
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

        // POST: api/tipoCuentas
        [ResponseType(typeof(tipoCuenta))]
        public async Task<IHttpActionResult> PosttipoCuenta(tipoCuenta tipoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoCuenta.Add(tipoCuenta);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tipoCuentaExists(tipoCuenta.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoCuenta.id }, tipoCuenta);
        }

        // DELETE: api/tipoCuentas/5
        [ResponseType(typeof(tipoCuenta))]
        public async Task<IHttpActionResult> DeletetipoCuenta(int id)
        {
            tipoCuenta tipoCuenta = await db.tipoCuenta.FindAsync(id);
            if (tipoCuenta == null)
            {
                return NotFound();
            }

            db.tipoCuenta.Remove(tipoCuenta);
            await db.SaveChangesAsync();

            return Ok(tipoCuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoCuentaExists(int id)
        {
            return db.tipoCuenta.Count(e => e.id == id) > 0;
        }
    }
}
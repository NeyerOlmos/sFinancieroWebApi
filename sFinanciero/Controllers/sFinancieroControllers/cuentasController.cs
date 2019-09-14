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
using Newtonsoft.Json.Linq;
using sFinanciero.DAL;

namespace sFinanciero.Controllers.sFinancieroControllers
{
    public class cuentasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/cuentas
        public IQueryable<cuenta> Getcuenta()
        {
            return db.cuenta;
        }

        // GET: api/cuentas/5
        [ResponseType(typeof(cuenta))]
        public async Task<IHttpActionResult> Getcuenta(int id)
        {
            cuenta cuenta = await db.cuenta.FindAsync(id);



            if (cuenta == null)
            {
                return NotFound();
            }


            cuenta.cliente = await db.cliente.Where(c => c.id == cuenta.idcliente).FirstOrDefaultAsync();
            cuenta.tipoCuenta = await db.tipoCuenta.Where(tc => tc.id == cuenta.idTipoCuenta).FirstOrDefaultAsync();
            cuenta.moneda = await db.moneda.Where(m => m.id == cuenta.idMoneda).FirstOrDefaultAsync();

            return Ok(cuenta);
        }
        

        // PUT: api/cuentas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcuenta(int id, cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuenta.id)
            {
                return BadRequest();
            }

            db.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cuentaExists(id))
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

        // POST: api/cuentas
        [ResponseType(typeof(cuenta))]
        public async Task<IHttpActionResult> Postcuenta(cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            cuenta.cliente = await db.cliente.Where(c => c.id == cuenta.idcliente).FirstOrDefaultAsync();
            cuenta.tipoCuenta = await db.tipoCuenta.Where(tc => tc.id == cuenta.idTipoCuenta).FirstOrDefaultAsync();
            cuenta.moneda = await db.moneda.Where(m => m.id == cuenta.idMoneda).FirstOrDefaultAsync();
            



            db.cuenta.Add(cuenta);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (cuentaExists(cuenta.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cuenta.id }, cuenta);
        }

        // DELETE: api/cuentas/5
        [ResponseType(typeof(cuenta))]
        public async Task<IHttpActionResult> Deletecuenta(int id)
        {
            cuenta cuenta = await db.cuenta.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            db.cuenta.Remove(cuenta);
            await db.SaveChangesAsync();

            return Ok(cuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cuentaExists(int id)
        {
            return db.cuenta.Count(e => e.id == id) > 0;
        }
    }
}
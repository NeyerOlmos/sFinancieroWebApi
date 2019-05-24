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
    public class planPagosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/planPagos
        public IQueryable<planPago> GetplanPago()
        {
            return db.planPago;
        }

        // GET: api/planPagos/5
        [ResponseType(typeof(planPago))]
        public async Task<IHttpActionResult> GetplanPago(int id)
        {
            planPago planPago = await db.planPago.FindAsync(id);
            if (planPago == null)
            {
                return NotFound();
            }

            return Ok(planPago);
        }

        // PUT: api/planPagos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutplanPago(int id, planPago planPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != planPago.id)
            {
                return BadRequest();
            }

            db.Entry(planPago).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!planPagoExists(id))
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

        // POST: api/planPagos
        [ResponseType(typeof(planPago))]
        public async Task<IHttpActionResult> PostplanPago(planPago planPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.planPago.Add(planPago);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (planPagoExists(planPago.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = planPago.id }, planPago);
        }

        // DELETE: api/planPagos/5
        [ResponseType(typeof(planPago))]
        public async Task<IHttpActionResult> DeleteplanPago(int id)
        {
            planPago planPago = await db.planPago.FindAsync(id);
            if (planPago == null)
            {
                return NotFound();
            }

            db.planPago.Remove(planPago);
            await db.SaveChangesAsync();

            return Ok(planPago);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool planPagoExists(int id)
        {
            return db.planPago.Count(e => e.id == id) > 0;
        }
    }
}
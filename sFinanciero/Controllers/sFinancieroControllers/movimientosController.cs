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
    public class movimientosController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/movimientos
        public IQueryable<movimiento> Getmovimiento()
        {
            return db.movimiento;
        }

        // GET: api/movimientos/5
        [ResponseType(typeof(movimiento))]
        public async Task<IHttpActionResult> Getmovimiento(int id)
        {
            movimiento movimiento = await db.movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return Ok(movimiento);
        }

        // PUT: api/movimientos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmovimiento(int id, movimiento movimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movimiento.id)
            {
                return BadRequest();
            }

            db.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!movimientoExists(id))
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

        // POST: api/movimientos
        [ResponseType(typeof(movimiento))]
        public async Task<IHttpActionResult> Postmovimiento(movimiento movimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.movimiento.Add(movimiento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = movimiento.id }, movimiento);
        }

        // DELETE: api/movimientos/5
        [ResponseType(typeof(movimiento))]
        public async Task<IHttpActionResult> Deletemovimiento(int id)
        {
            movimiento movimiento = await db.movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            db.movimiento.Remove(movimiento);
            await db.SaveChangesAsync();

            return Ok(movimiento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool movimientoExists(int id)
        {
            return db.movimiento.Count(e => e.id == id) > 0;
        }
    }
}
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
    public class tipoClientesController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/tipoClientes
        public IQueryable<tipoCliente> GettipoCliente()
        {
            return db.tipoCliente;
        }

        // GET: api/tipoClientes/5
        [ResponseType(typeof(tipoCliente))]
        public async Task<IHttpActionResult> GettipoCliente(int id)
        {
            tipoCliente tipoCliente = await db.tipoCliente.FindAsync(id);
            if (tipoCliente == null)
            {
                return NotFound();
            }

            return Ok(tipoCliente);
        }

        // PUT: api/tipoClientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttipoCliente(int id, tipoCliente tipoCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoCliente.id)
            {
                return BadRequest();
            }

            db.Entry(tipoCliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoClienteExists(id))
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

        // POST: api/tipoClientes
        [ResponseType(typeof(tipoCliente))]
        public async Task<IHttpActionResult> PosttipoCliente(tipoCliente tipoCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoCliente.Add(tipoCliente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tipoClienteExists(tipoCliente.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tipoCliente.id }, tipoCliente);
        }

        // DELETE: api/tipoClientes/5
        [ResponseType(typeof(tipoCliente))]
        public async Task<IHttpActionResult> DeletetipoCliente(int id)
        {
            tipoCliente tipoCliente = await db.tipoCliente.FindAsync(id);
            if (tipoCliente == null)
            {
                return NotFound();
            }

            db.tipoCliente.Remove(tipoCliente);
            await db.SaveChangesAsync();

            return Ok(tipoCliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoClienteExists(int id)
        {
            return db.tipoCliente.Count(e => e.id == id) > 0;
        }
    }
}
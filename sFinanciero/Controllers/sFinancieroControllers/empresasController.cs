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
    public class empresasController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/empresas
        public IQueryable<empresa> Getempresa()
        {
            return db.empresa;
        }

        // GET: api/empresas/5
        [ResponseType(typeof(empresa))]
        public async Task<IHttpActionResult> Getempresa(int id)
        {
            empresa empresa = await db.empresa.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        // PUT: api/empresas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putempresa(int id, empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empresa.id)
            {
                return BadRequest();
            }

            db.Entry(empresa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!empresaExists(id))
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

        // POST: api/empresas
        [ResponseType(typeof(empresa))]
        public async Task<IHttpActionResult> Postempresa(empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.empresa.Add(empresa);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (empresaExists(empresa.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = empresa.id }, empresa);
        }

        // DELETE: api/empresas/5
        [ResponseType(typeof(empresa))]
        public async Task<IHttpActionResult> Deleteempresa(int id)
        {
            empresa empresa = await db.empresa.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            db.empresa.Remove(empresa);
            await db.SaveChangesAsync();

            return Ok(empresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool empresaExists(int id)
        {
            return db.empresa.Count(e => e.id == id) > 0;
        }
    }
}
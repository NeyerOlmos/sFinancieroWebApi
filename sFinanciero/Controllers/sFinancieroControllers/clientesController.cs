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
    
    public class clientesController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();

        // GET: api/clientes
        public IQueryable<cliente> Getcliente()
        {
            var foo= db.cliente.Include(c => c.persona).Include(c=>c.grupo).Include(c=>c.tipoCliente).Include(c=>c.cuenta).Include(c=>c.DPF).Include(c=>c.empresa).Include(c=>c.solicitud).AsQueryable();
            return foo; 
        }
        [Route("api/clientesHabiles")]
        [HttpGet]
        public IQueryable<cliente> GetclientesHabiles()
        {
            var foo= db.cliente.Include(c => c.persona).Include(c=>c.grupo).Include(c=>c.tipoCliente).Include(c=>c.cuenta).Include(c=>c.DPF).Include(c=>c.empresa).Include(c=>c.solicitud).Where(c => c.estado == "Habilitado").AsQueryable();
            return foo;
        }

        // GET: api/clientes/5
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Getcliente(int id)
        {
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            cliente.persona =await db.persona.Where(p => p.id == cliente.idPersona).FirstOrDefaultAsync();
            cliente.cuenta =await db.cuenta.Where(c => c.idcliente==cliente.id).ToListAsync();
            cliente.DPF = await db.DPF.Where(d => d.idCliente == cliente.id).ToListAsync();
            cliente.grupo = await db.grupo.Where(g => g.id == cliente.idGrupo).FirstOrDefaultAsync();
            cliente.solicitud = await db.solicitud.Where(s => s.idCliente == cliente.id).ToListAsync();
            cliente.tipoCliente = await db.tipoCliente.Where(tc => tc.id == cliente.idTipoC).FirstOrDefaultAsync();
            return Ok(cliente);
        }

        // PUT: api/clientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcliente(int id, cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.id)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
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

        // POST: api/clientes
        [ResponseType(typeof(cliente))]
        
        public async Task<IHttpActionResult> Postcliente(cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cliente.Add(cliente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (clienteExists(cliente.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.id }, cliente);
        }

        // DELETE: api/clientes/5
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Deletecliente(int id)
        {
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            cliente.estado = "Deleted";
 
            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clienteExists(int id)
        {
            return db.cliente.Count(e => e.id == id) > 0;
        }
    }
}
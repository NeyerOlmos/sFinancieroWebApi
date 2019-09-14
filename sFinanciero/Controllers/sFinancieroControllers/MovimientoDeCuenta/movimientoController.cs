using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using sFinanciero.DAL;
using sFinanciero.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sFinanciero.Controllers.sFinancieroControllers.MovimientoDeCuenta
{
    public class movimientoController : ApiController
    {
        private sisFinancieroEntities db = new sisFinancieroEntities();
        // GET: api/cuentas/ByClienteId/11
        // [ResponseType(typeof(cuenta))]
        [HttpPost]
        [Route("api/CuentasByClienteId")]
        public IHttpActionResult CuentasByClienteId(JObject jObject)
        {
              int idCliente = Convert.ToInt32(jObject.GetValue("id").ToString());
            cliente cliente =  db.cliente.Find(idCliente);
            var cuentas = db.cuenta.Where(c => c.idcliente==cliente.id );

            return Ok(cuentas);
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpPost]
        [Route("api/getIdClienteByUserName")]
        public IHttpActionResult getIdClienteByUserName(JObject jObject)
        {
              string username = jObject.GetValue("username").ToString();

            ApplicationUser user = UserManager.FindByName(username);

            return Ok(user.Id);
        }

        [HttpPost]
        [Route("api/RealizarTransferencia")]
        [Authorize(Roles ="Admin,Operador,Cliente")]
        public IHttpActionResult RealizarTransferencia(JObject jObject)
        {
            string nroCuenta1 = jObject.GetValue("nroCuenta1").ToString() ;
            string nroCuenta2 = jObject.GetValue("nroCuenta2").ToString(); ;
            int monto = Convert.ToInt32(jObject.GetValue("monto").ToString()); ;
            string descripcion = jObject.GetValue("descripcion").ToString();
            string oficina = jObject.GetValue("oficina").ToString();
            string ubicacion = jObject.GetValue("ubicacion").ToString();


            cuenta cuenta1=db.cuenta.FirstOrDefault(c => c.nroCuenta == nroCuenta1);
            cuenta cuenta2=db.cuenta.FirstOrDefault(c => c.nroCuenta == nroCuenta2);
            transaccion transaccion = new transaccion() {cuenta=cuenta1,cuenta1=cuenta2 };

            if (cuenta1.saldo>=monto)
            {
                cuenta1.saldo = cuenta1.saldo - monto;
                cuenta2.saldo = cuenta2.saldo + monto;
                db.Entry(cuenta1).State = EntityState.Modified;
                db.Entry(cuenta2).State = EntityState.Modified;
                transaccion.estado = "aceptado";

                db.transaccion.Add(transaccion);
                detalleTrans detalleTrans = new detalleTrans() { monto=monto,idMoneda=1,transaccion=transaccion};
                db.detalleTrans.Add(detalleTrans);


                movimiento movimiento = new movimiento() { cuenta=cuenta1,descripcion=descripcion,fechaHora=DateTime.Now,monto=monto};
                db.movimiento.Add(movimiento);
                detalleMov detalleMov = new detalleMov() {movimiento=movimiento,saldo=cuenta1.saldo - monto,oficina=oficina,ubicacion=ubicacion };
                db.detalleMov.Add(detalleMov);


                movimiento movimiento2 = new movimiento() { cuenta = cuenta2,monto=monto,descripcion=descripcion,fechaHora=DateTime.Now };
                db.movimiento.Add(movimiento2);
                detalleMov detalleMov2 = new detalleMov() { movimiento = movimiento2, saldo = cuenta2.saldo + monto, oficina = oficina, ubicacion = ubicacion };
                db.detalleMov.Add(detalleMov2);

                
                
            }
            else
            {
                transaccion.estado = "cancelado";
                db.transaccion.Add(transaccion);
                db.SaveChanges();
                return Content(HttpStatusCode.Conflict,"No hay suficientes fondos en la cuenta 1");
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Conflict();
            }

            return Ok();

        }


        //[HttpPost]
        //[Route("api/RealizarTransferencia")]
        //[Authorize(Roles = "Admin,Operador,Cliente")]
        //public IHttpActionResult RealizarTransferencia2(JObject jObject)
        //{ }

        [HttpPost]
        [Route("api/RealizarRetiro")]
        public IHttpActionResult RealizarRetiro(JObject jObject)
        {

            string nroCuenta = jObject.GetValue("nroCuenta").ToString();
            decimal monto =Convert.ToDecimal( jObject.GetValue("monto").ToString());


            cuenta cuenta = db.cuenta.FirstOrDefault(c => c.nroCuenta == nroCuenta);
            transaccion transaccion = new transaccion() { };

            movimiento movimiento = new movimiento() {cuenta=cuenta,monto=monto,descripcion="Retiro" };
            return Ok();
        }



        // GET: api/movimiento
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/movimiento/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/movimiento
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/movimiento/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/movimiento/5
        public void Delete(int id)
        {
        }
    }
}

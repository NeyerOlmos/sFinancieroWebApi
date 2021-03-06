﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sFinanciero.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class sisFinancieroEntities : DbContext
    {
        public sisFinancieroEntities()
            : base("name=sisFinancieroEntities")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<banco> banco { get; set; }
        public virtual DbSet<chequera> chequera { get; set; }
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<credito> credito { get; set; }
        public virtual DbSet<cuenta> cuenta { get; set; }
        public virtual DbSet<cuota> cuota { get; set; }
        public virtual DbSet<deposito> deposito { get; set; }
        public virtual DbSet<detalleMov> detalleMov { get; set; }
        public virtual DbSet<detalleServicio> detalleServicio { get; set; }
        public virtual DbSet<detalleTrans> detalleTrans { get; set; }
        public virtual DbSet<DPF> DPF { get; set; }
        public virtual DbSet<empresa> empresa { get; set; }
        public virtual DbSet<grupo> grupo { get; set; }
        public virtual DbSet<moneda> moneda { get; set; }
        public virtual DbSet<movimiento> movimiento { get; set; }
        public virtual DbSet<oficina> oficina { get; set; }
        public virtual DbSet<pago> pago { get; set; }
        public virtual DbSet<persona> persona { get; set; }
        public virtual DbSet<planPago> planPago { get; set; }
        public virtual DbSet<retiro> retiro { get; set; }
        public virtual DbSet<servicio> servicio { get; set; }
        public virtual DbSet<socio> socio { get; set; }
        public virtual DbSet<solicitud> solicitud { get; set; }
        public virtual DbSet<tarjeta> tarjeta { get; set; }
        public virtual DbSet<tipoCambio> tipoCambio { get; set; }
        public virtual DbSet<tipoCliente> tipoCliente { get; set; }
        public virtual DbSet<tipoCuenta> tipoCuenta { get; set; }
        public virtual DbSet<tipoDPF> tipoDPF { get; set; }
        public virtual DbSet<tipoServicio> tipoServicio { get; set; }
        public virtual DbSet<tipoSolicitud> tipoSolicitud { get; set; }
        public virtual DbSet<tipoTarjeta> tipoTarjeta { get; set; }
        public virtual DbSet<transaccion> transaccion { get; set; }
        public virtual DbSet<cheque> cheque { get; set; }
    }
}

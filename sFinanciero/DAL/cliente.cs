//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            this.cuenta = new HashSet<cuenta>();
            this.DPF = new HashSet<DPF>();
            this.solicitud = new HashSet<solicitud>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> fechaReg { get; set; }
        public string estado { get; set; }
        public string password { get; set; }
        public string tipoC { get; set; }
        public Nullable<int> idPersona { get; set; }
        public Nullable<int> idTipoC { get; set; }
        public Nullable<int> idGrupo { get; set; }
        public Nullable<int> idEmpresa { get; set; }
    
        public virtual empresa empresa { get; set; }
        public virtual grupo grupo { get; set; }
        public virtual persona persona { get; set; }
        public virtual tipoCliente tipoCliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cuenta> cuenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DPF> DPF { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<solicitud> solicitud { get; set; }
    }
}

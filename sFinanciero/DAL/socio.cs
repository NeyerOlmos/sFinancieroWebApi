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
    
    public partial class socio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public socio()
        {
            this.servicio = new HashSet<servicio>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> fecha_ini { get; set; }
        public Nullable<System.DateTime> fecha_fin { get; set; }
        public Nullable<int> idEmpresa { get; set; }
    
        public virtual empresa empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicio> servicio { get; set; }
    }
}

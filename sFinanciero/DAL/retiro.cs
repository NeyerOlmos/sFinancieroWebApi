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
    
    public partial class retiro
    {
        public int id { get; set; }
        public Nullable<System.DateTime> fechaHora { get; set; }
        public Nullable<decimal> monto { get; set; }
        public Nullable<int> idCuenta { get; set; }
        public Nullable<int> idOf { get; set; }
    
        public virtual cuenta cuenta { get; set; }
        public virtual oficina oficina { get; set; }
    }
}

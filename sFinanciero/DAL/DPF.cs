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
    
    public partial class DPF
    {
        public int id { get; set; }
        public Nullable<System.DateTime> fechaIni { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
        public Nullable<decimal> monto { get; set; }
        public Nullable<int> idCliente { get; set; }
        public Nullable<int> idTipoDPF { get; set; }
    
        public virtual cliente cliente { get; set; }
        public virtual tipoDPF tipoDPF { get; set; }
    }
}

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
    
    public partial class cheque
    {
        public int id { get; set; }
        public string estado { get; set; }
        public Nullable<System.DateTime> fechaEmision { get; set; }
        public Nullable<System.DateTime> fechaVenc { get; set; }
        public string montoLiteral { get; set; }
        public Nullable<int> montoNumerico { get; set; }
        public string numSerie { get; set; }
        public string paguese { get; set; }
        public Nullable<int> idChequera { get; set; }
    
        public virtual chequera chequera { get; set; }
    }
}

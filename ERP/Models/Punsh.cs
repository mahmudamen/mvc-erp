//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Punsh
    {
        public int ID { get; set; }
        public int EmpID { get; set; }
        public Nullable<decimal> PunshMount { get; set; }
        public Nullable<decimal> PunshDay { get; set; }
        public int CutMonths { get; set; }
        public System.DateTime SCutMonths { get; set; }
        public string Notes { get; set; }
        public string OrderBy { get; set; }
        public int CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}

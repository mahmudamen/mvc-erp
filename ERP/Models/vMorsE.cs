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
    
    public partial class vMorsE
    {
        public int ID { get; set; }
        public int MorID { get; set; }
        public string FullName { get; set; }
        public int DepID { get; set; }
        public string Dept { get; set; }
        public decimal MorMount { get; set; }
        public decimal MorDay { get; set; }
        public System.DateTime DonDate { get; set; }
        public System.DateTime PayMonth { get; set; }
        public string Notes { get; set; }
        public string OrderBy { get; set; }
    }
}

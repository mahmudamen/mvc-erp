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
    
    public partial class EmpFin
    {
        public int ID { get; set; }
        public Nullable<int> EmpId { get; set; }
        public Nullable<bool> DegreeID { get; set; }
        public Nullable<decimal> BaseSalary { get; set; }
        public Nullable<decimal> BaseSalaryTax { get; set; }
        public Nullable<decimal> BaseSalaryNotTax { get; set; }
        public Nullable<decimal> BounceVal { get; set; }
        public Nullable<int> BnkID { get; set; }
        public Nullable<System.DateTime> E_Date_receipt_work { get; set; }
        public Nullable<System.DateTime> E_Date_Hiring { get; set; }
        public string MilitaryID { get; set; }
        public Nullable<int> MilitaryNum { get; set; }
        public Nullable<System.DateTime> MilitaryFinishDate { get; set; }
    
        public virtual HR_EmpReform HR_EmpReform { get; set; }
    }
}

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
    
    public partial class SysList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysList()
        {
            this.Ag = new HashSet<Ag>();
        }
    
        public int ID { get; set; }
        public string SysName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string ReMark { get; set; }
        public Nullable<bool> LandNum { get; set; }
        public Nullable<bool> FlatNum { get; set; }
        public Nullable<bool> BuildNum { get; set; }
        public Nullable<bool> StageNum { get; set; }
        public Nullable<int> CreateBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ag> Ag { get; set; }
    }
}

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
    
    public partial class ArchPro
    {
        public int ID { get; set; }
        public Nullable<int> ProListFK { get; set; }
        public Nullable<int> GFK { get; set; }
        public string PicPath { get; set; }
        public string Photo { get; set; }
        public string ReNamePic { get; set; }
        public string Subject { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> Posted { get; set; }
        public Nullable<System.DateTime> PosteDate { get; set; }
        public string Ex { get; set; }
        public byte[] Img { get; set; }
    
        public virtual Gallary Gallary { get; set; }
        public virtual ProList ProList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERP.Models.CoreManager
{
    public class CoreModel
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم المستخدم")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "يجب ادخال كلمة المرور")]
        public string Password { get; set; }
    }
    public class userinfo
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "يجب اختيار الموظف")]
        public int EmpID { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم المستخدم")]
        public string UserName { get; set; }
        [StringLength(10, ErrorMessage = "يجب أن لاتزيد كلمة المرور عن 10 حروف")]
        [Required(ErrorMessage = "يجب ادخال كلمة المرور")]
        [DataType(DataType.Password)]
        public string UserKey { get; set; }
        [StringLength(10, ErrorMessage = "يجب أن لاتزيد كلمة المرور عن 10 حروف")]
        [Compare("UserKey", ErrorMessage = "حقل كلمة المرور غير متطابق")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "يجب ادخال كلمة المرور")]
        public string UserKeyc { get; set; }
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "تليفون")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "رقم الموبايل غير صالح")]
        public string Telephone { get; set; }
        [Required(ErrorMessage = "يجب تحديد صلاحية المستخدم")]
        public int Role { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "يجب تحديد تاريخ التنشيط")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime ValidFrom { get; set; }
        public int Createby { get; set; }
    }
}
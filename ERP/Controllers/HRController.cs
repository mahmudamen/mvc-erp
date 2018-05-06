using ERP.Models;
using ERP.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    [AuthorizeRoles("Admin")]
    public class HRController : Controller
    {
        ErpDB db = new ErpDB();
        public ActionResult Index()
        {
            ViewBag.jobkadr = new SelectList(db.JobKader, "KDR_PK", "KDR_Name");
            ViewBag.Emp_JCat_FK = new SelectList(db.JobCategory, "JBC_PK", "JBC_Name");
            ViewBag.SOCIALSTATUSES = new SelectList(db.SOCIALSTATUSES.Where(x => x.IsActive == true), "ID", "SocialTitle");
            ViewBag.JobGarde = new SelectList(db.JobGarde, "JTL_JG_FK", "JG_Name");
            ViewBag.JobStatus = new SelectList(db.JobStatus.Where(x => x.IsActive == true), "StatusID", "JobStatusName");
            ViewBag.Hr_dept = new SelectList(db.Hr_dept.Where(x => x.IsActive == true), "DeptID", "DeptName");
            return View();
        }
        public JsonResult tbempdef()
        {
            ErpDB db = new ErpDB();
            var query = db.HR_EmpReform
            .Select(p => new { p.Emp_Id, p.Emp_Name });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult jcatfunk(int id)
        {
            ErpDB db = new ErpDB();
            var query = db.JobCategory.Where(x => x.Kdr_fk == id)
            .Select(p => new { p.JBC_PK, p.JBC_Name });
            SelectList data = new SelectList(query, "JBC_PK", "JBC_Name", 0);
            return Json(data);
        }
        public JsonResult tbdeptdef()
        {
            ErpDB db = new ErpDB();
            var query = db.Hr_dept
            .Select(p => new { p.DeptID, p.DeptName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult  empcountfuc()
        {
            var query = db.HR_EmpReform.Count();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adnewemp(string empname ,DateTime createDate ,string NationalID 
            , string address ,int SOCIALSTATUSES ,bool Gender ,int jobkadr ,int Emp_JCat_FK ,int JobGarde, int JobStatus , int Hr_dept , int cby)
        {
            HR_EmpReform erf = new HR_EmpReform();
            erf.Emp_Id = db.HR_EmpReform.ToList().LastOrDefault().Emp_Id + 1;
            erf.Emp_Name = empname;
            erf.Emp_NationalID = NationalID;
            erf.Address = address;
            erf.SocialStatues = SOCIALSTATUSES;
            erf.Gender = Gender;
            erf.Emp_kader_FK = jobkadr;
            erf.Emp_JCat_FK = Emp_JCat_FK;
            erf.Emp_JGrade = JobGarde;
            erf.Job_Status_ID = JobStatus;
            erf.DeptID = Hr_dept;
            erf.CreatedDate = DateTime.Now;
            erf.CreatedBy = cby;
            db.HR_EmpReform.Add(erf);
            db.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}

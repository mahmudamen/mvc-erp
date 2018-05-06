using ERP.Models;
using ERP.Models.CoreManager;
using ERP.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class UserController : Controller
    {
        ErpDB am = new ErpDB();
        [AuthorizeRoles("Admin")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutMe()
        {
            return View();
        }
        [AuthorizeRoles("Admin")]
        public ActionResult AddUser()
        {
            ViewBag.depts = new SelectList(am.Role, "ID", "RoleName");
            ViewBag.emp = new SelectList(am.vUsrEmp, "ID", "FullName");
            return View();
        }
        [AuthorizeRoles("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(userinfo de)
        {
            ViewBag.depts = new SelectList(am.Role, "ID", "RoleName");
            ViewBag.emp = new SelectList(am.vUsrEmp, "ID", "FullName");
            if (ModelState.IsValid)
            {
                CoreManager DM = new CoreManager();
                if (!DM.IsLoginNameExist(de.UserName))
                {
                    DM.adduser(de);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "الاسم موجود من قبل");
                }
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index", "User");
        }
        [AuthorizeRoles("Admin")]
        public ActionResult EditUser(int id)
        {
            ViewBag.depts = new SelectList(am.Role, "ID", "RoleName");
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Usr dpt = am.Usr.Where(v => v.UserID == id).FirstOrDefault();
            var empi = am.EmpInfo.Where(x => x.Usr == dpt.UserName).Select(c => c.FullName).FirstOrDefault();
            ViewBag.emp = empi;
            if (dpt == null)
            {
                return HttpNotFound();
            }
            return View(dpt);
        }
        [AuthorizeRoles("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "ID,UserID,EmpID,UserName,UserKey,Role,Telephone,EmpID,Email,IsActive,ValidFrom,CreatedBy")] Usr eh)
        {
            ViewBag.depts = new SelectList(am.Role, "ID", "RoleName");
            var empi = am.EmpInfo.Where(x => x.Usr == eh.UserName).Select(c => c.FullName).FirstOrDefault();
            ViewBag.emp = empi;
            if (ModelState.IsValid)
            {
                CoreManager DM = new CoreManager();
                if (!DM.IsLoginName(eh.UserName, eh.ID))
                {
                    var ei = am.Usr.SingleOrDefault(v => v.UserID == eh.UserID);
                    var ri = am.UserRole.SingleOrDefault(v => v.UserID == eh.UserID);
                    if (ei == null)
                        return HttpNotFound();
                    ei.UserName = eh.UserName.Trim();
                    ei.Role = eh.Role;
                    ei.Email = eh.Email;
                    ei.Telephone = eh.Telephone;
                    ei.IsActive = eh.IsActive;
                    ei.ValidFrom = eh.ValidFrom;
                    ei.CreatedBy = eh.CreatedBy;
                    ei.EmpID = eh.EmpID;
                    var emp = am.EmpInfo.Where(x => x.ID == eh.EmpID).FirstOrDefault();
                    if (emp != null) { emp.Usr = eh.UserName.Trim(); }
                    if (ri != null) { ri.RoleID = eh.Role; ri.ValidFrom = eh.ValidFrom; ri.IsActive = eh.IsActive; }
                    am.SaveChanges();
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "الاسم موجود من قبل");
                    return View();
                }
            }
            return RedirectToAction("Index", "User");
        }
        public JsonResult fuser()
        {
            var query = am.vURoles
            .Select(p => new { p.UserID, p.UserName, p.RoleName, p.FullName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult pwUpdat(int id, string pw, string npw)
        {
            Secur sr = new Secur();
            var udept = am.Usr.SingleOrDefault(v => v.UserID == id);
            if (pw == sr.Decrypt(udept.UserKey))
            {
                udept.UserKey = sr.Encrypt(npw);
                am.SaveChanges();
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { Success = false };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
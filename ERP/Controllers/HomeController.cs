using ERP.Models.CoreManager;
using ERP.Security;
using System;
using System.Collections.Generic;
using ERP.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ERP.Controllers
{
    public class HomeController : Controller
    {
        ErpDB db = new ErpDB();
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CoreModel ULV, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                CoreManager UM = new CoreManager();
                Secur sr = new Secur();
                string password = sr.Decrypt(UM.GUP(ULV.LoginName));
                if (string.IsNullOrEmpty(password))
                    ModelState.AddModelError("", "خطأ في اسم المستخدم أو كلمة المرور");
                else
                {
                    if (ULV.Password.Equals(password))
                    {
                        var role = (from q in db.vURoles
                                    where q.UserName.Equals(ULV.LoginName)
                                    select new { q.RoleName, q.UserID }).FirstOrDefault();
                        FormsAuthentication.SetAuthCookie(ULV.LoginName + "|" + role.UserID, false);
                        return RedirectToAction("Index", role.RoleName);
                    }

                    else
                    {
                        ModelState.AddModelError("", "خطأ في كلمة المرور");
                    }
                }
            }
            return View(ULV);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}
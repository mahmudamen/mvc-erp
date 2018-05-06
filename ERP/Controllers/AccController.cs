using ERP.Models;
using ERP.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class AccController : Controller
    {
        // GET: Acc
        ErpDB db = new ErpDB();
        public ActionResult Index()
        {
            return View();
        }
        //public JsonResult expfunc(DateTime s , DateTime e)
        //{
        //    var query = db.ex(s,e)
        //    .Select(p => new { p.expname, p.expval });
        //    return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult incomfunc(DateTime s, DateTime e)
        //{
        //    var query = db.incom(s, e)
        //    .Select(p => new { p.ItmName, p.totval });
        //    return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        //}
    }
}
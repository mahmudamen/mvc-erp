using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.Models;
using System.IO;
using System.Web.Mvc;
using ERP.Security;

namespace ERP.Controllers
{
    [AuthorizeRoles("Admin")]
    public class AgController : Controller
    {
        ErpDB db = new ErpDB();
        // GET: Ag
        [AuthorizeRoles("Admin")]
        public ActionResult Index()
        {
            ViewBag.sysid = new SelectList(db.SysList.Where(x => x.IsActive == true), "ID", "SysName");
            return View();
        }
        public JsonResult tbAgdef(int? id)
        {
            
            if(id == null) {
                id = 1;
            }
            var query = db.Ag.Where(s => s.SysID == id)
            .Select(p => new { p.ID, p.AgntName ,  p.LandNum });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbSysdef()
        {
           
            var query = db.SysList
            .Select(p => new { p.ID, p.SysName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult systmrep()
        {
            var query = db.Sysrep().Select(p => new { p.SysID, p.SysName ,
                p.DebF , p.CreF , p.DebCur , p.CreCur , p.DebTot , p.CreTot , p.DebLast , p.CreLast }); ;
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbAgMove(int? id)
        {
            
            var query = db.Ag.Where(s => s.ID == id)
            .Select(p => new { p.ID, p.DebCur, p.CreCur, p.DebF , p.CreF });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AdAc(string syname ,int cby)
        {
            SysList sl = new SysList();
            sl.ID = db.SysList.ToList().LastOrDefault().ID + 1;
            sl.SysName = syname;
            sl.IsActive = true;
            sl.CreateDate = DateTime.Now;
            sl.CreateBy = cby;
            db.SysList.Add(sl);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adpart(int id ,DateTime mdate , Decimal valmove ,int cby)
        {
            AgMove am = new AgMove();
            am.ID = db.AgMove.ToList().LastOrDefault().ID + 1;
            am.AgID = id;
            am.CreatedDate = mdate;
            am.Credit = valmove;
            am.Debit = 0;
            am.IsActive = true;
            am.CreateBy = cby;
            db.AgMove.Add(am);
            db.SaveChanges();
            var mi = db.Ag.SingleOrDefault(x => x.ID == id);
            mi.CreCur = mi.CreCur  + valmove;
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}
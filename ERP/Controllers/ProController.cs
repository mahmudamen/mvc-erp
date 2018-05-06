using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.Models;
using System.IO;
using System.Web.Mvc.Async;
using System.Web.Mvc;
using ERP.Security;

namespace ERP.Controllers
{

    public class ProController : Controller
    {
        ErpDB db = new ErpDB();
        // GET: Pro
        public ActionResult Index()
        {
            return View();
        }
        // GET: cpy
        public ActionResult Cpy()
        {
            ViewBag.activity = new SelectList(db.CActivity.Where(x => x.IsActive == true), "ID", "ActivityName");
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }
        // GET: prolist
        public ActionResult Prolist()
        {
            ViewBag.tpro = new SelectList(db.TPro.Where(x => x.IsActive == true), "ID", "TName");
            return View();
        }
        // Get : Gallery
        public ActionResult Gallerie()
        {
            var d = db.ArchPro.ToList();
            return View(d);
        }
        // Get : RPseven
        public ActionResult RPseven()
        {
            
            return View();
        }
        public ActionResult Adpro()
        {
            ViewBag.comfk = new SelectList(db.Company.Where(x => x.IsActive == true), "ID", "CompanyName");
            ViewBag.kprofk = new SelectList(db.KPro.Where(x => x.IsActive == true), "ID", "KName");
            ViewBag.purnamefk = new SelectList(db.PurType.Where(x => x.IsActive == true), "ID", "PurName");
            ViewBag.Galery = new SelectList(db.Gallary.Where(x => x.IsActive == true), "ID", "GallaryName");
            return View();
        }
        public ActionResult TechPro()
        {
            ViewBag.Galery = new SelectList(db.Gallary.Where(x => x.IsActive == true), "ID", "GallaryName");
            return View();
        }
        public ActionResult Garch()
        {
            return View();
        }
        public JsonResult tbGallery()
        {
            var query = db.Gallary.Where(x => x.IsActive == true).Select(x => new {x.ID , x.GallaryName ,x.ArchPro.Count }) ;
            return Json(new {aaData = query },JsonRequestBehavior.AllowGet);
        }
        public JsonResult adnpro(string proname,DateTime? fdate,DateTime? sdate,DateTime? tdate,DateTime? declaredate, decimal? totalval,int? kprofk, int? purnamefk, int cby)
        {
            ProList pl = new ProList();
            pl.ProName = proname;
            pl.TotalVal = totalval ?? 0;
            pl.KProFK = kprofk;
            pl.PurNameFK = purnamefk;
            pl.FirstAccepteDate = fdate;
            pl.SecandAccepteDate = sdate;
            pl.ThirdAceepteDate = tdate;
            pl.CreateBy = cby;
            pl.CreateDate = DateTime.Now;
            pl.IsActive = true;
            pl.Posted = 1;
            pl.ProStop = false;
            db.ProList.Add(pl);
            db.SaveChanges();
            return Json(new { Success = true, Message = " تم إضافة المشروع بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adeditpro(int id,DateTime? fdate,DateTime?  sdate,DateTime? tdate, DateTime? declaredate ,decimal? totalval, int? kprofk, int? purnamefk)
        {

            var m = db.ProList.Where(x => x.ID == id).SingleOrDefault();
            m.KProFK = kprofk ?? m.KProFK;
            m.PurNameFK = purnamefk ?? m.PurNameFK;
            m.FirstAccepteDate = fdate ?? m.FirstAccepteDate;
            m.SecandAccepteDate = sdate ?? m.SecandAccepteDate;
            m.ThirdAceepteDate = tdate ?? m.ThirdAceepteDate;
            m.DeclareDate = declaredate ?? m.DeclareDate;
            m.TotalVal = totalval ?? m.TotalVal;
            db.SaveChanges();

            return Json(new { Success = true , Message = " تم التعديل البيانات بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbcpydef()
        {
            ErpDB db = new ErpDB();
            var query = db.Company.Where(z => z.IsActive == true)
            .Select(p => new { p.ID, p.CompanyName, p.ProList.Count });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbTProdef()
        {
            ErpDB db = new ErpDB();
            var query = db.TPro.Where(z => z.IsActive == true)
            .Select(p => new { p.ID, p.TName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbprolistdef()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.Posted == 1 && z.ProStop == false)
                .Select(x => new { x.ID, x.ProName, x.TotalVal, x.KPro.TPro.TName, x.KPro.KName, x.PurType.PurName , x.FirstAccepteDate , x.SecandAccepteDate , x.ThirdAceepteDate , x.DeclareDate });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbprostate()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.ProStop == false)
                .Select(x => new { x.ID, x.ProName, x.PostList.PostedName , x.ExpForNow   });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbprofixstate()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.ProStop == true)
                .Select(x => new { x.ID, x.ProName, x.PostList.PostedName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbtechprolistdef()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.Posted == 2 && z.ProStop == false)
                .Select(x => new { x.ID, x.ProName,x.TotalVal, x.FirstAccepteDate , x.SecandAccepteDate ,x.ThirdAceepteDate ,x.DeclareDate , x.OpnTechDate , x.SetTechDate });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbfinprolistdef()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.Posted == 3 && z.ProStop == false)
                .Select(x => new { x.ID, x.ProName, x.TotalVal,
                    x.FirstAccepteDate, x.SecandAccepteDate, x.ThirdAceepteDate,
                    x.DeclareDate, x.OpnTechDate, x.SetTechDate , x.OpnFinDate , x.SetFinDate });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbfixprolistdef()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.Posted == 3 && z.ProStop == false)
                .Select(x => new {
                    x.ID,
                    x.ProName,
                    x.TotalVal,
                    x.FirstAccepteDate,
                    x.SecandAccepteDate,
                    x.ThirdAceepteDate,
                    x.DeclareDate,
                    x.OpnTechDate,
                    x.SetTechDate,
                    x.OpnFinDate,
                    x.SetFinDate,
                    x.ProNum,
                    x.ProDate,
                    x.ProVal
                });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbpurprolistdef()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.Posted == 3 && z.ProStop == false)
                .Select(x => new {
                    x.ID,
                    x.ProName,
                    x.Company.CompanyName,
                    x.PurType.PurName,
                    x.ProNum,
                    x.ProDate,
                    x.ProVal,
                    x.StartDate,
                    x.EndDate,
                    x.DurDateFirst,
                    x.DurDateSec,
                    x.DurDateThird
                });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbrpsixdef()
        {
            var query = db.ProList.Where(z => z.IsActive == true )
                .Select(x => new {
                    x.ID,
                    x.ProName,
                    x.TotalVal,
                    x.FirstAccepteDate,
                    x.SecandAccepteDate,
                    x.ThirdAceepteDate,
                    x.DeclareDate,
                    x.OpnTechDate,
                    x.SetTechDate,
                    x.OpnFinDate,
                    x.SetFinDate,
                    x.ProNum,
                    x.ProDate,
                    x.ProVal,
                    x.PostList.PostedName
                });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbprolistvdef(int? id)
        {
            var query = db.ProList.Where(x => x.IsActive && x.ComFK == id)
                .Select(x => new { x.ID, x.ProName, x.KPro.KName, x.ProNum, x.ProDate });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbpurdef()
        {
            var query = db.PurType.Where(z => z.IsActive == true)
                .Select(p => new { p.ID, p.PurName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbKProdef(int? id)
        {

            var query = db.KPro.Where(z => z.TProFK == id && z.IsActive == true)
            .Select(p => new { p.ID, p.KName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbgalary(int? id)
        {

            var query = db.ArchPro
            .Select(p => new { p.ID, p.ProList.ProName , p.ReNamePic,p.Photo });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbactivitydef()
        {
            ErpDB db = new ErpDB();
            var query = db.CActivity.Where(z => z.IsActive == true)
            .Select(p => new { p.ID, p.ActivityName });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adnewcomp(string CompanyName, int activity, string Address, string phone, string fax, string mob, string cemail, string taxid, string saleid, string tradeid, int cby)
        {
            var n = db.Company.Where(x => x.CompanyName == CompanyName).Any();
            if (n)
            {
                return Json(new { Success = false, Message = "هذا الاسم    " + CompanyName + " مستخدم من قبل    " }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Company cmp = new Company();

                cmp.CompanyName = CompanyName;
                cmp.ActivityID = activity;
                cmp.Address = Address;
                cmp.Phone = phone;
                cmp.Fax = fax;
                cmp.Mob = mob;
                cmp.CEmail = cemail;
                cmp.TaxID = taxid;
                cmp.SaleID = saleid;
                cmp.TradeID = tradeid;
                cmp.CreateDate = DateTime.Now;
                cmp.CreateBy = cby;
                cmp.IsActive = true;
                db.Company.Add(cmp);
                db.SaveChanges();
            }

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addtpro(string tname, int cby)
        {
            var p = db.TPro.Where(x => x.TName == tname).Any();
            if (p)
            {
                return Json(new { Success = false, Message = "هذا الاسم    " + tname + " مستخدم من قبل    " }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TPro tp = new TPro();
                tp.TName = tname;
                tp.CreateDate = DateTime.Now;
                tp.CreateBy = cby;
                tp.IsActive = true;
                db.TPro.Add(tp);
                db.SaveChanges();
            }

            return Json(new { Success = true, Message = "تمت إضافة النوع     " + tname + " بنجاح    " }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addkproj(int id, string kname, int cby)
        {
            var p = db.KPro.Where(x => x.KName == kname).Any();
            if (p)
            {
                return Json(new { Success = false, Message = "هذا الاسم    " + kname + " مستخدم من قبل    " }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                KPro tp = new KPro();
                tp.KName = kname;
                tp.TProFK = id;
                tp.CreateDate = DateTime.Now;
                tp.CreateBy = cby;
                tp.IsActive = true;
                db.KPro.Add(tp);
                db.SaveChanges();
            }

            return Json(new { Success = true, Message = "تمت إضافة النوع     " + kname + " بنجاح    " }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adnewactivity(string ActivityName, int cby)
        {
            var m = db.CActivity.Where(x => x.ActivityName == ActivityName).Any();
            if (m)
            {
                return Json(new { Success = false, Message = "هذا الاسم    " + ActivityName + " مستخدم من قبل    " }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CActivity cmp = new CActivity();
                string ip = HttpContext.Request.UserHostAddress;
                string md = Request.UserHostName;
                cmp.ActivityName = ActivityName;
                cmp.CreateDate = DateTime.Now;
                cmp.CreateBy = cby;
                cmp.IsActive = true;
                cmp.CreatedHost = ip;
                cmp.CreatedIP = md;
                db.CActivity.Add(cmp);
                db.SaveChanges();
            }
            return Json(new { Success = true, Message = "تمت إضافة نشاط    " + ActivityName + " بنجاح    " }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adnewpurtype(string purName, int cby)
        {
            var m = db.PurType.Where(x => x.PurName == purName).Any();
            if (m)
            {
                return Json(new { Success = false, Message = "هذا الاسم    " + purName + " مستخدم من قبل    " }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                PurType cmp = new PurType();

                cmp.PurName = purName;
                cmp.CreateDate = DateTime.Now;
                cmp.CreateBy = cby;
                cmp.IsActive = true;
                db.PurType.Add(cmp);
                db.SaveChanges();
            }
            return Json(new { Success = true, Message = "تمت إضافة طريقة    " + purName + " بنجاح    " }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult adgallary(int cby)
        {
            ArchPro h = new ArchPro();
            h.ProListFK = 2;
            h.GFK = 1;
            db.ArchPro.Add(h);
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Pcount()
        {

            var query = db.ProList.Where(x => x.IsActive == true && x.Posted == 1 && x.ProStop == false)
            .Count() ;
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tcount()
        {

            var query = db.ProList.Where(x => x.IsActive == true && x.Posted == 2 && x.ProStop == false)
            .Count();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult fcount()
        {

            var query = db.ProList.Where(x => x.IsActive == true && x.Posted == 3 )
            .Count();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult savcount()
        {

            var query = db.ProList.Where(x => x.IsActive == true && x.ProStop == true)
            .Count();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult fixcount()
        {

            var query = db.ProList.Where(x => x.IsActive == true && x.Posted == 4 && x.ProStop == false)
            .Count();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult pstproj(int id)
        {

            var q = db.ProList.Where(x => x.ID == id).SingleOrDefault();
            if (q.Posted == 1 && q.FirstAccepteDate != null && q.SecandAccepteDate != null && q.ThirdAceepteDate != null && q.DeclareDate != null )
            {
                q.Posted = 2;
                db.SaveChanges();
                return Json(new { Succcess = true, Message = " تم نقل المشروع للفتح الفني" }, JsonRequestBehavior.AllowGet);
            } else if (q.Posted == 2 && q.OpnTechDate != null && q.SetTechDate != null )
            {
                q.Posted = 3;
                db.SaveChanges();
                return Json(new { Succcess = true, Message = " تم نقل المشروع للفتح المالي" }, JsonRequestBehavior.AllowGet);
            } else if (q.Posted == 3 && q.OpnFinDate != null && q.SetFinDate != null )
            {
                q.Posted = 4;
                db.SaveChanges();
                return Json(new { Succcess = true, Message = " تم نقل المشروع لمرحلة الاسناد" }, JsonRequestBehavior.AllowGet);
            }else if (q.Posted == 4 && q.ProNum != null && q.ProDate != null && q.ProVal != null && q.ProStop == false)
        
            {
                q.Posted = 5;
                db.SaveChanges();
                return Json(new { Succcess = true, Message = " تم نقل المشروع لمرحلة الاسناد" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Succcess = false, Message = " برجاء استكمال بينات المرحلة " }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            HttpFileCollectionBase ha = Request.Files;
            HttpPostedFileBase g = ha[0];
            string ename =  g.FileName ;
            byte[] im = null;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    var ex = Path.GetExtension(g.FileName);
                  BinaryReader reader = new BinaryReader(g.InputStream);
                   im = reader.ReadBytes(g.ContentLength);
                    string category = HttpContext.Request.Form["cby"];
                    string vid = HttpContext.Request.Form["vid"]; 
                    string Galry = HttpContext.Request.Form["Galery"];
                    string Subject = HttpContext.Request.Form["Subject"];
                    string ReNamePic = HttpContext.Request.Form["ReNamePic"];
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  
                        HttpPostedFileBase file = files[i];                       
                        string fname;
                        // autocad dwg 
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];   
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it. 
                        var m = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        fname = Path.Combine(Server.MapPath("~/Img"), m);
                    //    fname = Path.Combine(Server.MapPath("~/Img"), fname );
                        ArchPro h = new ArchPro();
                        h.ProListFK = Convert.ToInt32(vid);
                        h.GFK =  Convert.ToInt32(Galry);
                        h.PicPath = "/Erp/Img/" + m;
                        h.Photo = file.FileName;
                        h.Subject = Subject;
                        h.ReNamePic = ReNamePic;
                        h.CreateBy = Convert.ToInt32(category);
                        h.CreateDate = DateTime.Now;
                        h.Ex = ex;
                       h.Img = im;
                        db.ArchPro.Add(h);
                        db.SaveChanges();
                        file.SaveAs(fname);
                        
                        
                    }

                    // Returns message that successfully uploaded  
                    return Json(new { Success = true,resulte = ename, Message = " الي معرض الصور بنجاح" + ename + " تم إضافة الصورة"}, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public JsonResult refa(int? id)
        {
            var query = db.ArchPro.Select(x => x.Photo);
            
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult editpro(int? id, DateTime? opntech , DateTime? settech,int cby)
        {
            if (id == null)
            {
                return Json(new { Success = false , Message = "اختار مشروع لتعديل بيناته" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
            var epro = db.ProList.Where(x => x.ID == id).Single();
                        epro.OpnTechDate = opntech ?? epro.OpnTechDate;
                        epro.SetTechDate = settech ?? epro.SetTechDate;
                        epro.TechDate = DateTime.Now;
                        epro.TechUser = cby;
                        db.SaveChanges();
                return Json(new { Success = true , Message = "تم بنجاح " } , JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult editfinpro(int? id, DateTime? opnfin, DateTime? setfin, int cby)
        {
            if (id == null)
            {
                return Json(new { Success = false, Message = "اختار مشروع لتعديل بيناته" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var epro = db.ProList.Where(x => x.ID == id).Single();
                epro.OpnFinDate = opnfin ?? epro.OpnFinDate;
                epro.SetFinDate = setfin ?? epro.SetFinDate;
                epro.FinDate = DateTime.Now;
                epro.FinUser = cby;
                db.SaveChanges();
                return Json(new { Success = true, Message = "تم بنجاح " }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult editfixpro(int? id, int? fixnum, DateTime? fixdate,decimal? profix, int cby)
        {
            if (id == null)
            {
                return Json(new { Success = false, Message = "اختار مشروع لتعديل بيناته" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var epro = db.ProList.Where(x => x.ID == id).Single();
                epro.ProDate = fixdate ?? epro.ProDate;
                epro.ProNum = fixnum ?? epro.ProNum ;
                epro.ProVal = profix ?? epro.ProVal;
                epro.FixDate = DateTime.Now;
                epro.FixUser = cby;
                db.SaveChanges();
                return Json(new { Success = true, Message = "تم بنجاح التعديل " }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult editdurpro(int? id,  DateTime? fdate, DateTime? sdate,DateTime? tdate)
        {
            if (id == null)
            {
                return Json(new { Success = false, Message = "اختار مشروع لتعديل بيناته" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var epro = db.ProList.Where(x => x.ID == id).Single();
                epro.DurDateFirst = fdate ?? epro.DurDateFirst;
                epro.DurDateSec = sdate ?? epro.DurDateSec;
                epro.DurDateThird = tdate ?? epro.DurDateThird;
                db.SaveChanges();
                return Json(new { Success = true, Message = "تم بنجاح " }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult editoverpro(int? id, DateTime? fdate, DateTime? sdate)
        {
            if (id == null)
            {
                return Json(new { Success = false, Message = "اختار مشروع لتعديل بيناته" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var epro = db.ProList.Where(x => x.ID == id).Single();
                epro.OverFirst = fdate ?? epro.OverFirst;
                epro.OverSec = sdate ?? epro.OverSec;
                db.SaveChanges();
                return Json(new { Success = true, Message = "تم بنجاح " }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult FinPro()
        {
            ViewBag.Galery = new SelectList(db.Gallary.Where(x => x.IsActive == true), "ID", "GallaryName");
            return View();
        }
        public ActionResult FixPro()
        {
            ViewBag.Galery = new SelectList(db.Gallary.Where(x => x.IsActive == true), "ID", "GallaryName");
            ViewBag.comfk = new SelectList(db.Company.Where(x => x.IsActive == true), "ID", "CompanyName");
            return View();
        } 
        //RPsix
        public ActionResult RPsix()
        {
            return View();
        }
        // Dur Pro
        public ActionResult DurPro()
        {
            ViewBag.Galery = new SelectList(db.Gallary.Where(x => x.IsActive == true), "ID", "GallaryName");
            return View();
        }
        public ActionResult OverPro()
        {
            ViewBag.Galery = new SelectList(db.Gallary.Where(x => x.IsActive == true), "ID", "GallaryName");
            return View();
        }
        public JsonResult OverProDef()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.Posted >= 4 && z.ProStop == false)
                .Select(x => new { x.ID ,
                                   x.ProName ,
                                   x.Company.CompanyName ,
                                   x.PurType.PurName ,
                                   x.ProNum ,
                                   x.ProDate ,
                                   x.ProVal ,
                                   x.OverFirst ,
                                   x.OverSec });
            return Json(new { aaData = query } , JsonRequestBehavior.AllowGet);
        }
        public JsonResult durprodef()
        {
            var query = db.ProList.Where(z => z.IsActive == true && z.Posted >= 4 && z.ProStop == false).
                Select(x => new
                {
                    x.ID ,
                    x.ProName ,
                    x.Company.CompanyName ,
                    x.PurType.PurName ,
                    x.ProNum ,
                    x.ProDate ,
                    x.ProVal ,
                    x.StartDate ,
                    x.StopDate ,
                    x.FinalDate,
                    x.DurDateFirst ,
                    x.DurDateSec ,
                    x.DurDateThird
                });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult savpro(int? id)
        {           
            var q = db.ProList.Where(x => x.ID == id).SingleOrDefault();
            q.ProStop = true;
            q.StopDate = DateTime.Now;
            db.SaveChanges();

            return Json(new { Success = true, Message = "تم بنجاح " }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult accnm(string Prefix)
        {
            var CityName = db.Company.Where(o => o.CompanyName.Contains(Prefix) && o.IsActive == true)
                .Select(x => new { x.ID, x.CompanyName }).Take(20).ToList();
            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
        public JsonResult imgdef(int id)
        {
            var query = db.ArchPro.Where(x => x.ID == id).SingleOrDefault().Photo;
            return Json(new { resulte = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addexp(int id,int expnum,DateTime expdate,decimal expval, int cby)
        {
            ExpPro xp = new ExpPro();
            xp.ProListFK = id;
            xp.ExpNum = expnum;
            xp.ExpVal = expval ;
            xp.CreateBy = cby;
            xp.Remark = "المنصرف";
            xp.CreateDate = DateTime.Now;
            xp.ExpDate = expdate;
            xp.IsActive = true;
            db.ExpPro.Add(xp);
            db.SaveChanges();
            return Json( new { Success = true, Message = "تم الإضافة بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult addurpro(int id ,DateTime? axdate ,DateTime? azdate,DateTime? asdate , DateTime? finaldate)
        {
            var p = db.ProList.Where(x => x.ID == id).FirstOrDefault();
            p.DurDateFirst = axdate;
            p.DurDateSec = azdate;
            p.DurDateThird = asdate ;
            p.FinalDate = finaldate;            
            DurList dl = new DurList();
            dl.ProFK = id;
            dl.DurDateFirst = axdate ;
            dl.DurDateSec = azdate;
            dl.DurDateThird = asdate;
            dl.FinalDate = finaldate;
            db.DurList.Add(dl);
            db.SaveChanges();
            return Json(new { Success = true, Message = " تم الإضافة بنجاح" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult du(int? id)
        {
            var query = db.DurList.Where(z => z.ProFK == id).
                Select(x => new
                    {
                        x.ID,
                        x.ProFK,
                        x.DurDateFirst,
                        x.DurDateSec,
                        x.DurDateThird,
                        x.FinalDate
                    });
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult redux(int id)
        {
            var q = db.DurList.Where(i => i.ID == id).FirstOrDefault();
            db.DurList.Remove(q);
            db.SaveChanges();
            return Json(new { Success = true , Message = "تم الحذف بنجاح"}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbarch(string Prefix)
        {
            var data = db.ProList.Where(x => x.ProName.Contains(Prefix)).Select(x => new { x.ID, x.ProName  });
            return Json( data , JsonRequestBehavior.AllowGet);
        }
        public JsonResult tbimg(int? id)
        {
            if(id == null) {
                id = 12 ;
                var query = db.ArchPro.Where(x => x.ProListFK == id).Select(x => new { x.ID, x.ProList.ProName, x.ReNamePic, x.Photo, x.PicPath, x.Ex, x.Gallary.GallaryName, x.Subject });
                return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
            else
            {
            var query = db.ArchPro.Where(x => x.ProListFK == id).Select(x => new {x.ID , x.ProList.ProName ,  x.ReNamePic , x.Photo , x.PicPath , x.Ex , x.Gallary.GallaryName , x.Subject});
            return Json(new { aaData = query }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;
using PagedList;

namespace QLSach.Controllers
{
    public class Sach_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Sach_Customer/
        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            var sach = db.Saches.Include(s => s.DanhMuc1).OrderBy(s => s.MaSach).ToPagedList(page, pageSize);
            return View(sach);
        }
        public ActionResult BookBuy(int page = 1, int pageSize = 3)
        {
            if(Session["makh"]==null)
            {
                return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/Sach_Customer/BookBuy" });
            }
            else
            {
                @ViewData["error"] = "succes";
                if((Session["quyen"].ToString()) == "Customer" || Session["quyen"].ToString() == "Admin")
                {
                    ModelState.AddModelError("fail", "Bạn không có quyền truy cập vào đây");
                    @ViewData["error"] = "error";
                }
                var sach = db.Saches.Where(s => s.TinhTrang == true).OrderBy(s => s.MaSach).ToPagedList(page, pageSize);
                return View(sach);
            }
            
            
        }

        // GET: /Sach_Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }
        public ActionResult GetBookByCategory(int id, int page = 1, int pageSize = 6)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var sach = db.Saches.Where(s=>s.DanhMuc==id).Include(s => s.DanhMuc1).OrderBy(s=>s.MaSach).ToPagedList(page,pageSize);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }
        [HttpPost]
        public ActionResult GetBookByName(string keyword, int page = 1, int pageSize = 6)
        {
            if (keyword == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sach = db.Saches.Where(s => s.TenSach.Contains(keyword)).Include(s => s.DanhMuc1).OrderBy(s => s.MaSach).ToPagedList(page, pageSize);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult GetBookByProducter(string name, int page = 1, int pageSize = 6)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sach = db.Saches.Where(s => s.NhaXuatBan == name).Include(s => s.DanhMuc1).OrderBy(s => s.MaSach).ToPagedList(page, pageSize); ;
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }
        public ActionResult GetBookByPrice(Decimal gia, int id, int page=1, int pageSize=10)
        {
            var sach = db.Saches.Where(s => s.Gia == gia && s.MaSach!=id).OrderBy(s => s.MaSach).ToPagedList(page, pageSize);
            return View(sach);
        }

        [HttpPost]
        public ActionResult NewBook()
        {
            Sach sach = db.Saches.Find(db.Saches.Max(s => s.MaSach));
            var respone = new { MaSach=sach.MaSach, Ten=sach.TenSach, Gia=sach.Gia, Anh=sach.AnhBia };
            return Json(respone, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

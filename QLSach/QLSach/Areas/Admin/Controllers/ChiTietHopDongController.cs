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

namespace QLSach.Areas.Admin.Controllers
{
    public class ChiTietHopDongController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/ChiTietHopDong/
        public ActionResult Index(int id,int page=1, int pageSize=10)
        {
            var chitiethopdong = db.ChiTietHopDongs.Include(c => c.HopDong).Include(c => c.Sach).Where(c=>c.MaHopDong==id).OrderBy(s=>s.MaHopDong).ToPagedList(page, pageSize);
            return View(chitiethopdong);
        }
        // GET: /Admin/ChiTietHopDong/Create
        public ActionResult Create(int mahd)
        {
            ViewBag.MaHopDong = mahd;
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");
            return View();
        }

        // POST: /Admin/ChiTietHopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHopDong,MaSach,DonGia")] ChiTietHopDong chitiethopdong)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHopDongs.Add(chitiethopdong);
                db.SaveChanges();
                return RedirectToAction("Index", new { id=chitiethopdong.MaHopDong });
            }
            ViewBag.MaHopDong = new SelectList(db.HopDongs, "MaHopDong", "TinhTrang", chitiethopdong.MaHopDong);
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", chitiethopdong.MaSach);
            return RedirectToAction("Index", new { id = chitiethopdong.MaHopDong  });
        }

        // GET: /Admin/ChiTietHopDong/Delete/5
        public ActionResult Delete(int MaHD, int MaSach)
        {
            ChiTietHopDong result = db.ChiTietHopDongs.Where(s => s.MaHopDong == MaHD && s.MaSach==MaSach).FirstOrDefault();
            db.ChiTietHopDongs.Remove(result);
            db.SaveChanges();
            return Redirect("/Admin/ChiTietHopDong/Index/"+MaHD);
        }
        public void Delete(int MaHD)
        {
            var result = db.ChiTietHopDongs.Where(s => s.MaHopDong == MaHD);
            foreach(var item in result)
            {
                db.ChiTietHopDongs.Remove(item);
            }
            db.SaveChanges();
        }
        public void Insert(int mahd, int masach, Decimal gia)
        {
            ChiTietHopDong cthd = new ChiTietHopDong();
            cthd.MaHopDong = mahd;
            cthd.MaSach = masach;
            cthd.DonGia = gia;
            db.ChiTietHopDongs.Add(cthd);
            db.SaveChanges();
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

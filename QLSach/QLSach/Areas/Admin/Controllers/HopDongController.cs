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
    public class HopDongController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        private ChiTietHopDongController chitiethopdong = new ChiTietHopDongController();

        // GET: /Admin/HopDong/
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var hopdong = db.HopDongs.Include(h => h.NhaCungCap).OrderBy(s => s.MaHopDong).ToPagedList(page, pageSize);
            return View(hopdong);
        }
        // GET: /Admin/HopDong/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            return View();
        }

        // POST: /Admin/HopDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaHopDong,NgayBatDau,NgayKetThuc,TinhTrang,MaNCC")] HopDong hopdong)
        {
            if (ModelState.IsValid)
            {
                db.HopDongs.Add(hopdong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", hopdong.MaNCC);
            return View(hopdong);
        }
        public ActionResult Insert(int MaNCC, int MaSach, Decimal Gia)
        {
            HopDong hd = new HopDong();
            hd.MaNCC = MaNCC;
            hd.TinhTrang = "1";
            hd.NgayBatDau = DateTime.Now;
            hd.NgayKetThuc = DateTime.Now.AddYears(1);
            db.HopDongs.Add(hd);
            db.SaveChanges();
            chitiethopdong.Insert(hd.MaHopDong, MaSach, Gia);
            return Redirect("/Admin/HopDong/Index");
        }

        // GET: /Admin/HopDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopdong = db.HopDongs.Find(id);
            if (hopdong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", hopdong.MaNCC);
            return View(hopdong);
        }

        // POST: /Admin/HopDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaHopDong,NgayBatDau,NgayKetThuc,TinhTrang,MaNCC")] HopDong hopdong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hopdong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", hopdong.MaNCC);
            return View(hopdong);
        }

        // GET: /Admin/HopDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong hopdong = db.HopDongs.Find(id);
            if (hopdong == null)
            {
                return HttpNotFound();
            }
            return View(hopdong);
        }

        // POST: /Admin/HopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HopDong hopdong = db.HopDongs.Find(id);
            chitiethopdong.Delete(id);
            db.HopDongs.Remove(hopdong);
            db.SaveChanges();
            return RedirectToAction("Index");
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

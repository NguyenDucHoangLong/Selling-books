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
    public class SachController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/Sach/
        public ActionResult Index(int page=1, int pageSize=10)
        {
            var sach = db.Saches.Include(s => s.DanhMuc1).OrderBy(s=>s.MaSach).ToPagedList(page, pageSize);
            return View(sach);
        }
        public ActionResult Index1(int page = 1, int pageSize = 10)
        {
            var sach = db.Saches.Include(s => s.DanhMuc1).Where(s=>s.TinhTrang==true).OrderBy(s => s.MaSach).ToPagedList(page, pageSize);
            return View(sach);
        }

        // GET: /Admin/Sach/Details/5
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

        // GET: /Admin/Sach/Create
        public ActionResult Create()
        {
            ViewBag.DanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: /Admin/Sach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaSach,TenSach,MoTa,DanhMuc,Gia,SoLuongTon,NamSanXuat,NhaXuatBan,TacGia,AnhBia,SoLuongCan,TinhTrang,SoLuongTonToiThieu,ThoiHan")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.DanhMuc);
            return View(sach);
        }

        public ActionResult Edit1(int? id)
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
            ViewBag.DanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.DanhMuc);
            return View(sach);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1([Bind(Include = "MaSach,TenSach,MoTa,DanhMuc,Gia,SoLuongTon,NamSanXuat,NhaXuatBan,TacGia,AnhBia,SoLuongCan,TinhTrang,SoLuongTonToiThieu,ThoiHan")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index1");
            }
            ViewBag.DanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.DanhMuc);
            return View(sach);
        }
        // GET: /Admin/Sach/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.DanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.DanhMuc);
            return View(sach);
        }

        // POST: /Admin/Sach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,MoTa,DanhMuc,Gia,SoLuongTon,NamSanXuat,NhaXuatBan,TacGia,AnhBia,SoLuongCan,TinhTrang,SoLuongTonToiThieu,ThoiHan")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.DanhMuc);
            return View(sach);
        }

        // GET: /Admin/Sach/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: /Admin/Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
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

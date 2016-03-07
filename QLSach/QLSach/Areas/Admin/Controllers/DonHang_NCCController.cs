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
    public class DonHang_NCCController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/DonHang_NCC/
        public ActionResult Index(int page=1, int pageSize=10)
        {
            var donhang_ncc = db.DonHang_NCC.Include(d => d.Sach).OrderBy(d=>d.MaDonHang).ToPagedList(page,pageSize);
            return View(donhang_ncc);
        }

        // GET: /Admin/DonHang_NCC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang_NCC donhang_ncc = db.DonHang_NCC.Find(id);
            if (donhang_ncc == null)
            {
                return HttpNotFound();
            }
            return View(donhang_ncc);
        }

        // GET: /Admin/DonHang_NCC/Create
        public ActionResult Create()
        {
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach");
            return View();
        }

        // POST: /Admin/DonHang_NCC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaDonHang,MaSach,NgayGiao,SoLuong,TinhTrang,TongTien,MaHopDong")] DonHang_NCC donhang_ncc)
        {
            if (ModelState.IsValid)
            {
                db.DonHang_NCC.Add(donhang_ncc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", donhang_ncc.MaSach);
            return View(donhang_ncc);
        }

        // GET: /Admin/DonHang_NCC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang_NCC donhang_ncc = db.DonHang_NCC.Find(id);
            if (donhang_ncc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", donhang_ncc.MaSach);
            return View(donhang_ncc);
        }

        // POST: /Admin/DonHang_NCC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaDonHang,MaSach,NgayGiao,SoLuong,TinhTrang,TongTien,MaHopDong")] DonHang_NCC donhang_ncc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donhang_ncc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSach = new SelectList(db.Saches, "MaSach", "TenSach", donhang_ncc.MaSach);
            return View(donhang_ncc);
        }

        // GET: /Admin/DonHang_NCC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang_NCC donhang_ncc = db.DonHang_NCC.Find(id);
            if (donhang_ncc == null)
            {
                return HttpNotFound();
            }
            return View(donhang_ncc);
        }

        // POST: /Admin/DonHang_NCC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang_NCC donhang_ncc = db.DonHang_NCC.Find(id);
            db.DonHang_NCC.Remove(donhang_ncc);
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

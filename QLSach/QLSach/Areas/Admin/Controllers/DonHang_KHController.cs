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
    public class DonHang_KHController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/DonHang_KH/
        public ActionResult Index(int page=1, int pageSize=10)
        {
            var donhang_kh = db.DonHang_KH.Include(d => d.KhachHang).OrderBy(d=>d.MaDonHang).ToPagedList(page,pageSize);
            return View(donhang_kh);
        }

        // GET: /Admin/DonHang_KH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang_KH donhang_kh = db.DonHang_KH.Find(id);
            if (donhang_kh == null)
            {
                return HttpNotFound();
            }
            return View(donhang_kh);
        }

        // GET: /Admin/DonHang_KH/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen");
            return View();
        }

        // POST: /Admin/DonHang_KH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaDonHang,NgayGiao,DiaChiGiao,TinhTrang,DaThanhToan,NgayLap,TongTien,MaKH")] DonHang_KH donhang_kh)
        {
            if (ModelState.IsValid)
            {
                db.DonHang_KH.Add(donhang_kh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donhang_kh.MaKH);
            return View(donhang_kh);
        }

        // GET: /Admin/DonHang_KH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang_KH donhang_kh = db.DonHang_KH.Find(id);
            if (donhang_kh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donhang_kh.MaKH);
            return View(donhang_kh);
        }

        // POST: /Admin/DonHang_KH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaDonHang,NgayGiao,DiaChiGiao,TinhTrang,DaThanhToan,NgayLap,TongTien,MaKH")] DonHang_KH donhang_kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donhang_kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "HoTen", donhang_kh.MaKH);
            return View(donhang_kh);
        }

        // GET: /Admin/DonHang_KH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang_KH donhang_kh = db.DonHang_KH.Find(id);
            if (donhang_kh == null)
            {
                return HttpNotFound();
            }
            return View(donhang_kh);
        }

        // POST: /Admin/DonHang_KH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang_KH donhang_kh = db.DonHang_KH.Find(id);
            db.DonHang_KH.Remove(donhang_kh);
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

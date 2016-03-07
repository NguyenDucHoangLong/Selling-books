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
    public class DanhMucController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/DanhMuc/
        public ActionResult Index(int page=1, int pageSize=10)
        {
            return View(db.DanhMucs.OrderBy(s=>s.MaDanhMuc).ToPagedList(page,pageSize));
        }

        // GET: /Admin/DanhMuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = db.DanhMucs.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        // GET: /Admin/DanhMuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/DanhMuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaDanhMuc,TenDanhMuc")] DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucs.Add(danhmuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhmuc);
        }

        // GET: /Admin/DanhMuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = db.DanhMucs.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        // POST: /Admin/DanhMuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaDanhMuc,TenDanhMuc")] DanhMuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhmuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhmuc);
        }

        // GET: /Admin/DanhMuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhmuc = db.DanhMucs.Find(id);
            if (danhmuc == null)
            {
                return HttpNotFound();
            }
            return View(danhmuc);
        }

        // POST: /Admin/DanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMuc danhmuc = db.DanhMucs.Find(id);
            
            var sach = db.Saches.Where(s => s.DanhMuc == id);
            foreach(var item in sach)
            {
                item.DanhMuc = null;
            }
            db.DanhMucs.Remove(danhmuc);
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

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
    public class NhaCungCapController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/NhaCungCap/
        public ActionResult Index(int page=1, int pageSize=10)
        {
            return View(db.NhaCungCaps.OrderBy(s=>s.MaNCC).ToPagedList(page,pageSize));
        }

        // GET: /Admin/NhaCungCap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhacungcap = db.NhaCungCaps.Find(id);
            if (nhacungcap == null)
            {
                return HttpNotFound();
            }
            return View(nhacungcap);
        }

        // GET: /Admin/NhaCungCap/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/NhaCungCap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaNCC,TenNCC,DiaChi,SDT,TaiKhoan,Email")] NhaCungCap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(nhacungcap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhacungcap);
        }

        // GET: /Admin/NhaCungCap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhacungcap = db.NhaCungCaps.Find(id);
            if (nhacungcap == null)
            {
                return HttpNotFound();
            }
            return View(nhacungcap);
        }

        // POST: /Admin/NhaCungCap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaNCC,TenNCC,DiaChi,SDT,TaiKhoan,Email")] NhaCungCap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhacungcap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhacungcap);
        }

        // GET: /Admin/NhaCungCap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCungCap nhacungcap = db.NhaCungCaps.Find(id);
            if (nhacungcap == null)
            {
                return HttpNotFound();
            }
            return View(nhacungcap);
        }

        // POST: /Admin/NhaCungCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhaCungCap nhacungcap = db.NhaCungCaps.Find(id);
            db.NhaCungCaps.Remove(nhacungcap);
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

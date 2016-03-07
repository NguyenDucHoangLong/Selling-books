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
    public class KhaNangCungCapController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/KhaNangCungCap/
        public ActionResult Index(int masach,  int page=1, int pageSize=10)
        {
            var khanangcungcap = db.KhaNangCungCaps.Include(k => k.NhaCungCap).Include(k => k.Sach).Where(k=>k.MaSach==masach).OrderBy(s=>s.MaNCC).ToPagedList(page,pageSize);
            return View(khanangcungcap);
        }
        [HttpGet]
        public ActionResult Delete(int MaNCC, int MaSach)
        {
            var result = db.KhaNangCungCaps.Where(s => s.MaNCC == MaNCC);
            foreach(var item in result)
            {
                if (@item.MaSach == MaSach)
                {
                    db.KhaNangCungCaps.Remove(item);
                    db.SaveChanges();
                }
            }
            return Redirect("/Admin/KhaNangCungCap/Index");
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

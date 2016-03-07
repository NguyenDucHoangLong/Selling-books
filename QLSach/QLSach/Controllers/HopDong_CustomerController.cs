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
    public class HopDong_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /HopDong/
        public ActionResult Index(int page=1,int pageSize=10)
        {
            if (Session["makh"] == null || Session["quyen"].ToString() != "Business")
                return RedirectToAction("Login","Logn_Customer", new { returnUrl="/HopDong/Index" });
            else
            {
                @ViewData["error"] = "success";
                if(Session["quyen"].ToString()== "Business")
                {
                    int id = int.Parse(Session["makh"].ToString());
                    var hopdongs = db.HopDongs.Include(h => h.NhaCungCap).Where(h => h.MaNCC == id).OrderBy(h => h.MaHopDong).ToPagedList(page, pageSize);
                    return View(hopdongs);
                }
                else
                {
                    @ViewData["error"] = "error";
                    ModelState.AddModelError("fail", "Bạn không có quyền truy cập vào đây");
                    var hopdong = db.HopDongs.Include(h => h.NhaCungCap).Where(h => h.MaNCC == 0).OrderBy(h => h.MaHopDong).ToPagedList(page, pageSize);
                    return View(hopdong);
                }
            }
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

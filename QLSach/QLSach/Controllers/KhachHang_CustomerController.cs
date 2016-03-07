using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;

namespace QLSach.Controllers
{
    public class KhachHang_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /KhachHang_Customer/
        public ActionResult Index()
        {
            int makh = int.Parse(Session["makh"].ToString());
            KhachHang kh = db.KhachHangs.Find(makh);
            return View(kh);
        }
        public void Insert(string hoten, DateTime ngsinh, string gioitinh, string diachi, string sdt, string email )
        {
            KhachHang kh = new KhachHang();
            kh.HoTen = hoten;
            kh.NgSinh = ngsinh;
            kh.GioiTinh = gioitinh;
            kh.DiaChi = diachi;
            kh.SDT = sdt;
            kh.Email = email;
            db.KhachHangs.Add(kh);
            db.SaveChanges();
        }
        public int MaxMaKH()
        {
            return db.KhachHangs.Max(s => s.MaKH);
        }
        public ActionResult Edit(FormCollection f)
        {
            KhachHang kh = db.KhachHangs.Find(int.Parse(f["makh"]));
            kh.HoTen = f["hoten"];
            kh.NgSinh = DateTime.Parse( f["ngaysinh"]);
            kh.GioiTinh = f["gioitinh"];
            kh.DiaChi = f["diachi"];
            kh.SDT = f["sdt"];
            kh.Email = f["email"];
            db.SaveChanges();
            return RedirectToAction("Index", "Home_Customer");
        }

        // GET: /KhachHang_Customer/Details/5

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

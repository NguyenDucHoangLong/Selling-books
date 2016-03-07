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
    public class CTDH_KH_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /CTDH_KH_Customer/
        public ActionResult Index(int id, int page=1, int pageSize=10)
        {
            var ctdh_kh = db.CTDH_KH.Include(c => c.DonHang_KH).Include(c => c.Sach).Where(c=>c.MaDonHang==id).OrderBy(c=>c.MaDonHang).ToPagedList(page,pageSize);
            return View(ctdh_kh);
        }
        public void Create(int madh, int masach, int soluong, decimal dongia)
        {
            CTDH_KH ctdh = new CTDH_KH();
            ctdh.MaDonHang = madh;
            ctdh.MaSach = masach;
            ctdh.DonGia = dongia;
            ctdh.SoLuong = soluong;
            ctdh.ThanhTien = soluong * dongia;
            db.CTDH_KH.Add(ctdh);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;

namespace QLSach.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        //
        // GET: /Admin/ThongKe/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThongKe(DateTime ngay1, DateTime ngay2)
        {
            DonHang_KH dh = db.DonHang_KH.Where(s => s.NgayLap <= ngay2 && s.NgayLap >= ngay1).FirstOrDefault();
            Decimal tongtien = 0;
            int tongdonhang = 0;
            int thatbai = 0;
            if(dh!=null)
            {
                tongtien = Decimal.Parse(db.DonHang_KH.Where(s => s.NgayLap <= ngay2 && s.NgayLap >= ngay1 && s.TinhTrang != "Delete").Sum(s => s.TongTien).ToString());
                tongdonhang = int.Parse(db.DonHang_KH.Where(s => s.NgayLap <= ngay2 && s.NgayLap >= ngay1).Count().ToString());
                thatbai = int.Parse(db.DonHang_KH.Where(s => s.NgayLap <= ngay2 && s.NgayLap >= ngay1 && s.TinhTrang == "Delete").Count().ToString());
            }
            var respone = new { Total=tongtien, Tong=tongdonhang, Fail=thatbai };
            return Json(respone, JsonRequestBehavior.AllowGet);
            
        }
	}
}
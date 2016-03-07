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
    public class DonHang_KH_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        private CTDH_KH_CustomerController ctdhkh = new CTDH_KH_CustomerController();

        // GET: /Customer_DonHang_KH/
        public ActionResult Index(int page=1, int pageSize=10)
        {
            if(Session["username"]==null && Session["makh"]==null)
            {
                return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/DonHang_KH_Customer/Index" });
            }
            else
            {
                //var donhang_kh="";
                @ViewData["error"] = "succes";
                if (Session["quyen"].ToString() == "Business" || Session["quyen"].ToString() == "Admin")
                {
                    @ViewData["error"] = "error";
                    ModelState.AddModelError("fail", "Bạn không có quyền truy cập vào đây");
                    var donhang = db.DonHang_KH.Where(d=>d.MaKH==1).Include(d => d.KhachHang).OrderByDescending(d => d.MaDonHang).ToPagedList(page, pageSize);
                    return View(donhang);
                }
                else
                {
                    int makh = int.Parse(Session["makh"].ToString());
                    var donhang_kh = db.DonHang_KH.Include(d => d.KhachHang).Where(d => d.MaKH == makh).OrderByDescending(d => d.MaDonHang).ToPagedList(page, pageSize);
                    return View(donhang_kh);
                }
            }
        }

        // GET: /Customer_DonHang_KH/Delete/5
        public ActionResult Delete(int id)
        {
            DonHang_KH donhang_kh = db.DonHang_KH.Find(id);
            if (donhang_kh == null)
            {
                return HttpNotFound();
            }
            else
            {
                DateTime today = DateTime.Now;
                if(today>=donhang_kh.NgayGiao ||donhang_kh.TinhTrang=="Finish")
                {
                    @ViewData["error"] = "succes";
                    ModelState.AddModelError("fail", "Bạn không thể hủy đơn hàng này");
                }
                else
                {
                    donhang_kh.TinhTrang = "Delete";
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult InformationOrder()
        {
            if (Session["makh"] == null)
            {
                return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/DonHang_KH_Customer/InformationOrder" });
            }
            else
            {
                if(Session["quyen"].ToString() == "Customer")
                {
                    int makh = int.Parse(Session["makh"].ToString());
                    KhachHang khachhang = db.KhachHangs.Find(makh);
                    return View(khachhang);
                }
                else
                {
                    ModelState.AddModelError("fail", "Bạn không có truy vập vào đây");
                    return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/DonHang_KH_Customer/InformationOrder" });
                }
            }
        }
        public ActionResult Insert(FormCollection f)
        {
            ShoppingCart lst = (ShoppingCart)Session["Cart"];
            DonHang_KH dh = new DonHang_KH();
            dh.NgayGiao = DateTime.Parse(f["Ngaygiao"]);
            dh.DiaChiGiao = f["diachi"];
            dh.NgayLap = DateTime.Today;
            dh.MaKH = int.Parse(f["makh"]);
            dh.DaThanhToan = false;
            dh.TinhTrang = "Start";
            dh.TongTien = lst.GetTotal();
            db.DonHang_KH.Add(dh);
            db.SaveChanges();
            foreach(var item in lst.ListItem)
            {
                ctdhkh.Create(dh.MaDonHang, item.ProductID, item.Quantity, item.Price);
                Sach sach = db.Saches.Find(item.ProductID);
                sach.SoLuongTon = sach.SoLuongTon - item.Quantity;
                db.SaveChanges();
            }
            if (f["thanhtoan"] == "0")
            {
                Session.Remove("Cart");
                return RedirectToAction("Index", "Home_Customer");
            }
            else
                return RedirectToAction("PayPalCheckOut", "PayPal", new { madonhang=dh.MaDonHang});
        }
        public void Update(int id)
        {
            DonHang_KH dh = db.DonHang_KH.Find(id);
            if(dh!=null)
            {
                dh.DaThanhToan = true;
                db.SaveChanges();
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

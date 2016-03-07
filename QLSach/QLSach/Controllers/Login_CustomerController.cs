using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;
using QLSach.Controllers;
using System.Text;
using System.Security.Cryptography;

namespace QLSach.Controllers
{
    public class Login_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        private KhachHang_CustomerController customer = new KhachHang_CustomerController();
        private NhaCungCap_CustomerController business = new NhaCungCap_CustomerController();
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home_Customer");
        }
        // GET: /Login_Customer/
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ActionResult CheckLogin(FormCollection f)
        {
            string user1 = f["username"];
            TaiKhoan kq = db.TaiKhoans.Where(s => s.TenDangNhap == user1).FirstOrDefault();
            if (kq.MatKhau == ToMD5(f["pass"]) && kq.TrangThai==true)
            {
                Session["username"] = f["username"];
                Session["makh"] = kq.MaNguoiDung;
                Session["quyen"] = kq.Quyen;
                return RedirectToLocal(f["returnUrl"]);
            }
            else
            {
                if (kq.TrangThai == false)
                    ModelState.AddModelError("fail", "Tài khoản đã bị khóa");
                else
                    ModelState.AddModelError("fail", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View("Login",ModelState);
            }
                
        }
        public ActionResult ChangePass()
        {
            return View();
        }
        public ActionResult ChangePassWord(FormCollection f)
        {
            int makh = int.Parse(Session["makh"].ToString());
            var result = db.TaiKhoans.Where(s => s.MaNguoiDung == makh);
            foreach(var item in result)
            {
                if(item.Quyen==Session["quyen"].ToString())
                {
                    if (item.MatKhau == ToMD5(f["oldpass"]))
                    {
                        TaiKhoan tk = db.TaiKhoans.Find(item.MaTK);
                        tk.MatKhau = ToMD5(f["newpass"]);
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("fail", "Nhập sai mật khẩu");
                        return View("ChangePass");
                    }   
                }
            }
            return RedirectToAction("CheckRole","Home_Customer");
        }
        // Thêm khách hàng
        public ActionResult CreateCustomer()
        {
            return View();
        }
        public ActionResult InsertCustomer(FormCollection f)
        {
            // thêm khách hàng
            customer.Insert(f["hoten"], DateTime.Parse(f["ngaysinh"]), f["gioitinh"], f["diachi"],f["sdt"], f["email"]);
            TaiKhoan tk = new TaiKhoan();
            tk.TenDangNhap = f["username"];
            tk.MatKhau = ToMD5(f["pass"]);
            tk.Quyen = "Customer";
            tk.TrangThai = true;
            tk.MaNguoiDung = customer.MaxMaKH();
            db.TaiKhoans.Add(tk);
            db.SaveChanges();
            return View("Login");
        }
        // Thêm nhà cung cấp
        public ActionResult CreateBusiness()
        {
            return View();
        }
        public ActionResult InsertBusiness(FormCollection f)
        {
            // thêm nhà cung cấp
            business.Insert(f["tenncc"], f["diachi"], f["sdt"], (f["taikhoan"]), f["email"]);
            int mancc= business.MaxNCC();
            // thêm token
            business.AddToken(mancc);
            // thêm tài khoản
            TaiKhoan tk = new TaiKhoan();
            tk.TenDangNhap = f["username"];
            tk.MatKhau = ToMD5(f["pass"]);
            tk.Quyen = "Business";
            tk.TrangThai = true;
            tk.MaNguoiDung = mancc;
            db.TaiKhoans.Add(tk);
            db.SaveChanges();
            return View("Login");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult CheckName(string name)
        {
            var response = new { Code = 0, Msg = "NO" };
            TaiKhoan tk = db.TaiKhoans.Where(s => s.TenDangNhap == name).FirstOrDefault();
            if (tk != null)
            {
                response = new { Code = 1, Msg = "YES" };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public string ToMD5(string input)
        {
            string result = "";
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach(byte b in buffer)
            {
                result += b.ToString("X2");
            }
            return result;
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home_Customer");
            }
        }
    }
}

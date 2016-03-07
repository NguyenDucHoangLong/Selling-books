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
using System.Security.Cryptography;
using System.Text;

namespace QLSach.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        // GET: /Admin/TaiKhoan/
        public ActionResult Index(int page=1, int pageSize=10)
        {
            return View(db.TaiKhoans.OrderBy(s=>s.MaTK).ToPagedList(page,pageSize));
        }

        // GET: /Admin/TaiKhoan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taikhoan = db.TaiKhoans.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // GET: /Admin/TaiKhoan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/TaiKhoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MaTK,TenDangNhap,MatKhau,TrangThai,Quyen,MaNguoiDung")] TaiKhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                taikhoan.MatKhau = ToMD5(taikhoan.MatKhau);
                db.TaiKhoans.Add(taikhoan);
                db.SaveChanges();
                return Redirect("/Admin/TaiKhoan/Index");
            }

            return View(taikhoan);
        }

        // GET: /Admin/TaiKhoan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taikhoan = db.TaiKhoans.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // POST: /Admin/TaiKhoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MaTK,TenDangNhap,MatKhau,TrangThai,Quyen,MaNguoiDung")] TaiKhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                taikhoan.MatKhau = ToMD5(taikhoan.MatKhau);
                db.Entry(taikhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taikhoan);
        }

        // GET: /Admin/TaiKhoan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taikhoan = db.TaiKhoans.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // POST: /Admin/TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taikhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taikhoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public string ToMD5(string input)
        {
            string result = "";
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (byte b in buffer)
            {
                result += b.ToString("X2");
            }
            return result;
        }
        public ActionResult CheckName(string name)
        {
            var response = new { Code = 0, Msg = "NO" };
            TaiKhoan tk = db.TaiKhoans.Where(s => s.TenDangNhap == name).FirstOrDefault();
            if(tk!=null)
            {
                response = new { Code = 1, Msg = "YES" };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ActionResult CheckLogin(FormCollection f)
        {
            string user1 = f["username"];
            TaiKhoan kq = db.TaiKhoans.Where(s => s.TenDangNhap == user1).FirstOrDefault();
            if (kq.MatKhau == ToMD5(f["pass"]) && kq.Quyen=="Admin")
            {
                Session["username"] = kq.TenDangNhap;
                Session["quyen"] = kq.Quyen;
                Session["makh"] = kq.MaNguoiDung;
                return RedirectToLocal(f["returnUrl"]);
            }
            else
            {
                if (kq.Quyen != "Admin")
                    ModelState.AddModelError("fail", "Không có quyền truy cập");
                else
                    ModelState.AddModelError("fail", "Tên đăng nhập hoặc mật khẩu không đúng");
                return Redirect("/Admin/TaiKhoan/Login");
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

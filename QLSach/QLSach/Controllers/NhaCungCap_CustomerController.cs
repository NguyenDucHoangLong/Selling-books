using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;
using System.Drawing;
using System.ComponentModel;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace QLSach.Controllers
{
    public class NhaCungCap_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        public ActionResult Index()
        {
            int makh = int.Parse(Session["makh"].ToString());
            NhaCungCap ncc = db.NhaCungCaps.Find(makh);
            Token tk = db.Tokens.Where(s => s.MaNCC == makh).FirstOrDefault();
            @ViewData["Access_token"] = tk.Access_token;
            @ViewData["Consumer_key"] = tk.Consumer_key;
            return View(ncc);
        }
        public void Insert(string tenncc, string diachi, string sdt, string taikhoan, string email)
        {
            NhaCungCap ncc = new NhaCungCap();
            ncc.TenNCC = tenncc;
            ncc.DiaChi = diachi;
            ncc.SDT = sdt;
            ncc.TaiKhoan = taikhoan;
            ncc.Email = email;
            db.NhaCungCaps.Add(ncc);
            db.SaveChanges();
        }
        public void AddToken(int mancc)
        {
            Token tk = new Token();
            Random rd = new Random();
            tk.Consumer_key = rd.Next(1, 1000).ToString();//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 1000
            tk.Request_token = "";
            tk.Veritify_token = "";
            tk.Access_token = "";
            tk.MaNCC = mancc;
            db.Tokens.Add(tk);
            db.SaveChanges();
        }
        public ActionResult Edit(FormCollection f)
        {
            NhaCungCap ncc = db.NhaCungCaps.Find(int.Parse(f["mancc"]));
            ncc.TenNCC = f["tenncc"];
            ncc.DiaChi = f["diachi"];
            ncc.SDT = f["sdt"];
            ncc.TaiKhoan = f["taikhoan"];
            ncc.Email = f["email"];
            db.SaveChanges();
            return RedirectToAction("Index", "Home_Customer");
        }
        public int MaxNCC()
        {
            return db.NhaCungCaps.Max(s => s.MaNCC);
        }
        public ActionResult GetConsumerKey(int mancc)
        {
            Random rd = new Random();
            Token tk = db.Tokens.Where(s => s.MaNCC==mancc).FirstOrDefault();
            tk.Consumer_key = rd.Next(1, 1000).ToString();//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 1000
            db.SaveChanges();
            return RedirectToAction("CheckRole","Home_Customer");
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

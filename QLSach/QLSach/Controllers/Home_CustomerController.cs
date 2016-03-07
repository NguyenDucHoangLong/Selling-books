using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;

namespace QLSach.Controllers
{
    public class Home_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        //
        // GET: /Home_Customer/
        public ActionResult Index()
        {
            int max = db.Saches.Max(p=>p.MaSach);
            int min = db.Saches.Min(p=>p.MaSach);
            var sach1 = db.Saches.Where(p=>p.MaSach >max-3);
            var sach2 = db.Saches.Where(p => p.MaSach < min + 3);
            @ViewData["sach"] = sach2.ToList();
            return View(sach1.ToList());
        }
        public ActionResult CheckRole()
        {
            if (Session["username"] == null && Session["makh"] == null)
            {
                return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/Home_Customer/CheckRole" });
            }
            else
            {
                if (Session["quyen"].ToString() == "Customer")
                    return RedirectToAction("Index", "KhachHang_Customer");
                else
                {
                    if (Session["quyen"].ToString() == "Business")
                        return RedirectToAction("Index", "NhaCungCap_Customer");
                    else
                        return RedirectToAction("Index","Home_Customer");
                }
            }
        }
        public ActionResult Contact(string suplier_key, string access_token)
        {
            return View();
        }
	}
}
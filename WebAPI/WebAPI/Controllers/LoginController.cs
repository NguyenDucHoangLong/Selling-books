using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CheckLogin(string name, string pass)
        {
            QLSachEntities db = new QLSachEntities();
            List<TaiKhoan> lst = db.TaiKhoans.ToList();
            foreach (TaiKhoan e in lst)
            {
                if (e.TenDangNhap == name && e.MatKhau == pass)
                {
                    Session["user"] = e.TenDangNhap;
                    if (e.Quyen == "Business")
                    {
                        Session["role"] = "Business";
                        Session["id"] = e.MaNguoiDung.ToString();
                    }
                    else
                    {
                        if (e.Quyen == "Admin")
                            Session["role"] = "Admin";
                        else
                            Session["role"] = "Customer";
                    }
                        
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Login");
        }
        public ActionResult Authorized()
        {
            return View();
        }
	}
}
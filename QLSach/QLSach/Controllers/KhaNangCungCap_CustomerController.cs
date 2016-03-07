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
    public class KhaNangCungCap_CustomerController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        // GET: /KhaNangCungCap_Customer/Create/
        public ActionResult Create(int id)
        {
            if (Session["makh"] == null)
            {
                return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/KhaNangCungCap/Create/"+id });
            }
            else
            {
                if(Session["quyen"].ToString() == "Business")
                {
                    @ViewData["MaNCC"] = Session["makh"];
                    @ViewData["MaSach"] = id;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/KhaNangCungCap/Create/" + id });
                }
            }
        }

        // POST: /KhaNangCungCap_Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert([Bind(Include="mancc,masach,dongia,soluong")] KhaNangCungCap khanangcungcap)
        {
            if (ModelState.IsValid)
            {
                db.KhaNangCungCaps.Add(khanangcungcap);
                db.SaveChanges();
                return RedirectToAction("BookBuy", "Sach_Customer");
            }
            return View();
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

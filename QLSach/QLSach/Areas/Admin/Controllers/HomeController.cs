using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLSach.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            if(Session["quyen"].ToString() != "Admin")
            {
                return RedirectToAction("Login", "TaiKhoan", new { returnUrl = "/Admin" });
            }
            return View();
        }
	}
}
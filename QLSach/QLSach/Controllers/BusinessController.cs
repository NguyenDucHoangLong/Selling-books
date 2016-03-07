using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;

namespace QLSach.Controllers
{
    public class BusinessController : Controller
    {
        private QLSachEntities db = new QLSachEntities();
        // GET: /Default1/
        static string _request_token;
        public ActionResult SetRequestToken(string request_token)
        {
            _request_token = request_token;
            ViewData["request_token"] = request_token;

            return View("auth");
        }

        public ActionResult Auth()
        {
            return View();
        }

        public ActionResult GetToken()
        {

            return View();
        }

        public ActionResult Verify()
        {
            string verifier_token = Request.QueryString["verifier_token"];
            ViewData["verifier_token"] = verifier_token;
            ViewData["request_token"] = _request_token;

            return View();
        }
        public ActionResult Index1()
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/Business/Index1" });
            else
            {
                @ViewData["username"] = Session["username"];
                int id = int.Parse(Session["makh"].ToString());
                var result=db.HopDongs.Where(s=>s.MaNCC==id && s.TinhTrang=="1").FirstOrDefault();
                if (result == null)
                    return View("Error");
                else
                    return View();
            }
            
        }
        public ActionResult Index2()
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "Login_Customer", new { returnUrl = "/Business/Index1" });
            else
            {
                @ViewData["username"] = Session["username"];
                int id = int.Parse(Session["makh"].ToString());
                var result = db.HopDongs.Where(s => s.MaNCC == id && s.TinhTrang == "1").FirstOrDefault();
                if (result == null)
                    return View("Error");
                else
                    return View();
            }
        }
        public ActionResult Error()
        {
            return View();
        }
	}
}
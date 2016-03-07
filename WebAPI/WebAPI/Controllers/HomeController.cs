using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InformationBook()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Login");
            else
            {
                if (Session["role"] == "Business")
                    return View();
                else
                    return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult InformationBook1()
        {
            //if (Session["user"] == null)
            //    return RedirectToAction("Login", "Login");
            //else
            //{
            //    if (Session["role"] == "Business")
            //        return View();
            //    else
            //        
            //}
            return View();
        }
	}
}
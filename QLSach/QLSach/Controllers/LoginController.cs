using QLSach.Custom;
using QLSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;

namespace WebAPI.Controllers
{
    public class LoginController : Controller
    {
        private QLSachEntities db = new QLSachEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string callback, string request_token)
        {
            ViewData["callback"] = callback;
            ViewData["token"] = request_token;
            return View();
        }


        [HttpPost]
        public ActionResult ChungThuc1(string callback, string token, string agree)
        {
            if (agree == "Yes")
            {
                var kt = db.Tokens.Where(t => t.Request_token == token).FirstOrDefault();

                if (true)
                {
                    Guid g = Guid.NewGuid();
                    string verifier_token = Convert.ToBase64String(g.ToByteArray());
                    verifier_token = verifier_token.Replace("=", "");
                    verifier_token = verifier_token.Replace("+", "");
                    verifier_token = verifier_token.Replace("/", "");

                    kt.Veritify_token = verifier_token;
                    db.SaveChanges();

                    return Redirect(callback + "?verifier_token=" + verifier_token);
                }
                else
                {
                    throw new HttpException(401, "Auth Failed");
                }
            }
            else
            {
                return Redirect(callback);
            }
        }

        [CheckLogin]
        public ActionResult ChungThuc(string request_token, string callback)
        {


            ViewData["callback"] = callback;
            ViewData["token"] = request_token;


            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string callback, string token)
        {
            if (ModelState.IsValid)
            {
                //Check thong tin dang nhap
                //Hash mat khau...
                string pass = ToMD5(password);
                var tk = db.TaiKhoans.Where(t => t.TenDangNhap == username && t.MatKhau == pass).FirstOrDefault();

                if (tk != null)
                {
                    //Check xem ma nha cung cap trong bang token co duoc cap cho user duoc dang nhap khong
                    //Neu khong thi user khong co quyen. Xuat thong bao
                    Session["username"] = username;

                    if (callback != string.Empty && token != string.Empty)
                    {
                        return RedirectToAction("ChungThuc", new { callback = callback, request_token = token });

                    }

                    return RedirectToAction("Index");


                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không đúng");
                    return RedirectToAction("Login", new { callback = callback, request_token = token });
                }
            }

            return View("Index");
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
	}
}
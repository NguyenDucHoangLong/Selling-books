using QLSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QLSach.Custom
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        private QLSachEntities db = new QLSachEntities();


        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                StartShipping ship = (StartShipping)actionContext.ActionArguments["ship"];

                if (ship.username != null && ship.password != null)
                {
                    bool kt = KiemTraUsername(ship.username, ship.password, ship.suppiler_key);
                    if (kt)
                    {
                        return;
                    }
                    else
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                        return;
                    }
                }
                else if (ship.access_token != null)
                {
                    bool kt = KiemTraToken(ship.access_token, ship.suppiler_key);
                    if (kt)
                    {
                        return;
                    }
                    else
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                        return;
                    }
                }



            }
            catch (Exception)
            {
            }

            try
            {

                string username = (string)actionContext.ActionArguments["username"];
                string password = (string)actionContext.ActionArguments["password"];
                string suppiler_key = (string)actionContext.ActionArguments["suppiler_key"];

                bool tk = KiemTraUsername(username, password, suppiler_key);
                //Kiem tra username, password
                if (tk)
                {
                    return;
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                    return;
                }
            }
            catch (Exception)
            {

            }

            try
            {
                string access_token = (string)actionContext.ActionArguments["access_token"];
                string suppiler_key = (string)actionContext.ActionArguments["suppiler_key"];


                if (KiemTraToken(access_token, suppiler_key))
                {
                    return;
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                    return;
                }

            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }


        }

        private bool KiemTraToken(string access_token, string consumer_key)
        {
            var kt = db.Tokens.Where(t => t.Access_token == access_token).FirstOrDefault();
            if (kt != null)
            {
                if (kt.Consumer_key != consumer_key) return false;
                //Neu co
                byte[] data = Convert.FromBase64String(access_token);
                DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
                DateTime now = DateTime.UtcNow;
                if (when < DateTime.UtcNow.AddHours(-1))
                {
                    kt.Access_token = "";

                    db.SaveChanges();

                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            // filterContext.Result = new RedirectResult("123");
            //return;
        }

        private bool KiemTraUsername(string username, string password, string consumer_key)
        {
            var tk = db.TaiKhoans.Where(t => t.TenDangNhap == username && t.MatKhau == password).FirstOrDefault();
            var token = db.Tokens.Where(t => t.Consumer_key == consumer_key).FirstOrDefault();

            if (tk == null || token == null)
            {

                return false;
            }
            else
            {
                if (tk.MaNguoiDung == token.MaNCC)
                    return true;
                else return false;

            }
        }
    }
}
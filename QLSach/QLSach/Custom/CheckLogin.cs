using QLSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLSach.Custom
{
    public class CheckLogin : ActionFilterAttribute, IActionFilter
    {
        private QLSachEntities db = new QLSachEntities();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Nếu chưa có session user (chưa đăng nhập)
            if (filterContext.HttpContext.Session["username"] == null)
            {
                string callback = filterContext.ActionParameters["callback"].ToString();
                string token = filterContext.ActionParameters["request_token"].ToString();

                string url = filterContext.HttpContext.Request.RawUrl;
                //Chuyen huong ve trang login


                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {{ "Controller", "Login" },
                                      { "Action", "Login" },
                                      {"callback", callback},
                                      {"request_token", token}
                                     
                
                });
                return;
            }


        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // filterContext.Result = new RedirectResult("123");
            //return;
        }
    }
}
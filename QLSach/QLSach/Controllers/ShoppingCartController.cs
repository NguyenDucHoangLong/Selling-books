using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLSach.Models;
using PagedList;

namespace QLSach.Controllers
{
    public class ShoppingCartController : Controller
    {
        private QLSachEntities db=new QLSachEntities();
        //
        // GET: /ShoppingCart/
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            ShoppingCart model = new ShoppingCart();
            model = (ShoppingCart)Session["Cart"];
            if(model!=null)
            {
                var result = model.ListItem.OrderBy(s => s.ProductID).ToPagedList(page, pageSize);
                return View(result);
            }
            else
                return View(model);
        }

        /// <summary>
        /// Them san pham vao gio hang
        /// </summary>
        /// <param name="id">ID san pham can them</param>
        /// <returns>Tra ve json theo dinh dang {Code: ReturnCode, Msg: "Return message"}</returns>
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            var response = new { Code = 1, Msg = "Fail" };
            Sach objProduct = db.Saches.Find(id);
            if (objProduct != null)
            {
                ShoppingCart objCart = (ShoppingCart)Session["Cart"];
                if (objCart == null)
                {
                    objCart = new ShoppingCart();
                }

                ShoppingCartItem item = new ShoppingCartItem()
                {
                    ProductID = objProduct.MaSach,
                    ProductName = objProduct.TenSach,
                    ProductImage = objProduct.AnhBia,
                    Price = objProduct.Gia.Value,
                    Quantity = 1,
                    Total = objProduct.Gia.Value,
                };

                objCart.AddToCart(item);
                Session["Cart"] = objCart;
                response = new { Code = 0, Msg = "Success" };
            }
            return Json(response,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Xoa san pham khoi gio hang
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var response = new { Code = 1, Msg = "Fail" };

            ShoppingCart objCart = (ShoppingCart)Session["Cart"];
            if (objCart != null)
            {
                objCart.RemoveFromCart(id);
                Session["Cart"] = objCart;
                response = new { Code = 0, Msg = "Success" };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cap nhat so luong san pham can mua trong gio hang
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var response = new { Code = 1, Msg = "Fail" };

            ShoppingCart objCart = (ShoppingCart)Session["Cart"];
            if (objCart != null)
            {
                if(CheckUpdateQuantity(id, quantity))
                {
                    objCart.UpdateQuantity(id, quantity);
                    Session["Cart"] = objCart;
                    response = new { Code = 0, Msg = "Success" };
                }
                else
                    response = new { Code = -1, Msg = "Số lượng không đủ" };
                
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public bool CheckUpdateQuantity(int id, int quantity)
        {
            Sach s = db.Saches.Find(id);
            if (s == null)
                return false;
            else 
            {
                if (quantity > s.SoLuongTon)
                    return false;
                else
                    return true;
            }
        }
        public ActionResult InformationCart()
        {
            
            ShoppingCart model = new ShoppingCart();
            model = (ShoppingCart)Session["Cart"];
            if(model!=null)
            {
                var response = new { Total = model.GetTotal(), Item = model.GetItem() };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var response = new { Total = 0, Item = 0 };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
	}
}
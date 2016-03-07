using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private QLSachEntities db = new QLSachEntities();
        // chứng thực access_token
        [Route("api/orders")]
        [HttpGet]
        public IHttpActionResult Get(string id, string access_token)
        {
            if (CheckAccessToken(access_token)==false)
                return Unauthorized();
            else
            {
                var result = db.ThongTinSanPham(int.Parse(id));
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
        }

        [Route("api/orders/start_shipping")]
        [HttpPost]
        public IHttpActionResult Start_shipping(StartShipping ship)
        {
            if (CheckAccessToken(ship.access_token) == false)
                return Unauthorized();
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string tinhtrang = "Start";
                var result = db.ThemDonHang(ship.product_id, ship.product_date, ship.product_quantity, tinhtrang, ship.order_id);
                return Ok(result);
            }
        }

        // chứng thực username, password

        [Route("api/orders/information")]
        [HttpGet]
        public IHttpActionResult Infmation(string id, string username, string password)
        {
            int Ma = int.Parse(id);
            var tk = db.TaiKhoans.Where(t => t.TenDangNhap == username).FirstOrDefault();
            //if (tk.TenDangNhap == username && tk.MatKhau == password && tk.Quyen == "Business" && tk.MaNguoiDung == Ma)
            //{
                var result = db.ThongTinSanPham(int.Parse(id));
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            //}
            //else
            //{
            //    return Unauthorized();
            //}
        }

        [Route("api/orders/start_shipping1")]
        [HttpPost]
        public IHttpActionResult Start_shipping1(StartShipping1 ship)
        {
            var tk = db.TaiKhoans.Where(t => t.TenDangNhap == ship.username).FirstOrDefault();
            if (tk.TenDangNhap == ship.username && tk.MatKhau == ship.password && tk.Quyen == "Business" && tk.MaNguoiDung == ship.suplier_key)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string tinhtrang = "Start";
                var result = db.ThemDonHang(ship.product_id, ship.product_date, ship.product_quantity, tinhtrang, ship.order_id);
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
        public bool CheckAccessToken(string access_token)
        {
            List<Token> lst = db.Tokens.ToList();
            foreach(Token t in lst)
            {
                if (t.Access_token == access_token)
                    return true;
            }
            return false;
        }
    }
}

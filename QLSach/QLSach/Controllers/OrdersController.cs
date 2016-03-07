using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLSach.Models;

namespace QLSach.Controllers
{
    public class OrdersController : ApiController
    {
        private QLSachEntities db = new QLSachEntities();
        // truyền vào access_token
        [Route("api/orders")]
        [HttpPost]
        public IHttpActionResult Get(StartShipping ship)
        {

            try
            {
                var token = db.Tokens.Where(t => t.Consumer_key == ship.suppiler_key).FirstOrDefault();
                int id = (int)token.MaNCC;
                var result = db.ThongTinSanPham(id);
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // truyền vào username, password


        [Route("api/orders/start_shipping")]
        [HttpPost]
        public IHttpActionResult Start_shipping(StartShipping ship)
        {
            Sach sach = db.Saches.Find(ship.product_id);
            var basd = new { Code = "0" };
            if (sach.SoLuongTon > sach.SoLuongTonToiThieu)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string tinhtrang = "Start";
            var result = db.ThemDonHang(ship.product_id, ship.product_date, ship.product_quantity, tinhtrang, ship.order_id);
            return Ok(result);

        }

    }
}

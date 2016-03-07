using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class StartShipping
    {
        public int suplier_key { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int product_quantity { get; set; }
        public DateTime product_date { get; set; }
        public string access_token { get; set; }
    }
}
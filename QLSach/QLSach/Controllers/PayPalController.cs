using QLSach.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QLSach.Controllers
{
    public class PayPalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaypalCheckout(int madonhang)
        {
            ViewData["business"] = "longdang123@gmail.com";
            ViewData["notify_url"] = "http://localhost:5589/Paypal/IPN";
            ViewData["return"] = "http://localhost:5589/Paypal/IPN";

            ViewData["madonhang"] = madonhang;
            ShoppingCart model = new ShoppingCart();
            model = (ShoppingCart)Session["Cart"];
            Session.Remove("Cart");
            return View(model.ListItem.ToList());
        }


        public ActionResult IPN()
        {
            var formVals = new Dictionary<String, String>();

            formVals.Add("cmd", "_notify-validate");

            string response = GetPayPalResponse(formVals, true);

            if (response == "VERIFIED")
            {
                string transactionID = Request["txn_id"];
                string sAmountPaid = Request["mc_gross"];
                string madonhang = Request["custom"];
                string business = Request["business"];
                string mc_currency = Request["mc_currency"];
                ////@ViewData["transactionID"] = transactionID;
                ////@ViewData["sAmountPaid"] = sAmountPaid;
                ////@ViewData["madonhang"] = madonhang;
                ////@ViewData["business"] = business;
                ////@ViewData["mc_currency"] = mc_currency;

                //Ghi chu cua khach hang
                string memo = Request["memo"];

                string payment_status = Request["payment_status"];

                Decimal amountPaid = 0;
                Decimal.TryParse(sAmountPaid, out amountPaid);

                if (payment_status != "Completed")
                {
                    return View("ErrorPayment");
                }
                if (business != "longdang123@gmail.com")
                {
                    return View("ErrorPayment");
                }
                if (sAmountPaid != "2.95")
                {
                    //Kiem tra tien tren don hang va tien nhan duoc

                    //return View("ErrorPayment");
                }

                if (mc_currency != "USD")
                {
                    //Tien khac USD
                    return View("ErrorPayment");
                }

                if (transactionID != "123")
                {
                    //Kiem tra transactionID da tung ton tai tren CSDL chua
                }
                DonHang_KH_CustomerController dhkh = new DonHang_KH_CustomerController();
                dhkh.Update(int.Parse(madonhang));
                return RedirectToAction("Index", "Home_Customer");
            }
            else
            {
                return View("ErrorPayment");
            }
        }
        public ActionResult ErrorPayment()
        {
            return View();
        }
        string GetPayPalResponse(Dictionary<String, String> formVals, bool useSandbox)
        {

            // Parse the variables
            // Choose whether to use sandbox or live environment
            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
            : "https://www.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
	}
}
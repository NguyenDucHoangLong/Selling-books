using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class OauthController : ApiController
    {
        private QLSachEntities db = new QLSachEntities();

        //request_token
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public IHttpActionResult request_token(string consumer_key,string callback)
        {
            var kt = db.Tokens.Where(t => t.Consumer_key == consumer_key).FirstOrDefault();
            if (kt != null)
            {
                Guid g = Guid.NewGuid();
                string request_token = Convert.ToBase64String(g.ToByteArray());
                request_token = request_token.Replace("=", "");
                request_token = request_token.Replace("+", "");
                request_token = request_token.Replace("/", "");

                kt.Request_token = request_token;
                db.SaveChanges();
                return Ok(kt.Request_token);


            }
            else
            {
                throw new HttpException(401, "Auth Failed");
            }
            
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public IHttpActionResult authenticate(string request_token)
        {
            var kt = db.Tokens.Where(t => t.Request_token == request_token).FirstOrDefault();
            if (kt != null)
            {
                Guid g = Guid.NewGuid();
                string verifier_token = Convert.ToBase64String(g.ToByteArray());
                verifier_token = verifier_token.Replace("=", "");
                verifier_token = verifier_token.Replace("+", "");
                verifier_token = verifier_token.Replace("/", "");

                kt.Veritify_token = verifier_token;
                db.SaveChanges();

                //var result = { callback = "http://localhost:3338/api/orders",verifilier_token =kt.Veritify_token };
                return Ok(kt.Veritify_token);
             }
             else
             {
                throw new HttpException(401, "Auth Failed");
             }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public IHttpActionResult access_token(string consumer_key, string request_token, string verifier_token)
        {
            var kt = db.Tokens.Where(t => t.Consumer_key == consumer_key).FirstOrDefault();

            if (kt != null && kt.Request_token == request_token && kt.Veritify_token == verifier_token)//Kiem tra 3 thong so
            {
                DateTime date = DateTime.UtcNow;
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string access_token = Convert.ToBase64String(time.Concat(key).ToArray());


                kt.Access_token = access_token;
                db.SaveChanges();


                return Ok(kt.Access_token);

            }

            throw new HttpException(401, "Auth Failed");
        }

    }
}

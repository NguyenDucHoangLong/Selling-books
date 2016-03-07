using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using QLSach.Models;

namespace QLSach.Controllers
{
    public class OauthController : ApiController
    {
        private QLSachEntities db = new QLSachEntities();



        [Route("api/oauth/request_token")]
        [HttpPost]
        public string request_token([FromBody]string consumer_key)
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

                return request_token;

            }
            else
            {
                throw new HttpException(401, "Auth Failed");
            }

            //request_token
        }

        [Route("api/oauth/authenticate")]
        [HttpPost]
        public IHttpActionResult authenticate(TokenInfo token)
        {

            String baseUri = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, String.Empty);

          
            var kt = db.Tokens.Where(t => t.Request_token == token.request_token).FirstOrDefault();

            if (kt != null)
            {
                IHttpActionResult response;
                HttpResponseMessage responseMsg = Request.CreateResponse<TokenInfo>(HttpStatusCode.OK, token);

                responseMsg.Headers.Location = new Uri(baseUri + "/Login/ChungThuc");
                response = ResponseMessage(responseMsg);

                return response;
            }
            else
            {
                throw new HttpException(401, "Auth Failed");
            }

        }


        [System.Web.Http.HttpPost]
        public string access_token(TokenInfo token)
        {
            var kt = db.Tokens.Where(t => t.Consumer_key == token.consumer_key).FirstOrDefault();


            if (kt != null && kt.Consumer_key == token.consumer_key && kt.Request_token == token.request_token && kt.Veritify_token == token.verifier_token)//Kiem tra 3 thong so
            {
                DateTime date = DateTime.UtcNow;
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string access_token = Convert.ToBase64String(time.Concat(key).ToArray());

                kt.Access_token = access_token;

                kt.Request_token = string.Empty;
                kt.Veritify_token = string.Empty;

                db.SaveChanges();


                return access_token;

            }

            throw new HttpException(401, "Auth Failed");
        }
        
        
    }
}

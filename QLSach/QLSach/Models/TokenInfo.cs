using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLSach.Models
{
    public class TokenInfo
    {
        public string request_token { get; set; }
        public string callback { get; set; }
        public string consumer_key { get; set; }
        public string verifier_token { get; set; }
    }
}
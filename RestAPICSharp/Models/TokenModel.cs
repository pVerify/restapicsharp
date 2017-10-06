using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace RestAPICSharp.Models
{
    public class TokenModel:APIResponse
    {

        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class APIResponse
    {

        public string ApiResponse { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Token { get; set; }
        public string ClientUserName { get; set; }
        public string ClientPassword { get; set; }
    }
}
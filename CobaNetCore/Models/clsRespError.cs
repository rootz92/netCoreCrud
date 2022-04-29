using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace CobaNetCore.Models
{
    public class clsRespError
    {
        public HttpStatusCode statusCode { get; set; }
        public string error { get; set; }
        public string message { get; set; }
    }
}

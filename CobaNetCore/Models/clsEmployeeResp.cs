using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace CobaNetCore.Models
{
    public class clsEmployeeResp
    {
        public HttpStatusCode statusCode { get; set; }
        public string error { get; set; }
        public clsEmployee[] employees { get; set; }
    }
}

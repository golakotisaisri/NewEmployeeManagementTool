using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployeeManagemnetAppLib.Models
{
    public class ResponseData_Handler
    {
        public string code { get; set; }
        public Meta meta { get; set; }
        public IEnumerable<Response> data { get; set; }
    }

    public class Response
    {
        public string field { get; set; }
        public string message { get; set; }
    }
}


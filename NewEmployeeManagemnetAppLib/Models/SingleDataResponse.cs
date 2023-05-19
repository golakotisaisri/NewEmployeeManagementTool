using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployeeManagemnetAppLib.Models
{

    public class SingleDataResponse:Response
    {
        public string code { get; set; }
        public Meta meta { get; set; }
        public EmployeeData data { get; set; }
    }

}


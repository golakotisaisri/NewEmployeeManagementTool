using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEmployeeManagemnetAppLib.Models
{
    public class ResponseData
    {
        public string code { get; set; }
        public Meta meta { get; set; }
        public IEnumerable<EmployeeData> data { get; set; }
    }
    public class Meta
    {
        public Pagination pagination { get; set; }
    }
    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }

    }
}

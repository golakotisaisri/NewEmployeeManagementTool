using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NewEmployeeManagementTool.Models
{
    public class EmployeeViewModel
    {
        [DisplayName("Employee Id")]
        public int id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Email")]
        [Required]
        public string email { get; set; }
        [Required]
        [DisplayName("Gender")]
        [RegularExpression("male|female|Male|Female|MALE|FEMALE", ErrorMessage = "The Gender must be either 'male' or 'female' only.")]
        public string gender { get; set; }
        [Required]
        [RegularExpression("active|inactive|Active|InActive|ACTIVE|INACTIVE", ErrorMessage = "The status must be either 'active' or 'inactive' only.")]
        [DisplayName("Status")]

        public string status { get; set; }
    }
}
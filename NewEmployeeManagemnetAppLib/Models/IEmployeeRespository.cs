using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;


namespace NewEmployeeManagemnetAppLib.Models
{
    public interface IEmployeeRespository
    {
        ResponseData GetAllEmployeeList(int pageNumber);
        ResponseData GetEmployeeDetails(int employeeId);
        Task<SingleDataResponse> InsertEmployee(EmployeeData employee);
    }
}

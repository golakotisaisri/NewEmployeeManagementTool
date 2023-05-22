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
    public class EmployeeRepository : IEmployeeRespository
    {
        public static string ServiceUrl = "https://gorest.co.in/public-api/users";

        //Get all employee list
        public ResponseData GetAllEmployeeList(int pageNumber)
        {
            ResponseData responseData = new ResponseData();
            var client =new HttpClient();
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            string urlParameters = $"?page={pageNumber}";
            try
            {
                client = GetHttpClientObj();

                // Get data response
                var response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body
                    var EmpResponse = response.Content.ReadAsStringAsync().Result;
                    responseData = JsonConvert.DeserializeObject<ResponseData>(EmpResponse);
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepton at GetAllEmployeeList: {ex.Message},{ex.StackTrace}");
            }

            return responseData;
        }

        //Get employee details by employee id
        public ResponseData GetEmployeeDetails(int employeeId)
        {
            ResponseData responseData = new ResponseData();
            string urlParameters = $"?id={employeeId}";
            var client = new HttpClient();
            try
            {
                // Getting client object
                client = GetHttpClientObj();

                // Get data response
                var response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body
                    var EmpResponse = response.Content.ReadAsStringAsync().Result;
                    responseData = JsonConvert.DeserializeObject<ResponseData>(EmpResponse);
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepton at GetEmployeeDetails: {ex.Message},{ex.StackTrace}");
            }

            return responseData;
        }

        //Insert new employee
        public async Task<SingleDataResponse> InsertEmployee(EmployeeData employeeData)
        {
            //Insert new employee
            SingleDataResponse singleDataResponse = new SingleDataResponse();
            var client = new HttpClient();
            try
            {
                // Getting client object
                client = GetHttpClientObj();

                // Initialization.
                HttpResponseMessage response = new HttpResponseMessage();

                // Http Post  
                response = await client.PostAsJsonAsync(ServiceUrl, employeeData).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    string empResponse = response.Content.ReadAsStringAsync().Result;
                    dynamic dynamicResponse = JsonConvert.DeserializeObject<dynamic>(empResponse);
                    int code = Convert.ToInt16(dynamicResponse.code);

                    //Converting reponse object based on error code 
                    if (code == 422)
                    {
                        ResponseData_Handler responseData_Handler = JsonConvert.DeserializeObject<ResponseData_Handler>(empResponse);
                        singleDataResponse.code = "422";
                        if (responseData_Handler != null && responseData_Handler.data != null)
                        {
                            foreach (var item in responseData_Handler.data)
                            {
                                singleDataResponse.data = new EmployeeData();
                                singleDataResponse.data.email = employeeData.email;
                                singleDataResponse.message = item.message;
                            }
                        }
                    }
                    else
                    {
                        singleDataResponse = JsonConvert.DeserializeObject<SingleDataResponse>(empResponse);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception at InsertEmployee()", ex.Message);
            }
            return singleDataResponse;
        }

        //Create generic http client object
        public HttpClient GetHttpClientObj()
        {
            var client = new HttpClient();
            try
            {
                // Setting Base address.
                client.BaseAddress = new Uri(ServiceUrl);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepton at  GetHttpClientObj: {ex.Message},{ex.StackTrace}");
            }
            return client;
        }
    }
}

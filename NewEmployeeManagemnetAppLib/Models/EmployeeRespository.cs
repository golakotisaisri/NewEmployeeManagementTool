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

        public ResponseData GetAllEmployeeList(int pageNumber)
        {
            string url = "https://gorest.co.in/public-api/users";
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            string urlParameters = $"?page={pageNumber}";
            var client = new HttpClient();
            ResponseData responseData = new ResponseData();
            try
            {
                client.BaseAddress = new Uri(url);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");

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

        public ResponseData GetEmployeeDetails(int employeeId)
        {
            string url = "https://gorest.co.in/public-api/users";
            string urlParameters = $"?id={employeeId}";
            var client = new HttpClient();
            ResponseData responseData = new ResponseData();
            try
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");


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

        public async Task<SingleDataResponse> InsertEmployee(EmployeeData employeeData)
        {
            //Insert Employee
            string url = "https://gorest.co.in/public-api/users";
            SingleDataResponse singleDataResponse = new SingleDataResponse();
            try
            {
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri(url);

                    // Setting content type.                   
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = await client.PostAsJsonAsync(url, employeeData).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string empResponse = response.Content.ReadAsStringAsync().Result;
                        dynamic dynamicResponse = JsonConvert.DeserializeObject<dynamic>(empResponse);
                        int code = Convert.ToInt16(dynamicResponse.code);
                        if (code == 422)
                        {
                          ResponseData_Handler  responseData_Handler = JsonConvert.DeserializeObject<ResponseData_Handler>(empResponse);
                            singleDataResponse.code ="422";
                            if (responseData_Handler != null && responseData_Handler.data!=null)
                            {
                                
                                foreach(var item in responseData_Handler.data)
                                {
                                    singleDataResponse.data = new EmployeeData();
                                    singleDataResponse.data.email=employeeData.email;
                                    singleDataResponse.message=item.message;
                                }
                            }
                            
                        }
                        else
                        {
                            singleDataResponse = JsonConvert.DeserializeObject<SingleDataResponse>(empResponse);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception at InsertEmployee", ex.Message);
            }
            return singleDataResponse;
        }

        //public async Task<SingleDataResponse> UpdateEmployee(EmployeeData employeeData)
        //{
        //    //Update Employee
        //    //Insert Employee
        //    string url = $"https://gorest.co.in/public-api/users/{employeeData.id}";
        //    SingleDataResponse singleDataResponse = new SingleDataResponse();
        //    try
        //    {
        //        // Posting.  
        //        using (var client = new HttpClient())
        //        {
        //            // Setting Base address.  
        //            client.BaseAddress = new Uri(url);

        //            // Setting content type.                   
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");

        //            // Initialization.
        //            HttpResponseMessage response = new HttpResponseMessage();

        //            // HTTP POST  
        //            response = await client.PostAsJsonAsync(url, employeeData).ConfigureAwait(false);

        //            // Verification  
        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Reading Response.  
        //                string empResponse = response.Content.ReadAsStringAsync().Result;
        //                singleDataResponse = JsonConvert.DeserializeObject<SingleDataResponse>(empResponse);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine("Exception at InsertEmployee", ex.Message);
        //    }
        //    return singleDataResponse;
        //}

        //public void DeleteEmployee(int employeeId)
        //{
        //    //Delete Employee
        //}

    }
}

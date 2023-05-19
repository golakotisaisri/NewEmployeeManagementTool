using NewEmployeeManagementTool.Models;
using NewEmployeeManagemnetAppLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewEmployeeManagementTool.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRespository _employeeRepository;
        public EmployeeController()
        {
            this._employeeRepository = new EmployeeRepository();
        }

        // GET: Employee
        public ActionResult Index()
        {

            List<EmployeeViewModel> employeeViewData = new List<EmployeeViewModel>();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            try
            {
                int pagenumber = 1;
                if (TempData["pageNum"] != null)
                {
                    pagenumber = (int)TempData["pageNum"];
                }
                ViewBag.pageNum = pagenumber;

                ResponseData responseData = _employeeRepository.GetAllEmployeeList(pagenumber);
                IEnumerable<EmployeeData> employeeData = responseData.data;
                foreach (EmployeeData emp in employeeData)
                {
                    employeeViewModel = new EmployeeViewModel();
                    employeeViewModel.id = emp.id;
                    employeeViewModel.name = emp.name;
                    employeeViewModel.email = emp.email;
                    employeeViewModel.gender = emp.gender;
                    employeeViewModel.status = emp.status;
                    employeeViewData.Add(employeeViewModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception at index action ", ex.Message);
            }

            return View(employeeViewData);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult pageAction(int id)
        {
            TempData["pageNum"] = id;
            return RedirectToAction("Index");
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeViewData)
        {
            try
            {
                // TODO: Add insert logic here
                EmployeeData employeeData = new EmployeeData();
                employeeData.name = employeeViewData.name;
                employeeData.email = employeeViewData.email;
                employeeData.gender = employeeViewData.gender;
                employeeData.status = employeeViewData.status;
                SingleDataResponse singleDataResponse = _employeeRepository.InsertEmployee(employeeData).Result;
                if (singleDataResponse.code == "422")
                {
                    ViewBag.Email = singleDataResponse.data.email;
                    ViewBag.ErrorMessage = "Email already exists";
                    return View("Error");
                }
                return RedirectToAction("Details", new { id = singleDataResponse.data.id });
            }
            catch (Exception ex)
            {
                ViewBag.Email = employeeViewData.email;
                ViewBag.ErrorMessage = "Exception occurs" + ex.Message;
                return View("Error");
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeViewModel employeeViewModel)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            ResponseData responseData = _employeeRepository.GetEmployeeDetails(id);
            IEnumerable<EmployeeData> employeeData = null;
            employeeData = responseData.data;
            foreach (EmployeeData emp in employeeData)
            {
                employeeViewModel = new EmployeeViewModel();
                employeeViewModel.id = emp.id;
                employeeViewModel.name = emp.name;
                employeeViewModel.email = emp.email;
                employeeViewModel.gender = emp.gender;
                employeeViewModel.status = emp.status;
            }
            return View(employeeViewModel);
        }




        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
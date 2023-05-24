using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewEmployeeManagemnetAppLib.Models;
using System;

namespace NewEmployeeManagementTool.Tests
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void GetAllEmployeesTestMethod()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            ResponseData responseData = employeeRepository.GetAllEmployeeList(1);
            Assert.IsNotNull(responseData);
        }
        [TestMethod]
        public void InsertEmployeeTestMethod()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeData employeeData = new EmployeeData();
            employeeData.name = "saisri";
            employeeData.email = "gola78902@gmail.com";
            employeeData.gender = "female";
            employeeData.status = "active";
            SingleDataResponse responseData = employeeRepository.InsertEmployee(employeeData).Result;
            Assert.IsTrue((responseData.code == "201" || responseData.code == "422") ? true : false, "Failed");
        }
        [TestMethod]
        public void AlreadyExistEmployeeTestMethod()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeData employeeData = new EmployeeData();
            employeeData.name = "saisrig";
            employeeData.email = "saisrig@gmail.com";
            employeeData.gender = "female";
            employeeData.status = "active";
            SingleDataResponse responseData = employeeRepository.InsertEmployee(employeeData).Result;
            Assert.AreEqual(responseData.code, "422");
        }
        [TestMethod]
        public void GetEmployeeDetailsTestMethod()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeData employeeData = new EmployeeData();

            ResponseData responseData = employeeRepository.GetEmployeeDetails(2008037);
            Assert.IsNotNull(responseData.code);
        }
        [TestMethod]
        public void UpdateEmployeeTestMethod()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeData employeeData = new EmployeeData();
            employeeData.id = 2078555;
            employeeData.name = "saisri";
            employeeData.email = "saisrigo777@gmail.com";
            employeeData.gender = "female";
            employeeData.status = "active";
            SingleDataResponse responseData = employeeRepository.UpdateEmployee(employeeData).Result;
            Assert.IsTrue(responseData.code == "200");
        }

    }
}

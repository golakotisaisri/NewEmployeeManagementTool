using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewEmployeeManagementTool;
using NewEmployeeManagementTool.Controllers;
using NewEmployeeManagementTool.Models;
using NewEmployeeManagemnetAppLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace NewEmployeeManagementTool.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void IndexActionTestMethod()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
       
        [TestMethod]

        public void GetActionTestMethod()
        {
            // Arrange
            EmployeeController controller = new EmployeeController();
          
            // Act
            ViewResult result = controller.Details(2008037) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


    }
}

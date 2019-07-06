using System;
using System.Web.Http;
using System.Web.Http.Results;
using CustomerAPI.Controllers;
using CustomerAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerAPI.Tests.Controllers
{
    [TestClass]
    public class CustomerUnitTest
    {

        [TestMethod]
        public void GetCustomerWithNoTransactionByCustid_ValidData()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            Customer c = new Customer() { customerID = 4};

            IHttpActionResult result = controller.PostCustomers(c);

            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
           
        }
        [TestMethod]
        public void GetCustomerWithNoTransactionByCustid_InValidData()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            Customer c = new Customer() { customerID =9 };

            IHttpActionResult result = controller.PostCustomers(c);


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }


        [TestMethod]
        public void GetCustomerWithOneTransactionByEmailId_ValidData()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            Customer c = new Customer() {  email = "joh.juwis1@gmail.com" };

            IHttpActionResult result = controller.PostCustomers(c);
            var createdResult = result as OkNegotiatedContentResult<Customer>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));

        }
        [TestMethod]
        public void GetCustomerWithOneTransactionByEmailId_InValidData()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            Customer c = new Customer() {  email = "" };

            IHttpActionResult result = controller.PostCustomers(c);


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }


        [TestMethod]
        public void GetAllTransactionByCustid_And_Email_ValidData()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            Customer c = new Customer(){customerID=4,email="joh.juwis1@gmail.com"};

            IHttpActionResult result = controller.PostCustomers(c) ;

             
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
           

        }
        [TestMethod]
        public void GetAllTransactionByCustid_And_Email_withInvalidCustomerData()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            Customer c = new Customer() { customerID = 6, email = "joh.juwis1@gmail.com" };

            IHttpActionResult result = controller.PostCustomers(c);


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }
        [TestMethod]
        public void GetAllTransactionByCustid_And_Email_emptyEmail()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            Customer c = new Customer() { customerID = 6, email = "" };

            IHttpActionResult result = controller.PostCustomers(c);


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));

        }
    }
}

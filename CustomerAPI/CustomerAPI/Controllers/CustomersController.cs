using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerAPI.Models;
using CustomerAPI.Repository;
using FluentValidation;

namespace CustomerAPI.Controllers
{
    public class CustomersController : ApiController
    {
        #region Global Declaration
            private IRepository<Customer> _Customerrepository = null;
            public CustomersController()
            {
                this._Customerrepository = new Repository<Customer>();
            }
        #endregion#


        //  private CustomerEntities db = new CustomerEntities();


        // GET: api/Customers
        public IHttpActionResult GetCustomers()
        {
            
            var response = _Customerrepository.GetAll();
                     
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // POST: api/Customers/
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomers(Customer customer)
        {
            object obj= new object() { };
            object transactions = null;
            Customer CustResult = null ;
            try
            {
                if (customer.email != null && customer.customerID != 0)
                {
                    if (ModelState.IsValid == false || string.IsNullOrEmpty(customer.email))
                    {
                        return BadRequest("Invalid  Email or Customer ID");
                    }

                    CustResult = _Customerrepository.GetById(customer.customerID);
                    if (CustResult != null)
                        transactions = CustResult.Transactions.Select(p => new { p.id, p.date, p.amount, p.currency, p.status });
                }
                else if (customer.email != null)
                {
                    if (ModelState.IsValid == false)
                    {
                        return BadRequest("Invalid Email Address");
                    }
                    CustResult = _Customerrepository.GetByEmail(customer.email);
                    Transaction tran = CustResult.Transactions.Take(1).FirstOrDefault();

                    CustResult.Transactions.Clear();
                    CustResult.Transactions.Add(tran);
                    transactions = CustResult.Transactions.Select(p => new { p.id, p.date, p.amount, p.currency, p.status });

                }
                else if (customer.customerID != 0)
                {
                    if (ModelState.IsValid == false)
                    {
                        return BadRequest("Invalid CustomerID");
                    }
                    CustResult = _Customerrepository.GetById(customer.customerID);
                    CustResult.Transactions.Clear();
                    transactions = CustResult.Transactions;
                }
                else
                {
                    return BadRequest("No inquiry criteria");

                }

            }
            catch (Exception)
            {
                return BadRequest();
            }
            
          
            if (CustResult == null)
            {
                return NotFound();
            }

            return Ok(new {
                            CustResult.customerID,
                            CustResult.name,
                            CustResult.email,
                            CustResult.mobile,
                            transactions
                            });
        }





    }
}
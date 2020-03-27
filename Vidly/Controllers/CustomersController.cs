using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer() { Id = 1, Name = "John Smith" },
                new Customer() { Id = 2, Name = "Mary Williams" }
            };

            CustomersViewModel customersViewModel = new CustomersViewModel()
            {
                Customers = customers
                /*Customers = null*/
            };

            return View(customersViewModel);
        }

        // GET: Customers/Details/id
        public ActionResult Details(int id)
        {
            List<Customer> customers = new List<Customer>()
            {
                new Customer() { Id = 1, Name = "John Smith" },
                new Customer() { Id = 2, Name = "Mary Williams" }
            };

            foreach (Customer customer in customers)
            {
                if (customer.Id == id)
                {
                    return View(customer);
                }
            }
            return Content("Customer not found!");
        }
    }
}
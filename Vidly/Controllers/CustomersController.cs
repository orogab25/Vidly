using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = _context.Customers.Include(c=>c.MembershipType).ToList();

            return View(customers);
        }

        // GET: Customers/Details/id
        public ActionResult Details(int id)
        {
            Customer customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id); //used lambda expression

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult New()
        {
            CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel()
            {
                Customer=new Customer(),
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", newCustomerViewModel);
        }

        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerFormViewModel newCustomerViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm",newCustomerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel customerFormViewModel = new CustomerFormViewModel()
                {
                    Customer=customer,
                    MembershipTypes=_context.MembershipTypes.ToList()
                };

                return View("CustomerForm",customerFormViewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                Customer customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribed = customer.IsSubscribed;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}
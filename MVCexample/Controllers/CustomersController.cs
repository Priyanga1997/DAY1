using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCexample.Models;
using System.Data.Entity;


namespace MVCexample.Controllers
{
   [Authorize] /*only authenticated users can view the pages*/
    public class CustomersController : Controller
    {
        private ApplicationDbContext DbContext = null;
        public CustomersController()
        {
            DbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
        }
        // GET: Customers
        [AllowAnonymous] /*this page alone will be displayed before login*/
        public ActionResult Index()
        {
            //use namespace System.Data.Entity to link one table with existing table
            List<Customer> customers = DbContext.Customers.Include(z => z.MembershipType).ToList(); //instead of List<Customer> we can use var
            return View("DisplayCustomer", customers);
        }
        
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer{ID=1,CustomerName="Viji",BirthDate=Convert.ToDateTime("17/07/1998"),Gender="Female",MobileNumber=9677695719},
                new Customer{ID=2,CustomerName="Hari",BirthDate=Convert.ToDateTime("25/09/2004"),Gender="Male",MobileNumber=9874560321},
                new Customer{ID=3,CustomerName="Maha",BirthDate=Convert.ToDateTime("02/11/1999"),Gender="Female",MobileNumber=7708965231}
            };

            return customers;
        }
        public ActionResult Details(int id)
        {
            //LINQ with Lambda
            var customers = DbContext.Customers.Include(z => z.MembershipType).ToList().SingleOrDefault(a => a.ID == id);
            return View(customers);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var customers = new Customer();
            ViewBag.Gender = ListGender();
            //ViewBag.membership = ListMembership();
            ViewBag.MembershipTypeId = ListMembership();
            return View(customers);
        }
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            var customers = DbContext.Customers.SingleOrDefault(c => c.ID == id);
            if(customers!= null)
             {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeId = ListMembership();
                return View(customers);


            }
            return HttpNotFound("ID does not exists ");
        }
        [HttpGet]
     public ActionResult DeleteCustomer(int id)
        {
            var customers = DbContext.Customers.SingleOrDefault(c => c.ID == id);
            if (customers != null)
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeId = ListMembership();
                return View(customers);
            }
        
            
                return HttpNotFound("ID does not exists");
            }
        [HttpPost]
        public ActionResult DeleteCustomer(Customer customerFromView)
        {
            if (ModelState.IsValid)
            {
                var customerInDB = DbContext.Customers.FirstOrDefault(c => c.ID == customerFromView.ID);
                DbContext.Customers.Remove(customerInDB);
                DbContext.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                return HttpNotFound("Deleted");
            }
        }




        [HttpPost]
        public ActionResult EditCustomer(Customer customerFromView)
        {
            if (ModelState.IsValid)
            {
                var customerInDB = DbContext.Customers.FirstOrDefault(c => c.ID == customerFromView.ID);
                customerInDB.CustomerName = customerFromView.CustomerName;
                customerInDB.City = customerFromView.City;
                customerInDB.BirthDate = customerFromView.BirthDate;
                customerInDB.Gender = customerFromView.Gender;
                customerInDB.MembershipTypeId = customerFromView.MembershipTypeId;
                DbContext.SaveChanges();
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                ViewBag.Gender = ListGender();
                ViewBag.MemberhipTypeId = ListMembership();
                return View(customerFromView);
            }
        }
     
      

        public ActionResult Create(Customer customerFromView)/*Modelstate step3*/
        {
            if(!ModelState.IsValid)
            {
                
                ViewBag.Gender = ListGender();
                //ViewBag.membership = ListMembership();
                ViewBag.MembershipTypeId = ListMembership();
                return View(customerFromView);
            }
            //return View();
            DbContext.Customers.Add(customerFromView);//insert Operation
            DbContext.SaveChanges();//Update to database
            return RedirectToAction("Index", "Customer");
        }

       // public IEnumerable<SelectListItem> gender = new List<SelectListItem> //var gender=new List<SelectListItem> can also be used
           // {
           [NonAction]
           public IEnumerable<SelectListItem> ListGender()
          { 
               var gender=new List<SelectListItem>
               {
                new SelectListItem{Text="Select Gender",Value="0",Disabled=true,Selected=true},
                new SelectListItem{Text="Male",Value="Male"},
                 new SelectListItem{Text="Female",Value="Female"},
                  new SelectListItem{Text="Others",Value="Others"},

             };
           // ViewBag.Gender = gender;
            return gender;
            }
        

            public IEnumerable<SelectListItem>ListMembership()
          {
            var membership = (from m in DbContext.MembershipTypes.AsEnumerable()
                              select new SelectListItem
                              {
                                  Text = m.Type,
                                  Value = m.Id.ToString()
                              }).ToList();
            membership.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            return membership;
           }

          }

        

     
    }

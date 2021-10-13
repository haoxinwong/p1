using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using BL;
using Microsoft.AspNetCore.Http;
using Models;
using System.Web;

namespace WebUI.Controllers
{
    public class CustomerHomeController : Controller
    {
        private IBL _bl;

        public CustomerHomeController(IBL bl)
        {
            _bl = bl;
        }

        public ActionResult Index()
        {
            var value = TempData.Peek("CustomerId");
            var cookiesvalue = Request.Cookies["CustomerId"];
            Customer customer = _bl.GetOneCustomerById(Int32.Parse(cookiesvalue));
            CustomerVM cust = new CustomerVM(customer);
            List<Order> orders = _bl.GetAllOrderbyId(Int32.Parse(cookiesvalue));
            if (Request.Cookies["HistoryOrder"] != null)
            {
                ViewBag.order = Request.Cookies["HistoryOrder"];
                switch (Request.Cookies["HistoryOrder"])
                {
                    case "TimeLastestToOldest":
                        orders = DisplayOrderbyDate90(orders);
                        break;
                    case "TimeOldestToLastest":
                        orders = DisplayOrderbyDate09(orders);
                        break;
                    case "CostLowestToHighest":
                        orders = DisplayOrderbyCost09(orders);
                        break;
                    case "CostHighestToLowest":
                        orders = DisplayOrderbyCost90(orders);
                        break;
                }
            }else
            {
                ViewBag.order = "TimeOldestToLastest";
            }
            cust.Orders = orders.Select(r => new OrderVM(r)).ToList();
            return View(cust);
        }


        private List<Order> DisplayOrderbyDate09(List<Order> customerOrders)
        {
            customerOrders.Sort((x, y) => DateTime.Compare(x.Time, y.Time));
            return customerOrders;
        }
        private List<Order> DisplayOrderbyDate90(List<Order> customerOrders)
        {
            customerOrders.Sort((x, y) => DateTime.Compare(y.Time, x.Time));
            return customerOrders;
        }
        private List<Order> DisplayOrderbyCost09(List<Order> customerOrders)
        {
            customerOrders.Sort((x, y) => x.Total.CompareTo(y.Total));
            return customerOrders;
        }
        private List<Order> DisplayOrderbyCost90(List<Order> customerOrders)
        {
            customerOrders.Sort((x, y) => y.Total.CompareTo(x.Total));
            return customerOrders;
        }


        public ActionResult HistoryOrder(string selectedBatchId)
        {
            
            if (Request.Cookies["HistoryOrder"] != null)
            {
                Response.Cookies.Delete("HistoryOrder");
            }
            Response.Cookies.Append("HistoryOrder", selectedBatchId);

            return RedirectToAction("Index", "CustomerHome");
        }


        public IActionResult Login(int i)
        {


            return View();
        }

        // GET: CustomerHomeController/Details/5
        public ActionResult Details(int id)
        {
            try
            {

                Order order = _bl.GetOneOrderbyId(id);
                OrderVM orderToShow = new OrderVM(order);
                orderToShow.LineItems = order.LineItems;
                return View(orderToShow);
            }
            catch
            {
                return RedirectToAction("Index","CustomerHome");
            }

        }

        // GET: CustomerHomeController/Create
        public ActionResult Create()
        {
            return View();
        }



        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM cust)
        {
            try
            {

                int i = _bl.Add(cust.ToModel()).Id;
                TempData["CustomerId"] = i;
                TempData.Keep("CustomerId");
                Response.Cookies.Append("CustomerId", i.ToString());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            Customer toEdit = _bl.GetOneCustomerById(id);
            return View(toEdit);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerVM cust)
        {
            try
            {

                _bl.Update(cust.ToModel());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

 
/*        public ActionResult Logout(int id)
        {
            Customer toEdit = _bl.GetOneCustomerById(id);
            return View(toEdit);
        }
*/
        // POST: CustomerController/Logout
        public ActionResult Logout()
        {
            try
            {

                /*TempData["CustomerId"] = null;
                TempData.Keep("CustomerId");*/
                if (Request.Cookies["CustomerId"] != null)
                {
                    Response.Cookies.Delete("CustomerId");

                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        // POST: CustomerController/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer cust)
        {


            List<CustomerVM> allCustomer = _bl.GetAll().Select(r => new CustomerVM(r)).ToList();
            CustomerVM currentCustomer = null;
            foreach (CustomerVM c in allCustomer)
            {
                if (c.Name.Equals(cust.Name))
                {
                    currentCustomer = c;
                    List<Order> orders = _bl.GetAllOrderbyId(currentCustomer.Id);
                    currentCustomer.Orders = orders.Select(r => new OrderVM(r)).ToList();
                }
            }

            if (cust.Name == "Admin")
            {
                TempData["CustomerId"] = "Admin";
                TempData.Keep("CustomerId");
                Response.Cookies.Append("CustomerId", "Admin");
                return RedirectToAction("Index", "AdminHome", currentCustomer);
            }
            if (currentCustomer == null)
            {
                TempData["CustomerId"] = null;
                return RedirectToAction("Create");
            }
            else
            {

                TempData["CustomerId"] = currentCustomer.Id;
                TempData.Keep("CustomerId");
                Response.Cookies.Append("CustomerId", currentCustomer.Id.ToString());

                return RedirectToAction("Index", "Home", currentCustomer);

            }
        }

    }
}


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
    public class AdminCustomerController : Controller
    {
        private IBL _bl;
        public AdminCustomerController(IBL bl)
        {
            _bl = bl;
        }

        // GET: AdminCustomerController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowAllCustomerName()
        {

            List<CustomerVM> allCustomer = _bl.GetAll().Select(r => new CustomerVM(r)).ToList();
            string result = "";
            if (allCustomer.Count == 0)
            {
                ViewBag.allName = "There is not Customer you poor guy";
            }
            else
            {
                foreach (CustomerVM c in allCustomer)
                {
                    result += c.Name + "\n";
                }
                ViewBag.allName = result.Trim();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchCustomerByName(CustomerVM customer)
        {
      
            try
            {
                if (customer.Name!="")
                {
                    List<CustomerVM> allCustomer = _bl.GetAll().Select(r => new CustomerVM(r)).ToList();
                    string name = customer.Name;
                    CustomerVM currentCustomer = new CustomerVM();
                    if (allCustomer.Count == 0)
                    {
                        if (Request.Cookies["AdminMessage"] != null)
                        {
                            Response.Cookies.Delete("AdminMessage");
                        }
                        Response.Cookies.Append("AdminMessage", "There is not Customer you poor guy");
                        return RedirectToAction("Error", "AdminCustomer");
                    }
                    else
                    {
                        foreach (CustomerVM c in allCustomer)
                        {
                            if (c.Name.Equals(name))
                            {
                                currentCustomer = c;
                                List<Order> orders = _bl.GetAllOrderbyId(currentCustomer.Id);
                                if (Request.Cookies["AdminHistoryOrder"] != null)
                                {
                                    ViewBag.Aorder = Request.Cookies["AdminHistoryOrder"];
                                    switch (Request.Cookies["AdminHistoryOrder"])
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

                                }
                                else
                                {
                                    ViewBag.Aorder = "TimeOldestToLastest";
                                }

                                if (Request.Cookies["AdminCustomerId"] != null)
                                {
                                    Response.Cookies.Delete("AdminCustomerId");
                                }
                                currentCustomer.Orders = orders.Select(r => new OrderVM(r)).ToList();
                                Response.Cookies.Append("AdminCustomerId", currentCustomer.Id.ToString());
                                
                                return View(currentCustomer);
                            }
                        }
                        if (Request.Cookies["AdminMessage"] != null)
                        {
                            Response.Cookies.Delete("AdminMessage");
                        }
                        Response.Cookies.Append("AdminMessage", "There is not such customer name within the database");
                        return RedirectToAction("Error", "AdminCustomer");
                    }

                }
                return RedirectToAction("Index", "AdminCustomer");
            }
            catch
            {
                return RedirectToAction("Index", "AdminCustomer");
            }
        }

        public ActionResult SearchCustomerByName()
        {
            var cookiesvalue = Request.Cookies["AdminCustomerId"];
            Customer customer = _bl.GetOneCustomerById(Int32.Parse(cookiesvalue));
            CustomerVM cust = new CustomerVM(customer);
            List<Order> orders = _bl.GetAllOrderbyId(Int32.Parse(cookiesvalue));
            if (Request.Cookies["AdminHistoryOrder"] != null)
            {
                ViewBag.Aorder = Request.Cookies["AdminHistoryOrder"];
                switch (Request.Cookies["AdminHistoryOrder"])
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
            }
            else
            {
                ViewBag.Aorder = "TimeOldestToLastest";
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

            if (Request.Cookies["AdminHistoryOrder"] != null)
            {
                Response.Cookies.Delete("AdminHistoryOrder");
            }
            Response.Cookies.Append("AdminHistoryOrder", selectedBatchId);
            return RedirectToAction("StoreHistory", "AdminStore");
        }

        public ActionResult Error()
        {
            if (Request.Cookies["AdminMessage"] != null)
            {
                ViewBag.AdminMessage = Request.Cookies["AdminMessage"];
            }
            return View();
        }

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
                return RedirectToAction("Index", "AdminCustomer");
            }

        }


    }
}

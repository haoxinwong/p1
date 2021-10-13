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
    public class AdminStoreController : Controller
    {
        private IBL _bl;
        private List<InventoryVM> inventory;
        public AdminStoreController(IBL bl)
        {
            _bl = bl;
        }
        // GET: AdminStoreController
        public ActionResult Index()
        {
            List<StoreVM> allStore = _bl.GetAllStore()
                                              .Select(r => new StoreVM(r)).ToList();
            return View(allStore);

        }

        // GET: AdminStoreController/Details/5
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
                return RedirectToAction("Index", "AdminStore");
            }

        }

        // GET: AdminStoreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminStoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreVM store)
        {
            try
            {
                //if the data in my form is valid
                if (ModelState.IsValid)
                {
                    _bl.Add(store.ToModel());
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: AdminStoreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new InventoryVM(_bl.GetOneInventory(id)));
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryVM inven)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    inven.StoreId = Int32.Parse(Request.Cookies["AdminStoreId"]);
                    _bl.UpdateInventory2(inven.ToModel());
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: AdminStoreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new InventoryVM(_bl.GetOneStoreById(Int32.Parse(Request.Cookies["AdminStoreId"])).Inventory[id]));
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bl.RemoveInventory(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Inventory(int id)
        {
            if (Request.Cookies["AdminStoreId"] != null&&id!=0)
            {
                Response.Cookies.Delete("AdminStoreId");
                Response.Cookies.Append("AdminStoreId", id + "");
            }
            else if (id == 0)
            {
                inventory = _bl.GetOneStoreById(Int32.Parse(Request.Cookies["AdminStoreId"])).Inventory.Select(r => new InventoryVM(r)).ToList();
                return View(inventory);
            }
            else if(Request.Cookies["AdminStoreId"] == null)
            {
                Response.Cookies.Append("AdminStoreId", id + "");
            }
            inventory = _bl.GetOneStoreById(id).Inventory.Select(r => new InventoryVM(r)).ToList();

            return View(inventory);
        }

        public ActionResult StoreHistory(int id)
        {
            
            List<Order> orders = new List<Order>();

            if (Request.Cookies["AdminHistoryStoreId"] != null && id != 0)
            {
                Response.Cookies.Delete("AdminHistoryStoreId");
                Response.Cookies.Append("AdminHistoryStoreId", id + "");
                
            }
            else if(Request.Cookies["AdminHistoryStoreId"] == null && id!=0)
            {
                Response.Cookies.Append("AdminHistoryStoreId", id + "");
            }
            
            orders = _bl.GetAllOrderbyStoreId(Int32.Parse(Request.Cookies["AdminHistoryStoreId"])).ToList();
            if (Request.Cookies["AdminStoreHistoryOrder"] != null)
            {
                ViewBag.Sorder = Request.Cookies["AdminStoreHistoryOrder"];
                switch (Request.Cookies["AdminStoreHistoryOrder"])
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
                ViewBag.Sorder = "TimeOldestToLastest";
            }

            return View(new List<OrderVM>(orders.Select(r => new OrderVM(r)).ToList()));
        }
        public ActionResult HistoryOrder(string selectedBatchId)
        {

            if (Request.Cookies["AdminStoreHistoryOrder"] != null)
            {
                Response.Cookies.Delete("AdminStoreHistoryOrder");
            }
            Response.Cookies.Append("AdminStoreHistoryOrder", selectedBatchId);
            return RedirectToAction("StoreHistory", "AdminStore");
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

        public ActionResult CreateInventory()
        {
            return View();
        }

        // POST: AdminStoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInventory(InventoryVM inventory)
        {
            try
            {
                //if the data in my form is valid
                if (ModelState.IsValid)
                {
                    inventory.StoreId = Int32.Parse(Request.Cookies["AdminStoreId"]);
                    _bl.AddInventoryItem(inventory.ToModel());
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}

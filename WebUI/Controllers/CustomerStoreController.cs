using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using WebUI.Models;
using Models;
using System.Diagnostics;

namespace WebUI.Controllers
{
    public class CustomerStoreController : Controller
    {
        private IBL _bl;
        private List<InventoryVM> inventory;
        public CustomerStoreController(IBL bl)
        {
            _bl = bl;
        }
        // GET: CustomerStoreController
        public ActionResult Index()
        {
            List<StoreVM> allStore = _bl.GetAllStore().Select(r => new StoreVM(r)).ToList();
            return View(allStore);
        }

        
        public ActionResult ShoppingCart()
        {
            string olddata = Request.Cookies["LineItem"];
            inventory = _bl.GetOneStoreById(Int32.Parse(Request.Cookies["StoreId"])).Inventory.Select(r => new InventoryVM(r)).ToList();
            List<string> tempData = new List<string>();
            List<LineItemVM> currentLineItem = new List<LineItemVM>();
            LineItemVM l = new LineItemVM();
            Dictionary<string, string> tempData2 = new Dictionary<string, string>();
            if (olddata != null && olddata!="")
            {
                tempData = olddata.Trim().Split(" ").ToList();
                foreach (string s in tempData)
                {
                    List<string> temp = s.Split("-").ToList();
                    l.Name = inventory[Int32.Parse(temp[0])].Name;
                    l.Price = inventory[Int32.Parse(temp[0])].Price;
                    l.Quantity = Int32.Parse(temp[1]);
                    currentLineItem.Add(l);
                }
            }
            else
            {
                ViewBag.message = "Nothing is your shopping cart";
            }
                return View(currentLineItem);
        }
        public ActionResult Inventory(int id)
        {
            
            if (Request.Cookies["StoreId"] != null&&id!=0)
            {
                Response.Cookies.Delete("LineItem");
                Response.Cookies.Delete("StoreId");
                Response.Cookies.Append("StoreId", id + "");
            }else if (id == 0)
            {
                
                inventory = _bl.GetOneStoreById(Int32.Parse(Request.Cookies["StoreId"])).Inventory.Select(r => new InventoryVM(r)).ToList();
                return View(inventory);
            }
            else
            {
                Response.Cookies.Append("StoreId", id + "");
            }
            inventory = _bl.GetOneStoreById(id).Inventory.Select(r=>new InventoryVM(r)).ToList();

            return View(inventory);
        }

        [HttpPost]
        public ActionResult ShoppingCart(int id)
        {

/*            Debug.WriteLine(Request.Cookies["StoreId"]);
            Debug.WriteLine(Request.Form["ItemId"]);
            Debug.WriteLine(Request.Form["ItemQuantity"]);*/
            List<LineItemVM> currentLineItem = new List<LineItemVM>();
            inventory = _bl.GetOneStoreById(Int32.Parse(Request.Cookies["StoreId"])).Inventory.Select(r => new InventoryVM(r)).ToList();
            string olddata = "";
            string dataAsString = "";
            LineItemVM l = new LineItemVM();
            List<string> tempData = new List<string>();
            bool isCurrentItem = false;
            bool isOk = true;
            Dictionary<string, string> tempData2 = new Dictionary<string, string>();
            try
            {
                if (Request.Form["ItemId"] != "" && Request.Form["ItemQuantity"] != "")
                {
                    if (inventory != null)
                    {
                        if (Int32.Parse(Request.Form["ItemId"]) >= 0 && Int32.Parse(Request.Form["ItemId"]) < inventory.Count)
                        {
                            if (Int32.Parse(Request.Form["ItemQuantity"]) >= 0 && Int32.Parse(Request.Form["ItemQuantity"]) < inventory[(Int32.Parse(Request.Form["ItemId"]))].Quantity)
                            {
                                
                                if (Request.Cookies["LineItem"] != null)
                                {
                                    olddata = Request.Cookies["LineItem"];
                                    tempData = olddata.Trim().Split(" ").ToList();
                                    foreach (string s in tempData)
                                    {
                                        List<string> temp = s.Split("-").ToList();
                                        if (temp[0].Equals(Request.Form["ItemId"]))
                                        {
                                            if (Int32.Parse(temp[1]) + Int32.Parse(Request.Form["ItemQuantity"]) <= inventory[(Int32.Parse(Request.Form["ItemId"]))].Quantity)
                                            {
                                                temp[1] = (Int32.Parse(temp[1]) + Int32.Parse(Request.Form["ItemQuantity"])).ToString();
                                                isCurrentItem = true;
                                                tempData2.Add(temp[0], temp[1]);
                                            }
                                            else
                                            {
                                                ViewBag.message = "Value out of range";
                                                isOk = false;
                                            }
                                        }
                                        else
                                        {
                                            tempData2.Add(temp[0], temp[1]);
                                        }

                                    }
                                    if (isCurrentItem == false && isOk)
                                    {
                                        tempData2.Add(Request.Form["ItemId"], Request.Form["ItemQuantity"]);
                                        Response.Cookies.Delete("LineItem");
                                        
                                    }
                                    foreach (KeyValuePair<string, string> kv in tempData2)
                                    {
                                        l = new LineItemVM();
                                        
                                        dataAsString = dataAsString + (kv.Key + "-" + kv.Value + " ");
                                        l.Id = inventory[Int32.Parse(kv.Key)].Id;
                                        l.Name = inventory[Int32.Parse(kv.Key)].Name;
                                        l.Price = inventory[Int32.Parse(kv.Key)].Price;
                                        l.Quantity = Int32.Parse(kv.Value);
                                        currentLineItem.Add(l);
                                    }

                                    Response.Cookies.Append("LineItem", dataAsString);
                                    ViewBag.message = "Adding Successful";
                                    return View(currentLineItem);
                                }
                                else
                                {
                                    
                                    tempData2.Add(Request.Form["ItemId"], Request.Form["ItemQuantity"]);
                                    Response.Cookies.Append("LineItem", dataAsString);
                                    foreach (KeyValuePair<string, string> kv in tempData2)
                                    {
                                        dataAsString = dataAsString + (kv.Key + "-" + kv.Value + " ");
                                        l.Id = inventory[Int32.Parse(kv.Key)].Id;
                                        l.Name = inventory[Int32.Parse(kv.Key)].Name;
                                        l.Price = inventory[Int32.Parse(kv.Key)].Price;
                                        l.Quantity = Int32.Parse(kv.Value);
                                        currentLineItem.Add(l);
                                    }

                                    Response.Cookies.Append("LineItem", dataAsString);
                                    ViewBag.message = "Adding Successful";
                                    return View(currentLineItem);
                                }

                                

                            }
                            else
                            {
                                ViewBag.message = "Quantity out of range";
                            }
                        }
                        else
                        {
                            ViewBag.message = "Id out of range";
                        }
                    }
                    else
                    {
                        ViewBag.message = "There is not inventory for this store";
                    }
                    
                }
                else
                {
                    ViewBag.message = "Value can't be null";
                }
            }
            catch
            {
                ViewBag.message = "Value need to be number";
            }
            return View(currentLineItem);

        }


        public ActionResult PlaceOrder()
        {
            List<LineItem> currentLineItem = new List<LineItem>();
            inventory = _bl.GetOneStoreById(Int32.Parse(Request.Cookies["StoreId"])).Inventory.Select(r => new InventoryVM(r)).ToList();
            string dataString = "";
            LineItemVM l = new LineItemVM();
            
            List<string> tempData = new List<string>();
            List<string> temp = new List<string>();
            Dictionary<string, string> tempData2 = new Dictionary<string, string>();
            OrderVM newOrder = new OrderVM();
            if (Request.Cookies["LineItem"] != null)
            {
                dataString = Request.Cookies["LineItem"];
                tempData = dataString.Trim().Split(" ").ToList();
                foreach (string s in tempData)
                {
                    temp = s.Split("-").ToList();
                    l.Id = inventory[Int32.Parse(temp[0])].Id;
                    l.Name = inventory[Int32.Parse(temp[0])].Name;
                    l.Price = inventory[Int32.Parse(temp[0])].Price;
                    l.Quantity = Int32.Parse(temp[1]);
                    
                    currentLineItem.Add(l.ToModel());
                }
                Order tempOrder = new Order(currentLineItem, _bl.GetOneStoreById(Int32.Parse(Request.Cookies["StoreId"])).Name,
                    Int32.Parse(Request.Cookies["StoreId"]),
                    Int32.Parse(Request.Cookies["CustomerId"]));
                newOrder = new OrderVM(tempOrder);
                Debug.WriteLine(newOrder.Time);
                Debug.WriteLine(newOrder.Total);
                int orderid = _bl.AddAOrder(newOrder.ToModel()).Id;
                foreach(LineItem item in currentLineItem)
                {
                    item.OrderId = orderid;
                    _bl.AddALineItem(item);
                }
                List<Inventory> currentInventory = _bl.GetOneStoreById(Int32.Parse(Request.Cookies["StoreId"])).Inventory;
               
                for (int i = 0; i < currentLineItem.Count; i++)
                {
                    int index = currentInventory.FindIndex(item => item.Id == currentLineItem[i].Id);
                    Debug.WriteLine(index);
                    currentInventory[index].Quantity = currentInventory[index].Quantity - currentLineItem[i].Quantity;
                    Inventory inven = currentInventory[index];
                    /*Debug.WriteLine(inven.Name);
                    Debug.WriteLine(inven.Id);
                    Debug.WriteLine(inven.Price);
                    Debug.WriteLine(inven.Quantity);
                    Debug.WriteLine(inven.StoreId);*/
                    
                }
                _bl.InventorToUpdate(currentInventory);

                ViewBag.message2 = "Thank you for ordering";
                Response.Cookies.Delete("LineItem");

            }
            else
            {
                ViewBag.message2 = "You have nothing in your shopping cart";

            }
                return View();
        }
    }
}

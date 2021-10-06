using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;
using System.Collections.Generic;
using Serilog;
namespace P0_M.UI
{
    public class StoreMenu:IMenu
    {
        private StoreBL _sbl;
        private CustomerBL _cbl;
        private Store _currentStore;
        private Order _currentOrder;
        private List<LineItem> _currentLineItem;
        private Customer _currentCustomer;
        private List<int> _itemPos;

        public StoreMenu(StoreBL sbl, CustomerBL cbl, Store currentStore, Customer currentCustomer){
            _sbl = sbl;
            _cbl = cbl;
            _currentStore = currentStore;
            _currentCustomer = currentCustomer;
            _currentOrder = new Order();
            _currentLineItem = new List<LineItem>();
            _itemPos = new List<int>();
        }

        // public StoreMenu(StoreBL sbl,Store currentStore, Order currentOrder):this(sbl, currentStore){
        //     _currentOrder = currentOrder;
        // }


        public void Start(){
            bool exit  = false;
            string input = "";

            do
            {
                Console.WriteLine($"This is {_currentStore.Name} Menu");
                Console.WriteLine("[0] Browse all items");
                Console.WriteLine("[1] View current order");
                Console.WriteLine("[2] Place order");
                Console.WriteLine("[<] Go Back");
                Console.WriteLine("[x] Exit");
                
                input = Console.ReadLine().ToLower();
                
                switch (input)
                {
                    case "0":
                        showAllItems();
                        break;
                    case "1":
                        showCurrentOrder();
                        break;
                    case "2":
                        placeOrder();
                        break;
                    case "<":
                        exit = true;
                        break;
                    case "x":
                        Console.WriteLine("bye");
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("?");
                        break;
                }
                
            } while (!exit);

        }
        private void showAllItems(){
            bool exitInventory = false;
            string customerInput = "";
            int itemResult = -1;
            int quantityResult = -1;
            do
            {
                Console.WriteLine("Store Inventory");
                List<Inventory> currentInventory = _sbl.GetOneStoreById(_currentStore.Id).Inventory;
                
                if (currentInventory.Count == 0){
                    Console.WriteLine("No Items");
                    break;
                }
                for (var i = 0; i < currentInventory.Count; i++)
                {
                    Inventory tempLineItem = currentInventory[i];

                    Console.WriteLine($"[{i}] Name: {tempLineItem.Name}");
                    Console.WriteLine($"Price: {tempLineItem.Price}");
                    Console.WriteLine($"Quantity: {tempLineItem.Quantity}");
                }
                
                enterItemNumber:
                {
                    Console.WriteLine("Select an item to add to your cart");
                    Console.WriteLine("Enter [n] if you don't want to add anything");
                    customerInput = Console.ReadLine().ToLower();
                }
                if (customerInput.Equals("n"))
                {
                    exitInventory = true;
                }else
                {
                    try
                    {
                        itemResult = Int32.Parse(customerInput);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Unable to parse '{customerInput}', reenter");
                        goto enterItemNumber;
                    }

                    if(itemResult >= 0 & itemResult < currentInventory.Count){
                        enterQuantityNumber:
                        Console.WriteLine("How many do you want?");
                        Console.WriteLine("Enter number: ");
                        customerInput = Console.ReadLine().ToLower();
                        try
                        {
                            quantityResult = Int32.Parse(customerInput);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{customerInput}', reenter");
                            goto enterQuantityNumber;
                        }
                        if (quantityResult>0 && quantityResult <= currentInventory[itemResult].Quantity)
                        {
                            if (_currentLineItem.Exists(x => x.Name.Equals(currentInventory[itemResult].Name)))
                            {
                                foreach(LineItem obj in _currentLineItem)
                                {
                                if(obj.Name.Equals(currentInventory[itemResult].Name))
                                {
                                    if(obj.Quantity + quantityResult > currentInventory[itemResult].Quantity){
                                        Console.WriteLine("Sorry, The quantity number is larger than current inventory quantity, reenter");
                                        goto enterItemNumber;
                                    }else{
                                        obj.Quantity =  obj.Quantity + quantityResult;
                                    }
                                    break;
                                }
                                }
                                
                            }else{
                                _currentLineItem.Add(new LineItem(currentInventory[itemResult].Name,currentInventory[itemResult].Price,quantityResult));
                                _itemPos.Add(itemResult);
                                
                            }
                            addAgain:
                            Console.WriteLine("Do you want to add more item?(y/n)");
                            
                            switch (Console.ReadLine().ToLower())
                            {
                                case "y":
                                    goto enterItemNumber;
                                case "n":
                                    Console.WriteLine("Thanks for adding thing to shopping cart");
                                    exitInventory = true;
                                    break;
                                default:
                                    Console.WriteLine("Input Error, reenter");
                                    goto addAgain;
                            }
                            
                            
                        }else{
                            Console.WriteLine("Input Error, reenter");
                            goto enterQuantityNumber;
                        }
                    }else
                    {
                        Console.WriteLine("Input Error, reenter");
                        goto enterItemNumber;
                    }
                }
            } while (!exitInventory);
        }

        private void showCurrentOrder(){
            if (_currentLineItem.Count!=0)
            {
                Console.WriteLine("Current Order");
                int i = 0;

                foreach (LineItem tempItem in _currentLineItem)
                {
                    i++;
                    Console.WriteLine($"[{i}] Product Name: {tempItem.Name}");
                    Console.WriteLine($"Product Price: {tempItem.Price}");
                    Console.WriteLine($"Product Quantity: {tempItem.Quantity}");
                }
            }else{
                Console.WriteLine("Current Shipping Cart is Empty");
            }
        }

        private void placeOrder(){
            if (_currentLineItem.Count!=0)
            {
                Console.WriteLine("You have place an order");
                // Console.WriteLine(_currentCustomer.Id);
                Order tempOrder = new Order(_currentLineItem,_currentStore.Name,_currentStore.Id,_currentCustomer.Id);
                // Console.WriteLine(tempOrder.CustomerId+" "+tempOrder.LocationId);
                int orderid = _cbl.AddAOrder(tempOrder).Id;
                // Console.WriteLine("I here"+orderid);
                foreach (LineItem item in _currentLineItem)
                {
                    // Console.WriteLine($"II here{item.Name}");
                    item.OrderId = orderid;
                    _cbl.AddALineItem(item);
                }
                List<Inventory> currentInventory = _sbl.GetOneStoreById(_currentStore.Id).Inventory;
                for (int i = 0; i < _currentLineItem.Count; i++)
                {
                    currentInventory[_itemPos[i]].Quantity = currentInventory[_itemPos[i]].Quantity - _currentLineItem[i].Quantity;
                    _sbl.UpdateInventory(currentInventory[_itemPos[i]]," ");
                }
                
                Console.WriteLine("Thank you for place order");
                Console.WriteLine($"Total is {tempOrder.Total}");
                refresh();
            }else{
                Console.WriteLine("Current Shipping Cart is Empty");
            }
        }

        private void refresh(){
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("../logs/logs.txt",rollingInterval:RollingInterval.Day)
            .CreateLogger();
            Log.Information("DB Refresh..");
            _cbl.GetAll();
            _sbl.GetAll();
            _currentLineItem = new List<LineItem>();
            _itemPos = new List<int>();
        }

    }
}
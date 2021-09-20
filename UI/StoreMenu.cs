using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;
using System.Collections.Generic;

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
                List<LineItem> currentInventory = _currentStore.Inventory;
                
                for (var i = 0; i < currentInventory.Count; i++)
                {
                    LineItem tempLineItem = currentInventory[i];

                    Console.WriteLine($"[{i+1}] Name: {tempLineItem.Item.Name}");
                    Console.WriteLine($"Price: {tempLineItem.Item.Price}");
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

                    if(itemResult >= 0 & itemResult <= currentInventory.Count){
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
                            _currentLineItem.Add(new LineItem(currentInventory[itemResult].Item,quantityResult));
                            _itemPos.Add(itemResult);
                            addAgain:
                            Console.WriteLine("Do you want to add more item?(y/n)");
                            
                            if (Console.ReadLine().ToLower().Equals("y"))
                            {
                                goto enterItemNumber;
                            }else if(Console.ReadLine().ToLower().Equals("n")){
                                exitInventory = true;
                                break;
                            }else{
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
                    Console.WriteLine($"[{i}] Product Name: {tempItem.Item.Name}");
                    Console.WriteLine($"Product Price: {tempItem.Item.Price}");
                    Console.WriteLine($"Product Quantity: {tempItem.Quantity}");
                }
            }
        }

        private void placeOrder(){
            if (_currentLineItem.Count!=0)
            {
                Console.WriteLine("You have place an order");
                Order tempOrder = new Order(_currentLineItem,_currentStore.Name);
                _currentCustomer.Orders.Add(tempOrder);
                _currentStore.AdjustInventory(_currentLineItem,_itemPos);
                _cbl.Update(_currentCustomer);
                _sbl.Update(_currentStore);
                Console.WriteLine("Thank you for place order");
                Console.WriteLine($"Total is{tempOrder.Total}");
                refresh();
            }else{
                Console.WriteLine("Current Shipping Cart is Empty");
            }
        }

        private void refresh(){
            _cbl.GetAll();
            _sbl.GetAll();
            _currentLineItem = new List<LineItem>();
            _itemPos = new List<int>();
        }

    }
}
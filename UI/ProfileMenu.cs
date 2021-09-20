using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;
using System.Text;
using System.Collections.Generic;


namespace P0_M.UI
{
    public class ProfileMenu:IMenu
    {
        private Customer _currentCustomer;

        public ProfileMenu(Customer customer){
            _currentCustomer = customer;
        }

        public void Start(){
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("This is Profile Menu");
                Console.WriteLine($"Customer name: {_currentCustomer.Name}");
                Console.WriteLine($"Customer Address: {_currentCustomer.Address}");
                Console.WriteLine($"Customer Phone Number: {_currentCustomer.PhoneNumber}");
                Console.WriteLine("[0] View History Order");
                Console.WriteLine("[<] Go Back to Customer Home Menu");
                Console.WriteLine("[x] Exit");

                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "0":
                        ViewHistoryOrder();
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
        private void ViewHistoryOrder(){
            bool ViewHistory = true;
            do
            {
                Console.WriteLine("[0] Display Order by Date (latest to oldest)");
                Console.WriteLine("[1] Display Order by Date (oldest to latest)");
                Console.WriteLine("[2] Display Order by Cost (least expensive to most expensive)");
                Console.WriteLine("[2] Display Order by Cost (most expensive to least expensive)");
                Console.WriteLine("[<] Stop Display Order History");
                string historyInput = Console.ReadLine();
                List<Order> TempOrders = new List<Order>();
                switch (historyInput)
                {
                    case "0":
                        Console.WriteLine(DisplayOrders(DisplayOrderbyDate09(_currentCustomer.Orders)));
                        ViewHistory = true;
                        break;
                    case "1":
                        Console.WriteLine(DisplayOrders(DisplayOrderbyDate90(_currentCustomer.Orders)));
                        ViewHistory = true;
                        break;
                    case "2":
                        Console.WriteLine(DisplayOrders(DisplayOrderbyCost09(_currentCustomer.Orders)));
                        ViewHistory = true;
                        break;
                    case "3":
                        Console.WriteLine(DisplayOrders(DisplayOrderbyCost90(_currentCustomer.Orders)));
                        ViewHistory = true;
                        break;
                    case "<":
                        Console.WriteLine("Thank you.");
                        ViewHistory = true;
                        break;
                    default:
                        Console.WriteLine("?");
                        ViewHistory = false;
                        break;
                }
            } while (!ViewHistory);

            
        }

        private List<Order> DisplayOrderbyDate09(List<Order>customerOrders){
            customerOrders.Sort((x, y) => DateTime.Compare(x.Time, y.Time));
            return customerOrders;
        }
        private List<Order> DisplayOrderbyDate90(List<Order>customerOrders){
            customerOrders.Sort((x, y) => DateTime.Compare(y.Time, x.Time));
            return customerOrders;
        }
        private List<Order> DisplayOrderbyCost09(List<Order>customerOrders){
            customerOrders.Sort((x,y) => x.Total.CompareTo(y.Total));
            return customerOrders;
        }
        private List<Order> DisplayOrderbyCost90(List<Order>customerOrders){
            customerOrders.Sort((x,y) => y.Total.CompareTo(x.Total));
            return customerOrders;
        }
        public StringBuilder DisplayOrders(List<Order>customerOrders){
            StringBuilder sb = new StringBuilder();
            sb.Append("Order Histroy\n");
            if (customerOrders.Count!=0){
                for (var i = 0; i < customerOrders.Count; i++)
                {
                    sb.Append($"Order Number: {i+1}\n");
                    sb.Append($"Order Time: {customerOrders[i].Time}\n");
                    sb.Append($"Order Location: {customerOrders[i].Location}\n");
                    sb.Append($"Order Items: \n");
                    for(var j = 0; j<customerOrders[i].LineItems.Count;j++){
                        sb.Append($"Item Number: {j+1}\n");
                        sb.Append($"Item Name: {customerOrders[i].LineItems[j].Item.Name}\n");
                        sb.Append($"Item Price: {customerOrders[i].LineItems[j].Item.Price}\n");
                        sb.Append($"Item Quantity: {customerOrders[i].LineItems[j].Quantity}\n");
                    }
                    sb.Append("\n");
                }
            }else{
                sb.Append("You do not have any order");
            }
            return sb;
        }
    }
}
using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;
using System.Collections.Generic;
using Serilog;
namespace P0_M.UI
{
    public class AdminLogin:IMenu
    {
        private string _name = "h";
        private string _num = "29";
        private bool _isAdmin = false;
        private StoreBL _sbl;
        private CustomerBL _cbl;

        public AdminLogin(StoreBL sbl, CustomerBL cbl,string name, string num){
            _sbl = sbl;
            _cbl = cbl;
            _isAdmin = checkAdmin(name,num);
        }

        public void Start(){
            if (_isAdmin){
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("../logs/logs.txt",rollingInterval:RollingInterval.Day)
                .CreateLogger();
                bool exit = false;
                string input = "";
                Log.Information("Admin Here");
                do
                {
                    Console.WriteLine($"Hello, Admin {_name}. What do you want to do");
                    Console.WriteLine("[0] View all store");
                    Console.WriteLine("[1] Search customer by name");
                    Console.WriteLine("[2] Display all the customers by name");
                    Console.WriteLine("[3] Add a store");
                    Console.WriteLine("[<] Go Back");
                    Console.WriteLine("[x] Exit");
                    input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "0":
                            GoToAdminStoresMenu();
                            break;
                        case "1":
                            SearchCustomerByName();
                            break;
                        case "2":
                            DisplayAllCustomerByName();
                            break;
                        case "3":
                            AddAStore();
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
            }else{
                Console.WriteLine("YOU ARE NOT THE CHOSEN ONE");
            }
        }

        private void AddAStore(){
            List<Inventory> inventory = new List<Inventory>();

            Console.WriteLine("Let's add a store");
            enterStoreName:
            Console.WriteLine("Enter store name:");
            string name = Console.ReadLine();
            if (CheckName(name).Equals("-1")){
                Console.WriteLine("Unacceptable Name, Reenter");
                goto enterStoreName;
            }

            enterStoreAddress:
            Console.WriteLine("Enter store address:");
            string address = Console.ReadLine();
            if (CheckAddress(address).Equals("-1")){
                Console.WriteLine("Unacceptable Address, Reenter");
                goto enterStoreAddress;
            }
            List<Inventory> tempI = new List<Inventory>();
            _sbl.Add(new Store(name, address, tempI ));
            Console.WriteLine($"Store {name} added");
        }
        private string CheckName(string str){
            string pattern = @"^[a-zA-z ]*$";
            string result = str;
            if (!(System.Text.RegularExpressions.Regex.IsMatch(str, pattern))){
                result = "-1" ;
            }
            return result;
        }
        private string CheckAddress(string str){
            string pattern = @"^[a-zA-z0-9 ]*$";
            string result = str;
            if (!(System.Text.RegularExpressions.Regex.IsMatch(str, pattern))){
                result = "-1" ;
            }
            return result;
        }

        private void GoToAdminStoresMenu(){
            List<Store> allStores = _sbl.GetAll();
            bool exit = false;
            if(allStores.Count!=0){
                for ( int i = 0; i < allStores.Count; i++)
                {
                    Store TempStore = allStores[i];
                    Console.WriteLine($"[{i}] Store Name:{TempStore.Name}, Store Address:{TempStore.Address}");
                }
            }else{
                Console.WriteLine("No available store");
            }
            Console.WriteLine("[<] Go Back");
            do
            {
                Console.WriteLine("Enter your number or <");
                string input = Console.ReadLine().ToLower();
                int result = -1;
                if (input.Equals("<")){
                    exit = true;
                }else{
                    try
                        {
                            result = Int32.Parse(input);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Unable to parse '{input}', reenter");
                        }
                        if (result >= 0 & result < allStores.Count){
                            List<Store> TempStores = _sbl.GetAll();
                            // Console.WriteLine(TempStores[result].Name);
                            MenuFactory.GetMenu("adminstore",TempStores[result]).Start();
                        }else{
                            Console.WriteLine("Input Error, reenter");
                        }
                }
            } while (!exit);
            
        
        }

        private bool checkAdmin(string name, string num){
            bool result = false;
            if(_name.Equals(name) & _num.Equals(num)){
                result = true;
            }
            return result;
        }

        private void SearchCustomerByName(){
            
            Console.WriteLine("Enter Customer Name");
            int result = searchName(Console.ReadLine());
            if (result!=-1){
                List<Customer> Customers = _cbl.GetAll();
                Console.WriteLine($"Customer Name :{Customers[result].Name}");
                Console.WriteLine($"Customer Address: {Customers[result].Address}");
                Console.WriteLine($"Customer Phonenumber: {Customers[result].PhoneNumber}");
            }else{
                Console.WriteLine("Customer doesnt exist");
            }
            
            
        }

        private void DisplayAllCustomerByName(){
            List<Customer> Customers = _cbl.GetAll();
            Console.WriteLine("All Customer Name");
            // int i = 1;
            for (int i = 0; i < Customers.Count; i++)
            {
                Console.WriteLine($"[{i}] Name :{Customers[i].Name}");
            }
        }

        private int searchName(string name){
            List<Customer>Customers = _cbl.GetAll();
            for (int i = 0; i < Customers.Count; i++)
            {
                if(Customers[i].Name.Equals(name)){
                    return i;
                }
            }
            return -1;
        }
    }
}
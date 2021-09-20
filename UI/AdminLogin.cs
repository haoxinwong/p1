using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;
using System.Collections.Generic;
namespace P0_M.UI
{
    public class AdminLogin:IMenu
    {
        private string _name = "h";
        private int _num = 29;
        private bool _isAdmin = false;
        private StoreBL _sbl = new StoreBL(new StoreDBRepo());
        private CustomerBL _cbl = new CustomerBL(new CustomerDBRepo());

        public AdminLogin(string name, int num){
            _isAdmin = checkAdmin(name,num);
        }

        public void Start(){
            if (_isAdmin){
                bool exit = false;
                string input = "";
                do
                {
                    Console.WriteLine($"Hello, Admin {_name}. What do you want to do");
                    Console.WriteLine("[0] View all store");
                    Console.WriteLine("[1] Search customer by name");
                    Console.WriteLine("[2] Display all the customers by name");
                    Console.WriteLine("[<] Go Back");
                    Console.WriteLine("[x] Exit");
                    input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "0":
                            GoToAdminStoresMenu();
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
                Console.WriteLine("YOU ARE NOT THE ONE");
            }
        }

        private void GoToAdminStoresMenu(){
            // List<Store> allStores = _sbl.GetAll();
            // if(allStores.Count!=0){
            //     for ( int i = 0; i < allStores.Count; i++)
            //     {
            //         Store TempStore = allStores[i];
            //         Console.WriteLine($"[{i}] Store Name:{TempStore.Name}, Store Address:{TempStore.Address}");
            //     }
            // }else{
            //     Console.WriteLine("No available store");
            // }
            new AdminStoreMenu().Start();
        
        }

        private bool checkAdmin(string name, int num){
            bool result = false;
            if(_name.Equals(name) & _num==num){
                result = true;
            }
            return result;
        }
    }
}
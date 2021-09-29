using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;

namespace P0_M.UI
{
    public class CustomerMenu:IMenu
    {
        private Customer _currentCustomer;
        public CustomerMenu(Customer customer){
            _currentCustomer = customer;
        }
        
        public void Start(){
            bool exit = false;
            do
            {
                Console.WriteLine($"Hello, {_currentCustomer.Name}, This is Browse Menu");
                Console.WriteLine("[0] View Profile and Order History");
                Console.WriteLine("[1] View available Stores");
                Console.WriteLine("[<] Go Back To Customer Login");
                Console.WriteLine("[x] Exit");

                switch (Console.ReadLine().ToLower())
                {
                    case "0":
                        GoToProfileMenu();
                        break;
                    case "1":
                        GoToStoresMenu();
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

        private void GoToProfileMenu(){
            MenuFactory.GetMenu("profile",_currentCustomer).Start();

        }

        private void GoToStoresMenu(){
            MenuFactory.GetMenu("stores",_currentCustomer).Start();
            
        }
    }
}
using System;
using P0_M.Models;

namespace P0_M.UI
{
    public class BrowseMenu:IMenu
    {
        private Customer _currentCustomer;
        public BrowseMenu(Customer customer){
            _currentCustomer = customer;
        }
        
        public void Start(){
            bool exit = false;
            do
            {
                Console.WriteLine("This is Browse Menu");
                switch (Console.ReadLine())
                {
                    case "x":
                        exit = true;
                        break;
                    default:
                        break;
                }
            } while (!exit);
        }
    }
}
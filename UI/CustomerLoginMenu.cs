using System;
using P0_M.Models;
using P0_M.BL;
using P0_M.DL;
using System.Collections.Generic;

namespace P0_M.UI
{
    public class CustomerLoginMenu:IMenu
    {
        private CustomerBL _cbl;
        public CustomerLoginMenu(CustomerBL cbl){
            _cbl = cbl;
        }
        public void Start(){
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("This is customer login menu.");
                Console.WriteLine("[0] Enter your name: ");
                Console.WriteLine("[x] Go Back To Main Menu");
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "0":
                        GoBrowseMenu();
                        break;
                    case "x":
                        exit = true;
                        break;
                    default:
                        break;
                }
            } while (!exit);
            
        }

        public void GoBrowseMenu(){
            Console.WriteLine("Enter your name");
            inputName:
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Customer customer = new Customer(); 

            if (name.Length!=0){
                try{
                    customer.Name = name;
                    bool newCustomer = true;
                    List<Customer> currentCustomerList = _cbl.GetAll();
                    foreach (Customer tempCustomer in currentCustomerList)
                    {
                        if (tempCustomer.Name.Equals(name)){
                            customer = tempCustomer;
                            newCustomer = false;
                        }
                    }
                    if (newCustomer){
                        customer = new Customer( name , new List<Order>());
                        _cbl.Add(customer);
                    }
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                    goto inputName;
                }
                new BrowseMenu(customer).Start();

            }


        }
    }
}
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
                Console.WriteLine("[<] Go Back To Main Menu");
                Console.WriteLine("[x] Exit");
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "0":
                        GoCustomerMenu();
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

        public void GoCustomerMenu(){
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
                            Console.WriteLine("Thank you for coming back.");
                        }
                    }
                    if (newCustomer){
                        Console.WriteLine("Thank you for join us.");
                        Console.WriteLine("You are a new customer.");
                        Console.WriteLine("Please enter following information");
                        enterAddress:
                        Console.WriteLine("Address: ");
                        string address = Console.ReadLine();
                        if (CheckAddress(address).Equals("-1")){
                            Console.WriteLine("Unacceptable Address, Reenter");
                            goto enterAddress;
                        }

                        enterPhonenumber:
                        Console.WriteLine("Phone Number: ");
                        string phonenumber = Console.ReadLine();
                        if (CheckPhoneNumber(phonenumber).Equals("-1")){
                            Console.WriteLine("Unacceptable Phonenumber, Reenter");
                            goto enterPhonenumber;
                        }
                        customer = new Customer( name, address, phonenumber, new List<Order>());
                        _cbl.Add(customer);
                    }
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                    goto inputName;
                }
                new CustomerMenu(customer).Start();
            }
        }

        private string CheckAddress(string str){
                string pattern = @"^[a-zA-z0-9 ]*$";
                string result = str;
                if (!(System.Text.RegularExpressions.Regex.IsMatch(str, pattern))){
                    result = "-1" ;
                }
                return result;
        }


        private string CheckPhoneNumber(string str){
                string pattern = @"^[0-9]*$";
                string result = str;

                if (!((str.Length==9)&&(System.Text.RegularExpressions.Regex.IsMatch(str, pattern)))){
                    result = "-1" ;
                }
                return result;
        }

    
    }
}
using System;
using System.Collections.Generic;
namespace P0_M.Models
{
    public class Customer
    {
        public string Name{get;set;}
        public string Address{get;set;}
        public string PhoneNumber{get;set;}

        public List<Order> Orders{get;set;}
        
        public Customer(){}

        public Customer(string name){
            this.Name = name;
        }

        public Customer(string name, string address, string phonenumber):this(name){
            this.Address = address;
            this.PhoneNumber = phonenumber;
        }

        public Customer(string name, string address, string phonenumber, List<Order>orders):this(name,address,phonenumber){
            this.Orders = orders;
        }


    }
}
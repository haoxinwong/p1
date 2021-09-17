using System;
using System.Collections.Generic;
namespace P0_M.Models
{
    public class Customer
    {
        public string Name{get;set;}

        public List<Order> Orders{get;set;}
        
        public Customer(){}

        public Customer(string name){
            this.Name = name;
        }

        public Customer(string name, List<Order>orders):this(name){
            this.Orders = orders;
        }


    }
}
using System;
using System.Collections.Generic;
namespace Models
{
    public class Customer
    {
        public int Id{get;set;}
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

        public string CheckPhoneNumber(string str)
        {
            string pattern = @"^[0-9]*$";
            string result = str;

            if (!((str.Length == 10) && (System.Text.RegularExpressions.Regex.IsMatch(str, pattern))))
            {
                result = "-1";
            }
            return result;
        }


    }
}
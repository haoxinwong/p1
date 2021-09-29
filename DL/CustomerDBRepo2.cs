using System.Collections.Generic;
using P0_M.Models;
using System.IO;
using System.Text.Json;
using System;
using System.Linq;

namespace P0_M.DL
{
    public class CustomerDBRepo2
    {
        private const string customerFilePath = "../DL/Customer.json";
        
        
        private string jsonString;
        public List<Customer> GetAll(){
            jsonString = File.ReadAllText(customerFilePath);

            return JsonSerializer.Deserialize<List<Customer>>(jsonString);
        }

        public Customer Add(Customer customer){
            List<Customer> allCustomers = GetAll();
            allCustomers.Add(customer);
            jsonString = JsonSerializer.Serialize(allCustomers);
            File.WriteAllText(customerFilePath,jsonString);
            return customer;
        }


        public Customer Update(Customer customerToUpdate){
            List<Customer> allCustomers = GetAll();
            int customerIndex = allCustomers.FindIndex(r => r.Equals(customerToUpdate));
            allCustomers[customerIndex] = customerToUpdate;
            jsonString = JsonSerializer.Serialize(allCustomers);
            File.WriteAllText(customerFilePath, jsonString);
            return customerToUpdate;
        }
    }
}
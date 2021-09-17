using System.Collections.Generic;
using P0_M.Models;
using System;
using P0_M.DL;

namespace P0_M.BL
{
    public class CustomerBL :IBL
    {
        private CustomerDBRepo _repo;

        public CustomerBL(CustomerDBRepo repo){
            _repo = repo;
        }

        public List<Customer> GetAll(){
            return _repo.GetAll();
        }
        public Customer Add(Customer customer){
            return _repo.Add(customer);

        }
        public Customer Update(Customer customer){
            return _repo.Update(customer);
        }
    }
}
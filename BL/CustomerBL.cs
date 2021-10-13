using System.Collections.Generic;
using Models;
using System;
using DL;

namespace BL
{
    public class CustomerBL :IBL
    {
        private IRepo _repo;

        public CustomerBL(IRepo repo){
            _repo = repo;
        }

        public Inventory GetOneInventory(int id)
        {
            return _repo.GetOneInventory(id);
        }
        public void RemoveInventory(int id)
        {
             _repo.RemoveInventory(id);
        }
        public Customer GetOneCustomerById(int custID)
        {
            return _repo.GetOneCustomerById(custID);
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

        public Order AddAOrder(Order order){
            return _repo.AddAOrder(order);
        }

        public LineItem AddALineItem(LineItem li){
            return _repo.AddALineItem(li);
        }

        public List<Order> GetAllOrderbyId(int i){
            return _repo.GetAllOrderbyId(i);
        }

        public List<Store> GetAllStore()
        {
            return _repo.GetAllStore();
        }
        public Store Add(Store store)
        {
            return _repo.Add(store);

        }
        public Store Update(Store store)
        {
            return _repo.Update(store);
        }

        public Store GetOneStoreById(int id)
        {
            return _repo.GetOneStoreById(id);
        }

        public Inventory UpdateInventory(Inventory i)
        {
            return _repo.UpdateInventory(i);
        }

        public void InventorToUpdate(List<Inventory> items)
        {
            _repo.InventorToUpdate(items);
        }
        public Order GetOneOrderbyId(int id)
        {
            return _repo.GetOneOrderbyId(id);
        }

        public Inventory AddInventoryItem(Inventory i)
        {
            return _repo.AddInventoryItem(i);
        }

        public Inventory UpdateInventory2(Inventory i)
        {
            return _repo.UpdateInventory2(i);
        }

        public List<Order> GetAllOrderbyStoreId(int i)
        {
            return _repo.GetAllOrderbyStoreId(i);
        }
    }
}
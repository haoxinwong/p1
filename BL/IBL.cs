using System.Collections.Generic;
using Models;

namespace BL
{
    public interface IBL
    {
        public Order AddAOrder(Order order);
        public List<Order> GetAllOrderbyId(int id);
        public LineItem AddALineItem(LineItem lineitem);
        public Customer Add(Customer cust);
        public List<Customer> GetAll();
        public Customer Update(Customer customerToUpdate);
        public Inventory UpdateInventory(Inventory inventory);
        public Inventory UpdateInventory(Inventory inventory, string str);
        public Inventory UpdateInventory2(Inventory inventory);
        public List<Store> GetAllStore();
        public Store GetOneStoreById(int id);
        public Store Update(Store storeToUpdate);
        public Inventory AddInventoryItem(Inventory inv);
        public Store Add(Store sto);
        public List<Order> GetAllOrderbyStoreId(int id);
    }
}
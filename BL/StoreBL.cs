using System.Collections.Generic;
using P0_M.Models;
using System;
using P0_M.DL;


namespace P0_M.BL
{
    public class StoreBL:IBL
    {
        private StoreDBRepo _repo;

        public StoreBL(StoreDBRepo repo){
            _repo = repo;
        }

        public List<Store> GetAll(){
            return _repo.GetAll();
        }
        public Store Add(Store store){
            return _repo.Add(store);

        }
        public Store Update(Store store){
            return _repo.Update(store);
        }

        public Store GetOneStoreById(int id){
            return _repo.GetOneStoreById(id);
        }

        public Inventory UpdateInventory(Inventory i){
            return _repo.UpdateInventory(i);
        }

        public Inventory UpdateInventory(Inventory i,string s){
            return _repo.UpdateInventory(i,s);
        }

        public Inventory AddInventoryItem(Inventory i){
            return _repo.AddInventoryItem(i);
        }

        public Inventory UpdateInventory2(Inventory i){
            return _repo.UpdateInventory2(i);
        }

        public List<Order> GetAllOrderbyId(int i){
            return _repo.GetAllOrderbyId(i);
        }
    }
}
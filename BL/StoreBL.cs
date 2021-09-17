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
    }
}
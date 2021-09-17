using System.Collections.Generic;
using P0_M.Models;
using System.IO;
using System.Text.Json;
using System;
using System.Linq;

namespace P0_M.DL
{
    //using fileRepo currently
    public class StoreDBRepo:IRepo
    {
        private const string storeFilePath = "../DL/Stores.json";
        
        
        private string jsonString;

        public List<Store> GetAll(){
            jsonString = File.ReadAllText(storeFilePath);

            return JsonSerializer.Deserialize<List<Store>>(jsonString);
        }
        public Store Add(Store store){
            List<Store>allStores = GetAll();
            allStores.Add(store);
            jsonString = JsonSerializer.Serialize(allStores);
            File.WriteAllText(storeFilePath,jsonString);
            return store;
        }


        public Store Update(Store storeToUpdate){
            List<Store>allStores = GetAll();
            int storeIndex = allStores.FindIndex(r => r.Equals(storeToUpdate));
            allStores[storeIndex] = storeToUpdate;
            jsonString = JsonSerializer.Serialize(allStores);
            File.WriteAllText(storeFilePath, jsonString);
            return storeToUpdate;
        }

        

    }
}
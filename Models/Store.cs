using System;
using System.Collections.Generic;
using System.Text;

namespace P0_M.Models
{
    public class Store
    {
        public string Name{get;set;}

        public string Address{get;set;}
        public List<LineItem> Inventory{get;set;}
        private StringBuilder _sb = new StringBuilder();
        private List<Customer> OrderHistory{get;set;}

        public Store(){}

        public Store(string name,string address,List<LineItem> inventory){
            this.Name = name;
            this.Address = address;
            this.Inventory = inventory;
        }

        public override string ToString(){
            return $"Store name: {this.Name} \n Address: {this.Address}";
        }

        public void AdjustInventory(List<LineItem>customerLineItem,List<int>itempos){
            for (int i = 0; i < customerLineItem.Count; i++)
            {
                Inventory[itempos[i]].Quantity = Inventory[itempos[i]].Quantity - customerLineItem[i].Quantity;
            }
        }



    }
}
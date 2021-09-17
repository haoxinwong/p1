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

        public Store(){}

        public Store(string name,string address,List<LineItem> inventory){
            this.Name = name;
            this.Address = address;
            this.Inventory = inventory;
        }

        public override string ToString(){
            return $"Store name: {this.Name} \n Address: {this.Address}";
        }

        // public string showInventory(){
        //     _sb = "Current Inventory\n";
        //     foreach (LineItem item in this.Inventory)
        //     {
        //         _sb.Append()
        //     }

        // }



    }
}
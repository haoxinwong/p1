using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Serilog;

namespace Models
{
    public class Store
    {

        public string Name { get; set; }
        /*private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Regex pattern = new Regex("^[a-zA-Z0-9 !?']+$");

                if (value?.Length == 0)
                {
                    InputInvalidException e = new InputInvalidException("Restaurant name can't be empty");
                    Log.Warning(e.Message);
                    throw e;
                }
                else if (!pattern.IsMatch(value))
                {
                    throw new InputInvalidException("Store name can only have alphanumeric characters, !, and ?.");
                }
                else
                {
                    _name = value;
                }
            }
        }*/

        public string Address { get; set; }

        /*private string _Address;

        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                Regex pattern = new Regex("^[a-zA-Z0-9 ]+$");

                if (value?.Length == 0)
                {
                    InputInvalidException e = new InputInvalidException("Restaurant name can't be empty");
                    Log.Warning(e.Message);
                    throw e;
                }
                else if (!pattern.IsMatch(value))
                {
                    throw new InputInvalidException("Store address can only have alphanumeric characters, and -");
                }
                else
                {
                    _name = value;
                }

            }
        }*/
        public List<Inventory> Inventory{get;set;}
        private StringBuilder _sb = new StringBuilder();

        public int Id{get;set;}

        public Store(){}

        public Store(string name,string address,List<Inventory> inventory){
            this.Name = name;
            this.Address = address;
            this.Inventory = inventory;
        }

        public Store(string name, string address)
        {
            this.Name = name;
            this.Address = address;
            this.Inventory = new List<Inventory>();
        }

        public override string ToString(){
            return $"Store name: {this.Name} \n Address: {this.Address}";
        }

        public void AdjustInventory(List<LineItem> customerLineItem,List<int> itempos){
            for (int i = 0; i < customerLineItem.Count; i++)
            {
                Inventory[itempos[i]].Quantity = Inventory[itempos[i]].Quantity - customerLineItem[i].Quantity;
            }
        }
    }
}
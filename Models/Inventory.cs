using System;
namespace Models
{
    public class Inventory
    {
        public string Name{get;set;}
        public decimal Price{get;set;}
        public int Quantity{get;set;}
        public int StoreId{get;set;}
        public int Id{get;set;}

        public Inventory(){}

        public Inventory(string name, decimal price){
            this.Name = name;
            this.Price = price;
        }

        public Inventory(string name, decimal price, int quantity):this(name,price){
            this.Quantity = quantity; 
        }

        public Inventory(string name, decimal price, int quantity,int storeid):this(name,price,quantity){
            this.StoreId = storeid;
        }

        public override string ToString(){
            return $"Name : {Name}, Price : {Price}, Quantity : {Quantity}";
        }
    }
}
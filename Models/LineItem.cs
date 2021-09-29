using System;
namespace P0_M.Models
{
    public class LineItem
    {
        public string Name{get;set;}
        public decimal Price{get;set;}
        public int Quantity{get;set;}
        public int Id{get;set;}
        public int OrderId{get;set;}

        public LineItem(){}

        public LineItem(string name, decimal price){
            this.Name = name;
            this.Price = price;
        }

        public LineItem(string name, decimal price, int quantity):this(name,price){
            this.Quantity = quantity; 
        }

        public LineItem(string name, decimal price, int quantity, int OrderId):this(name,price,quantity){
            this.OrderId = OrderId;
        }

        public override string ToString(){
            return $"Name : {Name}, Price : {Price}, Quantity : {Quantity}";
        }
    }
}
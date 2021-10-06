using System;

namespace Models
{
    public class Product
    {
        public string Name{get;set;}
        public decimal Price{get;set;}

        public Product(){

        }

        public Product(string name,decimal price){
            this.Name = name;
            this.Price = price;
        }

        public override string ToString(){
            return $"Name : {Name}, Price : {Price}";
        }
    }
}
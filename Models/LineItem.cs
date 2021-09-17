using System;
namespace P0_M.Models
{
    public class LineItem
    {
        public Product Item{get;set;}
        public int Quantity{get;set;}

        public LineItem(){}

        public LineItem(Product item){
            this.Item = item;
        }

        public LineItem(Product item, int quantity):this(item){
            this.Quantity = quantity; 
        }

        public override string ToString(){
            return $"Item : [{Item}], Quantity : {Quantity}";
        }
    }
}
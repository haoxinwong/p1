using System;
using System.Collections.Generic;
using System.DateTime;

namespace P0_M.Models
{
    public class Order
    {
        public decimal Total{get;set;}
        public List<LineItem>LineItems{get;set;}
        public DateTime time{get;set;}

        public Order(){}

        public Order(List<LineItem>lineItems){
            this.LineItems = lineItems;
            this.time = DateTime.Now;
            calTotal();
        }

        public decimal calTotal(){
            decimal Total = 0;
            if (LineItems!=null){
                foreach (LineItem item in LineItems)
                {
                    Total+=item.Item.Price*item.Quantity;
                }
            }
            return Total;
        }
        
    }
}
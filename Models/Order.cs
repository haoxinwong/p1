using System;
using System.Collections.Generic;

namespace P0_M.Models
{
    public class Order
    {
        public decimal Total{get;set;}
        public List<LineItem> LineItems{get;set;}
        public DateTime Time{get;set;}
        public string Location{get;set;}

        public Order(){}

        public Order(List<LineItem>lineItems){
            this.LineItems = lineItems;
            this.Time = DateTime.Now;
            calTotal();
        }
        public Order(List<LineItem>lineItems, string location){
            this.LineItems = lineItems;
            this.Location = location;
            this.Time = DateTime.Now;
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
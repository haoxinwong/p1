using System;
using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public int Id{get;set;}
        public decimal Total{get;set;}
        public List<LineItem> LineItems{get;set;}
        public DateTime Time{get;set;}
        public string Location{get;set;}
        public int StoreId{get;set;}
        public int CustomerId{get;set;}
        public Order(){}

        public Order(List<LineItem>lineItems){
            this.LineItems = lineItems;
            this.Time = DateTime.Now;
            this.Total = calTotal();
        }
        public Order(List<LineItem>lineItems, string location, int LocationID, int CustomerId){
            this.LineItems = lineItems;
            this.Location = location;
            this.Time = DateTime.Now;
            this.StoreId = LocationID;
            this.CustomerId = CustomerId;
            this.Total = calTotal();
        }

        public Order(List<LineItem>lineItems, string location, int LocationID,int CustomerId,DateTime time){
            this.LineItems = lineItems;
            this.Location = location;
            this.Time = time;
            this.StoreId = LocationID;
            this.CustomerId = CustomerId;
            this.Total = calTotal();
        }

        public decimal calTotal(){
            decimal Total = 0;
            if (LineItems!=null){
                foreach (LineItem item in LineItems)
                {
                    Total+=item.Price*item.Quantity;
                }
            }
            return Total;
        }
        
    }
}
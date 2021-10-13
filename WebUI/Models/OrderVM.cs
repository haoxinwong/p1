using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class OrderVM
    {
        public OrderVM() { }
        public OrderVM(Order order)
        {
            this.LineItems = order.LineItems;
            this.Id = order.Id;
            this.Total = order.Total;
            this.Time = order.Time;
            this.Location = order.Location;
            this.CustomerId = order.CustomerId;
            this.StoreId = order.StoreId;
        }

        public decimal Total { get; set; }
        public List<LineItem> LineItems { get; set; }
        public DateTime Time { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9 !?']+$", ErrorMessage = "Location name can only have alphanumeric characters, !, and ?.")]
        public string Location { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }

        public Order ToModel()
        {
            Order newOrder;
            try
            {
                newOrder = new Order
                {
                    Id = this.Id,
                    Total = this.Total,
                    Location = this.Location ?? "",
                    StoreId = this.StoreId,
                    CustomerId = this.CustomerId,
                    Time = this.Time
                };

                //ternary 
                // IfStatement ? ifTrue : ifFalse
                // null checker
                // ifExists/notNull ?? ifFalse
                // IsNull?.Prperty
            }
            catch
            {
                throw;
            }
            return newOrder;
        }
    }
}


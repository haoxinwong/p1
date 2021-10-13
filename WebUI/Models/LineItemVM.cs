using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.ComponentModel.DataAnnotations;
namespace WebUI.Models
{
    public class LineItemVM
    {
        public LineItemVM() { }
        public LineItemVM(LineItem l)
        {
            Name = l.Name;
            Price = l.Price;
            Quantity = l.Quantity;
            OrderId = l.OrderId;
            Id = l.Id;
        }
        [Required]
        [RegularExpression("^[a-zA-Z0-9 !?']+$", ErrorMessage = "LineItem Name can only have alphanumeric characters, !, and ?.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\-{0,1}[0-9]{0,}\.{0,1}[0-9]{1,}$", ErrorMessage = "customer phone number can only have 10 numbers")]
        public decimal Price { get; set; }
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "quantity can only have numbers")]
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int Id { get; set; }

        public LineItem ToModel()
        {
            LineItem newLineItem;
            try
            {
                newLineItem = new LineItem
                {
                    Id = this.Id,
                    Name = this.Name ?? "",
                    Price = this.Price,
                    OrderId = this.OrderId,
                    Quantity = this.Quantity
                };

            }
            catch
            {
                throw;
            }
            return newLineItem;
        }
    }
}

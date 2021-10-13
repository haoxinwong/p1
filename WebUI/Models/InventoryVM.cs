using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class InventoryVM
    {
        public InventoryVM() { }
        public InventoryVM(Inventory i)
        {
            Name = i.Name;
            Price = i.Price;
            Quantity = i.Quantity;
            StoreId = i.StoreId;
            Id = i.Id;

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
        public int StoreId { get; set; }
        public int Id { get; set; }
        public Inventory ToModel()
        {
            Inventory newInventory;
            try
            {
                newInventory = new Inventory
                {
                    Id = this.Id,
                    Name = this.Name ?? "",
                    Price = this.Price,
                    StoreId = this.StoreId,
                    Quantity = this.Quantity
                };

            }
            catch
            {
                throw;
            }
            return newInventory;
        }
    }
}

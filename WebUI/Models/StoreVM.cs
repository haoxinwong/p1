using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class StoreVM
    {
        public StoreVM() { }
        public StoreVM(Store store)
        {
            this.Id = store.Id;
            this.Name = store.Name;
            this.Address = store.Address;
        }
        public int Id { get; set; }

        [Display(Name = "Store Name")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9 !?']+$", ErrorMessage = "Store name can only have alphanumeric characters, !, and ?.")]
        public string Name { get; set; }

     
        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Store address can only have alphanumeric characters, and -")]
        public string Address { get; set; }

        public List<Inventory> Inventory { get; set; }

        public Store ToModel()
        {
            Store newStore;
            try
            {
                newStore = new Store
                {
                    Id = this.Id,
                    Name = this.Name ?? "",
                    Address = this.Address ?? ""
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
            return newStore;
        }
    }
}

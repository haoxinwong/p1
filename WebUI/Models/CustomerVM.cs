using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.Text;
namespace WebUI.Models
{
    public class CustomerVM
    {
        public CustomerVM() { }
        public CustomerVM(Customer customer)
        {
            this.Id = customer.Id;
            this.Name = customer.Name;
            this.Address = customer.Address;
            this.PhoneNumber = customer.PhoneNumber;
            
        }
        public int Id { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9 !?']+$", ErrorMessage = "customer name can only have alphanumeric characters, !, and ?.")]
        public string Name { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "customer address can only have alphanumeric characters, and -")]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$", ErrorMessage = "customer phone number can only have 10 numbers")]
        public string PhoneNumber { get; set; }
        public List<OrderVM> Orders { get; set; }

        public Customer ToModel()
        {
            Customer newCustomer;
            try
            {
                newCustomer = new Customer
                {
                    Id = this.Id,
                    Name = this.Name ?? "",
                    Address = this.Address ?? "",
                    PhoneNumber = this.PhoneNumber ?? ""
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
            return newCustomer;
        }
    }
}

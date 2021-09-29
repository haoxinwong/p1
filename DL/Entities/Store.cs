using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Store
    {
        public Store()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

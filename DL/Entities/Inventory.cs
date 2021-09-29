using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class CustomerLineItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}

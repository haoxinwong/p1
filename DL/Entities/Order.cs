using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Order
    {
        public Order()
        {
            CustomerLineItems = new HashSet<CustomerLineItem>();
        }

        public int Id { get; set; }
        public decimal? Total { get; set; }
        public DateTime? OrderTime { get; set; }
        public string StoreLocation { get; set; }
        public int? LocationId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Location { get; set; }
        public virtual ICollection<CustomerLineItem> CustomerLineItems { get; set; }
    }
}

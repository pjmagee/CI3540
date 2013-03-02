using System;
using System.Collections.Generic;

namespace CI3540.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual int? EmployeeId { get; set; }
        
        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public decimal Total { get; set; }

        public virtual Address ShippingAddress { get; set; }
        public virtual int? ShippingAddressId { get; set; }

        public virtual Address BillingAddress { get; set; }
        public virtual int? BillingAddressId { get; set; }
        
        public Status Status { get; set; }
    }
}
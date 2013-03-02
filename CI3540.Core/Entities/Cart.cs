using System;
using System.Collections.Generic;

namespace CI3540.Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
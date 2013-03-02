using System.Collections.Generic;

namespace CI3540.Core.Entities
{
    public class Customer : User
    {
        public virtual Cart Cart { get; set; }
        public virtual int? CartId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}


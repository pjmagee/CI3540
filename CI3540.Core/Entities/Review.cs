using System;

namespace CI3540.Core.Entities
{
    public class Review
    {
        public virtual int Id { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }

        public virtual Product Product { get; set; }
        public virtual int? ProductId { get; set; }

        public virtual string Comment { get; set; }
        public virtual float Rating { get; set; }
        public virtual bool Approved { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }
    }
}
namespace CI3540.Core.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual int? OrderId { get; set; }

        public virtual Product Product { get; set; }
        public virtual int? ProductId { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual int? CartId { get; set; }

        public Status Status { get; set; }
        public string Tracking { get; set; }
    }
}
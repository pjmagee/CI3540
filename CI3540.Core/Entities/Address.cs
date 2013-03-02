namespace CI3540.Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }
    }
}
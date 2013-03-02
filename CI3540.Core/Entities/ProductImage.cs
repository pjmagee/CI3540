namespace CI3540.Core.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public virtual Product Product { get; set; }
        public virtual int? ProductId { get; set; }

        public bool Primary { get; set; }
    }
}
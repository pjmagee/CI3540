namespace CI3540.UI.Areas.Store.Models
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public bool Default { get; set; }
        public string Url { get; set; }
        public bool Delete { get; set; }
        public int ProductId { get; set; }
    }
}
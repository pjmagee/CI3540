namespace CI3540.UI.Areas.Store.Models
{
    public class ProductSummaryViewModel
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int ReviewsCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
using System;

namespace CI3540.UI.Areas.Admin.Models
{
    public class OrderSummaryViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderSubmitted { get; set; }
        public string Status { get; set; }
        public int Items { get; set; }
        public int CustomerId { get; set; }
    }
}

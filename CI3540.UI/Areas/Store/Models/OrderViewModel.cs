using System;
using System.Collections.Generic;

namespace CI3540.UI.Areas.Store.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public IEnumerable<OrderLineViewModel> OrderLineViewModels { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
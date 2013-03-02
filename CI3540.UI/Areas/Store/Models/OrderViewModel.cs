﻿using System.Collections.Generic;

namespace CI3540.UI.Areas.Store.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public IEnumerable<OrderLineViewModel> OrderLineViewModels { get; set; }
    }
}
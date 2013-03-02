using System;
using System.Collections.Generic;

namespace CI3540.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Category Parent { get; set; }
        public virtual int? ParentId { get; set; }
    }
} 
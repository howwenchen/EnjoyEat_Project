using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public byte CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public byte DisplayOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

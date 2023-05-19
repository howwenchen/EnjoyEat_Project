using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public byte CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}

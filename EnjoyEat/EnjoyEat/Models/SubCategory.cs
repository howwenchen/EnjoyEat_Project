using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class SubCategory
    {
        public byte CategoryId { get; set; }
        public byte SubCategoryId { get; set; }
        public string SubCategoriesName { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
    }
}

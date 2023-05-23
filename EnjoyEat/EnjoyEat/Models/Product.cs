using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string? MealImg { get; set; }
        public string ProductName { get; set; } = null!;
        public short UnitPrice { get; set; }
        public byte Costs { get; set; }
        public short? Stock { get; set; }
        public string? Description { get; set; }
        public string? Recipe { get; set; }
        public byte SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

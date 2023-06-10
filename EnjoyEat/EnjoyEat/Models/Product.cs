using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string? MealImg { get; set; }
        public string ProductName { get; set; } 
        public short UnitPrice { get; set; }
        public short Costs { get; set; }
        public short? Stock { get; set; }
        public string? Description { get; set; }
        public string? Recipe { get; set; }
        public byte SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

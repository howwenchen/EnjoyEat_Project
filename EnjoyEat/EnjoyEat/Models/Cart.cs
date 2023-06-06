using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public int? MemberId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}

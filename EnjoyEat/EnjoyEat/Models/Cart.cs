using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Cart
    {

        public int CartId { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int? Quantity { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Table
    {
        public Table()
        {
            Orders = new HashSet<Order>();
        }

        public short TableId { get; set; }
        public string Location { get; set; } = null!;
        public short Capacity { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}

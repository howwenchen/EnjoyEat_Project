using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentTypes { get; set; } = null!;
        public DateTime CreateTime { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}

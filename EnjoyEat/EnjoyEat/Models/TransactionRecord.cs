using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class TransactionRecord
    {
        public int MemberId { get; set; }
        public int OrderId { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}

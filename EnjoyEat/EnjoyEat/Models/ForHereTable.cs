using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class ForHereTable
    {
        public short TableId { get; set; }
        public short? TableNumber { get; set; }
        public short? Capacity { get; set; }

        public virtual Table Table { get; set; } = null!;
    }
}

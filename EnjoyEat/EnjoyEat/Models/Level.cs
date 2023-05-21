using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Level
    {
        public Level()
        {
            Members = new HashSet<Member>();
        }

        public string LevelName { get; set; } = null!;
        public double DiscountRate { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}

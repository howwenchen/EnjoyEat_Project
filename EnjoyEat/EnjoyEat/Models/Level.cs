using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Level
    {
        public Level()
        {
            MemberLevels = new HashSet<MemberLevel>();
        }

        public string LevelName { get; set; } = null!;
        public int DiscountRate { get; set; }

        public virtual ICollection<MemberLevel> MemberLevels { get; set; }
    }
}

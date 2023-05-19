using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class MemberLevel
    {
        public int MemberId { get; set; }
        public string LevelName { get; set; } = null!;
        public int PointsId { get; set; }

        public virtual Level LevelNameNavigation { get; set; } = null!;
        public virtual Member? Member { get; set; }
    }
}

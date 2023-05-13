using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class MemberPoint
    {
        public MemberPoint()
        {
            MemberLevels = new HashSet<MemberLevel>();
        }

        public int PointsId { get; set; }
        public int MemberId { get; set; }
        public int AccumulatedPoints { get; set; }
        public DateTime GetDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual ICollection<MemberLevel> MemberLevels { get; set; }
    }
}

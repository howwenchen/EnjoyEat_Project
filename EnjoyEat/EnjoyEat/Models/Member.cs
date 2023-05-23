using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Member
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? RegisterDay { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? LevelName { get; set; }
        public double? LevelDiscount { get; set; }

        public virtual Level? LevelNameNavigation { get; set; }
        public virtual MemberLogin? MemberLogin { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

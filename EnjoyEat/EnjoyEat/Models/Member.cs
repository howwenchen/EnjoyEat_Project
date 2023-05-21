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
        public string Gender { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime RegisterDay { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LevelName { get; set; } = null!;

        public virtual Level LevelNameNavigation { get; set; } = null!;
        public virtual MemberLogin? MemberLogin { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

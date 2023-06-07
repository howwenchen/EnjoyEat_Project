using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class MemberLogin
    {
        public int MemberId { get; set; }
        public string Account { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
        public string? Salt { get; set; }

        public virtual Member Member { get; set; } = null!;
    }
}

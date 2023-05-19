using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class MemberLogin
    {
        public int MemberId { get; set; }
        public byte[] Account { get; set; } = null!;
        public byte[] Password { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Member
    {
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
        public byte[] Account { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
    }
}

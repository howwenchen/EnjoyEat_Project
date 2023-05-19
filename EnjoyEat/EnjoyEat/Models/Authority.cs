using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Authority
    {
        public Authority()
        {
            AuthorityUses = new HashSet<AuthorityUse>();
        }

        public int RoleId { get; set; }
        public string Role { get; set; } = null!;
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; } = null!;
        public virtual ICollection<AuthorityUse> AuthorityUses { get; set; }
    }
}

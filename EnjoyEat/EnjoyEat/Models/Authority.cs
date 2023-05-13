using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Authority
    {
        public int RoleId { get; set; }
        public string Role { get; set; } = null!;
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Authorities = new HashSet<Authority>();
        }

        public int PermissonId { get; set; }
        public string PermissionContent { get; set; } = null!;

        public virtual ICollection<Authority> Authorities { get; set; }
    }
}

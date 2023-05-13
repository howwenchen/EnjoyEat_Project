using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class AuthorityUse
    {
        public AuthorityUse()
        {
            Personnel = new HashSet<Personnel>();
        }

        public int EmployeesId { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<Personnel> Personnel { get; set; }
    }
}

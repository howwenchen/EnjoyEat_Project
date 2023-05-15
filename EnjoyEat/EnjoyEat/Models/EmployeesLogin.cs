using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class EmployeesLogin
    {
        public int EmployeesId { get; set; }
        public string UserName { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public DateTime LoginTime { get; set; }

        public virtual Employee Employees { get; set; } = null!;
    }
}

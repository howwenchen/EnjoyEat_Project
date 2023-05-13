using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Department
    {
        public Department()
        {
            Personnel = new HashSet<Personnel>();
        }

        public int DepartmentId { get; set; }
        public string Department1 { get; set; } = null!;
        public int EmployeesQuantity { get; set; }

        public virtual ICollection<Personnel> Personnel { get; set; }
    }
}

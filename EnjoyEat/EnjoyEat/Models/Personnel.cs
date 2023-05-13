using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Personnel
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string Posting { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public DateTime ResignationDate { get; set; }
        public int? Manager { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual AuthorityUse? ManagerNavigation { get; set; }
    }
}

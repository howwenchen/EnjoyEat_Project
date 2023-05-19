using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
            CustomerServices = new HashSet<CustomerService>();
            Personnel = new HashSet<Personnel>();
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string IdentityId { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Education { get; set; } = null!;

        public virtual EmployeesLogin? EmployeesLogin { get; set; }
        public virtual EmployeesSalary? EmployeesSalary { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<CustomerService> CustomerServices { get; set; }
        public virtual ICollection<Personnel> Personnel { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class EmployeesSalary
    {
        public int EmployeeId { get; set; }
        public int? BasicSalary { get; set; }
        public int? Bonus { get; set; }
        public int? OverTime { get; set; }
        public string? Performance { get; set; }
        public decimal? Wage { get; set; }
        public double? Hours { get; set; }
        public int TotalSalary { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class EmployeesSalary
    {
        public int EmployeeId { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? OverTime { get; set; }
        public decimal? Performance { get; set; }
        public decimal? Wage { get; set; }
        public double? Hours { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}

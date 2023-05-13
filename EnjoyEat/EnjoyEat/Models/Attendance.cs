using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Attendance
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpectedStartTime { get; set; }
        public DateTime ActualStartTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public DateTime ActualEndTime { get; set; }
        public int? LateTime { get; set; }
        public int? OverTime { get; set; }
        public double? SickLeave { get; set; }
        public double? PersonalLeave { get; set; }
        public double? FuneralLeave { get; set; }
        public DateTime? WorkTotalHours { get; set; }
        public double? LeaveTotalHours { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}

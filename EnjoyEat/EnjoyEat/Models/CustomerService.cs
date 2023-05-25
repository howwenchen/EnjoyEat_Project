using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class CustomerService
    {
        public int QuestionId { get; set; }
        public DateTime? QuestionDatetime { get; set; }
        public string QuestionKeynote { get; set; } = null!;
        public string QuestionContent { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string ServiceOption { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? EmployeeId { get; set; }
        public string? AnswerContent { get; set; }
        public DateTime? AnswerDatetime { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}

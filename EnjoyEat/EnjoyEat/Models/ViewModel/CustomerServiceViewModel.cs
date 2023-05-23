﻿namespace EnjoyEat.Models.ViewModel
{
    public class CustomerServiceViewModel
    {
        public int QuestionId { get; set; }
        public DateTime QuestionDatetime { get; set; }
        public string QuestionKeynote { get; set; } = null!;
        public string QuestionContent { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int EmployeeId { get; set; }
        public string AnswerContent { get; set; } = null!;
        public DateTime AnswerDatetime { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}

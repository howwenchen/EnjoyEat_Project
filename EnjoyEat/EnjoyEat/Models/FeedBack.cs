using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class FeedBack
    {
        public int OrderId { get; set; }
        public string? FeedBackName { get; set; }
        public string? Frequency { get; set; }
        public string? Age { get; set; }
        public string? Email { get; set; }
        public string? Source { get; set; }
        public string? Purpose { get; set; }
        public int? Enviroment { get; set; }
        public int? Serve { get; set; }
        public int? Dish { get; set; }
        public int? Price { get; set; }
        public int? Overall { get; set; }
        public double? Average { get; set; }
        public string? Suggestion { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; } = null!;
        public string? Category { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}

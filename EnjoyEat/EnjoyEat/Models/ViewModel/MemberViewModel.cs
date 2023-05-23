using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace EnjoyEat.Models.ViewModel
{
    public class MemberViewModel
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int MemberId { get; set; }
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Gender { get; set; } = null;
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? LevelName { get; set; } = null;

        public List<MemberOrderViewModel>? Orders { get; set; }

        public double? LevelDiscount { get; set; }


    }

    public class MemberOrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public short? TableId { get; set; }
        public int TotalPrice { get; set; }
        public bool IsTakeway { get; set; }
    }
}

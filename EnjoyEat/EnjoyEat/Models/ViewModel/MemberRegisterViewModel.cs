using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EnjoyEat.Models.ViewModel
{
    public class MemberRegisterViewModel
    {

        public int MemberId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? RegisterDay { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? LevelName { get; set; } = null!;
        public double? LevelDiscount { get; set; }
        public string? Account { get; set; } = null!;
        public string? Password { get; set; } = null!;
        public string? Role { get; set; }
        public bool? IsActive { get; set; }

        


    }

}


using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EnjoyEat.DTOs
{
    public class MemberRegisterDTO
    {
        [Required]
        [NotNull]
        public string FirstName { get; set; } = null!;
        [Required]
        [NotNull]
        public string LastName { get; set; } = null!;
        [Required]
        [NotNull]
        public string Gender { get; set; } = null!;
        [Required]
        [NotNull]
        public DateTime Birthday { get; set; }
        [Required]
        [NotNull]
        public string Address { get; set; } = null!;
        [Required]
        [StringLength(10)]
        [NotNull]
        public string Phone { get; set; } = null!;
        [Required]
        [EmailAddress]
        [NotNull]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(20)]
        [NotNull]
        public string Account {get; set; } = null!;
        [Required]
        [MaxLength(20)]
        [NotNull]
        public string Password { get; set; } = null!;

    }
}

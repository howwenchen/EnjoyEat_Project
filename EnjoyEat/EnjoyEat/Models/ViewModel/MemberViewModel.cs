using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace EnjoyEat.Models.ViewModel
{
    public class MemberViewModel
    {
        [System.ComponentModel.DataAnnotations.Key]

        public int MemberId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LevelName { get; set; } = null!;
        public byte[] Account { get; set; } = null!;
        public byte[] Password { get; set; } = null!;

        public virtual MemberLogin? MemberLogin { get; set; }
        public virtual TransactionRecord? TransactionRecord { get; set; }
        public virtual ICollection<MemberPoint> MemberPoints { get; set; }
    }
}

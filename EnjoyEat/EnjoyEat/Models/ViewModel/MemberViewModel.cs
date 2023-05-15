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
        public byte[] Password { get; set; } = null!;


        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TableId { get; set; }
        public int CustomerCount { get; set; }
        public int TotalPrice { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }


    }
}

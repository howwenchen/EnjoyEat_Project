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
		public double? FinalPrice { get; set; }
		public bool IsTakeway { get; set; }
		public virtual ICollection<MemberOrderDetailViewModel> OrderDetails { get; set; }
	}

	public class MemberOrderDetailViewModel
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int OrderDetialId { get; set; }
		public decimal UnitPrice { get; set; }
		public short Quantity { get; set; }
		public float Discount { get; set; }
		public int SubtotalPrice { get; set; }
		public string ProductName { get; set; } = null!;

	}
}
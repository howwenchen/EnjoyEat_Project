namespace EnjoyEat.Models.ViewModel
{
	public class SendOrderViewModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; } = null!;
		public short UnitPrice { get; set; }
	}

	public class SendOrderToCart
	{
		public int ProductId { get; set; }
		public string? MealImg { get; set; }
		public string ProductName { get; set; } = null!;
		public short UnitPrice { get; set; }
	}
}

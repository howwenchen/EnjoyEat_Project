namespace EnjoyEat.Models.ViewModel
{
	public class MenuViewModel
	{
		public int ProductId { get; set; }
		public string? MealImg { get; set; }
		public string ProductName { get; set; } = null!;
		public short UnitPrice { get; set; }
		public byte Costs { get; set; }
		public short? Stock { get; set; }
		public string? Description { get; set; }
		public string? Recipe { get; set; }
		public byte SubCategoryId { get; set; }
		public string CategoryName { get; set; }
		public virtual ICollection<SubCategory> SubCategory { get; set; } = null!;
	}
}
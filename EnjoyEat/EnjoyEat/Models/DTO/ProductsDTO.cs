namespace EnjoyEat.Models.DTO
{
    public class ProductsDTO
    {
        public int ProductId { get; set; }
        public string? MealImg { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public short UnitPrice { get; set; }
        public byte Costs { get; set; }
        public short? Stock { get; set; }
        public string? Description { get; set; }
        public string? Recipe { get; set; }
        public byte SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; } = null!;
        public string CategoryName { get; internal set; }
        public string SubCategoriesName { get; internal set; }
    }
    public class SubCategoryDTO
    {
        public byte CategoryId { get; set; }
        public byte SubCategoryId { get; set; }
        public string SubCategoriesName { get; set; } = null!;
    }
    public partial class CategoryDTO
    {
        public byte CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}


using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Models.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            MealImg = "/img/food/noPic.jpg";
        }
        public int ProductId { get; set; }
        public string? MealImg { get; set; }
        public string? ProductName { get; set; } = null!;
        public short UnitPrice { get; set; }
        public byte Costs { get; set; }
        public short? Stock { get; set; } = 0!;
        public string? Description { get; set; }
        public string? Recipe { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoriesName { get; set; }
        public byte SubCategoryId { get; set; }
    }
    public class SubCategoryDTO
    {
        public byte CategoryId { get; set; }
        public byte SubCategoryId { get; set; }
        public string SubCategoriesName { get; set; } = null!;
    }
    public partial class CategoryDTO { 
        public byte CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}


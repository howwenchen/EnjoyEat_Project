namespace EnjoyEat.Areas.OrderForHere.Models.ViewModels
{
    public class StartOrderViewModel
    {
        public class SubCategories
        {
            public byte CategoryId { get; set; }
            public byte SubCategoryId { get; set; }
            public string ?SubCategoriesName { get; set; }
        }

        public class Products
        {
            public int ProductId { get; set; }
            public byte SubCategoryId { get; set; }
            public string ?MealImg { get; set; }
            public string ?ProductName { get; set; }
            public short UnitPrice { get; set; }
        }
    }
}

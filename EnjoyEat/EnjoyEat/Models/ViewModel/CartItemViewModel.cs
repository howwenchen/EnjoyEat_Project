namespace EnjoyEat.Models.ViewModel
{
    public class CartItemViewModel
    {
        public string MealImg { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public byte SubCategoryId { get; set; }
        public short UnitPrice { get; set; }


    }
}

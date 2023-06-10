namespace EnjoyEat.Models.ViewModel
{
    namespace EnjoyEat.Models.ViewModel
    {
        public class OrderDetailViewModel
        {
            public int OrderDetailId { get; set; }
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public int SubtotalPrice { get; set; }
            public string MealImg { get; set; }
        }
    }

}

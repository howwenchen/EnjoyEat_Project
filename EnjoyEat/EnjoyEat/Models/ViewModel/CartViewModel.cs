namespace EnjoyEat.Models.ViewModel
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            Items = new List<CartItemViewModel>();
        }
        public List<CartItemViewModel>? Items { get; set; }
        public int? MemberId { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
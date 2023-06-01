namespace EnjoyEat.Models.ViewModel
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public short UnitPrice { get; set; }
        public int CartId { get; set; }   
        public int MemberId { get; set; }  
    }
}

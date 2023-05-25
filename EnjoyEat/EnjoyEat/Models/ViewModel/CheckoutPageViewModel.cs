namespace EnjoyEat.Models.ViewModel
{
    public class CheckoutPageViewModel
    {
        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsTakeway { get; set; }
        public short? TableId { get; set; }
        public int CustomerCount { get; set; }
        public int TotalPrice { get; set; }
        public bool IsSuccess { get; set; }
        public double? CampaignDiscount { get; set; }
        public double? LevelDiscount { get; set; }
        public double? FinalPrice { get; set; }
        public string? FirstName { get; internal set; }
        public string? LastName { get; internal set; }

        public List<OrderDetail>? OrderDetails { get; set; }
        public int TotalItems { get; set; }
    }
}

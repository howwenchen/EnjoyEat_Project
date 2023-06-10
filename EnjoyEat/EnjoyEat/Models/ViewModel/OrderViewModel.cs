namespace EnjoyEat.Models.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsTakeway { get; set; }
        public short? TableId { get; set; }
        public int? CustomerCount { get; set; }
        public int TotalPrice { get; set; }
        public bool IsSuccess { get; set; }
        public double? CampaignDiscount { get; set; }
        public double? LevelDiscount { get; set; }
        public double? FinalPrice { get; set; }
        public double? CountPrice { get; set; }
    }
}

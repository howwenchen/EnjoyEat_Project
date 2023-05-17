using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Payments = new HashSet<Payment>();
            TransactionRecords = new HashSet<TransactionRecord>();
        }

        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsTakeway { get; set; }
        public int? TableId { get; set; }
        public int CustomerCount { get; set; }
        public int TotalPrice { get; set; }
        public bool IsSuccess { get; set; }
        public double? CampaignDiscount { get; set; }
        public double? LevelDiscount { get; set; }

        public virtual Member? Member { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<TransactionRecord> TransactionRecords { get; set; }
    }
}

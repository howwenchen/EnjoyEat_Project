﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EnjoyEat.Areas.OrderForHere.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

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

        public virtual Members Member { get; set; }
        public virtual Table Table { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
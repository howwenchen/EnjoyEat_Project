﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EnjoyEat.Areas.OrderForHere.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int ProductId { get; set; }
        public string MealImg { get; set; }
        public string ProductName { get; set; }
        public short UnitPrice { get; set; }
        public byte Costs { get; set; }
        public short? Stock { get; set; }
        public string Description { get; set; }
        public string Recipe { get; set; }
        public byte SubCategoryId { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EnjoyEat.Areas.OrderForHere.Models
{
    public partial class Table
    {
        public Table()
        {
            Orders = new HashSet<Orders>();
        }

        public short TableId { get; set; }
        public string Location { get; set; }
        public short Capacity { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EnjoyEat.Areas.OrderForHere.Models
{
    public partial class Categories
    {
        public Categories()
        {
            SubCategories = new HashSet<SubCategories>();
        }

        public byte CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<SubCategories> SubCategories { get; set; }
    }
}
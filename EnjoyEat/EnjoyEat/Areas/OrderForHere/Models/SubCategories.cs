﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EnjoyEat.Areas.OrderForHere.Models
{
    public partial class SubCategories
    {
        public byte CategoryId { get; set; }
        public byte SubCategoryId { get; set; }
        public string SubCategoriesName { get; set; }

        public virtual Categories Category { get; set; }
    }
}
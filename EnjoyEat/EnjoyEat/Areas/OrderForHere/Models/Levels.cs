﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EnjoyEat.Areas.OrderForHere.Models
{
    public partial class Levels
    {
        public Levels()
        {
            MemberLevel = new HashSet<MemberLevel>();
        }

        public string LevelName { get; set; }
        public int DiscountRate { get; set; }

        public virtual ICollection<MemberLevel> MemberLevel { get; set; }
    }
}
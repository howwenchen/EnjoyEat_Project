﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace EnjoyEat.Models.ViewModel
{
    public class MemberLoginViewModel
    {
        public int MemberId { get; set; }

        public bool isTakeaway { get; set; }

        [Required, MinLength(8), MaxLength(12)]     
        public string Account { get; set; } = null!;
        
        [Required, MinLength(8),MaxLength(12)]
        public string Password { get; set; } = null!;

    }
    public class ChangeViewModel
    {
        [Required, MinLength(8), MaxLength(12)]
        public string OriginalPassword { get; set; } = null!;

        [Required, MinLength(8), MaxLength(12)]
        public string NewPassword { get; set; } = null!;

    }
    public class SetViewModel
    {
        [Required, MinLength(8), MaxLength(12)]
        public string Password { get; set; } = null!;
    }

}

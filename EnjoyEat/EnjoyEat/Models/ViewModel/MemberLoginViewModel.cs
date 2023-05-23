﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace EnjoyEat.Models.ViewModel
{
    public class MemberLoginViewModel
    {
        [EmailAddress]
        [Display(Name ="會員編號")]
        public int MemberId { get; set; }
        [Required]
        [Display(Name="帳號")]
        public string Account { get; internal set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="密碼")]
        public string Password { get; internal set; } = null!;

        public virtual Member Member { get; set; } = null!;
    }
    // @Html.ValidationMessageFor根據LoginViewModel的 meta規則，來做防呆
    
}

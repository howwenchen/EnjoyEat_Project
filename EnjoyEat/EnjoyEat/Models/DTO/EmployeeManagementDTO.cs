﻿using System.ComponentModel.DataAnnotations;

namespace EnjoyEat.Models.DTO
{
	public class EmployeeManagementDTO
	{
			public int EmployeeId { get; set; }
			public string? Name { get; set; } = null!;
			public string? Gender { get; set; }
			public string? IdentityId { get; set; }
			[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
			public DateTime? Birthday { get; set; }
			public string? Phone { get; set; } = null!;
			public string? Address { get; set; }
			public string? Email { get; set; } = null!;
			public string? Education { get; set; }
			// 員工薪資
			public decimal? BasicSalary { get; set; }
			public decimal? Bonus { get; set; }
			public decimal? OverTime { get; set; }
			public decimal? Performance { get; set; }
			public decimal? Wage { get; set; }
			public double? Hours { get; set; }
			public decimal TotalSalary { get; set; }
			public DateTime PaymentDate { get; set; }
			public string Account { get; set; } 
			public string? Password { get; set; }
			public string? Role { get; set; }
		
	}
}

using System.ComponentModel.DataAnnotations;

namespace EnjoyEat.Models.DTO
{
	public class EmployeeManagementDTO
	{
		public int EmployeeId { get; set; }
		public string? Name { get; set; } = null!;
		public string? Gender { get; set; }
		public string? Phone { get; set; } = null!;
		public string? Email { get; set; } = null!;
		public string? Account { get; set; }
		public string? Password { get; set; }
		public string? Role { get; set; }

	}
}

namespace EnjoyEat.Areas.backend.Models.DTO
{
	public class EmployeeManagementDTO
	{
		public int EmployeeId { get; set; }
		public string Name { get; set; } = null!;
		public string? Gender { get; set; }
		public string? IdentityId { get; set; }
		public DateTime? Birthday { get; set; }
		public string Phone { get; set; } = null!;
		public string? Address { get; set; }
		public string Email { get; set; } = null!;
		public string? Education { get; set; }
	}
}

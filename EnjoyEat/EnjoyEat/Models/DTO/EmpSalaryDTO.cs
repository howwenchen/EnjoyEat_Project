namespace EnjoyEat.Models.DTO
{
	public class EmpSalaryDTO
	{
		public int EmployeeId { get; set; }
		public int? BasicSalary { get; set; }
		public int? Bonus { get; set; }
		public string? Performance { get; set; }
		public int TotalSalary { get; set; }
	}
}

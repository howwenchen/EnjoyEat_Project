using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EnjoyEat.Models.DTO.EmployeeManagementDTO;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
	[Route("api/EmployeeManagementApi/[action]")]
	[ApiController]
	public class EmployeeManagementApiController : ControllerBase
	{
		private readonly db_a989fe_thm101team6Context _context;
		public EmployeeManagementApiController(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IEnumerable<EmployeeManagementDTO.Employee>> GetAll()
		{
			var emp = await _context.Employees.Select(emp =>
			new EmployeeManagementDTO.Employee
			{
				EmployeeId = emp.EmployeeId,
				Name = emp.Name,
				Gender = emp.Gender,
				Phone = emp.Phone,
				Email = emp.Email,
				Birthday = emp.Birthday,
				Education = emp.Education,
			}).ToListAsync();
			return emp;
		}

		//篩選功能
		[HttpPost]
		public async Task<IEnumerable<EmployeeManagementDTO.Employee>> FilterEmployees(
			[FromBody] EmployeeManagementDTO.Employee empDTO)
		{
			return _context.Employees.Where(emp =>
					 emp.EmployeeId == empDTO.EmployeeId ||
					 emp.Name.Contains(empDTO.Name) ||
					 emp.Phone.Contains(empDTO.Phone) ||
					 emp.Email.Contains(empDTO.Email)).Select(emp => new EmployeeManagementDTO.Employee
					 {
						 EmployeeId = emp.EmployeeId,
						 Name = emp.Name,
						 Phone = emp.Phone,
						 Email = emp.Email,
					 });
		}

	}
}
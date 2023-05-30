using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
	[Route("api/EmployeeManagementApi/[controller]")]
	[ApiController]
	public class EmployeeManagementApiController : ControllerBase
	{
		private readonly db_a989fe_thm101team6Context _context;
		public EmployeeManagementApiController(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}

		[HttpGet]
		public object All()
		{
			return _context.Employees.Select(emp => new
			{
				emp = new
				{
					EmployeeId = emp.EmployeeId,
					Name = emp.Name,
					Gender = emp.Gender,
					Birthday = emp.Birthday,
					Phone = emp.Phone,
					Email = emp.Email,
				}
			}).ToList();
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
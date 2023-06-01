using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EnjoyEat.Models.DTO.EmployeeManagementDTO;
using Employee = EnjoyEat.Models.Employee;

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
				Account = emp.Account,
				Gender = emp.Gender,
				Phone = emp.Phone,
				Email = emp.Email,
				Birthday = emp.Birthday,
				Education = emp.Education,
			}).ToListAsync();
			return emp;
		}

		//編輯功能
		[HttpPost]
		public ApiResultDto Edit([FromBody] EmployeeManagementDTO.Employee empDTO)
		{
			try
			{
				var editEmp = _context.Employees.FirstOrDefault(e => e.EmployeeId == empDTO.EmployeeId);
				if (editEmp == null) return new ApiResultDto() { Status = false, Message = "修改失敗" };

				editEmp.Name = empDTO.Name;
				editEmp.Account = empDTO.Account;
				editEmp.Password = empDTO.Password;
				editEmp.Gender = empDTO.Gender;
				editEmp.Phone = empDTO.Phone;
				editEmp.Email = empDTO.Email;

				_context.SaveChanges();
				return new ApiResultDto() { Status = true, Message = "修改成功" };
			}
			catch (Exception)
			{
				return new ApiResultDto() { Status = true, Message = "修改失敗" };
			}

		}

		//新增員工
		[HttpPost]
		public async Task<string> CreateEmp([FromBody] EmployeeManagementDTO.Employee empDTO)
		{
			try
			{
				Employee NewEmp = new Employee
				{
					Name = empDTO.Name,
					Account = empDTO.Account,
					Password = empDTO.Password,
					Gender = empDTO.Gender,
					Birthday = empDTO.Birthday,
					Phone = empDTO.Phone,
					Email = empDTO.Email
				};
				_context.Employees.Add(NewEmp);
				await _context.SaveChangesAsync();
				return "新增成功";
			}
			catch (Exception)
			{
				return "新增失敗";
			}

		}

		//刪除員工
		[HttpPost]
		public bool DeleteEmp([FromBody] EmployeeManagementDTO.Employee empDTO)
		{
			try
			{
				var emp = _context.Employees.FirstOrDefault(e => e.EmployeeId == empDTO.EmployeeId);
				if (emp == null) return false;
				_context.Employees.Remove(emp);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}


	}
}
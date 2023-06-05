using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Authorization;
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

		//篩選
		[HttpPost]
		public async Task<IEnumerable<EmployeeManagementDTO>> FilterEmployees([FromBody] EmployeeManagementDTO empDTO)
		{
			return _context.Employees.Where(emp =>
				emp.EmployeeId == empDTO.EmployeeId ||
				emp.Name.Contains(empDTO.Name) ||
				emp.Account.Contains(empDTO.Account) ||
				emp.Email.Contains(empDTO.Email) ||
				emp.Phone.Contains(empDTO.Phone) ||
				emp.Role.Contains(empDTO.Role)).Select(emp => new EmployeeManagementDTO
				{
					EmployeeId = emp.EmployeeId,
					Name = emp.Name,
					Account = emp.Account,
					Email = emp.Email,
					Phone = emp.Phone,
					Role = emp.Role,
				});
		}

		[HttpGet]
		public async Task<IEnumerable<EmployeeManagementDTO>> GetAll()
		{
			var emp = await _context.Employees.Select(emp =>
			new EmployeeManagementDTO
			{
				EmployeeId = emp.EmployeeId,
				Role = emp.Role,
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
		[Authorize(Roles = "manager")]
		public ApiResultDto Edit([FromBody] EmployeeManagementDTO empDTO)
		{
			try
			{
				var editEmp = _context.Employees.FirstOrDefault(e => e.EmployeeId == empDTO.EmployeeId);
				if (editEmp == null) return new ApiResultDto() { Status = false, Message = "修改失敗" };

				editEmp.Name = empDTO.Name;
				editEmp.Gender = empDTO.Gender;
				editEmp.Phone = empDTO.Phone;
				editEmp.Email = empDTO.Email;
				editEmp.Account = empDTO.Account;


				_context.SaveChanges();
				return new ApiResultDto() { Status = true, Message = "修改成功" };
			}
			catch (Exception)
			{
				return new ApiResultDto() { Status = false, Message = "修改失敗" };
			}

		}

		//新增員工
		[Authorize(Roles = "manager")]
		[HttpPost]
		public async Task<string> CreateEmp([FromBody] EmployeeManagementDTO empDTO)
		{
			try
			{
				Employee NewEmp = new Employee
				{
					Name = empDTO.Name,
					Gender = empDTO.Gender,
					Phone = empDTO.Phone,
					Email = empDTO.Email,
					Account = empDTO.Account,
					Password = empDTO.Password,
					Role = empDTO.Role,
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
		[Authorize(Roles = "manager")]
		[HttpPost]
		public bool DeleteEmp([FromBody] EmployeeManagementDTO empDTO)
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
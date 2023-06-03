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
		//編輯頁面
		[HttpPost]
		public IActionResult EditShow([FromBody] EmployeeManagementDTO empDTO) 
		{
			var getRole = HttpContext.Session.GetString("Role");

			if (getRole != "master") return Json(new returnObj(NoAuthorize));

			return Json(new returnObj(OK));

		}

		private OkResult NoAuthorize()
		{
			throw new NotImplementedException();
		}

		private OkResult OK()
		{
			throw new NotImplementedException();
		}

		private IActionResult Json(returnObj returnObj)
		{
			throw new NotImplementedException();
		}

		//編輯功能
		[HttpPost]
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
				editEmp.Password = empDTO.Password;


				_context.SaveChanges();
				return new ApiResultDto() { Status = true, Message = "修改成功" };
			}
			catch (Exception)
			{
				return new ApiResultDto() { Status = false, Message = "修改失敗" };
			}

		}

		//新增員工
		//[Authorize(Roles = "manager")]
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
		//[Authorize(Roles = "manager")]
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
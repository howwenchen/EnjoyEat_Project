using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;
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
		public ApiResultDto GetRoleFromCookie()
		{
			// 檢查是否有角色的 Claim
			if (User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "manager"))
			{
				return new ApiResultDto() { Status = true, Message = "成功" };
			}
			return new ApiResultDto() { Status = false, Message = "失敗" };
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

		//編輯員工薪資
		[Authorize(Roles = "manager")]
		[HttpPost]
		public ApiResultDto EditSalary([FromBody] EmployeeManagementDTO empDTO)
		{
			try
			{
				var emp = _context.Employees.Include(x => x.EmployeesSalary).FirstOrDefault(e => e.EmployeeId == empDTO.EmployeeId);
				if (emp == null) return new ApiResultDto() { Status = false, Message = "修改失敗" };

				if(emp.EmployeesSalary == null)
				{
					var empsla = new EmployeesSalary
					{
						EmployeeId = empDTO.EmployeeId,
						BasicSalary = empDTO.BasicSalary,
						Bonus = empDTO.Bonus,
						TotalSalary = empDTO.TotalSalary,
						Performance = empDTO.Performance
					};

					_context.EmployeesSalaries.Add(empsla);
				}
				else
				{
					emp.EmployeesSalary.BasicSalary = empDTO.BasicSalary;
					emp.EmployeesSalary.Bonus = empDTO.Bonus;
					emp.EmployeesSalary.TotalSalary = empDTO.TotalSalary;
					emp.EmployeesSalary.Performance = empDTO.Performance;
				}
				
				_context.SaveChanges();
				return new ApiResultDto() { Status = true, Message = "修改成功" };
			}

			catch
			{
				return new ApiResultDto() { Status = false, Message = "修改失敗" };
			}

		}

		//新增員工
		[Authorize(Roles = "manager")]
		[HttpPost]
		public ApiResultDto CreateEmp([FromBody] EmployeeManagementDTO empDTO)
		{
			var emp = _context.Employees.FirstOrDefault(e => e.Account.Equals(empDTO.Account) && e.Password.Equals(empDTO.Password));
			if (emp != null)
			{
				return new ApiResultDto() { Status = true, Message = "新增失敗" };
			}
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

				_context.SaveChanges();
				return new ApiResultDto() { Status = true, Message = "新增成功" };
			}
			catch (Exception)
			{
				return new ApiResultDto() { Status = true, Message = "新增失敗" };
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
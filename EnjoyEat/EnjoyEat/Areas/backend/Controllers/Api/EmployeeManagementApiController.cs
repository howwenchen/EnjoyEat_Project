using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Areas.OrderForHere.Models.ViewModels;
using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnjoyEat.Areas.backend.Controllers.API
{
	[Area("backend")]
	[Route("api/EmployeeManagementApi/[controller]")]
	[ApiController]
	public class EmployeeManagementApiController : ControllerBase
	{
		private readonly db_a989fe_thm101team6Context _context;
		public EmployeeManagementApiController(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}
		// GET: api/<EmployeeManagementApiController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> Employee()
		{
			try
			{
				var employee = await _context.Employees.AsNoTracking()
					.Select(e => new EmployeeManagementDTO.Employee
					{
						EmployeeId = e.EmployeeId,
						Name = e.Name,
						Gender = e.Gender,
						Birthday = e.Birthday,
						Phone = e.Phone,
						Email = e.Email,
					}).ToListAsync();

				return Ok(employee);
			}
			catch (Exception ex)
			{
				// 確認錯誤訊息
				Console.WriteLine(ex.ToString());
				return StatusCode(500, "Internal server error");
			}
		}


		// GET api/<EmployeeManagementApiController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<EmployeeManagementApiController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<EmployeeManagementApiController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<EmployeeManagementApiController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}

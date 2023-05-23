using EnjoyEat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeManagementAPIController : ControllerBase
	{
		private readonly db_a989fe_thm101team6Context _context;
		public EmployeeManagementAPIController(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}

		//public object All()
		//{
		//	//return _context.Employees.Select(emp =>
		//	//{
		//	//	emp = new
		//	//	{

		//	//	}

		//	//});
		
		//}

	}
}

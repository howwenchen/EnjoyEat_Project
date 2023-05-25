using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers
{
	public class EmployeeManagementController : Controller
	{
		[Area("backend")]
		public IActionResult Index()
		{
			return View();
		}
	}
}

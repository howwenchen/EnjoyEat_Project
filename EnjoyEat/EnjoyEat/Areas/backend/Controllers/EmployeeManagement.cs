using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers
{
	[Area("backend")]
	public class EmployeeManagement : Controller
    {

		public IActionResult Index()
        {
            return View();
        }
    }
}

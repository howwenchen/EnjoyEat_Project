using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers
{
	public class BackendHomeController : Controller
	{
		[Area("backend")]
		public IActionResult Index()
		{
			return View();
		}
	}
}

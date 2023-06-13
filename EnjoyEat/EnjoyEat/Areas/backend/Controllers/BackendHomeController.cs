using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers
{
	public class BackendHomeController : Controller
	{
		[Area("backend")]
        [Authorize(Roles = "manager,staff")]
        public IActionResult Index()
		{
			return View();
		}
	}
}

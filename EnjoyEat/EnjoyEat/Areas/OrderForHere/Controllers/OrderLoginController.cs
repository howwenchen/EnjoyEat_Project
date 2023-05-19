using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
    [Area("OrderForHere")]
    public class OrderLoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

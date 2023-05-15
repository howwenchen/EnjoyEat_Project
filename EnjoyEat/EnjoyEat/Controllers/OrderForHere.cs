using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
	public class OrderForHere : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

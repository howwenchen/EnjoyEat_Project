using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
	public class ShoppingCartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
    [Area("OrderForHere")]
    public class OrderForHere : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

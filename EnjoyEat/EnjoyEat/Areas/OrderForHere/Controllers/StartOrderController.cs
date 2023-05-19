using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
    [Area("OrderForHere")]
    public class StartOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

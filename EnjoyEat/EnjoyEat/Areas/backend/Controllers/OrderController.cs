using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers
{
    [Area("backend")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

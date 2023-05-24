using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class OrderTogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}

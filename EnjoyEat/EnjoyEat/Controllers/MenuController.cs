using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

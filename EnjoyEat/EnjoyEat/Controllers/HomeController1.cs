using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}

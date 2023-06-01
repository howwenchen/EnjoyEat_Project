using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

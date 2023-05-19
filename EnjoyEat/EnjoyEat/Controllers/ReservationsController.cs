using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class ReservationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

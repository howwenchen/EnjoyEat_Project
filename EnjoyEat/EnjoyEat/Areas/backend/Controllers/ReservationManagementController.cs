using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers
{
    [Area("backend")]
    public class ReservationManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EnjoyEat.Areas.backend.Controllers
{
    [Area("backend")]
    [Authorize(Roles = "manager,staff")]
    public class ReservationManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

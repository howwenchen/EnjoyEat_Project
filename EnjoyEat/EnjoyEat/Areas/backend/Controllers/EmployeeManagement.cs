using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers
{
    public class EmployeeManagement : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

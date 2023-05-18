using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class MemberManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

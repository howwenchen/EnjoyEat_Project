using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
    public class MemberManagementController : Controller
    {
        [Area("backend")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
    public class MemberManagementController : Controller
    {
        [Area("backend")]
        [Authorize(Roles = "manager,staff")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

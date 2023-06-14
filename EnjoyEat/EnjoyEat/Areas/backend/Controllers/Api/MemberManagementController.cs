using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
    public class MemberManagementController : Controller
	{
        [Area("backend")]
<<<<<<< HEAD
		[Authorize(Roles = "manager,staff")]
		public IActionResult Index()
=======
        [Authorize(Roles = "manager,staff")]
        public IActionResult Index()
>>>>>>> d432b430a87a6667fe599a58e1682bd9314c2fc5
        {
            return View();
        }
    }
}

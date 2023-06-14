using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnjoyEat.Areas.backend.Controllers
{
    [Area("backend")]
<<<<<<< HEAD
	[Authorize(Roles = "manager,staff")]
	public class ProductController : Controller
=======
    [Authorize(Roles = "manager,staff")]
    public class ProductController : Controller
>>>>>>> d432b430a87a6667fe599a58e1682bd9314c2fc5
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

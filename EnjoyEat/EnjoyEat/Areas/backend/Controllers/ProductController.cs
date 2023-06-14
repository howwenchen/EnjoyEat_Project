using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnjoyEat.Areas.backend.Controllers
{
    [Area("backend")]
	[Authorize(Roles = "manager,staff")]
	public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

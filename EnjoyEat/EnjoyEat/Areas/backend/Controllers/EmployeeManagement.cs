using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EnjoyEat.Areas.backend.Controllers
{
	[Area("backend")]
    [Authorize(Roles = "manager,staff")]
    public class EmployeeManagement : Controller
    {
		private readonly db_a989fe_thm101team6Context _context;
		public EmployeeManagement(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            return View();
        }
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}

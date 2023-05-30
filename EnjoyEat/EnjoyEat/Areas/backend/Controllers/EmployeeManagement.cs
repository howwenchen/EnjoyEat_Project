using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.backend.Controllers
{
	[Area("backend")]
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
	}
}

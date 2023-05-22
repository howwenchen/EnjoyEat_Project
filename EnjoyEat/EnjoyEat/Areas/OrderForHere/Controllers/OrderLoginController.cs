using EnjoyEat.Areas.OrderForHere.Models.ViewModel;
using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
	[Area("OrderForHere")]
	public class OrderLoginController : Controller
	{
		private readonly db_a989fe_thm101team6Context _context;

		public  OrderLoginController(db_a989fe_thm101team6Context context)
		{
			this._context = context;  
		}
			
        public IActionResult Index()
		{
			return View();
		}
	}
}

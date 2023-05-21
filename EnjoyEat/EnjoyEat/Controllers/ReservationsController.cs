using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly db_a989fe_thm101team6Context _db;
        public ReservationsController(db_a989fe_thm101team6Context projectContext)
        {
            this._db = projectContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult booking()
        {
            return View();
        }

    }
}

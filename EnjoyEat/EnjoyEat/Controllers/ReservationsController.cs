using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult success()
        {
            return View();
        }
    }
}

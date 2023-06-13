using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static NuGet.Packaging.PackagingConstants;

namespace EnjoyEat.Controllers
{
    public class MemberManagementController : Controller
    {
        private readonly db_a989fe_thm101team6Context _db;
        public MemberManagementController(db_a989fe_thm101team6Context db)
        {
            this._db = db;
        }

		[Authorize(Roles = "User")]
		public IActionResult Index()
		{
            return View();

        }

		[Authorize(Roles = "User")]
		public IActionResult EditPassword()
        {
            return View();
        }



    }
}

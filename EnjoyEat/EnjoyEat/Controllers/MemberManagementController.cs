using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
            public IActionResult Index()
        {
                return View();
            
        }

        public IActionResult EditPassword()
        { 
                return View();
        }

       
        
    }
}

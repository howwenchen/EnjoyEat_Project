using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;

namespace EnjoyEat.Controllers
{
    public class CheckoutPageController : Controller
    {
        private readonly db_a989fe_thm101team6Context _context;

        public CheckoutPageController(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}

        

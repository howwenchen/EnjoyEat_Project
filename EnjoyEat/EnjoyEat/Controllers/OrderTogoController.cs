using EnjoyEat.Models.ViewModel;
using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Controllers
{
    public class OrderTogoController : Controller
    {
        private readonly db_a989fe_thm101team6Context _context;

        public OrderTogoController(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Order()
        {
            var dbContext = _context.Products.Include(t => t.SubCategory);
            var temp = dbContext.Select(pro => new MenuViewModel
            {
                ProductId = pro.ProductId,
                MealImg = pro.MealImg,
                ProductName = pro.ProductName,
                UnitPrice = pro.UnitPrice,
                Description = pro.Description,
                CategoryName = pro.SubCategory.Category.CategoryName
            });

            return View(temp);
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}


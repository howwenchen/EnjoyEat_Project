using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnjoyEat.Controllers
{
    public class MenuController : Controller
    {
        private readonly db_a989fe_thm101team6Context _context;

        public MenuController(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {  
            return View();

            //var temp = _cnntext.Product.Select(opt => new MenuViewModel()
            //{
            //    ProductId = _context.Products.First().ProductId,
            //    ProductName = _context.Products.First().ProductName,
            //    UnitPrice = _context.Products.First().UnitPrice,
            //    CategoryId = _context.Category.First().CategoryId,
            //    CategoryMaame = _context.Category.Category
            //});
            //return View(temp);
        }
    }
}
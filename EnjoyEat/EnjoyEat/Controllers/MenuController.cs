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

        [HttpGet]
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

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var emps = new List<MenuViewModel> {
        //        new MenuViewModel {ProductName="rico", UnitPrice=35},
        //        new MenuViewModel {ProductName="sherry",UnitPrice=30},
        //        new MenuViewModel {ProductName="fifi",UnitPrice=4 },
        //        new MenuViewModel {ProductName="rico2",UnitPrice=34 },
        //        new MenuViewModel {ProductName="sherry2",UnitPrice=29 },
        //        new MenuViewModel {ProductName="fifi2",UnitPrice=3 },
        //        new MenuViewModel {ProductName="rico3",UnitPrice=33 },
        //        new MenuViewModel {ProductName="sherry3",UnitPrice=28 },
        //        new MenuViewModel {ProductName="fifi3",UnitPrice=2},
        //        new MenuViewModel {ProductName="Vue",UnitPrice=1}
        //    };
        //    var serverModel = JsonConvert.SerializeObject(emps);
        //    return View(new { serverModel });
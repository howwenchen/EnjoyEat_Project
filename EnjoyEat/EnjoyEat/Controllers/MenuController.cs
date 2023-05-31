using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Controllers;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;

namespace EnjoyEat.Controllers
{
    public class MenuController : Controller
    {
        //private readonly db_a989fe_thm101team6Context _context;

        //public MenuController(db_a989fe_thm101team6Context context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        public IActionResult Index()
        {
            //var dbContext = _context.Products.Include(t => t.SubCategory);
            //var temp = dbContext.Select(pro => new MenuViewModel
            //{
            //    ProductId = pro.ProductId,
            //    MealImg = pro.MealImg,
            //    ProductName = pro.ProductName,
            //    UnitPrice = pro.UnitPrice,
            //    Description = pro.Description,
            //    CategoryName = pro.SubCategory.Category.CategoryName
            //});
            //var categories = _context.SubCategories.Include(x => x.Products)
            //    .Select(c => new SubCategoriesViewModel
            //    {
            //        CategoryId = c.CategoryId,
            //        SubCategoryId = c.SubCategoryId,
            //        SubCategoriesName = c.SubCategoriesName,
            //        Products = (ICollection<Product>)c.Products.Select(p => new MenuViewModel
            //        {
            //            ProductId = p.ProductId,
            //            ProductName = p.ProductName,
            //            UnitPrice = p.UnitPrice,
            //            Costs = p.Costs,
            //            Stock = p.Stock,
            //            Description = p.Description,
            //        })
            //    });
            return View();
        }
    }
}
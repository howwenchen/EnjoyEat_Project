using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Areas.OrderForHere.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
    [Area("OrderForHere")]
    public class StartOrderController : Controller
    {
        private readonly SQL8005site4nownetContext _context;
        public StartOrderController(SQL8005site4nownetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FixedIndex()
        {
            return View();
        }

        // GET: OrderForHere/StartOrder/SubCategories
        [HttpGet("OrderForHere/StartOrder/SubCategories")]
        public async Task<ActionResult<IEnumerable<SubCategories>>> SubCategories()
        {
            try
            {
                var subCategories = await _context.SubCategories.AsNoTracking()
                    .Select(i=>new StartOrderViewModel.SubCategories
                {
                            SubCategoryId = i.SubCategoryId,
                            SubCategoriesName = i.SubCategoriesName,
                            CategoryId = i.CategoryId
                }
                    ).ToListAsync();
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                // 確認錯誤訊息
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }


        // GET: OrderForHere/StartOrder/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> Products()
        {
            try
            {
                var products = await _context.Products.AsNoTracking()
                    .Select(i=>new StartOrderViewModel.Products
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        SubCategoryId = i.SubCategoryId,
                        UnitPrice = i.UnitPrice,
                        MealImg = i.MealImg,
                    })
                    .ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }

        }
    }
}


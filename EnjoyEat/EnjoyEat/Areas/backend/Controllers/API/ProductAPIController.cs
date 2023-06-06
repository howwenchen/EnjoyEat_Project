using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.backend.Controllers.API
{
    [Route("api/ProductAPI/[action]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context _context;
        public ProductAPIController(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuViewModel>>> GetProduct()
        {
            try
            {
                var dbContext = _context.Products.Include(t => t.SubCategory);
                var product = await dbContext.AsNoTracking().Select(pro => new MenuViewModel
                {
                    ProductId = pro.ProductId,
                    MealImg = pro.MealImg,
                    ProductName = pro.ProductName,
                    Costs = pro.Costs,
                    UnitPrice = pro.UnitPrice,
                    Stock = pro.Stock,
                    Description = pro.Description,
                    Recipe = pro.Recipe,
                    CategoryName = pro.SubCategory.Category.CategoryName,
                    SubCategoriesName = pro.SubCategory.SubCategoriesName,
                }).ToListAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategory()
        {
            try
            {
                var category = await _context.Categories.AsNoTracking().Select(cat => new CategoryViewModel
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                }).ToListAsync();
                return Ok(category);
            }
            catch (Exception ex)
            {
                // 確認錯誤訊息
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }
        public async Task<ActionResult<IEnumerable<SubCategoryViewModel>>> GetSubCategory()
        {
            try
            {
                var subCategory = await _context.SubCategories.AsNoTracking().Select(cat => new SubCategoryViewModel
                {
                    CategoryId = cat.CategoryId,
                    SubCategoryId = cat.SubCategoryId,
                    SubCategoriesName = cat.SubCategoriesName,
                }).ToListAsync();
                return Ok(subCategory);
            }
            catch (Exception ex)
            {
                // 確認錯誤訊息
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<string> CreateProduct([FromBody] MenuViewModel meal)
        {
            try
            {
                Product pro = new Product
                {
                    ProductId = meal.ProductId,
                    MealImg = meal.MealImg,
                    ProductName = meal.ProductName,
                    Costs = meal.Costs,
                    UnitPrice = meal.UnitPrice,
                    Stock = meal.Stock,
                    SubCategoryId = meal.SubCategoryId,
                    Description = meal.Description,
                    Recipe = meal.Recipe,
                };
                _context.Products.Add(pro);
                await _context.SaveChangesAsync();
                return "新增成功";
            }
            catch (Exception)
            {
                return "新增失敗";
            }
        }
    }
}

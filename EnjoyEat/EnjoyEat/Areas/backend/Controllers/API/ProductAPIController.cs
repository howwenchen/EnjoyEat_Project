using EnjoyEat.Areas.backend.Controllers.Api;
using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
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

        // 取得餐點
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct()
        {
            try
            {
                var dbContext = _context.Products.Include(t => t.SubCategory);
                var product = await dbContext.AsNoTracking().Select(pro => new ProductDTO
                {
                    ProductId = pro.ProductId,
                    MealImg = pro.MealImg,
                    ProductName = pro.ProductName,
                    Costs = pro.Costs,
                    UnitPrice = pro.UnitPrice,
                    Stock = pro.Stock,
                    Description = pro.Description,
                    Recipe = pro.Recipe,
                    SubCategoryId = pro.SubCategoryId,
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            try
            {
                var category = await _context.Categories.AsNoTracking().Select(cat => new CategoryDTO
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
        public async Task<ActionResult<IEnumerable<SubCategoryDTO>>> GetSubCategory()
        {
            try
            {
                var subCategory = await _context.SubCategories.AsNoTracking().Select(cat => new SubCategoryDTO
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

        // 新增圖片
        [HttpPost]
        public async Task<ActionResult> UploadImage([FromForm]IFormFile image, [FromForm] int productId)
        {
            try
            {
                //using (var stream = System.IO.File.Create($@"C:\Users\Tibame_T14\Desktop\EnjoyEat\EnjoyEat\EnjoyEat\wwwroot\img\Food\{image.FileName}"))
                //{
                //    await image.CopyToAsync(stream);
                //}
                //return Ok(true);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine("wwwroot","img", "Food", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var productData = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
                //if (productData == null) {
                //    return BadRequest("此商品不存在");
                //}
                productData.MealImg = $"/img/Food/{fileName}";
                await _context.SaveChangesAsync();

                return Ok(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }

        // 新增餐點
        [HttpPost]
        public async Task<ApiResultDto> CreateProduct([FromBody] ProductDTO meal)
        {
            try
            {
                //// 新增餐點前先處理圖片
                //string imagePath = await UploadImage(meal.MealImg);
                //meal.MealImg = imagePath;

                // 檢查是否存在相同的 ProductId
                bool isExistingProductId = await _context.Products.AnyAsync(p => p.ProductId == meal.ProductId);
                if (isExistingProductId)
                {
                    return new ApiResultDto() { Status = false, Message = "產品編號已存在，請重新輸入。" };
                }

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
                return new ApiResultDto() { Status = true, Message = "新增成功" };
            }
            catch (Exception)
            {
                return new ApiResultDto() { Status = false, Message = "新增失敗，請檢查必填欄位" };
            }
        }

        // 刪除餐點
        [HttpPost]
        public bool DeleteProduct([FromBody] ProductDTO meal)
        {
            try
            {
                var pro = _context.Products.FirstOrDefault(p => p.ProductId == meal.ProductId);
                if (pro == null) return false;
                _context.Products.Remove(pro);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 更新餐點
        [HttpPost]
        public async Task<ApiResultDto> EditProduct([FromBody] ProductDTO meal)
        {
            try
            {
                var pro = _context.Products.FirstOrDefault(p => p.ProductId == meal.ProductId);
                //var file=new FileStream(pro.MealImg,FileAccess.Read)
                pro.MealImg = meal.MealImg;
                pro.ProductName = meal.ProductName;
                pro.Costs = meal.Costs;
                pro.UnitPrice = meal.UnitPrice;
                pro.SubCategoryId = meal.SubCategoryId;
                pro.Description = meal.Description;
                pro.Recipe = meal.Recipe;
                pro.Stock = meal.Stock;

                if (pro.ProductName == "") 
                    return new ApiResultDto() { Status = false, Message = "新增失敗，請檢查必填欄位" };
                await _context.SaveChangesAsync();
                return new ApiResultDto() { Status = true, Message = "修改成功" };
            }
            catch (Exception)
            {
                return new ApiResultDto() { Status = false, Message = "修改失敗" };
            }
        }
    }
}

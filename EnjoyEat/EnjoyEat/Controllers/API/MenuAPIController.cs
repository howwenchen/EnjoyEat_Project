﻿using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnjoyEat.Controllers.API
{
    [Route("api/menu/[action]")]
    [ApiController]
    public class MenuAPIController : ControllerBase
    {
        private db_a989fe_thm101team6Context _context;
        private readonly ILogger<MenuAPIController> _logger;

        public MenuAPIController(db_a989fe_thm101team6Context context, ILogger<MenuAPIController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetMenu()
        {
            var dbContext = _context.Products.Include(t => t.SubCategory);
            var temp = dbContext.Select(pro => new MenuViewModel.Products
            {
                ProductId = pro.ProductId,
                MealImg = pro.MealImg,
                ProductName = pro.ProductName,
                UnitPrice = pro.UnitPrice,
                Description = pro.Description,
                CategoryName = pro.SubCategory.Category.CategoryName,
                SubCategoryId = pro.SubCategoryId,
            });
            return Ok(temp);
        }

        [HttpGet("/api/Menu/CategoriesWithSubs")]
        public async Task<ActionResult<IEnumerable<Category>>> CategoriesWithSubs()
        {
            try
            {
                var categories = await _context.Categories.AsNoTracking().ToListAsync();
                var subcategories = await _context.SubCategories.AsNoTracking().ToListAsync();

                foreach (var category in categories)
                {
                    category.SubCategories = subcategories
                        .Where(i => i.CategoryId == category.CategoryId)
                        .ToList();
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // 確認錯誤訊息
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("/api/Menu/Products")]
        public async Task<ActionResult<IEnumerable<Product>>> Products()
        {
            try
            {
                var products = await _context.Products.AsNoTracking()
                    .Select(i => new MenuViewModel.Products
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

        [HttpPost("/api/Menu/CreateOrder")]
        public IActionResult CreateOrder([FromBody] CartViewModel cartViewModel)
        {
            try
            {
                var cart = new Cart
                {
                    MemberId = cartViewModel.MemberId,
                };

                foreach (var item in cartViewModel.Items)
                {
                    var cartItem = new CartItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };
                    cart.CartItems.Add(cartItem);
                }

                _context.Carts.Add(cart);
                _context.SaveChanges();

                return Ok(cart.CartId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("/api/Menu/GetCart")]
        public async Task<ActionResult<CartViewModel>> GetCart()
        {
            try
            {
                var memberId = HttpContext.Session.GetInt32("MemberId");
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.MemberId == memberId);

                if (cart == null)
                {
                    return NotFound("找不到此購物車");
                }

                var cartItemViewModels = cart.CartItems.Select(item => new CartItemViewModel
                {
                    ProductId = (int)item.ProductId,
                    ProductName = item.Product?.ProductName,
                    UnitPrice = item.Product.UnitPrice,
                    Quantity = (int)item.Quantity
                }).ToList();

                var cartViewModel = new CartViewModel
                {
                    CartId = cart.CartId,
                    Items = cartItemViewModels
                };

                return Ok(cartViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得購物車時發生錯誤。");
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        [HttpPost("/api/Menu/AddToCart")]
        public async Task<ActionResult<CartItem>> AddToCart(CartItemViewModel item, int memberId)
        {
            if (item == null)
            {
                return BadRequest("無效的商品。");
            }

            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.MemberId == memberId);

                if (cart == null)
                {
                    return NotFound("找不到此購物車。");
                }

                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                cart.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();

                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "將商品加入購物車時發生錯誤。");
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        [HttpPost("/api/Menu/RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(CartItemViewModel item, int memberId)
        {
            if (item == null)
            {
                return BadRequest("購物車內已沒有品項。");
            }

            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.MemberId == memberId);
                if (cart == null)
                {
                    return NotFound("找不到購物車編號。");
                }

                var existingItem = await _context.CartItems.FirstOrDefaultAsync(c => c.CartId == cart.CartId && c.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity -= item.Quantity;
                    if (existingItem.Quantity <= 0)
                    {
                        _context.CartItems.Remove(existingItem);
                    }
                    await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "從購物車移除商品時發生錯誤。");
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        [HttpPost("/api/Menu/UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] CartViewModel cartViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var cartId = HttpContext.Session.GetInt32("CartId");
                if (cartId == null)
                {
                    return NotFound("找不到購物車編號。");
                }

                var cart = await _context.Carts.FindAsync(cartId);

                if (cart == null)
                {
                    return NotFound("找不到此購物車。");
                }

                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();

                foreach (var item in cartViewModel.Items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);

                    if (product == null)
                    {
                        return NotFound($"Product with ID {item.ProductId} not found.");
                    }

                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };

                    _context.CartItems.Add(cartItem);
                }

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新購物車失敗");
                return StatusCode(500, "內部伺服器錯誤");
            }
        }

        [HttpPost("/api/Menu/CreateCart")]
        public async Task<ActionResult<int>> CreateCart(int memberId)
        {
            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.MemberId == memberId);
                if (cart != null)
                {
                    return Ok(cart.CartId);
                }

                var newCart = new Cart { MemberId = memberId };
                _context.Carts.Add(newCart);
                await _context.SaveChangesAsync();

                return Ok(newCart.CartId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "創建新購物車時發生錯誤。");
                return StatusCode(500, "內部伺服器錯誤");
            }
        }
    }
}

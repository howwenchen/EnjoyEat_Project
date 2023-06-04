using EnjoyEat.Controllers.API;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}


[ApiController]
[Route("api/Order/[Action]")]
public class OrderAPIController : Controller
{
    private db_a989fe_thm101team6Context _context;
    private readonly ILogger<OrderAPIController> _logger;

    public OrderAPIController(db_a989fe_thm101team6Context context, ILogger<OrderAPIController> logger)
    {
        _context = context;
        _logger = logger;
    }


    //會員訪客判斷條件
    private bool IsUserLoggedIn()
    {
        return HttpContext.Session.GetInt32("MemberId") != null;
    }

    //從session取得memberid回傳
    [HttpGet]
    public ActionResult<int> GetMemberId()
    {
        try
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
            {
                return NotFound("找不到會員編號");
            }
            return Ok(memberId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500, "Internal server error");
        }
    }

    //拿取大小分類資料
    [HttpGet]
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

    //拿取產品資料
    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> AddToCart(CartItemViewModel item)
    {
        if (item == null)
        {
            return BadRequest("Invalid item.");
        }

        try
        {
            if (IsUserLoggedIn())
            {
                var memberId = HttpContext.Session.GetInt32("MemberId");
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.MemberId == memberId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        MemberId = memberId.Value,
                    };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }

                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    return BadRequest("Invalid product.");
                }

                var existingItem = cart.CartItems.FirstOrDefault(c => c.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                }
                else
                {
                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                        Product = product
                    };

                    cart.CartItems.Add(cartItem);
                }

                await _context.SaveChangesAsync(); 
                return Ok(cart.CartItems);
            }
            else
            {
                var cart = HttpContext.Session.Get<CartViewModel>("Cart");
                if (cart == null)
                {
                    cart = new CartViewModel();
                }

                var existingItem = cart.Items.FirstOrDefault(c => c.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                }
                else
                {
                    var cartItem = new CartItemViewModel
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                    };

                    cart.Items.Add(cartItem);
                }

                HttpContext.Session.Set("Cart", cart);
                return Ok(cart.Items);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding item to cart.");
            return StatusCode(500, "Internal server error");
        }
    }



    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(CartItemViewModel item)
    {
        if (item == null)
        {
            return BadRequest("購物車內已沒有品項。");
        }

        try
        {
            if (IsUserLoggedIn())
            {
                var memberId = HttpContext.Session.GetInt32("MemberId");
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.MemberId == memberId);

                if (cart == null)
                {
                    return NotFound("找不到此購物車。");
                }

                var existingItem = cart.CartItems.FirstOrDefault(c => c.ProductId == item.ProductId);
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
            else
            {
                var cart = HttpContext.Session.Get<CartViewModel>("Cart");
                if (cart == null)
                {
                    return NotFound("找不到此購物車。");
                }

                var existingItem = cart.Items.FirstOrDefault(c => c.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity -= item.Quantity;
                    if (existingItem.Quantity <= 0)
                    {
                        cart.Items.Remove(existingItem);
                    }
                    HttpContext.Session.Set("Cart", cart);
                }

                return Ok();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing item from cart.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCart([FromBody] CartViewModel cartViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            if (IsUserLoggedIn())
            {
                var memberId = HttpContext.Session.GetInt32("MemberId");
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.MemberId == memberId);

                if (cart == null)
                {
                    return NotFound("找不到此購物車。");
                }

                var newCartItems = new List<CartItem>();

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
                        Quantity = item.Quantity,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                    };

                    newCartItems.Add(cartItem);
                }

                foreach (var newItem in newCartItems)
                {
                    cart.CartItems.Add(newItem);
                }

                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                var cart = new CartViewModel();
                foreach (var item in cartViewModel.Items)
                {
                    var cartItem = new CartItemViewModel
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                    };
                    cart.Items.Add(cartItem);
                }
                HttpContext.Session.Set("Cart", cart);

                return Ok();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cart.");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpGet]
    public IActionResult GetCartOrCreate()
    {
        try
        {
            if (IsUserLoggedIn())
            {
                var memberId = HttpContext.Session.GetInt32("MemberId");
                var cart = _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault(c => c.MemberId == memberId);

                if (cart == null)
                {
                    return CreateCart().Result;
                }

                var cartItemViewModels = cart.CartItems.Select(item => new CartItemViewModel
                {
                    ProductId = item.ProductId,
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
            else
            {
                var cart = HttpContext.Session.Get<CartViewModel>("Cart");
                if (cart == null)
                {
                    return CreateCart().Result;
                }
                return Ok(cart);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cart or creating new cart.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public ActionResult<int> CreateCart()
    {
        try
        {
            if (IsUserLoggedIn())
            {
                var memberId = HttpContext.Session.GetInt32("MemberId");
                var cart = _context.Carts.FirstOrDefault(c => c.MemberId == memberId);

                if (cart != null)
                {
                    return Ok(cart.CartId);
                }

                var newCart = new Cart
                {
                    MemberId = memberId
                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();

                return Ok(newCart.CartId);
            }
            else
            {
                var cart = new CartViewModel();
                HttpContext.Session.Set("Cart", cart);

                return Ok(cart.CartId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new cart.");
            return StatusCode(500, "Internal server error");
        }
    }



    //建立訂單
    [HttpPost]
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
            // Log the exception and return an error response
            return StatusCode(500, "Internal server error");
        }
    }
}

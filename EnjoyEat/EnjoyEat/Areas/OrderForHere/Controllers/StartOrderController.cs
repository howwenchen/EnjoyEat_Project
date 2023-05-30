using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Areas.OrderForHere.Models.ViewModels;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
	[Area("OrderForHere")]
	public class StartOrderController : Controller
	{
		private readonly db_a989fe_thm101team6Context _context;
		public StartOrderController(db_a989fe_thm101team6Context context)
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

		// GET: OrderForHere/StartOrder/CategoriesWithSubs
		[HttpGet("OrderForHere/StartOrder/CategoriesWithSubs")]
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


		// GET: OrderForHere/StartOrder/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Products>>> Products()
		{
			try
			{
				var products = await _context.Products.AsNoTracking()
					.Select(i => new StartOrderViewModel.Products
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

		//[HttpPost("/OrderForHere/StartOrder/CreateOrder")]
		//public IActionResult CreateOrder([FromBody] CartViewModel cartViewModel)
		//{
		//    // 創建一個新的 Cart 實體並將 CartViewModel 的資訊映射過去
		//    var cart = new Cart
		//    {
		//        // 假設 Cart 和 CartViewModel 有相同的屬性
		//        Items = cartViewModel.Items.Select(item => new CartItem
		//        {
		//            ProductId = item.ProductId,
		//            Quantity = item.Quantity
		//        }).ToList()
		//    };

		//    // 創建一個新的Order
		//    var order = new Order();
		//    _context.Orders.Add(order);
		//    _context.SaveChanges();

		//    foreach (var item in cart.Items)
		//    {
		//        // 對於購物車中的每個項目，創建一個新的OrderDetail
		//        var orderDetail = new OrderDetail
		//        {
		//            OrderId = order.OrderId,
		//            ProductId = item.ProductId,
		//            Quantity = item.Quantity
		//        };

		//        // 將OrderDetail添加到數據庫
		//        _context.OrderDetails.Add(orderDetail);
		//    }

		//    // 保存更改
		//    _context.SaveChanges();

		//    return Ok(order.Id);
		//}

	}


}

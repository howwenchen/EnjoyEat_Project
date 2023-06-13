using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using EnjoyEat.Services;
using EnjoyEat.Models.ViewModel.EnjoyEat.Models.ViewModel;
using EnjoyEat.Models.DTO;
using System.Text;
using System.Text.Json;

namespace EnjoyEat.Controllers
{
	[Route("Checkout/{action}/{id?}")]
	public class CheckoutController : Controller
	{
		private readonly db_a989fe_thm101team6Context _context;
		private readonly ILogger<CheckoutController> _logger;
        private readonly IPaymentService _service;


        public CheckoutController(db_a989fe_thm101team6Context context, ILogger<CheckoutController> logger, IPaymentService service)
		{
			_context = context;
			_logger = logger;
            _service = service;
        }

        public IActionResult Index()
		{
			return View();
		}

        [HttpGet]
        public async Task<IActionResult> GetMemberAndOrder()
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            var orderId = await _context.Orders
                .Where(o => o.MemberId == memberId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => o.OrderId)
                .FirstOrDefaultAsync();
            var order = await _context.Orders.FindAsync(orderId);
            var customerCount = order.CustomerCount;
            var tableId = order.TableId;

            if (memberId == null)
            {
                return NotFound("未登入");
            }

            //回傳memberId跟OrderId跟order
            return Json(new { memberId, orderId, customerCount, tableId });
        }


        [HttpGet]
        public async Task<IActionResult> GetOrderDetails([FromQuery] int? OrderId)
        {
            if (OrderId == null)
            {
                return BadRequest("未指定訂單編號");
            }

            var order = await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product) 
                .FirstOrDefaultAsync(o => o.OrderId == OrderId);

            if (order == null)
            {
                return NotFound($"訂單編號 {OrderId} 不存在");
            }

            int totalItems = order.OrderDetails.Sum(od => od.Quantity);

            var OrderItem = new CheckoutViewModel
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                FirstName = order.Member.FirstName,
                LastName = order.Member.LastName,
                OrderDate = order.OrderDate,
                IsTakeway = order.IsTakeway,
                TableId = order.TableId,
                CustomerCount = order.CustomerCount,
                TotalPrice = order.TotalPrice,
                CampaignDiscount = order.CampaignDiscount,
                LevelDiscount = order.LevelDiscount,
                FinalPrice = order.FinalPrice,
                IsSuccess = false, //預設為未成功
                TotalItems = totalItems,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailViewModel
                {
                    OrderDetailId = od.OrderDetailId,
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    ProductName = od.Product.ProductName, // 加入 productName
                    Quantity = od.Quantity,
                    SubtotalPrice = od.SubtotalPrice,
                    MealImg = od.Product.MealImg
                }).ToList()
            };

            return Json(OrderItem);
        }

        [HttpPost]
        public async Task<IActionResult> OnlinePayment([FromBody] CheckoutViewModel data)
        {
            try
            {
                // 檢查訂單 Id 是否為 null
                if (data.OrderId == null)
                {
                    return BadRequest("未提供訂單編號");
                }

                // 從資料庫中根據 orderId 獲取訂單資訊
                var order = await _context.Orders
                                          .Include(o => o.Member)
                                          .Include(o => o.OrderDetails)
                                          .ThenInclude(od => od.Product) 
                                          .FirstOrDefaultAsync(o => o.OrderId == data.OrderId);

                // 確認是否找到對應的 orderId
                if (order == null)
                {
                    return BadRequest("找不到提供的訂單編號");
                }

                // 轉換 Order 到 CheckoutViewModel
                var orderViewModel = new CheckoutViewModel
                {
                    // 指定 ViewModel 的屬性
                    MemberId = order.MemberId,
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    IsTakeway = order.IsTakeway,
                    TotalPrice = order.TotalPrice,
                    CampaignDiscount = order.CampaignDiscount,
                    LevelDiscount = order.LevelDiscount,
                    FinalPrice = order.FinalPrice,
                    IsSuccess = false, // 預設為未成功
                    TotalItems = order.OrderDetails.Sum(od => od.Quantity),
                    //增加memberId查詢到email
                    Email = order.Member.Email,
                    OrderDetails = order.OrderDetails.Select(od => new OrderDetailViewModel
                    {
                        OrderDetailId = od.OrderDetailId,
                        OrderId = od.OrderId,
                        ProductId = od.ProductId,
                        ProductName = od.Product.ProductName, 
                        Quantity = od.Quantity,
                        SubtotalPrice = od.SubtotalPrice,
                        MealImg = od.Product.MealImg 
                    }).ToList()
                };

                // 使用這個訂單進行付款處理
                var requestData = await _service.ProcessPaymentAsync(orderViewModel); 

                return Json(requestData);
            }
            catch (Exception ex)
            {
                // 付款過程發生錯誤，回傳 500 狀態碼
                _logger.LogError(ex, "處理付款過程中出現錯誤");
                return StatusCode(500, "處理付款時出現錯誤。請稍後再試。");
            }
        }
    }
}

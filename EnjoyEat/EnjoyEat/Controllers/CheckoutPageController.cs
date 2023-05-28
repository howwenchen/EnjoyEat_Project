using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using EnjoyEat.Areas.OrderForHere.Models;
using static EnjoyEat.Models.ViewModel.OnlinePaymentViewModel;
using EnjoyEat.Services;

namespace EnjoyEat.Controllers
{
	public class CheckoutPageController : Controller
	{
		private readonly db_a989fe_thm101team6Context _context;

		public CheckoutPageController(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}


		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Route("/CheckoutPage/GetOrder/{OrderId?}")]
		public IActionResult GetOrder(int? orderId)
		{
			if (orderId == null)
			{
				return BadRequest("未指定訂單編號");
			}

			var order = _context.Orders
				.Include(o => o.Member)
				.Include(o => o.OrderDetails)
				.FirstOrDefault(o => o.OrderId == orderId);

			if (order == null)
			{
				return NotFound($"訂單編號 {orderId} 不存在");
			}

			int totalItems = order.OrderDetails.Sum(od => od.Quantity);

			var OrderItem = new CheckoutPageViewModel
			{
				OrderId = order.OrderId,
				MemberId = order.MemberId,
				FirstName = order.Member.FirstName,
				LastName = order.Member.LastName,
				OrderDate = order.OrderDate,
				IsTakeway = order.IsTakeway,
				TableId = order.TableId,
				CustomerCount = (int)order.CustomerCount,
				TotalPrice = order.TotalPrice,
				CampaignDiscount = order.CampaignDiscount,
				LevelDiscount = order.LevelDiscount,
				FinalPrice = order.FinalPrice,
				IsSuccess = order.IsSuccess,
				TotalItems = totalItems,

			};

			return Json(OrderItem);
		}


		[HttpGet]
		[Route("/CheckoutPage/GetLatestOrder/{MemberId?}")]
		public IActionResult GetLatestOrder(int memberId)
		{
			var latestOrder = _context.Orders
									  .Where(o => o.MemberId == memberId)
									  .OrderByDescending(o => o.OrderDate)
									  .FirstOrDefault();

			if (latestOrder == null)
			{
				return NotFound($"No orders found for member id {memberId}");
			}

			return Ok(latestOrder.OrderId);
		}

		[ValidateAntiForgeryToken]
		public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
		{
			SendToNewebPayOut outModel = new SendToNewebPayOut();

			// 藍新金流線上付款

			//交易欄位
			List<KeyValuePair<string, string>> TradeInfo = new List<KeyValuePair<string, string>>();
			// 商店代號
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantID", inModel.MerchantID));
			// 回傳格式
			TradeInfo.Add(new KeyValuePair<string, string>("RespondType", "String"));
			// TimeStamp
			TradeInfo.Add(new KeyValuePair<string, string>("TimeStamp", DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0)).ToUnixTimeSeconds().ToString()));
			// 串接程式版本
			TradeInfo.Add(new KeyValuePair<string, string>("Version", "2.0"));
			// 商店訂單編號
			TradeInfo.Add(new KeyValuePair<string, string>("MerchantOrderNo", inModel.MerchantOrderNo));
			// 訂單金額
			TradeInfo.Add(new KeyValuePair<string, string>("Amt", inModel.Amt));
			// 商品資訊
			TradeInfo.Add(new KeyValuePair<string, string>("ItemDesc", inModel.ItemDesc));
			// 繳費有效期限(適用於非即時交易)
			TradeInfo.Add(new KeyValuePair<string, string>("ExpireDate", inModel.ExpireDate));
			// 支付完成返回商店網址
			TradeInfo.Add(new KeyValuePair<string, string>("ReturnURL", inModel.ReturnURL));
			// 支付通知網址
			TradeInfo.Add(new KeyValuePair<string, string>("NotifyURL", inModel.NotifyURL));
			// 商店取號網址
			TradeInfo.Add(new KeyValuePair<string, string>("CustomerURL", inModel.CustomerURL));
			// 支付取消返回商店網址
			TradeInfo.Add(new KeyValuePair<string, string>("ClientBackURL", inModel.ClientBackURL));
			// 付款人電子信箱
			TradeInfo.Add(new KeyValuePair<string, string>("Email", inModel.Email));
			// 付款人電子信箱 是否開放修改(1=可修改 0=不可修改)
			TradeInfo.Add(new KeyValuePair<string, string>("EmailModify", "0"));

			//信用卡 付款
			if (inModel.ChannelID == "CREDIT")
			{
				TradeInfo.Add(new KeyValuePair<string, string>("CREDIT", "1"));
			}
			//ATM 付款
			if (inModel.ChannelID == "VACC")
			{
				TradeInfo.Add(new KeyValuePair<string, string>("VACC", "1"));
			}
			string TradeInfoParam = string.Join("&", TradeInfo.Select(x => $"{x.Key}={x.Value}"));

			// API 傳送欄位
			// 商店代號
			outModel.MerchantID = inModel.MerchantID;
			// 串接程式版本
			outModel.Version = "2.0";
			//交易資料 AES 加解密
			IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
			string HashKey = Config.GetSection("HashKey").Value;//API 串接金鑰
			string HashIV = Config.GetSection("HashIV").Value;//API 串接密碼
			string TradeInfoEncrypt = AesService.AesEncryptToHex(TradeInfoParam, HashKey, HashIV);
			outModel.TradeInfo = TradeInfoEncrypt;
			//交易資料 SHA256 加密
			outModel.TradeSha = AesService.AddSHA256CheckCode(TradeInfoEncrypt, HashKey, HashIV);

			return Json(outModel);
		}
	}
}


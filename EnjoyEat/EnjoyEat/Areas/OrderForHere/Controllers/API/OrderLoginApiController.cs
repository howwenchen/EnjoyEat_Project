using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using static EnjoyEat.Areas.OrderForHere.Models.ViewModels.StartOrderViewModel;
using static EnjoyEat.Models.DTO.OnlinePaymentDTO;
using System.Text.Json;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace EnjoyEat.Areas.OrderForHere.API
{
	[Route("api/OrderLoginApi/{action}")]
	[ApiController]
	public class OrderLoginApiController : ControllerBase
	{
		private readonly db_a989fe_thm101team6Context _context;

		public OrderLoginApiController(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}

		//[HttpPost]
		//public async Task<string> QuickRegister(QuickRegisterViewModel condition)
		//{
		//	QuickResult result = new QuickResult() { IsSucess = false };
		//	//判斷輸入的電話號碼格式是否正確
		//	string pattern = @"09\d{2}(\d{6}|-\d{3}-\d{3})";
		//	Regex regex = new Regex(pattern);

		//	if (!regex.IsMatch(condition.Phone))
		//	{
		//		return "請輸入有效的手機號碼";
		//	}

		//	// 判斷手機號碼是否有使用過
		//	var alreadyHas = await _context.Members.Where(x => x.Phone == condition.Phone).Select(x => x).ToListAsync();

		//	if (alreadyHas.Count > 0)
		//	{
		//		result.IsSucess = false;
		//		return "手機號碼已使用過";
		//	}

		//	//判斷帳號是否使用過
		//	var accountHas = await _context.MemberLogins.Where(a => a.Account == condition.Account).Select(x => x).ToListAsync();
		//	if (accountHas.Count > 0)
		//	{
		//		result.IsSucess = false;
		//		return "帳號已使用過";
		//	}

		//	Member mbr = new Member
		//	{
		//		MemberId = condition.MemberId,
		//		FirstName = condition.FirstName,
		//		LastName = condition.LastName,
		//		Phone = condition.Phone,
		//	};

		//	_context.Members.Add(mbr);
		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch
		//	{
		//		return "新增會員失敗";
		//	}


		//	//將填入的電話號碼預設為密碼
		//	Member member = _context.Members.Include(x => x.MemberLogin).FirstOrDefault(m => m.Phone == condition.Phone);
		//	{
		//		if (member != null)
		//		{
		//			EnjoyEat.Models.MemberLogin ml = new EnjoyEat.Models.MemberLogin
		//			{
		//				MemberId = member.MemberId,
		//				Account = condition.Account,
		//				Password = member.Phone
		//			};
		//			_context.MemberLogins.Add(ml);
		//			try
		//			{
		//				await _context.SaveChangesAsync();
		//				result.IsSucess = true;
		//				return "新增會員成功";
		//			}
		//			catch
		//			{
		//				return "新增會員失敗";
		//			}
		//		}
		//	}

		//	return "新增會員成功";
		//}

		[HttpGet]
		public short GetMaxGuests(short tableId)
		{
			var table = _context.Tables.FirstOrDefault(t => t.TableId == tableId);
			if (table != null)
			{
				return table.Capacity;
			}
			else
			{
				return 0;
			}
		}


		[HttpPost]
		public IActionResult StartOrder([FromBody] SendOrder request)
		{
			if (!string.IsNullOrEmpty(request.TableId) && !string.IsNullOrEmpty(request.CustomerCount))
			{
				HttpContext.Session.SetInt32("TableId", int.Parse(request.TableId));
				HttpContext.Session.SetInt32("CustomerCount", int.Parse(request.CustomerCount));
				Console.WriteLine("TableId: " + HttpContext.Session.GetString("TableId"));
				Console.WriteLine("CustomerCount: " + HttpContext.Session.GetString("CustomerCount"));

				return Ok("開始點餐");
			}
			return BadRequest("請輸入桌號及人數");
		}


		[HttpGet]
		public UserOrder GetTableInfo()
		{
			string tableNumber = HttpContext.Session.GetString("tableNumber");
			string capacity = HttpContext.Session.GetString("partySize");

			return new UserOrder{ tableNumber= tableNumber, partySize= capacity };
		}
	}
}

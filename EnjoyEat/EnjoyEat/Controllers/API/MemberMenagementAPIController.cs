using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq;

namespace EnjoyEat.Controllers.API
{
	[Route("api/member/[action]")]
	[ApiController]
	public class MemberMenagementAPIController : ControllerBase
	{
		private readonly db_a989fe_thm101team6Context db;
		public MemberMenagementAPIController(db_a989fe_thm101team6Context db)
		{
			this.db = db;
		}

		//抓取會員資料
		[HttpGet]
		public IActionResult GetMember()
		{
			var userId = 20230006;
			var user = db.Members.Include(x => x.Orders).Include(x => x.LevelNameNavigation).FirstOrDefault(x => x.MemberId == userId);
			if (user == null)
			{
				return NotFound();
			}
			var member = new MemberViewModel()
			{
				MemberId = user.MemberId,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Gender = user.Gender,
				Birthday = user.Birthday,
				Address = user.Address,
				Phone = user.Phone,
				LevelName = user.LevelName,
				LevelDiscount = user.LevelDiscount,
				Orders = user.Orders.Select(x => new MemberOrderViewModel
				{
					OrderDate = x.OrderDate,
					OrderId = x.OrderId,
					TableId = x.TableId,
					TotalPrice = x.TotalPrice,
					IsTakeway = x.IsTakeway,
				}).ToList(),
			};
			return Ok(member);
		}


		//取得餐點明細
		[HttpGet]
		public IActionResult GetOrder()
		{
			var userId = 20230006;
			var orders = db.OrderDetails.Include(x => x.Product).Where(o => o.Order.MemberId == userId).Select(od => new MemberOrderDetailViewModel
			{
				OrderId = od.OrderId,
				ProductId = od.ProductId,
				Quantity = od.Quantity,
				UnitPrice = od.UnitPrice,
				SubtotalPrice = od.SubtotalPrice,
				ProductName = od.Product.ProductName,
			}).ToList();

			// 延遲載入 OrderDetails
			//foreach(var order in orders)
			//{
			//    db.Entry(order).Collection(x => x.).Load();
			//}
			return Ok(orders);
		}

		//把等級寫回資料庫
		[HttpPut]
		public async Task<IActionResult> WriteLev([FromBody] MemberViewModel memberViewModel)
		{
			var id = 20230006;
			Member member = await db.Members.FindAsync(id);
			if (member == null)
			{
				return NotFound();
			}
			member.LevelName = memberViewModel.LevelName;
			member.LevelDiscount = memberViewModel.LevelDiscount;

			db.Entry(member).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok();
		}

		//修改會員資料
		[HttpPut]
		public async Task<IActionResult> EditMemberInfo([FromBody] MemberViewModel memberViewModel)
		{
			var id = 20230006;
			Member member = await db.Members.FindAsync(id);
			if (member == null)
			{
				return NotFound();
			}

			member.FirstName = memberViewModel.FirstName;
			member.LastName = memberViewModel.LastName;
			member.Address = memberViewModel.Address;
			member.Email = memberViewModel.Email;
			member.Phone = memberViewModel.Phone;
			member.Birthday = memberViewModel.Birthday;
			member.Gender = memberViewModel.Gender;


			db.Entry(member).State = EntityState.Modified;
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
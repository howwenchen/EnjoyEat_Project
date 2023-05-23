﻿using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

		[HttpPost]
		public async Task<string> QuickRegister(RegisterViewModel condition)
		{
			Result result = new Result() { IsSucess = false };
			//判斷輸入的電話號碼格式是否正確
			string pattern = @"09\d{2}(\d{6}|-\d{3}-\d{3})";
			Regex regex = new Regex(pattern);

			if (!regex.IsMatch(condition.Phone))
			{
				return "請輸入有效的手機號碼";
			}

			// 判斷手機號碼是否有使用過
			var alreadyHas = await _context.Members.Where(x => x.Phone == condition.Phone).Select(x => x).ToListAsync();

			if (alreadyHas.Count > 0)
			{
				result.IsSucess = false;
				return "手機號碼已使用過";
			}

			//判斷帳號是否使用過
			var accountHas = await _context.MemberLogins.Where(a => a.Account == condition.Account).Select(x => x).ToListAsync();
			if (accountHas.Count > 0)
			{
				result.IsSucess = false;
				return "帳號已使用過";
			}

			Member mbr = new Member
			{
				MemberId = condition.MemberId,
				FirstName = condition.FirstName,
				LastName = condition.LastName,
				Phone = condition.Phone,
			};

			_context.Members.Add(mbr);
			try
			{
				await _context.SaveChangesAsync();
				//return "新增會員成功";
			}
			catch
			{
				return "新增會員失敗";
			}


			//將填入的電話號碼預設為密碼
			Member member = _context.Members.Include(x => x.MemberLogin).FirstOrDefault(m => m.Phone == condition.Phone);
			{
				if (member != null)
				{
					EnjoyEat.Models.MemberLogin ml = new EnjoyEat.Models.MemberLogin
					{
						MemberId = member.MemberId,
						Account = condition.Account,
						Password = member.Phone
					};
					_context.MemberLogins.Add(ml);
					try
					{
						await _context.SaveChangesAsync();
						result.IsSucess = true;
					}
					catch
					{
						return "新增會員失敗";
					}
				}
			}

			return "新增會員成功";
		}

		//[HttpGet]
		//public async Task<string> CheckMember(Member mb)
		//{
		//	Result result = new Result() { IsSucess = false };

		//	var member = await _context.Members.FirstOrDefaultAsync(x => x.Phone == mb.Phone);
		//	if (member == null)
		//	{
		//		return "該號碼尚未成為會員!";
		//	}
		//	return "開始點餐";
		//}
	}
}
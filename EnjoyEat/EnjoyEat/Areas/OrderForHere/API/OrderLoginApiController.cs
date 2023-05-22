using EnjoyEat.Areas.OrderForHere.Models.ViewModel;
using EnjoyEat.Models;
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
        public async Task<Result> QuickRegister(RegisterViewModel condition)
        {
			Result result = new Result() { IsSucess = false };

			string pattern = @"/(\d{2,3}-?|\(\d{2,3}\))\d{3,4}-?\d{4}|09\d{2}(\d{6}|-\d{3}-\d{3})/g";
			Regex regex = new Regex(pattern);

			if (!regex.IsMatch(condition.Phone))
			{
				result.ReturnMessage = "格式錯誤";
				return result;
			}

			var alreadyHas = await _context.Members.Where(x => x.Phone == condition.Phone).Select(x => x).ToListAsync();

			if(alreadyHas.Count > 0)
			{
				result.IsSucess = false;
				return result;
			}

			// TODO ...

			result.IsSucess = true;
			return result;
        }

	}
}

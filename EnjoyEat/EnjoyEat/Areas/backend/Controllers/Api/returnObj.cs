using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
	internal class returnObj
	{
		private Func<OkResult> ok;

		public returnObj(Func<OkResult> ok)
		{
			this.ok = ok;
		}
	}
}
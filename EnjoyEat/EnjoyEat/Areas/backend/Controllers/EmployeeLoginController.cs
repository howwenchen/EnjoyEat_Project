using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EnjoyEat.Areas.OrderForHere.API;

namespace EnjoyEat.Areas.backend.Controllers
{
	[Area("backend")]
	public class EmployeeLoginController : Controller
	{
		private readonly db_a989fe_thm101team6Context _context;
		public EmployeeLoginController(db_a989fe_thm101team6Context context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login([FromBody] EmployeeManagementDTO model)
		{
			var emp = _context.Employees.FirstOrDefault(e => e.Account.Equals(model.Account) && e.Password.Equals(model.Password));

			if (emp == null)
			{
				ModelState.AddModelError(string.Empty, "使用者帳號或密碼錯誤");
				return View();
			}
			HttpContext.Session.SetString("帳號", model.Account);
			//HttpContext.Session.SetString("密碼", model.Password);
			HttpContext.Session.SetString("職位", emp.Role);
			Console.WriteLine("帳號: " + HttpContext.Session.GetString("Account"));
			return Ok(model);
		}

		[HttpGet]
		public GetEmpRole GetEmpInfo()
		{
			string account = HttpContext.Session.GetString("Account");
			string password = HttpContext.Session.GetString("Password");
			string role = HttpContext.Session.GetString("Role");
			return new GetEmpRole { Account = account, Password = password , Role = role};
		}

		[HttpPost]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();

			return RedirectToAction("EmployeeLogin", "Index");
		}
	}
}

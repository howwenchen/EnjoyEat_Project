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
		public async Task<IActionResult> Login([FromBody] EmployeeManagementDTO model)
		{
			var emp = _context.Employees.FirstOrDefault(e => e.Account.Equals(model.Account) && e.Password.Equals(model.Password));

			if (emp == null)
			{
				ModelState.AddModelError(string.Empty, "使用者帳號或密碼錯誤");
				return View();
			}
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, emp.Name),
				new Claim(ClaimTypes.Role, emp.Role)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
			await HttpContext.SignInAsync(claimsPrinciple);
			return Ok();
		}
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "EmployeeLogin");
		}
	}
}

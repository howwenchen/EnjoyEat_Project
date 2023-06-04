using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using AspNetCore;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EnjoyEat.Controllers
{

    public class MemberLoginController : Controller
    {
        private readonly db_a989fe_thm101team6Context _db;
        public MemberLoginController(db_a989fe_thm101team6Context db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult ChangePd()
        {
            return View();
        }
        public IActionResult ChangePDSC()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }


        [Authorize(Roles = "User")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "home");
        }


    }
}






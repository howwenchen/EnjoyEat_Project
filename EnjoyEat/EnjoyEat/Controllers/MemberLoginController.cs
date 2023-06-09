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
using EnjoyEat.Services;
using System.Text.Json;

namespace EnjoyEat.Controllers
{

    public class MemberLoginController : Controller
    {
        private readonly db_a989fe_thm101team6Context _db;
        private readonly EncryptService encrypt;

        public MemberLoginController(db_a989fe_thm101team6Context db, EncryptService encrypt)
        {
            this._db = db;
            this.encrypt = encrypt;
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
        public IActionResult Success(string code) {
            var str = encrypt.AesDecryptToString(code);
            var obj = JsonSerializer.Deserialize<AesValidationDto>(str);
            if (DateTime.Now > obj.ExpiredDate)
            {
                return BadRequest("連結已過期");
            }
            var user = _db.MemberLogins.FirstOrDefault(x => x.Account == obj.Account);
            if (user != null)
            {
                user.IsActive = true;
                _db.SaveChanges();
            }
            return View();
        }
        //發送郵件確認
        public IActionResult ChangePDSC(string code)
        {
            var str = encrypt.AesDecryptToString(code);
            var obj = JsonSerializer.Deserialize<AesValidationDto>(str);
            if (DateTime.Now > obj.ExpiredDate)
            {
                return BadRequest("連結已過期");
            }
            var user = _db.MemberLogins.FirstOrDefault(x => x.Account == obj.Account);
            var account = user.Account;
            ViewBag.Account = account;
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






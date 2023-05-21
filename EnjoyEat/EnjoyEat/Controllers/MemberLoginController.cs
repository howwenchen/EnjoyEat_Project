using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Security.Claims;

namespace EnjoyEat.Controllers
{
    public class MemberLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly db_a989fe_thm101team6Context _db;
        public MemberLoginController(db_a989fe_thm101team6Context context)
        {
            _db = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Login(MemberLoginViewModel model)
        {
           var user=_db.Members.FirstOrDefault(x=>x.Account==model.Account&&
           x.Password==model.Password);
        
            if(user!=null)
            {
                ViewBag.Error = "帳號密碼錯誤";
                return View("Login");
            }

            return View();
        }
        
    }
}
        */
            

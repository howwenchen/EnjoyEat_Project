<<<<<<< HEAD
﻿//using EnjoyEat.Models;
//using EnjoyEat.Models.ViewModel;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Drawing.Text;
//using System.Security.Claims;
//using System.Security.Cryptography.X509Certificates;
//using AspNetCore;
//using System.Text;

//namespace EnjoyEat.Controllers
//{

//    public class MemberLoginController : Controller
//    {
//        private readonly db_a989fe_thm101team6Context _db;
//        public IActionResult Index()
//        {
//            return View();
//        }

//        public MemberLoginController(db_a989fe_thm101team6Context db)
//        {
//            this._db = db;
//        }
//        public IActionResult ForgetPassword()
//        {
//            return View();
//        }
//        public IActionResult ChangePd()
//        {
//            return View();
//        }
//        public IActionResult ChangePDSC()
//        {
//            return View();
//        }
//        public IActionResult Login()
//        {
//            return View();
//        }


//        public async Task<IActionResult> LogoutAsync()
//        {
//            await HttpContext.SignOutAsync();
//            return RedirectToAction("Index", "Home");
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]

//        public async Task<IActionResult> LoginAsync(MemberLoginViewModel model)
//        {


//            var user = _db.MemberLogins.FirstOrDefault(x => x.Account.Equals(model.Account) && x.Password.Equals(model.Password));

//            if (user == null)
//            {
//                ModelState.AddModelError(string.Empty, "使用者帳號或密碼錯誤");
//                return View();
//            }

//            //if (user.Password != model.Password)
//            //{
//            //    ModelState.AddModelError(string.Empty, "使用者密碼錯誤");
//            //    return View();
//            //}
          
//            if(HttpContext.Session.Keys.Contains("SessionKey"))
//            {
//                    string? SessionValue = HttpContext.Session.GetString("SessionKey");
//            }
    
          
            
            
//            //claim加密   
//            var claims = new List<Claim>() {
//                 new Claim(ClaimTypes.Name,null),
//                 new Claim(ClaimTypes.Role,null),
//            };
//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
//            await HttpContext.SignInAsync(claimsPrincipal);
//            return RedirectToAction("Index", "home");
//        }
//        public IActionResult AccessDenied()
//        {
//            return View();
//        }
//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();

//            return RedirectToAction("Index", "Home");
//        }

//        //public IActionResult ValidGoogleLogin()
//        //{
//        //    string? formCredential = Request.Form["credential"];   //回傳憑證
//        //    string? formToken = Request.Form[""];//回傳令牌
//        //    string? cookieToken = Request.Cookies[""];//Cookie令牌

//        //    //驗證google token
//        //    GoogleJsonWebSignature.Payload?playload= VerifyGoogleToken(formCredential, formToken, cookieToken);
//        //    if (playload == null)
//        //    {
//        //        // 驗證失敗
//        //        ViewData["Msg"] = "驗證 Google 授權失敗";
//        //    }
//        //    else
//        //    {
//        //        //驗證成功，取使用者資訊內容
//        //        ViewData["Msg"] = "驗證 Google 授權成功" + "<br>";
//        //        ViewData["Msg"] += "Email:" + payload.Email + "<br>";
//        //        ViewData["Msg"] += "Name:" + payload.Name + "<br>";
//        //        ViewData["Msg"] += "Picture:" + payload.Picture;
//        //    }
//        //    return View();
//        //}
//        //public  async  Task<GoogleJsonWebSignature.Playload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookieToken)
//        //{
//        //    // 檢查空值
//        //    if (formCredential == null || formToken == null && cookiesToken == null)
//        //    {
//        //        return null;
//        //    }

//        //    GoogleJsonWebSignature.Payload? payload;
//        //    try
//        //    {
//        //        // 驗證 token
//        //        if (formToken != cookiesToken)
//        //        {
//        //            return null;
//        //        }

//        //        // 驗證憑證
//        //        IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
//        //        string GoogleApiClientId = Config.GetSection("GoogleApiClientId").Value;
//        //        var settings = new GoogleJsonWebSignature.ValidationSettings()
//        //        {
//        //            Audience = new List<string>() { GoogleApiClientId }
//        //        };
//        //        payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);
//        //        if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
//        //        {
//        //            return null;
//        //        }
//        //        if (payload.ExpirationTimeSeconds == null)
//        //        {
//        //            return null;
//        //        }
//        //        else
//        //        {
//        //            DateTime now = DateTime.Now.ToUniversalTime();
//        //            DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
//        //            if (now > expiration)
//        //            {
//        //                return null;
//        //            }
//        //        }
//        //    }
//        //    catch
//        //    {
//        //        return null;
//        //    }
//        //    return payload;


//    }
//}

=======
﻿using EnjoyEat.Models;
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

namespace EnjoyEat.Controllers
{

    public class MemberLoginController : Controller
    {
        private readonly db_a989fe_thm101team6Context _db;
        public IActionResult Index()
        {
            return View();
        }

        public MemberLoginController(db_a989fe_thm101team6Context db)
        {
            this._db = db;
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


        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LoginAsync(MemberLoginViewModel model)
        {


            var user = _db.MemberLogins.FirstOrDefault(x => x.Account.Equals(model.Account) && x.Password.Equals(model.Password));

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "使用者帳號或密碼錯誤");
                return View();
            }

            //if (user.Password != model.Password)
            //{
            //    ModelState.AddModelError(string.Empty, "使用者密碼錯誤");
            //    return View();
            //}
          
            if(HttpContext.Session.Keys.Contains("SessionKey"))
            {
                    string? SessionValue = HttpContext.Session.GetString("SessionKey");
            }
    
          
            
            
            //claim加密   
            var claims = new List<Claim>() {
                 new Claim(ClaimTypes.Name,null),
                 new Claim(ClaimTypes.Role,null),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index", "home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        //public IActionResult ValidGoogleLogin()
        //{
        //    string? formCredential = Request.Form["credential"];   //回傳憑證
        //    string? formToken = Request.Form[""];//回傳令牌
        //    string? cookieToken = Request.Cookies[""];//Cookie令牌

        //    //驗證google token
        //    GoogleJsonWebSignature.Payload?playload= VerifyGoogleToken(formCredential, formToken, cookieToken);
        //    if (playload == null)
        //    {
        //        // 驗證失敗
        //        ViewData["Msg"] = "驗證 Google 授權失敗";
        //    }
        //    else
        //    {
        //        //驗證成功，取使用者資訊內容
        //        ViewData["Msg"] = "驗證 Google 授權成功" + "<br>";
        //        ViewData["Msg"] += "Email:" + payload.Email + "<br>";
        //        ViewData["Msg"] += "Name:" + payload.Name + "<br>";
        //        ViewData["Msg"] += "Picture:" + payload.Picture;
        //    }
        //    return View();
        //}
        //public  async  Task<GoogleJsonWebSignature.Playload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookieToken)
        //{
        //    // 檢查空值
        //    if (formCredential == null || formToken == null && cookiesToken == null)
        //    {
        //        return null;
        //    }

        //    GoogleJsonWebSignature.Payload? payload;
        //    try
        //    {
        //        // 驗證 token
        //        if (formToken != cookiesToken)
        //        {
        //            return null;
        //        }

        //        // 驗證憑證
        //        IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        //        string GoogleApiClientId = Config.GetSection("GoogleApiClientId").Value;
        //        var settings = new GoogleJsonWebSignature.ValidationSettings()
        //        {
        //            Audience = new List<string>() { GoogleApiClientId }
        //        };
        //        payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);
        //        if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
        //        {
        //            return null;
        //        }
        //        if (payload.ExpirationTimeSeconds == null)
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            DateTime now = DateTime.Now.ToUniversalTime();
        //            DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
        //            if (now > expiration)
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    return payload;


    }
}
>>>>>>> c9571974c37cd7c65830c554835d732dffc14c34




<<<<<<< HEAD
=======

>>>>>>> c9571974c37cd7c65830c554835d732dffc14c34

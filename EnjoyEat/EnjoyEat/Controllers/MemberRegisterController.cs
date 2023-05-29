using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Linq;
using System.Xml;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;
using WebApplication4.Services;

namespace EnjoyEat.Controllers
{
    public class MemberRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public MemberRegisterController(db_a989fe_thm101team6Context db)
        {
            this._db = db;
        }
        private readonly db_a989fe_thm101team6Context _db;
        private readonly EncryptService encrypt;
        private object _userManager;
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterSuccess()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Register(MemberRegisterViewModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        //檢查帳號是否已存在於資料庫
        //        if (IsAccountExists(model.Account))
        //        {
        //            ModelState.AddModelError("Username", "帳號已被使用");
        //            return View(model);
        //        }
        //        var memberDB = new MemberRegisterViewModel()
        //        {
        //            MemberId = model.MemberId,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            Email = model.Email,
        //            Address = model.Address,
        //            Phone = model.Phone,
        //            Gender = model.Gender,
        //            Birthday = model.Birthday,
        //            RegisterDay = model.RegisterDay,
        //            LevelName = model.LevelName,
        //            Account = model.Account,
        //            Password = model.Password,
        //        };
        //        _db.Set<MemberRegisterViewModel>().Add(memberDB);
        //        _db.SaveChanges();

        //        //寄信
        //        var obj = new AesValidationDto(model.Account.ToString(), DateTime.Now.AddDays(3));
        //        var jString = JsonSerializer.Serialize(obj);
        //        var code = encrypt.AesEncryptToBase64(jString);


        //        var mail = new MailMessage()
        //        {
        //            From = new MailAddress("thm101team66@gmail.com"),
        //            Subject = "啟用網站驗證",
        //            Body = @$"請點這<a src='https://localhost:7157/user/enable?code={code}'>這裡</a>來啟用你的帳號",
        //            IsBodyHtml = true,
        //            BodyEncoding = Encoding.UTF8,
        //        };
        //        mail.To.Add(new MailAddress(model.Account.ToString()));
        //        try
        //        {
        //            using (var sm = new SmtpClient("smtp.gmail.com", 587)) //465 ssl
        //            {
        //                sm.EnableSsl = true;
        //                sm.Credentials = new NetworkCredential("thm101team66@gmail.com", "kfwpggrhucetwqsd");
        //                sm.Send(mail);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }

        //        // 使用密碼加密服務將密碼加密
        //        string password = "dnjuey";
        //        //密碼加密
        //        string hashValue = HashPassword(password);
        //        Console.WriteLine("加密後的hash value:{0}", hashValue);
        //        //模仿輸入密碼,驗證密碼是否正確
        //        string inputPassword = "dnjuery";
        //        var result = "";
        //        if (ValidatePassword(inputPassword, hashValue))
        //        {
        //            Console.WriteLine("密碼正確");
        //            result = "Succedded";
        //        }
        //        else
        //        {
        //            Console.WriteLine("密碼錯誤");
        //            result = "Errors";

        //        }
        //        //output:密碼正確

        //        // 儲存用戶到數據庫
        //        if (result == "Succedded")
        //        {
        //            // 註冊成功，執行其他相關操作，例如登入用戶等(導回已登入頁面)

        //            // 返回註冊成功的視圖或重新導向到其他頁面
        //            return RedirectToAction("/MemberManagement/Index");
        //        }
        //        else // 如果儲存用戶到數據庫中失敗，顯示錯誤訊息(導回首頁)
        //        {

        //            return RedirectToAction("/Home/Index");

        //        }
        //        return RedirectToAction("RegisterSuccess");
        //    }
        //    return View(model);

        //}
        //private bool IsAccountExists(byte[] account)
        //{
        //    return _db.Members.Any(m => m.Equals(account));
        //}

        ////驗證Hash密碼
        //private static bool ValidatePassword(string inputPassword, string hashValue)
        //{
        //    string validHashValue = HashPassword(hashValue);
        //    return validHashValue.Equals(hashValue);
        //}
        ////加密(加鹽)密碼
        //private static string HashPassword(string password)
        //{
        //    const string salt = "SecretSalt";
        //    //密碼加鹽
        //    string passwordWithSalt = password + salt;
        //    //將加鹽後的密碼轉成Byte
        //    byte[] passwordWithSaltBytes = Encoding.Default.GetBytes(passwordWithSalt);
        //    //選擇SHA256加密演算法
        //    var hash = new SHA256CryptoServiceProvider();
        //    ////hash加密
        //    byte[] hashValue = hash.ComputeHash(passwordWithSaltBytes);

        //    return Convert.ToBase64String(hashValue);
        //}
        //public async Task<IActionResult> Enable(string code)
        //{
        //    var str = encrypt.AesDecryptToString(code);
        //    var obj = JsonSerializer.Deserialize<AesValidationDto>(str);
        //    if (DateTime.Now > obj.ExpiredDate)
        //    {
        //        return BadRequest("過期");
        //    }
        //    var user = _db.Member.FirstOrDefault(x => x.Account == obj.Account);
        //    if (user != null)
        //    {
        //        user.IsActive = true;
        //        _db.SaveChanges();
        //    }

        //    return Ok($@"code:{code}  str:{str}");
        //}


    }

}



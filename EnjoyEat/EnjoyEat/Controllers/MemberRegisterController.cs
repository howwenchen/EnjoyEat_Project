//using EnjoyEat.Areas.OrderForHere.Models;
//using EnjoyEat.Models;
//using EnjoyEat.Models.ViewModel;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Cryptography.X509Certificates;
//using System.Security.Principal;
//using System.Security.Cryptography;
//using System.Xml;
//using System.Text;

//namespace EnjoyEat.Controllers
//{
//    public class MemberRegisterController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//        private readonly db_a989fe_thm101team6Context _context;
//        private object _Context;
//        private object _userManager;

//        public MemberRegisterController(db_a989fe_thm101team6Context context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> RegisterAsync(MemberRegisterViewModel model)
//        {
//            {
//                // 檢查模型狀態是否有效
//                if (!ModelState.IsValid)
//                {
//                    // 如果模型狀態無效，返回註冊頁面並顯示錯誤訊息
//                    return View(model);
//                }
//                //檢查帳號是否已存在於資料庫
//                var user = _context.Members.FirstOrDefault(x => x.Account == model.Account);
//                if (user != null)
//                {
//                    ModelState.AddModelError("Account", "該帳號已被使用");
//                    return View();
//                }

//                // 創建一筆資料給db
//                //MemberLogin跟Member資料表進行關聯 (才會有帳號/密碼的資訊)
//                var memberLoginViewModels = _context.MemberLogins
//                    .Include(ml => ml.Member) // 導航到Member資料表
//                    .Select(ml => new MemberLoginViewModel
//                    {
//                        Account = ml.Account,
//                        Password = ml.Password

//                    }).ToList();
//                // 使用密碼加密服務將密碼加密
//                string password = "dnjuey";
//                //密碼加密
//                string hashValue = HashPassword(password);
//                Console.WriteLine("加密後的hash value:{0}", hashValue);
//                //模仿輸入密碼,驗證密碼是否正確
//                string inputPassword = "dnjuery";
//                if (ValidatePassword(inputPassword, hashValue))
//                {
//                    Console.WriteLine("密碼正確");
//                }
//                else
//                {
//                    Console.WriteLine("密碼錯誤");
//                }
//                //output:密碼正確

//                // 儲存用戶到數據庫中
//                /*  var result = await _userManager.CreateAsync(neUser);
//                      if (result.Succeeded)
//                      {
//                          // 註冊成功，執行其他相關操作，例如登入用戶等

//                          // 返回註冊成功的視圖或重新導向到其他頁面
//                          return RedirectToAction("RegisterSuccess");
//                      }

//                      // 如果儲存用戶到數據庫中失敗，顯示錯誤訊息
//                      foreach (var error in result.Errors)
//                      {
//                          ModelState.AddModelError("", error.Description);
//                      }
//                */
//                return View(model);

//            }

//        }
//        //驗證Hash密碼
//        private static bool ValidatePassword(string inputPassword, string hashValue)
//        {
//            string validHashValue = HashPassword(hashValue);
//            return validHashValue.Equals(hashValue);
//        }
//        //加密(加鹽)密碼
//        private static string HashPassword(string password)
//        {
//            const string salt = "SecretSalt";
//            //密碼加鹽
//            string passwordWithSalt = password + salt;
//            //將加鹽後的密碼轉成Byte
//            byte[] passwordWithSaltBytes = Encoding.Default.GetBytes(passwordWithSalt);
//            //選擇SHA256加密演算法
//            var hash = new SHA256CryptoServiceProvider();
//            //hash加密
//            byte[] hashValue = hash.ComputeHash(passwordWithSaltBytes);

//            return Convert.ToBase64String(hashValue);
//        }



//    }
//}

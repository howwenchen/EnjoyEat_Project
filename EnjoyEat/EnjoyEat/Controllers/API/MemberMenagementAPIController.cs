﻿using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using EnjoyEat.Services;
using System.Reflection;
using System.Security.Policy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Web;

namespace EnjoyEat.Controllers.API
{
    [Route("api/member/[action]")]
    [ApiController]
    public class MemberMenagementAPIController : Controller
    {
        private readonly db_a989fe_thm101team6Context db;
        private readonly EncryptService encrypt;
        private readonly HashService hash;

        public MemberMenagementAPIController(db_a989fe_thm101team6Context db, HashService hash, EncryptService encrypt)
        {
            this.db = db;
            this.encrypt = encrypt;
            this.hash = hash;
        }
        //註冊會員
        [HttpPost]
        public async Task<string> Register([FromBody]MemberRegisterViewModel model)
        {
            if (EmailExists(model.Email))
            {
                return "郵件已註冊過";
            }

            var user =await db.MemberLogins.FirstOrDefaultAsync(x => x.Account == model.Account);
            if (user != null)
            {
                return "帳號已註冊過";
            }
            Member MemberInfo = new Member
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                RegisterDay = DateTime.Now,
                Gender = model.Gender,
                Email = model.Email,
                Birthday = model.Birthday,
                Address = model.Address,
                LevelName = "平民",
                LevelDiscount = 0.95,
            };
            db.Members.Add(MemberInfo);
            await db.SaveChangesAsync();
            int memberId = MemberInfo.MemberId;
            
            string salt = Guid.NewGuid().ToString("N");
            MemberLogin MemberAccount = new MemberLogin()
            {
                MemberId = memberId,
                Account = model.Account,
                Password = hash.GetHash(string.Concat(model.Password, salt).ToString()),                
                Salt = salt,
                Role ="User",
                IsActive =false,
            };
            
            await db.MemberLogins.AddAsync(MemberAccount);
            await db.SaveChangesAsync();

            //寄信
            var obj = new AesValidationDto(model.Account,DateTime.Now.AddDays(3));
            var jString = JsonSerializer.Serialize(obj);
            var code = encrypt.AesEncryptToBase64(jString);
            var encode = HttpUtility.UrlEncode(code);

            var mail = new MailMessage()
            {
                From = new MailAddress("thm101team66@gmail.com"),
                Subject = "啟用網站驗證",
                Body = @$"請點這<a href=`https://enjoyeat.azurewebsites.net/MemberLogin/Success?code={encode}`>此處</a>來啟用你的帳號",
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
            };

            mail.To.Add(new MailAddress(model.Email));
            try
            {
                using (var sm = new SmtpClient("smtp.gmail.com", 587)) //465 ssl
                {
                    sm.EnableSsl = true;
                    sm.Credentials = new NetworkCredential("thm101team66@gmail.com", "lepbkbyfphbmjwtx");
                    sm.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return "註冊成功";
        }

        //會員登入
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] MemberLoginViewModel model)
        {
            var user = db.MemberLogins.FirstOrDefault(x => x.Account == model.Account);

            if (user == null)
            {
                return Content("帳號密碼錯誤");
            }
            var hashedPassword = hash.GetHash(string.Concat(model.Password, user.Salt).ToString());
            var userPassword = db.MemberLogins.FirstOrDefault(x => x.Password == hashedPassword);
            if (userPassword == null)
            {
                return Content("帳號密碼錯誤");
            }
            if (user.IsActive == false)
            {
                return Content("此帳號尚未驗證，請至信箱驗證");
            }

            var member = db.Members.FirstOrDefault(x => x.MemberId == user.MemberId);
            var fullname = member.LastName + member.FirstName;

            //通行證
            var claims = new List<Claim>() {
                 new Claim(ClaimTypes.Name, fullname),
                 new Claim(ClaimTypes.Role, user.Role),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            HttpContext.Session.SetInt32("MemberId", user.MemberId);
            //HttpContext.Session.SetString("Email", member.Email);

            //拿取在Index儲存的外帶／內用資訊
            var isTakeaway = HttpContext.Session.GetString("IsTakeaway");

            //根據拿到的資訊導向不同路徑的點餐頁面
            if (isTakeaway == "True")
            {
                var redirectUrl = Url.Action("Order", "OrderTogo");
                return Json(new { Redirect = redirectUrl });
            }
            else if (isTakeaway == "False")
            {
                var redirectUrl = Url.Action("Order", "OrderHere");
                return Json(new { Redirect = redirectUrl });
            }
            else
            {
                return Content("登入成功");
            }
        }

        //忘記密碼
        [HttpPost]
        public async Task<string> ForgetPassword([FromBody] ForgetViewModel model)
        {

            var member = db.Members.FirstOrDefault(x => x.Email == model.Email);
            if (member == null)
            {
                return "信箱錯誤";
            }
            var account =db.MemberLogins.Where(y=>y.MemberId==member.MemberId).FirstOrDefault(x =>x.Account == model.Account);
            if (account == null)
            {
                return "帳號錯誤";
            }

            //寄信
            var obj = new AesValidationDto(model.Account, DateTime.Now.AddDays(3));
            var jString = JsonSerializer.Serialize(obj);
            var code = encrypt.AesEncryptToBase64(jString);
            var encode=HttpUtility.UrlEncode(code);

            var mail = new MailMessage()
            {
                From = new MailAddress("thm101team66@gmail.com"),
                Subject = "重新設定密碼",
                Body = @$"請點這<a href=`https://enjoyeat.azurewebsites.net/MemberLogin/ChangePDSC?code={encode}`>這裡</a>重新設定密碼",
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
            };

            mail.To.Add(new MailAddress(model.Email));
            try
            {
                using (var sm = new SmtpClient("smtp.gmail.com", 587)) //465 ssl
                {
                    sm.EnableSsl = true;
                    sm.Credentials = new NetworkCredential("thm101team66@gmail.com", "lepbkbyfphbmjwtx");
                    sm.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            HttpContext.Session.SetInt32("MemberId", member.MemberId);
            return "成功";
        }

        //重設密碼
        [HttpPut]
        public async Task<string> SetPassword(SetViewModel model)
        {
            var user = db.MemberLogins.FirstOrDefault(x=>x.Account==model.Account);
            if (user == null)
            {
                return "無此帳號";
            }
            user.Password= hash.GetHash(string.Concat(model.Password, user.Salt).ToString());
            await db.SaveChangesAsync();
            return "成功";

        }

        //輸入新密碼
        [HttpPut]       
        public async Task<string> NewPassword(SetViewModel model)
        {
            var memberId=HttpContext.Session.GetInt32("MemberId");
            var user=db.MemberLogins.FirstOrDefault(x=>x.MemberId==memberId);
            var hashedPassword = hash.GetHash(string.Concat(model.Password, user.Salt).ToString());
            user.Password = hashedPassword;
            await db.SaveChangesAsync();
            return "成功";
        }

        //取得會員資料
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult GetMember()
        {
            var memberId =HttpContext.Session.GetInt32("MemberId");
            var user = db.Members.Include(x => x.Orders).Include(x => x.LevelNameNavigation).FirstOrDefault(x => x.MemberId == memberId);
            if (user == null)
            {
                return NotFound();
            }
            var member = new MemberViewModel()
            {
                MemberId = user.MemberId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Address = user.Address,
                Phone = user.Phone,
                LevelName = user.LevelName,
                LevelDiscount = user.LevelDiscount,
                MemberPoint = user.MemberPoint,
                Orders = user.Orders.Select(x => new MemberOrderViewModel
                {
                    OrderDate = x.OrderDate,
                    OrderId = x.OrderId,
                    TableId = x.TableId,
                    FinalPrice = x.FinalPrice,
                    IsTakeway = x.IsTakeway,
                }).ToList(),
            };
            return Ok(member);
        }


        //取得餐點明細
        [HttpGet]
        public IActionResult GetOrder()
        {
            var memberId = HttpContext.Session.GetInt32("MemberId"); 
            var orders = db.OrderDetails.Include(x =>x.Product).Where(o => o.Order.MemberId == memberId).Select(od => new MemberOrderDetailViewModel
            {
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = (short)od.Quantity,
                UnitPrice = od.UnitPrice,
                SubtotalPrice = od.SubtotalPrice,
                ProductName =od.Product.ProductName,
            }).ToList();

            // 延遲載入 OrderDetails
            //foreach(var order in orders)
            //{
            //    db.Entry(order).Collection(x => x.).Load();
            //}
            return Ok(orders);
        }


        //修改會員資料
        [HttpPut]
        public async Task<IActionResult> EditMemberInfo([FromBody] MemberViewModel memberViewModel)
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            Member member = await db.Members.FindAsync(memberId);
            if (member == null)
            {
                return NotFound();
            }

            member.FirstName = memberViewModel.FirstName;
            member.LastName = memberViewModel.LastName;
            member.Address = memberViewModel.Address;
            member.Email = memberViewModel.Email;
            member.Phone=memberViewModel.Phone;
            member.Birthday = memberViewModel.Birthday;
            member.Gender = memberViewModel.Gender;


            db.Entry(member).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }


        //修改密碼
        [HttpPut]
        public async Task<string> ChangePassword(ChangeViewModel model)
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            var user = db.MemberLogins.FirstOrDefault(x=> x.MemberId==memberId);
            var hashedPassword = hash.GetHash(string.Concat(model.OriginalPassword, user.Salt).ToString());
            var password = db.MemberLogins.FirstOrDefault(x => x.Password == hashedPassword);
            if (password == null)
            {
                return "密碼錯誤";
            }

            password.Password = hash.GetHash(string.Concat(model.NewPassword, user.Salt).ToString());
            await db.SaveChangesAsync();
            return "成功";
        }

        private bool EmailExists(string Email)
        {
            return db.Members.Any(member => member.Email == Email);
        }

       

    }
}

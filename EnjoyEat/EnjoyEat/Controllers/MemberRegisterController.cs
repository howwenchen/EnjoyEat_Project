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
using EnjoyEat.Services;
using System.Diagnostics;

namespace EnjoyEat.Controllers
{
    public class MemberRegisterController : Controller
    {
        private readonly db_a989fe_thm101team6Context _db;
        private readonly EncryptService encrypt;

        public MemberRegisterController(db_a989fe_thm101team6Context db,EncryptService encrypt)
        {
            this._db = db;
            this.encrypt = encrypt;
        }

        public IActionResult Index()
        {
            return View();
        }

        //發送郵件確認
        public async Task<IActionResult> Enable(string code)
        {
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

            return Ok($@"code:{code}  str:{str}");
        }
        public IActionResult RegisterSuccess()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

}



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



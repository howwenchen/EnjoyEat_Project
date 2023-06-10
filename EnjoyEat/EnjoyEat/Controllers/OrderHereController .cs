﻿using EnjoyEat.Models.ViewModel;
using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Controllers
{
    public class OrderHereController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("IsTakeaway", "False");
            HttpContext.Session.SetInt32("MemberId", 0);
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult OrderLogin()
        {
			return View();
		}
    }
}


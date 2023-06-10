using EnjoyEat.Models.ViewModel;
using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Controllers
{
    public class OrderTogoController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("IsTakeaway","True");
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
    }
}


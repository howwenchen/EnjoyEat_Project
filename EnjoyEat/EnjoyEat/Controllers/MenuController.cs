using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Controllers;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;

namespace EnjoyEat.Controllers
{
    public class MenuController : Controller
    {
        
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
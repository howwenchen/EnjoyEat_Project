using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Controllers
{
    public class HomeController : Controller
    {
        public readonly db_a989fe_thm101team6Context _context;
        public readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, db_a989fe_thm101team6Context context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/GetNews/{page?}")]
        public IActionResult GetNews(int page = 1)
        {
            int pageSize = 8;
            int skipCount = (page - 1) * pageSize;

            var totalNewsCount = _context.News.Count();
            var totalPageCount = (int)Math.Ceiling(totalNewsCount / (double)pageSize);

            if (totalPageCount == 0)
            {
                return BadRequest("No news available.");
            }

            if (page > totalPageCount)
            {
                return BadRequest("The requested page number exceeds the total page count.");
            }

            var news = _context.News.Skip(skipCount).Take(pageSize).ToList();
            return Json(news);
        }



        [HttpGet]
        [Route("Home/GetPageCount")]
        public IActionResult GetPageCount()
        {
            int pageSize = 8;
            var pageCount = (int)Math.Ceiling(_context.News.Count() / (double)pageSize);

            // 回傳頁數
            return Json(pageCount);
        }
        public IActionResult News()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
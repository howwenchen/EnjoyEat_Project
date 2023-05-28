using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;

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

            var News = _context.News.AsNoTracking().Select(i => new NewsViewModel
            {
                NewsId = i.NewsId,
                Title = i.Title,
                Category = i.Category,
                Content = i.Content,
                ImageUrl = i.ImageUrl,
                PublishDate = i.PublishDate,
                LastModified = i.LastModified
            })
            ;
            
            int pageSize = 8;
            int skipCount = (page - 1) * pageSize;

            var totalNewsCount = News.Count();
            var totalPageCount = (int)Math.Ceiling(totalNewsCount / (double)pageSize);

            if (totalPageCount == 0)
            {
                return BadRequest("No news available.");
            }

            if (page > totalPageCount)
            {
                return BadRequest("The requested page number exceeds the total page count.");
            }

            var newsItem = News.Skip(skipCount).Take(pageSize).ToList();
            return Json(newsItem);
        }



        [HttpGet]
        [Route("Home/GetPageCount")]
        public IActionResult GetPageCount()
        {
            var News = _context.News.Select(i => new NewsViewModel
            {
                NewsId = i.NewsId,
            });
            int pageSize = 8;
            var pageCount = (int)Math.Ceiling(News.Count() / (double)pageSize);

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
        public IActionResult FaceBookLogin()
        {
            var prop = new AuthenticationProperties
            {
                RedirectUri = Url.Action("FaceBookResponse")
            };
            return Challenge(prop,FacebookDefaults.AuthenticationScheme);

        }
        public async Task<IActionResult> FaceBookResponse()
        {
            var result= await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var claims=result.Principal.Claims.Select(x => new
                {
                    x.Type,
                    x.Value,
                });
                return Json(claims);
            }
            return Ok();
        }
        
	}
}
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace EnjoyEat.Areas.OrderForHere.Controllers.api
{
    [Route("api/StartOrderApi/{action}")]
    [ApiController]
    public class StartOrderApiController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly db_a989fe_thm101team6Context _context;

        public StartOrderApiController(IWebHostEnvironment env, db_a989fe_thm101team6Context context)
        {
            this._env = env;
            this._context = context;
        }
        [HttpGet]
        public IEnumerable<MenuViewModel> GetAllProducts() 
        {
            var menu = from pro in _context.Products
                       select new MenuViewModel
                       {
                           ProductId = pro.ProductId,
                           MealImg = pro.MealImg,
                           ProductName = pro.ProductName,
                           UnitPrice = pro.UnitPrice,
                       };
            return menu;
        }

        [HttpPost]
        public string AddToCart([FromBody] SendOrderViewModel cart)
        {
            CookieOptions cko = new CookieOptions();
            cko.Expires = new DateTimeOffset(DateTime.Now.AddHours(2));
            string json = JsonConvert.SerializeObject(cart);
            HttpContext.Response.Cookies.Append("Cart", json, cko);
            return "OK";
        }
    }
}

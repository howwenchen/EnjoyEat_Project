using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
    [Area("OrderForHere")]
    public class FeedBackController : Controller
    {
        
        public IActionResult FeedBack(string OrderId)
        {
            ViewBag.order = OrderId;
            return View();
        }
        public IActionResult Thanks()
        { 
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
    [Area("OrderForHere")]
    public class FeedBackController : Controller
    {
        
        public IActionResult Feedback(string OrderId)
        {
            ViewBag.order = OrderId;
            return View();
        }
    }
}

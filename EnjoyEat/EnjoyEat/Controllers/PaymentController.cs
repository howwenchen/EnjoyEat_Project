using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Notify()
        {
            // 在這裡處理藍新金流回傳的資訊

            // 根據回傳資訊執行相應的邏輯

            // 回傳一個畫面
            return View();
        }

        // GET: Payment/Return
        [HttpGet]
        public IActionResult Return()
        {
            // 在這裡處理用戶返回後的相應操作
            // 根據需要執行相應的邏輯

            // 回傳一個畫面
            return View();
        }
    }
}

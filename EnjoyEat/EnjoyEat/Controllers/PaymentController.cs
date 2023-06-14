using EnjoyEat.Models;
using EnjoyEat.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static EnjoyEat.Models.DTO.OnlinePaymentDTO;

namespace EnjoyEat.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AesService _aesService;
        private readonly db_a989fe_thm101team6Context _context;
        private readonly IConfiguration _config;

        public PaymentController(AesService aesService, db_a989fe_thm101team6Context context, IConfiguration config)
        {
            _aesService = aesService;
            _context = context;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("Payment/Notify")]
        [Consumes("application/json", "application/x-www-form-urlencoded")]
        public async Task<IActionResult> Notify([FromForm] ResponseData responseData)
        {
            if (responseData != null)
            {
                // 將回傳加密資訊從 hex string 轉回 byte array   
                byte[] tradeInfoBytes = _aesService.ToByteArray(responseData.TradeInfo);

                // 轉換回 string 且移除多餘的 dash
                string unpaddedTradeInfo = BitConverter.ToString(tradeInfoBytes).Replace("-", "");

                // AES 解密
                var decryptedTradeInfo = _aesService.AesDecryptFromHex(unpaddedTradeInfo, _config["Payment:HashKey"], _config["Payment:HashIV"]);

                // 驗證 SHA256 雜湊值
                var calculatedTradeSha = _aesService.AddSHA256CheckCode(decryptedTradeInfo, _config["Payment:HashKey"], _config["Payment:HashIV"]);
                if (calculatedTradeSha != responseData.TradeSha)
                {
                    return BadRequest("Invalid TradeSha.");
                }

                TradeInfoResponse? tradeInfo = JsonConvert.DeserializeObject<TradeInfoResponse>(decryptedTradeInfo);
                string serializedTradeInfo = _aesService.SerializeObject(tradeInfo);

                //將這筆回傳OrderId的Order的IsSuccess的狀態改成true
                if (tradeInfo != null)
                {
                    var order = await _context.Orders.FindAsync(tradeInfo.Result.MerchantOrderNo);
                    if (order == null)
                    {
                        return NotFound("Order not found.");
                    }
                    order.IsSuccess = true;
                    await _context.SaveChangesAsync();
                    ViewBag.Result = tradeInfo;
                    return Ok("訂單狀態已變更");
                }

                return BadRequest();
            }

            return BadRequest("Invalid response data.");
        }
    }
}
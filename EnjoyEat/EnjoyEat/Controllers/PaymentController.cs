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
        public async Task<IActionResult> Notify(ResponseData responseData)
        {
            if (responseData != null)
            {
                // 將回傳加密資訊從 hex string 轉回 byte array   
                byte[] tradeInfoBytes = _aesService.ToByteArray(responseData.TradeInfo);

                // 移除 PKCS7 填充
                byte[] unpaddedTradeInfoBytes = _aesService.RemovePKCS7Padding(tradeInfoBytes);

                // 轉換回 string 且移除多餘的 dash
                string unpaddedTradeInfo = BitConverter.ToString(unpaddedTradeInfoBytes).Replace("-", "");

                // AES 解密
                var decryptedTradeInfo = _aesService.AesDecryptFromHex(unpaddedTradeInfo, _config["Payment: HashKey"], _config["Payment:HashIV"]);

                // 驗證 SHA256 雜湊值
                var calculatedTradeSha = _aesService.AddSHA256CheckCode(decryptedTradeInfo, _config["Payment: HashKey"], _config["Payment:HashIV"]);
                if (calculatedTradeSha != responseData.TradeSha)
                {
                    return BadRequest("Invalid TradeSha.");
                }

                TradeInfoResponse tradeInfo = JsonConvert.DeserializeObject<TradeInfoResponse>(decryptedTradeInfo);

                //將這筆回傳OrderId的Order的IsSuccess的狀態改成true
                var order = await _context.Orders.FindAsync(tradeInfo.Result.MerchantOrderNo);
                order.IsSuccess = true;
                await _context.SaveChangesAsync();
                ViewBag.Result = tradeInfo.Result;
                
                return View();
            }

            // If there is no data, return HTTP 400 Bad Request.
            return BadRequest();
        }

    }
}

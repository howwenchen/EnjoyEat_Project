using EnjoyEat.Models.DTO;
using EnjoyEat.Models.ViewModel;
using EnjoyEat.Models;
using EnjoyEat.Services;
using System.Text;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using static EnjoyEat.Models.DTO.OnlinePaymentDTO;
using System.Net.Http.Headers;

public interface IPaymentService
{
    Task<CheckoutViewModel> MapToPaymentModelAsync(CheckoutViewModel model);
    Task<RequestData> ProcessPaymentAsync(CheckoutViewModel order);
}


public class PaymentService : IPaymentService
{
    private readonly IConfiguration _config;
    private readonly ILogger<PaymentService> _logger;
    private readonly HttpClient _httpClient;
    private readonly AesService _aesService;
    private readonly db_a989fe_thm101team6Context _context;

    public PaymentService(IConfiguration config, ILogger<PaymentService> logger, AesService aesService, db_a989fe_thm101team6Context context)
    {
        _config = config;
        _logger = logger;
        _httpClient = new HttpClient();
        _aesService = aesService;
        _context = context;
    }

    //將order資訊存到Payment資料表
    public async Task<CheckoutViewModel> MapToPaymentModelAsync(CheckoutViewModel model)
    {
        // 取得MemberId
        var memberId = model.MemberId;
        // 取得OrderId
        var orderId = await _context.Orders
            .Where(o => o.MemberId == memberId)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => o.OrderId)
            .FirstOrDefaultAsync();

        model.OrderId = orderId;

        // 儲存需要的資訊到Payment資料表
        var payment = new Payment
        {
            OrderId = orderId,
            PaymentTypes = model.PaymentMethod,
            CreateTime = DateTime.UtcNow,
        };
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return model;
    }


    // 建立TradeInfo by Order
    private string CreateTradeInfo(CheckoutViewModel order)
    {
        var values = new Dictionary<string, string>
    {
        { "MerchantID", _config["Payment:MerchantID"] },
        { "RespondType", "JSON" },
        { "TimeStamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() },
        { "Version", "2.0" },
        { "MerchantOrderNo", order.OrderId.ToString() },
        { "Amt", order.TotalPrice.ToString() },
        { "ItemDesc", "訂單編號： " + order.OrderId.ToString() },
        { "NotifyURL", _config["Payment:NotifyURL"] },
        { "ReturnURL", _config["Payment:ReturnURL"] },
        { "Credit", "1" },
        { "LinePay", "1" },
        { "AndroidPay", "1" }
    };

        var content = new FormUrlEncodedContent(values);
        var httpContent = content.ReadAsStringAsync().Result;
        return httpContent;
    }


    public async Task<RequestData> ProcessPaymentAsync(CheckoutViewModel order)
    {
        try
        {
            string MerchantID = _config["Payment:MerchantID"];
            // 建立交易資訊的http encode 字串
            var tradeInfo = CreateTradeInfo(order);
            // 將交易資訊進行Aes加密並轉為十六進位字串
            var TradeInfo = _aesService.AesEncryptToHex(tradeInfo, _config["Payment:HashKey"], _config["Payment:HashIV"]); 
            //加上檢查碼後SHA256加密
            var TradeSha = _aesService.AddSHA256CheckCode(TradeInfo, _config["Payment:HashKey"], _config["Payment:HashIV"]);
            
            var requestData = new RequestData
            {
                MerchantID = MerchantID,
                TradeInfo = TradeInfo,
                TradeSha = TradeSha,
                Version = "2.0"
            };

            return requestData;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ProcessPayment failed.");
            throw;
        }
    }
    //解密付款資訊
    private string DecryptTradeInfo(ResponseData responseData)
    {
        string HashKey = _config["Payment:HashKey"];
        string HashIV = _config["Payment:HashIV"];
        string decryptedTradeInfo = _aesService.AesDecryptFromHex(responseData.TradeInfo, HashKey, HashIV); // 將回應資訊的交易資訊部分進行解密
        return decryptedTradeInfo; // 返回解密後的交易資訊
    }


    //透過檢查碼確認回傳資訊真偽
    private void VerifyTradeSha(ResponseData responseData, string decryptedTradeInfo)
    {
        string HashKey = _config["Payment:HashKey"];
        string HashIV = _config["Payment:HashIV"];
        string validTradeSha = _aesService.AddSHA256CheckCode(decryptedTradeInfo, HashKey, HashIV);
        if (validTradeSha != responseData.TradeSha)
        {
            throw new Exception("TradeSha verification failed.");
        }
    }

}
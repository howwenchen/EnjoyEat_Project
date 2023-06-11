using EnjoyEat.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models.ViewModel;
using System.Linq;
using System.Text.Json;
using System.Text;
using EnjoyEat.Models.DTO;

namespace EnjoyEat.Hubs
{
    public class PaymentHub : Hub
    {
        private readonly db_a989fe_thm101team6Context _dbContext;

        public PaymentHub(db_a989fe_thm101team6Context dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task NotifyPaymentStatus(string orderId, bool isSuccess)
        {
            await Clients.All.SendAsync("PaymentStatusChanged", orderId, isSuccess);
        }

        public async Task TrackPaymentStatus(string orderId)
        {

            bool isPaid = CheckPaymentStatus(orderId);
            // 呼叫前端的 PaymentStatusChanged 方法
            await Clients.Caller.SendAsync("PaymentStatusChanged", isPaid);
        }
        private bool CheckPaymentStatus(string orderId)
        {
            // 取得訂單狀態
            var order = _dbContext.Orders.Find(orderId);
            // 判斷訂單是否已付款
            bool isPaid = order.IsSuccess == true;

            return isPaid;
        }
    }
}

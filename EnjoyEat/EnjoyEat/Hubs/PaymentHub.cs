using EnjoyEat.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace EnjoyEat.Hubs
{
    public class PaymentHub : Hub
    {
        private readonly db_a989fe_thm101team6Context _dbContext;

        public PaymentHub(db_a989fe_thm101team6Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task TrackPaymentStatus(string orderId)
        {
            // 取得訂單狀態
            var order = await _dbContext.Orders.FindAsync(orderId);
            // 判斷訂單是否已付款
            bool isPaid = order != null && order.IsSuccess == true;
            // 呼叫前端的 PaymentStatusChanged 方法
            await Clients.Caller.SendAsync("PaymentStatusChanged", isPaid);
        }
    }
}

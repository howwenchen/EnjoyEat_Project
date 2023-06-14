using EnjoyEat.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

public class PaymentHub : Hub
{
    private readonly db_a989fe_thm101team6Context _dbContext;
    private static ConcurrentDictionary<string, string> _usersConnections = new ConcurrentDictionary<string, string>();
    private readonly ILogger<PaymentHub> _logger;

    public PaymentHub(db_a989fe_thm101team6Context dbContext, ILogger<PaymentHub> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.GetHttpContext().Request.Query.TryGetValue("orderId", out var orderId))
        {
            _usersConnections.AddOrUpdate(orderId.ToString(), Context.ConnectionId, (key, oldValue) => Context.ConnectionId);
            await TrackPaymentStatus(orderId);

            // 紀錄連線資訊
            _logger.LogInformation($"Connection ID: {Context.ConnectionId} connected for order ID: {orderId}");
        }
        else
        {
            _logger.LogInformation("No order ID specified for the connection");
        }

        await base.OnConnectedAsync();
    }


    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var item = _usersConnections.FirstOrDefault(x => x.Value == Context.ConnectionId);
        if (item.Key != null)
        {
            _usersConnections.TryRemove(item.Key, out _);
        }

        return base.OnDisconnectedAsync(exception);
    }

    public async Task TrackPaymentStatus(string orderId)
    {
        Console.WriteLine($"TrackPaymentStatus called with orderId: {orderId}");

        bool isPaid = CheckPaymentStatus(orderId);
        if (_usersConnections.TryGetValue(orderId.ToString(), out var connectionId))
        {
            Console.WriteLine($"Sending PaymentStatusChanged to connection ID: {connectionId} with status: {isPaid}");
            await Clients.Client(connectionId).SendAsync("PaymentStatusChanged", isPaid);
        }
    }


    private bool CheckPaymentStatus(string orderId)
    {
        int id = int.Parse(orderId);
        var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == id);
        if (order != null)
        {
            // 先更新 IsSuccess 屬性
            order.IsSuccess = true;
            _dbContext.SaveChanges();

            // 再返回更新後的值
            return order.IsSuccess;
        }
        else
        {
            return false;
        }
    }

}

using EnjoyEat.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

public class PaymentHub : Hub
{
    private readonly db_a989fe_thm101team6Context _dbContext;
    //private static ConcurrentDictionary<string, string> _usersConnections = new ConcurrentDictionary<string, string>();
    private static ConcurrentDictionary<string, ConcurrentBag<string>> _usersConnections = new ConcurrentDictionary<string, ConcurrentBag<string>>();
    private readonly ILogger<PaymentHub> _logger;

    public PaymentHub(db_a989fe_thm101team6Context dbContext, ILogger<PaymentHub> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var query = Context.GetHttpContext().Request.Query;
        foreach (var param in query)
        {
            _logger.LogInformation($"Key: {param.Key}, Value: {param.Value}");
        }

        //從後台訂單管理來的連線獲取OrderId
        if (Context.GetHttpContext().Request.Query.TryGetValue("orderId", out var orderId))
        {
            var connectionIds = _usersConnections.GetOrAdd(orderId.ToString(), _ => new ConcurrentBag<string>());
            connectionIds.Add(Context.ConnectionId);
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
        var item = _usersConnections.FirstOrDefault(x => x.Value.Contains(Context.ConnectionId));
        if (item.Key != null)
        {
            item.Value.TryTake(out _);
        }

        return base.OnDisconnectedAsync(exception);
    }

    public async Task TrackPaymentStatus(string orderId)
    {
        Console.WriteLine($"TrackPaymentStatus called with orderId: {orderId}");

        //此orderId的訂單是否付款成功
        bool isPaid = CheckPaymentStatus(orderId);
        //如果此OrderId有已記錄的connectionId，則發送PaymentStatusChanged事件
        if (_usersConnections.TryGetValue(orderId.ToString(), out var connectionIds))
        {
            foreach (var connectionId in connectionIds)
            {
                Console.WriteLine($"Sending PaymentStatusChanged to connection ID: {connectionId} with status: {isPaid}");
                //發送PaymentStatusChanged事件
                //await Clients.Client(connectionId).SendAsync("PaymentStatusChanged", isPaid);
                await Clients.Client(connectionId).SendAsync("PaymentStatusChanged", isPaid);
            }
        }
        await Clients.All.SendAsync("PaymentStatusChanged", isPaid);
    }


    private bool CheckPaymentStatus(string orderId)
    {
        int id = int.Parse(orderId);
        var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == id);
        if (order != null)
        {
            return order.IsSuccess;
        }
        else
        {
            return false;
        }
    }

}

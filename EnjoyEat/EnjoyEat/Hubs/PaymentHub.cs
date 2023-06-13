using EnjoyEat.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Linq;

public class PaymentHub : Hub
{
    private readonly db_a989fe_thm101team6Context _dbContext;
    private static ConcurrentDictionary<string, string> _usersConnections = new ConcurrentDictionary<string, string>();

    public PaymentHub(db_a989fe_thm101team6Context dbContext)
    {
        _dbContext = dbContext;
    }

    public override Task OnConnectedAsync()
    {
        if (Context.GetHttpContext().Request.Query.TryGetValue("orderId", out var orderId))
        {
            _usersConnections.TryAdd(orderId, Context.ConnectionId);
        }

        return base.OnConnectedAsync();
    }


    public async Task TrackPaymentStatus(string orderId)
    {
        bool isPaid = CheckPaymentStatus(orderId);
        if (_usersConnections.TryGetValue(orderId.ToString(), out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("PaymentStatusChanged", isPaid);
        }
    }

    private bool CheckPaymentStatus(string orderId)
    {
        var order = _dbContext.Orders.Find(orderId);
        bool isPaid = order.IsSuccess == true;

        return isPaid;
    }
}

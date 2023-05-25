using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpPost]
    public IActionResult GenerateOrder([FromBody] OrderData orderData)
    {
        // 使用收到的訂單資料生成訂單
        Order order = new Order
        {
            // 訂單相關資料，例如訂單編號、日期等
        };

        foreach (var detailData in orderData.OrderDetails)
        {
            OrderDetail orderDetail = new OrderDetail
            {
                ProductId = detailData.ProductId,
                Quantity = (short)detailData.Quantity
            };

            order.OrderDetails.Add(orderDetail);
        }

        // 將訂單保存到資料庫中
        // ...

        return Ok("訂單生成成功");
    }
}

public class OrderData
{
    // 訂單相關資料的模型，根據你的需求定義屬性
    public List<OrderDetailData> OrderDetails { get; set; }
}

public class OrderDetailData
{
    // 每個商品的資料的模型，根據你的需求定義屬性
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

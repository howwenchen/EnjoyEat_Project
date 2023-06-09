﻿using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using EnjoyEat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
    [Route("api/OrderBackend/[action]")]
    [ApiController]
	[Authorize(Roles = "manager,staff")]
	public class OrderAPIController : Controller
    {
        private readonly db_a989fe_thm101team6Context db;
        public OrderAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        [HttpGet]
        //取得訂單資料
        public async Task<IQueryable<OrderViewModel>> GetOrder()
        {
            return db.Orders.Select(x => new OrderViewModel
            {
                OrderId = x.OrderId,
                MemberId = x.MemberId,
                OrderDate = x.OrderDate,
                TableId = x.TableId,
                CustomerCount = x.CustomerCount,
                IsTakeway = x.IsTakeway,
                IsSuccess = x.IsSuccess,
                LevelDiscount = x.LevelDiscount,
                TotalPrice = x.TotalPrice,
                FinalPrice = x.FinalPrice,
                CountPrice=x.CountPrice,
            });
        }

        [HttpGet("{orderId}")]
        //取得訂單明細
        public async Task<IQueryable<MemberOrderDetailViewModel>> GetOrderDetail(int orderId)
        {
            return db.OrderDetails.Include(x => x.Product).Where(o => o.OrderId == orderId).Select(od => new MemberOrderDetailViewModel
            {
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                OrderDetailId =od.OrderDetailId,
                Quantity = (short)od.Quantity,
                UnitPrice = od.UnitPrice,
                Discount = od.Discount,
                SubtotalPrice = od.SubtotalPrice,
                ProductName = od.Product.ProductName,
            });
        }
        [HttpPut("{OrderId}")]
        //修改訂單狀態
        public async Task<string> EditPayStatus(int OrderId)
        {
            var order=db.Orders.FirstOrDefault(x=>x.OrderId ==OrderId);
            order.IsSuccess= true;
            db.SaveChanges();
            return "成功";
        }

        //修改訂單
        [HttpPut]
        public async Task<string> EditOrderDetail(BackendOrderDetailViewModel model)
        {
            var order = db.OrderDetails.FirstOrDefault(x => x.OrderDetailId == model.OrderDetailId);
            order.OrderDetailId=model.OrderDetailId;
            order.Quantity=model.Quantity;
            order.Discount=model.Discount;
            order.SubtotalPrice=model.SubtotalPrice;
            db.SaveChanges();
            return "成功";
        }
        [HttpDelete("{orderDetailId}")]
        public async Task<string> DeleteOrderDetail(int orderDetailId)
        {
            var order = db.OrderDetails.FirstOrDefault(x => x.OrderDetailId == orderDetailId);
            db.Remove(order);
            db.SaveChanges();
            return "刪除成功!";
        }
    }
}

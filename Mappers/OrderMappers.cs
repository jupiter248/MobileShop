using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Order;
using MainApi.Dtos.Orders.Order;
using MainApi.Models;
using MainApi.Models.Orders;

namespace MainApi.Mappers
{
    public static class OrderMappers
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                UserName = order.User.UserName,
                StatusName = order.OrderStatus.StatusName,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(i => i.ToOrderItemDto()).ToList()
            };
        }
        public static Order ToOrderFromAdd(this AddOrderRequestDto addOrderRequestDto)
        {
            return new Order()
            {
                StatusId = addOrderRequestDto.StatusId,
                OrderItems = addOrderRequestDto.OrderItems.Select(i => i.ToOrderItemFromAdd()).ToList()
            };
        }
    }
}
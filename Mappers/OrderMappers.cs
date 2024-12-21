using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Order;
using MainApi.Models;

namespace MainApi.Mappers
{
    public static class OrderMappers
    {
        public static OrderDto ToOrderDto(this Order order, OrderStatus orderStatus)
        {
            return new OrderDto()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId,
                StatusName = orderStatus.StatusName,
                orderItemDtos = order.OrderItems.Select(i => i.ToOrderDto()).ToList()
            };
        }
    }
}
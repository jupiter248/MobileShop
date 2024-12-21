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
        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                UserId = order.UserId,
                OrderId = order.StatusId,
                orderItemDtos = order.OrderItems.Select(i => i.ToOrderItemDto()).ToList()
            };
        }
    }
}
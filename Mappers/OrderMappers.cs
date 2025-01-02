using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Order;
using MainApi.Dtos.Orders.Order;
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
                OrderItems = order.OrderItems.Select(i => i.ToOrderItemDto()).ToList()
            };
        }
        public static Order ToOrderFromAdd(this AddOrderRequestDto addOrderRequestDto)
        {
            return new Order()
            {
                TotalAmount = addOrderRequestDto.TotalAmount,
                OrderItems = addOrderRequestDto.OrderItems.Select(i => i.ToOrderItemFromAdd()).ToList()
            };
        }
    }
}
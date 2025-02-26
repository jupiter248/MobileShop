using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders.OrderItem;
using MainApi.Models;
using MainApi.Models.Orders;

namespace MainApi.Mappers
{
    public static class OrderItemMappers
    {
        public static OrderItemDto ToOrderItemDto(this OrderItem orderItem)
        {
            return new OrderItemDto()
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                PriceAtPurchase = orderItem.PriceAtPurchase,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity
            };
        }
        public static OrderItem ToOrderItemFromAdd(this AddOrderItemRequestDto addOrderItemRequestDto)
        {
            return new OrderItem()
            {
                Quantity = addOrderItemRequestDto.Quantity,
                ProductId = addOrderItemRequestDto.ProductId
            };
        }
    }
}
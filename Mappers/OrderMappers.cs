using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders;
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
                Address = order.Address.ToAddressDto(order.User.UserName),
                OrderDate = order.OrderDate,
                StatusName = order.OrderStatus.StatusName,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(i => i.ToOrderItemDto()).ToList()
            };
        }
        public static OrderItem ToOrderItem(this CartItem cartItem)
        {
            return new OrderItem()
            {
                PriceAtPurchase = cartItem.TotalPrice,
                ProductId = cartItem.ProductId,
                Product = cartItem.Product,
                Quantity = cartItem.Quantity,
                AttributeXml = cartItem.AttributeXml
            };
        }
        public static OrderItemDto ToOrderItemDto(this OrderItem orderItem)
        {
            return new OrderItemDto()
            {
                Id = orderItem.Id,
                AttributeXml = orderItem.AttributeXml,
                OrderId = orderItem.OrderId,
                Quantity = orderItem.Quantity,
                ProductId = orderItem.ProductId,
                PriceAtPurchase = orderItem.PriceAtPurchase
            };
        }
    }
}
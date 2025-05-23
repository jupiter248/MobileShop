using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Orders.Order;
using MainApi.Domain.Models;
using MainApi.Domain.Models.Orders;

namespace MainApi.Application.Mappers
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
        public static OrderStatusesDto ToOrderStatusDto(this OrderStatus orderStatus)
        {
            return new OrderStatusesDto()
            {
                Id = orderStatus.Id,
                Name = orderStatus.StatusName,
                Description = orderStatus.Description
            };
        }
        public static OrderStatus ToOrderStatus(this AddOrderStatusRequestDto statusRequestDto)
        {
            return new OrderStatus()
            {
                StatusName = statusRequestDto.Name,
                Description = statusRequestDto.Description
            };
        }
    }
}
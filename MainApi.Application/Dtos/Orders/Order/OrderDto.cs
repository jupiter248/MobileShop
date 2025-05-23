using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Application.Dtos.Address;
using MainApi.Application.Dtos.Orders.Order;
using MainApi.Domain.Models.Orders;

namespace MainApi.Application.Dtos.Orders.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public required string StatusName { get; set; }
        public required decimal TotalAmount { get; set; }
        public required AddressDto Address { get; set; }
        public required DateTime OrderDate { get; set; }
        public required List<OrderItemDto> OrderItems { get; set; }
    }
}
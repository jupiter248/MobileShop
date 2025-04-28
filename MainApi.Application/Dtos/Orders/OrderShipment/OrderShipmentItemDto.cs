using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Orders;

namespace MainApi.Application.Dtos.Orders.OrderShipment
{
    public class OrderShipmentItemDto
    {
        public int id { get; set; }
        public required int OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Orders;

namespace MainApi.Dtos.Orders.OrderShipment
{
    public class OrderShipmentItemDto
    {
        public int id { get; set; }
        public required int OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
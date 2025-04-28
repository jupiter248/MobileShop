using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Orders.OrderShipment
{
    public class AddShipmentRequestDto
    {
        public required int OrderId { get; set; }
        public required string TrackingNumber { get; set; }
        public required decimal TotalWeight { get; set; }
        public DateTime? ShippedDate { get; set; }
        public required string StatusName { get; set; }
        public required List<int> OrderItemsIds { get; set; }
    }
}
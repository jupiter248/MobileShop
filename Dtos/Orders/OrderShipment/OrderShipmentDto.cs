using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Orders;

namespace MainApi.Dtos.Orders.OrderShipment
{
    public class OrderShipmentDto
    {
        [Key]
        public int Id { get; set; }
        public required int OrderId { get; set; }
        public required string TrackingNumber { get; set; }
        public required decimal TotalWeight { get; set; }
        public DateTime ShippedDate { get; set; }
        public DateTime DeliveredDate { get; set; }
        public required string ShippingStatusName { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<OrderShipmentItemDto>? ShipmentItemDtos { get; set; }
    }
}
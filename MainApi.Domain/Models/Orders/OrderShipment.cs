using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Orders
{
    public class OrderShipment
    {
        [Key]
        public int Id { get; set; }
        public required int OrderId { get; set; }
        public Order? Order { get; set; }
        public required string TrackingNumber { get; set; }
        public required decimal TotalWeight { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public required int ShippingStatusId { get; set; }
        public required ShippingStatus ShippingStatus { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<ShipmentItem> ShipmentItems { get; set; } = new List<ShipmentItem>();
    }
}
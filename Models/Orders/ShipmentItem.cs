using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Orders
{
    public class ShipmentItem
    {
        [Key]
        public int Id { get; set; }
        public  int? OrderShipmentId { get; set; }
        public required int OrderItemId { get; set; }
        public OrderItem? OrderItem { get; set; }
    }
}
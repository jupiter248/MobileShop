using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Orders
{
    public class ShippingStatus
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<OrderShipment> OrderShipment { get; set; } = new List<OrderShipment>();

    }
}
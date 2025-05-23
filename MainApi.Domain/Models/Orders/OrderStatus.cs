using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Orders
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public required string StatusName { get; set; }
        public required string Description { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
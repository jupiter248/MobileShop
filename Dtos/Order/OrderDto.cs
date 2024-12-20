using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public int StatusId { get; set; }
    }
}
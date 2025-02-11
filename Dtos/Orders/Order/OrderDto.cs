using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Dtos.Orders.OrderItem;

namespace MainApi.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? StatusName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
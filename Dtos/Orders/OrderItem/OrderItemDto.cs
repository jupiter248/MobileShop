using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models;

namespace MainApi.Dtos.Orders.OrderItem
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}
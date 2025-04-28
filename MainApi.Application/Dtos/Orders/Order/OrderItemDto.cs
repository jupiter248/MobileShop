using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models;

namespace MainApi.Application.Dtos.Orders.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public  int? OrderId { get; set; }
        public required int ProductId { get; set; }
        public required string AttributeXml { get; set; }
    }
}
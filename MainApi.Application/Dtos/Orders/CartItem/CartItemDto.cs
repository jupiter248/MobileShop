using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Dtos.Orders.CartItem
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public required string AttributeXml { get; set; }
        public required Product Product { get; set; }
    }
}
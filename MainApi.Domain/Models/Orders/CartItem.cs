using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products;
using MainApi.Domain.Models.User;

namespace MainApi.Domain.Models.Orders
{
    public class CartItem
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required AppUser AppUser { get; set; }
        public required int ProductId { get; set; }
        public required Product Product { get; set; }
        public required int Quantity { get; set; }
        public required string AttributeXml { get; set; }
        public required decimal BasePrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
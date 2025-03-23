using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products;
using MainApi.Models.User;

namespace MainApi.Models.Orders
{
    public class CartItem
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public required int Quantity { get; set; }
        public required string AttributeXml { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
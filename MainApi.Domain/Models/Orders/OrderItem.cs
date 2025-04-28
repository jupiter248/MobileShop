using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products;

namespace MainApi.Domain.Models.Orders
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public required string AttributeXml { get; set; }

    }
}
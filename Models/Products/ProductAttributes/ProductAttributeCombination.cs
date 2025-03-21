using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Products.ProductAttributes
{
    public class ProductAttributeCombination
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public required string AttributeCombination { get; set; }
        public required int Quantity { get; set; }
        public required decimal FinalPrice { get; set; }
        public required string Sku { get; set; }
    }
}
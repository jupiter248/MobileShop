using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Products.ProductAttributes
{
    public class ProductCombination
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public required int Quantity { get; set; }
        public required decimal FinalPrice { get; set; }
        public required string Sku { get; set; }
        public List<ProductCombinationAttribute>? CombinationAttributes { get; set; }
    }
}
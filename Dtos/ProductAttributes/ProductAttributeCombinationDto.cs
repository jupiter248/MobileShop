using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products;

namespace MainApi.Dtos.ProductAttributes
{
    public class ProductAttributeCombinationDto
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required List<int> AttributeCombination { get; set; }
        public required int Quantity { get; set; }
        public required decimal FinalPrice { get; set; }
        public required string Sku { get; set; }
    }
}
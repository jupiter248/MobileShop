using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products;

namespace MainApi.Dtos.ProductAttributes
{
    public class ProductCombinationDto
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required decimal FinalPrice { get; set; }
        public Dictionary<string, string>? Attributes { get; set; }
        public List<ColorOptionDto>? AvailableColors { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products;

namespace MainApi.Application.Dtos.ProductAttributes
{
    public class ProductCombinationDto
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
            public Dictionary<string, string>? Attributes { get; set; }
        public List<ColorOptionDto>? AvailableColors { get; set; }
    }
}
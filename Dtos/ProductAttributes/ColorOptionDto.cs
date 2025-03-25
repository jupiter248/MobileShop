using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.ProductAttributes
{
    public class ColorOptionDto
    {
        public required string Name { get; set; }
        public required int Stock { get; set; }
        public required decimal Price { get; set; }
        public required string Sku { get; set; }
    }
}
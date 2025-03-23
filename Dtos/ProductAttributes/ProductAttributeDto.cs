using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Products.ProductAttributes;

namespace MainApi.Dtos.ProductAttributes
{
    public class ProductAttributeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<PredefinedProductAttributeValueDto>? Values { get; set; }
    }
}
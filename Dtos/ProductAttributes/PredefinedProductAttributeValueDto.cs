using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.ProductAttributes
{
    public class PredefinedProductAttributeValueDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
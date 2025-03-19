using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.SpecificationAttributes
{
    public class ProductSpecificationAttributeDto
    {
        public required string SpecificationName { get; set; }
        public required string SpecificationValue { get; set; }
    }
}
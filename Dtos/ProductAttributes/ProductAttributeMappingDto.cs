using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.ProductAttributes
{
    public class ProductAttributeMappingDto
    {
        public int Id { get; set; }
        public bool IsRequired { get; set; }
        public required ProductAttributeDto Attribute { get; set; }


    }
}
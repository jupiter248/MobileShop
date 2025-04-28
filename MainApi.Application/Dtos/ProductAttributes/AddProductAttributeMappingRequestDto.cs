using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.ProductAttributes
{
    public class AddProductAttributeMappingRequestDto
    {
        public required int ProductId { get; set; }
        public required int ProductAttributeId { get; set; }
        public required bool IsRequired { get; set; }

    }
}
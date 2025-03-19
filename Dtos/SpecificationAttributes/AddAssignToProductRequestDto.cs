using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.SpecificationAttributes
{
    public class AddAssignToProductRequestDto
    {
        public required int ProductId { get; set; }
        public required int SpecificationAttributeOptionId { get; set; }
        public required bool AllowFiltering { get; set; }
        public required bool ShowOnProductPage { get; set; }
    }
}
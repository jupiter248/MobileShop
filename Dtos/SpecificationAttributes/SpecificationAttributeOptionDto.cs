using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.SpecificationAttributes
{
    public class SpecificationAttributeOptionDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
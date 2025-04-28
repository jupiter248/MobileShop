using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Domain.Models.Products.SpecificationAttributes;

namespace MainApi.Application.Dtos.SpecificationAttributes
{
    public class SpecificationAttributeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<SpecificationAttributeOptionDto>? Options { get; set; }
    }
}
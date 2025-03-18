using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Products.SpecificationAttributes
{
    public class SpecificationAttribute
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        List<SpecificationAttributeOption> SpecificationAttributeOptions { get; set; } = new List<SpecificationAttributeOption>();
    }
}
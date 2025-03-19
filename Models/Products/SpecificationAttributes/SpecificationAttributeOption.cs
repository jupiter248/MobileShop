using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Products.SpecificationAttributes
{
    public class SpecificationAttributeOption
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int SpecificationAttributeId { get; set; }
        public required SpecificationAttribute SpecificationAttribute { get; set; }
        public List<Product_SpecificationAttribute_Mapping> Product_SpecificationAttribute_Mappings { get; set; } = new List<Product_SpecificationAttribute_Mapping>();
    }
}
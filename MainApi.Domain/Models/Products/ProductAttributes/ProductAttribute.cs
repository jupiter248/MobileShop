using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Products.ProductAttributes
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<PredefinedProductAttributeValue> PredefinedProductAttributeValues { get; set; } = new List<PredefinedProductAttributeValue>();
        public List<ProductAttributeMapping> Product_ProductAttribute_Mappings { get; set; } = new List<ProductAttributeMapping>();
    }
}
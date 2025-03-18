using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Products.ProductAttributes
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<PredefinedProductAttributeValue> PredefinedProductAttributeValues { get; set; } = new List<PredefinedProductAttributeValue>();
        public List<Product_ProductAttribute_Mapping> Product_ProductAttribute_Mappings { get; set; } = new List<Product_ProductAttribute_Mapping>();
    }
}
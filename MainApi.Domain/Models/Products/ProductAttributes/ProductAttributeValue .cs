using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Products.ProductAttributes
{
    public class ProductAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public int ProductAttributeMappingId { get; set; }
        public ProductAttributeMapping? productAttributeMapping { get; set; }
        public string? Name { get; set; }
        public decimal PriceAdjustment { get; set; }
    }
}
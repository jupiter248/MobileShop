using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Products.ProductAttributes
{
    public class ProductAttributeMapping
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int ProductAttributeId { get; set; }
        public ProductAttribute? ProductAttribute { get; set; }
        public bool IsRequired { get; set; }
        public List<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
    }
}
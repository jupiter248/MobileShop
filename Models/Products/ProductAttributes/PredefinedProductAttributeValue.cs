using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.Products.ProductAttributes
{
    public class PredefinedProductAttributeValue
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ProductAttributeId { get; set; }
        public ProductAttribute? ProductAttribute { get; set; }
    }
}
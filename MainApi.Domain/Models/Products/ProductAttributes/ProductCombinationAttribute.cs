using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Products.ProductAttributes
{
    public class ProductCombinationAttribute
    {
        public int Id { get; set; }
        public int ProductCombinationId { get; set; }
        public int AttributeValueId { get; set; }
        public ProductCombination? ProductCombination { get; set; }
        public PredefinedProductAttributeValue? AttributeValue { get; set; }
    }
}
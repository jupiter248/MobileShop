using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Domain.Models.Products.SpecificationAttributes
{
    public class Product_SpecificationAttribute_Mapping
    {
        [Key]
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public required int SpecificationAttributeOptionId { get; set; }
        public SpecificationAttributeOption? SpecificationAttributeOption { get; set; }
        public bool AllowFiltering { get; set; }
        public bool ShowOnProductPage { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.ProductAttributes
{
    public class AddPredefinedProductAttributeValueRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Predefined attribute name can not be over 25 character")]
        public required string Name { get; set; }
        [Required]
        public required int ProductAttributeId { get; set; }
    }
}
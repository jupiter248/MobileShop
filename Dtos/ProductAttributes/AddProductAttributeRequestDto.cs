using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.ProductAttributes
{
    public class AddProductAttributeRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Attribute name can not be over 25 character")]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.SpecificationAttributes
{
    public class AddSpecificationAttributeRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Specification can not be over 25 character")]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
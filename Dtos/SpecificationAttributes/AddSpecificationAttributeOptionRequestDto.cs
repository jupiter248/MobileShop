using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.SpecificationAttributes
{
    public class AddSpecificationAttributeOptionRequestDto
    {
        [MaxLength(25, ErrorMessage = "AttributeName can not be over 25 character")]
        public required string AttributeName { get; set; }
        [MaxLength(25, ErrorMessage = "AttributeValue can not be over 25 character")]
        public required string AttributeValue { get; set; }
    }
}
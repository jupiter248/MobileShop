using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Application.Dtos.Address
{
    public class EditAddressRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Country name can not be over 25 characters")]
        public required string Country { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "City name can not be over 25 characters")]
        public required string City { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "State name can not be over 25 characters")]
        public required string State { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Street name can not be over 25 characters")]
        public required string Street { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Plate name can not be over 10 characters")]
        public required string Plate { get; set; }
        public required string PostalCode { get; set; }
    }
}
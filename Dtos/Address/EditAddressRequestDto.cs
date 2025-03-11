using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Address
{
    public class EditAddressRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Country name can not be over 25 characters")]
        public string? Country { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "City name can not be over 25 characters")]
        public string? City { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "State name can not be over 25 characters")]
        public string? State { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Street name can not be over 25 characters")]
        public string? Street { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Plate name can not be over 10 characters")]
        public string? Plate { get; set; }
        public string? PostalCode { get; set; }
    }
}
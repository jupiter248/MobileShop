using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Dtos.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public string? Plate { get; set; }
        public string? PostalCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
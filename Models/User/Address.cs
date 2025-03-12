using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApi.Models.User
{
    public class Address
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public AppUser appUser { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public string? Plate { get; set; }
        public string? PostalCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
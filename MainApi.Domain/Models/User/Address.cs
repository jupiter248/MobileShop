using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using MainApi.Domain.Models.Orders;

namespace MainApi.Domain.Models.User
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public  AppUser? appUser { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Street { get; set; }
        public required string Plate { get; set; }
        public required string PostalCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
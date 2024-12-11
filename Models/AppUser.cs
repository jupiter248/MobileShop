using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
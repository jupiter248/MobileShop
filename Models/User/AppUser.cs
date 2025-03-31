using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainApi.Models.Orders;
using MainApi.Models.Payments;
using MainApi.Models.Products;
using Microsoft.AspNetCore.Identity;

namespace MainApi.Models.User
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<WishList> WishLists { get; set; } = new List<WishList>();
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public List<Payment> Payments { get; set; } = new List<Payment>();


    }
}